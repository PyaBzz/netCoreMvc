using System;
using Xunit;
using myCoreMvc.App.Services;
using myCoreMvc.App.Interfaces;
using myCoreMvc.Domain;
using System.Linq;
using Baz.Core;
using System.Collections.Generic;

namespace myCoreMvc.Persistence.Test
{
    [Trait("Group", "Repos")]
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
    }
}
