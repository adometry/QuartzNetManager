using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Quartz;
using Quartz.Collection;
using System.Net.Sockets;
//using ClickForensics.Quartz.Jobs;
using System.IO;
using System.Reflection;
using log4net;

namespace ClickForensics.Quartz.Manager
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            jobGroupsTreeView.AfterSelect += new TreeViewEventHandler(jobGroupsTreeView_AfterSelect);
            jobGroupsTreeView.MouseDown += new MouseEventHandler(jobGroupsTreeView_MouseDown);
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
                if (((TriggerNode)e.Node).Trigger is CronTrigger)
                {
                    pnlDetails.Controls.Add(new CronTriggerDisplay((CronTrigger)((TriggerNode)e.Node).Trigger));
                    jobDetailsToggle(true);
                }
                if (((TriggerNode)e.Node).Trigger is SimpleTrigger)
                {
                    pnlDetails.Controls.Add(new SimpleTriggerDisplay((SimpleTrigger)((TriggerNode)e.Node).Trigger));
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
            QuartzScheduler scheduler = getScheduler(node);

            string name = node.Trigger.Name;
            string group = node.Trigger.Group;
            if (scheduler.GetScheduler().GetTriggerState(name, group) == TriggerState.Paused)
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
                        QuartzScheduler scheduler = new QuartzScheduler(form.Server, form.Port, form.Scheduler);
                        serverConnectStatusLabel.Text = string.Format("Connected to {0}", scheduler.Address);
                        jobsToolStripMenuItem.Enabled = true;
                        loadJobGroups(scheduler);
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
        }

        private void loadJobGroups(QuartzScheduler scheduler)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                jobDetailsToggle(false);

                SchedulerNode schedulerNode = new SchedulerNode(scheduler);
                if (jobGroupsTreeView.Nodes.ContainsKey(schedulerNode.Name))
                {
                    jobGroupsTreeView.Nodes.RemoveByKey(schedulerNode.Name);
                }
                
                schedulerNode.ContextMenuStrip = ctxScheduler;
                jobGroupsTreeView.Nodes.Add(schedulerNode);
                TreeNode jobGroupsNode = schedulerNode.Nodes.Add("Job Groups");
                string[] jobGroups = schedulerNode.Scheduler.GetScheduler().JobGroupNames;
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
            TreeNode orphanJobsNode = schedulerNode.Nodes.Add("Orphan Jobs");
            var groupNames = schedulerNode.Scheduler.GetScheduler().JobGroupNames;
            foreach (var jobGroupName in groupNames)
            {
                var jobNames = schedulerNode.Scheduler.GetScheduler().GetJobNames(jobGroupName);
                foreach (var jobName in jobNames)
                {
                    try
                    {
                        var triggers = schedulerNode.Scheduler.GetScheduler().GetTriggersOfJob(jobName, jobGroupName);
                        if (triggers.Length == 0)
                        {
                            orphanJobsNode.Nodes.Add(
                                new JobNode(schedulerNode.Scheduler.GetScheduler().GetJobDetail(jobName, jobGroupName)));
                        }
                    }
                    catch (Exception ex)
                    {
                        _Log.Error("Error loading orphan jobs.", ex);

                    }
                }
            }
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

        private void addJobNodes(TreeNode node)
        {
            string group = node.Parent.Text;
            QuartzScheduler scheduler = getScheduler(node);

            string[] jobs = scheduler.GetScheduler().GetJobNames(group);
            foreach (string jobName in jobs)
            {
                try
                {
                    JobDetail detail = scheduler.GetScheduler().GetJobDetail(jobName, group);
                    JobNode jobNode = new JobNode(detail);
                    node.Nodes.Add(jobNode);
                    addTriggerNodes(jobNode);
                    addListenerNodes(jobNode);
                }
                catch (Exception ex)
                {
                    node.Nodes.Add(string.Format("Unknown Job Type ({0})", jobName));
                    //TODO: Do something useful with this exception. Most likely cause is the client does not have a copy of a given dll and can't load the type.
                    _Log.Error("Error loading jobs.", ex);
                }
            }
        }


        private void addListenerNodes(JobNode node)
        {
            string jobName = node.Text;
            string jobGroupName = node.Parent.Text;
            string[] listenerNames = node.Detail.JobListenerNames;
            foreach (string listener in listenerNames)
            {
                node.Text = string.Format("JL {0}", listenerNames);
            }
        }

        private void addTriggerNodes(TreeNode treeNode)
        {
            QuartzScheduler scheduler = getScheduler(treeNode);

            Trigger[] triggers = scheduler.GetScheduler().GetTriggersOfJob(treeNode.Text, treeNode.Parent.Parent.Text);
            TreeNode triggersNode = treeNode.Nodes.Add("Triggers");
            foreach (Trigger trigger in triggers)
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
                List<QuartzScheduler> schedulers = getAllSchedulers();
                foreach (var quartzScheduler in schedulers)
                {
                    DataTable table = quartzScheduler.GetRunningJobs();
                    foreach (DataRow row in table.Rows)
                    {
                        ListViewItem item = new ListViewItem(new string[] { Convert.ToString(row["JobName"]), Convert.ToString(row["Runtime"]) });
                        listView_RunningJobs.Items.Add(item);
                    }
                }

                StripStatusLabel_Jobs_Refresh_date.Text = DateTime.Now.ToString("yyyy.MM.dd HH:mm.ss");

                int timer_was = timer_Refresh_Running_Jobs.Interval;
                timer_Refresh_Running_Jobs.Interval = timer_was + 1;
                timer_Refresh_Running_Jobs.Interval = timer_was;

                timer_Refresh_Running_Jobs.Start();
            }
                catch(Exception ex)
                {
                    _Log.Error("Unable to load running jobs", ex);
                }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private List<QuartzScheduler> getAllSchedulers()
        {
            List<QuartzScheduler> schedulers = new List<QuartzScheduler>();
            foreach (var node in jobGroupsTreeView.Nodes)
            {
                schedulers.Add(((SchedulerNode)node).Scheduler);
            }
            return schedulers;
        }

        private void addJobToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddJobForm form = new AddJobForm();
            form.ShowDialog();
            if (form.JobDetail != null && form.Trigger != null)
            {
                QuartzScheduler scheduler = getSelectedScheduler();
                scheduler.GetScheduler().ScheduleJob(form.JobDetail, form.Trigger);
                loadJobGroups(scheduler);
            }
        }

        private QuartzScheduler getSelectedScheduler()
        {
            TreeNode node= jobGroupsTreeView.SelectedNode;
            return getScheduler(node);
        }

        private void btnRefreshRunningJobs_Click(object sender, EventArgs e)
        {
            updateRunningJobs();
        }

        private void btnRefreshJobGroups_Click(object sender, EventArgs e)
        {
            List<QuartzScheduler> schedulers = getAllSchedulers();
            foreach (var quartzScheduler in schedulers)
            {
                loadJobGroups(quartzScheduler);
            }
        }

        private void btnRunJobNow_Click(object sender, EventArgs e)
        {
            JobNode node = (JobNode)jobGroupsTreeView.SelectedNode;
            QuartzScheduler scheduler = getScheduler(node);

            string job = node.Detail.Name;
            string group = node.Detail.Group;
            scheduler.GetScheduler().TriggerJobWithVolatileTrigger(job, group);
        }

        private void btnDeleteJob_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = jobGroupsTreeView.SelectedNode;
            if (selectedNode is JobNode)
            {
                JobNode node = (JobNode)jobGroupsTreeView.SelectedNode;
                QuartzScheduler scheduler = getScheduler(node);
                string job = node.Detail.Name;
                string group = node.Detail.Group;
                scheduler.GetScheduler().DeleteJob(job, group);
                jobGroupsTreeView.SelectedNode.Remove();

            }
            if (selectedNode is TriggerNode)
            {
                TriggerNode node = (TriggerNode)selectedNode;
                QuartzScheduler scheduler = getScheduler(node);

                scheduler.GetScheduler().UnscheduleJob(node.Trigger.Name, ((TriggerNode)selectedNode).Trigger.Group);
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            TriggerNode node = (TriggerNode)jobGroupsTreeView.SelectedNode;
            QuartzScheduler scheduler = getScheduler(node);
            string name = node.Trigger.Name;
            string group = node.Trigger.Group;
            if (scheduler.GetScheduler().GetTriggerState(name, group) == TriggerState.Paused)
            {
                scheduler.GetScheduler().ResumeTrigger(name, group);
            }
            else
            {
                scheduler.GetScheduler().PauseTrigger(name, group);
            }
            setPauseButtonText();
        }

        private QuartzScheduler getScheduler(TreeNode node)
        {
            if (node is SchedulerNode)
            {
                return ((SchedulerNode)node).Scheduler;
            }
            return getScheduler(node.Parent);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            TriggerNode node = (TriggerNode)jobGroupsTreeView.SelectedNode;
            QuartzScheduler scheduler = getScheduler(node);
            AddJobForm form = new AddJobForm(node);
            form.ShowDialog();
            if (form.JobDetail != null && form.Trigger != null)
            {
                scheduler.GetScheduler().RescheduleJob(node.Trigger.Name, node.Trigger.Group, form.Trigger);
                loadJobGroups(scheduler);
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
        private static readonly ILog _Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);        
    }
}
