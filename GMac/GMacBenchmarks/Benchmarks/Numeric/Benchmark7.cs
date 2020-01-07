using System.Collections;
using BenchmarkDotNet.Attributes;
using GeometricAlgebraNumericsLib.Frames;

namespace GMacBenchmarks.Benchmarks.Numeric
{
    /// <summary>
    /// Benchmark SignEGP(i, j) Computation methods
    /// </summary>
    public class Benchmark7
    {
        private BitArray[] _bitArrays;
        //private BitArray _bitArray;

        //[Params(2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12)]
        public int VSpaceDim { get; set; } = 15;

        public int GaSpaceDim 
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

            for (var id1 = 0; id1 < GaSpaceDim; id1++)
            {
                var bitArray = new BitArray(GaSpaceDim);

                for (var id2 = 0; id2 < GaSpaceDim; id2++)
                {
                    var value = GaNumFrameUtils.IsNegativeEGp(id1, id2);

                    bitArray[id2] = value;
                    //_bitArray[(id1 << VSpaceDim) | id1] = value;
                }

                _bitArrays[id1] = bitArray;
            }
        }


        //[Benchmark]
        public bool ComputedIsNegativeEGp()
        {
            var result = false;

            for (var id1 = 0; id1 < GaSpaceDim; id1++)
                for (var id2 = 0; id2 < GaSpaceDim; id2++)
                    result = GaNumFrameUtils.IsNegativeEGp(id1, id2);

            return result;
        }

        [Benchmark]
        public bool LookupIsNegativeEGp1()
        {
            var result = false;

            for (var id1 = 0; id1 < GaSpaceDim; id1++)
                for (var id2 = 0; id2 < GaSpaceDim; id2++)
                    result = _bitArrays[id1][id2];

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
    }
}