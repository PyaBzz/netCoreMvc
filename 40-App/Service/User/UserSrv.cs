using myCoreMvc.Domain;
using Baz.Core;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace myCoreMvc.App.Services
{
    public class UserSrv : IUserSrv
    {
        // private IDataRepo DataRepo;

        // public UserBiz(IDataRepo dataRepo)
        //     => DataRepo = dataRepo;

        User IUserSrv.Get(Guid id) => throw new NotImplementedException();

        List<User> IUserSrv.GetList() => throw new NotImplementedException();

        Task<bool> IUserSrv.GetPrincipal(string userName, string passWord, out ClaimsPrincipal claimsPrincipal)
        {
            claimsPrincipal = null;

            if ((this as IUserSrv).ValidateCredentials(userName, passWord, out var user).Result == false)
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

        Task<bool> IUserSrv.ValidateCredentials(string userName, string passWord, out User user)
        {
            throw new NotImplementedException();
            // user = DataRepo.Get<User>(u => u.Name.Equals(userName, StringComparison.OrdinalIgnoreCase));
            // if (user != null)
            // {
            //     var existingHash = user.Hash;
            //     var hashBytes = KeyDerivation.Pbkdf2(passWord, user.Salt, KeyDerivationPrf.HMACSHA512, 100, 256 / 8);
            //     var hash = Convert.ToBase64String(hashBytes);
            //     if (hash == existingHash)
            //         return Task.FromResult(true);
            // }
            // return Task.FromResult(false);
        }

        IUserSrvOf IUserSrv.Of(User user)
            => throw new NotImplementedException();
    }
}
