using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using PooyasFramework;

namespace myCoreMvc.Models
{
    public class WorkPlan : Thing, IClonable
    {
        [Display(Name = "Plan name"), Required]
        public string Name { get; set; }

        public IEnumerable<WorkItem> WorkItems { get; set; }

        public override string ToString() => Name;
    }
}
