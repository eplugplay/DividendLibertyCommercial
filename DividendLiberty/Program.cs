using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DividendLiberty
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        public static MainMenu MainMenu;
        [STAThread]
        static void Main()
        {
            bool ok;
            var m = new System.Threading.Mutex(true, "DividendDreams", out ok);

            if (!ok)
            {
                MessageBox.Show("Another instance is already running.");
                return;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(MainMenu = new MainMenu());
        }
    }
}
