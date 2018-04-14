using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using myCoreMvc.Models;

namespace myCoreMvc
{
    public class DataProvider
    {
        private static List<WorkPlan> _WorkPlans;

        private static IEnumerable<WorkPlan> WorkPlans
        {
            get
            {
                if (_WorkPlans == null)
                {
                    _WorkPlans = new List<WorkPlan>
                    {
                        new WorkPlan { Id = Guid.NewGuid(), Name = "Plan1" },
                        new WorkPlan { Id = Guid.NewGuid(), Name = "Plan2" },
                    };
                }
                return _WorkPlans;
            }
        }

        private static List<WorkItem> _WorkItems;

        private static IEnumerable<WorkItem> WorkItems
        {
            get
            {
                if (_WorkItems == null)
                {
                    _WorkItems = new List<WorkItem>
                    {
                        new WorkItem { Id = Guid.NewGuid(), Reference = "WI1", Priority = 1, Name = "FirstItem" },
                        new WorkItem { Id = Guid.NewGuid(), Reference = "WI2", Priority = 2, Name = "SecondItem" },
                        new WorkItem { Id = Guid.NewGuid(), Reference = "WI3", Priority = 3, Name = "ThirdItem" }
                    };
                }
                return _WorkItems;
            }
        }

        public static IEnumerable<T> GetList<T>()
        {
            var source = typeof(DataProvider).GetProperties(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public)
                .SingleOrDefault(pi => pi.PropertyType == typeof(IEnumerable<T>))?.GetValue(null) as IEnumerable<T>;
            if (source == null) throw new NullReferenceException($"DataProvider knows no source collection of type {typeof(T)}");
            return source;
        }

        public static IEnumerable<T> GetList<T>(Func<T, bool> func)
        {
            var source = typeof(DataProvider).GetProperties(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public)
                .SingleOrDefault(pi => pi.PropertyType == typeof(IEnumerable<T>))?.GetValue(null) as IEnumerable<T>;
            if (source == null) throw new NullReferenceException($"DataProvider knows no source collection of type {typeof(T)}");
            return source.Where(i => func(i));
        }

        public static T Get<T>(Func<T, bool> func)
        {
            var source = typeof(DataProvider).GetProperties(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public)
                .SingleOrDefault(pi => pi.PropertyType == typeof(IEnumerable<T>))?.GetValue(null) as IEnumerable<T>;
            if (source == null) throw new NullReferenceException($"DataProvider knows no source collection of type {typeof(T)}");
            return source.SingleOrDefault(i => func(i));
        }

        public static bool Save<T>(T obj) where T : Thing, new() // TODO: Use generics + reflection like the Get<T> method
        {
            if (obj.Id == Guid.Empty)
            {
                obj.Id = Guid.NewGuid();
                _WorkItems.Add(obj as WorkItem);
                return true;
            }
            else
            {
                var existingObj = Get<WorkItem>(wi => wi.Id == obj.Id);

                if (existingObj == null)
                {
                    _WorkItems.Add(obj as WorkItem);
                    return true;
                }
                else
                {
                    var properties = typeof(T).GetProperties(System.Reflection.BindingFlags.DeclaredOnly | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
                    foreach (var property in properties)
                    {
                        typeof(T).GetProperty(property.Name).SetValue(existingObj, typeof(T).GetProperty(property.Name).GetValue(obj));
                    }
                    return false;
                }
            }
        }
    }
}
