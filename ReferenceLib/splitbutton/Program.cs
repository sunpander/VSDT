using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace demo
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
            Form1 frm = new Form1();
           // frm.Visible = true;
            frm.Show();
            Application.Run(new Form1());
		}
	}
}