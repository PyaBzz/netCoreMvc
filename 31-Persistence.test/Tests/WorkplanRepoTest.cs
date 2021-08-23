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
    public class WorkplanRepoTest : IDisposable
    {
        private readonly IWorkplanRepo repo;

        private readonly WorkPlan plan1 = new WorkPlan { Name = "Plan1" };
        private readonly WorkPlan plan2 = new WorkPlan { Name = "Plan2" };
        private readonly WorkPlan plan3 = new WorkPlan { Name = "Plan3" };

        public WorkplanRepoTest(IWorkplanRepo rep)
        {
            this.repo = rep;
        }

        public void Dispose()
        {
            repo.DeleteAll();
        }
    }
}
