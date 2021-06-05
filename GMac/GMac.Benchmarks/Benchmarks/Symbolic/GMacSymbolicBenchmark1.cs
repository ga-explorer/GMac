using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using GeometricAlgebraNumericsLib;
using GeometricAlgebraSymbolicsLib;
using GeometricAlgebraSymbolicsLib.Frames;
using GeometricAlgebraSymbolicsLib.Maps;
using GeometricAlgebraSymbolicsLib.Maps.Bilinear;
using GeometricAlgebraSymbolicsLib.Multivectors;
using GeometricAlgebraSymbolicsLib.Products;

namespace GMac.Benchmarks.Benchmarks.Symbolic
{
    public class GMacSymbolicBenchmark1
    {
        private readonly GaRandomGenerator _randGen 
            = new GaRandomGenerator(10);

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
                _multivectors.Add(_randGen.GetSymMultivector(_frame.VSpaceDimension));
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
