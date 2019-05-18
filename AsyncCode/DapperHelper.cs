using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Dapper.Contrib.Extensions;

namespace AsyncCode
{
    public class DapperHelper
    {

        //protected const string ConnStr = "Data Source = 192.168.11.5;Initial Catalog = test;User Id = sa;Password = ok;";
        protected const string ConnStr = "Data Source = 47.98.209.58;Initial Catalog = services_vleader;User Id = services_vleader;Password = 2iYkFrP626;";
        public static List<SeoCustomer> GetList()
        {
            using (var connection = new SqlConnection(ConnStr))
            {
                var result = connection.GetAll<SeoCustomer>().Where(x => x.WebDomain != "");
                var list = result.ToList();
                return list;
            }
        }

        public static List<SeoRankList> GetList1()
        {
            using (var connection = new SqlConnection(ConnStr))
            {
                var result = connection.GetAll<SeoRankList>().Where(x => x.ProtId.Equals(1));
                var list = result.ToList();
                return list;
            }
        }

        public static void UpdateModel(string id, string domain)
        {
            try
            {
                using (var connection = new SqlConnection(ConnStr))
                {
                    var sqlStr = $"UPDATE SeoCustomer SET WebDomain='{domain}' WHERE ID='{id}'";
                    connection.Execute(sqlStr);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static void UpdateModel2(string id, string domain)
        {
            try
            {
                using (var connection = new SqlConnection(ConnStr))
                {
                    var sqlStr = $"UPDATE SeoRankList SET SeoWebDomain='{domain}' WHERE ID='{id}'";
                    connection.Execute(sqlStr);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }

    [Table("Users")]
    public class Users
    {
        public  int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    [Table("SeoCustomer")]
    public class SeoCustomer
    {
        public string ID { get; set; }
        public string WebDomain { get; set; }
    }

    [Table("SeoRankList")]
    public class SeoRankList
    {
        public string ID { get; set; }
        public string SeoWebDomain { get; set; }
        public int ProtId { get; set; }
    }
}
