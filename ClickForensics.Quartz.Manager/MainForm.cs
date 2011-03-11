using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Common.Logging;
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
            QuartzScheduler scheduler = getScheduler(node);
            if (scheduler.GetScheduler().GetTriggerState(node.Trigger.Key) == TriggerState.Paused)
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
                        connectToolStripMenuItem.Enabled = false;
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
                var jobGroups = scheduler.GetScheduler().GetJobGroupNames();
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
            var groupNames = schedulerNode.Scheduler.GetScheduler().GetJobGroupNames();
            foreach (var jobGroupName in groupNames)
            {
                var matcher = GroupMatcher<JobKey>.GroupEquals(jobGroupName);
                var jobKeys = schedulerNode.Scheduler.GetScheduler().GetJobKeys(matcher);
                foreach (var jobKey in jobKeys)
                {
                    try
                    {
                        var triggers = schedulerNode.Scheduler.GetScheduler().GetTriggersOfJob(jobKey);
                        if (triggers.Count == 0)
                        {
                            orphanJobsNode.Nodes.Add(
                                new JobNode(schedulerNode.Scheduler.GetScheduler().GetJobDetail(jobKey)));
                        }
                    }
                    catch (Exception ex)
                    {
                        _Log.Error("Error loading orphan jobs.", ex);
                        schedulerNode.Nodes.Add(string.Format("Unable to add job {0})", jobKey.Name));
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
            var groupMatcher = GroupMatcher<JobKey>.GroupContains(group);
            QuartzScheduler scheduler = getScheduler(node);
            var jobKeys = scheduler.GetScheduler().GetJobKeys(groupMatcher);
            foreach (var jobKey in jobKeys)
            {
                try
                {
                    IJobDetail detail = scheduler.GetScheduler().GetJobDetail(jobKey);
                    JobNode jobNode = new JobNode(detail);
                    node.Nodes.Add(jobNode);
                    addTriggerNodes(jobNode);
                }
                catch (Exception ex)
                {
                    node.Nodes.Add(string.Format("Unknown Job Type ({0})", jobKey.Name));
                    //TODO: Do something useful with this exception. Most likely cause is the client does not have a copy of a given dll and can't load the type.
                    _Log.Error("Error loading jobs.", ex);
                }
            }
        }

        private void addTriggerNodes(TreeNode treeNode)
        {
            QuartzScheduler scheduler = getScheduler(treeNode);
            var triggers = scheduler.GetScheduler().GetTriggersOfJob(new JobKey(treeNode.Text, treeNode.Parent.Parent.Text));
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
                foreach (var quartzScheduler in getAllSchedulers())
                {
                    DataTable table = quartzScheduler.GetRunningJobs();
                    foreach (DataRow row in table.Rows)
                    {
                        ListViewItem item =
                            new ListViewItem(new string[] { Convert.ToString(row["JobName"]), Convert.ToString(row["Runtime"]) });
                        listView_RunningJobs.Items.Add(item);
                    }
                }
                StripStatusLabel_Jobs_Refresh_date.Text = DateTime.Now.ToString("yyyy.MM.dd HH:mm.ss");

                int timer_was = timer_Refresh_Running_Jobs.Interval;
                timer_Refresh_Running_Jobs.Interval = timer_was + 1;
                timer_Refresh_Running_Jobs.Interval = timer_was;

                timer_Refresh_Running_Jobs.Start();
            }
            catch (Exception ex)
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
            TreeNode node = jobGroupsTreeView.SelectedNode;
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
            scheduler.GetScheduler().TriggerJob(node.Detail.Key);
        }

        private void btnDeleteJob_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = jobGroupsTreeView.SelectedNode;
            if (selectedNode is JobNode)
            {
                JobNode node = (JobNode)jobGroupsTreeView.SelectedNode;
                QuartzScheduler scheduler = getScheduler(selectedNode);
                scheduler.GetScheduler().DeleteJob(node.Detail.Key);
                jobGroupsTreeView.SelectedNode.Remove();

            }
            if (selectedNode is TriggerNode)
            {
                QuartzScheduler scheduler = getScheduler(selectedNode);
                scheduler.GetScheduler().UnscheduleJob(((TriggerNode)selectedNode).Trigger.Key);
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            TriggerNode node = (TriggerNode)jobGroupsTreeView.SelectedNode;
            QuartzScheduler scheduler = getScheduler(node);
            if (scheduler.GetScheduler().GetTriggerState(node.Trigger.Key) == TriggerState.Paused)
            {
                scheduler.GetScheduler().ResumeTrigger(node.Trigger.Key);
            }
            else
            {
                scheduler.GetScheduler().PauseTrigger(node.Trigger.Key);
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
                scheduler.GetScheduler().RescheduleJob(node.Trigger.Key, form.Trigger);
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

