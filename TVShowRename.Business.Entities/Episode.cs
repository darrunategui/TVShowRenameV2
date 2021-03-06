﻿using System;
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
         return ToTwoDigitString(Number);
      }

      public string SeasonAsString()
      {
         return ToTwoDigitString(Season);
      }

      private string ToTwoDigitString(int input)
      {
         return (input.ToString().Length == 1) ?
            String.Format("0{0}", input) :
            input.ToString();
      }
   }
}
