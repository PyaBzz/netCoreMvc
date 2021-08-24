using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using myCoreMvc.Domain;
using System;

namespace myCoreMvc.App.Services
{
    public class UserSrvOf : IUserSrvOf
    {
        // private IDataRepo DataRepo;
        public User User { get; }

        // public UserBizOf(IDataRepo dataRepo, User user)
        // {
        //     DataRepo = dataRepo;
        //     User = user;
        // }

        User IUserSrvOf.Save()
        {
            throw new NotImplementedException();
            //Task: Is this the best way to determine if the object is new?
            // if (User.Id == Guid.Empty)
            // {
            //     User.Salt = new byte[128 / 8];
            //     using (var rng = RandomNumberGenerator.Create())
            //     {
            //         rng.GetBytes(User.Salt);
            //     }
            // }
            // return DataRepo.Save(User);
        }

        User IUserSrvOf.SetPassword(string password)
        {
            var hashBytes = KeyDerivation.Pbkdf2(password, User.Salt, KeyDerivationPrf.HMACSHA512, 100, 256 / 8);
            User.Hash = Convert.ToBase64String(hashBytes);
            return User;
        }

        void IUserSrvOf.Delete()
        {
            throw new NotImplementedException();
            // DataRepo.Delete<User>(User.Id);
        }
    }
}
