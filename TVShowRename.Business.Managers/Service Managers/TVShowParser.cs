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
   [Export(typeof(ITVDBService))]
   [PartCreationPolicy(CreationPolicy.NonShared)]
   public class TVShowParser : ITVShowParser
   {
      private const string TitleGroup = "Title";
      private const string InfoGroup = "Info";

      /// <summary>
      /// To match tv shows with the following format: House.Of.Cards.S01E04.*****.mkv
      /// &lt;Name of show (period delimited)&gt;.&lt;season &amp; episode #&gt;.&lt;anything else&gt;.&lt;file extension&gt;
      /// </summary>
      private Regex _showRegex = new Regex(@"(?<" + TitleGroup + @">.*?)\.(?<" + InfoGroup + @">[s]\d{2}[e]\d{2})(.*)",
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
            int season = GetSeasonNumberFrom(info);
            int episode = GetEpisodeNumberFrom(info);

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
         return _showRegex.IsMatch(filename);
      }



      /// <summary>
      /// Gets the season number from the Season And Episode Identifier.
      /// </summary>
      /// <param name="SeasonAndEpisodeIdentifier">The identifier</param>
      /// <returns>Returns the season number.</returns>
      private int GetSeasonNumberFrom(string SeasonAndEpisodeIdentifier)
      {
         return int.Parse(SeasonAndEpisodeIdentifier.Substring(1, 2));
      }

      /// <summary>
      /// Gets the episode number from the Season And Episode Identifier.
      /// </summary>
      /// <param name="SeasonAndEpisodeIdentifier"></param>
      /// <returns>Returns the episode number.</returns>
      private int GetEpisodeNumberFrom(string SeasonAndEpisodeIdentifier)
      {
         return int.Parse(SeasonAndEpisodeIdentifier.Substring(4, 2));
      }

   }
}
