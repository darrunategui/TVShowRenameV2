using Core.Common.Singleton;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TVShowRename.Business.Entities;

namespace TVShowRename.Client
{
    public partial class MainForm : Form, IMainView, ISingletonEnforcer
    {
        private MainController _controller;

        public MainForm()
        {
            InitializeComponent();
        }

        #region IMainView methods
        public void SetController(Controller controller)
        {
            _controller = (MainController)controller;
        }

        public void AddShowResults(IEnumerable<Show> shows)
        {
        }
        #endregion

        #region ISingletonEnforcer methods
        public void OnMessageReceived(string[] args)
        {
            // Do nothing for now...
        }
        #endregion

    }
}
