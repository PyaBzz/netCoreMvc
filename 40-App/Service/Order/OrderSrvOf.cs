using System;
using myCoreMvc.Domain;

namespace myCoreMvc.App.Services
{
    public class OrderSrvOf : IOrderSrvOf
    {
        // private readonly IDataRepo DataRepo;
        public Order Order { get; }

        // public OrderBizOf(IDataRepo dataRepo, Order order)
        // {
        //     DataRepo = dataRepo;
        //     Order = order;
        // }

        Order IOrderSrvOf.Save() => throw new NotImplementedException();
        void IOrderSrvOf.Delete()
        {
            throw new NotImplementedException();
            // DataRepo.Delete<Order>(Order.Id);
        }
    }
}
