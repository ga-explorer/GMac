using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Running;

namespace GMacBenchmarks2
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Benchmarking Code
            //var summary = BenchmarkRunner.Run<Benchmarks.Numeric.Benchmark9>();
            //var sample = new Benchmarks.Numeric.Benchmark9 {VSpaceDim = 12};
            //sample.Setup();
            //var result = sample.Validate();

            //var fileName = DateTime.Now.ToString("s").Replace(":", "-");
            //var filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName + ".log");
            //File.WriteAllText(filePath, result);

            //Console.WriteLine(result);

            //Console.WriteLine();
            //Console.Write(@"Press any key to continue...");
            //Console.ReadKey();
            #endregion

            #region Samples Code
            //var sample = new Samples.Rendering.Numeric.Sample1();
            var sample = new Samples.Memory.Numeric.Sample3();
            //var sample = new Samples.Validations.GaUnilinearMapsValidation();
            //var sample = new Samples.Generators.VectorKVectorOpGenerator();
            //var sample = new Samples.Generators.VectorKVectorOpIndexTablesGenerator();
            var result = sample.Execute();

            var fileName = DateTime.Now.ToString("s").Replace(":", "-");
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName + ".log");
            File.WriteAllText(filePath, result);

            Console.WriteLine(result);

            Console.WriteLine();
            Console.Write(@"Press any key to continue...");
            Console.ReadKey();
            #endregion
        }
    }
}
