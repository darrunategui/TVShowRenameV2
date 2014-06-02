using Core.Common.Core;
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
using TVShowRename.Client.Views.Forms;
using TVShowRename.Common;
using TVShowRename.Common.Exceptions;

namespace TVShowRename.Client.Controllers
{
   public class MainController : Controller
   {
      private IMainView _view;

      public MainController(IMainView view)
      {
         _view = view;
         view.SetController(this);
      }

      /// <summary>
      /// Parses the <paramref name="inputFiles"/> parameter.
      /// </summary>
      /// <param name="inputFiles">The files to parse.</param>
      /// <param name="successfullyParsedFiles">When this method returns, contains the files that were successfully parsed and the values of the parsed files.</param>
      /// <param name="unsuccessfullyParsedFiles">When this method returns, contains the files that were unsuccessfully parsed.</param>
      internal void ParseTVShows(IEnumerable<string> inputFiles, out Dictionary<string, List<TVShowFile>> successfullyParsedFiles, out List<string> unsuccessfullyParsedFiles)
      {
         if (inputFiles == null)
         {
            throw new ArgumentNullException("inputFiles");
         }

         successfullyParsedFiles = new Dictionary<string, List<TVShowFile>>(); // At first, nothing is successfully parsed.
         unsuccessfullyParsedFiles = new List<string>(inputFiles); // At first, everything is unsuccessful. 

         foreach (string file in inputFiles) // parse each input file
         {
            foreach (ITVShowParser parser in _serviceFactory.GetServiceManagers<ITVShowParser>()) // use every parser available.
            {
               if (!parser.CanParse(file))
               {
                  continue; // Maybe the next parser can parse the file.
               }

               TVShowFile tvShowFile = parser.Parse(file);

               // Add the file to the successful list and remove it from the UNsuccessful list.
               if (!successfullyParsedFiles.ContainsKey(tvShowFile.ShowName))
               {
                  successfullyParsedFiles.Add(tvShowFile.ShowName, new List<TVShowFile>());
               }
               successfullyParsedFiles[tvShowFile.ShowName].Add(tvShowFile);
               unsuccessfullyParsedFiles.Remove(file);
            }
         }
      }


      internal async Task Rename(string file)
      {
         // TODO: set status text to parsing
         SetStatus("Parsing input file.");
         _view.VisualizeProgress(true);

         // Loop through each parser and check if it can parse the input file.
         foreach (ITVShowParser parser in _serviceFactory.GetServiceManagers<ITVShowParser>())
         {
            if (!parser.CanParse(file))
            {
               // Maybe the next parser will be able to parse the file.
               continue;
            }
            else
            {
               SetStatus("Successful parser found.");
               try
               {
                  TVShowFile tvShowFile = parser.Parse(file);
                  ITVDBService tvdbManager = _serviceFactory.GetServiceManager<ITVDBService>();

                  SetStatus(String.Format("Searching for a show with the name '{0}'", tvShowFile.ShowName));
                  Task<List<Show>> showsTaskResult = tvdbManager.GetShowsByTitle(tvShowFile.ShowName);
                  // TODO: downloading show data..
                  // TODO: update the progress bar to look like work is being done...
                  List<Show> results = await showsTaskResult;
                  // TODO: make sure the progress bar finishes

                  InterpretShowResults(results, tvShowFile);
               }
               catch (Exception ex)
               {
                  // TODO: Set status text to error
                  SetStatus(String.Format("An error occured renaming the file. {0}", ex.Message));
                  break;
               }

               // TODO: I believe here would be the successful case so display some success message.
               break;
            }
         }
         _view.VisualizeProgress(false);
      }


      /* I don't like the bubbling exceptions in the methods below.  
       * Find a better way to do it... */

      internal async Task<List<Show>> GetShowsByTitle(string show)
      {
         List<Show> showResults;
         // if the id is already saved, return the show with the ID.
         // TODO: get the ID from a saved file.

         //else
         // Search TVDB for shows with the given name.
         try
         {
            showResults = await DownloadShowsByTitle(show);
         }
         catch
         {
            throw;
         }

         return showResults;
      }

      /// <summary>
      /// Downloads show data from TVDB.
      /// </summary>
      /// <param name="show">the name of the show to get data for.</param>
      /// <returns>A list of shows matching the specified <paramref name="show"/>.</returns>
      private async Task<List<Show>> DownloadShowsByTitle(string show)
      {
         try
         {
            ITVDBService tvdbManager = _serviceFactory.GetServiceManager<ITVDBService>();
            Task<List<Show>> searchTask = tvdbManager.GetShowsByTitle(show);
            SetStatus(String.Format("Searching for shows with the name '{0}'", show));
            List<Show> results = await searchTask;
            return results;
         }
         catch
         {
            throw;
         }
      }

      private void InterpretShowResults(IEnumerable<Show> results, TVShowFile fileToRename)
      {
         switch (results.Count())
         {
            case 0:
               {
                  // no results... to bad..
                  // Not sure if ths code would ever get reached.
                  throw new ShowNotFoundException(String.Format("No show with the name '{0}' was found.", fileToRename.ShowName), fileToRename.ShowName);
               }
            case 1:
               {
                  // one result... use the show ID to rename the file.
                  Show show = results.First();
                  ShowsController controller = new ShowsController(fileToRename);
                  try
                  {
                     controller.Rename(show);
                  }
                  catch (EpisodeNotFoundException)
                  {
                     // TODO: Let the user know that the episode data could not be found.
                  }
                  break;
               }
            default:
               {
                  // TODO: create the ShowsFrom to display the results.
                  SetStatus(String.Format("Multiple shows with the name {0} were found. Select the intended show to continue...", fileToRename.ShowName));
                  using (ShowsForm form = new ShowsForm())
                  {
                     ShowsController controller = new ShowsController(form, fileToRename, results);
                     if (form.ShowDialog() != DialogResult.OK)
                     {
                        // TODO: error case.
                     }

                  }
                  break;
               }
         }
      }

      private void SetStatus(string text)
      {
         _view.Status = text;
      }

   }
}
