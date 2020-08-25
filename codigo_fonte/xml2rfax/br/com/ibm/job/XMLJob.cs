using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using xml2rfax.br.com.ibm.service;

namespace xml2rfax.br.com.ibm.job
{
    /// <summary>
    /// Job para faxes discartados
    /// </summary>
    public class XMLJob : IJob
    {
        /// <summary>
        /// Inicializador dos jobs para XML discartados com base
        /// no ciclo de vida configurado 
        /// </summary>
        /// <param name="context"></param>
        public void Execute(IJobExecutionContext context)
        {
            new XMLService().moveXMLDiscarded();
        }
    }
}
