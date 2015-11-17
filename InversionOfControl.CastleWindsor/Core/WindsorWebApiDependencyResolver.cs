using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;
using Castle.MicroKernel.Lifestyle;
using System.Web.Http.Controllers;
using Castle.MicroKernel.Registration;

namespace InversionOfControl.CastleWindsor.Core
{
    public class WindsorWebApiDependencyResolver : IDependencyResolver
    {
        private readonly IWindsorContainer _container;
        private static readonly Type WebApiControllerType = typeof(IHttpController);

        public WindsorWebApiDependencyResolver(IWindsorContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            _container = container;
        }
        public object GetService(Type serviceType)
        {
            if (_container.Kernel.HasComponent(serviceType))
            {
                return _container.Resolve(serviceType);
            }

            return null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (_container.Kernel.HasComponent(serviceType))
            {
                return _container.ResolveAll(serviceType).Cast<object>().ToArray();
            }

            return new object[] { };
        }

        public IDependencyScope BeginScope()
        {
            return new WindsorWebApiDependencyScope(_container);
        }

        public void Dispose()
        {

        }
    }

    
}