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
    public partial class ErrorDialog : Form
    {
        public ErrorDialog()
        {
            InitializeComponent();
            Load += new EventHandler(ErrorDialog_Load);
        }

        void ErrorDialog_Load(object sender, EventArgs e)
        {
            lblMessage.Text = Message;
            txtLongMessage.Text = Description;
        }
        public string Message { get; set; }
        public string Description { get; set; }
        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
