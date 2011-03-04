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
using Quartz.Impl;
using Quartz.Impl.Triggers;
using Quartz.Job;
using System.IO;
using Quartz.Util;

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
            var assemblies = AssemblyRepository.GetAssemblies();
			SortedList<string, string> jobTypes = new SortedList<string, string>();
            foreach (var assemblyName in assemblies)
			{
                Assembly assembly = Assembly.LoadFile(Environment.CurrentDirectory + "\\" + assemblyName);
				foreach (Type type in assembly.GetTypes())
				{
					if (typeof(IJob).IsAssignableFrom(type) && type.IsClass)
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
			setTriggerData((CronTriggerImpl)node.Trigger);
			setJobData(((JobNode)node.Parent.Parent).Detail);
		}

		private void setTriggerData(CronTriggerImpl trigger)
		{
			setTriggerType();
			txtCronExpression.Text = trigger.CronExpressionString;
			txtTriggerDescription.Text = trigger.Description;
			txtTriggerGroup.Text = trigger.Key.Group;
			txtTriggerName.Text = trigger.Key.Name;
		}

		private void setJobData(IJobDetail detail)
		{
			setJobType(detail);
			txtJobDescription.Text = detail.Description;
			txtJobGroup.Text = detail.Key.Group;
			txtJobName.Text = detail.Key.Name;
			setJobDataMap(detail);
		}

		private void setJobDataMap(IJobDetail detail)
		{
			jobDataListView.Items.Clear();
			foreach (var item in detail.JobDataMap.GetKeys())
			{
				jobDataListView.Items.Add(new ListViewItem(new string[] { item, detail.JobDataMap.Get(item).ToString() }));
			}
		}

		private void setJobType(IJobDetail detail)
		{
			cboJobType.SelectedItem = detail.JobType.FullName;

		}

		private void setTriggerType()
		{
			//nothing to do right now
		}
		public IJobDetail JobDetail { get; set; }
		public AbstractTrigger Trigger { get; set; }
		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			JobDetail = getJobDetail();
			ITrigger trigger = getTrigger(JobDetail);
			this.Close();
		}

		private IJobDetail getJobDetail()
		{
			IJobDetail detail = JobBuilder
				.NewJob()
				.OfType(getJobType())
				.WithDescription(txtJobDescription.Text)
				.WithIdentity(new JobKey(txtJobName.Text, txtJobGroup.Text))
				.UsingJobData(getJobDataMap())
				.Build();

			return detail;
		}

		private ITrigger getTrigger(IJobDetail jobDetail)
		{
			var builder =
				TriggerBuilder
					.Create()
					.ForJob(jobDetail)
					.WithDescription(txtTriggerDescription.Text)
					.WithIdentity(new TriggerKey(txtTriggerName.Text, txtTriggerGroup.Text));

			if (cboTriggerType.SelectedText == "Simple")
			{
				return builder.WithSchedule(SimpleScheduleBuilder.Create()).Build();
			}
			else
			{
				return builder.WithSchedule(CronScheduleBuilder.CronSchedule(txtCronExpression.Text)).Build();
			}
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
