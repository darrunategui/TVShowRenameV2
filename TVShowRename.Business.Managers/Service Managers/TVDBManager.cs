using Core.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TVShowRename.Business.Contracts;
using TVShowRename.Business.Entities;
using TVShowRename.Common;
using TVShowRename.Common.Data;

namespace TVShowRename.Business.Managers
{
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

        public IEnumerable<Show> GetShowsByTitle(string title)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Episode> GetEpisodesByShowId(int id)
        {
            // Argument check
            if ( id <= 0 )
            {
                throw new ArgumentException("The show ID must be greater than 0");
            }

            // Download the xml document with the episode data for the given show ID
            string address = String.Format("{0}/api/{1}/series/{2}/all/en.xml", mirror, apiKey, id);
            WebClient client = new WebClient();
            string xml = client.DownloadString(address);

            // Make sure the downloaded data is valid.
            if (String.IsNullOrEmpty(xml))
            {
                throw new ShowNotFoundException(String.Format("The show ID '{0}' did not result in any matches", id));
            }

            try
            {
                XDocument document = XDocument.Parse(xml);
                List<Episode> episodes = new List<Episode>();

                foreach (XElement episode in document.Elements("Episode"))
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
            catch(Exception ex)
            {
                throw new InvalidOperationException("Could not parse the episodes", ex);
            }
        }
    }
}
