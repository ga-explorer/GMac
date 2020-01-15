using BenchmarkDotNet.Attributes;
using GeometricAlgebraNumericsLib;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Multivectors;
using GeometricAlgebraNumericsLib.Products;

namespace GMacBenchmarks2.Benchmarks.Numeric
{
    /// <summary>
    /// Benchmark computed implementation method of standard products on orthogonal frames
    /// </summary>
    public class Benchmark2
    {
        private GaRandomGenerator _randGen;
        private GaNumFrameNonOrthogonal _frame;
        private GaNumFrame _frameOrtho;


        private GaNumMultivector[] _termsArray1;
        private GaNumMultivector[] _termsArray2;

        private GaNumMultivector[] _mappedTermsArray1;
        private GaNumMultivector[] _mappedTermsArray2;


        private GaNumMultivector[] _kVectorsArray1;
        private GaNumMultivector[] _kVectorsArray2;

        private GaNumMultivector[] _mappedKVectorsArray1;
        private GaNumMultivector[] _mappedKVectorsArray2;


        private GaNumMultivector _fullMultivector1;
        private GaNumMultivector _fullMultivector2;

        private GaNumMultivector _mappedFullMultivector1;
        private GaNumMultivector _mappedFullMultivector2;


        [Params(3, 4, 5, 6, 7, 8, 9, 10, 11, 12)]
        //[Params(12)]
        public int VSpaceDim { get; set; }
            //= 12;

        public int GaSpaceDim 
            => VSpaceDim.ToGaSpaceDimension();


        [GlobalSetup]
        public void Setup()
        {
            _randGen = new GaRandomGenerator(10);

            _frame = GaNumFrame.CreateConformal(VSpaceDim);
            _frame.SetProductsImplementation(GaBilinearProductImplementation.Computed);
            
            if (_frame.IsNonOrthogonal)
            {
                _frameOrtho = _frame.BaseOrthogonalFrame;
                _frameOrtho.SetProductsImplementation(GaBilinearProductImplementation.Computed);
            }

            var factor = 1;
            _termsArray1 = new GaNumMultivector[GaSpaceDim / factor];
            _termsArray2 = new GaNumMultivector[GaSpaceDim / factor];

            if (_frame.IsNonOrthogonal)
            {
                _mappedTermsArray1 = new GaNumMultivector[GaSpaceDim / factor];
                _mappedTermsArray2 = new GaNumMultivector[GaSpaceDim / factor];
            }

            for (var id = 0; id < GaSpaceDim; id += factor)
            {
                var idx = id / factor;

                _termsArray1[idx] = _randGen.GetNumTerm(GaSpaceDim, id);
                _termsArray2[idx] = _randGen.GetNumTerm(GaSpaceDim, id);

                _termsArray1[idx].GetInternalTermsTree();
                _termsArray2[idx].GetInternalTermsTree();


                if (_frame.IsNonOrthogonal)
                {
                    _mappedTermsArray1[idx] = _frame.ThisToBaseFrameCba[_termsArray1[idx]];
                    _mappedTermsArray2[idx] = _frame.ThisToBaseFrameCba[_termsArray2[idx]];

                    _mappedTermsArray1[idx].GetInternalTermsTree();
                    _mappedTermsArray2[idx].GetInternalTermsTree();
                }
            }

            _kVectorsArray1 = new GaNumMultivector[VSpaceDim + 1];
            _kVectorsArray2 = new GaNumMultivector[VSpaceDim + 1];

            if (_frame.IsNonOrthogonal)
            {
                _mappedKVectorsArray1 = new GaNumMultivector[VSpaceDim + 1];
                _mappedKVectorsArray2 = new GaNumMultivector[VSpaceDim + 1];
            }

            for (var grade = 0; grade <= VSpaceDim; grade++)
            {
                _kVectorsArray1[grade] = _randGen.GetNumKVector(GaSpaceDim, grade);
                _kVectorsArray2[grade] = _randGen.GetNumKVector(GaSpaceDim, grade);

                _kVectorsArray1[grade].GetInternalTermsTree();
                _kVectorsArray2[grade].GetInternalTermsTree();


                if (_frame.IsNonOrthogonal)
                {
                    _mappedKVectorsArray1[grade] = _frame.ThisToBaseFrameCba[_kVectorsArray1[grade]];
                    _mappedKVectorsArray2[grade] = _frame.ThisToBaseFrameCba[_kVectorsArray2[grade]];

                    _mappedKVectorsArray1[grade].GetInternalTermsTree();
                    _mappedKVectorsArray2[grade].GetInternalTermsTree();
                }
            }

            _fullMultivector1 = _randGen.GetNumMultivectorFull(_frame.GaSpaceDimension);
            _fullMultivector2 = _randGen.GetNumMultivectorFull(_frame.GaSpaceDimension);

            _fullMultivector1.GetInternalTermsTree();
            _fullMultivector2.GetInternalTermsTree();


            if (_frame.IsNonOrthogonal)
            {
                _mappedFullMultivector1 = _frame.ThisToBaseFrameCba[_fullMultivector1];
                _mappedFullMultivector2 = _frame.ThisToBaseFrameCba[_fullMultivector2];

                _mappedFullMultivector1.GetInternalTermsTree();
                _mappedFullMultivector2.GetInternalTermsTree();
            }
        }


