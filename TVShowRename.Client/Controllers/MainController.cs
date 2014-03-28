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

namespace TVShowRename.Client
{
   public class MainController : Controller
   {
      private const string TitleGroup = "Title";
      private const string InfoGroup = "Info";

      private IMainView _view;
      private string _currentfile;

      public MainController(IMainView view)
      {
         _view = view;
         view.SetController(this);
      }

      internal async Task Rename(string file)
      {
         // TODO: set status text to parsing
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
               try
               {
                  // TODO: set status text to renaming...
                  TVShowFile tvShowFile = parser.Parse(file);
                  ITVDBService tvdbManager = _serviceFactory.GetServiceManager<ITVDBService>();
                  // TODO: set status text to searching for show.
                  Task<IEnumerable<Show>> showsTaskResult = tvdbManager.GetShowsByTitle(tvShowFile.ShowName);
                  // TODO: downloading show data..
                  IEnumerable<Show> results = await showsTaskResult;

                  InterpretShowResults(results);
               }
               catch
               {
                  // TODO: Set status text to error
                  break;
               }
               // TODO: I believe here would be the successfull case so display some success message.
               break;
            }
         }
      }

      private void InterpretShowResults(IEnumerable<Show> results)
      {
         switch ( results.Count() )
         {
            case 0:
               // no results... to bad..
               break;
            case 1:
               // one result... use the show ID to rename the file.
               Show show = results.First();
               //Rename(show);
               break;
            default:
               // TODO: create the ShowsFrom to display the results.
               // More than one result... display the results in the view.
               _view.AddShowResults(results);
               break;
         }
      }


      
   }
}
