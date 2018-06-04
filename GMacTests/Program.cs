using System;
using System.IO;
using GMacTests.Numeric;

namespace GMacTests
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = new GMacNumericTest3().Execute();

            var fileName = DateTime.Now.ToString("s").Replace(":", "-");
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName + ".log");
            File.WriteAllText(filePath, result);

            Console.Out.WriteLine(result);
            Console.Out.WriteLine();
            Console.Out.Write("Press 'Enter' to continue...");
            Console.In.Read();
        }
    }
}
