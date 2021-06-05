using System.Collections;
using BenchmarkDotNet.Attributes;
using GeometricAlgebraNumericsLib.Multivectors;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraStructuresLib.Frames;

namespace GMac.Benchmarks.Benchmarks.Numeric
{
    /// <summary>
    /// Benchmark SignEGP(i, j) Computation methods
    /// </summary>
    public class Benchmark7
    {
        private GaNumDarMultivector mv1;
        private GaNumDarMultivector mv2;

        private BitArray[] _bitArrays;
        //private BitArray _bitArray;

        [Params(2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13)]
        public int VSpaceDim { get; set; }

        public ulong GaSpaceDim 
            => VSpaceDim.ToGaSpaceDimension();


        //private bool LookupIsNegativeEGp(int id1, int id2)
        //{
        //    return _bitArrays[id1][id2];
        //}


        [GlobalSetup]
        public void Setup()
        {
            //_bitArray = new BitArray(GaSpaceDim * GaSpaceDim);
            _bitArrays = new BitArray[GaSpaceDim];

            for (var id1 = 0UL; id1 < GaSpaceDim; id1++)
            {
                var bitArray = new BitArray((int)GaSpaceDim);

                for (var id2 = 0UL; id2 < GaSpaceDim; id2++)
                {
                    var value = GaFrameUtils.IsNegativeEGp(id1, id2);

                    bitArray[(int)id2] = value;
                    //_bitArray[(id1 << VSpaceDim) | id1] = value;
                }

                _bitArrays[id1] = bitArray;
            }

            mv1 = GaNumDarMultivector.CreateZero(VSpaceDim);
            mv2 = GaNumDarMultivector.CreateZero(VSpaceDim);
        }


        [Benchmark]
        public bool ComputedIsNegativeEGp()
        {
            var result = false;

            for (var id1 = 0UL; id1 < GaSpaceDim; id1++)
                for (var id2 = 0UL; id2 < GaSpaceDim; id2++)
                    result = GaFrameUtils.ComputeIsNegativeEGp(id1, id2);

            return result;
        }

        [Benchmark]
        public bool LookupIsNegativeEGp1()
        {
            var result = false;

            for (var id1 = 0UL; id1 < GaSpaceDim; id1++)
                for (var id2 = 0UL; id2 < GaSpaceDim; id2++)
                    result = GaFrameUtils.IsNegativeEGp(id1, id2);
                    //result = _bitArrays[id1][id2];

            return result;
        }

        //[Benchmark]
        //public bool LookupIsNegativeEGp2()
        //{
        //    var result = false;

        //    for (var id1 = 0; id1 < GaSpaceDim; id1++)
        //        for (var id2 = 0; id2 < GaSpaceDim; id2++)
        //            result = _bitArray[(id1 << VSpaceDim) | id1];

        //    return result;
        //}

        [Benchmark]
        public GaTerm<double> ComputedEGpTerms()
        {
            GaTerm<double> result = null;

            foreach (var term1 in mv1.GetStoredTerms())
            {
                foreach (var term2 in mv2.GetStoredTerms())
                {
                    var value =
                        term1.ScalarValue * term2.ScalarValue *
                        (GaFrameUtils.IsNegativeEGp(term1.BasisBladeId, term2.BasisBladeId) ? -1.0d : 1.0d);

                    result = new GaTerm<double>(term1.BasisBladeId ^ term2.BasisBladeId, value);
                }
            }

            return result;
        }
    }
}