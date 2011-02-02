using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Quartz;
using Quartz.Impl;

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
			lblGroup.Text = _detail.Key.Group;
			lblName.Text = _detail.Key.Name;
            loadJobDataMap();
        }

        private void loadJobDataMap()
        {
            foreach (var item in _detail.JobDataMap.GetKeys())
            {
                jobDataListView.Items.Add(new ListViewItem(new string[] { item, _detail.JobDataMap.Get(item).ToString() }));
            }
        }
        public NativeJobDetailDisplay(IJobDetail detail)
            : this()
        {
            _detail = detail;
        }
		private IJobDetail _detail;

    }
}
