using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using PyaFramework.Core;

namespace myCoreMvc.Domain
{
    public class WorkPlan : Thing, IClonable
    {
        /*================================  Properties ================================*/

        [Display(Name = "Plan name"), Required]
        public string Name { get; set; }

        public IEnumerable<WorkItem> WorkItems { get; set; }

        /*==================================  Methods =================================*/

        public override string ToString() => Name;
    }
}
