using BenchmarkDotNet.Attributes;
using GeometricAlgebraNumericsLib;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories;
using GeometricAlgebraNumericsLib.Products;

namespace GMacBenchmarks2.Benchmarks.Numeric.Products
{
    /// <summary>
    /// Benchmark implementation methods of standard products
    /// </summary>
    public class Benchmark11
    {
        private GaRandomGenerator _randGen;
        private GaNumFrame _frame;
        private GaNumSarMultivector _mv1;
        private GaNumSarMultivector _mv2;

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

            _mv1 = _randGen.GetNumFullKVectorTerms(_frame.VSpaceDimension, Grade1).CreateSarMultivector(_frame.VSpaceDimension);
            _mv2 = _randGen.GetNumFullKVectorTerms(_frame.VSpaceDimension, Grade2).CreateSarMultivector(_frame.VSpaceDimension);
        }

        [Benchmark]
        public GaNumSarMultivector Gp()
        {
            return _frame.Gp[_mv1, _mv2];
        }

        [Benchmark]
        public GaNumSarMultivector Op()
        {
            return _frame.Op[_mv1, _mv2];
        }

        [Benchmark]
        public GaNumSarMultivector Lcp()
        {
            return _frame.Lcp[_mv1, _mv2];
        }

        [Benchmark]
        public GaNumSarMultivector Sp()
        {
            return _frame.Sp[_mv1, _mv2];
        }
    }
}
