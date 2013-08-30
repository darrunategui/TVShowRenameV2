using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TVShowRename
{
    public partial class MainForm : Form
    {
        #region Instance variables
        
        private RenameController ivRenameControl;
        #endregion

        public MainForm()
        {
            InitializeComponent();
            ivRenameControl = new RenameController(this);
        }

        private void showDraggedOnForm(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
                dropImage.BackgroundImage = Properties.Resources.DropImageDarkened;
            }
        }

        private void showDraggedOffForm(object sender, EventArgs e)
        {
            dropImage.BackgroundImage = Properties.Resources.DropImage;
        }

        private void showDroppedOnForm(object sender, DragEventArgs e)
        {
            String[] tvsDroppedFiles = (String[])e.Data.GetData(DataFormats.FileDrop);
            ivRenameControl.renameTVShows(tvsDroppedFiles);
        }

        private void showWasSelected(object sender, EventArgs e)
        {
            ivRenameControl.showWasSelected();
        }

        

    }
}
