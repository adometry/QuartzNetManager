using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
//using ClickForensics.Quartz.Jobs;

namespace ClickForensics.Quartz.Manager
{
    public partial class AddListenerForm : Form
    {
        public AddListenerForm()
        {
            InitializeComponent();
            this.Load += new EventHandler(AddListenerForm_Load);
        }

        void AddListenerForm_Load(object sender, EventArgs e)
        {
            //loadListenerTypes();
        }

        private List<Type> getListenerTypes()
        {
            //JobHistoryListener listener = new JobHistoryListener();
            List<Type> types = new List<Type>();
            Type theThing = ListenerInterface.BaseType;
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (ListenerInterface.UnderlyingSystemType.IsAssignableFrom(type))
                    {
                        types.Add(type);
                    }
                }
            }
            return types;
        }
        //private void loadListenerTypes()
        //{
        //    listenerTypeCombo.Items.Clear();
        //    foreach (var type in getListenerTypes())
        //    {
        //        listenerTypeCombo.Items.Add(type);
        //    }
        //}
        public Type ListenerInterface { get; set; }
        public string ListenerType { get; set; }
        public string ListenerName { get; set; }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ListenerName=txtListenerName.Text;
            ListenerType = txtListenerType.Text;
            this.Close();
        }
    }
}
