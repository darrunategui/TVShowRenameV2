using Core.Common.Singleton;
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

namespace TVShowRename.Client
{
    public partial class MainForm : Form, IMainView, ISingletonEnforcer
    {
        private MainController _controller;

        public MainForm()
        {
            InitializeComponent();
        }

        #region IMainView methods
        public void SetController(Controller controller)
        {
            _controller = (MainController)controller;
        }

        public void AddShowResults(IEnumerable<Show> shows)
        {
            foreach (Show show in shows)
            {
                ListViewItem item = new ListViewItem(new string[] { show.Title, show.Network, show.Description });
                item.Tag = show;
                lstShowResults.Items.Add(item);
            }
            foreach (ColumnHeader column in lstShowResults.Columns)
            {
               column.Width = -2;               
            }
        }
        #endregion

        #region ISingletonEnforcer methods
        public void OnMessageReceived(string[] args)
        {
            // Do nothing for now...
        }
        #endregion

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] droppedFiles = (string[])e.Data.GetData(DataFormats.FileDrop);
                _controller.Rename(droppedFiles);
                
            }
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void MainForm_DragLeave(object sender, EventArgs e)
        {

        }

        private void lstShowResults_DoubleClick(object sender, EventArgs e)
        {

        }

    }
}
