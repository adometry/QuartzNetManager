using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Quartz;
using Quartz.Impl.Triggers;

namespace ClickForensics.Quartz.Manager
{
	public partial class CronTriggerDisplay : UserControl
	{
		public CronTriggerDisplay()
		{
			InitializeComponent();
			this.Load += new EventHandler(CronTriggerDisplay_Load);
		}

		void CronTriggerDisplay_Load(object sender, EventArgs e)
		{
			txtCronExpression.Text = _Trigger.CronExpressionString;
			lblDescription.Text = _Trigger.Description;
			lblGroup.Text = _Trigger.Key.Group;
			lblName.Text = _Trigger.Key.Name;
			if (_Trigger.GetNextFireTimeUtc().HasValue)
			{
				lblNextFireTime.Text = _Trigger.GetNextFireTimeUtc().Value.ToLocalTime().ToString();
			}
			else
			{
				lblNextFireTime.Text = "Unknown";
			}

			if (_Trigger.GetPreviousFireTimeUtc().HasValue)
			{
				lblPreviousFireTime.Text = _Trigger.GetPreviousFireTimeUtc().Value.ToLocalTime().ToString();
			}
			else
			{
				lblPreviousFireTime.Text = "Unknown";
			}
		}
		public CronTriggerDisplay(ICronTrigger trigger)
			: this()
		{
			_Trigger = trigger;

		}

		private ICronTrigger _Trigger;
	}
}
