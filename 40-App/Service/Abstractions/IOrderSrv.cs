using myCoreMvc.Domain;
using System;
using System.Collections.Generic;

namespace myCoreMvc.App.Services
{
    public interface IOrderSrv
    {
        Order Save(Order x);
        List<Order> GetAll();
        Order Get(Guid? id);
        void Delete(Guid? id);
        void DeleteAll();
    }
}
