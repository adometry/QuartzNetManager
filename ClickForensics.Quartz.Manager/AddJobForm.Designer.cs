namespace ClickForensics.Quartz.Manager
{
    partial class AddJobForm
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
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cboJobType = new System.Windows.Forms.ComboBox();
            this.cboTriggerType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtJobGroup = new System.Windows.Forms.TextBox();
            this.txtJobName = new System.Windows.Forms.TextBox();
            this.txtTriggerName = new System.Windows.Forms.TextBox();
            this.txtTriggerDescription = new System.Windows.Forms.TextBox();
            this.txtCronExpression = new System.Windows.Forms.TextBox();
            this.txtTriggerGroup = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblJobDescription = new System.Windows.Forms.Label();
            this.txtJobDescription = new System.Windows.Forms.TextBox();
            this.jobDataListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.btnAddKeyValue = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(32, 273);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(140, 273);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cboJobType
            // 
            this.cboJobType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboJobType.FormattingEnabled = true;
            this.cboJobType.Location = new System.Drawing.Point(131, 13);
            this.cboJobType.Name = "cboJobType";
            this.cboJobType.Size = new System.Drawing.Size(277, 21);
            this.cboJobType.TabIndex = 2;
            // 
            // cboTriggerType
            // 
            this.cboTriggerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTriggerType.FormattingEnabled = true;
            this.cboTriggerType.Location = new System.Drawing.Point(131, 122);
            this.cboTriggerType.Name = "cboTriggerType";
            this.cboTriggerType.Size = new System.Drawing.Size(277, 21);
            this.cboTriggerType.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Job Type:";
            // 
            // txtJobGroup
            // 
            this.txtJobGroup.Location = new System.Drawing.Point(131, 41);
            this.txtJobGroup.Name = "txtJobGroup";
            this.txtJobGroup.Size = new System.Drawing.Size(277, 20);
            this.txtJobGroup.TabIndex = 5;
            // 
            // txtJobName
            // 
            this.txtJobName.Location = new System.Drawing.Point(131, 68);
            this.txtJobName.Name = "txtJobName";
            this.txtJobName.Size = new System.Drawing.Size(277, 20);
            this.txtJobName.TabIndex = 6;
            // 
            // txtTriggerName
            // 
            this.txtTriggerName.Location = new System.Drawing.Point(131, 177);
            this.txtTriggerName.Name = "txtTriggerName";
            this.txtTriggerName.Size = new System.Drawing.Size(277, 20);
            this.txtTriggerName.TabIndex = 8;
            // 
            // txtTriggerDescription
            // 
            this.txtTriggerDescription.Location = new System.Drawing.Point(131, 204);
            this.txtTriggerDescription.Name = "txtTriggerDescription";
            this.txtTriggerDescription.Size = new System.Drawing.Size(277, 20);
            this.txtTriggerDescription.TabIndex = 7;
            // 
            // txtCronExpression
            // 
            this.txtCronExpression.Location = new System.Drawing.Point(131, 231);
            this.txtCronExpression.Name = "txtCronExpression";
            this.txtCronExpression.Size = new System.Drawing.Size(277, 20);
            this.txtCronExpression.TabIndex = 10;
            // 
            // txtTriggerGroup
            // 
            this.txtTriggerGroup.Location = new System.Drawing.Point(131, 150);
            this.txtTriggerGroup.Name = "txtTriggerGroup";
            this.txtTriggerGroup.Size = new System.Drawing.Size(277, 20);
            this.txtTriggerGroup.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Job Group:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Job Name:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Trigger Type:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(29, 207);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Trigger Description:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(29, 180);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Trigger Name:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(29, 153);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Trigger Group:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(29, 234);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(86, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Cron Expression:";
            // 
            // lblJobDescription
            // 
            this.lblJobDescription.AutoSize = true;
            this.lblJobDescription.Location = new System.Drawing.Point(29, 98);
            this.lblJobDescription.Name = "lblJobDescription";
            this.lblJobDescription.Size = new System.Drawing.Size(83, 13);
            this.lblJobDescription.TabIndex = 19;
            this.lblJobDescription.Text = "Job Description:";
            // 
            // txtJobDescription
            // 
            this.txtJobDescription.Location = new System.Drawing.Point(131, 95);
            this.txtJobDescription.Name = "txtJobDescription";
            this.txtJobDescription.Size = new System.Drawing.Size(277, 20);
            this.txtJobDescription.TabIndex = 18;
            // 
            // jobDataListView
            // 
            this.jobDataListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.jobDataListView.FullRowSelect = true;
            this.jobDataListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.jobDataListView.Location = new System.Drawing.Point(559, 43);
            this.jobDataListView.MultiSelect = false;
            this.jobDataListView.Name = "jobDataListView";
            this.jobDataListView.Size = new System.Drawing.Size(233, 99);
            this.jobDataListView.TabIndex = 20;
            this.jobDataListView.UseCompatibleStateImageBehavior = false;
            this.jobDataListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Key";
            this.columnHeader1.Width = 82;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Value";
            this.columnHeader2.Width = 145;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(556, 17);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 13);
            this.label10.TabIndex = 21;
            this.label10.Text = "Job Data:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(560, 162);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(28, 13);
            this.label11.TabIndex = 23;
            this.label11.Text = "Key:";
            // 
            // txtKey
            // 
            this.txtKey.Location = new System.Drawing.Point(604, 159);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(188, 20);
            this.txtKey.TabIndex = 22;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(560, 188);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(37, 13);
            this.label12.TabIndex = 25;
            this.label12.Text = "Value:";
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(604, 185);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(188, 20);
            this.txtValue.TabIndex = 24;
            // 
            // btnAddKeyValue
            // 
            this.btnAddKeyValue.Location = new System.Drawing.Point(559, 211);
            this.btnAddKeyValue.Name = "btnAddKeyValue";
            this.btnAddKeyValue.Size = new System.Drawing.Size(50, 23);
            this.btnAddKeyValue.TabIndex = 26;
            this.btnAddKeyValue.Text = "Add";
            this.btnAddKeyValue.UseVisualStyleBackColor = true;
            this.btnAddKeyValue.Click += new System.EventHandler(this.btnAddKeyValue_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(798, 41);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(52, 23);
            this.btnDelete.TabIndex = 27;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // AddJobForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1044, 338);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAddKeyValue);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtKey);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.jobDataListView);
            this.Controls.Add(this.lblJobDescription);
            this.Controls.Add(this.txtJobDescription);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCronExpression);
            this.Controls.Add(this.txtTriggerGroup);
            this.Controls.Add(this.txtTriggerName);
            this.Controls.Add(this.txtTriggerDescription);
            this.Controls.Add(this.txtJobName);
            this.Controls.Add(this.txtJobGroup);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboTriggerType);
            this.Controls.Add(this.cboJobType);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAdd);
            this.Name = "AddJobForm";
            this.Text = "AddJobForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cboJobType;
        private System.Windows.Forms.ComboBox cboTriggerType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtJobGroup;
        private System.Windows.Forms.TextBox txtJobName;
        private System.Windows.Forms.TextBox txtTriggerName;
        private System.Windows.Forms.TextBox txtTriggerDescription;
        private System.Windows.Forms.TextBox txtCronExpression;
        private System.Windows.Forms.TextBox txtTriggerGroup;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblJobDescription;
        private System.Windows.Forms.TextBox txtJobDescription;
        private System.Windows.Forms.ListView jobDataListView;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.Button btnAddKeyValue;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
    }
}