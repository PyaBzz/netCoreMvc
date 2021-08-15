using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Baz.Core;

namespace myCoreMvc.Domain
{
    public class WorkPlan : Thing, IClonable
    {
        /*================================  Properties ================================*/

        [Display(Name = "Plan name"), Required]
        public string Name { get; set; }

        //Todo: Make calculated
        //public List<WorkItem> WorkItems { get; set; }

        /*==================================  Methods =================================*/

        public override string ToString() => Name;
    }
}
