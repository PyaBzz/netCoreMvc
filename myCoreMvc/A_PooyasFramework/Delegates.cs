using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PooyasFramework
{
    // A delegate is a TYPE that safely encapsulates a METHOD!
    public delegate string DelegateType(string s);

    public static class Delegates
    {
        public static string DelegateImplementation1(string parameter)
        {
            return $"{parameter} is now in Method 1 !!";
        }

        public static string DelegateImplementation2(string parameter)
        {
            return $"{parameter} is now in Method 2 !!";
        }
    }
}
