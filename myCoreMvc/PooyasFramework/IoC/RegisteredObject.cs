using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myCoreMvc.PooyasFramework.IoC
{
    public class RegisteredObject
    {
        public Type TypeToResolve { get; }
        public Type ConcreteType { get; }
        public LifeCycle LifeCycle { get; }
        public object Instance { get; set; }

        public RegisteredObject(Type typeToResolve, Type concreteType, LifeCycle lifeCycle)
        {
            TypeToResolve = typeToResolve;
            ConcreteType = concreteType;
            LifeCycle = lifeCycle;
        }

        internal void CreateInstance(object[] ConstructorParams)
        {
            Instance = Activator.CreateInstance(ConcreteType, ConstructorParams);
        }
    }
}
