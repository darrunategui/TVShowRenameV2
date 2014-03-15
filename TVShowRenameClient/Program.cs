using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TVShowRename
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // Check if there is another instance of the application running.
            Process process;
            if (SingletonController.TryGetPreviousInstance(Process.GetCurrentProcess(), out process))
            {
                // Send the arguments the the currently running instance.
                SingletonController.SendArguments(args);
                // Return to end the process.
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            RenameController control = new RenameController();
            SingletonController.RegisterReceiveEvent(control.MainForm.Recieve);
            Application.Run(control.MainForm);
        }
    }
}
