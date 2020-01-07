using System;
using System.IO;

namespace GMacBenchmarks
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            #region Benchmarking Code
            //var summary = BenchmarkRunner.Run<Benchmarks.Numeric.Benchmark10>();
            //var b = new Benchmarks.Numeric.Benchmark3();
            //b.Setup();
            //b.Validate();
            #endregion

            #region Samples Code
            var sample = new Samples.Rendering.Numeric.Sample1();
            //var sample = new Samples.Memory.Numeric.Sample1();
            //var sample = new Samples.Validations.GaUnilinearMapsValidation();
            //var sample = new Samples.Generators.VectorKVectorOpGenerator();
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
