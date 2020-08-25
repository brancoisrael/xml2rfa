using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xml2rfax.br.com.ibm.service;

namespace xml2rfax.br.com.ibm.job
{
    /// <summary>
    /// Job para faxes recebidos
    /// </summary>
    public class FaxJob : IJob
    {
        /// <summary>
        /// Inicializador dos jobs para analisar faxes recebidos
        /// </summary>
        /// <param name="context"></param>
        public void Execute(IJobExecutionContext context)
        {
            new FaxService().moveFaxes();
        }
    }
}
