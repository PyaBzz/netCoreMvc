using myCoreMvc.Domain;
using myCoreMvc.App.Interfaces;

namespace myCoreMvc.Persistence
{
    public class OrderRepo : CrudRepo<Order>, IOrderRepo
    {
        public OrderRepo(IDbConFactory conFac) : base(conFac)
        {
        }
    }
}
