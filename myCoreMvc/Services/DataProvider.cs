using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using myCoreMvc.Models;

namespace myCoreMvc
{
    public class DataProvider
    {
        private static List<WorkItem> _WorkItems;

        public static IEnumerable<WorkItem> WorkItems
        {
            get
            {
                if (_WorkItems == null)
                {
                    _WorkItems = new List<WorkItem>
                    {
                        new WorkItem { Id = new Guid("1d8c794d-da09-4227-b5cd-5b91b9f4f7fd"), Reference = "WI1", Priority = 1, Name = "FirstItem" },
                        new WorkItem { Id = new Guid("83f5d9c1-3b3b-41ed-82f2-f25193dba798"), Reference = "WI2", Priority = 2, Name = "SecondItem" },
                        new WorkItem { Id = new Guid("ddc59522-fc29-46cd-a944-5b05d320a9e5"), Reference = "WI3", Priority = 3, Name = "ThirdItem" }
                    };
                }
                return _WorkItems;
            }
        }

        public static bool Save<T>(T obj) where T : Thing, new()
        {
            if (obj.Id == Guid.Empty)
            {
                obj.Id = Guid.NewGuid();
                _WorkItems.Add(obj as WorkItem);
                return true;
            }
            else
            {
                var existingObj = WorkItems.SingleOrDefault(wi => wi.Id == obj.Id) as T;

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
