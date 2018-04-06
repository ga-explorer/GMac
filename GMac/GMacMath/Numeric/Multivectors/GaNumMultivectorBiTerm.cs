namespace GMac.GMacMath.Numeric.Multivectors
{
    public class GaNumMultivectorBiTerm
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
            => GMacMathUtils.IsNegativeEGp(Id1, Id2);

        public bool IsNonZeroOp
            => GMacMathUtils.IsNonZeroOp(Id1, Id2);

        public bool IsNonZeroSp
            => GMacMathUtils.IsNonZeroESp(Id1, Id2);

        public bool IsNonZeroLcp
            => GMacMathUtils.IsNonZeroELcp(Id1, Id2);

        public bool IsNonZeroRcp
            => GMacMathUtils.IsNonZeroERcp(Id1, Id2);

        public bool IsNonZeroFdp
            => GMacMathUtils.IsNonZeroEFdp(Id1, Id2);

        public bool IsNonZeroHip
            => GMacMathUtils.IsNonZeroEHip(Id1, Id2);

        public bool IsNonZeroAcp
            => GMacMathUtils.IsNonZeroEAcp(Id1, Id2);

        public bool IsNonZeroCp
            => GMacMathUtils.IsNonZeroECp(Id1, Id2);


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
