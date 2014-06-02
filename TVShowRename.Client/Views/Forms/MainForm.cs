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
using TVShowRename.Common;

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

        public void VisualizeProgress(bool showprogess)
        {
           prgProgress.MarqueeAnimationSpeed = (showprogess) ? 20 : 0;
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
           if (!e.Data.GetDataPresent(DataFormats.FileDrop))
           {
              // Bad case. Bail out.
              return;
           }


           string[] droppedFiles = (string[])e.Data.GetData(DataFormats.FileDrop);
           if (droppedFiles == null || droppedFiles.Length == 0)
           {
              // TODO: show some error message.
              return;
           }

           List<string> unsuccessfullyParsedFiles;
           Dictionary<string, List<TVShowFile>> successfullyParsedFiles;
           _controller.ParseTVShows(droppedFiles, out successfullyParsedFiles, out unsuccessfullyParsedFiles);
           // At this point, any files that can't be parsed should be seperated from the ones that can.
           // Multiple files of the same show will be grouped together so only one query is made to the database per show.

           // TODO: show unsuccessful files (show that they will be ignored).

           // Contains the show results from TVDB by tv show.
           Dictionary<string, List<Show>> showsWithIDs = new Dictionary<string, List<Show>>();

           foreach (string show in successfullyParsedFiles.Keys.Cast<string>())
           {
              try
              {
                 List<Show> showResults = await _controller.GetShowsByTitle(show);
                 showsWithIDs.Add(show, showResults);
              }
              catch (ShowNotFoundException ex)
              {
                 // TODO: let the user know that the show didn't have any results.
                 continue;
              }
              catch (Exception ex)
              {
                 // TODO: general exception. log it out or just show a msg box to know what happened.
                 continue;
              }
           }
           // At this point, All show results should be in the showsWithIDs list. The dictionary
           // keys will provide a link back the the files to be renamed in successfullyParsedFiles.

           //TODO: now we need to display a show picker screen to choose the shows that returned multiple results.

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
