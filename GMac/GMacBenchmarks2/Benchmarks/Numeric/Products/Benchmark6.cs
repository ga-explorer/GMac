using System;
using BenchmarkDotNet.Attributes;
using GeometricAlgebraNumericsLib;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Maps.Bilinear;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories;
using GeometricAlgebraNumericsLib.Products;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GMacBenchmarks2.Benchmarks.Numeric.Products
{
    /// <summary>
    /// Benchmark computed implementation method of standard products on non-orthogonal frames for
    /// full graded multivectors
    /// </summary>
    public class Benchmark6
    {
        private GaRandomGenerator _randGen;
        private GaNumFrameNonOrthogonal _frame;
        private GaNumFrame _frameOrtho;

        private GaNumDgrMultivector _fullMultivector1;
        private GaNumDgrMultivector _fullMultivector2;

        private GaNumDgrMultivector _mappedFullMultivector1;
        private GaNumDgrMultivector _mappedFullMultivector2;

        private GaNumDgrMultivector _mappedFullMultivectorResult;


        [Params("gp", "lcp", "sp")]
        public string ProductName { get; set; }

        [Params(3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13)]
        public int VSpaceDim { get; set; }

        public int GaSpaceDim
            => VSpaceDim.ToGaSpaceDimension();

        public IGaNumMapBilinear ProductOrtho { get; set; }

        public IGaNumMapBilinear ProductNonOrtho { get; set; }


        [GlobalSetup]
        public void Setup()
        {
            _randGen = new GaRandomGenerator(10);

            var ipm = DenseMatrix.OfArray(
                _randGen.GetSymmetricArray(VSpaceDim)
            );

            _frame = GaNumFrame.CreateNonOrthogonalFromIpm(ipm);
            _frame.SetProductsImplementation(GaBilinearProductImplementation.Computed);

            _frameOrtho = _frame.BaseOrthogonalFrame;
            _frameOrtho.SetProductsImplementation(GaBilinearProductImplementation.Computed);

            _fullMultivector1 = 
                _randGen
                    .GetNumFullMultivectorTerms(_frame.VSpaceDimension)
                    .CreateDgrMultivector(_frame.VSpaceDimension);

            _fullMultivector2 = 
                _randGen
                    .GetNumFullMultivectorTerms(_frame.VSpaceDimension)
                    .CreateDgrMultivector(_frame.VSpaceDimension);

            _mappedFullMultivector1 = _frame.ThisToBaseFrameCba[_fullMultivector1];
            _mappedFullMultivector2 = _frame.ThisToBaseFrameCba[_fullMultivector2];

            if (ProductName == "gp")
            {
                ProductOrtho = _frameOrtho.Gp;
                ProductNonOrtho = _frame.Gp;
            }
            else if (ProductName == "lcp")
            {
                ProductOrtho = _frameOrtho.Lcp;
                ProductNonOrtho = _frame.Lcp;
            }
            else
            {
                ProductOrtho = _frameOrtho.Sp;
                ProductNonOrtho = _frame.Sp;
            }

            _mappedFullMultivectorResult = ProductOrtho[
                _mappedFullMultivector1,
                _mappedFullMultivector2
            ];
        }


        [Benchmark]
        public GaNumDgrMultivector NonOrthogonalProduct_FullMultivectors()
        {
            return ProductNonOrtho[_fullMultivector1, _fullMultivector2];
        }

        [Benchmark]
        public Tuple<GaNumDgrMultivector, GaNumDgrMultivector> InputsMapping_FullMultivectors()
        {
            return Tuple.Create(
                _frame.ThisToBaseFrameCba[_fullMultivector1],
                _frame.ThisToBaseFrameCba[_fullMultivector2]
            );
        }

        [Benchmark]
        public GaNumDgrMultivector OrthogonalProduct_FullMultivectors()
        {
            return ProductOrtho[_mappedFullMultivector1, _mappedFullMultivector2];
        }

        [Benchmark]
        public GaNumDgrMultivector OutputMapping_FullMultivectors()
        {
            return _frame.BaseFrameToThisCba[_mappedFullMultivectorResult];
        }
    }
}