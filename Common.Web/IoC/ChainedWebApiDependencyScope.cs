using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;

namespace Common.Web.IoC
{
    public class ChainedWebApiDependencyScope : IDependencyScope
    {
        protected IDependencyResolver _fallbackResolver;
        protected IDependencyResolver _newResolver;

        public ChainedWebApiDependencyScope(IDependencyResolver newResolver, IDependencyResolver fallbackResolver)
        {
            _newResolver = newResolver;
            _fallbackResolver = fallbackResolver;
        }

        public object GetService(Type serviceType)
        {
            object result = null;

            result = _newResolver.GetService(serviceType);
            if (result != null)
            {
                return result;
            }

            return _fallbackResolver.GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            IEnumerable<object> result = Enumerable.Empty<object>();
            result = _newResolver.GetServices(serviceType);
            if (result.Any())
            {
                return result;
            }

            return _fallbackResolver.GetServices(serviceType);
        }

        public void Dispose()
        {
            try
            {
                _newResolver.Dispose();
            }
            catch
            {
            }

            try
            {
                _fallbackResolver.Dispose();
            }
            catch
            {
            }
        }
    }
}