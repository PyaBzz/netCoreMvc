using myCoreMvc.Models;
using PooyasFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace myCoreMvc.Services
{
    public class UserServiceMock : IUserService
    {
        private IDataProvider DataProvider = ServiceInjector.Resolve<IDataProvider>();

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
                var hash = passWord;
                if (hash == existingHash)
                    return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public TransactionResult Save(User user)
        {
            TransactionResult transactionResult;
            if (user.Id == Guid.Empty)
            {
                //user.Salt = 
                user.Hash =
                transactionResult = DataProvider.Add(user);
            }
            else
            {
                transactionResult = DataProvider.Update(user);
            }
            return transactionResult;
        }
    }
}
