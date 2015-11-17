using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;

namespace Common.Web.IoC
{
    /// <summary>
    /// Dependency Resolver for WebApi
    /// </summary>
    public class ChainedWebApiResolver : ChainedWebApiDependencyScope, IDependencyResolver
    {
        public ChainedWebApiResolver(IDependencyResolver newResolver, IDependencyResolver currentResolver)
            : base(newResolver, currentResolver)
        {
            _newResolver = newResolver;
            _fallbackResolver = currentResolver;
        }

        public IDependencyScope BeginScope()
        {
            return new ChainedWebApiDependencyScope(_newResolver, _fallbackResolver);
        }
    }
}