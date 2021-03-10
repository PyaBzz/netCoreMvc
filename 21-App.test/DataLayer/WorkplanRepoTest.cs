using System;
using Xunit;
using myCoreMvc.App.Services;
using myCoreMvc.Domain;
using System.Linq;
using System.Collections.Generic;

namespace myCoreMvc.Test.DataLayer
{
    public class WorkplanRepoTest
    {
        private readonly string _plan2Id = "53c88402-4092-4834-8e7f-6ce70057cdc5";
        private readonly IWorkplanRepo repo;

        public WorkplanRepoTest(IWorkplanRepo rep)
        {
            this.repo = rep;
        }

        [Fact]
        public void GetAll_GetsTheRightType()
        {
            Assert.IsType<List<WorkPlan>>(repo.GetAll());
        }

        [Fact]
        public void GetAll_GetsAllItems()
        {
            Assert.StrictEqual(repo.GetAll().Count(), 2);
        }

        [Fact]
        public void Get_GetsByStringId()
        {
            Assert.StrictEqual("Plan2", repo.Get(_plan2Id).Name);
        }

        [Fact]
        public void Get_GetsByGuid()
        {
            Assert.StrictEqual("Plan2", repo.Get(new Guid(_plan2Id)).Name);
        }

        [Fact]
        public void Get_IsCaseInsensitiveToId()
        {
            Assert.StrictEqual(repo.Get(_plan2Id), repo.Get(_plan2Id.ToUpper()));
        }
    }
}
