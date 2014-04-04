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
using TVShowRename.Client.Controllers;

namespace TVShowRename.Client.Views.Forms
{
    public partial class MainForm : Form, IMainView, ISingletonEnforcer
    {
        private MainController _controller;

        public MainForm()
        {
            InitializeComponent();
        }

        #region IMainView methods
        public string Status 
        {
           set { lblStatus.Text = value; }
        }
        public void SetController(Controller controller)
        {
            _controller = (MainController)controller;
        }
        #endregion

        #region ISingletonEnforcer methods
        public void OnMessageReceived(string[] args)
        {
            // Do nothing for now...
        }
        #endregion

        private async void MainForm_DragDrop(object sender, DragEventArgs e)
        {
           if (e.Data.GetDataPresent(DataFormats.FileDrop))
           {
              string[] droppedFiles = (string[])e.Data.GetData(DataFormats.FileDrop);
              if (droppedFiles == null || droppedFiles.Length == 0)
              {
                 // TODO: show some error message.
                 return;
              }
              else
              {
                 foreach (string file in droppedFiles.Where(a => !String.IsNullOrEmpty(a)))
                 {
                    await _controller.Rename(file);
                 }
              }

              //TODO: show that the show is being processed.
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
