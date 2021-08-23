using System;
using System.Linq;
using System.Reflection;
using Baz.Core;
using myCoreMvc.Domain.Attributes;

namespace myCoreMvc.Domain
{
    public abstract class Thing : ISavable, IClonable //Todo: Do we need IClonable?
    {
        [Persist]
        public Guid? Id { get; set; }

        public void Validate() //Todo: Unit test
        {
            var selfType = this.GetType();
            var mandatoryProps = selfType
            .GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
            .Where(x => Attribute.IsDefined(x, typeof(Mandatory)));
            foreach (var prop in mandatoryProps)
            {
                if (prop.GetValue(this) == null)
                {
                    throw new NullReferenceException($"Property {prop.Name} is {nameof(Mandatory)} for object type {selfType.Name}");
                }
            }
        }

        public override bool Equals(object obj)
        {
            if (Id.HasValue == false || obj == null || GetType() != obj.GetType())
                return false;

            return Id == ((Thing)obj).Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
