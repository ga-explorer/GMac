using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using GMacTests.Numeric;
using GMacTests.Symbolic;
using GMacTests.Validations;

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
