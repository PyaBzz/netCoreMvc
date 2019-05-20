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

        User IUserBiz.Get(Guid id) => DataProvider.Get<User>(id);

        List<User> IUserBiz.GetList() => DataProvider.GetList<User>();

        Task<bool> IUserBiz.GetPrincipal(string userName, string passWord, out ClaimsPrincipal claimsPrincipal)
        {
            claimsPrincipal = null;

            if ((this as IUserBiz).ValidateCredentials(userName, passWord, out var user).Result == false)
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

        Task<bool> IUserBiz.ValidateCredentials(string userName, string passWord, out User user)
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

        IUserBizOf IUserBiz.Of(User user)
            => new UserBizOf(DataProvider, user);
    }
}
