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
			txtCronExpression.Text = _trigger.CronExpressionString;
			lblDescription.Text = _trigger.Description;
			lblGroup.Text = _trigger.Group;
			lblName.Text = _trigger.Name;
			if (_trigger.GetNextFireTimeUtc().HasValue)
			{
				lblNextFireTime.Text = _trigger.GetNextFireTimeUtc().Value.ToLocalTime().ToString();
			}
			else
			{
				lblNextFireTime.Text = "Unknown";
			}

			if (_trigger.GetPreviousFireTimeUtc().HasValue)
			{
				lblPreviousFireTime.Text = _trigger.GetPreviousFireTimeUtc().Value.ToLocalTime().ToString();
			}
			else
			{
				lblPreviousFireTime.Text = "Unknown";
			}
		}
		public CronTriggerDisplay(CronTriggerImpl trigger)
			: this()
		{
			_trigger = trigger;

		}

		private CronTriggerImpl _trigger;
	}
}
