using System;
using BenchmarkDotNet.Attributes;
using GeometricAlgebraNumericsLib;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Maps.Bilinear;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories;
using GeometricAlgebraNumericsLib.Products;
using GeometricAlgebraStructuresLib.Frames;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GMac.Benchmarks.Benchmarks.Numeric.BilinearProducts
{
    /// <summary>
    /// Benchmark computed implementation method of standard products on non-orthogonal frames for
    /// full tree multivectors
    /// </summary>
    public class Benchmark5
    {
        private GaRandomGenerator _randGen;
        private GaNumFrameNonOrthogonal _frame;
        private GaNumFrame _frameOrtho;

        private GaNumSarMultivector _fullMultivector1;
        private GaNumSarMultivector _fullMultivector2;

        private GaNumSarMultivector _mappedFullMultivector1;
        private GaNumSarMultivector _mappedFullMultivector2;

        private GaNumSarMultivector _mappedFullMultivectorResult;


        [Params("gp", "lcp", "sp")]
        public string ProductName { get; set; }

        [Params(3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13)]
        public int VSpaceDim { get; set; }

        public ulong GaSpaceDim
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
                    .CreateSarMultivector(_frame.VSpaceDimension);
            
            _fullMultivector2 = 
                _randGen
                    .GetNumFullMultivectorTerms(_frame.VSpaceDimension)
                    .CreateSarMultivector(_frame.VSpaceDimension);

            _fullMultivector1.GetBtrRootNode();
            _fullMultivector2.GetBtrRootNode();

            _mappedFullMultivector1 = _frame.ThisToBaseFrameCba[_fullMultivector1];
            _mappedFullMultivector2 = _frame.ThisToBaseFrameCba[_fullMultivector2];

            _mappedFullMultivector1.GetBtrRootNode();
            _mappedFullMultivector2.GetBtrRootNode();

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
        public GaNumSarMultivector NonOrthogonalProduct_FullMultivectors()
        {
            return ProductNonOrtho[_fullMultivector1, _fullMultivector2];
        }

        [Benchmark]
        public Tuple<GaNumSarMultivector, GaNumSarMultivector> InputsMapping_FullMultivectors()
        {
            return Tuple.Create(
                _frame.ThisToBaseFrameCba[_fullMultivector1],
                _frame.ThisToBaseFrameCba[_fullMultivector2]
            );
        }

        [Benchmark]
        public GaNumSarMultivector OrthogonalProduct_FullMultivectors()
        {
            return ProductOrtho[_mappedFullMultivector1, _mappedFullMultivector2];
        }

        [Benchmark]
        public GaNumSarMultivector OutputMapping_FullMultivectors()
        {
            return _frame.BaseFrameToThisCba[_mappedFullMultivectorResult];
        }
    }
}