using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PooyasFramework
{
    // A delegate is a TYPE that safely encapsulates a METHOD!
    public delegate string DelegateType(int i);

    public static class Delegates
    {
        public static string DelegateImplementation1(int i)
        {
            return "Method 1 running! " + i.ToString();
        }
        public static string DelegateImplementation2(int i)
        {
            return "Method 2 running! " + i.ToString();
        }
    }
}
