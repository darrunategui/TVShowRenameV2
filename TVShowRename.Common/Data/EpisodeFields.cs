using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVShowRename.Common.Data
{
    public class EpisodeFields
    {
        public static string Id = "id";
        public static string Director = "Director";
        public static string Title = "EpisodeName";
        public static string Number = "EpisodeNumber";
        public static string Season = "SeasonNumber";
        public static string Description = "Overview";


        public static int DefaultId = 0;
        public static string DefaultDirector = "Director not found";
        public static string DefaultTitle = "Title not found";
        public static int DefaultNumber = 0;
        public static int DefaultSeason = 0;
        public static string DefaultDescription = "Description not found";
    }
}
