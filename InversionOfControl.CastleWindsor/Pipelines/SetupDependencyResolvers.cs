using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Releasers;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Common.Web.IoC;
using InversionOfControl.CastleWindsor.Core;
using Sitecore.Pipelines;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using IMVCDependencyResolver = System.Web.Mvc.IDependencyResolver;
using IWebAPIDependencyResolver = System.Web.Http.Dependencies.IDependencyResolver;
using SCSitecoreControllerFactory = global::Sitecore.Mvc.Controllers.SitecoreControllerFactory;

namespace InversionOfControl.CastleWindsor.Pipelines
{
    public class SetupDependencyResolvers
    {
        public void Process(PipelineArgs args)
        {
            IWindsorContainer container = this.BuildContainerAndRegisterTypes();

            // create the chained resolvers using our own resolvers then falling back to whatever was previously set.
            // MVC
            IMVCDependencyResolver chainedMVCResolver = new ChainedMvcResolver(new WindsorMvcDependencyResolver(container),
                                                                               System.Web.Mvc.DependencyResolver.Current);
            System.Web.Mvc.DependencyResolver.SetResolver(chainedMVCResolver);

            //WebAPI
            IWebAPIDependencyResolver chainedWebAPIResolver = new ChainedWebApiResolver(new WindsorWebApiDependencyResolver(container),
                                                                                GlobalConfiguration.Configuration.DependencyResolver);
            System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = chainedWebAPIResolver;

            // Optional use of Controller Factories (for both Mvc and WebApi)
            //this.SetupMvcControllerFactory(DependencyResolver.Current);
            //this.SetupWebApiControllerActivator(GlobalConfiguration.Configuration.DependencyResolver, container);
        }

        private IWindsorContainer BuildContainerAndRegisterTypes()
        {
            var container = new WindsorContainer();
            container.Kernel.ReleasePolicy = new NoTrackingReleasePolicy();

            // register types from the config file
            IWindsorInstaller[] installers = new[]
                            {
                                Castle.Windsor.Installer.Configuration.FromXmlFile("app_config\\windsor\\components.config")
                            };

            container.Install(installers);

            // Register MVC controllers
            container.Register(Classes.FromAssemblyNamed("CustomController")
                                      .BasedOn<System.Web.Mvc.IController>()
                                      .LifestyleTransient());

            // Register WebApi controllers
            // http://forums.asp.net/t/1727621.aspx?Castle+Windsor+IOC+No+component+for+supporting+the+service+Web+Controllers+HomeController+was+found
            container.Register(Classes.FromAssemblyNamed("CustomController")
                                      .BasedOn<System.Web.Http.Controllers.IHttpController>()
                                      .LifestylePerWebRequest()
                                      .Configure(x => x.Named(x.Implementation.FullName)));

            return container;
        }

        /// <summary>
        /// Initialize's the MVC Controller Factory, using Sitecore's Controller Factory as the inner factory.
        /// http://www.superstarcoders.com/blogs/posts/using-castle-windsor-with-sitecore-mvc-for-dependency-injection.aspx
        /// </summary>
        public void SetupMvcControllerFactory(IDependencyResolver resolver)
        {
            IControllerFactory controllerFactory = new WindsorControllerFactory(resolver);
            SCSitecoreControllerFactory scSitecoreControllerFactory = new SCSitecoreControllerFactory(controllerFactory);

            ControllerBuilder.Current.SetControllerFactory(scSitecoreControllerFactory);
        }

        public void SetupWebApiControllerActivator(System.Web.Http.Dependencies.IDependencyResolver resolver, IWindsorContainer container)
        {
            var activator = new WindsorHttpControllerActivator(resolver, container);
            GlobalConfiguration.Configuration.Services.Replace(typeof(System.Web.Http.Dispatcher.IHttpControllerActivator), activator);
        }
    }
}