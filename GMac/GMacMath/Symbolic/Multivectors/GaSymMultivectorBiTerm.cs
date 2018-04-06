using SymbolicInterface.Mathematica.ExprFactory;
using Wolfram.NETLink;

namespace GMac.GMacMath.Symbolic.Multivectors
{
    public class GaSymMultivectorBiTerm
    {
        public int Id1 { get; }

        public int Id2 { get; }

        public Expr Value1 { get; }

        public Expr Value2 { get; }

        public Expr MetricValue { get; }

        public Expr ValuesProduct
            => Mfs.Times[Value1, Value2];

        public Expr TotalProduct
            => Mfs.Times[Value1, Value2, MetricValue];

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


        internal GaSymMultivectorBiTerm(int id1, int id2, Expr value1, Expr value2)
        {
            Id1 = id1;
            Id2 = id2;
            Value1 = value1;
            Value2 = value2;
            MetricValue = Expr.INT_ONE;
        }

        internal GaSymMultivectorBiTerm(int id1, int id2, Expr coef1, Expr coef2, Expr metricValue)
        {
            Id1 = id1;
            Id2 = id2;
            Value1 = coef1;
            Value2 = coef2;
            MetricValue = metricValue;
        }
    }
}
