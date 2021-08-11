using System;
using Baz.Core;

namespace myCoreMvc.Domain
{
    public class Thing : ISavable, IClonable
    {
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
