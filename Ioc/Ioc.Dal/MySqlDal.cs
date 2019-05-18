using System;
using Ico.IDal;

namespace Ioc.Dal
{
    public class MySqlDal : IDataAccess
    {
        public string Get()
        {
            return "通过MySql获取数据";
        }

        public void Add()
        {
            Console.WriteLine("在MySql数据库中添加一条订单!");
        }
    }
}
