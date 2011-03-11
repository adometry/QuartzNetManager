using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using Quartz.Impl;
using Quartz;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;
using log4net;
using System.Reflection;

namespace ClickForensics.Quartz.Manager
{
    public class QuartzScheduler
    {
        public QuartzScheduler(string server, int port, string scheduler)
        {
            Address = string.Format("tcp://{0}:{1}/{2}", server, port, scheduler);
            _schedulerFactory = new StdSchedulerFactory(getProperties(Address));

            try
            {
                _scheduler = _schedulerFactory.GetScheduler();
            }
            catch (SchedulerException ex)
            {
                _Log.Error(string.Format("Unable to connect to scheduler {0}", Address), ex);
                MessageBox.Show("Unable to connect to the specified server", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        public string Address { get; private set; }
        private NameValueCollection getProperties(string address)
        {
            NameValueCollection properties = new NameValueCollection();
            properties["quartz.scheduler.instanceName"] = "RemoteClient"+DateTime.Now.ToString("yyyyMMddHHmmss");
            properties["quartz.scheduler.proxy"] = "true";
            properties["quartz.threadPool.threadCount"] = "0";
            properties["quartz.scheduler.proxy.address"] = address;
            return properties;
        }
        public IScheduler GetScheduler()
        {
            return _scheduler;
        }
        public DataTable GetJobs()
        {
            DataTable table = new DataTable();
            table.Columns.Add("GroupName");
            table.Columns.Add("JobName");
            table.Columns.Add("JobDescription");
            table.Columns.Add("TriggerName");
            table.Columns.Add("TriggerGroupName");
            table.Columns.Add("TriggerType");
            table.Columns.Add("TriggerState");
            table.Columns.Add("NextFireTime");
            table.Columns.Add("PreviousFireTime");
            string[] jobGroups = GetScheduler().JobGroupNames;
            foreach (string group in jobGroups)
            {
                string[] jobNames = GetScheduler().GetJobNames(group);
                foreach (string job in jobNames)
                {
                    JobDetail detail = GetScheduler().GetJobDetail(job, group);
                    Trigger[] triggers = GetScheduler().GetTriggersOfJob(job, group);
                    foreach (Trigger trigger in triggers)
                    {
                        DataRow row = table.NewRow();
                        row["GroupName"] = group;
                        row["JobName"] = job;
                        row["JobDescription"] = detail.Description;
                        row["TriggerName"] = trigger.Name;
                        row["TriggerGroupName"] = trigger.Group;
                        row["TriggerType"] = trigger.GetType().Name;
                        row["TriggerState"] = GetScheduler().GetTriggerState(trigger.Name, trigger.Group);
                        DateTime? nextFireTime = trigger.GetNextFireTimeUtc();
                        if (nextFireTime != null)
                        {
                            row["NextFireTime"] = TimeZone.CurrentTimeZone.ToLocalTime((DateTime)nextFireTime);
                        }

                        DateTime? previousFireTime = trigger.GetPreviousFireTimeUtc();
                        if (previousFireTime != null)
                        {
                            row["PreviousFireTime"] = TimeZone.CurrentTimeZone.ToLocalTime((DateTime)previousFireTime);
                        }

                        table.Rows.Add(row);
                    }
                }
            }
            return table;
        }

        public void ScheduleOneTimeJob(Type jobType, JobDataMap dataMap, int clientID)
        {
            string name = string.Format("{0}-{1}", jobType.Name, clientID);
            string group = clientID.ToString();
            JobDetail jobDetail = new JobDetail(name, group, jobType);
            jobDetail.Description = "One time job";
            jobDetail.Durable = false;
            jobDetail.Group = group;
            jobDetail.JobDataMap = dataMap;
            jobDetail.JobType = jobType;
            jobDetail.Name = name;
            jobDetail.Volatile = true;
            SimpleTrigger trigger = new SimpleTrigger();
            trigger.Name = name;
            trigger.Group = group;
            trigger.StartTimeUtc = DateTime.UtcNow;
            trigger.RepeatCount = 0;
            trigger.RepeatInterval = TimeSpan.Zero;
            GetScheduler().ScheduleJob(jobDetail, trigger);
        }

        private ISchedulerFactory _schedulerFactory;

        private IScheduler _scheduler;

        public DataTable GetRunningJobs()
        {
            DataTable table = new DataTable();
            table.Columns.Add("JobName", typeof(string));
            table.Columns.Add("RunTime", typeof(int));
            try
            {
                IList jobs = GetScheduler().GetCurrentlyExecutingJobs();
                foreach (JobExecutionContext context in jobs)
                {
                    DataRow row = table.NewRow();
                    row["JobName"] = context.JobDetail.Name;
                    row["RunTime"] = (DateTime.Now.ToUniversalTime() - (DateTime)context.FireTimeUtc).TotalMinutes;
                    table.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                //TODO: Let the user know we couldn't load the running jobs.
                _Log.Error("Error loading running jobs.", ex);
            }

            return table;
        }

        public void BackupToFile(System.IO.FileInfo file)
        {
            IScheduler scheduler = GetScheduler();
            string[] jobGroupNames = scheduler.JobGroupNames;
            List<JobDetail> jobDetails = new List<JobDetail>();
            foreach (var jobGroup in jobGroupNames)
            {
                string[] jobNames = scheduler.GetJobNames(jobGroup);
                foreach (var jobName in jobNames)
                {
                    jobDetails.Add(scheduler.GetJobDetail(jobName, jobGroup));
                }
            }
            writeToFile(file, jobDetails);

        }

        private void writeToFile(System.IO.FileInfo file, List<JobDetail> jobDetails)
        {
            using (StreamWriter writer = file.CreateText())
            {
                XNamespace ns = "http://quartznet.sourceforge.net/JobSchedulingData";
                XDocument doc = new XDocument(new XDeclaration("1.0", "UTF-8", "yes")
                    , new XElement(ns + "quartz"
                        , new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance")
                        , new XAttribute("version", "1.0")
                        , new XAttribute("overwrite-existing-jobs", "true")
                        )
                    );
                foreach (JobDetail detail in jobDetails)
                {
                    doc.Root.Add(
                            new XElement(ns + "job"
                            , new XElement(ns + "job-detail"
                            , new XElement(ns + "name", detail.Name)
                            , new XElement(ns + "group", detail.Group)
                            , new XElement(ns + "description", detail.Description)
                            , new XElement(ns + "job-type", detail.JobType.FullName + "," + detail.JobType.Assembly.FullName)
                            , new XElement(ns + "volatile", detail.Volatile)
                            , new XElement(ns + "durable", detail.Durable)
                            , new XElement(ns + "recover", detail.RequestsRecovery)
                            , getJobDataMap(ns, detail.JobDataMap)
                            )
                            , getTriggers(ns, detail)
                            )
                        );
                }
                writer.Write(doc);
                writer.Flush();
                writer.Close();
            }
        }

        private XElement getJobDataMap(XNamespace ns, JobDataMap jobDataMap)
        {
            XElement map = new XElement(ns + "job-data-map");
            foreach (var key in jobDataMap.GetKeys())
            {
                map.Add(new XElement(ns + "entry"
                    , new XElement(ns + "key", key)
                    , new XElement(ns + "value", jobDataMap[key])
                    )
                );
            }

            return map;
        }

        private XElement[] getTriggers(XNamespace ns, JobDetail detail)
        {
            Trigger[] triggers = _scheduler.GetTriggersOfJob(detail.Name, detail.Group);
            XElement[] elements = new XElement[triggers.Length];
            for (int i = 0; i < triggers.Length; i++)
            {
                elements[i] = new XElement(ns + "trigger");
                if (triggers[i] is SimpleTrigger)
                {
                    elements[i].Add(getSimpleTrigger(ns, (SimpleTrigger)triggers[i]));
                }
                else if (triggers[i] is CronTrigger)
                {
                    elements[i].Add(getCronTrigger(ns, (CronTrigger)triggers[i]));
                }
            }
            return elements;
        }

        private XElement getCronTrigger(XNamespace ns, CronTrigger trigger)
        {
            XElement cronTrigger = new XElement(ns + "cron");
            addCommonTriggerData(ns, cronTrigger, trigger);
            cronTrigger.Add(
                new XElement(ns + "cron-expression", trigger.CronExpressionString)
                );
            return cronTrigger;
        }

        private void addCommonTriggerData(XNamespace ns, XElement rootTriggerElement, Trigger trigger)
        {
            rootTriggerElement.Add(
                new XElement(ns + "name", trigger.Name)
                , new XElement(ns + "group", trigger.Group)
                , new XElement(ns + "description", trigger.Description)
                , new XElement(ns + "misfire-instruction", getMisfireInstructionText(trigger))
                , new XElement(ns + "volatile", trigger.Volatile)
                , new XElement(ns + "job-name", trigger.JobName)
                , new XElement(ns + "job-group", trigger.JobGroup)
                );
        }

        private string getMisfireInstructionText(Trigger trigger)
        {
            if (trigger is CronTrigger)
            {
                return getCronTriggerMisfireInstructionText(trigger.MisfireInstruction);
            }
            return getSimpleTriggerMisfireInstructionText(trigger.MisfireInstruction);
        }

        private string getSimpleTriggerMisfireInstructionText(int misfireInstruction)
        {
            switch (misfireInstruction)
            {
                case 0:
                    return "SmartPolicy";
                case 1:
                    return "FireNow";
                case 2:
                    return "RescheduleNowWithExistingRepeatCount";
                case 3:
                    return "RescheduleNowWithRemainingRepeatCount";
                case 4:
                    return "RescheduleNextWithRemainingCount";
                case 5:
                    return "RescheduleNextWithExistingCount";
                default:
                    throw new ArgumentOutOfRangeException(string.Format("{0} is not a supported misfire instruction for SimpleTrigger See Quartz.MisfireInstruction for more details.", misfireInstruction));
            }
        }

        private string getCronTriggerMisfireInstructionText(int misfireInstruction)
        {
            switch (misfireInstruction)
            {
                case 0:
                    return "SmartPolicy";
                case 1:
                    return "FireOnceNow";
                case 2:
                    return "DoNothing";
                default:
                    throw new ArgumentOutOfRangeException(string.Format("{0} is not a supported misfire instruction for CronTrigger See Quartz.MisfireInstruction for more details.", misfireInstruction));
            }
        }

        private XElement getSimpleTrigger(XNamespace ns, SimpleTrigger trigger)
        {
            XElement simpleTrigger = new XElement(ns + "simple");
            addCommonTriggerData(ns, simpleTrigger, trigger);
            simpleTrigger.Add(
                new XElement(ns + "repeat-count", trigger.RepeatCount)
                , new XElement(ns + "repeat-interval", trigger.RepeatInterval.Milliseconds)
                );
            return simpleTrigger;
        }
        private static readonly ILog _Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
    }
}
