using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Quartz;
using Quartz.Impl.Triggers;

namespace ClickForensics.Quartz.Manager
{
	public class TriggerNode : TreeNode
	{
		public TriggerNode(AbstractTrigger trigger)
		{
			Text = trigger.Name;

			Trigger = trigger;
		}
		public AbstractTrigger Trigger { get; set; }
	}
}
