using System;

using PooyasFramework;
using System.Collections.Generic;
using System.Linq;

namespace myCoreMvc.Services
{
    //TODO: Further develop this container (factory) to inject concrete implementation of services

    public class ServiceInjector
    {
        private static readonly List<RegisteredService> registeredServices = new List<RegisteredService>();

        public static void Register<TypeToResolve, ConcreteType>(Injection injection)
        {
            registeredServices.Add(new RegisteredService(typeof(TypeToResolve), typeof(ConcreteType), injection));
        }

        public static TypeToResolve Resolve<TypeToResolve>()
        {
            return (TypeToResolve)ResolveService(typeof(TypeToResolve));
        }

        private static object ResolveService(Type typeToResolve)
        {
            var registeredService = registeredServices.FirstOrDefault(o => o.TypeToResolve == typeToResolve);
            if (registeredService == null)
            {
                throw new Exception($"The type {typeToResolve.Name} has not been registered");
            }
            return GetInstance(registeredService);
        }

        private static object GetInstance(RegisteredService registeredObject)
        {
            if (registeredObject.Instance == null || registeredObject.Injection == Injection.Transient)
            {
                var parameters = ResolveConstructorParameters(registeredObject);
                registeredObject.CreateInstance(parameters.ToArray());
            }
            return registeredObject.Instance;
        }

        private static IEnumerable<object> ResolveConstructorParameters(RegisteredService registeredObject)
        {
            var constructorInfo = registeredObject.ConcreteType.GetConstructors().First();
            foreach (var parameter in constructorInfo.GetParameters())
            {
                yield return ResolveService(parameter.ParameterType);
            }
        }

        //private static IDataProvider dataProvider;

        //public static IDataProvider DataProvider
        //{
        //    get
        //    {
        //        if (dataProvider == null)
        //        {
        //            dataProvider = new DbMock();
        //        }
        //        return dataProvider;
        //    }
        //}
    }
}
