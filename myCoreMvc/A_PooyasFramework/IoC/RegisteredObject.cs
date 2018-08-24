using System;

namespace PooyasFramework.IoC
{
    public class RegisteredObject
    {
        public Type TypeToResolve { get; }
        public Type ConcreteType { get; }
        public Injection LifeCycle { get; }
        public object Instance { get; set; }

        public RegisteredObject(Type typeToResolve, Type concreteType, Injection lifeCycle)
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
