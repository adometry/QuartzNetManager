using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClickForensics.Quartz.Manager
{
    public partial class ServerConnectForm : Form
    {
        public ServerConnectForm()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            Server = txtServer.Text;
            Port = int.Parse(txtPort.Text);
            Scheduler = txtScheduler.Text;
            this.Close();
        }
        public string Server { get; set; }
        public int Port { get; set; }
        public string Scheduler { get; set; }

  
    }
}
