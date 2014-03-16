using Core.Common.Core;
using Core.Common.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TVShowRename.Business.Bootstrapper;

namespace TVShowRename.Client
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            ObjectBase.Container = MEFLoader.Initialize();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            MainForm form = new MainForm();
            MainController controller = new MainController(form);
            using (SingletonController singletonController = new SingletonController("TVShowRename", form))
            {
                if ( singletonController.IsFirstInstance )
                {
                    Application.Run((MainForm)singletonController.Enforcer);
                }
                else
                {
                    singletonController.SendMessageToFirstInstance(args);
                }
            }
        }
    }
}
