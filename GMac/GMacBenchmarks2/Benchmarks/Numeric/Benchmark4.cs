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
    /// k-vectors
    /// </summary>
    public class Benchmark4
    {
        private GaRandomGenerator _randGen;
        private GaNumFrameNonOrthogonal _frame;
        private GaNumFrame _frameOrtho;

        private GaNumMultivector[] _kVectors1;
        private GaNumMultivector[] _kVectors2;

        private GaNumMultivector[] _mappedKVectors1;
        private GaNumMultivector[] _mappedKVectors2;

        private GaNumMultivector[,] _mappedKVectorResults;


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

            _kVectors1 = new GaNumMultivector[VSpaceDim + 1];
            _kVectors2 = new GaNumMultivector[VSpaceDim + 1];
            _mappedKVectors1 = new GaNumMultivector[VSpaceDim + 1];
            _mappedKVectors2 = new GaNumMultivector[VSpaceDim + 1];
            _mappedKVectorResults = new GaNumMultivector[VSpaceDim + 1, VSpaceDim + 1];

            for (var g = 0; g <= VSpaceDim; g++)
            {
                _kVectors1[g] = _randGen.GetNumKVector(GaSpaceDim, g);
                _kVectors2[g] = _randGen.GetNumKVector(GaSpaceDim, g);

                _kVectors1[g].GetInternalTermsTree();
                _kVectors2[g].GetInternalTermsTree();

                _mappedKVectors1[g] = _frame.ThisToBaseFrameCba[_kVectors1[g]];
                _mappedKVectors2[g] = _frame.ThisToBaseFrameCba[_kVectors2[g]];

                _mappedKVectors1[g].GetInternalTermsTree();
                _mappedKVectors1[g].GetInternalTermsTree();
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
        public GaNumMultivector NonOrthogonalProduct_KVectors()
        {
            GaNumMultivector mv = null;

            for (var g1 = 0; g1 <= VSpaceDim; g1++)
                for (var g2 = 0; g2 <= VSpaceDim; g2++)
                    mv = ProductNonOrtho[_kVectors1[g1], _kVectors2[g2]];

            return mv;
        }

        [Benchmark]
        public Tuple<GaNumMultivector, GaNumMultivector> InputsMapping_KVectors()
        {
            GaNumMultivector mv1 = null;
            GaNumMultivector mv2 = null;

            for (var g1 = 0; g1 <= VSpaceDim; g1++)
                for (var g2 = 0; g2 <= VSpaceDim; g2++)
                {
                    mv1 = _frame.ThisToBaseFrameCba[_kVectors1[g1]];
                    mv2 = _frame.ThisToBaseFrameCba[_kVectors2[g2]];
                }

            return Tuple.Create(mv1, mv2);
        }

        [Benchmark]
        public GaNumMultivector OrthogonalProduct_KVectors()
        {
            GaNumMultivector mv = null;

            for (var g1 = 0; g1 <= VSpaceDim; g1++)
                for (var g2 = 0; g2 <= VSpaceDim; g2++)
                    mv = ProductOrtho[_mappedKVectors1[g1], _mappedKVectors2[g2]];

            return mv;
        }

        [Benchmark]
        public GaNumMultivector OutputMapping_KVectors()
        {
            GaNumMultivector mv = null;

            for (var g1 = 0; g1 <= VSpaceDim; g1++)
                for (var g2 = 0; g2 <= VSpaceDim; g2++)
                    mv = _frame.BaseFrameToThisCba[_mappedKVectorResults[g1, g2]];

            return mv;
        }
    }
}