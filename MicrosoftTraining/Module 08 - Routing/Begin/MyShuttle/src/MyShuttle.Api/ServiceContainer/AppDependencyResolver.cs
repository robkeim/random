using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;

namespace MyShuttle.Api.ServiceContainer
{
    /// <summary>
    /// MyShuttle Dependency Resolver
    /// </summary>
    public class AppDependencyResolver : IDependencyResolver
    {
        private readonly AppServiceContainer _serviceContainer;

        public AppDependencyResolver(AppServiceContainer serviceContainer)
        {
            _serviceContainer = serviceContainer;
        }

        public IDependencyScope BeginScope()
        {
            return new AppDependencyResolver(_serviceContainer);
        }

        public void Dispose()
        {
            // Method intentionally left empty.
        }

        public object GetService(Type serviceType)
        {
            return _serviceContainer.GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            yield return _serviceContainer.GetService(serviceType);
        }
    }
}