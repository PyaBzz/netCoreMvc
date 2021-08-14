using System;
using Xunit;
using myCoreMvc.App.Services;
using myCoreMvc.Domain;
using System.Linq;
using Baz.Core;
using System.Collections.Generic;

namespace myCoreMvc.Test.DataLayer
{
    [Trait("Group", "Data")]
    public class UserRepoTest : IDisposable
    {
        private readonly User jim, sam, adam;
        private readonly IUserRepo repo;

        public UserRepoTest(IUserRepo rep, IHashFactory hashFac)
        {
            this.repo = rep;
            var salts = hashFac.GetSalt(3);

            jim = new User { Name = "Jim", Role = AuthConstants.JuniorRoleName, DateOfBirth = new DateTime(2018, 01, 22), Salt = salts[0], Hash = hashFac.GetHash("jjj", salts[0]) };
            sam = new User { Name = "Sam", Role = AuthConstants.SeniorRoleName, DateOfBirth = new DateTime(2010, 01, 22), Salt = salts[1], Hash = hashFac.GetHash("sss", salts[1]) };
            adam = new User { Name = "Adam", Role = AuthConstants.AdminRoleName, DateOfBirth = new DateTime(2000, 01, 22), Salt = salts[2], Hash = hashFac.GetHash("aaa", salts[2]) };
        }

        public void Dispose()
        {
            repo.DeleteAll();
        }

        [Fact]
        public void Save_AssignsIdToNewRecords()
        {
            Assert.False(jim.Id.HasValue);
            repo.Save(jim);
            Assert.True(jim.Id.HasValue);
            Assert.StrictEqual(jim, repo.Get(jim.Id));
        }

        [Fact]
        public void Save_PreservesIdOfExistingRecords()
        {
            Assert.False(jim.Id.HasValue);
            repo.Save(jim);
            var id1 = jim.Id.Value;
            repo.Save(jim);
            var id2 = jim.Id.Value;
            Assert.StrictEqual(id1, id2);
        }

        [Fact]
        public void Save_AssignsUniqueIds()
        {
            repo.Save(jim);
            repo.Save(sam);
            Assert.NotEqual(jim.Id.Value, sam.Id.Value);
        }

        [Fact]
        public void Save_SavesDetailsOfNewRecords()
        {
            repo.Save(jim);
            var retrievedJim = repo.Get(jim.Id);
            Assert.StrictEqual(jim.Id, retrievedJim.Id);
            Assert.StrictEqual(jim.Name, retrievedJim.Name);
            Assert.StrictEqual(jim.DateOfBirth, retrievedJim.DateOfBirth);
            Assert.StrictEqual(jim.Hash, retrievedJim.Hash);
            Assert.True(jim.Salt.SequenceEqual(retrievedJim.Salt));
            Assert.StrictEqual(jim.Role, retrievedJim.Role);
            repo.Delete(jim.Id);
        }

        [Fact]
        public void Save_UpdatesName()
        {
            repo.Save(jim);
            var recordToChange = repo.Get(jim.Id);
            var originalName = recordToChange.Name;
            var newName = "updated";
            recordToChange.Name = newName;
            repo.Save(recordToChange);
            Assert.StrictEqual(newName, repo.Get(jim.Id).Name);
        }

        [Fact]
        public void GetAll_GetsTheRightType()
        {
            Assert.IsType<List<User>>(repo.GetAll());
        }

        [Fact]
        public void GetAll_GetsAllItems()
        {
            Assert.StrictEqual(0, repo.GetAll().Count());
            repo.Save(jim);
            Assert.StrictEqual(1, repo.GetAll().Count());
            repo.Save(sam);
            Assert.StrictEqual(2, repo.GetAll().Count());
            repo.Save(adam);
            Assert.StrictEqual(3, repo.GetAll().Count());
        }

        [Fact]
        public void Get_GetsByGuid()
        {
            repo.Save(jim);
            Assert.StrictEqual(jim, repo.Get(jim.Id));
        }

        [Fact]
        public void Get_GetsByStringId()
        {
            repo.Save(jim);
            Assert.StrictEqual(jim, repo.Get(jim.Id.ToString()));
        }

        [Fact]
        public void Get_IsCaseInsensitiveToId()
        {
            repo.Save(jim);
            Assert.StrictEqual(repo.Get(jim.Id.ToString().ToLower()), repo.Get(jim.Id.ToString().ToUpper()));
        }

        [Fact]
        public void Get_ReturnsNewObject()
        {
            repo.Save(jim);
            var retrievedObject = repo.Get(jim.Id);
            Assert.StrictEqual(jim, retrievedObject);
            Assert.NotSame(jim, retrievedObject);
        }

        [Fact]
        public void Delete_DeletesByGuid()
        {
            repo.Save(jim);
            repo.Delete(jim.Id);
            Assert.Null(repo.Get(jim.Id));
        }

        [Fact]
        public void Delete_DeletesByStringId()
        {
            repo.Save(jim);
            repo.Delete(jim.Id.ToString());
            Assert.Null(repo.Get(jim.Id));
        }
    }
}
