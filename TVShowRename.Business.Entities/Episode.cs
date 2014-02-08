using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVShowRename.Business.Entities
{
    public class Episode
    {
        public string Title { get; private set; }

        public int Number { get; private set; }

        public int Season { get; private set; }

        public Episode(string title, int number, int season)
        {
            Title = title;
            Number = number;
            Season = season;
        }
    }
}
