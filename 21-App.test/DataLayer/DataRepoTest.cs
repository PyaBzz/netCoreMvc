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
        public void GetList_Filters_IfPredicate()
        {
            Assert.StrictEqual("Plan2", dataRepo.GetList<WorkPlan>(wp => wp.Name == "Plan2").First().Name);
            Assert.StrictEqual(1, dataRepo.GetList<WorkPlan>(wp => wp.Name == "Plan2").Count());
        }

        [Fact]
        public void Get_GetsById()
        {
            Assert.StrictEqual("Plan2", dataRepo.Get<WorkPlan>(new Guid("53c88402-4092-4834-8e7f-6ce70057cdc5")).Name);
        }
    }
}
