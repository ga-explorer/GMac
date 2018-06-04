using BenchmarkDotNet.Attributes;
using GeometricAlgebraNumericsLib;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Multivectors;
using GeometricAlgebraNumericsLib.Products;

namespace GMacBenchmarks.Benchmarks.Numeric
{
    public class GMacNumericBenchmark1
    {
        private GaRandomGenerator _randGen;
        private GaNumFrame _frame;
        private GaNumMultivector _mv1;
        private GaNumMultivector _mv2;

        //[Params(0, 1, 2, 3)]
        public int Grade { get; set; }
        = 2;

        [Params(
            GaBilinearProductImplementation.Computed,
            GaBilinearProductImplementation.LookupArray,
            GaBilinearProductImplementation.LookupCoefSums,
            GaBilinearProductImplementation.LookupHash,
            GaBilinearProductImplementation.LookupTree
        )]
        public GaBilinearProductImplementation ProductsImplementation { get; set; } 
            //= GaBilinearProductImplementation.LookupCoefSums;

        [GlobalSetup]
        public void Setup()
        {
            _randGen = new GaRandomGenerator(10);

            _frame = GaNumFrame.CreateConformal(5);
            _frame.SetProductsImplementation(ProductsImplementation);

            _mv1 = _randGen.GetNumKVector(_frame.GaSpaceDimension, Grade);
            _mv2 = _randGen.GetNumKVector(_frame.GaSpaceDimension, Grade);
        }

        [Benchmark]
        public GaNumMultivector Gp()
        {
            return _frame.Gp[_mv1, _mv2];
        }

        [Benchmark]
        public GaNumMultivector Op()
        {
            return _frame.Op[_mv1, _mv2];
        }

        //[Benchmark]
        public GaNumMultivector Lcp()
        {
            return _frame.Lcp[_mv1, _mv2];
        }

        //[Benchmark]
        public GaNumMultivector Sp()
        {
            return _frame.Sp[_mv1, _mv2];
        }
    }
}
