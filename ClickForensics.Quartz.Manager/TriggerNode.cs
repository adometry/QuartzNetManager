using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Quartz;

namespace ClickForensics.Quartz.Manager
{
    public class TriggerNode : TreeNode
    {
        public TriggerNode(Trigger trigger)
        {
            Text = trigger.Name;
            
            Trigger = trigger;
        }
        public Trigger Trigger { get; set; }
    }
}
