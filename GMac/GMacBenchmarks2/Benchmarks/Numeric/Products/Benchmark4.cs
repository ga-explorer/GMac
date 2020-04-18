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
    /// Benchmark computed implementation method of standard products on orthogonal frames for sparse multivectors
    /// </summary>
    public class Benchmark4
    {
        private GaRandomGenerator _randGen;
        private GaNumFrame _frame;

        private GaNumSarMultivector[] _treeMultivectors1;
        private GaNumSarMultivector[] _treeMultivectors2;

        private GaNumDgrMultivector[] _gradedMultivectors1;
        private GaNumDgrMultivector[] _gradedMultivectors2;

        public int MultivectorsCount { get; }
            = 10;

        [Params("op", "gp", "lcp", "sp")]
        public string ProductName { get; set; }

        [Params(3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13)]
        public int VSpaceDim { get; set; }

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

            _treeMultivectors1 = new GaNumSarMultivector[MultivectorsCount];
            _treeMultivectors2 = new GaNumSarMultivector[MultivectorsCount];

            _gradedMultivectors1 = new GaNumDgrMultivector[MultivectorsCount];
            _gradedMultivectors2 = new GaNumDgrMultivector[MultivectorsCount];

            for (var i = 0; i < MultivectorsCount; i++)
            {
                _treeMultivectors1[i] = _randGen.GetNumMultivectorTerms(VSpaceDim).CreateSarMultivector(VSpaceDim);
                _treeMultivectors2[i] = _randGen.GetNumMultivectorTerms(VSpaceDim).CreateSarMultivector(VSpaceDim);

                _treeMultivectors1[i].GetBtrRootNode();
                _treeMultivectors2[i].GetBtrRootNode();

                _gradedMultivectors1[i] = _treeMultivectors1[i].GetDgrMultivector();
                _gradedMultivectors2[i] = _treeMultivectors2[i].GetDgrMultivector();
            }

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

            for (var i1 = 0; i1 < MultivectorsCount; i1++)
            for (var i2 = 0; i2 < MultivectorsCount; i2++)
                mv = Product[_treeMultivectors1[i1], _treeMultivectors2[i2]];

            return mv;
        }

        [Benchmark]
        public GaNumDgrMultivector OrthogonalProduct_GradedMultivectors()
        {
            GaNumDgrMultivector mv = null;

            for (var i1 = 0; i1 < MultivectorsCount; i1++)
            for (var i2 = 0; i2 < MultivectorsCount; i2++)
                mv = Product[_gradedMultivectors1[i1], _gradedMultivectors2[i2]];

            return mv;
        }
    }
}