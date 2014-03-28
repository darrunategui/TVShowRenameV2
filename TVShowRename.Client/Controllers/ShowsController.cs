using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TVShowRename.Business.Contracts;
using TVShowRename.Business.Entities;
using TVShowRename.Client.Views.Interfaces;

namespace TVShowRename.Client.Controllers
{
   public class ShowsController : Controller
   {
      private IShowsView _view;
      private TVShowFile _fileToRename;
      private IEnumerable<Show> _shows;

      public ShowsController(IShowsView view, TVShowFile fileToRename, IEnumerable<Show> shows)
      {
         _view = view;
      }

      internal void InitializeView()
      {
         _view.AddShows(_shows);
      }

      internal void Rename(Show show)
      {
         try
         {
            ITVDBService tvdbManager = _serviceFactory.GetServiceManager<ITVDBService>();
            // TODO: downloading episode data...
            IEnumerable<Episode> episodes = tvdbManager.GetEpisodesByShowId(show.Id);

            Episode episode = episodes
                              .Where(e =>
                                 e.Season == _fileToRename.SeasonNumber &&
                                 e.Number == _fileToRename.EpisodeNumber).FirstOrDefault();
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
         catch
         { }
      }

   }
}
