using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Quartz;
using System.Reflection;
using Quartz.Job;
using System.IO;

namespace ClickForensics.Quartz.Manager
{
    public partial class AddJobForm : Form
    {
        public AddJobForm()
        {
            InitializeComponent();
            loadJobAssemblies();
            cboTriggerType.Items.Add("Cron");
            cboTriggerType.SelectedItem = "Cron";
            if (cboJobType.SelectedText == "NativeJob")
            {
                jobDataListView.Items.Add(new ListViewItem(new string[] { "consumeStreams", "true" }));
                jobDataListView.Items.Add(new ListViewItem(new string[] { "waitForProcess", "true" }));
                txtKey.Text = "command";
            }
        }
        private void loadJobAssemblies()
        {
            FileStream stream = File.OpenRead("JobAssemblies.txt");
            StreamReader reader = new StreamReader(stream);
            string line;
            SortedList<string, string> jobTypes = new SortedList<string, string>();
            while ((line = reader.ReadLine()) != null)
            {
                Assembly assembly = Assembly.LoadFile(Environment.CurrentDirectory + "\\" + line);
                foreach (Type type in assembly.GetTypes())
                {
                    if (typeof(IJob).IsAssignableFrom(type))
                    {
                        jobTypes.Add(type.FullName, assembly.GetName().Name);
                    }
                }
            }
            foreach (var item in jobTypes)
            {
                cboJobType.Items.Add(new JobType() { AssemblyName = item.Value, FullName = item.Key });
            }
            //cboJobType.Items.AddRange(jobTypes.Values.ToArray<string>());

        }

        public AddJobForm(TriggerNode node)
            : this()
        {
            setTriggerData((CronTrigger)node.Trigger);
            setJobData(((JobNode)node.Parent.Parent).Detail);
        }

        private void setTriggerData(CronTrigger trigger)
        {
            setTriggerType();
            txtCronExpression.Text = trigger.CronExpressionString;
            txtTriggerDescription.Text = trigger.Description;
            txtTriggerGroup.Text = trigger.Group;
            txtTriggerName.Text = trigger.Name;
        }

        private void setJobData(JobDetail detail)
        {
            setJobType(detail);
            txtJobDescription.Text = detail.Description;
            txtJobGroup.Text = detail.Group;
            txtJobName.Text = detail.Name;
            setJobDataMap(detail);
        }

        private void setJobDataMap(JobDetail detail)
        {
            jobDataListView.Items.Clear();
            foreach (var item in detail.JobDataMap.GetKeys())
            {
                jobDataListView.Items.Add(new ListViewItem(new string[] { item, detail.JobDataMap.Get(item).ToString() }));
            }
        }

        private void setJobType(JobDetail detail)
        {
            cboJobType.SelectedItem = detail.JobType.FullName;

        }

        private void setTriggerType()
        {
            //nothing to do right now
        }
        public JobDetail JobDetail { get; set; }
        public Trigger Trigger { get; set; }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            JobDetail = getJobDetail();
            Trigger = getTrigger();
            Trigger.JobGroup = JobDetail.Group;
            Trigger.JobName = JobDetail.Name;
            this.Close();
        }

        private JobDetail getJobDetail()
        {
            JobDetail detail = new JobDetail();
            detail.Description = txtJobDescription.Text;
            detail.Group = txtJobGroup.Text;
            detail.JobDataMap = getJobDataMap();
            detail.JobType = getJobType();
            detail.Name = txtJobName.Text;
            return detail;
        }

        private Trigger getTrigger()
        {
            Trigger trigger;
            if (cboTriggerType.SelectedText == "Simple")
            {
                trigger = new SimpleTrigger();
            }
            else
            {
                trigger = new CronTrigger();
                ((CronTrigger)trigger).CronExpressionString = txtCronExpression.Text;
            }
            trigger.Description = txtTriggerDescription.Text;
            trigger.Group = txtTriggerGroup.Text;
            trigger.Name = txtTriggerName.Text;
            return trigger;
        }

        private Type getJobType()
        {
            JobType type = (JobType)cboJobType.SelectedItem;
            return Type.GetType(type.FullName + "," + type.AssemblyName, true);
        }

        private JobDataMap getJobDataMap()
        {
            JobDataMap map = new JobDataMap();
            foreach (ListViewItem item in jobDataListView.Items)
            {
                map.Add(item.SubItems[0].Text, item.SubItems[1].Text);
            }

            return map;
        }

        private void btnAddKeyValue_Click(object sender, EventArgs e)
        {
            ListViewItem item = new ListViewItem(new string[] { txtKey.Text, txtValue.Text });
            jobDataListView.Items.Add(item);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in jobDataListView.SelectedItems)
            {
                jobDataListView.Items.Remove(item);
            }
        }
    }
}
