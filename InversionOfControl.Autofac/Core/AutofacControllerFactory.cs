using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;

namespace InversionOfControl.Autofac.Core
{
    public class AutofacControllerFactory : DefaultControllerFactory
    {
        private readonly IContainer _container;

        public AutofacControllerFactory(IContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            _container = container;
        }

        protected override IController GetControllerInstance(RequestContext context, Type controllerType)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (controllerType == null)
            {
                throw new HttpException(404,
                    string.Format("No Controller Type Found For Path {0}", context.HttpContext.Request.Path));
            }

            object controller;

            if (_container.TryResolve(controllerType, out controller))
            {
                return (IController) controller;
            }

            throw new HttpException(404,
                string.Format("No Controller Type: {0} Found For Path {1}", controllerType.FullName,
                    context.HttpContext.Request.Path));
        }

        public override void ReleaseController(IController controller)
        {
        }
    }
}