using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xml2rfax.br.com.ibm.service;

namespace xml2rfax.br.com.ibm.job
{
    public class FaxJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            new FaxService().moveFaxes();
        }
    }
}
