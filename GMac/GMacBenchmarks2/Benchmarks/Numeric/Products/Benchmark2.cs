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
    /// Benchmark computed implementation method of standard products on orthogonal frames for k-vectors
    /// </summary>
    public class Benchmark2
    {
        private GaRandomGenerator _randGen;
        private GaNumFrame _frame;

        private GaNumSarMultivector[] _treeMultivectors1;
        private GaNumSarMultivector[] _treeMultivectors2;

        private GaNumDgrMultivector[] _gradedMultivectors1;
        private GaNumDgrMultivector[] _gradedMultivectors2;

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

            _treeMultivectors1 = new GaNumSarMultivector[_frame.VSpaceDimension + 1];
            _treeMultivectors2 = new GaNumSarMultivector[_frame.VSpaceDimension + 1];

            _gradedMultivectors1 = new GaNumDgrMultivector[_frame.VSpaceDimension + 1];
            _gradedMultivectors2 = new GaNumDgrMultivector[_frame.VSpaceDimension + 1];

            for (var grade = 0; grade < _frame.VSpaceDimension + 1; grade++)
            {
                _treeMultivectors1[grade] = _randGen.GetNumFullKVectorTerms(VSpaceDim, grade).CreateSarMultivector(VSpaceDim);
                _treeMultivectors2[grade] = _randGen.GetNumFullKVectorTerms(VSpaceDim, grade).CreateSarMultivector(VSpaceDim);

                _treeMultivectors1[grade].GetBtrRootNode();
                _treeMultivectors2[grade].GetBtrRootNode();

                _gradedMultivectors1[grade] = _treeMultivectors1[grade].GetDgrMultivector();
                _gradedMultivectors2[grade] = _treeMultivectors2[grade].GetDgrMultivector();
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

            for (var grade1 = 0; grade1 < _frame.VSpaceDimension + 1; grade1++)
            for (var grade2 = 0; grade2 < _frame.VSpaceDimension + 1; grade2++)
                mv = Product[_treeMultivectors1[grade1], _treeMultivectors2[grade2]];

            return mv;
        }

        [Benchmark]
        public GaNumDgrMultivector OrthogonalProduct_GradedMultivectors()
        {
            GaNumDgrMultivector mv = null;

            for (var grade1 = 0; grade1 < _frame.VSpaceDimension + 1; grade1++)
            for (var grade2 = 0; grade2 < _frame.VSpaceDimension + 1; grade2++)
                mv = Product[_gradedMultivectors1[grade1], _gradedMultivectors2[grade2]];

            return mv;
        }
    }
}