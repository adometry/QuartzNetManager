using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Quartz;
using Quartz.Impl;

namespace ClickForensics.Quartz.Manager
{
	public class JobNode : TreeNode
	{
		public JobNode(IJobDetail jobDetail)
			: base()
		{
			this.Text = jobDetail.Key.Name;
			Detail = jobDetail;
		}
		public IJobDetail Detail { get; private set; }

	}
}
