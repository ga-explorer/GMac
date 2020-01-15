using BenchmarkDotNet.Attributes;
using GeometricAlgebraNumericsLib;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Multivectors;
using GeometricAlgebraNumericsLib.Products;

namespace GMacBenchmarks2.Benchmarks.Numeric
{
    /// <summary>
    /// Benchmark implementation methods of standard products
    /// </summary>
    public class Benchmark1
    {
        private GaRandomGenerator _randGen;
        private GaNumFrame _frame;
        private GaNumMultivector _mv1;
        private GaNumMultivector _mv2;

        //[Params(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15)]
        public int Grade1 { get; set; }
            = 5;

        //[Params(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15)]
        public int Grade2 { get; set; }
            = 5;

        [Params(
            GaBilinearProductImplementation.Computed,
            GaBilinearProductImplementation.LookupArray,
            GaBilinearProductImplementation.LookupCoefSums,
            GaBilinearProductImplementation.LookupHash,
            GaBilinearProductImplementation.LookupTree
        )]
        public GaBilinearProductImplementation ProductsImplementation { get; set; } 
            //= GaBilinearProductImplementation.Computed;

        [GlobalSetup]
        public void Setup()
        {
            _randGen = new GaRandomGenerator(10);

            _frame = GaNumFrame.CreateConformal(10);
            _frame.SetProductsImplementation(ProductsImplementation);

            _mv1 = _randGen.GetNumKVector(_frame.GaSpaceDimension, Grade1);
            _mv2 = _randGen.GetNumKVector(_frame.GaSpaceDimension, Grade2);
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

        [Benchmark]
        public GaNumMultivector Lcp()
        {
            return _frame.Lcp[_mv1, _mv2];
        }

        [Benchmark]
        public GaNumMultivector Sp()
        {
            return _frame.Sp[_mv1, _mv2];
        }
    }
}
