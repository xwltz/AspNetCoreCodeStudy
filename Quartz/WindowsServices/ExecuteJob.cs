using System;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;
using Quartz;

namespace WindowsServices
{
    internal class ExecuteJob : IJob
    {
        /// <inheritdoc />
        /// <summary>
        /// 执行调度任务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task IJob.Execute(IJobExecutionContext context)
        {
            //var basePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            //var rootpath = basePath.Substring(0, basePath.LastIndexOf("\\bin\\", StringComparison.Ordinal));
            const string filePath = "D:\\execute.log";

            try
            {
                if (!File.Exists(filePath)) File.Create(filePath).Close();

                var execSql = $"DELETE FROM [dbo].[ArticleLog] where createTime BETWEEN '{DateTime.Now:yyyy-MM-dd 00:00:00}' and '{DateTime.Now:yyyy-MM-dd 23:59:59}'";
                var rowCount = DbHelper.ExecuteSql(execSql);

                var nextTime = context.NextFireTimeUtc == null ? DateTime.Now.AddHours(1).ToString("yyyy-MM-dd HH:mm:ss") : context.NextFireTimeUtc.Value.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
                if (rowCount > 0)
                {
                    File.AppendAllText(filePath, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + $" 清除文章记录：【{rowCount}】行 - 下次执行时间：{nextTime}" + Environment.NewLine);
                }
                else
                {
                    File.AppendAllText(filePath, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + $" 【找不到需要清除的文章记录】 - 下次执行时间：{nextTime}" + Environment.NewLine);
                }

                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                File.AppendAllText(filePath, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " [" + ex.Message + "]" + Environment.NewLine);
                throw;
            }
        }
    }

    public abstract class DbHelper
    {
        protected static string ConnStr = "Data Source = 47.98.97.153;Initial Catalog = vy_aiseo_com;User Id = vy_aiseo_com;Password = Pki4hPjSam;";
        public static int ExecuteSql(string sqlStr)
        {
            using (SqlConnection connection = new SqlConnection(ConnStr))
            {
                using (SqlCommand cmd = new SqlCommand(sqlStr, connection))
                {
                    try
                    {
                        connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (SqlException ex)
                    {
                        connection.Close();
                        throw new Exception(ex.Message);
                    }
                }
            }
        }
    }
}
