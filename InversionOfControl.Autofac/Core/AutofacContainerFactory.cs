using System;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using CustomController.Implementations;
using CustomController.Interfaces;

namespace InversionOfControl.Autofac.Core
{
    public class AutofacContainerFactory
    {
        public IContainer Create()
        {
            var builder = new ContainerBuilder();

            // Register All Controllers In The Current Scope
            builder.RegisterControllers(AppDomain.CurrentDomain.GetAssemblies());

            builder.RegisterApiControllers(AppDomain.CurrentDomain.GetAssemblies());

            // Register Custom Assembly
            builder.RegisterType<Log4NetLogger>()
                .As<ILogger>();


            // Register Additional Things
            // ...

            // Build The Container and return it as a result
            return builder.Build();
        }
    }
}