using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using myCoreMvc.App.Consuming;
using myCoreMvc.Domain;
using PyaFramework.Core;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace myCoreMvc.App.Providing
{
    public class UserBizOf : IUserBizOf
    {
        private IDataProvider DataProvider;
        public User User { get; }

        public UserBizOf(IDataProvider dataProvider, User user)
        {
            DataProvider = dataProvider;
            User = user;
        }

        TransactionResult IUserBizOf.Save()
        {
            //Task: Is this the best way to determine if the object is new?
            if (User.Id == Guid.Empty)
            {
                User.Salt = new byte[128 / 8];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(User.Salt);
                }
            }
            return DataProvider.Save(User);
        }

        TransactionResult IUserBizOf.SetPassword(string password)
        {
            if (User == null)
                return TransactionResult.NotFound;

            var hashBytes = KeyDerivation.Pbkdf2(password, User.Salt, KeyDerivationPrf.HMACSHA512, 100, 256 / 8);
            User.Hash = Convert.ToBase64String(hashBytes);
            return TransactionResult.Updated;
        }

        TransactionResult IUserBizOf.Delete() => DataProvider.Delete<User>(User.Id);
    }
}
