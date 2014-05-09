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
using TVShowRename.Common.Exceptions;

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

      #region Constructors
      /// <summary>
      /// Initializes a new instance of the ShowsController object.
      /// </summary>
      /// <remarks>Should only be used when a view is to be shown to display multiple show results.</remarks>
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
      /// Initializes a new instance of the ShowsController object.
      /// </summary>
      /// <remarks>Should only be used when a view is to be shown to display multiple show results.</remarks>
      /// <param name="filetoRename">The file to rename.</param>
      public ShowsController(TVShowFile filetoRename)
      {
         _fileToRename = filetoRename;
      }
      #endregion

      #region public methods
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

         ITVDBService tvdbManager = _serviceFactory.GetServiceManager<ITVDBService>();
         // TODO: downloading episode data...
         IEnumerable<Episode> episodes = tvdbManager.GetEpisodesByShowId(show.Id);

         Episode episode = (from e in episodes
                            where e.Season == _fileToRename.SeasonNumber &&
                                  e.Number == _fileToRename.EpisodeNumber
                            select e).FirstOrDefault();
         if (episode == null)
         {
            // TODO: should be some way to determine if it was successfull or not.
            // Maybe just use First() and catch the exception if nothing is found.
            throw new EpisodeNotFoundException(String.Format("Show: '{0}' did not contain information for season {1}, episode {2}",
                                                             show.Title, _fileToRename.SeasonNumber, _fileToRename.EpisodeNumber));
         }

         // TODO: status - building the new filename.
         // TODO: Allow multiple output filename templates
         string newFilename = String.Format("{0} S{1}E{2} - {3}", show.Title, episode.SeasonAsString(), episode.NumberAsString(), episode.Title);

         // Strip off any illegal filename characters.
         string invalidChars = new string(Path.GetInvalidFileNameChars());
         Regex regex = new Regex(String.Format("[{0}]", Regex.Escape(invalidChars)));
         newFilename = regex.Replace(newFilename, String.Empty);

         try
         {
            // TODO: status - renaming file.
            File.Move(_fileToRename.Filename, String.Format(@"{0}\{1}", Directory.GetParent(_fileToRename.Filename), newFilename));
            // TODO: I want to know if any exception occurs. Log it out or even just show a message box for testing.
            // The previous version of TVShow rename got hung up way to often without any indication of what was happening.
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message);
         }
      }
      #endregion

      #region private methods
      private void UpdateStatus(string status)
      {
         // TODO: check if the status should be updated (is there a view to update or not).
         if ( _view == null )
         {
            return;
         }
         // Continue..
      }
      #endregion

   }
}
