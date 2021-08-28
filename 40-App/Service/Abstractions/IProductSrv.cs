using System;
using myCoreMvc.Domain;
using System.Collections.Generic;

namespace myCoreMvc.App.Services
{
    public interface IProductSrv
    {
        List<Product> GetAll();
        Product Get(Guid id);
        void Delete(Guid id);
        void DeleteAll();
    }
}
