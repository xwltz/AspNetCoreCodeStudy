namespace Ico.IDal
{
    public interface IDataAccess: IDependency
    {
        void Add();
        string Get();
    }
}
