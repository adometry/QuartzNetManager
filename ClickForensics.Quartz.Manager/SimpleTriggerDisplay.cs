using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Quartz;

namespace ClickForensics.Quartz.Manager
{
    public partial class SimpleTriggerDisplay : UserControl
    {
        public SimpleTriggerDisplay()
        {
            InitializeComponent();
            this.Load += new EventHandler(SimpleTriggerDisplay_Load);
        }

        void SimpleTriggerDisplay_Load(object sender, EventArgs e)
        {
            lblDescription.Text = _trigger.Description;
            lblGroup.Text = _trigger.Key.Group;
            lblName.Text = _trigger.Key.Name;
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
            lblRepeatCount.Text = _trigger.RepeatCount.ToString();
            lblRepeatInterval.Text = _trigger.RepeatInterval.ToString();
        }
        public SimpleTriggerDisplay(ISimpleTrigger trigger)
            : this()
        {
            _trigger = trigger;

        }

        private ISimpleTrigger _trigger;
    }
}
