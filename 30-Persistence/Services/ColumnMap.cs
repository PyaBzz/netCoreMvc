using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Baz.Core;
using myCoreMvc.Domain.Attributes;

namespace myCoreMvc.Persistence.Services
{
    // Todo: Unit test with a dynamically created class at runtime
    public class ColumnMap<T>
    {
        private static Dictionary<Type, string[]> memo = new Dictionary<Type, string[]>();

        private string[] GetPropNames()
        {
            string[] res;
            if (memo.TryGetValue(typeof(T), out res))
            {
                // Console.WriteLine("Found in cache!");
            }
            else
            {
                // Console.WriteLine("Not in cache!");
                res = typeof(T)
                .GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
                .Where(x => Attribute.IsDefined(x, typeof(Persist)))
                .Select(x => x.Name)
                .ToArray();
                memo.Add(typeof(T), res);
            }
            return res;
        }
        public string Get()
        {
            var propNames = GetPropNames();
            return String.Join(", ", propNames);
        }

        public string GetPlaceholders()
        {
            var propNames = GetPropNames().Select(x => "@" + x).ToArray();
            return String.Join(", ", propNames);
        }

        public string GetAssignments()
        {
            var propNames = GetPropNames();
            propNames = propNames.Select(x => $"{x} = @{x}").ToArray();
            return String.Join(", ", propNames);
        }
    }
}
