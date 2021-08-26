using System;
using System.Collections.Generic;
using myCoreMvc.App.Interfaces;
using myCoreMvc.Domain;

namespace myCoreMvc.App.Services
{
    public class ProductSrv : IProductSrv
    {
        private IProductRepo repo;

        public ProductSrv(IProductRepo r)
        {
            repo = r;
        }

        public Product Get(Guid id) => repo.Get(id);
        public List<Product> GetAll() => repo.GetAll();
        public void Delete(Guid id) => repo.Delete(id);
    }
}
