using System.Linq;
using BenchmarkDotNet.Attributes;
using GeometricAlgebraNumericsLib;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Maps.Bilinear;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories;
using GeometricAlgebraNumericsLib.Products;

namespace GMacBenchmarks2.Benchmarks.Numeric.Products
{
    /// <summary>
    /// Benchmark computed implementation method of standard products on orthogonal frames for full multivectors
    /// </summary>
    public class Benchmark3
    {
        private GaRandomGenerator _randGen;
        private GaNumFrame _frame;

        private GaNumSarMultivector _treeMultivector1;
        private GaNumSarMultivector _treeMultivector2;

        private GaNumDgrMultivector _gradedMultivector1;
        private GaNumDgrMultivector _gradedMultivector2;


        [Params("op", "gp", "lcp", "sp")]
        public string ProductName { get; set; }

        [Params(3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13)]
        public int VSpaceDim { get; set; }
        //= 12;

        public int GaSpaceDim
            => VSpaceDim.ToGaSpaceDimension();

        public IGaNumMapBilinear Product { get; set; }


        [GlobalSetup]
        public void Setup()
        {
            _randGen = new GaRandomGenerator(10);

            var basisVectorsSignatures = _randGen.GetScalars(VSpaceDim).ToArray();
            //basisVectorsSignatures[0] = 0;
            //basisVectorsSignatures[1] = 0;
            _frame = GaNumFrame.CreateOrthogonal(basisVectorsSignatures);
            _frame.SetProductsImplementation(GaBilinearProductImplementation.Computed);

            _treeMultivector1 = 
                _randGen
                    .GetNumFullMultivectorTerms(VSpaceDim)
                    .CreateSarMultivector(VSpaceDim);

            _treeMultivector2 = 
                _randGen
                    .GetNumFullMultivectorTerms(VSpaceDim)
                    .CreateSarMultivector(VSpaceDim);

            _treeMultivector1.GetBtrRootNode();
            _treeMultivector2.GetBtrRootNode();

            _gradedMultivector1 = _treeMultivector1.GetDgrMultivector();
            _gradedMultivector2 = _treeMultivector2.GetDgrMultivector();

            if (ProductName == "gp")
                Product = _frame.Gp;

            else if (ProductName == "op")
                Product = _frame.Op;

            else if (ProductName == "lcp")
                Product = _frame.Lcp;

            else
                Product = _frame.Sp;
        }


        [Benchmark]
        public GaNumSarMultivector OrthogonalProduct_TreeMultivectors()
        {
            GaNumSarMultivector mv = null;

            mv = Product[_treeMultivector1, _treeMultivector2];

            return mv;
        }

        [Benchmark]
        public GaNumDgrMultivector OrthogonalProduct_GradedMultivectors()
        {
            GaNumDgrMultivector mv = null;

            mv = Product[_gradedMultivector1, _gradedMultivector2];

            return mv;
        }
    }
}