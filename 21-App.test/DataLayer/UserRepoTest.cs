using System;
using Xunit;
using myCoreMvc.App.Services;
using myCoreMvc.Domain;
using System.Linq;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Baz.Core;
using System.Security.Cryptography;
using System.Collections.Generic;

namespace myCoreMvc.Test.DataLayer
{
    [Trait("Group", "Data")]
    public class UserRepoTest : IDisposable
    {
        private readonly User jim, sam, adam;
        private readonly IUserRepo repo;

        public UserRepoTest(IUserRepo rep, ISaltFactory saltFac)
        {
            this.repo = rep;
            var salts = saltFac.GetMany(3);

            jim = new User
            {
                Salt = salts[0],
                Name = "Jim",
                Hash = Convert.ToBase64String(KeyDerivation.Pbkdf2("jjj", salts[0], KeyDerivationPrf.HMACSHA512, 100, 256 / 8)),
                DateOfBirth = new DateTime(2018, 01, 22),
                Role = AuthConstants.JuniorRoleName
            };
            sam = new User
            {
                Salt = salts[1],
                Name = "Sam",
                Hash = Convert.ToBase64String(KeyDerivation.Pbkdf2("sss", salts[1], KeyDerivationPrf.HMACSHA512, 100, 256 / 8)),
                DateOfBirth = new DateTime(2010, 01, 22),
                Role = AuthConstants.SeniorRoleName
            };
            adam = new User
            {
                Salt = salts[2],
                Name = "Adam",
                Hash = Convert.ToBase64String(KeyDerivation.Pbkdf2("aaa", salts[2], KeyDerivationPrf.HMACSHA512, 100, 256 / 8)),
                DateOfBirth = new DateTime(2000, 01, 22),
                Role = AuthConstants.AdminRoleName
            };
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
