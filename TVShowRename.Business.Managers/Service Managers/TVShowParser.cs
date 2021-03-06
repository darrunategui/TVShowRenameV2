﻿using System;
using System.Text;
using System.ComponentModel.Composition;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using TVShowRename.Business.Contracts;
using TVShowRename.Business.Entities;

namespace TVShowRename.Business.Managers
{
    [Export(typeof(ITVShowParser))]
   [PartCreationPolicy(CreationPolicy.NonShared)]
   public class TVShowParser : ITVShowParser
   {
      private const string TitleGroup = "Title";
      private const string SeasonGroup = "Season";
      private const string EpisodeGroup = "Episode";

      /// <summary>
      /// To match tv shows with the following format: House.Of.Cards.S01E04.*****.mkv
      /// &lt;Name of show (period delimited)&gt;.&lt;season &amp; episode #&gt;.&lt;anything else&gt;.&lt;file extension&gt;
      /// </summary>
      private Regex _showRegex = new Regex(@"(?<" + TitleGroup + @">.+?)\.[s](?<" + SeasonGroup + @">\d+)[e](?<" + EpisodeGroup + @">\d+)(.*)",
                                          RegexOptions.IgnoreCase | RegexOptions.Compiled);

      /// <summary>
      /// Parses the given filename.
      /// </summary>
      /// <param name="filename">the filename to parse.</param>
      /// <returns>an object containing the important information parsed from the filename.</returns>
      public TVShowFile Parse(string filename)
      {
         if (String.IsNullOrEmpty(filename))
         {
            throw new ArgumentException("Parameter cannot be null of empty.", "filename");
         }

         if (!CanParse(filename))
         {
            throw new InvalidOperationException("Cannot parse the input file");
         }

         Match match = _showRegex.Match(filename);

         // parts contains the show name in an array. i.e. [game, of, thrones]
         string[] parts = Path.GetFileName(match.Groups[TitleGroup].Value).Split('.');
         StringBuilder sb = new StringBuilder();
         // Prepare the show name as one string. Capitalize the first letters and add spaces between the words
         for (int i = 0; i < parts.Length; ++i)
         {
            if ((parts[i].Length > 3) || (i == 0))
            {
               sb.Append(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(parts[i])); // Capitalize the first letter of each word greater than 3 characters (or if it's the first word).
            }
            else
            {
               sb.Append(parts[i].ToLower());
            }
            sb.Append(" ");
         }
         sb.Length--; // Trim the trailing space character;

         int season = int.Parse(match.Groups[SeasonGroup].Value);
         int episode = int.Parse(match.Groups[EpisodeGroup].Value);

         TVShowFile show = new TVShowFile(filename, sb.ToString(), season, episode);
         return show;
      }

      /// <summary>
      /// Determines whether or not this parser can parse the given filename.
      /// </summary>
      /// <param name="filename">the filename to parse</param>
      /// <returns>true if the filename can be parsed; otherwise, false.</returns>
      public bool CanParse(string filename)
      {
         if (String.IsNullOrEmpty(filename))
         {
            throw new ArgumentException("Parameter cannot be null or empty.", "filename");
         }
         return _showRegex.IsMatch(filename);
      }

   }
}
