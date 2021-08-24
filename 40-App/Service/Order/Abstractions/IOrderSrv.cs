using myCoreMvc.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace myCoreMvc.App.Services
{
    public interface IOrderSrv
    {
        Order Get(Guid id);
        Order Get(Predicate<Order> func);

        List<Order> GetList();
        List<Order> GetList(Predicate<Order> func);
        List<Order> GetListIncluding(params Expression<Func<Order, object>>[] includeProperties);
        List<Order> GetListIncluding(Predicate<Order> predicate, params Expression<Func<Order, object>>[] includeProperties);

        IOrderSrvOf Of(Order order);
    }
}
