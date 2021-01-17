using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TextComposerLib.UserInterface;
using TextComposerLibSamples.Samples;

namespace TextComposerLibSamples
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

            var tasks = SamplesFactory.CreateSamples();

            Application.Run(new FormSamples(tasks));
        }
    }
}
