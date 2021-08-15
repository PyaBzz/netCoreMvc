using System;

namespace myCoreMvc.App.Services
{
    public interface IHashFactory
    {
        Byte[] GetSalt();

        Byte[][] GetSalt(int count);

        string GetHash(string password, byte[] salt);
    }
}
