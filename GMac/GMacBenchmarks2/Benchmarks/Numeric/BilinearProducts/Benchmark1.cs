using BenchmarkDotNet.Attributes;
using GeometricAlgebraNumericsLib;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Maps.Bilinear;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories;
using GeometricAlgebraNumericsLib.Products;
using GeometricAlgebraStructuresLib.Frames;

namespace GMacBenchmarks2.Benchmarks.Numeric.BilinearProducts
{
    /// <summary>
    /// Benchmark computed implementation method of standard products on orthogonal frames for terms
    /// </summary>
    public class Benchmark1
    {
        private GaRandomGenerator _randGen;
        private GaNumFrame _frame;

        private ulong _factor = 8;

        public ulong MultivectorsCount
            => GaSpaceDim / _factor;

        private GaNumSarMultivector[] _treeMultivectors1;
        private GaNumSarMultivector[] _treeMultivectors2;

        private GaNumDgrMultivector[] _gradedMultivectors1;
        private GaNumDgrMultivector[] _gradedMultivectors2;


        [Params(3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16)]
        public int VSpaceDim { get; set; }
        //= 12;

        public ulong GaSpaceDim
            => VSpaceDim.ToGaSpaceDimension();

        public IGaNumMapBilinear Product { get; set; }


        [GlobalSetup]
        public void Setup()
        {
            _randGen = new GaRandomGenerator(10);

            _frame = GaNumFrame.CreateEuclidean(VSpaceDim);
            _frame.SetProductsImplementation(GaBilinearProductImplementation.Computed);

            _treeMultivectors1 = new GaNumSarMultivector[MultivectorsCount];
            _treeMultivectors2 = new GaNumSarMultivector[MultivectorsCount];

            _gradedMultivectors1 = new GaNumDgrMultivector[MultivectorsCount];
            _gradedMultivectors2 = new GaNumDgrMultivector[MultivectorsCount];

            for (var id = 0UL; id < MultivectorsCount; id++)
            {
                _treeMultivectors1[id] = _randGen.GetNumTerm(id * _factor).CreateSarMultivector(VSpaceDim);
                _treeMultivectors2[id] = _randGen.GetNumTerm(id * _factor).CreateSarMultivector(VSpaceDim);

                _treeMultivectors1[id].GetBtrRootNode();
                _treeMultivectors2[id].GetBtrRootNode();

                _gradedMultivectors1[id] = _treeMultivectors1[id].GetDgrMultivector();
                _gradedMultivectors2[id] = _treeMultivectors2[id].GetDgrMultivector();
            }

            Product = _frame.Gp;
        }


        [Benchmark]
        public GaNumSarMultivector OrthogonalProduct_TreeMultivectors()
        {
            GaNumSarMultivector mv = null;

            for (var id1 = 0UL; id1 < MultivectorsCount; id1++)
            for (var id2 = 0UL; id2 < MultivectorsCount; id2++)
                mv = Product[_treeMultivectors1[id1], _treeMultivectors2[id2]];

            return mv;
        }

        [Benchmark]
        public GaNumDgrMultivector OrthogonalProduct_GradedMultivectors()
        {
            GaNumDgrMultivector mv = null;

            for (var id1 = 0UL; id1 < MultivectorsCount; id1++)
            for (var id2 = 0UL; id2 < MultivectorsCount; id2++)
                mv = Product[_gradedMultivectors1[id1], _gradedMultivectors2[id2]];

            return mv;
        }
    }
}