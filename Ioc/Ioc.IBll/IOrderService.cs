using Ico.IDal;

namespace Ioc.IBll
{
    public interface IOrderService
    {
        void SetDependence(IDataAccess ida);
        string Get();
        void Add();
    }
}