using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomController.Interfaces
{
    public interface ILogger
    {
        void Info(string format);
        void Error(string format);
    }
}
