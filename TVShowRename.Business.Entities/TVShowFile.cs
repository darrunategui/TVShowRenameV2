using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVShowRename.Business.Entities
{
   public class TVShowFile
   {
      public string Filename { get; set; }

      public string ShowName { get; set; }

      public int SeasonNumber { get; set; }

      public int EpisodeNumber { get; set; }

      public string Extension
      {
         get
         {
            if ( !String.IsNullOrEmpty(Filename) )
            {
               return Path.GetExtension(Filename);
            }
            else
            {
               return String.Empty;
            }
         }
      }

      public TVShowFile(string filename, string title, int season, int episode)
      {
         Filename = filename;
         ShowName = title;
         SeasonNumber = season;
         EpisodeNumber = episode;
      }
   }
}
