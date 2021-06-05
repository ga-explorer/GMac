using BenchmarkDotNet.Attributes;
using GeometricAlgebraNumericsLib;
using GeometricAlgebraNumericsLib.Metrics;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories;
using GeometricAlgebraNumericsLib.Products;
using GeometricAlgebraStructuresLib.Frames;

namespace GMac.Benchmarks.Benchmarks.Numeric.TrilinearProducts
{
/// <summary>
    /// Benchmark computed implementation method of standard products on orthogonal frames for k-vectors
    /// </summary>
    public class Benchmark1
    {
        private GaRandomGenerator _randGen;
        private GaNumMetricOrthogonal _metric;

        private IGaNumMultivector _multivector1;
        private IGaNumMultivector _multivector2;
        private IGaNumMultivector _multivector3;

        [Params(3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13)]
        public int VSpaceDimension { get; set; }

        public ulong GaSpaceDimension
            => VSpaceDimension.ToGaSpaceDimension();


        [GlobalSetup]
        public void Setup()
        {
            _randGen = new GaRandomGenerator(10);

            //var basisVectorsSignatures = _randGen.GetScalars(VSpaceDimension).ToArray();
            //_metric = GaNumMetricOrthogonal.Create(basisVectorsSignatures);

            _multivector1 = _randGen
                .GetNumFullMultivectorTerms(VSpaceDimension)
                .SumAsSarMultivector(VSpaceDimension);

            _multivector2 = _randGen
                .GetNumFullMultivectorTerms(VSpaceDimension)
                .SumAsSarMultivector(VSpaceDimension);

            _multivector3 = _randGen
                .GetNumFullMultivectorTerms(VSpaceDimension)
                .SumAsSarMultivector(VSpaceDimension);
        }


        //[Benchmark]
        public IGaNumMultivector EGpGp_Trilinear()
        {
            return _multivector1
                .GetGbtEGpGpLaTerms(_multivector2, _multivector3)
                .SumAsSarMultivector(VSpaceDimension);
        }

        //[Benchmark]
        public IGaNumMultivector EGpGp_Bilinear()
        {
            return _multivector1
                .GetGbtEGpTerms(_multivector2)
                .SumAsSarMultivector(VSpaceDimension)
                .GetGbtEGpTerms(_multivector3)
                .SumAsSarMultivector(VSpaceDimension);
        }

        [Benchmark]
        public IGaNumMultivector EOpLcp_Trilinear()
        {
            return _multivector1
                .GetGbtEOpLcpLaTerms(_multivector2, _multivector3)
                .SumAsSarMultivector(VSpaceDimension);
        }

        [Benchmark]
        public IGaNumMultivector EOpLcp_Bilinear()
        {
            return _multivector1
                .GetGbtOpTerms(_multivector2)
                .SumAsSarMultivector(VSpaceDimension)
                .GetGbtELcpTerms(_multivector3)
                .SumAsSarMultivector(VSpaceDimension);
        }
    }
}
