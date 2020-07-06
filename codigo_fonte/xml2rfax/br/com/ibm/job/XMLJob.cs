using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using xml2rfax.br.com.ibm.service;

namespace xml2rfax.br.com.ibm.job
{
    public class XMLJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            new XMLService().moveXMLDiscarded();
        }
    }
}
