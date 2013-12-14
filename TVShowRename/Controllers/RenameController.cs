using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVDBDLL;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;

namespace TVShowRename
{
    public class RenameController
    {
        #region Instance Variables
        /// Delegate to call the errorHandler.
        public delegate void errorCall(Exception exc);
        /// Event of errorCall.
        private event errorCall formErrorCaller;
        /// Object that holds reference to tvdb connection.
        private TVDB _tvDbHandler;
        /// <summary>
        /// Regex to match the common format of downloaded tv show files
        /// </summary>
        private Regex _inputFilenameRegex = new Regex("[\\w|.|\\s]+\\.[s]\\d{2}[e]\\d{2}[\\w*|.|\\s]+", RegexOptions.IgnoreCase);
        /// <summary>
        /// Regex to match the S00E00 part of the downloaded tv show file
        /// </summary>
        private Regex _episodeInfoRegex = new Regex("[s]\\d{2}[e]\\d{2}", RegexOptions.IgnoreCase);
        /// <summary>
        /// Name of the tvShow
        /// </summary>
        private String _TVShowName;
        /// <summary>
        /// Episode and season info
        /// </summary>
        private String _episodeInfo;
        /// <summary>
        /// The title of the episode
        /// </summary>
        private String _episodeTitle;
        /// <summary>
        /// Full path to the input file
        /// </summary>
        private String _absolutePathWithFile;
        /// <summary>
        /// The file extension of the input file
        /// </summary>
        private String _fileExtension;
        /// <summary>
        /// Determines if the application should close directly after renaming a file
        /// </summary>
        private Boolean _shouldCloseAfterRename = false;
        /// <summary>
        /// Reference to the mainForm
        /// </summary>
        public MainForm MainForm { get; set; }
        /// <summary>
        /// Handles saving and getting TV Show ID's
        /// </summary>
        private SavedTVShowsController _savedTVShowControl;
        #endregion

        public RenameController()
        {
            formErrorCaller += new errorCall(errorHandler);
            _savedTVShowControl = new SavedTVShowsController();
            MainForm = new MainForm(this);
            prepareForNextEpisode();
            enableControlVisibility(false);
        }

        #region Renaming Methods

        /// <summary>
        /// Extracts the necessary information from the input files.
        /// If the input TV Show's ID is saved, then the renaming is initiated automatically.
        /// If not, the Show Possibilities grid is populated with the possible results.
        /// </summary>
        /// <param name="filesToRename">The list of files the rename.</param>
        public void renameTVShows(String[] filesToRename)
        {
            // Each String 'file' will contain the absolute path to the file including the filename
            foreach (String file in filesToRename)
            {
                _absolutePathWithFile = file;
                // If the input file matches that of the usual downloaded filename type, proceed
                if (_inputFilenameRegex.IsMatch(file))
                {
                    // Get only the name of the file with the extension
                    String fileName = Path.GetFileName(file);

                    // Split the file name into tokens seperated by '.'
                    // This way the tv Show name, season/episode Identifier, and file extension can be isolated
                    String[] tokens = fileName.Split('.');

                    // The last token will be the file extension
                    _fileExtension = Path.GetExtension(file);

                    // Build the formatted tvShow name
                    foreach (string token in tokens)
                    {
                        //if the token is the S01E01 part, ensure it is all uppercase
                        if (_episodeInfoRegex.IsMatch(token))
                        {
                            _episodeInfo += token.ToUpper();
                            break; // break since the rest of the filename is not important
                        }
                        else
                        {
                            _TVShowName += CapitalizeFirstLetter(token) + " "; // Assumes the tokens start with the name of the tv show
                        }
                    }
                    _TVShowName = _TVShowName.Substring(0, _TVShowName.Length - 1); // remove the extra space
                    
                    // At this point, 
                    //  ivTVShowName holds the full name of the TV Show 
                    //  ivEpisodeInfo holds the season/episode identifier

                    int tvShowID = _savedTVShowControl.getShowID(_TVShowName); // attempt to get the tvShows ID
                    if (tvShowID > 0) // Positive ShowID means it has been saved before and is valid
                    {
                        renameShow(tvShowID);
                    }
                    else // TVShow ID is not saved, so a query must be made to determine the ID
                    {
                        // Populate the showPossibilities table shows matching the given TV show name
                        populateShowPossibilitiesList(_TVShowName);
                    }
                }
            }
        }

