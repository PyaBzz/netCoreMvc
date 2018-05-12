﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PooyasFramework
{
    public static class Extensions
    {
        /// <summary>
        /// Returns an object that is member-wise copy of the given instance.
        /// </summary>
        /// <typeparam name="T">is any type that implements IClonable interface.</typeparam>
        /// <param name="origin"></param>
        /// <returns></returns>
        public static T Clone<T>(this T origin) where T : IClonable, new()
        {
            var result = new T();
            var propertyInfos = typeof(T).GetPublicDeclaredInstancePropertyInfos();
            foreach (var propertyInfo in propertyInfos)
            {
                propertyInfo.SetValue(result, propertyInfo.GetValue(origin));
            }
            return result;
        }

        public static T CopyPropertiesFrom<T>(this T target, T origin) where T : IClonable
        {
            var propertyInfos = typeof(T).GetPublicDeclaredInstancePropertyInfos();
            foreach (var propertyInfo in propertyInfos)
            {
                propertyInfo.SetValue(target, propertyInfo.GetValue(origin));
            }
            return target;
        }

        public static T CopyPropertiesTo<T>(this T origin, T target) where T : IClonable
        {
            var propertyInfos = typeof(T).GetPublicDeclaredInstancePropertyInfos();
            foreach (var propertyInfo in propertyInfos)
            {
                propertyInfo.SetValue(target, propertyInfo.GetValue(origin));
            }
            return origin;
        }

        public static T CopySimilarPropertiesFrom<T, U>(this T it, U origin) where T : IClonable
        {
            // We need to have two separate sets of PropInfos because they are unique to their types!
            var propertyInfosOfT = typeof(T).GetPublicInstancePropertyInfos();
            var propertyInfosOfU = typeof(U).GetPublicInstancePropertyInfos();
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

        public static string ToString<T>(this IEnumerable<T> source, string separator)
        {
            return string.Join(separator, source);
        }

        public static IEnumerable<PropertyInfo> GetPublicInstancePropertyInfos(this Type T)
        {
            return T.GetProperties(BindingFlags.Instance | BindingFlags.Public);
        }

        public static IEnumerable<PropertyInfo> GetPublicDeclaredInstancePropertyInfos(this Type T)
        {
            return T.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
        }

        public static string GetStringOfProperties<T>(this T obj)
        {
            var result = string.Empty;
            var strings = new List<string>();

            foreach (var propInfo in typeof(T).GetPublicInstancePropertyInfos())
            {
                strings.Add(propInfo.GetValue(obj).ToString());
            }

            return strings.ToString(" ");
        }

        public static IEnumerable<T> AppliedWithFilters<T>(this IEnumerable<T> source, IEnumerable<Func<T, bool>> filters)
        {
            foreach (var filter in filters) source = source.Where(i => filter(i));
            return source;
        }
    }
}