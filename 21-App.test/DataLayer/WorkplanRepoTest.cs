using System;
using Xunit;
using myCoreMvc.App.Services;
using myCoreMvc.Domain;
using System.Linq;
using System.Collections.Generic;
using myCoreMvc.App;

namespace myCoreMvc.Test.DataLayer
{
    [Trait("Group", "Data")]
    public class WorkplanRepoTest
    {
        private readonly string _plan1Id = "60f9fc29-083f-4ed2-a3e2-3948b503c25f";
        private readonly string _plan2Id = "53c88402-4092-4834-8e7f-6ce70057cdc5";
        private readonly string _plan3Id = "2692d0a1-7c44-4007-95a6-4732f16131d8";

        private readonly IWorkplanRepo repo;

        public WorkplanRepoTest(IWorkplanRepo rep)
        {
            this.repo = rep;
        }

        [Fact]
        public void Add_Adds()
        {
            Assert.Null(repo.Get(_plan3Id));
            var newPlan = new WorkPlan { Id = new Guid(_plan3Id), Name = "Plan3" };
            Assert.StrictEqual(newPlan, repo.Add(newPlan));
            Assert.StrictEqual(3, repo.GetAll().Count());
            repo.Delete(_plan3Id);
            Assert.StrictEqual(2, repo.GetAll().Count());
        }

        [Fact]
        public void GetAll_GetsTheRightType()
        {
            Assert.IsType<List<WorkPlan>>(repo.GetAll());
        }

        [Fact]
        public void GetAll_GetsAllItems()
        {
            Assert.StrictEqual(2, repo.GetAll().Count());
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

        [Fact]
        public void Update_UpdatesName()
        {
            var wpToChange = repo.Get(_plan1Id);
            var originalName = wpToChange.Name;
            var newName = "updated";
            wpToChange.Name = newName;
            Assert.StrictEqual(wpToChange, repo.Update(wpToChange));
            Assert.StrictEqual(newName, repo.Get(_plan1Id).Name);
            wpToChange.Name = originalName;
            Assert.StrictEqual(wpToChange, repo.Update(wpToChange));
            Assert.StrictEqual(originalName, repo.Get(_plan1Id).Name);
        }

        [Fact]
        public void Delete_DeletesByStringId()
        {
            repo.Delete(_plan2Id);
            Assert.Null(repo.Get(_plan2Id));
            Assert.StrictEqual(1, repo.GetAll().Count());
            var newPlan = new WorkPlan { Id = new Guid(_plan2Id), Name = "Plan2" };
            Assert.StrictEqual(newPlan, repo.Add(newPlan));
            Assert.StrictEqual(2, repo.GetAll().Count());
        }
    }
}
