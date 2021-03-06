﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TVShowRename
{
    public partial class MainForm : Form
    {
        #region Instance variables
        private RenameController ivRenameControl;
        #endregion

        public MainForm(RenameController control)
        {
            InitializeComponent();
            this.Size = new Size(409, 410);
            ivRenameControl = control;
            setOutputFormatText();
            MaximizeBox = false;
        }

        #region drag-n-drop events
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
            saveOutputFormat();
        }
        #endregion drag-n-drop

        private void showWasSelected(object sender, EventArgs e)
        {
            ivRenameControl.showWasSelected();
        }

        private void formClosing(object sender, FormClosingEventArgs e)
        {
            successNotification.Icon = null;
            successNotification.Visible = false;
            successNotification.Dispose();
        }

        private void advancedOptionsButtonClick(object sender, EventArgs e)
        {
            int tviHeightDifference = 45;
            if (advancedOptionsButton.Text == "▼")
            {
                int tviNewHeight = this.Size.Height + tviHeightDifference;
                for (int i = this.Size.Height; i < tviNewHeight; ++i)
                {
                    this.Size = new Size(this.Size.Width, i);
                    wait(2000000);
                }
                advancedOptionsButton.Text = "▲";
            }
            else
            {
                int tviNewHeight = this.Size.Height - tviHeightDifference;
                for (int i = this.Size.Height; i > tviNewHeight; --i)
                {
                    this.Size = new Size(this.Size.Width, i);
                    wait(2000000);
                }
                advancedOptionsButton.Text = "▼";
            }
            toggleAdvancedOptions();
        }

        private void toggleAdvancedOptions()
        {
            outputTemplateLabel.Visible = !outputTemplateLabel.Visible;
            outputTemplateTextBox.Visible = !outputTemplateTextBox.Visible;
        }

        private void setOutputFormatText()
        {
            OutputFormatController outputFormat = new OutputFormatController();
            outputTemplateTextBox.Text = outputFormat.getSavedOutputFormat();
            if (outputTemplateTextBox.Text == String.Empty)
            {
                outputTemplateTextBox.Text = "{TVShow} S{S}E{E} - {Title}";
            }
        }

        private void saveOutputFormat()
        {
            OutputFormatController outputFormat = new OutputFormatController();
            outputFormat.saveOutputFormat(outputTemplateTextBox.Text);
        }

        private void wait(int interval)
        {
            Thread.Sleep(2);
        }

        public void Recieve(string[] args)
        {
            Invoke(new Action(() =>
            {
                // Restore the window if it was minimized.
                if (WindowState == FormWindowState.Minimized)
                {
                    WindowState = FormWindowState.Normal;
                    Location = new Point((RestoreBounds.X < 0) ? 0 : RestoreBounds.X, (RestoreBounds.Y < 0) ? 0 : RestoreBounds.Y);
                }

                // Bring the window to the foreground.
                NativeMethods.SetForegroundWindow(Handle);
            }));
        }
    }
}
