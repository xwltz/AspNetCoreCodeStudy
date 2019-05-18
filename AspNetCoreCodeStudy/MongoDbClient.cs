using MongoDB.Driver;

namespace AspNetCoreCodeStudy
{
    public static class MongoDbClient<T> where T : class
    {
        #region MongoDbInfoClient 获取mongodb实例
        /// <summary>
        /// 获取mongodb实例
        /// </summary>
        /// <param name="host">连接字符串，库，表</param>
        /// <returns></returns>
        /// 
        public static IMongoCollection<T> MongodbInfoClient(MongoDbHost host)
        {
            var client = new MongoClient(host.Connection);
            var dataBase = client.GetDatabase(host.DataBase);
            return dataBase.GetCollection<T>(host.Table);
        }
        #endregion
    }

    public class MongoDbHost
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string Connection { get; set; } = "mongodb://192.168.11.5:27017";

        /// <summary>
        /// 库
        /// </summary>
        public string DataBase { get; set; } = "KeyWordArticle_1";

        /// <summary>
        /// 表
        /// </summary>
        public string Table { get; set; } = "ArticleTable_1";
    }
}