        //[Benchmark]
        public GaNumMultivector Gp_Terms()
        {
            GaNumMultivector mv = null;

            foreach (var term1 in _termsArray1)
            foreach (var term2 in _termsArray2)
                mv = _frame.Gp[term1, term2];

            return mv;
        }

        //[Benchmark]
        public GaNumMultivector Gp_KVectors()
        {
            GaNumMultivector mv = null;

            for (var grade1 = 0; grade1 <= VSpaceDim; grade1++)
                for (var grade2 = 0; grade2 <= VSpaceDim; grade2++)
                    mv = _frame.Gp[_kVectorsArray1[grade1], _kVectorsArray2[grade2]];

            return mv;
        }

        //[Benchmark]
        public GaNumMultivector Gp_FullMultivectors()
        {
            return _frame.Gp[_fullMultivector1, _fullMultivector2];
        }


        //[Benchmark]
        public GaNumMultivector Op_Terms()
        {
            GaNumMultivector mv = null;

            foreach (var term1 in _termsArray1)
            foreach (var term2 in _termsArray2)
                mv = _frame.Op[term1, term2];

            return mv;
        }

        //[Benchmark]
        public GaNumMultivector Op_KVectors()
        {
            GaNumMultivector mv = null;

            for (var grade1 = 0; grade1 <= VSpaceDim; grade1++)
                for (var grade2 = 0; grade2 <= VSpaceDim; grade2++)
                    mv = _frame.Op[_kVectorsArray1[grade1], _kVectorsArray2[grade2]];

            return mv;
        }

        //[Benchmark]
        public GaNumMultivector Op_FullMultivectors()
        {
            return _frame.Op[_fullMultivector1, _fullMultivector2];
        }


        //[Benchmark]
        public GaNumMultivector Lcp_Terms()
        {
            GaNumMultivector mv = null;

            foreach (var term1 in _termsArray1)
            foreach (var term2 in _termsArray2)
                mv = _frame.Lcp[term1, term2];

            return mv;
        }

        //[Benchmark]
        public GaNumMultivector Lcp_KVectors()
        {
            GaNumMultivector mv = null;

            for (var grade1 = 0; grade1 <= VSpaceDim; grade1++)
                for (var grade2 = 0; grade2 <= VSpaceDim; grade2++)
                    mv = _frame.Lcp[_kVectorsArray1[grade1], _kVectorsArray2[grade2]];

            return mv;
        }

        //[Benchmark]
        public GaNumMultivector Lcp_FullMultivectors()
        {
            return _frame.Lcp[_fullMultivector1, _fullMultivector2];
        }


