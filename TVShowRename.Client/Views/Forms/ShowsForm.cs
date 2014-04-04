using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TVShowRename.Business.Entities;
using TVShowRename.Client.Controllers;
using TVShowRename.Client.Views.Interfaces;

namespace TVShowRename.Client.Views.Forms
{
   public partial class ShowsForm : Form, IShowsView
   {
      private ShowsController _controller;

      public ShowsForm()
      {
         InitializeComponent();
      }

      #region events
      private void ShowsForm_Load(object sender, EventArgs e)
      {
         _controller.InitializeView();
      }

      private void lstShows_MouseDoubleClick(object sender, MouseEventArgs e)
      {
         ListViewItem item = lstShows.HitTest(e.Location).Item;
         if ( item != null )
         {
            // TODO: send the tag to the controller to rename.
            Close();
         }
      }
      #endregion

      #region IShowsView methods

      public string Label
      {
         set { lblChooseShow.Text = value; }
      }

      public void SetController(Controller controller)
      {
         _controller = (ShowsController)controller;
      }

      public void AddShows(IEnumerable<Show> shows)
      {
         lstShows.Items.Clear();
         foreach (Show show in shows)
         {
            ListViewItem item = new ListViewItem(new string[] { show.Title, show.Network, show.Description });
            item.Tag = show;
            lstShows.Items.Add(item);
         }
         foreach (ColumnHeader column in lstShows.Columns)
         {
            column.Width = -2;
         }
      }
      #endregion



      
   }
}
