namespace TVShowRename.Client.Views.Forms
{
   partial class ShowsForm
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
         this.lstShows = new System.Windows.Forms.ListView();
         this.lstClmTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
         this.lstClmNetwork = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
         this.lstClmDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
         this.SuspendLayout();
         // 
         // lstShows
         // 
         this.lstShows.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.lstShows.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lstClmTitle,
            this.lstClmNetwork,
            this.lstClmDescription});
         this.lstShows.FullRowSelect = true;
         this.lstShows.Location = new System.Drawing.Point(12, 57);
         this.lstShows.Name = "lstShows";
         this.lstShows.Size = new System.Drawing.Size(616, 462);
         this.lstShows.TabIndex = 0;
         this.lstShows.UseCompatibleStateImageBehavior = false;
         this.lstShows.View = System.Windows.Forms.View.Details;
         this.lstShows.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstShows_MouseDoubleClick);
         // 
         // lstClmTitle
         // 
         this.lstClmTitle.Text = "Title";
         this.lstClmTitle.Width = 128;
         // 
         // lstClmNetwork
         // 
         this.lstClmNetwork.Text = "Network";
         this.lstClmNetwork.Width = 144;
         // 
         // lstClmDescription
         // 
         this.lstClmDescription.Text = "Description";
         this.lstClmDescription.Width = 310;
         // 
         // ShowsForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(640, 531);
         this.Controls.Add(this.lstShows);
         this.Name = "ShowsForm";
         this.Text = "ShowsForm";
         this.Load += new System.EventHandler(this.ShowsForm_Load);
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.ListView lstShows;
      private System.Windows.Forms.ColumnHeader lstClmTitle;
      private System.Windows.Forms.ColumnHeader lstClmNetwork;
      private System.Windows.Forms.ColumnHeader lstClmDescription;
   }
}