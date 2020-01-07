using BenchmarkDotNet.Attributes;
using GeometricAlgebraNumericsLib.Frames;
using GMac.GMacMath;

namespace GMacBenchmarks.Benchmarks.Symbolic
{
    public class GMacSymbolicBenchmark5
    {
        [Benchmark]
        public bool ComputedIsNegEGp()
        {
            var flag = false;

            for (var id1 = 0; id1 < 1024; id1++)
                for (var id2 = 0; id2 < 1024; id2++)
                    flag = flag | GaNumFrameUtils.IsNegativeEGp(id1, id2);

            return flag;
        }

        [Benchmark]
        public bool LookupIsNegEGp()
        {
            var flag = false;

            for (var id1 = 0; id1 < 1024; id1++)
                for (var id2 = 0; id2 < 1024; id2++)
                    flag = flag | GMacMathUtils.IsNegativeEGp(id1, id2);

            return flag;
        }
    }
}
