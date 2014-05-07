using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TVShowRename.Business.Contracts;
using TVShowRename.Business.Entities;

namespace TVShowRename.Business.Managers
{
   [Export(typeof(ITVShowParser))]
   [PartCreationPolicy(CreationPolicy.NonShared)]
   public class TVShowParser : ITVShowParser
   {
      private const string TitleGroup = "Title";
      private const string InfoGroup = "Info";
      private const string SeasonGroup = "Season";
      private const string EpisodeGroup = "Episode";

      /// <summary>
      /// To match tv shows with the following format: House.Of.Cards.S01E04.*****.mkv
      /// &lt;Name of show (period delimited)&gt;.&lt;season &amp; episode #&gt;.&lt;anything else&gt;.&lt;file extension&gt;
      /// </summary>
      private Regex _showRegex = new Regex(@"(?<" + TitleGroup + @">.*?)\.[s](?<" + SeasonGroup + @">\d+)[e](?<" + EpisodeGroup + @">\d+)(.*)",
                                          RegexOptions.IgnoreCase | RegexOptions.Compiled);

      /// <summary>
      /// Parses the given filename.
      /// </summary>
      /// <param name="filename">the filename to parse.</param>
      /// <returns>an object containing the important information parsed from the filename.</returns>
      public TVShowFile Parse(string filename)
      {
         if (CanParse(filename))
         {
            Match match = _showRegex.Match(filename);
            string showTitle = Path.GetFileName(match.Groups[TitleGroup].Value).Replace('.', ' ');
            string info = match.Groups[InfoGroup].Value;
            int season = int.Parse(match.Groups[SeasonGroup].Value);
            int episode = int.Parse(match.Groups[EpisodeGroup].Value);

            TVShowFile show = new TVShowFile(filename, showTitle, season, episode);
            return show;
         }
         else
         {
            throw new InvalidOperationException("Cannot parse the input file");
         }
      }

      /// <summary>
      /// Determines whether or not this parser can parse the given filename.
      /// </summary>
      /// <param name="filename">the filename to parse</param>
      /// <returns>true if the filename can be parsed; otherwise, false.</returns>
      public bool CanParse(string filename)
      {
         if ( String.IsNullOrEmpty(filename))
         {
            throw new ArgumentException("Parameter cannot be null or empty.", "filename");
         }
         return _showRegex.IsMatch(filename);
      }

   }
}
