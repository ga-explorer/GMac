using GeometricAlgebraNumericsLib.Frames;

namespace GeometricAlgebraNumericsLib.Multivectors
{
    /// <summary>
    /// This class holds information on a pair of terms of two multivectors
    /// used for metric-based computations on multivectors
    /// </summary>
    public sealed class GaNumMultivectorBiTerm
    {
        public int Id1 { get; }

        public int Id2 { get; }

        public double Value1 { get; }

        public double Value2 { get; }

        public double MetricValue { get; }

        public double ValuesProduct
            => Value1 * Value2;

        public double TotalProduct
            => Value1 * Value2 * MetricValue;

        public int IdAnd
            => Id1 & Id2;

        public int IdXor
            => Id1 ^ Id2;

        public bool IsNegativeEGp
            => GaNumFrameUtils.IsNegativeEGp(Id1, Id2);

        public bool IsNonZeroOp
            => GaNumFrameUtils.IsNonZeroOp(Id1, Id2);

        public bool IsNonZeroSp
            => GaNumFrameUtils.IsNonZeroESp(Id1, Id2);

        public bool IsNonZeroLcp
            => GaNumFrameUtils.IsNonZeroELcp(Id1, Id2);

        public bool IsNonZeroRcp
            => GaNumFrameUtils.IsNonZeroERcp(Id1, Id2);

        public bool IsNonZeroFdp
            => GaNumFrameUtils.IsNonZeroEFdp(Id1, Id2);

        public bool IsNonZeroHip
            => GaNumFrameUtils.IsNonZeroEHip(Id1, Id2);

        public bool IsNonZeroAcp
            => GaNumFrameUtils.IsNonZeroEAcp(Id1, Id2);

        public bool IsNonZeroCp
            => GaNumFrameUtils.IsNonZeroECp(Id1, Id2);


        internal GaNumMultivectorBiTerm(int id1, int id2, double value1, double value2)
        {
            Id1 = id1;
            Id2 = id2;
            Value1 = value1;
            Value2 = value2;
            MetricValue = 1.0d;
        }

        internal GaNumMultivectorBiTerm(int id1, int id2, double coef1, double coef2, double metricValue)
        {
            Id1 = id1;
            Id2 = id2;
            Value1 = coef1;
            Value2 = coef2;
            MetricValue = metricValue;
        }
    }
}
