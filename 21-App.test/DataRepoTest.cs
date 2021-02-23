using System;
using Xunit;
using myCoreMvc.App.Services;
using myCoreMvc.Domain;
using System.Linq;
using System.Collections.Generic;

namespace myCoreMvc.Test.Data
{
    public class DataRepoTest
    {
        private readonly IDataRepo dp = new DataRepo();

        [Fact]
        public void GetList_GetsTheRightType()
        {
            Assert.IsType<List<WorkPlan>>(dp.GetList<WorkPlan>());
            Assert.IsType<List<WorkItem>>(dp.GetList<WorkItem>());
            Assert.IsType<List<User>>(dp.GetList<User>());
        }

        [Fact]
        public void GetList_GetsAllItems()
        {
            Assert.StrictEqual(dp.GetList<WorkPlan>().Count(), 2);
        }

        [Fact]
        public void Get_GetsById()
        {
            Assert.StrictEqual("WorkPlanB", dp.Get<WorkPlan>(new Guid("DC2A8B2C-80FD-4344-8D30-67E94E4E77E6")).Name);
        }
    }
}
