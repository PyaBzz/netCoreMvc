using System;
using Xunit;
using myCoreMvc.App.Services;
using myCoreMvc.Domain;
using System.Linq;
using System.Collections.Generic;
using myCoreMvc.App;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Baz.Core;
using System.Security.Cryptography;

namespace myCoreMvc.Test.DataLayer
{
    [Trait("Group", "Data")]
    public class UserRepoTest
    {
        private readonly User _Jim;
        private readonly User _Sam;
        private readonly User _Adam;

        private readonly IUserRepo repo;

        public UserRepoTest(IUserRepo rep)
        {
            this.repo = rep;

            using (var rng = RandomNumberGenerator.Create())
            {
                var Salts = new[] { new byte[128 / 8], new byte[128 / 8], new byte[128 / 8] };
                foreach (var salt in Salts)
                    rng.GetBytes(salt);

                _Jim = new User
                {
                    Id = new Guid("5d45a66d-fc2d-4a7f-b9dc-aac9f723f034"),
                    Salt = Salts[0],
                    Name = "Jim",
                    Hash = Convert.ToBase64String(KeyDerivation.Pbkdf2("jjj", Salts[0], KeyDerivationPrf.HMACSHA512, 100, 256 / 8)),
                    DateOfBirth = new DateTime(2018, 01, 22),
                    Role = AuthConstants.JuniorRoleName
                };
                _Sam = new User
                {
                    Id = new Guid("91555540-6137-4668-9d55-5c22471237f3"),
                    Salt = Salts[1],
                    Name = "Sam",
                    Hash = Convert.ToBase64String(KeyDerivation.Pbkdf2("sss", Salts[1], KeyDerivationPrf.HMACSHA512, 100, 256 / 8)),
                    DateOfBirth = new DateTime(2010, 01, 22),
                    Role = AuthConstants.SeniorRoleName
                };
                _Adam = new User
                {
                    Id = new Guid("97ba3d59-a990-4b55-ba91-7865fca0a4a2"),
                    Salt = Salts[2],
                    Name = "Adam",
                    Hash = Convert.ToBase64String(KeyDerivation.Pbkdf2("aaa", Salts[2], KeyDerivationPrf.HMACSHA512, 100, 256 / 8)),
                    DateOfBirth = new DateTime(2000, 01, 22),
                    Role = AuthConstants.AdminRoleName
                };
            }
        }

        [Fact]
        public void Add_Adds()
        {
            Assert.StrictEqual(0, repo.GetAll().Count());
            Assert.Null(repo.Get(_Jim.Id));
            Assert.StrictEqual(_Jim, repo.Add(_Jim));
            Assert.StrictEqual(1, repo.GetAll().Count());
            Assert.StrictEqual(_Jim.Name, repo.Get(_Jim.Id).Name);
            repo.Delete(_Jim.Id);
            Assert.StrictEqual(0, repo.GetAll().Count());
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
        }

        [Fact]
        public void Get_GetsByStringId()
        {
            Assert.StrictEqual(_Jim, repo.Add(_Jim));
            Assert.StrictEqual(_Jim.Name, repo.Get(_Jim.Id.ToString()).Name);
            repo.Delete(_Jim.Id);
        }

        [Fact]
        public void Get_GetsByGuid()
        {
            Assert.StrictEqual(_Jim, repo.Add(_Jim));
            Assert.StrictEqual(_Jim.Name, repo.Get(_Jim.Id).Name);
            repo.Delete(_Jim.Id);
        }

        [Fact]
        public void Get_IsCaseInsensitiveToId()
        {
            Assert.StrictEqual(_Jim, repo.Add(_Jim));
            Assert.StrictEqual(repo.Get(_Jim.Id.ToString().ToLower()), repo.Get(_Jim.Id.ToString().ToUpper()));
            repo.Delete(_Jim.Id);
        }

        [Fact]
        public void Update_UpdatesName()
        {
            Assert.Same(_Jim, repo.Add(_Jim));
            var recordToChange = repo.Get(_Jim.Id);
            var originalName = recordToChange.Name;
            var newName = "updated";
            recordToChange.Name = newName;
            Assert.Same(recordToChange, repo.Update(recordToChange));
            Assert.StrictEqual(newName, repo.Get(_Jim.Id).Name);
            recordToChange.Name = originalName;
            Assert.Same(recordToChange, repo.Update(recordToChange));
            Assert.StrictEqual(originalName, repo.Get(_Jim.Id).Name);
            repo.Delete(_Jim.Id);
        }

        [Fact]
        public void Delete_DeletesByStringId()
        {
            Assert.StrictEqual(_Jim, repo.Add(_Jim));
            Assert.StrictEqual(_Jim.Name, repo.Get(_Jim.Id).Name);
            repo.Delete(_Jim.Id);
            Assert.Null(repo.Get(_Jim.Id));
        }
    }
}