        //[Benchmark]
        public GaNumMultivector Sp_Terms()
        {
            GaNumMultivector mv = null;

            foreach (var term1 in _termsArray1)
            foreach (var term2 in _termsArray2)
                mv = _frame.Sp[term1, term2];

            return mv;
        }

        //[Benchmark]
        public GaNumMultivector Sp_KVectors()
        {
            GaNumMultivector mv = null;

            for (var grade1 = 0; grade1 <= VSpaceDim; grade1++)
                for (var grade2 = 0; grade2 <= VSpaceDim; grade2++)
                    mv = _frame.Sp[_kVectorsArray1[grade1], _kVectorsArray2[grade2]];

            return mv;
        }

        //[Benchmark]
        public GaNumMultivector Sp_FullMultivectors()
        {
            return _frame.Sp[_fullMultivector1, _fullMultivector2];
        }



        //#region Mapped Functions
        //[Benchmark]
        //public GaNumMultivector Gp_MappedTerms()
        //{
        //    GaNumMultivector mv = null;

        //    foreach (var term1 in _mappedTermsArray1)
        //        foreach (var term2 in _mappedTermsArray2)
        //            mv = _frameOrtho.Gp[term1, term2];

        //    return mv;
        //}

        //[Benchmark]
        //public GaNumMultivector Gp_MappedKVectors()
        //{
        //    GaNumMultivector mv = null;

        //    for (var grade1 = 0; grade1 <= VSpaceDim; grade1++)
        //        for (var grade2 = 0; grade2 <= VSpaceDim; grade2++)
        //            mv = _frameOrtho.Gp[_mappedKVectorsArray1[grade1], _mappedKVectorsArray2[grade2]];

        //    return mv;
        //}

        [Benchmark]
        public GaNumMultivector Gp_MappedFullMultivectors()
        {
            return _frameOrtho.Gp[_mappedFullMultivector1, _mappedFullMultivector2];
        }


        //[Benchmark]
        //public GaNumMultivector Lcp_MappedTerms()
        //{
        //    GaNumMultivector mv = null;

        //    foreach (var term1 in _mappedTermsArray1)
        //        foreach (var term2 in _mappedTermsArray2)
        //            mv = _frameOrtho.Lcp[term1, term2];

        //    return mv;
        //}

        //[Benchmark]
        //public GaNumMultivector Lcp_MappedKVectors()
        //{
        //    GaNumMultivector mv = null;

        //    for (var grade1 = 0; grade1 <= VSpaceDim; grade1++)
        //        for (var grade2 = 0; grade2 <= VSpaceDim; grade2++)
        //            mv = _frameOrtho.Lcp[_mappedKVectorsArray1[grade1], _mappedKVectorsArray2[grade2]];

        //    return mv;
        //}

        //[Benchmark]
        //public GaNumMultivector Lcp_MappedFullMultivectors()
        //{
        //    return _frameOrtho.Lcp[_mappedFullMultivector1, _mappedFullMultivector2];
        //}


        //[Benchmark]
        //public GaNumMultivector Sp_MappedTerms()
        //{
        //    GaNumMultivector mv = null;

        //    foreach (var term1 in _mappedTermsArray1)
        //        foreach (var term2 in _mappedTermsArray2)
        //            mv = _frameOrtho.Sp[term1, term2];

        //    return mv;
        //}

        //[Benchmark]
        //public GaNumMultivector Sp_MappedKVectors()
        //{
        //    GaNumMultivector mv = null;

        //    for (var grade1 = 0; grade1 <= VSpaceDim; grade1++)
        //        for (var grade2 = 0; grade2 <= VSpaceDim; grade2++)
        //            mv = _frameOrtho.Sp[_mappedKVectorsArray1[grade1], _mappedKVectorsArray2[grade2]];

        //    return mv;
        //}

        //[Benchmark]
        //public GaNumMultivector Sp_MappedFullMultivectors()
        //{
        //    return _frameOrtho.Sp[_mappedFullMultivector1, _mappedFullMultivector2];
        //}
        //#endregion
    }
}