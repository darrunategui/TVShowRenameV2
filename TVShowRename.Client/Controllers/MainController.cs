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

      //private Regex _showRegex = new Regex(@"(?<" + TitleGroup + @">.*?)\.(?<" + InfoGroup + @">[s]\d{2}[e]\d{2})(.*)", 
      //RegexOptions.IgnoreCase | RegexOptions.Compiled);

      private string _currentfile;

      public MainController(IMainView view)
      {
         _view = view;
         view.SetController(this);
      }


      public void Rename(string[] filesToRename)
      {
         if ( filesToRename == null || filesToRename.Length == 0 )
         {
            throw new ArgumentException("No files to rename");
         }

         foreach ( string file in filesToRename )
         {
            // Loop through each parser and check if it can parse the input file.
            foreach (ITVShowParser parser in _serviceFactory.GetServiceManagers<ITVShowParser>())
            {
               if ( !parser.CanParse(file) )
               {
                  // Maybe the next parser will be able to parse the file.
                  continue;
               }
               else
               {
                  try
                  {
                     TVShowFile tvShowFile = parser.Parse(file);
                     ITVDBService tvdbManager = _serviceFactory.GetServiceManager<ITVDBService>();
                     IEnumerable<Show> results = tvdbManager.GetShowsByTitle(tvShowFile.ShowName);
                     InterpretShowResults(results);
                  }
                  catch
                  {
                     break;
                  }
                  break;
               }
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
               Rename(show);
               break;
            default:
               // More than one result... display the results in the view.
               _view.AddShowResults(results);
               break;
         }
      }

      private void Rename(Show show) //TODO: need to find a way to save the TVShowFile info and use it in this method.
      {
         // TODO: maybe I should have a rename controller that is initialized witht he TVShowFile that takes care of everything.
         // TODO: A new form that lets the user choose the tv show from the list might be a good idea. 
         // maybe even a modal form or some async task to process one file at a time.
         if ( showId == null || showId < 0 )
         {
            throw new ArgumentException("Show ID must not be null or less than zero.");
         }

         try
         {
            ITVDBService tvdbManager = _serviceFactory.GetServiceManager<ITVDBService>();
            IEnumerable<Episode> episodes = tvdbManager.GetEpisodesByShowId(showId);
         }
         catch
         {
            
         }
      }

   }
}
