using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace myCoreMvc.Models
{
    public class WorkItem
    {
        public int Id { get; set; }
        public int Priority { get; set; }
        [Display(Name ="Item name"), Required]
        public string Name { get; set; }

        private static IEnumerable<int> _PriorityChoices = new List<int> { 1, 2, 3, 4 };

        public static WorkItem FindById(int id)
        {
            return DataProvider.WorkItems.SingleOrDefault(i => i.Id == id);
        }

        public static IEnumerable<int> PriorityChoices
        {
            get
            {
                return _PriorityChoices;
            }
        }
    }
}
