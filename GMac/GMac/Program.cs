using System;
using System.Windows.Forms;
using GMac.IDE;
using GMac.IDE.Editor;

namespace GMac
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

            //Load GMacIDE form
            Application.Run(new FormGMacSplash());
            Application.Run(new FormGMacDslEditor());

            //Application.Run(new FormSamples(SamplesFactory.CreateSamples()));
        }
    }
}
