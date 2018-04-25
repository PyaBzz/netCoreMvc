using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace myCoreMvc
{
    public static class Extensions
    {
        public static int Factorial(this int instance)
        {
            if (instance < 2) return 1;
            var result = 1;
            for (var i = 2; i <= instance; i++) result *= i;
            return result;
        }

        /// <summary>
        /// Returns an object that is member-wise copy of the given instance.
        /// </summary>
        /// <typeparam name="T">is any type that implements IClonable interface.</typeparam>
        /// <param name="origin"></param>
        /// <returns></returns>
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

        public static T CopyPropertiesFrom<T>(this T it, T origin) where T : IClonable
        {
            var propertyInfos = typeof(T).GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
            foreach (var propertyInfo in propertyInfos)
            {
                propertyInfo.SetValue(it, propertyInfo.GetValue(origin));
            }
            return it;
        }

        public static T CopyCommonPropertiesFrom<T, U>(this T it, U origin) where T : IClonable
        {
            var propertyInfosOfT = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);
            var propertyInfosOfU = typeof(U).GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (var propertyInfo in propertyInfosOfT)
            {
                var correspondingPiOfU = propertyInfosOfU.SingleOrDefault(pi => pi.Name == propertyInfo.Name && pi.PropertyType == propertyInfo.PropertyType);
                if (correspondingPiOfU != null)
                {
                    var value = typeof(U).GetProperty(propertyInfo.Name).GetValue(origin);
                    propertyInfo.SetValue(it, value);
                }
            }
            return it;
        }

        public static T CopyPropertiesTo<T>(this T origin, T target) where T : IClonable
        {
            var propertyInfos = typeof(T).GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
            foreach (var propertyInfo in propertyInfos)
            {
                propertyInfo.SetValue(target, propertyInfo.GetValue(origin));
            }
            return origin;
        }
    }
}
