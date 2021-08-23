using System;
using System.ComponentModel.DataAnnotations;
using Baz.Core;
using myCoreMvc.Domain;
using myCoreMvc.Domain.Attributes;

namespace myCoreMvc.Persistence
{
    public class DummyA : Thing, IClonable
    {
        [Persist]
        public string Name { get; set; }

        // [Persist]
        // public string Reference { get; set; }

        // [Persist]
        // public int Priority { get; set; }

        // [Persist]
        // public Guid WorkPlanId { get; set; }

        // public WorkPlan WorkPlan { get; set; }
    }
}
