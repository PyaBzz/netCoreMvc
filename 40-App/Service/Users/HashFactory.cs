using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;

namespace myCoreMvc.App.Services
{
    public class HashFactory : IHashFactory
    {
        public const int LENGTH = 16;

        public Byte[] GetSalt()
        {
            var res = new Byte[LENGTH];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(res);
            }
            return res;
        }

        public Byte[][] GetSalt(int count)
        {
            Byte[][] res = new Byte[count][];
            using (var rng = RandomNumberGenerator.Create())
            {
                for (var i = 0; i < count; i++)
                {
                    res[i] = new byte[LENGTH];
                    rng.GetBytes(res[i]);
                }
            }
            return res;
        }

        public string GetHash(string password, byte[] salt)
        { //Todo: Unit test this
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA512, 100, 256 / 8));
        }
    }
}
