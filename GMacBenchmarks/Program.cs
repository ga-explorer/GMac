using BenchmarkDotNet.Running;
using GMacBenchmarks.Benchmarks.Numeric;

namespace GMacBenchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<GMacNumericBenchmark1>();
            //var b = new GMacNumericBenchmark1();
            //b.Setup();
            //b.Gp();
        }
    }
}
