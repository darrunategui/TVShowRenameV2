using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TVShowRename
{
    class OutputFormatController
    {
        #region Instance Variables
        /// <summary>
        /// Name of the file which holds the saved tv show ID's
        /// </summary>
        private String ivsOutputFormatFilename;

        /// <summary>
        /// XML Document
        /// </summary>
        private XmlDocument ivXmlDocument;
        #endregion


        public OutputFormatController()
        {
            ivXmlDocument = new XmlDocument();
            ivsOutputFormatFilename = "OutputFormat.xml";
        }

        public String getSavedOutputFormat()
        {
            // Try to load the saved output format file
            try
            {
                ivXmlDocument.Load(ivsOutputFormatFilename);
            }
            // If the file doesn't exist, return empty string
            catch (System.IO.FileNotFoundException)
            {
                return String.Empty;
            }

            XmlNode outputFormatNode = ivXmlDocument.SelectSingleNode("/root/OutputFormat");
            return outputFormatNode.InnerText;
        }
        public void saveOutputFormat(String fvsOutputFormat)
        {
            // Try to load the saved TV shows file
            try
            {
                ivXmlDocument.Load(ivsOutputFormatFilename);
            }
            // If the file doesnt exists, create a new one
            catch (System.IO.FileNotFoundException)
            {
                ivXmlDocument.AppendChild(ivXmlDocument.CreateXmlDeclaration("1.0", "utf-8", null));
                ivXmlDocument.AppendChild(ivXmlDocument.CreateElement("root"));
                // Create the new output format element
                XmlNode tvShowNode = createNodeWithInnerText("OutputFormat", fvsOutputFormat);
                ivXmlDocument.DocumentElement.AppendChild(tvShowNode);
                // Save the new data
                ivXmlDocument.Save(ivsOutputFormatFilename);
                return;
            }

            // Create the new output format element
            XmlNode outputFormatNode = ivXmlDocument.SelectSingleNode("/root/OutputFormat");
            outputFormatNode.InnerText = fvsOutputFormat;
            // Save the new data
            ivXmlDocument.Save(ivsOutputFormatFilename);
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
    }
}
