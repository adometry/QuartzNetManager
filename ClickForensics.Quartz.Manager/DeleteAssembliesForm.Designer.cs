namespace ClickForensics.Quartz.Manager
{
    partial class DeleteAssembliesForm
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
            this.lbxAssemblies = new System.Windows.Forms.ListBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbxAssemblies
            // 
            this.lbxAssemblies.FormattingEnabled = true;
            this.lbxAssemblies.Location = new System.Drawing.Point(42, 25);
            this.lbxAssemblies.Name = "lbxAssemblies";
            this.lbxAssemblies.Size = new System.Drawing.Size(162, 212);
            this.lbxAssemblies.TabIndex = 0;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(80, 243);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // DeleteAssembliesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(247, 291);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.lbxAssemblies);
            this.Name = "DeleteAssembliesForm";
            this.Text = "DeleteAssembliesForm";
            this.Load += new System.EventHandler(this.DeleteAssembliesForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbxAssemblies;
        private System.Windows.Forms.Button btnDelete;
    }
}