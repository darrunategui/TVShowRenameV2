using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TVDBDLL
{
    /// <summary>
    /// Class that represents one episode and its necessary data
    /// </summary>
    public class EpisodeResultItem
    {
        /// <summary>
        /// Title of the episode
        /// </summary>
        private String title;

        /// <summary>
        /// The episode number within its season
        /// </summary>
        private int episodeNumber;

        /// <summary>
        /// The season the episode is in
        /// </summary>
        private int seasonNumber;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="title">title of the episode</param>
        /// <param name="episodeNumber">episode number within the season</param>
        /// <param name="seasonNumber">the season number that the episode is in</param>
        public EpisodeResultItem(String title, int episodeNumber, int seasonNumber)
        {
            this.title = title;
            this.episodeNumber = episodeNumber;
            this.seasonNumber = seasonNumber;
        }

        /// <summary>
        /// getter for title
        /// </summary>
        public String Title
        {
            get { return title; }
        }

        /// <summary>
        /// getter for episodeNumber
        /// </summary>
        public int EpisodeNumber
        {
            get { return episodeNumber; }
        }

        /// <summary>
        /// getter for seasonNumber
        /// </summary>
        public int SeasonNumber
        {
            get { return seasonNumber; }
        }
    }
}
