using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using myCoreMvc.Domain;
using Baz.Core;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace myCoreMvc.App.Services
{
    public class SaltFactory
    {
        public Byte[] Get()
        {
            var res = new Byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(res);
            }
            return res;
        }

        public Byte[][] GetMany(int count)
        {
            Byte[][] res = new Byte[count][];
            using (var rng = RandomNumberGenerator.Create())
            {
                for (var i = 0; i < count; i++)
                {
                    res[i] = new byte[128 / 8];
                    rng.GetBytes(res[i]);
                }
            }
            return res;
        }
    }
}
