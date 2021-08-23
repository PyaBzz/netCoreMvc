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
    public class WorkItemRepoTest : IDisposable
    {
        private readonly IWorkItemRepo repo;
        private readonly WorkItem wi11, wi12, wi21, wi22;

        public WorkItemRepoTest(IWorkItemRepo rep)
        {
            this.repo = rep;
            wi11 = new WorkItem { Name = "wi11" };
            wi12 = new WorkItem { Name = "wi12" };
            wi21 = new WorkItem { Name = "wi21" };
            wi22 = new WorkItem { Name = "wi22" };
        }

        public void Dispose()
        {
            repo.DeleteAll();
        }
    }
}
