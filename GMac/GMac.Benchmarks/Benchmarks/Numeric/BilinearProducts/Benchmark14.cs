using System;
using BenchmarkDotNet.Attributes;
using GeometricAlgebraNumericsLib;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Maps.Bilinear;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories;
using GeometricAlgebraNumericsLib.Products;
using GeometricAlgebraStructuresLib.Frames;

namespace GMac.Benchmarks.Benchmarks.Numeric.BilinearProducts
{
    /// <summary>
    /// Benchmark computed implementation method of standard products on non-orthogonal frames for
    /// k-vectors
    /// </summary>
    public class Benchmark14
    {
        private GaRandomGenerator _randGen;
        private GaNumFrameNonOrthogonal _frame;
        private GaNumFrame _frameOrtho;

        private GaNumSarMultivector[] _kVectors1;
        private GaNumSarMultivector[] _kVectors2;

        private GaNumSarMultivector[] _mappedKVectors1;
        private GaNumSarMultivector[] _mappedKVectors2;

        private GaNumSarMultivector[,] _mappedKVectorResults;


        [Params(3, 4, 5, 6, 7, 8, 9, 10, 11, 12)]
        //[Params(12)]
        public int VSpaceDim { get; set; }
        //= 12;

        public ulong GaSpaceDim
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

            _kVectors1 = new GaNumSarMultivector[VSpaceDim + 1];
            _kVectors2 = new GaNumSarMultivector[VSpaceDim + 1];
            _mappedKVectors1 = new GaNumSarMultivector[VSpaceDim + 1];
            _mappedKVectors2 = new GaNumSarMultivector[VSpaceDim + 1];
            _mappedKVectorResults = new GaNumSarMultivector[VSpaceDim + 1, VSpaceDim + 1];

            for (var g = 0; g <= VSpaceDim; g++)
            {
                _kVectors1[g] = _randGen.GetNumFullKVectorTerms(VSpaceDim, g).CreateSarMultivector(VSpaceDim);
                _kVectors2[g] = _randGen.GetNumFullKVectorTerms(VSpaceDim, g).CreateSarMultivector(VSpaceDim);

                _kVectors1[g].GetBtrRootNode();
                _kVectors2[g].GetBtrRootNode();

                _mappedKVectors1[g] = _frame.ThisToBaseFrameCba[_kVectors1[g]];
                _mappedKVectors2[g] = _frame.ThisToBaseFrameCba[_kVectors2[g]];

                _mappedKVectors1[g].GetBtrRootNode();
                _mappedKVectors1[g].GetBtrRootNode();
            }

            ProductOrtho = _frameOrtho.Sp;
            ProductNonOrtho = _frame.Sp;

            for (var g1 = 0; g1 <= VSpaceDim; g1++)
                for (var g2 = 0; g2 <= VSpaceDim; g2++)
                    _mappedKVectorResults[g1, g2] = ProductOrtho[
                        _mappedKVectors1[g1],
                        _mappedKVectors2[g2]
                    ];
        }


        [Benchmark]
        public GaNumSarMultivector NonOrthogonalProduct_KVectors()
        {
            GaNumSarMultivector mv = null;

            for (var g1 = 0; g1 <= VSpaceDim; g1++)
                for (var g2 = 0; g2 <= VSpaceDim; g2++)
                    mv = ProductNonOrtho[_kVectors1[g1], _kVectors2[g2]];

            return mv;
        }

        [Benchmark]
        public Tuple<GaNumSarMultivector, GaNumSarMultivector> InputsMapping_KVectors()
        {
            GaNumSarMultivector mv1 = null;
            GaNumSarMultivector mv2 = null;

            for (var g1 = 0; g1 <= VSpaceDim; g1++)
                for (var g2 = 0; g2 <= VSpaceDim; g2++)
                {
                    mv1 = _frame.ThisToBaseFrameCba[_kVectors1[g1]];
                    mv2 = _frame.ThisToBaseFrameCba[_kVectors2[g2]];
                }

            return Tuple.Create(mv1, mv2);
        }

        [Benchmark]
        public GaNumSarMultivector OrthogonalProduct_KVectors()
        {
            GaNumSarMultivector mv = null;

            for (var g1 = 0; g1 <= VSpaceDim; g1++)
                for (var g2 = 0; g2 <= VSpaceDim; g2++)
                    mv = ProductOrtho[_mappedKVectors1[g1], _mappedKVectors2[g2]];

            return mv;
        }

        [Benchmark]
        public GaNumSarMultivector OutputMapping_KVectors()
        {
            GaNumSarMultivector mv = null;

            for (var g1 = 0; g1 <= VSpaceDim; g1++)
                for (var g2 = 0; g2 <= VSpaceDim; g2++)
                    mv = _frame.BaseFrameToThisCba[_mappedKVectorResults[g1, g2]];

            return mv;
        }
    }
}