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
        private readonly WorkItem wi11, wi12, wi21, wi22;
        private readonly IWorkItemRepo repo;

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

        [Fact]
        public void Save_AssignsIdToNewRecords()
        {
            Assert.False(wi11.Id.HasValue);
            repo.Save(wi11);
            Assert.True(wi11.Id.HasValue);
            Assert.StrictEqual(wi11, repo.Get(wi11.Id));
        }

        [Fact]
        public void Save_PreservesIdOfExistingRecords()
        {
            Assert.False(wi11.Id.HasValue);
            repo.Save(wi11);
            var id1 = wi11.Id.Value;
            repo.Save(wi11);
            var id2 = wi11.Id.Value;
            Assert.StrictEqual(id1, id2);
        }

        [Fact]
        public void Save_AssignsUniqueIds()
        {
            repo.Save(wi11);
            repo.Save(wi12);
            Assert.NotEqual(wi11.Id.Value, wi12.Id.Value);
        }

        [Fact]
        public void Save_SavesDetailsOfNewRecords()
        {
            repo.Save(wi11);
            var retrievedWi11 = repo.Get(wi11.Id);
            Assert.StrictEqual(wi11.Id, retrievedWi11.Id);
            Assert.StrictEqual(wi11.Name, retrievedWi11.Name);
            repo.Delete(wi11.Id);
        }

        [Fact]
        public void Save_UpdatesName()
        {
            repo.Save(wi11);
            var recordToChange = repo.Get(wi11.Id);
            var originalName = recordToChange.Name;
            var newName = "updated";
            recordToChange.Name = newName;
            repo.Save(recordToChange);
            Assert.StrictEqual(newName, repo.Get(wi11.Id).Name);
        }

        [Fact]
        public void GetAll_GetsTheRightType()
        {
            Assert.IsType<List<WorkItem>>(repo.GetAll());
        }

        [Fact]
        public void GetAll_GetsAllItems()
        {
            Assert.StrictEqual(0, repo.GetAll().Count());
            repo.Save(wi11);
            Assert.StrictEqual(1, repo.GetAll().Count());
            repo.Save(wi12);
            Assert.StrictEqual(2, repo.GetAll().Count());
            repo.Save(wi21);
            Assert.StrictEqual(3, repo.GetAll().Count());
        }

        [Fact]
        public void Get_GetsByGuid()
        {
            repo.Save(wi11);
            Assert.StrictEqual(wi11, repo.Get(wi11.Id));
        }

        [Fact]
        public void Get_GetsByStringId()
        {
            repo.Save(wi11);
            Assert.StrictEqual(wi11, repo.Get(wi11.Id.ToString()));
        }

        [Fact]
        public void Get_IsCaseInsensitiveToId()
        {
            repo.Save(wi11);
            Assert.StrictEqual(repo.Get(wi11.Id.ToString().ToLower()), repo.Get(wi11.Id.ToString().ToUpper()));
        }

        [Fact]
        public void Get_ReturnsNewObject()
        {
            repo.Save(wi11);
            var retrievedObject = repo.Get(wi11.Id);
            Assert.StrictEqual(wi11, retrievedObject);
            Assert.NotSame(wi11, retrievedObject);
        }

        [Fact]
        public void Delete_DeletesByGuid()
        {
            repo.Save(wi11);
            repo.Delete(wi11.Id);
            Assert.Null(repo.Get(wi11.Id));
        }

        [Fact]
        public void Delete_DeletesByStringId()
        {
            repo.Save(wi11);
            repo.Delete(wi11.Id.ToString());
            Assert.Null(repo.Get(wi11.Id));
        }
    }
}
