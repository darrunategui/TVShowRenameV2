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

      public string Show { get; set; }

      public int Season { get; set; }

      public int Episode { get; set; }

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
         Show = title;
         Season = season;
         Episode = episode;
      }
   }
}
