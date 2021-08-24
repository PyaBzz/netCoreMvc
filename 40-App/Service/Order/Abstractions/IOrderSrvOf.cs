using myCoreMvc.Domain;

namespace myCoreMvc.App.Services
{
    public interface IOrderSrvOf
    {
        Order Order { get; }

        Order Save();
        void Delete();
    }
}
