using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace TVShowRename
{
    class SavedTVShowsController
    {
        #region Instance Variables
        /// <summary>
        /// Name of the file which holds the saved tv show ID's
        /// </summary>
        private String ivsSavedTVShowsFilename;

        /// <summary>
        /// XML Document
        /// </summary>
        private XmlDocument ivXmlDocument;
        #endregion


        public SavedTVShowsController()
        {
            ivXmlDocument = new XmlDocument();
            ivsSavedTVShowsFilename = "SavedTVShows.xml";
        }

        /// <summary>
        /// Saves a new tvshow (and its ID) to the SavedTVShows file
        /// for faster processing.
        /// </summary>
        /// <param name="fvsShowName">The full TV show name as it appears in the TVDB</param>
        /// <param name="fvsPreferredShowName">The preferred TV Show name as it appears in the file to be renamed</param>
        /// <param name="ID">The shows ID</param>
        public void saveNewTVShow(String fvsShowName, String fvsPreferredShowName, int ID)
        {
            // Try to load the saved TV shows file
            try
            {
                ivXmlDocument.Load(ivsSavedTVShowsFilename);
            }
            // If the file doesnt exists, create a new one
            catch (System.IO.FileNotFoundException)
            {
                ivXmlDocument.AppendChild(ivXmlDocument.CreateXmlDeclaration("1.0", "utf-8", null));
                ivXmlDocument.AppendChild(ivXmlDocument.CreateElement("root"));
                ivXmlDocument.Save(ivsSavedTVShowsFilename);
            }
            // Create the new tvShow element
            XmlNode tvShowNode = createNodeWithInnerText("TVShow", null);
            tvShowNode.AppendChild( createNodeWithInnerText("Name", fvsShowName) );
            tvShowNode.AppendChild( createNodeWithInnerText("PreferredName", fvsPreferredShowName) );
            tvShowNode.AppendChild( createNodeWithInnerText("ID", ID.ToString()) );
            ivXmlDocument.DocumentElement.AppendChild(tvShowNode);
            // Save the new data
            ivXmlDocument.Save(ivsSavedTVShowsFilename);
        }

        /// <summary>
        /// Creates an XML Node element with a given tag name and inner text
        /// </summary>
        /// <param name="fvsNodeName">The name of the xml node</param>
        /// <param name="fvsNodeInnerText">The inner text of the xml node</param>
        /// <returns>Returns the XmlNode</returns>
        private XmlNode createNodeWithInnerText(String fvsNodeName, String fvsNodeInnerText)
        {
            XmlNode returnNode = ivXmlDocument.CreateNode(XmlNodeType.Element, fvsNodeName, null);
            returnNode.InnerText = fvsNodeInnerText;
            return returnNode;
        }

        /// <summary>
        /// Checks the SavedTVShows file to see if the given show has already been saved.
        /// If so, returns the show ID.
        /// </summary>
        /// <param name="fvsShowName">Show name to find</param>
        /// <returns>Returns the show ID if it is found, -1 if the file doesn't exist, 
        /// and 0 if the show has not been saved before.</returns>
        public int getShowID(String fvsShowName)
        {
            // Try to load the saved TV shows file
            try
            {
                ivXmlDocument.Load(ivsSavedTVShowsFilename);
            }
            // If the file doesn't exist, return -1 (error)
            catch (System.IO.FileNotFoundException)
            {
                return -1;
            }

            XmlNodeList tvShowNodesList = ivXmlDocument.SelectNodes("/root/TVShow");
            
            // Loop through all existing saved TV Shows to check if the ID has previously been saved
            foreach (XmlNode existingNode in tvShowNodesList)
            {
                if (fvsShowName == existingNode["PreferredName"].InnerText)
                {
                    return int.Parse(existingNode["ID"].InnerText);
                }
            }
            return 0;
        }
    }
}
