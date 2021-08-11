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
    public class UserRepoTest
    {
        private readonly User jim, sam, adam;

        private readonly IUserRepo repo;

        public UserRepoTest(IUserRepo rep)
        {
            this.repo = rep;

            using (var rng = RandomNumberGenerator.Create())
            {
                var Salts = new[] { new byte[128 / 8], new byte[128 / 8], new byte[128 / 8] };
                foreach (var salt in Salts)
                    rng.GetBytes(salt);

                jim = new User
                {
                    Salt = Salts[0],
                    Name = "Jim",
                    Hash = Convert.ToBase64String(KeyDerivation.Pbkdf2("jjj", Salts[0], KeyDerivationPrf.HMACSHA512, 100, 256 / 8)),
                    DateOfBirth = new DateTime(2018, 01, 22),
                    Role = AuthConstants.JuniorRoleName
                };
                sam = new User
                {
                    Salt = Salts[1],
                    Name = "Sam",
                    Hash = Convert.ToBase64String(KeyDerivation.Pbkdf2("sss", Salts[1], KeyDerivationPrf.HMACSHA512, 100, 256 / 8)),
                    DateOfBirth = new DateTime(2010, 01, 22),
                    Role = AuthConstants.SeniorRoleName
                };
                adam = new User
                {
                    Salt = Salts[2],
                    Name = "Adam",
                    Hash = Convert.ToBase64String(KeyDerivation.Pbkdf2("aaa", Salts[2], KeyDerivationPrf.HMACSHA512, 100, 256 / 8)),
                    DateOfBirth = new DateTime(2000, 01, 22),
                    Role = AuthConstants.AdminRoleName
                };
            }
        }

        [Fact]
        public void Save_Assigns_Id_To_New_Records()
        {
            Assert.False(jim.Id.HasValue);
            repo.Save(jim);
            Assert.True(jim.Id.HasValue);
            Assert.StrictEqual(jim, repo.Get(jim.Id));
            repo.Delete(jim.Id);
        }

        [Fact]
        public void Save_Saves_Details_Of_New_Records()
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
        public void GetAll_GetsTheRightType()
        {
            Assert.IsType<List<User>>(repo.GetAll());
        }

        // [Fact]
        // public void GetAll_GetsAllItems()
        // {
        //     Assert.StrictEqual(0, repo.GetAll().Count());
        // }

        // [Fact]
        // public void Get_GetsByStringId()
        // {
        //     Assert.StrictEqual(_Jim, repo.Save(_Jim));
        //     Assert.StrictEqual(_Jim.Name, repo.Get(_Jim.Id.ToString()).Name);
        //     repo.Delete(_Jim.Id);
        // }

        // [Fact]
        // public void Get_GetsByGuid()
        // {
        //     Assert.StrictEqual(_Jim, repo.Save(_Jim));
        //     Assert.StrictEqual(_Jim.Name, repo.Get(_Jim.Id).Name);
        //     repo.Delete(_Jim.Id);
        // }

        // [Fact]
        // public void Get_IsCaseInsensitiveToId()
        // {
        //     Assert.StrictEqual(_Jim, repo.Save(_Jim));
        //     Assert.StrictEqual(repo.Get(_Jim.Id.ToString().ToLower()), repo.Get(_Jim.Id.ToString().ToUpper()));
        //     repo.Delete(_Jim.Id);
        // }

        // [Fact]
        // public void Update_UpdatesName()
        // {
        //     Assert.Same(_Jim, repo.Save(_Jim));
        //     var recordToChange = repo.Get(_Jim.Id);
        //     var originalName = recordToChange.Name;
        //     var newName = "updated";
        //     recordToChange.Name = newName;
        //     Assert.Same(recordToChange, repo.Save(recordToChange));
        //     Assert.StrictEqual(newName, repo.Get(_Jim.Id).Name);
        //     recordToChange.Name = originalName;
        //     Assert.Same(recordToChange, repo.Save(recordToChange));
        //     Assert.StrictEqual(originalName, repo.Get(_Jim.Id).Name);
        //     repo.Delete(_Jim.Id);
        // }

        // [Fact]
        // public void Delete_DeletesByStringId()
        // {
        //     Assert.StrictEqual(_Jim, repo.Save(_Jim));
        //     Assert.StrictEqual(_Jim.Name, repo.Get(_Jim.Id).Name);
        //     repo.Delete(_Jim.Id);
        //     Assert.Null(repo.Get(_Jim.Id));
        // }
    }
}
