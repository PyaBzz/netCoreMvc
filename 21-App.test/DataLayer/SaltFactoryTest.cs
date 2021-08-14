using System;
using Xunit;
using myCoreMvc.App.Services;
using myCoreMvc.Domain;
using System.Linq;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Baz.Core;
using System.Security.Cryptography;
using System.Collections.Generic;

namespace myCoreMvc.Test
{
    [Trait("Group", "Etc")]
    public class SaltFactoryTest : IDisposable
    {
        private const int LENGTH = 16;
        private readonly ISaltFactory factory;

        public SaltFactoryTest(ISaltFactory saltFac)
        {
            this.factory = saltFac;
        }

        public void Dispose()
        {
        }

        [Fact]
        public void Get_GetsTheRightLength()
        {
            var salt = factory.Get();
            Assert.StrictEqual(LENGTH, salt.Length);
        }

        [Fact]
        public void Get_GetsNonZeroValues()
        {
            var salt = factory.Get();
            Assert.Contains(salt, x => x != 0);
        }

        [Fact]
        public void Get_GetsUniqueSalts()
        {
            var salt1 = factory.Get();
            var salt2 = factory.Get();
            for (var i = 0; i < LENGTH; i++)
            {
                salt1[i] = (byte)(salt1[i] ^ salt2[i]);
            }
            Assert.Contains(salt1, x => x != 0);
        }
    }
}
