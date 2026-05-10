using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCP_Server
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.ThreadException += (sender, e) => {
                Console.WriteLine($"Thread Exception: {e.Exception}");
            };
            AppDomain.CurrentDomain.UnhandledException += (sender, e) => {
                Console.WriteLine($"Unhandled Exception: {e.ExceptionObject}");
            };

            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Critical Error: {ex}");
            }
        }
    }
}