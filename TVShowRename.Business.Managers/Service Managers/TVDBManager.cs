﻿using Core.Common.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using TVShowRename.Business.Contracts;
using TVShowRename.Business.Entities;
using TVShowRename.Common;
using TVShowRename.Common.Data;

namespace TVShowRename.Business.Managers
{
    [Export(typeof(ITVDBService))]
   [PartCreationPolicy(CreationPolicy.NonShared)]
   public class TVDBManager : ITVDBService
   {
      /// <summary>
      /// Api Key to acces tvdb.
      /// </summary>
      private const string apiKey = "57B9F10354E657BE";

      /// <summary>
      /// Only active mirror.
      /// </summary>
      private const string mirror = "http://thetvdb.com/";

      public async Task<List<Show>> GetShowsByTitle(string title)
      {
         if (String.IsNullOrEmpty(title))
         {
            throw new ArgumentException("Parameter cannot be null or empty.", "title");
         }

         string address = String.Format("{0}/api/GetSeries.php?seriesname={1}&language=en", mirror, title);
         string xml;
         using (WebClient client = new WebClient())
         {
            xml = await client.DownloadStringTaskAsync(address);
         }

         // Make sure the downloaded data is valid.
         if (String.IsNullOrEmpty(xml))
         {
            throw new WebException(String.Format("Nothing was returned by the query '{0}'", address));
         }

         XDocument document;
         try { document = XDocument.Parse(xml); }
         catch { throw; }

         if (!document.Root.HasElement(ShowFields.Series))
         {
            throw new ShowNotFoundException("The show could not be found.", title);
         }

         try
         {
            List<Show> shows = new List<Show>();
            foreach (XElement show in document.Root.Elements(ShowFields.Series))
            {
               int id = (show.HasElement(ShowFields.Id)) ?
                        int.Parse(show.Element(ShowFields.Id).Value) :
                        ShowFields.DefaultId;

               string language = (show.HasElement(ShowFields.Language)) ?
                                 show.Element(ShowFields.Language).Value :
                                 ShowFields.DefaultLanguage;

               string showTitle = (show.HasElement(ShowFields.Title)) ?
                                  show.Element(ShowFields.Title).Value :
                                  ShowFields.DefaultTitle;

               string description = (show.HasElement(ShowFields.Description)) ?
                                    show.Element(ShowFields.Description).Value :
                                    ShowFields.DefaultDescription;

               DateTime firstAired = (show.HasElement(ShowFields.FirstAired) && DateTime.TryParse(show.Element(ShowFields.FirstAired).Value, out firstAired)) ?
                                     firstAired :
                                     ShowFields.DefaultFirstAired;

               string network = (show.HasElement(ShowFields.Network)) ?
                                    show.Element(ShowFields.Network).Value :
                                    ShowFields.DefaultNetwork;

               Show newShow = new Show(id, showTitle, description, language, firstAired, network);

               shows.Add(newShow);
            }
            return shows;
         }
         catch (Exception ex)
         {
            throw new InvalidOperationException("Could not parse the show data.", ex);
         }
      }

      public List<Episode> GetEpisodesByShowId(int id)
      {
         // Argument check
         if (id <= 0)
         {
            throw new ArgumentException("The show ID must be greater than 0", "id");
         }

         // Download the xml document with the episode data for the given show ID
         string address = String.Format("{0}/api/{1}/series/{2}/all/en.xml", mirror, apiKey, id);
         string xml;
         using (WebClient client = new WebClient())
         {
            xml = client.DownloadString(address);
         }

         // Make sure the downloaded data is valid.
         if (String.IsNullOrEmpty(xml))
         {
            throw new WebException(String.Format("Nothing was returned by the query '{0}'", address));
         }

         try
         {
            XDocument document = XDocument.Parse(xml);
            List<Episode> episodes = new List<Episode>();

            foreach (XElement episode in document.Descendants("Episode"))
            {
               int episodeId = (episode.HasElement(EpisodeFields.Id)) ?
                               int.Parse(episode.Element(EpisodeFields.Id).Value) :
                               EpisodeFields.DefaultId;

               string director = (episode.HasElement(EpisodeFields.Director)) ?
                                 episode.Element(EpisodeFields.Director).Value :
                                 EpisodeFields.DefaultDirector;

               string title = (episode.HasElement(EpisodeFields.Title)) ?
                                 episode.Element(EpisodeFields.Title).Value :
                                 EpisodeFields.DefaultTitle;

               int number = (episode.HasElement(EpisodeFields.Number)) ?
                            int.Parse(episode.Element(EpisodeFields.Number).Value) :
                            EpisodeFields.DefaultNumber;

               int season = (episode.HasElement(EpisodeFields.Season)) ?
                            int.Parse(episode.Element(EpisodeFields.Season).Value) :
                            EpisodeFields.DefaultSeason;

               string description = (episode.HasElement(EpisodeFields.Description)) ?
                                    episode.Element(EpisodeFields.Description).Value :
                                    EpisodeFields.DefaultDescription;

               Episode newEpisode = new Episode(episodeId, director, title, number, season, description);

               episodes.Add(newEpisode);
            }
            return episodes;
         }
         catch (Exception ex)
         {
            throw new InvalidOperationException("Could not parse the episode data.", ex);
         }
      }
   }
}