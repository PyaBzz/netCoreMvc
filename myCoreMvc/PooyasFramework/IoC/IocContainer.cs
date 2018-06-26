using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace myCoreMvc.PooyasFramework.IoC
{
    public enum LifeCycle { Singleton, Transient };

    public class IocContainer
    {
        private readonly List<RegisteredObject> registeredObjects = new List<RegisteredObject>();

        //public void Register<TypeToResolve, ConcreteType>()
        //{
        //    Register<TypeToResolve, ConcreteType>(LifeCycle.Singleton);
        //}

        public void Register<TypeToResolve, ConcreteType>(LifeCycle lifeCycle)
        {
            registeredObjects.Add(new RegisteredObject(typeof(TypeToResolve), typeof(ConcreteType), lifeCycle));
        }

        public TypeToResolve Resolve<TypeToResolve>()
        {
            return (TypeToResolve)ResolveObject(typeof(TypeToResolve));
        }

        //public object Resolve(Type typeToResolve)
        //{
        //    return ResolveObject(typeToResolve);
        //}

        private object ResolveObject(Type typeToResolve)
        {
            var registeredObject = registeredObjects.FirstOrDefault(o => o.TypeToResolve == typeToResolve);
            if (registeredObject == null)
            {
                throw new Exception($"The type {typeToResolve.Name} has not been registered");
            }
            return GetInstance(registeredObject);
        }

        private object GetInstance(RegisteredObject registeredObject)
        {
            if (registeredObject.Instance == null || registeredObject.LifeCycle == LifeCycle.Transient)
            {
                var parameters = ResolveConstructorParameters(registeredObject);
                registeredObject.CreateInstance(parameters.ToArray());
            }
            return registeredObject.Instance;
        }

        private IEnumerable<object> ResolveConstructorParameters(RegisteredObject registeredObject)
        {
            var constructorInfo = registeredObject.ConcreteType.GetConstructors().First();
            foreach (var parameter in constructorInfo.GetParameters())
            {
                yield return ResolveObject(parameter.ParameterType);
            }
        }
    }
}
