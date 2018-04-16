using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        public static T Clone<T>(this T origin) where T : IClonable, new()
        {
            var result = new T();
            var propertyInfos = typeof(T).GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
            foreach (var propertyInfo in propertyInfos)
            {
                propertyInfo.SetValue(result, propertyInfo.GetValue(origin));
            }
            return result;
        }
    }
}
