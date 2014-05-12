using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVShowRename.Business.Entities
{
   /// <summary>
   /// Represents a tv show (media) file on the file system.
   /// </summary>
   public class TVShowFile
   {
      /// <summary>
      /// Gets the filename of the show (the location on the file system).
      /// </summary>
      public string Filename { get; private set; }

      /// <summary>
      /// Gets the name of the show.
      /// </summary>
      public string ShowName { get; private set; }

      /// <summary>
      /// Gets the season number.
      /// </summary>
      public int Season { get; private set; }

      /// <summary>
      /// Gets the episode number.
      /// </summary>
      public int Episode { get; private set; }

      /// <summary>
      /// Gets the file extension of the <see cref="Filename"/> property.
      /// </summary>
      public string Extension
      {
         get
         {
            if ( !String.IsNullOrEmpty(Filename) )
            {
               return Path.GetExtension(Filename).Substring(1);
            }
            else
            {
               return String.Empty;
            }
         }
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="TVShowFile"/> class
      /// </summary>
      /// <param name="filename">The filename.</param>
      /// <param name="title">The shows title.</param>
      /// <param name="season">The shows season number.</param>
      /// <param name="episode">The shows episode number.</param>
      public TVShowFile(string filename, string title, int season, int episode)
      {
         Filename = filename;
         ShowName = title;
         Season = season;
         Episode = episode;
      }
   }
}
