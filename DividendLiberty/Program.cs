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
            PleaseWait pw = new PleaseWait();
            pw.lblMsg.Text = "Connecting to yahoo for stock data..";
            pw.lblMsg.Visible = true;
            pw.Show();
            Application.DoEvents();
            Application.Run(MainMenu = new MainMenu());
            pw.Close();
        }
    }
}
