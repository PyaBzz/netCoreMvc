using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using myCoreMvc.Domain;
using myCoreMvc.Domain.Attributes;

namespace myCoreMvc.Persistence.Services
{
    // Todo: Unit test with a dynamically created class at runtime
    public class DbMap<T>
    {
        private static readonly Dictionary<Type, string> tableNames = new Dictionary<Type, string>
        {
            {typeof(DummyA), "DummiesA"},
            {typeof(User), "Users"},
            {typeof(Product), "Products"},
            {typeof(Order), "Orders"}
        };

        private static Dictionary<Type, string[]> propsMemo = new Dictionary<Type, string[]>();

        public string Table => tableNames[typeof(T)];

        private string[] GetPropNames()
        {
            string[] res;
            if (propsMemo.TryGetValue(typeof(T), out res))
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
                propsMemo.Add(typeof(T), res);
            }
            return res;
        }

        public string GetColumns()
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
