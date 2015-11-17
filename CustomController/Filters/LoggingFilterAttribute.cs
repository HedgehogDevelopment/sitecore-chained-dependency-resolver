using CustomController.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Net.Http;

namespace CustomController.Filters
{
    public class LoggingFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext context)
        {
            var logger = context.Request.GetDependencyScope().GetService(typeof(ILogger)) as ILogger;
            logger.Info("Executing action.");
        }
    }
}