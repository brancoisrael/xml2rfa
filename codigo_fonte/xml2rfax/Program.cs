﻿using RFCOMAPILib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using xml2rfax.br.com.ibm.service;
using xml2rfax.br.com.ibm.util;

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
            

            /*
            FaxService faxService = new FaxService();
            faxService.moveFaxes();
            */


            /*XMLService xMLService = new XMLService();
            xMLService.moveXMLDiscarded();*/

        }
    }
}
