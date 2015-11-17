using CustomController.Filters;
using CustomController.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace CustomController.Controllers
{
    public class MyCustomWebAPIController : System.Web.Http.ApiController
    {
        private ILogger _logger;

        
        public MyCustomWebAPIController(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Default constructor which can also be used for testing
        /// </summary>
        //public MyCustomWebAPIController()
        //{
        //    _logger = (ILogger)System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(ILogger));
        //}

        /// <summary>
        /// hit at the custom route URL /api/Custom/MyCustomWebApi/MyAction
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        [LoggingFilter]
        public string MyAction()
        {
            _logger.Info("Go time!");

            return "Logger worked: Sitecore context database is " + (Sitecore.Context.Database != null ? Sitecore.Context.Database.Name : "null");
        }

        /// <summary>
        /// hit at the custom route URL /api/Custom/MyCustomWebApi/MyActionWithFilter
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        [LoggingFilter]
        public string MyActionWithFilter()
        {
            _logger.Info("Go time!");

            return "Logger worked: Sitecore context database is " + (Sitecore.Context.Database != null ? Sitecore.Context.Database.Name : "null");
        }
    }
}