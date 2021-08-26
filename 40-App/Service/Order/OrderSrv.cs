using System;
using myCoreMvc.Domain;
using myCoreMvc.App.Interfaces;
using System.Collections.Generic;

namespace myCoreMvc.App.Services
{
    public class OrderSrv : IOrderSrv
    {
        private readonly IOrderRepo repo;
        public OrderSrv(IOrderRepo r) => repo = r;

        public Order Save(Order x) => repo.Save(x);

        public List<Order> GetAll() => repo.GetAll();

        public Order Get(Guid? id) => repo.Get(id);

        public void Delete(Guid? id) => repo.Delete(id);
    }
}
