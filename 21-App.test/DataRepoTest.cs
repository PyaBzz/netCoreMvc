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
        private readonly IDataRepo dataRepo;

        public DataRepoTest(IDataRepo dataRepo)
        {
            this.dataRepo = dataRepo;
        }

        [Fact]
        public void GetList_GetsTheRightType()
        {
            Assert.IsType<List<WorkPlan>>(dataRepo.GetList<WorkPlan>());
            Assert.IsType<List<WorkItem>>(dataRepo.GetList<WorkItem>());
            Assert.IsType<List<User>>(dataRepo.GetList<User>());
        }

        [Fact]
        public void GetList_GetsAllItems()
        {
            Assert.StrictEqual(dataRepo.GetList<WorkPlan>().Count(), 2);
        }

        [Fact]
        public void Get_GetsById()
        {
            Assert.StrictEqual("WorkPlanB", dataRepo.Get<WorkPlan>(new Guid("DC2A8B2C-80FD-4344-8D30-67E94E4E77E6")).Name);
        }
    }
}
