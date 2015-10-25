using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace OS_Project1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            {
                new OS_with_MrStefanovic()
            };

            if (Environment.UserInteractive)
            {
                var service = ServicesToRun[0] as OS_with_MrStefanovic;

                if (service != null)
                    service.RunThreading();
            }
            else
            {
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
