using System.Web.Http;
using System.Web.Mvc;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Common.Web.IoC;
using InversionOfControl.Autofac.Core;
using Sitecore.Pipelines;

namespace InversionOfControl.Autofac.Pipelines
{
    public class SetupDependencyResolvers
    {
        public void Process(PipelineArgs args)
        {
            var autofacContainerFactory = new AutofacContainerFactory();

            var container = autofacContainerFactory.Create();

            if (container != null)
            {
                // MVC Resolver
                IDependencyResolver chainedMvcResolver = new ChainedMvcResolver(
                    new AutofacDependencyResolver(container),
                    DependencyResolver.Current);
                DependencyResolver.SetResolver(chainedMvcResolver);


                // WebAPI Resolver
                System.Web.Http.Dependencies.IDependencyResolver chainedWebApiResolver =
                    new ChainedWebApiResolver(new AutofacWebApiDependencyResolver(container),
                        GlobalConfiguration.Configuration.DependencyResolver);
                GlobalConfiguration.Configuration.DependencyResolver = chainedWebApiResolver;

                // MVC Controller Factory
                ControllerBuilder.Current.SetControllerFactory(new AutofacControllerFactory(container));
            }
        }
    }
}