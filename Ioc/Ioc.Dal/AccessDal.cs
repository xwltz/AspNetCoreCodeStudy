using System;
using Ico.IDal;

namespace Ioc.Dal
{
    public class AccessDal : IDataAccess
    {
        public string Get()
        {
            return "通过Access获取数据";
        }

        public void Add()
        {
            Console.WriteLine("在Access数据库中添加一条订单!");
        }
    }
}
