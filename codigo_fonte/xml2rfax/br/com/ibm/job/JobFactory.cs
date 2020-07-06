using Quartz;
using Quartz.Impl;
using Quartz.Impl.Triggers;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using xml2rfax.br.com.ibm.util;

namespace xml2rfax.br.com.ibm.job
{
    public class JobFactory
    {
        private static JobFactory jobFactory;
        private IScheduler scheduler;


        private JobFactory()
        { }

        public static JobFactory getInstance()
        {
            if (jobFactory == null)
                jobFactory = new JobFactory();

            return jobFactory;
        }


        public void startJobs()
        {
            var properties = new NameValueCollection();

            properties["quartz.threadPool.threadCount"] = "1";

            scheduler = new StdSchedulerFactory(properties).GetScheduler();

            var faxJob = new JobDetailImpl("FaxJob", "FaxGroupJob", typeof(FaxJob));
            var trigger1 = new CronTriggerImpl("FaxTrigger", "GrupoFaxTrigger", "FaxJob", "FaxGroupJob", PropertiesUtil.getInstance().getProperties("cron_move_fax"));

            var xmlJob = new JobDetailImpl("XMLJob", "XMLGroupJob", typeof(XMLJob));
            var trigger2 = new CronTriggerImpl("XMLTrigger", "GrupoXMLTrigger", "XMLJob", "XMLGroupJob", PropertiesUtil.getInstance().getProperties("cron_xml_move_age"));

            scheduler.ScheduleJob(faxJob, trigger1);
            scheduler.ScheduleJob(xmlJob, trigger2);

            scheduler.Start();           
        }

        public void stopJobs()
        {
            if(scheduler!=null)
                scheduler.Shutdown();
        }

    }
}
