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

namespace TVShowRename.Client.Controllers
{
   public class MainController : Controller
   {
      private const string TitleGroup = "Title";
      private const string InfoGroup = "Info";

      private IMainView _view;

      public MainController(IMainView view)
      {
         _view = view;
         view.SetController(this);
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
                  Task<IEnumerable<Show>> showsTaskResult = tvdbManager.GetShowsByTitle(tvShowFile.ShowName);
                  // TODO: downloading show data..
                  // TODO: update the progress bar to look like work is being done...
                  IEnumerable<Show> results = await showsTaskResult;
                  // TODO: make sure the progress bar finishes

                  InterpretShowResults(results, tvShowFile);
               }
               catch (Exception ex)
               {
                  // TODO: Set status text to error
                  SetStatus(String.Format("An error occured renaming the file. {0}", ex.Message));
                  break;
               }

               // TODO: I believe here would be the successfull case so display some success message.
               break;
            }
         }
         _view.VisualizeProgress(false);
      }

      private void InterpretShowResults(IEnumerable<Show> results, TVShowFile fileToRename)
      {
         switch (results.Count())
         {
            case 0:
               {
                  // no results... to bad..
                  break;
               }
            case 1:
               {
                  // one result... use the show ID to rename the file.
                  Show show = results.First();
                  ShowsController controller = new ShowsController(fileToRename);
                  controller.Rename(show);
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
