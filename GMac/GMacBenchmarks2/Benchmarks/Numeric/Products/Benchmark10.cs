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
    /// Benchmark computed implementation method of standard products on non-orthogonal frames for JAR sparse multivectors
    /// </summary>
    public class Benchmark10
    {
        private GaRandomGenerator _randGen;
        private GaNumFrameNonOrthogonal _frame;
        private GaNumFrame _frameOrtho;

        private GaNumDgrMultivector[] _inputMultivectors1;
        private GaNumDgrMultivector[] _inputMultivectors2;

        private GaNumDgrMultivector[] _mappedInputMultivectors1;
        private GaNumDgrMultivector[] _mappedInputMultivectors2;

        private GaNumDgrMultivector[] _mappedInputMultivectorProducts;


        public int MultivectorsCount
            => 10;

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

            _inputMultivectors1 = new GaNumDgrMultivector[MultivectorsCount];
            _inputMultivectors2 = new GaNumDgrMultivector[MultivectorsCount];

            _mappedInputMultivectors1 = new GaNumDgrMultivector[MultivectorsCount];
            _mappedInputMultivectors2 = new GaNumDgrMultivector[MultivectorsCount];

            _mappedInputMultivectorProducts = new GaNumDgrMultivector[MultivectorsCount * MultivectorsCount];

            for (var i = 0; i < MultivectorsCount; i++)
            {
                _inputMultivectors1[i] = _randGen.GetNumMultivectorTerms(_frame.VSpaceDimension).CreateDgrMultivector(_frame.VSpaceDimension);
                _inputMultivectors2[i] = _randGen.GetNumMultivectorTerms(_frame.VSpaceDimension).CreateDgrMultivector(_frame.VSpaceDimension);

                _mappedInputMultivectors1[i] = _frame.ThisToBaseFrameCba[_inputMultivectors1[i]];
                _mappedInputMultivectors2[i] = _frame.ThisToBaseFrameCba[_inputMultivectors2[i]];
            }

            var k = 0;
            for (var i1 = 0; i1 < MultivectorsCount; i1++)
            {
                for (var i2 = 0; i2 < MultivectorsCount; i2++)
                {
                    _mappedInputMultivectorProducts[k++] = ProductOrtho[
                        _mappedInputMultivectors1[i1],
                        _mappedInputMultivectors2[i2]
                    ];
                }
            }
        }


        [Benchmark]
        public GaNumDgrMultivector NonOrthogonalProduct()
        {
            GaNumDgrMultivector mv = null;

            for (var i1 = 0; i1 < MultivectorsCount; i1++)
            for (var i2 = 0; i2 < MultivectorsCount; i2++)
                mv = ProductNonOrtho[_inputMultivectors1[i1], _inputMultivectors2[i2]];

            return mv;
        }

        [Benchmark]
        public Tuple<GaNumDgrMultivector, GaNumDgrMultivector> InputsMapping()
        {
            var tuple = new Tuple<GaNumDgrMultivector, GaNumDgrMultivector>(null, null);

            for (var i1 = 0; i1 < MultivectorsCount; i1++)
            for (var i2 = 0; i2 < MultivectorsCount; i2++)
                tuple = Tuple.Create(
                    _frame.ThisToBaseFrameCba[_inputMultivectors1[i1]],
                    _frame.ThisToBaseFrameCba[_inputMultivectors2[i2]]
                );

            return tuple;
        }

        [Benchmark]
        public GaNumDgrMultivector OrthogonalProduct()
        {
            GaNumDgrMultivector mv = null;

            for (var i1 = 0; i1 < MultivectorsCount; i1++)
            for (var i2 = 0; i2 < MultivectorsCount; i2++)
                mv = ProductOrtho[_mappedInputMultivectors1[i1], _mappedInputMultivectors2[i2]];

            return mv;
        }

        [Benchmark]
        public GaNumDgrMultivector OutputMapping()
        {
            GaNumDgrMultivector mv = null;

            var k = 0;
            for (var i1 = 0; i1 < MultivectorsCount; i1++)
            for (var i2 = 0; i2 < MultivectorsCount; i2++)
                mv = _frame.BaseFrameToThisCba[_mappedInputMultivectorProducts[k++]];

            return mv;
        }
    }
}