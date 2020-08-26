using System.Threading.Tasks;

using Quartz;
using Quartz.Impl;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using xml2rfax.br.com.ibm.service;
using System.Collections.Specialized;
using xml2rfax.br.com.ibm.job;
using Quartz.Impl.Triggers;

namespace xml2rfax
{
    public partial class XML2RFAX : ServiceBase
    {
        /// <summary>
        /// Construtor padrao
        /// </summary>
        public XML2RFAX()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Inicia container de jobs
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
                JobFactory.getInstance().startJobs();
        }

        /// <summary>
        /// Para container de jobs
        /// </summary>
        protected override void OnStop()
        {
            JobFactory.getInstance().stopJobs();
        }
    }
}
