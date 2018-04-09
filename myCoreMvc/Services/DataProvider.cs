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
                        new WorkItem { Id = 1, Priority = 1, Name = "FirstItem" },
                        new WorkItem { Id = 2, Priority = 2, Name = "SecondItem" },
                        new WorkItem { Id = 3, Priority = 3, Name = "ThirdItem" }
                    };
                }
                return _WorkItems;
            }
        }

        public static bool SaveWorkItem(WorkItem workItem)
        {
            var targetItem = WorkItems.SingleOrDefault(wi => wi.Id == workItem.Id);

            if (targetItem == null)
            {
                _WorkItems.Add(workItem);
                return true;
            }
            else
            {
                targetItem.Name = workItem.Name;
                targetItem.Priority = workItem.Priority;
                return false;
            }
        }
    }
}
