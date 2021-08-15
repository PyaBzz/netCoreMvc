using System;
using Xunit;
using myCoreMvc.App.Services;

namespace myCoreMvc.App.Test
{
    [Trait("Group", "Etc")]
    public class HashFactoryTest : IDisposable
    {
        private const int LENGTH = 16;
        private readonly IHashFactory factory;

        public HashFactoryTest(IHashFactory fac)
        {
            this.factory = fac;
        }

        public void Dispose()
        {
        }

        [Fact]
        public void Get_GetsTheRightLength()
        {
            var salt = factory.GetSalt();
            Assert.StrictEqual(LENGTH, salt.Length);
        }

        [Fact]
        public void Get_GetsNonZeroValues()
        {
            var salt = factory.GetSalt();
            Assert.Contains(salt, x => x != 0);
        }

        [Fact]
        public void Get_GetsUniqueSalts()
        {
            var salt1 = factory.GetSalt();
            var salt2 = factory.GetSalt();
            for (var i = 0; i < LENGTH; i++)
            {
                salt1[i] = (byte)(salt1[i] ^ salt2[i]);
            }
            Assert.Contains(salt1, x => x != 0);
        }
    }
}
