using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using PyaFramework.Core;

namespace myCoreMvc.Models
{
    public class WorkItem : Thing, IClonable
    {
        /*================================  Properties ================================*/

        public String Reference { get; set; }
        public int Priority { get; set; }
        [Display(Name = "Item name"), Required]
        public string Name { get; set; }
        public WorkPlan WorkPlan { get; set; }

        public static IEnumerable<int> PriorityChoices { get; } = new List<int> { 1, 2, 3, 4 }; //Task: Make it a static field

        /*==================================  Methods =================================*/

        public override string ToString() => Name;
    }
}
