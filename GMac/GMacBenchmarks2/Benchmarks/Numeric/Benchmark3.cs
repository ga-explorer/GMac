using System;
using BenchmarkDotNet.Attributes;
using GeometricAlgebraNumericsLib;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Maps.Bilinear;
using GeometricAlgebraNumericsLib.Multivectors;
using GeometricAlgebraNumericsLib.Products;

namespace GMacBenchmarks2.Benchmarks.Numeric
{
    /// <summary>
    /// Benchmark computed implementation method of standard products on non-orthogonal frames for
    /// full multivectors
    /// </summary>
    public class Benchmark3
    {
        private GaRandomGenerator _randGen;
        private GaNumFrameNonOrthogonal _frame;
        private GaNumFrame _frameOrtho;

        private GaNumMultivector _fullMultivector1;
        private GaNumMultivector _fullMultivector2;

        private GaNumMultivector _mappedFullMultivector1;
        private GaNumMultivector _mappedFullMultivector2;

        private GaNumMultivector _mappedFullMultivectorResult;


        [Params(3, 4, 5, 6, 7, 8, 9, 10, 11, 12)]
        //[Params(12)]
        public int VSpaceDim { get; set; }
        //= 12;

        public int GaSpaceDim
            => VSpaceDim.ToGaSpaceDimension();

        public IGaNumMapBilinear ProductOrtho { get; set; }

        public IGaNumMapBilinear ProductNonOrtho { get; set; }


        [GlobalSetup]
        public void Setup()
        {
            _randGen = new GaRandomGenerator(10);

            _frame = GaNumFrame.CreateConformal(VSpaceDim);
            _frame.SetProductsImplementation(GaBilinearProductImplementation.Computed);

            _frameOrtho = _frame.BaseOrthogonalFrame;
            _frameOrtho.SetProductsImplementation(GaBilinearProductImplementation.Computed);

            _fullMultivector1 = _randGen.GetNumMultivectorFull(_frame.GaSpaceDimension);
            _fullMultivector2 = _randGen.GetNumMultivectorFull(_frame.GaSpaceDimension);

            _fullMultivector1.GetInternalTermsTree();
            _fullMultivector2.GetInternalTermsTree();

            _mappedFullMultivector1 = _frame.ThisToBaseFrameCba[_fullMultivector1];
            _mappedFullMultivector2 = _frame.ThisToBaseFrameCba[_fullMultivector2];

            _mappedFullMultivector1.GetInternalTermsTree();
            _mappedFullMultivector2.GetInternalTermsTree();

            ProductOrtho = _frameOrtho.Sp;
            ProductNonOrtho = _frame.Sp;

            _mappedFullMultivectorResult = ProductOrtho[
                _mappedFullMultivector1,
                _mappedFullMultivector2
            ];
        }


        [Benchmark]
        public GaNumMultivector NonOrthogonalProduct_FullMultivectors()
        {
            return ProductNonOrtho[_fullMultivector1, _fullMultivector2];
        }

        [Benchmark]
        public Tuple<GaNumMultivector, GaNumMultivector> InputsMapping_FullMultivectors()
        {
            return Tuple.Create(
                _frame.ThisToBaseFrameCba[_fullMultivector1],
                _frame.ThisToBaseFrameCba[_fullMultivector2]
            );
        }

        [Benchmark]
        public GaNumMultivector OrthogonalProduct_FullMultivectors()
        {
            return ProductOrtho[_mappedFullMultivector1, _mappedFullMultivector2];
        }

        [Benchmark]
        public GaNumMultivector OutputMapping_FullMultivectors()
        {
            return _frame.BaseFrameToThisCba[_mappedFullMultivectorResult];
        }
    }
}