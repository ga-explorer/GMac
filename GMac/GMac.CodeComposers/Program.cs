using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMac.CodeComposers.TextComposers;

namespace GMac.CodeComposers
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());

            var tableText = ProductTablesComposer.ComposeHga4D();
            File.WriteAllText("table.txt", tableText);
            Console.Out.Write(tableText);

            Console.Out.WriteLine();
            Console.Out.WriteLine("Press Enter to Exit...");
            Console.In.ReadLine();
        }
    }
}
