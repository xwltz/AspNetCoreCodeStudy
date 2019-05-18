using System;
using Ico.IDal;

namespace Ioc.Dal
{
    public class OracleDal : IDataAccess
    {
        public string Get()
        {
            return "通过Oracle获取数据";
        }

        public void Add()
        {
            Console.WriteLine("在Oracle数据库中添加一条订单!");
        }
    }
}
