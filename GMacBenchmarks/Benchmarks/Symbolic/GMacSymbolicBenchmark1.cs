using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using GMac.GMacMath;
using GMac.GMacMath.Symbolic.Frames;
using GMac.GMacMath.Symbolic.Maps;
using GMac.GMacMath.Symbolic.Maps.Bilinear;
using GMac.GMacMath.Symbolic.Multivectors;
using GMac.GMacMath.Symbolic.Products;

namespace GMacBenchmarks.Benchmarks.Symbolic
{
    public class GMacSymbolicBenchmark1
    {
        private readonly GMacRandomGenerator _randGen 
            = new GMacRandomGenerator(10);

        private readonly List<GaSymMultivector> _multivectors
            = new List<GaSymMultivector>();

        private GaSymFrame _frame;

        private GaSymMapBilinearHash _lcpLookupTable;


        [GlobalSetup]
        public void Setup()
        {
            _frame = GaSymFrame.CreateConformal(5);
            _lcpLookupTable = _frame.Lcp.ToHashMap();

            for (var i = 0; i < 10; i++)
                _multivectors.Add(_randGen.GetSymMultivector(_frame.GaSpaceDimension));
        }


        [Benchmark]
        public List<GaSymMultivector> ExecuteLcpLookup()
        {
            var result =
                new List<GaSymMultivector>(_multivectors.Count * _multivectors.Count);

            result.AddRange(
                _multivectors.SelectMany(
                    mv1 => _multivectors, 
                    (mv1, mv2) => _lcpLookupTable[mv1, mv2]
                )
            );

            return result;
        }

        [Benchmark]
        public List<GaSymMultivector> ExecuteLcpComputed()
        {
            var result =
                new List<GaSymMultivector>(_multivectors.Count * _multivectors.Count);

            result.AddRange(
                _multivectors.SelectMany(
                    mv1 => _multivectors, 
                    (mv1, mv2) => _frame.Lcp[mv1, mv2]
                )
            );

            return result;
        }

        [Benchmark]
        public List<GaSymMultivector> ExecuteOp()
        {
            var result =
                new List<GaSymMultivector>(_multivectors.Count * _multivectors.Count);

            result.AddRange(
                _multivectors.SelectMany(
                    mv1 => _multivectors, 
                    (mv1, mv2) => mv1.Op(mv2)
                )
            );

            return result;
        }

    }
}
