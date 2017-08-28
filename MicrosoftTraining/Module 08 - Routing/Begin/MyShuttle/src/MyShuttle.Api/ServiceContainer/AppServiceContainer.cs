using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MyShuttle.Api.ServiceContainer
{
    /// <summary>
    /// MyShttle IoC container.
    /// </summary>
    /// <remarks>Do not use this container in a production environment, it's meant for demonstration purpose only</remarks>
    public class AppServiceContainer
    {
        // The dictionary to hold the services and its implementations
        private readonly Dictionary<Type, Type> _container;

        public AppServiceContainer()
        {
            _container = new Dictionary<Type, Type>();
        }

        public void AddService<TService, TImplementation>() where TImplementation : TService
        {
            _container.Add(typeof(TService), typeof(TImplementation));
        }

        public object GetService(Type serviceType)
        {
            // This get service is not very well optimized, it's just for demonstrating the concept of dependency injection and the IoC containers
            try
            {
                Type service;
                if (!_container.TryGetValue(serviceType, out service))
                {
                    // If the service doesn't exists in the dictionary then just pick up that serviceType to be the service
                    service = serviceType;
                }

                var serviceConstructors = service.GetConstructors();
                if (serviceConstructors.Length > 0)
                {
                    var resolvedParameters = ResolveConstructorParameters(serviceConstructors[0]).ToArray();
                    return Activator.CreateInstance(service, resolvedParameters);
                }

                return Activator.CreateInstance(service);
            }
            catch
            {
                return null;
            }
        }

        private IEnumerable<object> ResolveConstructorParameters(ConstructorInfo constructorInfo)
        {
            // ⚠ Might have a problem with circular parameters!
            foreach (var parameter in constructorInfo.GetParameters())
            {
                yield return GetService(parameter.ParameterType);
            }
        }
    }
}