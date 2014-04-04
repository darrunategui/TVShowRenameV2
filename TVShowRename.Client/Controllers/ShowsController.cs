using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using TVShowRename.Business.Contracts;
using TVShowRename.Business.Entities;
using TVShowRename.Client.Views.Interfaces;

namespace TVShowRename.Client.Controllers
{
   public class ShowsController : Controller
   {
      #region Fields
      /// <summary>
      /// Reference to the view.
      /// </summary>
      private IShowsView _view;
      /// <summary>
      /// Represents the file to rename.
      /// </summary>
      private TVShowFile _fileToRename;
      /// <summary>
      /// Represents the possible shows to choose from.
      /// </summary>
      private IEnumerable<Show> _shows;
      #endregion

      /// <summary>
      /// Initializes a new instance of the ShowsController object.
      /// </summary>
      /// <param name="view">The associated view for this controller.</param>
      /// <param name="fileToRename">The file to rename.</param>
      /// <param name="shows">The show results to display on the view.</param>
      public ShowsController(IShowsView view, TVShowFile fileToRename, IEnumerable<Show> shows)
      {
         _view = view;
         _view.SetController(this);
         _fileToRename = fileToRename;
         _shows = shows;
      }

      /// <summary>
      /// Initializes the view with possible show results.
      /// </summary>
      internal void InitializeView()
      {
         _view.AddShows(_shows);
         _view.Label = "Please choose the intended show...";
      }

      /// <summary>
      /// Renames the file that given to this object on initialization.
      /// </summary>
      /// <param name="show"></param>
      internal void Rename(Show show)
      {
         try
         {
            ITVDBService tvdbManager = _serviceFactory.GetServiceManager<ITVDBService>();
            // TODO: downloading episode data...
            IEnumerable<Episode> episodes = tvdbManager.GetEpisodesByShowId(show.Id);

            Episode episode = (from e in episodes
                               where e.Season == _fileToRename.SeasonNumber &&
                                     e.Number == _fileToRename.EpisodeNumber
                               select e).FirstOrDefault();
            if ( episode == null )
            {
               // TODO: should be some way to determine if it was successfull or not.
               // Maybe just use First() and catch the exception if nothing is found.
               return;
            }

            // TODO: status - building the new filename.
            string newFilename = String.Format("{0} S{1}E{2} - {3}", show.Title, episode.Season, episode.Number, episode.Title);
            
            // Strip off any illegal filename characters.
            string invalidChars = new string(Path.GetInvalidFileNameChars());
            Regex regex = new Regex(String.Format("[{0}]", Regex.Escape(invalidChars)));
            newFilename = regex.Replace(newFilename, String.Empty);

            // TODO: status - renaming file.
            File.Move(_fileToRename.Filename, String.Format(@"{0}\{1}", Directory.GetParent(_fileToRename.Filename), newFilename));
            // TODO: I want to know if any exception occurs. Log it out or even just show a message box for testing.
         }
         catch ( Exception ex )
         {
            MessageBox.Show(ex.Message);
         }
      }

   }
}
