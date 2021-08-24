using Baz.Core;
using myCoreMvc.Domain.Attributes;

namespace myCoreMvc.Domain
{
    public class Product : Thing, IClonable
    {
        /*================================  Properties ================================*/

        [Persist]
        public string Name { get; set; }

        //Todo: Make calculated
        //public List<Order> Orders { get; set; }

        /*==================================  Methods =================================*/

        public override string ToString() => Name;
    }
}
