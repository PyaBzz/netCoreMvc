using System;
using System.ComponentModel.DataAnnotations;
using Py.Core;

namespace myCoreMvc.Domain
{
    public class WorkItem : Thing, IClonable
    {
        //Task: Is PriorityChoices a responsibility of WorkItem or its Biz class?
        public static readonly int[] PriorityChoices = new[] { 1, 2, 3, 4 };

        /*================================  Properties ================================*/

        public string Reference { get; set; }
        public int Priority { get; set; }
        [Display(Name = "Item name"), Required]
        public string Name { get; set; }
        public WorkPlan WorkPlan { get; set; }

        /*==================================  Methods =================================*/

        public override string ToString() => Name;
    }
}
