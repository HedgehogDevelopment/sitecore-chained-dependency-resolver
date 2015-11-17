using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Net.Http;
using System.Web.Http.Dependencies;

namespace InversionOfControl.CastleWindsor.Core
{
    /// <summary>
    /// Custom controller activator for WebAPI with CastleWindsor Dependency Injection.
    /// Derived from: http://blog.ploeh.dk/2012/10/03/DependencyInjectioninASP.NETWebAPIwithCastleWindsor/
    /// </summary>
    public class WindsorHttpControllerActivator : IHttpControllerActivator
    {
        private IDependencyResolver _resolver;
        private IWindsorContainer _container;

        public WindsorHttpControllerActivator(IDependencyResolver resolver, IWindsorContainer container)
        {
            _resolver = resolver;
            _container = container;
        }

        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            var controller = (IHttpController)this._resolver.GetService(controllerType);
            request.RegisterForDispose(new Release(() => this._container.Release(controller)));

            return controller;
        }
            
        private class Release : IDisposable
        {
            private readonly Action release;

            public Release(Action release)
            {
                this.release = release;
            }

            public void Dispose()
            {
                this.release();
            }
        }
    }
}