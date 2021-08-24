using System;
using Xunit;
using myCoreMvc.App.Services;
using myCoreMvc.Domain;
using System.Linq;
using System.Collections.Generic;
using myCoreMvc.App;
using myCoreMvc.App.Interfaces;

namespace myCoreMvc.Persistence.Test
{
    [Trait("Group", "Repos")]
    public class OrderRepoTest : IDisposable
    {
        private readonly IOrderRepo repo;
        private readonly Order wi11, wi12, wi21, wi22;

        public OrderRepoTest(IOrderRepo rep)
        {
            this.repo = rep;
            wi11 = new Order { Name = "wi11" };
            wi12 = new Order { Name = "wi12" };
            wi21 = new Order { Name = "wi21" };
            wi22 = new Order { Name = "wi22" };
        }

        public void Dispose()
        {
            repo.DeleteAll();
        }
    }
}
