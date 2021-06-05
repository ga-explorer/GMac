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
    /// terms
    /// </summary>
    public class Benchmark15
    {
        private GaRandomGenerator _randGen;
        private GaNumFrameNonOrthogonal _frame;
        private GaNumFrame _frameOrtho;

        private ulong _factor = 8;

        public ulong TermsCount 
            => GaSpaceDim / _factor;

        private GaNumSarMultivector[] _terms1;
        private GaNumSarMultivector[] _terms2;

        private GaNumSarMultivector[] _mappedTerms1;
        private GaNumSarMultivector[] _mappedTerms2;

        private GaNumSarMultivector[,] _mappedTermResults;
        

        //[Params(3, 4, 5, 6, 7, 8, 9, 10)]
        [Params(11, 12)]
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

            _terms1 = new GaNumSarMultivector[TermsCount];
            _terms2 = new GaNumSarMultivector[TermsCount];
            _mappedTerms1 = new GaNumSarMultivector[TermsCount];
            _mappedTerms2 = new GaNumSarMultivector[TermsCount];
            _mappedTermResults = new GaNumSarMultivector[TermsCount, TermsCount];

            for (var id = 0UL; id < TermsCount; id++)
            {
                _terms1[id] = _randGen.GetNumTerm(id * _factor).CreateSarMultivector(VSpaceDim);
                _terms2[id] = _randGen.GetNumTerm(id * _factor).CreateSarMultivector(VSpaceDim);

                _terms1[id].GetBtrRootNode();
                _terms2[id].GetBtrRootNode();

                _mappedTerms1[id] = _frame.ThisToBaseFrameCba[_terms1[id]];
                _mappedTerms2[id] = _frame.ThisToBaseFrameCba[_terms2[id]];

                _mappedTerms1[id].GetBtrRootNode();
                _mappedTerms1[id].GetBtrRootNode();
            }

            ProductOrtho = _frameOrtho.Sp;
            ProductNonOrtho = _frame.Sp;

            for (var id1 = 0UL; id1 < TermsCount; id1++)
            for (var id2 = 0UL; id2 < TermsCount; id2++)
                _mappedTermResults[id1, id2] = ProductOrtho[
                    _mappedTerms1[id1],
                    _mappedTerms2[id2]
                ];
        }


        [Benchmark]
        public GaNumSarMultivector NonOrthogonalProduct_Terms()
        {
            GaNumSarMultivector mv = null;

            for (var id1 = 0UL; id1 < TermsCount; id1++)
            for (var id2 = 0UL; id2 < TermsCount; id2++)
                mv = ProductNonOrtho[_terms1[id1], _terms2[id2]];

            return mv;
        }

        [Benchmark]
        public Tuple<GaNumSarMultivector, GaNumSarMultivector> InputsMapping_Terms()
        {
            GaNumSarMultivector mv1 = null;
            GaNumSarMultivector mv2 = null;

            for (var id1 = 0UL; id1 < TermsCount; id1++)
            for (var id2 = 0UL; id2 < TermsCount; id2++)
            {
                mv1 = _frame.ThisToBaseFrameCba[_terms1[id1]];
                mv2 = _frame.ThisToBaseFrameCba[_terms2[id2]];
            }
            
            return Tuple.Create(mv1, mv2);
        }

        [Benchmark]
        public GaNumSarMultivector OrthogonalProduct_Terms()
        {
            GaNumSarMultivector mv = null;

            for (var id1 = 0UL; id1 < TermsCount; id1++)
            for (var id2 = 0UL; id2 < TermsCount; id2++)
                mv = ProductOrtho[_mappedTerms1[id1], _mappedTerms2[id2]];

            return mv;
        }

        [Benchmark]
        public GaNumSarMultivector OutputMapping_Terms()
        {
            GaNumSarMultivector mv = null;

            for (var id1 = 0UL; id1 < TermsCount; id1++)
            for (var id2 = 0UL; id2 < TermsCount; id2++)
                mv = _frame.BaseFrameToThisCba[_mappedTermResults[id1, id2]];

            return mv;
        }
    }
}