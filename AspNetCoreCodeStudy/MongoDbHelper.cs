using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
// ReSharper disable UnusedMember.Global
// ReSharper disable PossibleIntendedRethrow

namespace AspNetCoreCodeStudy
{
    public static class MongoDbHelper<TEntity> where TEntity : class, new()
    {
        #region +Add 添加一条数据
        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <param name="t">添加的实体</param>
        /// <param name="host">mongodb连接信息</param>
        /// <returns></returns>
        public static int Add(MongoDbHost host, TEntity t)
        {
            try
            {
                var client = MongoDbClient<TEntity>.MongodbInfoClient(host);
                client.InsertOne(t);
                return 1;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region +AddAsync 异步添加一条数据
        /// <summary>
        /// 异步添加一条数据
        /// </summary>
        /// <param name="t">添加的实体</param>
        /// <param name="host">mongodb连接信息</param>
        /// <returns></returns>
        public static async Task<int> AddAsync(MongoDbHost host, TEntity t)
        {
            try
            {
                var client = MongoDbClient<TEntity>.MongodbInfoClient(host);
                await client.InsertOneAsync(t);
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region +InsertMany 批量插入
        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="host">mongodb连接信息</param>
        /// <param name="t">实体集合</param>
        /// <returns></returns>
        public static int InsertMany(MongoDbHost host, List<TEntity> t)
        {
            try
            {
                var client = MongoDbClient<TEntity>.MongodbInfoClient(host);
                client.InsertMany(t);
                return t.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region +InsertManyAsync 异步批量插入
        /// <summary>
        /// 异步批量插入
        /// </summary>
        /// <param name="host">mongodb连接信息</param>
        /// <param name="t">实体集合</param>
        /// <returns></returns>
        public static async Task<int> InsertManyAsync(MongoDbHost host, List<TEntity> t)
        {
            try
            {
                var client = MongoDbClient<TEntity>.MongodbInfoClient(host);
                await client.InsertManyAsync(t);
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region +Update 修改一条数据

        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <param name="t">添加的实体</param>
        /// <param name="host">mongodb连接信息</param>
        /// <param name="id">objectId</param>
        /// <returns></returns>
        public static UpdateResult Update(MongoDbHost host, TEntity t, string id)
        {
            try
            {
                var client = MongoDbClient<TEntity>.MongodbInfoClient(host);
                //修改条件
                FilterDefinition<TEntity> filter = Builders<TEntity>.Filter.Eq("_id", new ObjectId(id));
                //要修改的字段
                var list = new List<UpdateDefinition<TEntity>>();
                foreach (var item in t.GetType().GetProperties())
                {
                    if (item.Name.ToLower() == "id") continue;
                    list.Add(Builders<TEntity>.Update.Set(item.Name, item.GetValue(t)));
                }
                var updatefilter = Builders<TEntity>.Update.Combine(list);
                return client.UpdateOne(filter, updatefilter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region +UpdateAsync 异步修改一条数据

        /// <summary>
        /// 异步修改一条数据
        /// </summary>
        /// <param name="t">添加的实体</param>
        /// <param name="host">mongodb连接信息</param>
        /// <param name="id">objectId</param>
        /// <returns></returns>
        public static async Task<UpdateResult> UpdateAsync(MongoDbHost host, TEntity t, string id)
        {
            try
            {
                var client = MongoDbClient<TEntity>.MongodbInfoClient(host);
                //修改条件
                FilterDefinition<TEntity> filter = Builders<TEntity>.Filter.Eq("_id", new ObjectId(id));
                //要修改的字段
                var list = new List<UpdateDefinition<TEntity>>();
                foreach (var item in t.GetType().GetProperties())
                {
                    if (item.Name.ToLower() == "id") continue;
                    list.Add(Builders<TEntity>.Update.Set(item.Name, item.GetValue(t)));
                }
                var updatefilter = Builders<TEntity>.Update.Combine(list);
                return await client.UpdateOneAsync(filter, updatefilter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region +UpdateManay 批量修改数据
        /// <summary>
        /// 批量修改数据
        /// </summary>
        /// <param name="dic">要修改的字段</param>
        /// <param name="host">mongodb连接信息</param>
        /// <param name="filter">修改条件</param>
        /// <returns></returns>
        public static UpdateResult UpdateManay(MongoDbHost host, Dictionary<string, string> dic, FilterDefinition<TEntity> filter)
        {
            try
            {
                var client = MongoDbClient<TEntity>.MongodbInfoClient(host);
                TEntity t = new TEntity();
                //要修改的字段
                var list = new List<UpdateDefinition<TEntity>>();
                foreach (var item in t.GetType().GetProperties())
                {
                    if (!dic.ContainsKey(item.Name)) continue;
                    var value = dic[item.Name];
                    list.Add(Builders<TEntity>.Update.Set(item.Name, value));
                }
                var updatefilter = Builders<TEntity>.Update.Combine(list);
                return client.UpdateMany(filter, updatefilter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region +UpdateManayAsync 异步批量修改数据
        /// <summary>
        /// 异步批量修改数据
        /// </summary>
        /// <param name="dic">要修改的字段</param>
        /// <param name="host">mongodb连接信息</param>
        /// <param name="filter">修改条件</param>
        /// <returns></returns>
        public static async Task<UpdateResult> UpdateManayAsync(MongoDbHost host, Dictionary<string, string> dic, FilterDefinition<TEntity> filter)
        {
            try
            {
                var client = MongoDbClient<TEntity>.MongodbInfoClient(host);
                TEntity t = new TEntity();
                //要修改的字段
                var list = new List<UpdateDefinition<TEntity>>();
                foreach (var item in t.GetType().GetProperties())
                {
                    if (!dic.ContainsKey(item.Name)) continue;
                    var value = dic[item.Name];
                    list.Add(Builders<TEntity>.Update.Set(item.Name, value));
                }
                var updatefilter = Builders<TEntity>.Update.Combine(list);
                return await client.UpdateManyAsync(filter, updatefilter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Delete 删除一条数据
        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="host">mongodb连接信息</param>
        /// <param name="id">objectId</param>
        /// <returns></returns>
        public static DeleteResult Delete(MongoDbHost host, string id)
        {
            try
            {
                var client = MongoDbClient<TEntity>.MongodbInfoClient(host);
                FilterDefinition<TEntity> filter = Builders<TEntity>.Filter.Eq("_id", new ObjectId(id));
                return client.DeleteOne(filter);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region DeleteAsync 异步删除一条数据
        /// <summary>
        /// 异步删除一条数据
        /// </summary>
        /// <param name="host">mongodb连接信息</param>
        /// <param name="id">objectId</param>
        /// <returns></returns>
        public static async Task<DeleteResult> DeleteAsync(MongoDbHost host, string id)
        {
            try
            {
                var client = MongoDbClient<TEntity>.MongodbInfoClient(host);
                //修改条件
                FilterDefinition<TEntity> filter = Builders<TEntity>.Filter.Eq("_id", new ObjectId(id));
                return await client.DeleteOneAsync(filter);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region DeleteMany 删除多条数据
        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="host">mongodb连接信息</param>
        /// <param name="filter">删除的条件</param>
        /// <returns></returns>
        public static DeleteResult DeleteMany(MongoDbHost host, FilterDefinition<TEntity> filter)
        {
            try
            {
                var client = MongoDbClient<TEntity>.MongodbInfoClient(host);
                return client.DeleteMany(filter);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region DeleteManyAsync 异步删除多条数据
        /// <summary>
        /// 异步删除多条数据
        /// </summary>
        /// <param name="host">mongodb连接信息</param>
        /// <param name="filter">删除的条件</param>
        /// <returns></returns>
        public static async Task<DeleteResult> DeleteManyAsync(MongoDbHost host, FilterDefinition<TEntity> filter)
        {
            try
            {
                var client = MongoDbClient<TEntity>.MongodbInfoClient(host);
                return await client.DeleteManyAsync(filter);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region Count 根据条件获取总数
        /// <summary>
        /// 根据条件获取总数
        /// </summary>
        /// <param name="host">mongodb连接信息</param>
        /// <param name="filter">条件</param>
        /// <returns></returns>
        public static long Count(MongoDbHost host, FilterDefinition<TEntity> filter)
        {
            try
            {
                var client = MongoDbClient<TEntity>.MongodbInfoClient(host);
                return client.CountDocuments(filter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region CountAsync 异步根据条件获取总数
        /// <summary>
        /// 异步根据条件获取总数
        /// </summary>
        /// <param name="host">mongodb连接信息</param>
        /// <param name="filter">条件</param>
        /// <returns></returns>
        public static async Task<long> CountAsync(MongoDbHost host, FilterDefinition<TEntity> filter)
        {
            try
            {
                var client = MongoDbClient<TEntity>.MongodbInfoClient(host);
                return await client.CountDocumentsAsync(filter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region FindOne 根据id查询一条数据
        /// <summary>
        /// 根据id查询一条数据
        /// </summary>
        /// <param name="host">mongodb连接信息</param>
        /// <param name="id">objectid</param>
        /// <param name="field">要查询的字段，不写时查询全部</param>
        /// <returns></returns>
        public static TEntity FindOne(MongoDbHost host, string id, string[] field = null)
        {
            try
            {
                var client = MongoDbClient<TEntity>.MongodbInfoClient(host);
                FilterDefinition<TEntity> filter = Builders<TEntity>.Filter.Eq("_id", new ObjectId(id));
                //不指定查询字段
                if (field == null || field.Length == 0)
                {
                    return client.Find(filter).FirstOrDefault<TEntity>();
                }

                //制定查询字段
                var fieldList = new List<ProjectionDefinition<TEntity>>();
                foreach (var t in field)
                {
                    fieldList.Add(Builders<TEntity>.Projection.Include(t));
                }
                var projection = Builders<TEntity>.Projection.Combine(fieldList);
                fieldList.Clear();
                return client.Find(filter).Project<TEntity>(projection).FirstOrDefault<TEntity>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region FindOneAsync 异步根据id查询一条数据

        /// <summary>
        /// 异步根据id查询一条数据
        /// </summary>
        /// <param name="host">mongodb连接信息</param>
        /// <param name="id">objectid</param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static async Task<TEntity> FindOneAsync(MongoDbHost host, string id, string[] field = null)
        {
            try
            {
                var client = MongoDbClient<TEntity>.MongodbInfoClient(host);
                FilterDefinition<TEntity> filter = Builders<TEntity>.Filter.Eq("_id", new ObjectId(id));
                //不指定查询字段
                if (field == null || field.Length == 0)
                {
                    return await client.Find(filter).FirstOrDefaultAsync();
                }

                //制定查询字段
                var fieldList = new List<ProjectionDefinition<TEntity>>();
                foreach (var t in field)
                {
                    fieldList.Add(Builders<TEntity>.Projection.Include(t));
                }
                var projection = Builders<TEntity>.Projection.Combine(fieldList);
                fieldList.Clear();
                return await client.Find(filter).Project<TEntity>(projection).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region FindList 查询集合
        /// <summary>
        /// 查询集合
        /// </summary>
        /// <param name="host">mongodb连接信息</param>
        /// <param name="filter">查询条件</param>
        /// <param name="field">要查询的字段,不写时查询全部</param>
        /// <param name="sort">要排序的字段</param>
        /// <returns></returns>
        public static List<TEntity> FindList(MongoDbHost host, FilterDefinition<TEntity> filter, string[] field = null, SortDefinition<TEntity> sort = null)
        {
            try
            {
                var client = MongoDbClient<TEntity>.MongodbInfoClient(host);
                //不指定查询字段
                if (field == null || field.Length == 0)
                {
                    if (sort == null) return client.Find(filter).ToList();
                    //进行排序
                    return client.Find(filter).Sort(sort).ToList();
                }

                //制定查询字段
                var fieldList = new List<ProjectionDefinition<TEntity>>();
                foreach (var t in field)
                {
                    fieldList.Add(Builders<TEntity>.Projection.Include(t));
                }
                var projection = Builders<TEntity>.Projection.Combine(fieldList);
                fieldList.Clear();
                if (sort == null) return client.Find(filter).Project<TEntity>(projection).ToList();
                //排序查询
                return client.Find(filter).Sort(sort).Project<TEntity>(projection).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region FindListAsync 异步查询集合
        /// <summary>
        /// 异步查询集合
        /// </summary>
        /// <param name="host">mongodb连接信息</param>
        /// <param name="filter">查询条件</param>
        /// <param name="field">要查询的字段,不写时查询全部</param>
        /// <param name="sort">要排序的字段</param>
        /// <returns></returns>
        public static async Task<List<TEntity>> FindListAsync(MongoDbHost host, FilterDefinition<TEntity> filter, string[] field = null, SortDefinition<TEntity> sort = null)
        {
            try
            {
                var client = MongoDbClient<TEntity>.MongodbInfoClient(host);
                //不指定查询字段
                if (field == null || field.Length == 0)
                {
                    if (sort == null) return await client.Find(filter).ToListAsync();
                    return await client.Find(filter).Sort(sort).ToListAsync();
                }

                //制定查询字段
                var fieldList = new List<ProjectionDefinition<TEntity>>();
                foreach (var t in field)
                {
                    fieldList.Add(Builders<TEntity>.Projection.Include(t));
                }
                var projection = Builders<TEntity>.Projection.Combine(fieldList);
                fieldList.Clear();
                if (sort == null) return await client.Find(filter).Project<TEntity>(projection).ToListAsync();
                //排序查询
                return await client.Find(filter).Sort(sort).Project<TEntity>(projection).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region FindListByPage 分页查询集合
        /// <summary>
        /// 分页查询集合
        /// </summary>
        /// <param name="host">mongodb连接信息</param>
        /// <param name="filter">查询条件</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="count">总条数</param>
        /// <param name="field">要查询的字段,不写时查询全部</param>
        /// <param name="sort">要排序的字段</param>
        /// <returns></returns>
        public static List<TEntity> FindListByPage(MongoDbHost host, FilterDefinition<TEntity> filter, int pageIndex, int pageSize, out long count, string[] field = null, SortDefinition<TEntity> sort = null)
        {
            try
            {
                var client = MongoDbClient<TEntity>.MongodbInfoClient(host);
                count = client.CountDocuments(filter);
                //不指定查询字段
                if (field == null || field.Length == 0)
                {
                    if (sort == null) return client.Find(filter).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToList();
                    //进行排序
                    return client.Find(filter).Sort(sort).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToList();
                }

                //制定查询字段
                var fieldList = new List<ProjectionDefinition<TEntity>>();
                foreach (var t in field)
                {
                    fieldList.Add(Builders<TEntity>.Projection.Include(t));
                }
                var projection = Builders<TEntity>.Projection.Combine(fieldList);
                fieldList.Clear();

                //不排序
                if (sort == null) return client.Find(filter).Project<TEntity>(projection).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToList();

                //排序查询
                return client.Find(filter).Sort(sort).Project<TEntity>(projection).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region FindListByPageAsync 异步分页查询集合
        /// <summary>
        /// 异步分页查询集合
        /// </summary>
        /// <param name="host">mongodb连接信息</param>
        /// <param name="filter">查询条件</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="field">要查询的字段,不写时查询全部</param>
        /// <param name="sort">要排序的字段</param>
        /// <returns></returns>
        public static async Task<List<TEntity>> FindListByPageAsync(MongoDbHost host, FilterDefinition<TEntity> filter, int pageIndex, int pageSize, string[] field = null, SortDefinition<TEntity> sort = null)
        {
            try
            {
                var client = MongoDbClient<TEntity>.MongodbInfoClient(host);
                //不指定查询字段
                if (field == null || field.Length == 0)
                {
                    if (sort == null) return await client.Find(filter).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToListAsync();
                    //进行排序
                    return await client.Find(filter).Sort(sort).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToListAsync();
                }

                //制定查询字段
                var fieldList = new List<ProjectionDefinition<TEntity>>();
                foreach (var t in field)
                {
                    fieldList.Add(Builders<TEntity>.Projection.Include(t));
                }
                var projection = Builders<TEntity>.Projection.Combine(fieldList);
                fieldList.Clear();

                //不排序
                if (sort == null) return await client.Find(filter).Project<TEntity>(projection).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToListAsync();

                //排序查询
                return await client.Find(filter).Sort(sort).Project<TEntity>(projection).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToListAsync();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
