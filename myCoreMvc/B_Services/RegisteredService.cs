using System;
using PyaFramework.Core;

namespace myCoreMvc.Services
{
    public class RegisteredService
    {
        public Type TypeToResolve { get; }
        public Type ConcreteType { get; }
        public Injection Injection { get; }
        public object Instance { get; set; }

        public RegisteredService(Type typeToResolve, Type concreteType, Injection injection)
        {
            TypeToResolve = typeToResolve;
            ConcreteType = concreteType;
            Injection = injection;
        }

        internal void CreateInstance(object[] ConstructorParams)
        {
            Instance = Activator.CreateInstance(ConcreteType, ConstructorParams);
        }
    }
}
