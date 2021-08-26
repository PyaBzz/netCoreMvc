using System;
using Xunit;
using myCoreMvc.App.Services;
using myCoreMvc.Domain;
using System.Linq;
using System.Collections.Generic;
using myCoreMvc.App;
using myCoreMvc.App.Interfaces;

namespace myCoreMvc.Test.DataLayer
{
    [Trait("Group", "Repos")]
    public class ProductRepoTest : IDisposable
    {
        private readonly IProductRepo repo;

        private readonly Product Product1 = new Product { Name = "Product1" };
        private readonly Product Product2 = new Product { Name = "Product2" };
        private readonly Product Product3 = new Product { Name = "Product3" };

        public ProductRepoTest(IProductRepo rep)
        {
            this.repo = rep;
        }

        public void Dispose()
        {
            repo.DeleteAll();
        }
    }
}
