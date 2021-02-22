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
        }

        [Fact]
        public void GetList_GetsAllItems()
        {
            Assert.StrictEqual(dp.GetList<WorkPlan>().Count(), 2);
        }
    }
}
