using System;
using Baz.Core;
using myCoreMvc.Domain;
using myCoreMvc.Domain.Attributes;

namespace myCoreMvc.Persistence
{
    public class DummyA : Thing, IClonable
    {
        [Persist]
        public string Name { get; set; }

        [Persist]
        public Guid RefId { get; set; }

        [Persist]
        public Guid? NullableRefId { get; set; }

        [Persist, Mandatory]
        public Guid? MandatoryRefId { get; set; }

        [Persist]
        public int Qty { get; set; }
    }
}
