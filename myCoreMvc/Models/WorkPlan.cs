using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace myCoreMvc.Models
{
    public class WorkPlan : Thing
    {
        [Display(Name = "Plan name"), Required]
        public string Name { get; set; }

        public IEnumerable<WorkItem> WorkItems { get; set; }
    }
}
