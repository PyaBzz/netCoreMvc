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

        private readonly Product plan1 = new Product { Name = "Plan1" };
        private readonly Product plan2 = new Product { Name = "Plan2" };
        private readonly Product plan3 = new Product { Name = "Plan3" };

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
