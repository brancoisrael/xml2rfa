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
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            JobFactory.getInstance().startJobs();
        }

        protected override void OnStop()
        {
            JobFactory.getInstance().stopJobs();
        }
    }
}
