using System;
using Baz.Core;
using myCoreMvc.Domain.Attributes;

namespace myCoreMvc.Domain
{
    public class Order : Thing, IClonable
    {
        //Task: Is PriorityChoices a responsibility of Order or its Biz class?
        public static readonly int[] PriorityChoices = new[] { 1, 2, 3, 4 };

        /*================================  Properties ================================*/

        [Persist]
        public string Reference { get; set; }

        [Persist]
        public int Priority { get; set; }

        [Persist]
        public string Name { get; set; }

        [Persist]
        public Guid? ProductId { get; set; }

        public Product Product { get; set; }

        /*==================================  Methods =================================*/

        public override string ToString() => Name;
    }
}
