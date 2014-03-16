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

namespace TVShowRename.Client
{
    public partial class MainForm : Form, ISingletonEnforcer
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public void OnMessageReceived(string[] args)
        {
            // Do nothing for now...
        }
    }
}
