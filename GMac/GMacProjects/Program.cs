using System;
using System.IO;
using GMacProjects.TextComposers;

namespace GMacProjects
{
    class Program
    {
        static void Main(string[] args)
        {
            var tableText = ProductTablesComposer.ComposeHga4D();
            File.WriteAllText("table.txt", tableText);
            Console.Out.Write(tableText);

            Console.Out.WriteLine();
            Console.Out.WriteLine("Press Enter to Exit...");
            Console.In.ReadLine();
        }
    }
}
