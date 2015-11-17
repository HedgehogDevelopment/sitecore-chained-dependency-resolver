using CustomController.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomController.Implementations
{
    public class Log4NetLogger : ILogger
    {
        void ILogger.Info(string format)
        {
            Sitecore.Diagnostics.Log.Info(format, this);
        }

        void ILogger.Error(string format)
        {
            Sitecore.Diagnostics.Log.Error(format, this);
        }
    }
}