        /// <summary>
        /// Handles the situation when the show's ID is not saved.
        /// Populates the show possibilites grid with the possible show results so the user can choose the tv show they intended.
        /// If only one tv show is found with the given name, it is automatically selected and renamed.
        /// </summary>
        /// <param name="fvTVShowName">The TV Shows name</param>
        public void populateShowPossibilitiesList(String fvTVShowName)
        {
            _tvDbHandler = new TVDB();
            _tvDbHandler.parentErrorCaller = formErrorCaller;
            List<ShowResultItem> results = _tvDbHandler.searchShowByName(fvTVShowName);
            if (results.Count == 0) // no results, so return
            {
                MessageBox.Show("No TV Show by the name '" + fvTVShowName + "' was found.");
                return;
            }
            // Only 1 tv show resulted from the given name query, so save the ID and choose this tvShow automatically
            else if (results.Count == 1)
            {
                _savedTVShowControl.saveNewTVShow(fvTVShowName, _TVShowName, results[0].ID);
                renameShow(results[0].ID);
            }
            // Multiple tvShows resulted from the given name query, so populate the listView
            // so the user can choose the tvShow they want
            else
            {
                MainForm.showPossibilities.Items.Clear();
                foreach (ShowResultItem result in results)
                {
                    ListViewItem item = new ListViewItem(result.ShowName);
                    item.SubItems.Add(result.Network);
                    item.SubItems.Add(parseFirstAiredValue(result.FirstAired));
                    item.Tag = result;
                    MainForm.showPossibilities.Items.Add(item);
                }
                // Autosize the column widths to the data just added
                foreach (ColumnHeader column in MainForm.showPossibilities.Columns)
                {
                    column.Width = -2;
                }
                enableControlVisibility(true);
                _shouldCloseAfterRename = false;
            }
        }

        /// <summary>
        /// Queries the TV Show database to get the most up to date episode information of a given show
        /// and renames the file.
        /// </summary>
        /// <param name="fvShowID">The input files Show ID.</param>
        private void renameShow(int fvShowID)
        {
            _tvDbHandler = new TVDB();  // TVDB will handle the TV show query
            _tvDbHandler.parentErrorCaller = formErrorCaller;
            Dictionary<String, EpisodeResultItem> episodeResults = _tvDbHandler.getEpisodesFromShowID(fvShowID); // Get all episodes for the given show ID

            if (episodeResults.Count > 0)
            {
                String tvEpisodeKey = "" + getSeasonNumberFrom(_episodeInfo) + getEpisodeNumberFrom(_episodeInfo); // The key to the episode in the dictionary
                
                _episodeTitle = episodeResults[tvEpisodeKey].Title; // Save the title of the episode
                
                char[] illegalChars = { '<', '>', ':', '"', '/', '\\', '|', '?', '*' };
                // Loop through all the illegal filename characters and remove any if present
                foreach (char c in illegalChars)
                {
                    _episodeTitle = _episodeTitle.Replace(c.ToString(), "");
                }

                // Save the absolute path to the new filename so it can be renamed
                String tvAbsolutePathWithFileRenamed = getNewAbsolutePathWithFileRenamed(_absolutePathWithFile.Split('\\'), _TVShowName, _episodeInfo, _episodeTitle, _fileExtension);
                try
                {
                    // Try to rename the file
                    File.Move(_absolutePathWithFile, tvAbsolutePathWithFileRenamed);
                    // Show a success notification
                    MainForm.successNotification.ShowBalloonTip(1, "Success", _TVShowName + " " + _episodeInfo + " has been renamed to '" + _episodeTitle + "'", ToolTipIcon.Info);
                }
                catch (SystemException e)
                {
                    // Catch any exception that might have occured
                    MessageBox.Show("Exception occured while renaming file.\n" + "\n" + e);
                }
            }
            enableControlVisibility(false);
            prepareForNextEpisode();
        }

        /// <summary>
        /// Handles the selection of a TV Show.
        /// Saves the selected show's ID and initiates the renaming process.
        /// </summary>
        public void showWasSelected()
        {
            if (MainForm.showPossibilities.SelectedItems.Count == 1)
            {
                ShowResultItem selectedShow = (ShowResultItem) MainForm.showPossibilities.SelectedItems[0].Tag;
                int showID = selectedShow.ID;
                _savedTVShowControl.saveNewTVShow(selectedShow.ShowName, _TVShowName, showID); // save the showID for next time
                renameShow(showID);
            }
        }

