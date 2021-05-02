using BenchmarkDotNet.Attributes;
using GeometricAlgebraNumericsLib;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories;
using GeometricAlgebraNumericsLib.Outermorphisms;
using GeometricAlgebraStructuresLib.Frames;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GMacBenchmarks2.Benchmarks.Numeric.Outermorphisms
{
    /// <summary>
    /// Benchmark outermorphisms on full multivectors
    /// </summary>
    public class Benchmark6
    {
        private GaRandomGenerator _randGen;

        private GaNumOutermorphism _omComputed;
        private GaNumSarMultivector _treeMultivector;
        private GaNumDgrMultivector _gradedMultivector;

        [Params(3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15)]
        //[Params(3, 4, 5, 6, 7, 8, 9, 10)]
        public int VSpaceDim { get; set; }

        public ulong GaSpaceDim
            => VSpaceDim.ToGaSpaceDimension();


        [GlobalSetup]
        public void Setup()
        {
            _randGen = new GaRandomGenerator(10);

            var matrix = DenseMatrix.Create(
                VSpaceDim,
                VSpaceDim,
                (i, j) => _randGen.GetScalar()
            );

            _omComputed = GaNumOutermorphism.Create(matrix);

            _treeMultivector = _randGen.GetNumMultivectorTerms(VSpaceDim).CreateSarMultivector(VSpaceDim);

            _treeMultivector.GetBtrRootNode();

            _gradedMultivector = _treeMultivector.GetDgrMultivector();
        }


        [Benchmark]
        public GaNumSarMultivector OmComputed_TreeMultivectors()
        {
            var mv = _omComputed[_treeMultivector];

            return mv;
        }

        [Benchmark]
        public GaNumDgrMultivector OmComputed_GradedMultivectors()
        {
            var mv = _omComputed[_gradedMultivector];

            return mv;
        }
    }
}