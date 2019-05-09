using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using myCoreMvc.Domain;
using PyaFramework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace myCoreMvc.App.Consuming
{
    public class UserServiceMock : IUserService
    {
        private IDataProvider DataProvider;

        internal UserServiceMock(IDataProvider dataProvider)
        {
            DataProvider = dataProvider;
        }

        public Task<bool> GetPrincipal(string userName, string passWord, out ClaimsPrincipal claimsPrincipal)
        {
            claimsPrincipal = null;

            if (ValidateCredentials(userName, passWord, out var user).Result == false)
                return Task.FromResult(false);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var claimsIdentity = new ClaimsIdentity
                (
                    claims: claims,
                    authenticationType: AuthConstants.SchemeName,
                    nameType: ClaimTypes.Name,
                    roleType: ClaimTypes.Role
                );

            claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            return Task.FromResult(true);
        }

        public Task<bool> ValidateCredentials(string userName, string passWord, out IUser iUser)
        {
            iUser = DataProvider.Get<User>(u => u.Name.Equals(userName, StringComparison.OrdinalIgnoreCase));
            if (iUser != null)
            {
                var existingHash = iUser.Hash;
                var hashBytes = KeyDerivation.Pbkdf2(passWord, iUser.Salt, KeyDerivationPrf.HMACSHA512, 100, 256 / 8);
                var hash = Convert.ToBase64String(hashBytes);
                if (hash == existingHash)
                    return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public TransactionResult Save(IUser iUser)
        {
            if (iUser.Id == Guid.Empty)
            {
                iUser.Salt = new byte[128 / 8];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(iUser.Salt);
                }
                return DataProvider.Add(iUser);
            }
            else
            {
                var existingUser = DataProvider.Get<User>(iUser.Id);
                iUser.Salt = existingUser.Salt;
                iUser.Hash = existingUser.Hash;
                return DataProvider.Update(iUser);
            }
        }

        public TransactionResult SetPassword(Guid id, string password)
        {
            var user = DataProvider.Get<User>(id);
            if (user == null)
                return TransactionResult.NotFound;

            var hashBytes = KeyDerivation.Pbkdf2(password, user.Salt, KeyDerivationPrf.HMACSHA512, 100, 256 / 8);
            user.Hash = Convert.ToBase64String(hashBytes);
            return TransactionResult.Updated;
        }
    }
}
