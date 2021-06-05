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
    /// Benchmark computed implementation method of standard products on non-orthogonal frames for BTR k-vectors
    /// </summary>
    public class Benchmark7
    {
        private GaRandomGenerator _randGen;
        private GaNumFrameNonOrthogonal _frame;
        private GaNumFrame _frameOrtho;

        private GaNumSarMultivector[] _inputMultivectors1;
        private GaNumSarMultivector[] _inputMultivectors2;

        private GaNumSarMultivector[] _mappedInputMultivectors1;
        private GaNumSarMultivector[] _mappedInputMultivectors2;

        private GaNumSarMultivector[] _mappedInputMultivectorProducts;


        public int MultivectorsCount
            => VSpaceDim + 1;

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

            _inputMultivectors1 = new GaNumSarMultivector[MultivectorsCount];
            _inputMultivectors2 = new GaNumSarMultivector[MultivectorsCount];

            _mappedInputMultivectors1 = new GaNumSarMultivector[MultivectorsCount];
            _mappedInputMultivectors2 = new GaNumSarMultivector[MultivectorsCount];

            _mappedInputMultivectorProducts = new GaNumSarMultivector[MultivectorsCount * MultivectorsCount];

            for (var i = 0; i < MultivectorsCount; i++)
            {
                _inputMultivectors1[i] = _randGen.GetNumFullKVectorTerms(_frame.VSpaceDimension, i).CreateSarMultivector(_frame.VSpaceDimension);
                _inputMultivectors2[i] = _randGen.GetNumFullKVectorTerms(_frame.VSpaceDimension, i).CreateSarMultivector(_frame.VSpaceDimension);

                _inputMultivectors1[i].GetBtrRootNode();
                _inputMultivectors2[i].GetBtrRootNode();

                _mappedInputMultivectors1[i] = _frame.ThisToBaseFrameCba[_inputMultivectors1[i]];
                _mappedInputMultivectors2[i] = _frame.ThisToBaseFrameCba[_inputMultivectors2[i]];

                _mappedInputMultivectors1[i].GetBtrRootNode();
                _mappedInputMultivectors2[i].GetBtrRootNode();
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
        public GaNumSarMultivector NonOrthogonalProduct()
        {
            GaNumSarMultivector mv = null;

            for (var i1 = 0; i1 < MultivectorsCount; i1++)
                for (var i2 = 0; i2 < MultivectorsCount; i2++)
                    mv = ProductNonOrtho[_inputMultivectors1[i1], _inputMultivectors2[i2]];

            return mv;
        }

        [Benchmark]
        public Tuple<GaNumSarMultivector, GaNumSarMultivector> InputsMapping()
        {
            var tuple = new Tuple<GaNumSarMultivector, GaNumSarMultivector>(null, null);

            for (var i1 = 0; i1 < MultivectorsCount; i1++)
                for (var i2 = 0; i2 < MultivectorsCount; i2++)
                    tuple = Tuple.Create(
                        _frame.ThisToBaseFrameCba[_inputMultivectors1[i1]],
                        _frame.ThisToBaseFrameCba[_inputMultivectors2[i2]]
                    );

            return tuple;
        }

        [Benchmark]
        public GaNumSarMultivector OrthogonalProduct()
        {
            GaNumSarMultivector mv = null;

            for (var i1 = 0; i1 < MultivectorsCount; i1++)
                for (var i2 = 0; i2 < MultivectorsCount; i2++)
                    mv = ProductOrtho[_mappedInputMultivectors1[i1], _mappedInputMultivectors2[i2]];

            return mv;
        }

        [Benchmark]
        public GaNumSarMultivector OutputMapping()
        {
            GaNumSarMultivector mv = null;

            var k = 0;
            for (var i1 = 0; i1 < MultivectorsCount; i1++)
                for (var i2 = 0; i2 < MultivectorsCount; i2++)
                    mv = _frame.BaseFrameToThisCba[_mappedInputMultivectorProducts[k++]];

            return mv;
        }
    }
}