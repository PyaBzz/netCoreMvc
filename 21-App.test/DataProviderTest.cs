using System;
using Xunit;
using myCoreMvc.App.Services;
using myCoreMvc.Domain;

namespace myCoreMvc.Test.Data
{
    public class DataProviderTest
    {
        private readonly IDataProvider dp = new DataProvider();

        [Fact]
        public void GetList_GetsTheRightType()
        {
            Assert.IsType<WorkPlan>(dp.GetList<WorkPlan>()[0]);
        }
    }
}
