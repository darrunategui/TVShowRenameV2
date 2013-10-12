/*
 * This file is part of TVDBDLL.
 *
 *  TVDBDLL is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  TVDBDLL is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with TVDBDLL.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TVDBDLL
{
    /// <summary>
    /// Class that represents one result fetched from the site
    /// </summary>
    public class ShowResultItem
    {
        /// <summary>
        /// id of the serie
        /// </summary>
        private int id;

        /// <summary>
        /// Show name
        /// </summary>
        private String showName;

        /// <summary>
        /// Overview of the serie
        /// </summary>
        private String overview;

        /// <summary>
        /// Language of the serie's info
        /// </summary>
        private String language;

        /// <summary>
        /// Main banner of the serie
        /// </summary>
        private String banner;

        /// <summary>
        /// IMDB ID of the serie
        /// </summary>
        private String ImdbId;

        /// <summary>
        /// Network of the serie
        /// </summary>
        private String network;

        /// <summary>
        /// First aired date of the serie
        /// </summary>
        private String firstAired;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="sN">name</param>
        /// <param name="l">language</param>
        /// <param name="o">overview</param>
        /// <param name="b">banner</param>
        /// <param name="imdb">imdb id</param>
        public ShowResultItem(int id, String sN, String l, String o, String b, String imdb, String network, String firstAired)
        {
            this.id = id;
            this.showName = sN;
            this.language = l;
            this.overview = o;
            this.banner = b;
            this.ImdbId = imdb;
            this.network = network;
            this.firstAired = firstAired;
        }

        /// <summary>
        /// getter for id
        /// </summary>
        public int ID
        {
            get { return id; }
        }

        /// <summary>
        /// getter for imdb id
        /// </summary>
        public String IMDB_ID
        {
            get { return ImdbId; }
        }

        /// <summary>
        /// getter for main banner
        /// </summary>
        public String Banner
        {
            get { return banner; }
        }

        /// <summary>
        /// getter for Show Name
        /// </summary>
        public String ShowName
        {
            get { return showName; }
        }

        /// <summary>
        /// getter for language
        /// </summary>
        public String Language
        {
            get { return language; }
        }

        /// <summary>
        /// getter for overview
        /// </summary>
        public String Overview
        {
            get { return overview; }
        }

        /// <summary>
        /// getter for network
        /// </summary>
        public String Network
        {
            get { return network; }
        }

        /// <summary>
        /// getter for firstAired
        /// </summary>
        public String FirstAired
        {
            get { return firstAired; }
        }
    }
}
