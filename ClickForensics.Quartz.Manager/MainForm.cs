using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Quartz;
using Quartz.Collection;
using System.Net.Sockets;
//using ClickForensics.Quartz.Jobs;
using System.IO;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using Quartz.Impl.Triggers;

namespace ClickForensics.Quartz.Manager
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            jobGroupsTreeView.AfterSelect += new TreeViewEventHandler(jobGroupsTreeView_AfterSelect);
            ctxScheduler.Opening += new CancelEventHandler(ctxScheduler_Opening);
            jobGroupsTreeView.MouseDown += new MouseEventHandler(jobGroupsTreeView_MouseDown);

        }

        void ctxScheduler_Opening(object sender, CancelEventArgs e)
        {

        }

        void jobGroupsTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            jobDetailsToggle(false);
            if (e.Node is TriggerNode || e.Node is JobNode)
            {
                btnDeleteJob.Enabled = true;
            }
            else
            {
                btnDeleteJob.Enabled = false;
            }

            if (e.Node is JobNode)
            {
                btnRunJobNow.Enabled = true;
                pnlDetails.Controls.Add(new NativeJobDetailDisplay(((JobNode)e.Node).Detail));
                jobDetailsToggle(true);
            }
            else
            {
                btnRunJobNow.Enabled = false;

            }
            if (e.Node is TriggerNode)
            {
                btnPause.Enabled = true;
                setPauseButtonText();
                if (((TriggerNode)e.Node).Trigger is ICronTrigger)
                {
                    pnlDetails.Controls.Add(new CronTriggerDisplay((ICronTrigger)((TriggerNode)e.Node).Trigger));
                    jobDetailsToggle(true);
                }
                if (((TriggerNode)e.Node).Trigger is ISimpleTrigger)
                {
                    pnlDetails.Controls.Add(new SimpleTriggerDisplay((ISimpleTrigger)((TriggerNode)e.Node).Trigger));
                    jobDetailsToggle(true);
                }
                btnEdit.Enabled = true;
            }
            else
            {
                btnEdit.Enabled = false;
                btnPause.Enabled = false;
            }
        }

        private void setPauseButtonText()
        {
            TriggerNode node = (TriggerNode)jobGroupsTreeView.SelectedNode;
            if (Scheduler.GetScheduler().GetTriggerState(node.Trigger.Key) == TriggerState.Paused)
            {
                btnPause.Text = "Resume";
            }
            else
            {
                btnPause.Text = "Pause";
            }
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ServerConnectForm form = new ServerConnectForm())
            {
                form.ShowDialog();
                if (!form.Cancelled)
                {
                    try
                    {
                        Scheduler = new QuartzScheduler(form.Server, form.Port, form.Scheduler);
                        serverConnectStatusLabel.Text = string.Format("Connected to {0}", Scheduler.Address);
                        connectToolStripMenuItem.Enabled = false;
                        jobsToolStripMenuItem.Enabled = true;
                        loadJobGroups();
                        updateRunningJobs();
                    }
                    catch (SocketException ex)
                    {
                        ErrorDialog dialog = new ErrorDialog();
                        dialog.Message = string.Format("Unable to connect to scheduler {0} on {1}:{2}", form.Scheduler, form.Server, form.Port);
                        dialog.Description = ex.Message;
                        dialog.ShowDialog();
                    }
                }
                form.Close();
            }
            //loadGlobalTriggers();
        }

        //private void loadGlobalTriggers()
        //{
        //    foreach (IJobListener jobListener in Scheduler.GetScheduler().GetJobDetail(null,null)..GlobalJobListeners)
        //    {
        //        globalTriggersListView.Items.Add(jobListener.Name);
        //    }
        //}

        private void loadJobGroups()
        {

            try
            {
                this.Cursor = Cursors.WaitCursor;

                jobDetailsToggle(false);
                jobGroupsTreeView.Nodes.Clear();
                SchedulerNode schedulerNode = new SchedulerNode(Scheduler);
                schedulerNode.ContextMenuStrip = ctxScheduler;
                jobGroupsTreeView.Nodes.Add(schedulerNode);
                TreeNode jobGroupsNode = schedulerNode.Nodes.Add("Job Groups");
                var jobGroups = Scheduler.GetScheduler().GetJobGroupNames();
                foreach (string jobGroup in jobGroups)
                {
                    TreeNode jobGroupNode = jobGroupsNode.Nodes.Add(jobGroup);
                    TreeNode jobsNode = jobGroupNode.Nodes.Add("Jobs");
                    addJobNodes(jobsNode);
                }

                jobGroupsTreeView.Nodes[0].Expand();
                jobGroupsNode.Expand();

                StripStatusLabel_Job_Groups.Text = DateTime.Now.ToString("yyyy.MM.dd HH:mm.ss");
                loadOrphanJobs(schedulerNode);
                loadStuckTriggers(schedulerNode);
                loadCalendars(schedulerNode);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }


        }

        private static void loadCalendars(SchedulerNode schedulerNode)
        {
            TreeNode calendarsNode = schedulerNode.Nodes.Add("Calendars");
            foreach (var calendarName in schedulerNode.Scheduler.GetScheduler().GetCalendarNames())
            {
                //TODO: make this a calendar node instead
                calendarsNode.Nodes.Add(calendarName);
            }
        }

        private void loadStuckTriggers(SchedulerNode schedulerNode)
        {
            TreeNode jobGroupsNode = schedulerNode.Nodes.Add("Stuck Triggers");
        }

        private void loadOrphanJobs(SchedulerNode schedulerNode)
        {
            TreeNode jobGroupsNode = schedulerNode.Nodes.Add("Orphan Jobs");
        }

        private void jobDetailsToggle(bool isVisible)
        {
            if (isVisible == false)
            {
                pnlDetails.Controls.Clear();
            }
        }

        void jobGroupsTreeView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TreeNode node = jobGroupsTreeView.GetNodeAt(e.X, e.Y);
                if (node != null)
                {
                    jobGroupsTreeView.SelectedNode = node;
                    ctxScheduler.Show(jobGroupsTreeView, e.Location);
                }
            }
        }
        //private void loadJobs()
        //{
        //    foreach (TreeNode node in jobGroupsTreeView.Nodes)
        //    {
        //        addJobNodes(node);
        //    }
        //}

        private void addJobNodes(TreeNode node)
        {
            string group = node.Parent.Text;
            var groupMatcher = GroupMatcher<JobKey>.GroupContains(group);
            var jobKeys = Scheduler.GetScheduler().GetJobKeys(groupMatcher);
            foreach (var jobKey in jobKeys)
            {
                try
                {
                    IJobDetail detail = Scheduler.GetScheduler().GetJobDetail(jobKey);
                    JobNode jobNode = new JobNode(detail);
                    node.Nodes.Add(jobNode);
                    addTriggerNodes(jobNode);
                    addListenerNodes(jobNode);
                }
                catch (Exception ex)
                {
                    node.Nodes.Add(string.Format("Unknown Job Type ({0})", jobKey.Name));
                    //TODO: Do something useful with this exception. Most likely cause is the client does not have a copy of a given dll and can't load the type.
                }
            }
        }

        private void addListenerNodes(JobNode node)
        {
            string jobName = node.Text;
            string jobGroupName = node.Parent.Text;
            //TODO: see joblistenermanager get listeners
            //string[] listenerNames = node.Detail.JobListenerNames;
            //foreach (string listener in listenerNames)
            //{
            //    node.Text = string.Format("JL {0}", listenerNames);
            //}
            //ISet set = Scheduler.GetScheduler().JobListenerNames;
        }

        private void addTriggerNodes(TreeNode treeNode)
        {
            var triggers = Scheduler.GetScheduler().GetTriggersOfJob(new JobKey(treeNode.Text, treeNode.Parent.Parent.Text));
            TreeNode triggersNode = treeNode.Nodes.Add("Triggers");
            foreach (var trigger in triggers)
            {
                TriggerNode node = new TriggerNode(trigger);
                triggersNode.Nodes.Add(node);
                addCalendarNode(node);
            }

        }

        private void addCalendarNode(TriggerNode node)
        {
            if (node.Trigger.CalendarName != null)
            {
                //TODO: Convert this to a CalendarNode and implement CalendarDisplay controls
                node.Nodes.Add(node.Trigger.CalendarName);
            }
            else
            {
                node.Nodes.Add("No calendar found");
            }
        }

        private void updateRunningJobs()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                timer_Refresh_Running_Jobs.Stop();

                listView_RunningJobs.Items.Clear();

                DataTable table = Scheduler.GetRunningJobs();
                foreach (DataRow row in table.Rows)
                {
                    //JobName JobDuration
                    ListViewItem item = new ListViewItem(new string[] { Convert.ToString(row["JobName"]), Convert.ToString(row["Runtime"]) });
                    listView_RunningJobs.Items.Add(item);
                }
                StripStatusLabel_Jobs_Refresh_date.Text = DateTime.Now.ToString("yyyy.MM.dd HH:mm.ss");


                //reset the timer ( documentation not clear if .stop = restart @ 0 in timing, but changing the interval sure should do that. )
                int timer_was = timer_Refresh_Running_Jobs.Interval;
                timer_Refresh_Running_Jobs.Interval = timer_was + 1;
                timer_Refresh_Running_Jobs.Interval = timer_was;

                timer_Refresh_Running_Jobs.Start();
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        public QuartzScheduler Scheduler { get; set; }

        private void addGlobalListenerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddListenerForm form = new AddListenerForm();
            form.ListenerInterface = typeof(IJobListener);
            form.ShowDialog();
            JobDataMap map = new JobDataMap();
            map.Add("type", form.ListenerType);
            //Scheduler.ScheduleOneTimeJob(typeof(AddJobListenerJob), map, 0);
            loadJobGroups();
        }

        private void addJobListenerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = jobGroupsTreeView.SelectedNode;
            if (selectedNode != null && selectedNode is JobNode)
            {
                AddListenerForm form = new AddListenerForm();
                form.ListenerInterface = typeof(IJobListener);
                form.ShowDialog();
                //JobHistoryListener listener = new JobHistoryListener();
                //listener.Name = null;
                //((JobNode)selectedNode).Detail.AddJobListener();
            }
        }

        private void addJobToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddJobForm form = new AddJobForm();
            form.ShowDialog();
            if (form.JobDetail != null && form.Trigger != null)
            {
                Scheduler.GetScheduler().ScheduleJob(form.JobDetail, form.Trigger);
                loadJobGroups();
            }
        }

        private void btnRefreshRunningJobs_Click(object sender, EventArgs e)
        {
            updateRunningJobs();
        }

        private void btnRefreshJobGroups_Click(object sender, EventArgs e)
        {
            loadJobGroups();
        }

        private void btnRunJobNow_Click(object sender, EventArgs e)
        {
            JobNode node = (JobNode)jobGroupsTreeView.SelectedNode;
            Scheduler.GetScheduler().TriggerJob(node.Detail.Key);
        }

        private void btnDeleteJob_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = jobGroupsTreeView.SelectedNode;
            if (selectedNode is JobNode)
            {
                JobNode node = (JobNode)jobGroupsTreeView.SelectedNode;
                Scheduler.GetScheduler().DeleteJob(node.Detail.Key);
                jobGroupsTreeView.SelectedNode.Remove();

            }
            if (selectedNode is TriggerNode)
            {
                Scheduler.GetScheduler().UnscheduleJob(((TriggerNode)selectedNode).Trigger.Key);
            }

            //loadJobGroups();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            TriggerNode node = (TriggerNode)jobGroupsTreeView.SelectedNode;
            if (Scheduler.GetScheduler().GetTriggerState(node.Trigger.Key) == TriggerState.Paused)
            {
                Scheduler.GetScheduler().ResumeTrigger(node.Trigger.Key);
            }
            else
            {
                Scheduler.GetScheduler().PauseTrigger(node.Trigger.Key);
            }
            setPauseButtonText();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            TriggerNode node = (TriggerNode)jobGroupsTreeView.SelectedNode;
            AddJobForm form = new AddJobForm(node);
            form.ShowDialog();
            if (form.JobDetail != null && form.Trigger != null)
            {
                Scheduler.GetScheduler().RescheduleJob(node.Trigger.Key, form.Trigger);
                loadJobGroups();
            }
        }

        private void backupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuartzScheduler scheduler = ((SchedulerNode)((TreeView)((ContextMenuStrip)((ToolStripMenuItem)sender).Owner).SourceControl).SelectedNode).Scheduler;
            FileDialog dialog = new SaveFileDialog();
            dialog.ShowDialog();
            FileInfo file = new FileInfo(dialog.FileName);
            scheduler.BackupToFile(file);
        }

        private void timer_Refresh_Running_Jobs_Tick(object sender, EventArgs e)
        {
            updateRunningJobs();
        }

        private void addAssemblyMenuItem_Click(object sender, EventArgs e)
        {
            FileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            dialog.Filter = "Assemblies (*.dll)|*.dll";
            dialog.ShowDialog();
            string fileName = Path.GetFileName(dialog.FileName);
            AssemblyRepository.AddAssembly(fileName);
        }

        private void deleteAssemblyMenuItem_Click(object sender, EventArgs e)
        {
            using (DeleteAssembliesForm form = new DeleteAssembliesForm())
            {
                form.ShowDialog();
                form.Close();
            }
        }
    }
}

