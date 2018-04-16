using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace myCoreMvc.Models
{
    public class Thing
    {
        public Guid Id { get; set; }

        public T CopyMembersFrom<T>(T origin) where T : Thing
        {
            var propertyInfos = typeof(T).GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
            foreach (var propertyInfo in propertyInfos)
            {
                propertyInfo.SetValue(this, propertyInfo.GetValue(origin));
            }
            return this as T;
        }
    }
}
