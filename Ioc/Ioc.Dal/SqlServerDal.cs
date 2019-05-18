using System;
using Ico.IDal;

namespace Ioc.Dal
{
    public class SqlServerDal : IDataAccess
    {
        public string Get()
        {
            return "通过SqlServer获取数据";
        }

        public void Add()
        {
            Console.WriteLine("在SqlServer数据库中添加一条订单!");
        }
    }
}
