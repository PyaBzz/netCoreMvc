using myCoreMvc.Domain;
using myCoreMvc.App.Interfaces;

namespace myCoreMvc.Persistence
{
    public class ProductRepo : CrudRepo<Product>, IProductRepo
    {
        public ProductRepo(IDbConFactory conFac) : base(conFac)
        {
        }
    }
}
