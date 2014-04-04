using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVShowRename.Business.Entities
{
   public class Episode
   {
      public int Id { get; private set; }

      public string Director { get; private set; }

      public string Title { get; private set; }

      public int Number { get; private set; }

      public int Season { get; private set; }

      public string Description { get; private set; }

      public Episode(int id, string director, string title, int number, int season, string description)
      {
         Id = id;
         Director = director;
         Title = title;
         Number = number;
         Season = season;
         Description = description;
      }

      public string NumberAsString()
      {
         return (Number.ToString().Length == 1) ?
            String.Format("0{0}", Number) :
            Number.ToString();
      }

      public string SeasonAsString()
      {
         return (Season.ToString().Length == 1) ?
            String.Format("0{0}", Season) :
            Season.ToString();
      }
   }
}
