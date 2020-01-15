using BenchmarkDotNet.Attributes;
using GeometricAlgebraNumericsLib;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Multivectors;
using GeometricAlgebraNumericsLib.Structures;

namespace GMacBenchmarks2.Benchmarks.Numeric
{
    /// <summary>
    /// Benchmark internal tree construction of multivectors
    /// </summary>
    public class Benchmark11
    {
        private GaRandomGenerator _randGen;
        private GaNumMultivector _mv1;


        //[Params(2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15)]
        public int VSpaceDim { get; set; }
            = 15;

        [Params(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15)]
        public int Grade { get; set; }
            //= 8;

        [GlobalSetup]
        public void Setup()
        {
            _randGen = new GaRandomGenerator(10);

            //_mv1 = _randGen.GetNumMultivectorFull(
            //    VSpaceDim.ToGaSpaceDimension()
            //);

            _mv1 = _randGen.GetNumBlade(
                VSpaceDim.ToGaSpaceDimension(), 
                Grade
            );
        }


        [Benchmark]
        public GaBinaryTreeInternalNode<double> ConstructMultivectorTree()
        {
            _mv1.ClearInternalTermsTree();

            return _mv1.GetInternalTermsTree();
        }
    }
}