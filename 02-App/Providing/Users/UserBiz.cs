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
    public class UserBiz : IUserBiz
    {
        private IDataProvider DataProvider;

        public UserBiz(IDataProvider dataProvider)
            => DataProvider = dataProvider;

        public User Get(Guid id) => DataProvider.Get<User>(id);

        public List<User> GetList() => DataProvider.GetList<User>();

        public TransactionResult Delete(Guid id) => DataProvider.Delete<User>(id);

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

        public Task<bool> ValidateCredentials(string userName, string passWord, out User user)
        {
            user = DataProvider.Get<User>(u => u.Name.Equals(userName, StringComparison.OrdinalIgnoreCase));
            if (user != null)
            {
                var existingHash = user.Hash;
                var hashBytes = KeyDerivation.Pbkdf2(passWord, user.Salt, KeyDerivationPrf.HMACSHA512, 100, 256 / 8);
                var hash = Convert.ToBase64String(hashBytes);
                if (hash == existingHash)
                    return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public TransactionResult Save(User user)
        {
            if (user.Id == Guid.Empty)
            {
                user.Salt = new byte[128 / 8];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(user.Salt);
                }
                return DataProvider.Add(user);
            }
            else
            {
                var existingUser = DataProvider.Get<User>(user.Id);
                user.Salt = existingUser.Salt;
                user.Hash = existingUser.Hash;
                return DataProvider.Update(user);
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
