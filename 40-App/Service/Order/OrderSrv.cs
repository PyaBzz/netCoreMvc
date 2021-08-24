using myCoreMvc.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace myCoreMvc.App.Services
{
    public class OrderSrv : IOrderSrv
    {
        // private readonly IDataRepo DataRepo;

        // public OrderBiz(IDataRepo dataRepo)
        //     => DataRepo = dataRepo;

        public Order Get(Guid id) => throw new NotImplementedException();
        public Order Get(Predicate<Order> func) => throw new NotImplementedException();

        public List<Order> GetList() => throw new NotImplementedException();
        public List<Order> GetList(Predicate<Order> func) => throw new NotImplementedException();
        public List<Order> GetListIncluding(params Expression<Func<Order, object>>[] includeProperties)
            => throw new NotImplementedException();
        public List<Order> GetListIncluding(Predicate<Order> predicate, params Expression<Func<Order, object>>[] includeProperties)
            => throw new NotImplementedException();

        public IOrderSrvOf Of(Order order)
            => throw new NotImplementedException();
    }
}
