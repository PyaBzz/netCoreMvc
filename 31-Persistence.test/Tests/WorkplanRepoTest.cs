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

        [Fact]
        public void Save_AssignsIdToNewRecords()
        {
            Assert.False(plan1.Id.HasValue);
            repo.Save(plan1);
            Assert.True(plan1.Id.HasValue);
            Assert.StrictEqual(plan1, repo.Get(plan1.Id));
        }

        [Fact]
        public void Save_PreservesIdOfExistingRecords()
        {
            Assert.False(plan1.Id.HasValue);
            repo.Save(plan1);
            var id1 = plan1.Id.Value;
            repo.Save(plan1);
            var id2 = plan1.Id.Value;
            Assert.StrictEqual(id1, id2);
        }

        [Fact]
        public void Save_AssignsUniqueIds()
        {
            repo.Save(plan1);
            repo.Save(plan2);
            Assert.NotEqual(plan1.Id.Value, plan2.Id.Value);
        }

        [Fact]
        public void Save_SavesDetailsOfNewRecords()
        {
            repo.Save(plan1);
            var retrievedPlan1 = repo.Get(plan1.Id);
            Assert.StrictEqual(plan1.Id, retrievedPlan1.Id);
            Assert.StrictEqual(plan1.Name, retrievedPlan1.Name);
        }

        [Fact]
        public void Save_UpdatesName()
        {
            repo.Save(plan1);
            var recordToChange = repo.Get(plan1.Id);
            var originalName = recordToChange.Name;
            var newName = "updated";
            recordToChange.Name = newName;
            repo.Save(recordToChange);
            Assert.StrictEqual(newName, repo.Get(plan1.Id).Name);
        }

        [Fact]
        public void GetAll_GetsTheRightType()
        {
            Assert.IsType<List<WorkPlan>>(repo.GetAll());
        }

        [Fact]
        public void GetAll_GetsAllItems()
        {
            Assert.StrictEqual(0, repo.GetAll().Count());
            repo.Save(plan1);
            Assert.StrictEqual(1, repo.GetAll().Count());
            repo.Save(plan2);
            Assert.StrictEqual(2, repo.GetAll().Count());
            repo.Save(plan3);
            Assert.StrictEqual(3, repo.GetAll().Count());
        }

        [Fact]
        public void Get_GetsByGuid()
        {
            repo.Save(plan1);
            Assert.StrictEqual(plan1, repo.Get(plan1.Id));
        }

        [Fact]
        public void Get_GetsByStringId()
        {
            repo.Save(plan1);
            Assert.StrictEqual(plan1, repo.Get(plan1.Id.ToString()));
        }

        [Fact]
        public void Get_IsCaseInsensitiveToId()
        {
            repo.Save(plan1);
            Assert.StrictEqual(repo.Get(plan1.Id.ToString().ToLower()), repo.Get(plan1.Id.ToString().ToUpper()));
        }

        [Fact]
        public void Get_ReturnsNewObject()
        {
            repo.Save(plan1);
            var retrievedObject = repo.Get(plan1.Id);
            Assert.StrictEqual(plan1, retrievedObject);
            Assert.NotSame(plan1, retrievedObject);
        }

        [Fact]
        public void Delete_DeletesByGuid()
        {
            repo.Save(plan1);
            repo.Delete(plan1.Id);
            Assert.Null(repo.Get(plan1.Id));
        }

        [Fact]
        public void Delete_DeletesByStringId()
        {
            repo.Save(plan1);
            repo.Delete(plan1.Id.ToString());
            Assert.Null(repo.Get(plan1.Id));
        }
    }
}
