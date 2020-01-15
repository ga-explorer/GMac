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
    /// terms
    /// </summary>
    public class Benchmark5
    {
        private GaRandomGenerator _randGen;
        private GaNumFrameNonOrthogonal _frame;
        private GaNumFrame _frameOrtho;

        private int _factor = 8;

        public int TermsCount 
            => GaSpaceDim / _factor;

        private GaNumMultivector[] _terms1;
        private GaNumMultivector[] _terms2;

        private GaNumMultivector[] _mappedTerms1;
        private GaNumMultivector[] _mappedTerms2;

        private GaNumMultivector[,] _mappedTermResults;
        

        //[Params(3, 4, 5, 6, 7, 8, 9, 10)]
        [Params(11, 12)]
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

            _terms1 = new GaNumMultivector[TermsCount];
            _terms2 = new GaNumMultivector[TermsCount];
            _mappedTerms1 = new GaNumMultivector[TermsCount];
            _mappedTerms2 = new GaNumMultivector[TermsCount];
            _mappedTermResults = new GaNumMultivector[TermsCount, TermsCount];

            for (var id = 0; id < TermsCount; id++)
            {
                _terms1[id] = _randGen.GetNumTerm(GaSpaceDim, id * _factor);
                _terms2[id] = _randGen.GetNumTerm(GaSpaceDim, id * _factor);

                _terms1[id].GetInternalTermsTree();
                _terms2[id].GetInternalTermsTree();

                _mappedTerms1[id] = _frame.ThisToBaseFrameCba[_terms1[id]];
                _mappedTerms2[id] = _frame.ThisToBaseFrameCba[_terms2[id]];

                _mappedTerms1[id].GetInternalTermsTree();
                _mappedTerms1[id].GetInternalTermsTree();
            }

            ProductOrtho = _frameOrtho.Sp;
            ProductNonOrtho = _frame.Sp;

            for (var id1 = 0; id1 < TermsCount; id1++)
            for (var id2 = 0; id2 < TermsCount; id2++)
                _mappedTermResults[id1, id2] = ProductOrtho[
                    _mappedTerms1[id1],
                    _mappedTerms2[id2]
                ];
        }


        [Benchmark]
        public GaNumMultivector NonOrthogonalProduct_Terms()
        {
            GaNumMultivector mv = null;

            for (var id1 = 0; id1 < TermsCount; id1++)
            for (var id2 = 0; id2 < TermsCount; id2++)
                mv = ProductNonOrtho[_terms1[id1], _terms2[id2]];

            return mv;
        }

        [Benchmark]
        public Tuple<GaNumMultivector, GaNumMultivector> InputsMapping_Terms()
        {
            GaNumMultivector mv1 = null;
            GaNumMultivector mv2 = null;

            for (var id1 = 0; id1 < TermsCount; id1++)
            for (var id2 = 0; id2 < TermsCount; id2++)
            {
                mv1 = _frame.ThisToBaseFrameCba[_terms1[id1]];
                mv2 = _frame.ThisToBaseFrameCba[_terms2[id2]];
            }
            
            return Tuple.Create(mv1, mv2);
        }

        [Benchmark]
        public GaNumMultivector OrthogonalProduct_Terms()
        {
            GaNumMultivector mv = null;

            for (var id1 = 0; id1 < TermsCount; id1++)
            for (var id2 = 0; id2 < TermsCount; id2++)
                mv = ProductOrtho[_mappedTerms1[id1], _mappedTerms2[id2]];

            return mv;
        }

        [Benchmark]
        public GaNumMultivector OutputMapping_Terms()
        {
            GaNumMultivector mv = null;

            for (var id1 = 0; id1 < TermsCount; id1++)
            for (var id2 = 0; id2 < TermsCount; id2++)
                mv = _frame.BaseFrameToThisCba[_mappedTermResults[id1, id2]];

            return mv;
        }
    }
}