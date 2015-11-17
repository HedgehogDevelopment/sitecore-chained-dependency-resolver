using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Lifestyle;

namespace InversionOfControl.CastleWindsor.Core
{
    public class WindsorWebApiDependencyScope : IDependencyScope
    {
        private readonly IWindsorContainer _container;
        private readonly IDisposable _scope;

        public WindsorWebApiDependencyScope(IWindsorContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            _container = container;
            _scope = _container.BeginScope();
        }

        /// <summary>
        /// Resolves single registered services that support arbitrary object creation.
        /// </summary>
        /// <param name="serviceType">The type of the requested service or object.</param>
        /// <returns>
        /// The requested service or object.
        /// </returns>
        public object GetService(Type serviceType)
        {
            if (_container.Kernel.HasComponent(serviceType))
            {
                return _container.Resolve(serviceType);
            }

            return null;
        }

        /// <summary>
        /// Resolves multiple registered services.
        /// </summary>
        /// <param name="serviceType">The type of the requested services.</param>
        /// <returns>
        /// The requested services.
        /// </returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (_container.Kernel.HasComponent(serviceType))
            {
                return _container.ResolveAll(serviceType).Cast<object>();
            }

            return new object[] { };
        }

        public void Dispose()
        {
            _scope.Dispose();
        }
    }
}