using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myCoreMvc
{
    public static class Extensios
    {
        public static int Factorial(this int instance)
        {
            if (instance < 2) return 1;
            var result = 1;
            for (var i = 2; i <= instance; i++) result *= i;
            return result;
        }
    }
}
