using System;
using Xunit;
using myCoreMvc.App.Services;
using myCoreMvc.Domain;
using System.Linq;
using System.Collections.Generic;
using myCoreMvc.App;
using myCoreMvc.App.Interfaces;
using Baz.Core;

namespace myCoreMvc.Persistence.Test
{
    [Trait("Group", "CrudRepo")]
    public class CrudRepoTest : IDisposable
    { //Todo: Cover all CRUD related operations
        private readonly CrudRepo<DummyA> repo;
        private readonly DummyA
            A1 = new DummyA { Name = "A1" },
            A2 = new DummyA { Name = "A2" },
            A3 = new DummyA { Name = "A3" },
            A4 = new DummyA { Name = "A4" };

        public CrudRepoTest(CrudRepo<DummyA> rep)
        {
            this.repo = rep;
        }

        public void Dispose()
        {
            repo.DeleteAll();
        }

        private DummyA SaveAndRetrieve(DummyA x)
        {
            repo.Save(x);
            return repo.Get(x.Id);
        }

        /*==================================  Create  =================================*/

        [Fact]
        public void Save_AssignsIdToNewRecords()
        {
            Assert.False(A1.Id.HasValue);
            var retrieved = SaveAndRetrieve(A1);
            Assert.True(A1.Id.HasValue);
            Assert.StrictEqual(A1, repo.Get(A1.Id));
        }

        [Fact]
        public void Save_AssignsUniqueIds()
        {
            repo.Save(A1);
            repo.Save(A2);
            Assert.NotEqual(A1.Id.Value, A2.Id.Value);
        }

        [Fact]
        public void Save_Saves_String_Uninitialised_As_Null()
        {
            var x = new DummyA();
            Assert.Null(x.Name);
            var retrieved = SaveAndRetrieve(x);
            Assert.Null(retrieved.Name);
        }

        [Fact]
        public void Save_Saves_String_Empty_As_Empty()
        {
            var x = new DummyA { Name = string.Empty };
            Assert.Empty(x.Name);
            Assert.StrictEqual(string.Empty, x.Name);
            var retrieved = SaveAndRetrieve(x);
            Assert.StrictEqual(string.Empty, retrieved.Name);
        }

        [Fact]
        public void Save_Saves_String_Correctly()
        {
            var retrieved = SaveAndRetrieve(A1);
            Assert.StrictEqual(A1.Name, retrieved.Name);
        }

        /*==================================  Read  =================================*/

        [Fact]
        public void GetAll_GetsTheRightType()
        {
            Assert.IsType<List<DummyA>>(repo.GetAll());
        }

        [Fact]
        public void GetAll_GetsAllItems()
        {
            List<DummyA> all;
            Assert.StrictEqual(0, repo.GetAll().Count());

            repo.Save(A1);
            all = repo.GetAll();
            Assert.StrictEqual(1, all.Count());
            Assert.Contains(A1, all);

            repo.Save(A2);
            all = repo.GetAll();
            Assert.StrictEqual(2, all.Count());
            Assert.Contains(A1, all);
            Assert.Contains(A2, all);

            repo.Save(A3);
            all = repo.GetAll();
            Assert.StrictEqual(3, all.Count());
            Assert.Contains(A1, all);
            Assert.Contains(A2, all);
            Assert.Contains(A3, all);
        }

        [Fact]
        public void Get_GetsByGuid()
        {
            repo.Save(A1);
            Assert.StrictEqual(A1, repo.Get(A1.Id));
        }

        [Fact]
        public void Get_GetsByStringId()
        {
            repo.Save(A1);
            Assert.StrictEqual(A1, repo.Get(A1.Id.ToString()));
        }

        [Fact]
        public void Get_IsCaseInsensitiveToId()
        {
            repo.Save(A1);
            Assert.StrictEqual(repo.Get(A1.Id.ToString().ToLower()), repo.Get(A1.Id.ToString().ToUpper()));
        }

        [Fact]
        public void Get_ReturnsNewObject()
        {
            repo.Save(A1);
            var retrievedObject = repo.Get(A1.Id);
            Assert.StrictEqual(A1, retrievedObject);
            Assert.NotSame(A1, retrievedObject);
        }

        /*==================================  Update  =================================*/

        [Fact]
        public void Save_PreservesIdOfExistingRecords()
        {
            Assert.False(A1.Id.HasValue);
            repo.Save(A1);
            var id1 = A1.Id.Value;
            repo.Save(A1);
            var id2 = A1.Id.Value;
            Assert.StrictEqual(id1, id2);
        }

        [Fact]
        public void Save_Updates_String()
        {
            var recordToChange = SaveAndRetrieve(A1);
            var originalName = recordToChange.Name;
            var newName = "updated";
            recordToChange.Name = newName;
            repo.Save(recordToChange);
            Assert.StrictEqual(newName, repo.Get(A1.Id).Name);
        }

        /*==================================  Delete  =================================*/

        [Fact]
        public void Delete_DeletesByGuid()
        {
            repo.Save(A1);
            repo.Delete(A1.Id);
            Assert.Null(repo.Get(A1.Id));
        }

        [Fact]
        public void Delete_DeletesByStringId()
        {
            repo.Save(A1);
            repo.Delete(A1.Id.ToString());
            Assert.Null(repo.Get(A1.Id));
        }
    }
}
