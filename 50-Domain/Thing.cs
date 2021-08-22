using System;
using Baz.Core;
using myCoreMvc.Domain.Attributes;

namespace myCoreMvc.Domain
{
    public abstract class Thing : ISavable, IClonable
    {
        [Persist]
        public Guid? Id { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            return Id == ((Thing)obj).Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
