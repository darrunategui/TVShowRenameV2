using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVShowRename.Common.Data
{
    public class ShowFields
    {
        public static string Series = "Series";
        public static string Id = "seriesid";
        public static string Language = "language";
        public static string Title = "SeriesName";
        public static string Description = "Overview";
        public static string FirstAired = "FirstAired";
        public static string Network = "Network";
        public static string ImdbId = "IMDB_ID";

        public static int DefaultId = 0;
        public static string DefaultLanguage = "Unknown";
        public static string DefaultTitle = "Title not found";
        public static string DefaultDescription = "Description not found";
        public static DateTime DefaultFirstAired = DateTime.Now;
        public static string DefaultNetwork = "Network not found";
        public static int DefaultImdbId = 0;

    }
}
