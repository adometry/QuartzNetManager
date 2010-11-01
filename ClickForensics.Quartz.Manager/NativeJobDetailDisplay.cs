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
    public partial class NativeJobDetailDisplay : UserControl
    {
        public NativeJobDetailDisplay()
        {
            InitializeComponent();
            this.Load += new EventHandler(NativeJobDetailDisplay_Load);
        }

        void NativeJobDetailDisplay_Load(object sender, EventArgs e)
        {
            lblDescription.Text = _detail.Description;
            lblGroup.Text = _detail.Group;
            lblName.Text = _detail.Name;
            loadJobDataMap();
        }

        private void loadJobDataMap()
        {
            foreach (var item in _detail.JobDataMap.GetKeys())
            {
                jobDataListView.Items.Add(new ListViewItem(new string[] { item, _detail.JobDataMap.Get(item).ToString() }));
            }
        }
        public NativeJobDetailDisplay(JobDetail detail)
            : this()
        {
            _detail = detail;
        }
        private JobDetail _detail;

    }
}
