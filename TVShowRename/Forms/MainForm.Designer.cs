namespace TVShowRename
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.showPossibilitiesLabel = new System.Windows.Forms.Label();
            this.dragTVShowLabel = new System.Windows.Forms.Label();
            this.dropImage = new System.Windows.Forms.PictureBox();
            this.showPossibilities = new System.Windows.Forms.ListView();
            this.nameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.networkColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.firstAiredColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.successNotification = new System.Windows.Forms.NotifyIcon(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dropImage)).BeginInit();
            this.SuspendLayout();
            // 
            // showPossibilitiesLabel
            // 
            this.showPossibilitiesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.showPossibilitiesLabel.Location = new System.Drawing.Point(12, 96);
            this.showPossibilitiesLabel.Name = "showPossibilitiesLabel";
            this.showPossibilitiesLabel.Size = new System.Drawing.Size(762, 36);
            this.showPossibilitiesLabel.TabIndex = 0;
            this.showPossibilitiesLabel.Text = "Show Possibilities";
            this.showPossibilitiesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.showPossibilitiesLabel.Visible = false;
            // 
            // dragTVShowLabel
            // 
            this.dragTVShowLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dragTVShowLabel.Location = new System.Drawing.Point(12, 467);
            this.dragTVShowLabel.Name = "dragTVShowLabel";
            this.dragTVShowLabel.Size = new System.Drawing.Size(762, 37);
            this.dragTVShowLabel.TabIndex = 1;
            this.dragTVShowLabel.Text = "Drag TV Show Here";
            this.dragTVShowLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dropImage
            // 
            this.dropImage.BackgroundImage = global::TVShowRename.Properties.Resources.DropImage;
            this.dropImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.dropImage.Location = new System.Drawing.Point(189, 135);
            this.dropImage.Name = "dropImage";
            this.dropImage.Size = new System.Drawing.Size(386, 346);
            this.dropImage.TabIndex = 2;
            this.dropImage.TabStop = false;
            // 
            // showPossibilities
            // 
            this.showPossibilities.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameColumn,
            this.networkColumn,
            this.firstAiredColumn});
            this.showPossibilities.Location = new System.Drawing.Point(12, 135);
            this.showPossibilities.Name = "showPossibilities";
            this.showPossibilities.Size = new System.Drawing.Size(762, 566);
            this.showPossibilities.TabIndex = 3;
            this.showPossibilities.UseCompatibleStateImageBehavior = false;
            this.showPossibilities.View = System.Windows.Forms.View.Details;
            this.showPossibilities.Visible = false;
            this.showPossibilities.DoubleClick += new System.EventHandler(this.showWasSelected);
            // 
            // nameColumn
            // 
            this.nameColumn.Text = "Show Name";
            this.nameColumn.Width = 137;
            // 
            // networkColumn
            // 
            this.networkColumn.Text = "Network";
            this.networkColumn.Width = 121;
            // 
            // firstAiredColumn
            // 
            this.firstAiredColumn.Text = "First Aired";
            this.firstAiredColumn.Width = 192;
            // 
            // successNotification
            // 
            this.successNotification.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.successNotification.Icon = ((System.Drawing.Icon)(resources.GetObject("successNotification.Icon")));
            this.successNotification.Text = "TV Show Rename";
            this.successNotification.Visible = true;
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(786, 713);
            this.Controls.Add(this.dragTVShowLabel);
            this.Controls.Add(this.dropImage);
            this.Controls.Add(this.showPossibilitiesLabel);
            this.Controls.Add(this.showPossibilities);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "TV Show Rename";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.showDroppedOnForm);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.showDraggedOnForm);
            this.DragLeave += new System.EventHandler(this.showDraggedOffForm);
            ((System.ComponentModel.ISupportInitialize)(this.dropImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label showPossibilitiesLabel;
        public System.Windows.Forms.Label dragTVShowLabel;
        public System.Windows.Forms.PictureBox dropImage;
        public System.Windows.Forms.ListView showPossibilities;
        public System.Windows.Forms.ColumnHeader nameColumn;
        public System.Windows.Forms.ColumnHeader networkColumn;
        public System.Windows.Forms.ColumnHeader firstAiredColumn;
        public System.Windows.Forms.NotifyIcon successNotification;

    }
}

