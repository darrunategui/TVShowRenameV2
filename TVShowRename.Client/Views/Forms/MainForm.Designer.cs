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
         this.lblStatus.Location = new System.Drawing.Point(11, 50);
         this.lblStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
         this.lblStatus.Name = "lblStatus";
         this.lblStatus.Size = new System.Drawing.Size(280, 65);
         this.lblStatus.TabIndex = 0;
         this.lblStatus.Text = "label1";
         // 
         // prgProgress
         // 
         this.prgProgress.Location = new System.Drawing.Point(6, 141);
         this.prgProgress.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
         this.prgProgress.MarqueeAnimationSpeed = 0;
         this.prgProgress.Name = "prgProgress";
         this.prgProgress.Size = new System.Drawing.Size(290, 21);
         this.prgProgress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
         this.prgProgress.TabIndex = 1;
         // 
         // MainForm
         // 
         this.AllowDrop = true;
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(302, 190);
         this.Controls.Add(this.prgProgress);
         this.Controls.Add(this.lblStatus);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
         this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
         this.MaximizeBox = false;
         this.Name = "MainForm";
         this.Text = "TV Show Rename";
         this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
         this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
         this.DragLeave += new System.EventHandler(this.MainForm_DragLeave);
         this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ProgressBar prgProgress;

    }
}

