using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVShowRename.Business.Entities
{
    public class Show
    {
        public int Id { get; private set; }

        public string Language { get; private set; }

        public string Title { get; private set; }

        public string Description { get; private set; }

        public DateTime FirstAired { get; private set; }

        public string Network { get; private set; }

        public Show(int id, string title, string description, string language,
                    DateTime firstAired, string network)
        {
            Id = id;
            Title = title;
            Description = description;
            Language = language;
            FirstAired = firstAired;
            Network = network;
        }
    }
}