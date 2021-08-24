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

        public List<Product> GetAll() => null;
    }
}
