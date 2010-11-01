namespace ClickForensics.Quartz.Manager
{
    partial class CronTriggerDisplay
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblGroup = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblPreviousFireTime = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblNextFireTime = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtCronExpression = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cron Expression:";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(116, 47);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(35, 13);
            this.lblName.TabIndex = 3;
            this.lblName.Text = "label2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Name:";
            // 
            // lblGroup
            // 
            this.lblGroup.AutoSize = true;
            this.lblGroup.Location = new System.Drawing.Point(116, 68);
            this.lblGroup.Name = "lblGroup";
            this.lblGroup.Size = new System.Drawing.Size(35, 13);
            this.lblGroup.TabIndex = 5;
            this.lblGroup.Text = "label4";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Group:";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(116, 89);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(35, 13);
            this.lblDescription.TabIndex = 7;
            this.lblDescription.Text = "label6";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 89);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Description:";
            // 
            // lblPreviousFireTime
            // 
            this.lblPreviousFireTime.AutoSize = true;
            this.lblPreviousFireTime.Location = new System.Drawing.Point(116, 131);
            this.lblPreviousFireTime.Name = "lblPreviousFireTime";
            this.lblPreviousFireTime.Size = new System.Drawing.Size(35, 13);
            this.lblPreviousFireTime.TabIndex = 9;
            this.lblPreviousFireTime.Text = "label8";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(14, 131);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(97, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "Previous Fire Time:";
            // 
            // lblNextFireTime
            // 
            this.lblNextFireTime.AutoSize = true;
            this.lblNextFireTime.Location = new System.Drawing.Point(116, 110);
            this.lblNextFireTime.Name = "lblNextFireTime";
            this.lblNextFireTime.Size = new System.Drawing.Size(41, 13);
            this.lblNextFireTime.TabIndex = 11;
            this.lblNextFireTime.Text = "label10";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(14, 110);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(78, 13);
            this.label11.TabIndex = 10;
            this.label11.Text = "Next Fire Time:";
            // 
            // txtCronExpression
            // 
            this.txtCronExpression.Location = new System.Drawing.Point(119, 23);
            this.txtCronExpression.Name = "txtCronExpression";
            this.txtCronExpression.ReadOnly = true;
            this.txtCronExpression.Size = new System.Drawing.Size(100, 20);
            this.txtCronExpression.TabIndex = 12;
            this.txtCronExpression.Text = "txtCronExpression";
            // 
            // CronTriggerDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtCronExpression);
            this.Controls.Add(this.lblNextFireTime);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.lblPreviousFireTime);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblGroup);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Name = "CronTriggerDisplay";
            this.Size = new System.Drawing.Size(324, 168);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblGroup;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblPreviousFireTime;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblNextFireTime;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtCronExpression;
    }
}
