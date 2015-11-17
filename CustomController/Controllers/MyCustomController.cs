using CustomController.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace CustomController.Controllers
{
    public class MyCustomController : System.Web.Mvc.Controller
    {
        private ILogger _logger;

        public MyCustomController(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Default constructor which can also be used for testing
        /// </summary>
        //public MyCustomController()
        //{
        //    _logger = (ILogger)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(ILogger));
        //}

        /// <summary>
        /// hit using Sitecore routing at the URL /api/sitecore/mycustom/myaction
        /// OR
        /// hit using the Sitecore rendering at the URL / (the Home item holds the Custom Controller rendering).
        /// </summary>
        /// <returns></returns>
        public string MyAction()
        {
            _logger.Info("Booyah!!!");

            return "Logger worked: Sitecore context database is " + (Sitecore.Context.Database != null ? Sitecore.Context.Database.Name : "null");
        }
    }
}