        public void errorHandler(Exception exc)
        {
            MessageBox.Show(exc.StackTrace, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion

        #region Helper Functions

        /// <summary>
        /// Gets the season number from the Season And Episode Identifier.
        /// </summary>
        /// <param name="SeasonAndEpisodeIdentifier">The identifier</param>
        /// <returns>Returns the season number.</returns>
        private int getSeasonNumberFrom(String SeasonAndEpisodeIdentifier)
        {
            return int.Parse(SeasonAndEpisodeIdentifier.Substring(1, 2));
        }

        /// <summary>
        /// Gets the episode number from the Season And Episode Identifier.
        /// </summary>
        /// <param name="SeasonAndEpisodeIdentifier"></param>
        /// <returns>Returns the episode number.</returns>
        private int getEpisodeNumberFrom(String SeasonAndEpisodeIdentifier)
        {
            return int.Parse(SeasonAndEpisodeIdentifier.Substring(4, 2));
        }

        /// <summary>
        /// Returns the the path to the new file, with the file renamed.
        /// </summary>
        /// <param name="absolutPathTokens">The path to the file split along '\'.</param>
        /// <param name="tvShowName">Name of the TV Show.</param>
        /// <param name="episodeInfo">Episode Identifier.</param>
        /// <param name="episodeTitle">Name of the episode.</param>
        /// <param name="fileExtension">The file extension.</param>
        /// <returns>Returns the path to the new file, with the file renamed.</returns>
        private String getNewAbsolutePathWithFileRenamed(String[] absolutPathTokens, String tvShowName, String episodeInfo, String episodeTitle, String fileExtension)
        {
            String fileLocation = "";
            for (int i = 0; i < absolutPathTokens.Length - 1; i++)
            {
                fileLocation += absolutPathTokens[i] + "\\";
            }
            return fileLocation + getFileOutputFormat(tvShowName, episodeInfo, episodeTitle) + "." + fileExtension;
        }

        /// <summary>
        /// returns the renamed file according to the specified output format
        /// </summary>
        private String getFileOutputFormat(String tvShowName, String episodeInfo, String episodeTitle)
        {
            String tvsOutputFormat = MainForm.outputTemplateTextBox.Text;
            if (tvsOutputFormat == String.Empty)
            {
                return tvShowName + " " + episodeInfo + " - " + episodeTitle;
            }
            tvsOutputFormat = tvsOutputFormat.Replace("{TVShow}", tvShowName);
            tvsOutputFormat = tvsOutputFormat.Replace("{S}", episodeInfo.Substring(1, 2));
            tvsOutputFormat = tvsOutputFormat.Replace("{E}", episodeInfo.Substring(4, 2));
            tvsOutputFormat = tvsOutputFormat.Replace("{Title}", episodeTitle);
            return tvsOutputFormat;
        }

        /// <summary>
        /// Attempts to parse a date value as a string.
        /// </summary>
        /// <param name="date">String to parse as date.</param>
        /// <returns>Returns the input string in the long date format if valid.  Returns 'Not Available' if the input is not valid.</returns>
        private String parseFirstAiredValue(String date)
        {
            try
            {
                return DateTime.Parse(date).ToLongDateString();
            }
            catch (Exception)
            {
                return "Not Available";   
            }
        }

        /// <summary>
        /// Capitalizes the first letter of the input string.
        /// </summary>
        /// <param name="s">String to be modified.</param>
        /// <returns>Returns the input string with the first letter capitalized.</returns>
        private String CapitalizeFirstLetter(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        /// <summary>
        /// Hides and shows the various controls depending on state.
        /// </summary>
        /// <param name="controlVisibility">False, shows 'drag TV Show here' controls. True, shows the list of show posibilities.</param>
        private void enableControlVisibility(Boolean controlVisibility)
        {
            MainForm.showPossibilities.Visible = controlVisibility;
            MainForm.showPossibilitiesLabel.Visible = controlVisibility;
            MainForm.dragTVShowLabel.Visible = !controlVisibility;
            MainForm.dropImage.Visible = !controlVisibility;
            MainForm.advancedOptionsButton.Visible = !controlVisibility;
            MainForm.advancedOptionsLabel.Visible = !controlVisibility;
        }

        /// <summary>
        /// Clears saved episode information so next episode can be renamed.
        /// </summary>
        private void prepareForNextEpisode()
        {
            _TVShowName = "";
            _episodeInfo = "";
            _episodeTitle = "";
            MainForm.dropImage.BackgroundImage = Properties.Resources.DropImage;
        }
        #endregion
    }
}
