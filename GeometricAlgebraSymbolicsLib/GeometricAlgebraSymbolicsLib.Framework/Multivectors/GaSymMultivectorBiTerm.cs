using GeometricAlgebraStructuresLib.Frames;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.ExprFactory;
using Wolfram.NETLink;

namespace GeometricAlgebraSymbolicsLib.Multivectors
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
            => GaFrameUtils.IsNegativeEGp(Id1, Id2);

        public bool IsNonZeroOp
            => GaFrameUtils.IsNonZeroOp(Id1, Id2);

        public bool IsNonZeroSp
            => GaFrameUtils.IsNonZeroESp(Id1, Id2);

        public bool IsNonZeroLcp
            => GaFrameUtils.IsNonZeroELcp(Id1, Id2);

        public bool IsNonZeroRcp
            => GaFrameUtils.IsNonZeroERcp(Id1, Id2);

        public bool IsNonZeroFdp
            => GaFrameUtils.IsNonZeroEFdp(Id1, Id2);

        public bool IsNonZeroHip
            => GaFrameUtils.IsNonZeroEHip(Id1, Id2);

        public bool IsNonZeroAcp
            => GaFrameUtils.IsNonZeroEAcp(Id1, Id2);

        public bool IsNonZeroCp
            => GaFrameUtils.IsNonZeroECp(Id1, Id2);


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
