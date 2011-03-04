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
    public partial class DeleteAssembliesForm : Form
    {
        public DeleteAssembliesForm()
        {
            InitializeComponent();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lbxAssemblies.SelectedItem != null)
            {
                AssemblyRepository.DeleteAssembly(lbxAssemblies.SelectedItem as string);
                lbxAssemblies.DataSource = AssemblyRepository.GetAssemblies().ToList();
            }
        }

        private void DeleteAssembliesForm_Load(object sender, EventArgs e)
        {
            lbxAssemblies.DataSource = AssemblyRepository.GetAssemblies().ToList();
        }
    }
}
