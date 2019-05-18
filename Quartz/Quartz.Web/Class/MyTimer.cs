using System;
using System.IO;
using System.Threading.Tasks;

namespace Quartz.Web.Class
{
    public class MyTimer : IJob
    {
        Task IJob.Execute(IJobExecutionContext context)
        {
            var filePath = AppDomain.CurrentDomain.BaseDirectory + "\\Log\\Quartz_.html";
            if (!File.Exists(filePath)) File.Create(filePath).Close();

            const string preFix = "<hr style='border:1px solid red;'>";
            File.AppendAllText(filePath, preFix + DateTime.Now + Environment.NewLine);

            return Task.FromResult(true);
        }
    }
}