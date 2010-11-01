namespace ClickForensics.Quartz.Manager
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.schedulerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jobsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addJobToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listenersStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.globalListenersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addGlobalJobListenerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addTriggerListenerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addJobListenerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.serverConnectStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.StripStatusLabel_Job_Groups = new System.Windows.Forms.ToolStripStatusLabel();
            this.StripStatusLabel_Jobs_Refresh_date = new System.Windows.Forms.ToolStripStatusLabel();
            this.jobGroupsTreeView = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnRefreshRunningJobs = new System.Windows.Forms.Button();
            this.btnRefreshJobGroups = new System.Windows.Forms.Button();
            this.btnDeleteJob = new System.Windows.Forms.Button();
            this.btnRunJobNow = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.pnlDetails = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.btnEdit = new System.Windows.Forms.Button();
            this.ctxScheduler = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.backupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer_Refresh_Running_Jobs = new System.Windows.Forms.Timer(this.components);
            this.listView_RunningJobs = new System.Windows.Forms.ListView();
            this.JobName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.JobDuration = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.mainMenuStrip.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.ctxScheduler.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.schedulerToolStripMenuItem,
            this.jobsToolStripMenuItem,
            this.listenersStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(913, 24);
            this.mainMenuStrip.TabIndex = 0;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // schedulerToolStripMenuItem
            // 
            this.schedulerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToolStripMenuItem});
            this.schedulerToolStripMenuItem.Name = "schedulerToolStripMenuItem";
            this.schedulerToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.schedulerToolStripMenuItem.Text = "Scheduler";
            // 
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            this.connectToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.connectToolStripMenuItem.Text = "Connect";
            this.connectToolStripMenuItem.Click += new System.EventHandler(this.connectToolStripMenuItem_Click);
            // 
            // jobsToolStripMenuItem
            // 
            this.jobsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addJobToolStripMenuItem});
            this.jobsToolStripMenuItem.Enabled = false;
            this.jobsToolStripMenuItem.Name = "jobsToolStripMenuItem";
            this.jobsToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
            this.jobsToolStripMenuItem.Text = "Jobs";
            // 
            // addJobToolStripMenuItem
            // 
            this.addJobToolStripMenuItem.Name = "addJobToolStripMenuItem";
            this.addJobToolStripMenuItem.Size = new System.Drawing.Size(96, 22);
            this.addJobToolStripMenuItem.Text = "Add";
            this.addJobToolStripMenuItem.Click += new System.EventHandler(this.addJobToolStripMenuItem_Click);
            // 
            // listenersStripMenuItem
            // 
            this.listenersStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.globalListenersToolStripMenuItem,
            this.addJobListenerToolStripMenuItem});
            this.listenersStripMenuItem.Enabled = false;
            this.listenersStripMenuItem.Name = "listenersStripMenuItem";
            this.listenersStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.listenersStripMenuItem.Text = "Listeners";
            // 
            // globalListenersToolStripMenuItem
            // 
            this.globalListenersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addGlobalJobListenerToolStripMenuItem,
            this.addTriggerListenerToolStripMenuItem});
            this.globalListenersToolStripMenuItem.Name = "globalListenersToolStripMenuItem";
            this.globalListenersToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.globalListenersToolStripMenuItem.Text = "Global";
            // 
            // addGlobalJobListenerToolStripMenuItem
            // 
            this.addGlobalJobListenerToolStripMenuItem.Name = "addGlobalJobListenerToolStripMenuItem";
            this.addGlobalJobListenerToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.addGlobalJobListenerToolStripMenuItem.Text = "Add Job Listener";
            this.addGlobalJobListenerToolStripMenuItem.Click += new System.EventHandler(this.addGlobalListenerToolStripMenuItem_Click);
            // 
            // addTriggerListenerToolStripMenuItem
            // 
            this.addTriggerListenerToolStripMenuItem.Name = "addTriggerListenerToolStripMenuItem";
            this.addTriggerListenerToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.addTriggerListenerToolStripMenuItem.Text = "Add Trigger Listener";
            // 
            // addJobListenerToolStripMenuItem
            // 
            this.addJobListenerToolStripMenuItem.Name = "addJobListenerToolStripMenuItem";
            this.addJobListenerToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.addJobListenerToolStripMenuItem.Text = "Add Job Listener";
            this.addJobListenerToolStripMenuItem.Click += new System.EventHandler(this.addJobListenerToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.serverConnectStatusLabel,
            this.StripStatusLabel_Job_Groups,
            this.StripStatusLabel_Jobs_Refresh_date});
            this.statusStrip1.Location = new System.Drawing.Point(0, 639);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(913, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // serverConnectStatusLabel
            // 
            this.serverConnectStatusLabel.Name = "serverConnectStatusLabel";
            this.serverConnectStatusLabel.Size = new System.Drawing.Size(86, 17);
            this.serverConnectStatusLabel.Text = "Not connected";
            // 
            // StripStatusLabel_Job_Groups
            // 
            this.StripStatusLabel_Job_Groups.BackColor = System.Drawing.Color.LightCyan;
            this.StripStatusLabel_Job_Groups.Name = "StripStatusLabel_Job_Groups";
            this.StripStatusLabel_Job_Groups.Size = new System.Drawing.Size(157, 17);
            this.StripStatusLabel_Job_Groups.Text = "StripStatusLabel_Job_Groups";
            this.StripStatusLabel_Job_Groups.ToolTipText = "Last Refresh of Job Groups";
            // 
            // StripStatusLabel_Jobs_Refresh_date
            // 
            this.StripStatusLabel_Jobs_Refresh_date.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.StripStatusLabel_Jobs_Refresh_date.Name = "StripStatusLabel_Jobs_Refresh_date";
            this.StripStatusLabel_Jobs_Refresh_date.Size = new System.Drawing.Size(191, 17);
            this.StripStatusLabel_Jobs_Refresh_date.Text = "StripStatusLabel_Jobs_Refresh_date";
            this.StripStatusLabel_Jobs_Refresh_date.ToolTipText = "Last Refresh Date of Running Jobs";
            // 
            // jobGroupsTreeView
            // 
            this.jobGroupsTreeView.HideSelection = false;
            this.jobGroupsTreeView.Location = new System.Drawing.Point(8, 48);
            this.jobGroupsTreeView.Name = "jobGroupsTreeView";
            this.jobGroupsTreeView.Size = new System.Drawing.Size(351, 252);
            this.jobGroupsTreeView.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Scheduler Objects";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 319);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Running Jobs";
            // 
            // btnRefreshRunningJobs
            // 
            this.btnRefreshRunningJobs.Location = new System.Drawing.Point(371, 609);
            this.btnRefreshRunningJobs.Name = "btnRefreshRunningJobs";
            this.btnRefreshRunningJobs.Size = new System.Drawing.Size(75, 23);
            this.btnRefreshRunningJobs.TabIndex = 6;
            this.btnRefreshRunningJobs.Text = "Refresh";
            this.btnRefreshRunningJobs.UseVisualStyleBackColor = true;
            this.btnRefreshRunningJobs.Click += new System.EventHandler(this.btnRefreshRunningJobs_Click);
            // 
            // btnRefreshJobGroups
            // 
            this.btnRefreshJobGroups.Location = new System.Drawing.Point(284, 306);
            this.btnRefreshJobGroups.Name = "btnRefreshJobGroups";
            this.btnRefreshJobGroups.Size = new System.Drawing.Size(75, 23);
            this.btnRefreshJobGroups.TabIndex = 7;
            this.btnRefreshJobGroups.Text = "Refresh";
            this.btnRefreshJobGroups.UseVisualStyleBackColor = true;
            this.btnRefreshJobGroups.Click += new System.EventHandler(this.btnRefreshJobGroups_Click);
            // 
            // btnDeleteJob
            // 
            this.btnDeleteJob.Enabled = false;
            this.btnDeleteJob.Location = new System.Drawing.Point(533, 306);
            this.btnDeleteJob.Name = "btnDeleteJob";
            this.btnDeleteJob.Size = new System.Drawing.Size(65, 23);
            this.btnDeleteJob.TabIndex = 8;
            this.btnDeleteJob.Text = "Delete";
            this.btnDeleteJob.UseVisualStyleBackColor = true;
            this.btnDeleteJob.Click += new System.EventHandler(this.btnDeleteJob_Click);
            // 
            // btnRunJobNow
            // 
            this.btnRunJobNow.Enabled = false;
            this.btnRunJobNow.Location = new System.Drawing.Point(391, 306);
            this.btnRunJobNow.Name = "btnRunJobNow";
            this.btnRunJobNow.Size = new System.Drawing.Size(65, 23);
            this.btnRunJobNow.TabIndex = 9;
            this.btnRunJobNow.Text = "Run";
            this.btnRunJobNow.UseVisualStyleBackColor = true;
            this.btnRunJobNow.Click += new System.EventHandler(this.btnRunJobNow_Click);
            // 
            // btnPause
            // 
            this.btnPause.Enabled = false;
            this.btnPause.Location = new System.Drawing.Point(462, 306);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(65, 23);
            this.btnPause.TabIndex = 10;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // pnlDetails
            // 
            this.pnlDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDetails.Location = new System.Drawing.Point(391, 45);
            this.pnlDetails.Name = "pnlDetails";
            this.pnlDetails.Size = new System.Drawing.Size(342, 252);
            this.pnlDetails.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(388, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Details";
            // 
            // btnEdit
            // 
            this.btnEdit.Enabled = false;
            this.btnEdit.Location = new System.Drawing.Point(604, 306);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(65, 23);
            this.btnEdit.TabIndex = 13;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // ctxScheduler
            // 
            this.ctxScheduler.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backupToolStripMenuItem});
            this.ctxScheduler.Name = "ctxScheduler";
            this.ctxScheduler.Size = new System.Drawing.Size(109, 26);
            // 
            // backupToolStripMenuItem
            // 
            this.backupToolStripMenuItem.Name = "backupToolStripMenuItem";
            this.backupToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.backupToolStripMenuItem.Text = "Backup";
            this.backupToolStripMenuItem.Click += new System.EventHandler(this.backupToolStripMenuItem_Click);
            // 
            // timer_Refresh_Running_Jobs
            // 
            this.timer_Refresh_Running_Jobs.Interval = 30000;
            this.timer_Refresh_Running_Jobs.Tick += new System.EventHandler(this.timer_Refresh_Running_Jobs_Tick);
            // 
            // listView_RunningJobs
            // 
            this.listView_RunningJobs.AllowColumnReorder = true;
            this.listView_RunningJobs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.JobName,
            this.JobDuration});
            this.listView_RunningJobs.Location = new System.Drawing.Point(8, 335);
            this.listView_RunningJobs.Name = "listView_RunningJobs";
            this.listView_RunningJobs.Size = new System.Drawing.Size(725, 268);
            this.listView_RunningJobs.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listView_RunningJobs.TabIndex = 14;
            this.listView_RunningJobs.UseCompatibleStateImageBehavior = false;
            this.listView_RunningJobs.View = System.Windows.Forms.View.Details;
            // 
            // JobName
            // 
            this.JobName.Text = "Job Name";
            // 
            // JobDuration
            // 
            this.JobDuration.Text = "Duration";
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(533, 306);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(65, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Delete";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnDeleteJob_Click);
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(391, 306);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(65, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "Run";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btnRunJobNow_Click);
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(462, 306);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(65, 23);
            this.button3.TabIndex = 10;
            this.button3.Text = "Pause";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // button4
            // 
            this.button4.Enabled = false;
            this.button4.Location = new System.Drawing.Point(604, 306);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(65, 23);
            this.button4.TabIndex = 13;
            this.button4.Text = "Edit";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(913, 661);
            this.Controls.Add(this.btnRefreshJobGroups);
            this.Controls.Add(this.listView_RunningJobs);
            this.Controls.Add(this.pnlDetails);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnRefreshRunningJobs);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.mainMenuStrip);
            this.Controls.Add(this.btnRunJobNow);
            this.Controls.Add(this.jobGroupsTreeView);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnDeleteJob);
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "MainForm";
            this.Text = "Quartz Manager";
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ctxScheduler.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem schedulerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel serverConnectStatusLabel;
        private System.Windows.Forms.TreeView jobGroupsTreeView;
        private System.Windows.Forms.ToolStripMenuItem listenersStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem globalListenersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addGlobalJobListenerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addTriggerListenerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addJobListenerToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem jobsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addJobToolStripMenuItem;
        private System.Windows.Forms.Button btnRefreshRunningJobs;
        private System.Windows.Forms.Button btnRefreshJobGroups;
        private System.Windows.Forms.Button btnDeleteJob;
        private System.Windows.Forms.Button btnRunJobNow;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Panel pnlDetails;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.ContextMenuStrip ctxScheduler;
        private System.Windows.Forms.ToolStripMenuItem backupToolStripMenuItem;
        private System.Windows.Forms.Timer timer_Refresh_Running_Jobs;
        private System.Windows.Forms.ToolStripStatusLabel StripStatusLabel_Jobs_Refresh_date;
        private System.Windows.Forms.ToolStripStatusLabel StripStatusLabel_Job_Groups;
        private System.Windows.Forms.ListView listView_RunningJobs;
        private System.Windows.Forms.ColumnHeader JobName;
        private System.Windows.Forms.ColumnHeader JobDuration;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}

