using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using GMacBenchmarks.Benchmarks;
using GMacBenchmarks.Benchmarks.Numeric;
using GMacBenchmarks.Benchmarks.Symbolic;
using SymbolicInterface.Mathematica.Expression;

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
