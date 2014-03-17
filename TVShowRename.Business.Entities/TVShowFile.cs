using System;
using System.Collections.Generic;
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

      public TVShowFile(string filename, string title, int season, int episode)
      {
         Filename = filename;
         Show = title;
         Season = season;
         Episode = episode;
      }
   }
}
