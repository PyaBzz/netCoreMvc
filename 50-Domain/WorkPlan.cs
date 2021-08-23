using Baz.Core;
using myCoreMvc.Domain.Attributes;

namespace myCoreMvc.Domain
{
    public class WorkPlan : Thing, IClonable
    {
        /*================================  Properties ================================*/

        [Persist]
        public string Name { get; set; }

        //Todo: Make calculated
        //public List<WorkItem> WorkItems { get; set; }

        /*==================================  Methods =================================*/

        public override string ToString() => Name;
    }
}
