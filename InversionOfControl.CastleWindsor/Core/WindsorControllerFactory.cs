using System;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web;
using Castle.MicroKernel;
using Castle.Windsor;

namespace InversionOfControl.CastleWindsor.Core
{
    /// <summary>
    /// Custom MVC Controller Factory, which will use Sitecore's Controller Factory as the inner factory.
    /// http://www.superstarcoders.com/blogs/posts/using-castle-windsor-with-sitecore-mvc-for-dependency-injection.aspx
    /// </summary>
    public class WindsorControllerFactory : DefaultControllerFactory
    {
        System.Web.Mvc.IDependencyResolver _resolver;

        public WindsorControllerFactory(System.Web.Mvc.IDependencyResolver resolver)
        {
            _resolver = resolver;
        }

        public override void ReleaseController(IController controller)
        {
            base.ReleaseController(controller);
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                throw new HttpException(404,
                   String.Format("The controller for path '{0}' could not be found.",
                      requestContext.HttpContext.Request.Path));
            }

            var controller = (IController)_resolver.GetService(controllerType);
            return controller;
        }
    }
}