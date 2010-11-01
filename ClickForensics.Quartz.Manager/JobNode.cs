using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Quartz;

namespace ClickForensics.Quartz.Manager
{
    public class JobNode : TreeNode
    {
        public JobNode(JobDetail jobDetail)
            : base()
        {
            this.Text = jobDetail.Name;
            Detail = jobDetail;
        }
        public JobDetail Detail { get; private set; }

    }
}
