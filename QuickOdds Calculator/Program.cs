using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace QuickOdds_Calculator
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
            bool singleInstance;
            System.Threading.Mutex m = new System.Threading.Mutex(true, "QuickOddsCalculator", out singleInstance);
            if (!singleInstance)
            {
                System.Windows.Forms.MessageBox.Show("There is already another instance of the application running", "Error");
                return;
            }
            
            Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());

            GC.KeepAlive(m);
		}
	}
}