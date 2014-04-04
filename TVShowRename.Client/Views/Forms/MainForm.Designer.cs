namespace TVShowRename.Client.Views.Forms
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
         this.lblStatus = new System.Windows.Forms.Label();
         this.prgProgress = new System.Windows.Forms.ProgressBar();
         this.SuspendLayout();
         // 
         // lblStatus
         // 
         this.lblStatus.AutoSize = true;
         this.lblStatus.Location = new System.Drawing.Point(12, 244);
         this.lblStatus.Name = "lblStatus";
         this.lblStatus.Size = new System.Drawing.Size(70, 25);
         this.lblStatus.TabIndex = 0;
         this.lblStatus.Text = "label1";
         // 
         // prgProgress
         // 
         this.prgProgress.Location = new System.Drawing.Point(12, 272);
         this.prgProgress.Name = "prgProgress";
         this.prgProgress.Size = new System.Drawing.Size(580, 41);
         this.prgProgress.TabIndex = 1;
         // 
         // MainForm
         // 
         this.AllowDrop = true;
         this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(604, 519);
         this.Controls.Add(this.prgProgress);
         this.Controls.Add(this.lblStatus);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
         this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
         this.MaximizeBox = false;
         this.Name = "MainForm";
         this.Text = "TV Show Rename";
         this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
         this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
         this.DragLeave += new System.EventHandler(this.MainForm_DragLeave);
         this.ResumeLayout(false);
         this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ProgressBar prgProgress;

    }
}

