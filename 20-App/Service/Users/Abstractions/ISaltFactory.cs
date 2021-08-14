using System;

namespace myCoreMvc.App.Services
{
    public interface ISaltFactory
    {
        Byte[] Get();

        Byte[][] GetMany(int count);
    }
}
