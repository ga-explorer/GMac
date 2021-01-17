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
    /// Benchmark outermorphisms on k-vectors
    /// </summary>
    public class Benchmark5
    {
        private GaRandomGenerator _randGen;

        private GaNumOutermorphism _omComputed;
        private GaNumSarMultivector[] _treeMultivectors;
        private GaNumDgrMultivector[] _gradedMultivectors;

        [Params(3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15)]
        public int VSpaceDim { get; set; }

        public int GaSpaceDim
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

            _treeMultivectors = new GaNumSarMultivector[VSpaceDim + 1];
            _gradedMultivectors = new GaNumDgrMultivector[VSpaceDim + 1];

            for (var grade = 0; grade <= VSpaceDim; grade++)
            {
                _treeMultivectors[grade] = _randGen.GetNumFullKVectorTerms(VSpaceDim, grade).CreateSarMultivector(VSpaceDim);

                _treeMultivectors[grade].GetBtrRootNode();

                _gradedMultivectors[grade] = _treeMultivectors[grade].GetDgrMultivector();
            }
        }


        [Benchmark]
        public GaNumSarMultivector OmComputed_TreeMultivectors()
        {
            GaNumSarMultivector mv = null;

            for (var grade1 = 0; grade1 <= VSpaceDim; grade1++)
                mv = _omComputed[_treeMultivectors[grade1]];

            return mv;
        }

        [Benchmark]
        public GaNumDgrMultivector OmComputed_GradedMultivectors()
        {
            GaNumDgrMultivector mv = null;

            for (var grade1 = 0; grade1 <= VSpaceDim; grade1++)
                mv = _omComputed[_gradedMultivectors[grade1]];

            return mv;
        }
    }
}