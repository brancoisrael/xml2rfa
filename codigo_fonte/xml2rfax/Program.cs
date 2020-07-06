using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using xml2rfax.br.com.ibm.job;
using xml2rfax.br.com.ibm.service;

namespace xml2rfax
{
    static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        static void Main()
        {

            
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Service1()
            };
            ServiceBase.Run(ServicesToRun);
            
            //JobFactory.getInstance().startJobs();
           
        }
    }
}
