using Sitecore.Pipelines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace CustomController.Pipelines
{
    public class SetupRoutes
    {
        public void Process(PipelineArgs args)
        {
            HttpConfiguration config = GlobalConfiguration.Configuration;

            config.Routes.MapHttpRoute("CustomWebApiRoute", "api/Custom/{controller}/{action}");
        }
    }
}