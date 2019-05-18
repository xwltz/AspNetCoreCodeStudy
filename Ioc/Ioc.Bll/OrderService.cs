using Ico.IDal;
using Ioc.IBll;

namespace Ioc.Bll
{
    public class OrderService : IOrderService
    {
        public OrderService(IDataAccess dal)
        {
            Ida = dal;
        }

        public IDataAccess Ida { set; get; }

        public void SetDependence(IDataAccess ida)
        {
            Ida = ida;
        }

        public string Get()
        {
            return Ida.Get();
        }

        public void Add()
        {
            Ida.Add();
        }
    }
}