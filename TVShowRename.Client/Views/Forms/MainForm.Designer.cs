namespace TVShowRename.Client
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
            this.lstShowResults = new System.Windows.Forms.ListView();
            this.lstClmTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lstClmNetwork = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lstClmDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lstShowResults
            // 
            this.lstShowResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstShowResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lstClmTitle,
            this.lstClmNetwork,
            this.lstClmDescription});
            this.lstShowResults.FullRowSelect = true;
            this.lstShowResults.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstShowResults.Location = new System.Drawing.Point(12, 87);
            this.lstShowResults.MultiSelect = false;
            this.lstShowResults.Name = "lstShowResults";
            this.lstShowResults.Size = new System.Drawing.Size(1085, 367);
            this.lstShowResults.TabIndex = 0;
            this.lstShowResults.UseCompatibleStateImageBehavior = false;
            this.lstShowResults.View = System.Windows.Forms.View.Details;
            this.lstShowResults.DoubleClick += new System.EventHandler(this.lstShowResults_DoubleClick);
            // 
            // lstClmTitle
            // 
            this.lstClmTitle.Text = "Title";
            this.lstClmTitle.Width = 123;
            // 
            // lstClmNetwork
            // 
            this.lstClmNetwork.Text = "Network";
            this.lstClmNetwork.Width = 141;
            // 
            // lstClmDescription
            // 
            this.lstClmDescription.Text = "Description";
            this.lstClmDescription.Width = 405;
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1109, 519);
            this.Controls.Add(this.lstShowResults);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "TV Show Rename";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            this.DragLeave += new System.EventHandler(this.MainForm_DragLeave);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lstShowResults;
        private System.Windows.Forms.ColumnHeader lstClmTitle;
        private System.Windows.Forms.ColumnHeader lstClmNetwork;
        private System.Windows.Forms.ColumnHeader lstClmDescription;
    }
}

