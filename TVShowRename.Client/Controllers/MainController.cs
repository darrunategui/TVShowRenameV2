using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using TVShowRename.Business.Contracts;
using TVShowRename.Business.Entities;

namespace TVShowRename.Client
{
    public class MainController : Controller
    {
        private const string TitleGroup = "Title";
        private const string InfoGroup = "Info";

        private IMainView _view;

        private Regex _showRegex = new Regex(@"(?<" + TitleGroup + @">.*?)\.(?<" + InfoGroup + @">[s]\d{2}[e]\d{2})(.*)", 
                                            RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private string _currentfile;

        public MainController(IMainView view)
        {
            _view = view;
            view.SetController(this);
        }


        public void Rename(string[] filesToRename)
        {
            if ( filesToRename == null || filesToRename.Length == 0)
            {
                throw new ArgumentException("No files to rename");
            }

            // Some of the input files may not be in the correct format to parse
            // TODO: To allow more input formats, consider creating a parsing interface to check file input formats
            List<string> invalidFiles = new List<string>();
            foreach (string file in filesToRename)
            {
                // Check which files match
                if (!_showRegex.IsMatch(file))
                {
                    invalidFiles.Add(file);
                }
            }

            if (invalidFiles.Count > 0)
            {
                StringBuilder sb = new StringBuilder(String.Empty);
                foreach (string invalidFile in invalidFiles)
                {
                    sb.Append(String.Format("- {0}{1}", invalidFile, Environment.NewLine));
                }

                // Let the user know that some files will not be renamed.
                MessageBox.Show(String.Format("The following files will not be renamed{0}{1}", Environment.NewLine, sb.ToString()),
                                "Input format error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            foreach (string file in filesToRename.Where(input => _showRegex.IsMatch(input)))
            {
                _currentfile = file;
                Match match = _showRegex.Match(file);

                string showTitle = Path.GetFileName(match.Groups[TitleGroup].Value).Replace('.', ' ');
                string Info = match.Groups[InfoGroup].Value;
                string extension = Path.GetExtension(file);

                ITVDBService tvdbManager = _serviceFactory.GetServiceManager<ITVDBService>();
                try
                {
                    IEnumerable<Show> results = tvdbManager.GetShowsByTitle(showTitle);
                    _view.AddShowResults(results);
                }
                catch
                {
                    continue;
                }
            }

        }

        private void Rename(int showId)
        {
            
        }

    }
}
