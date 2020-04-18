using System;

namespace GeometricAlgebraNumericsLib.Applications.GAPoT
{
    public sealed class GaPoTNumSinglePhaseBivectorTerm
    {
        public static GaPoTNumSinglePhaseBivectorTerm operator -(GaPoTNumSinglePhaseBivectorTerm t)
        {
            return new GaPoTNumSinglePhaseBivectorTerm(t.TermId1, t.TermId2, -t.Value);
        }

        public static GaPoTNumSinglePhaseBivectorTerm operator +(GaPoTNumSinglePhaseBivectorTerm t1, GaPoTNumSinglePhaseBivectorTerm t2)
        {
            if (t1.TermId1 != t2.TermId1 || t1.TermId2 != t2.TermId2)
                throw new InvalidOperationException();

            return new GaPoTNumSinglePhaseBivectorTerm(t1.TermId1, t1.TermId2, t1.Value + t2.Value);
        }

        public static GaPoTNumSinglePhaseBivectorTerm operator -(GaPoTNumSinglePhaseBivectorTerm t1, GaPoTNumSinglePhaseBivectorTerm t2)
        {
            if (t1.TermId1 != t2.TermId1 || t1.TermId2 != t2.TermId2)
                throw new InvalidOperationException();

            return new GaPoTNumSinglePhaseBivectorTerm(t1.TermId1, t1.TermId2, t1.Value - t2.Value);
        }


        public int TermId1 { get; }

        public int TermId2 { get; }

        public double Value { get; }

        public bool IsScalar 
            => TermId1 == TermId2;

        public bool IsNonScalar
            => TermId1 != TermId2;


        public GaPoTNumSinglePhaseBivectorTerm(int id1, int id2, double value)
        {
            if (id1 == id2)
            {
                TermId1 = 0;
                TermId2 = 0;
                Value = value;
            }
            else if (id1 < id2)
            {
                TermId1 = id1;
                TermId2 = id2;
                Value = value;
            }
            else
            {
                TermId1 = id2;
                TermId2 = id1;
                Value = -value;
            }
        }


        public double Norm2()
        {
            return Value * Value;
        }

        public GaPoTNumSinglePhaseBivectorTerm Reverse()
        {
            return IsScalar 
                ? this 
                : new GaPoTNumSinglePhaseBivectorTerm(TermId1, TermId2, -Value);
        }

        public GaPoTNumSinglePhaseBivectorTerm ScaledReverse(double s)
        {
            return IsScalar 
                ? new GaPoTNumSinglePhaseBivectorTerm(TermId1, TermId2, Value * s)
                : new GaPoTNumSinglePhaseBivectorTerm(TermId1, TermId2, -Value * s);
        }


        public string ToText()
        {
            if (Value == 0)
                return "0";

            return IsScalar
                ? $"{Value:G}"
                : $"{Value:G} <{TermId1 + 1},{TermId2 + 1}>";
        }

        public string ToLaTeX()
        {
            if (Value.IsNearZero())
                return "0";

            var valueText = Value.GetLaTeXNumber();
            var basisText = $"{TermId1 + 1},{TermId2 + 1}".GetLaTeXBasisName();

            return IsScalar
                ? $@"{valueText}"
                : $@"\left( {valueText} \right) {basisText}";
        }
 

        public override string ToString()
        {
            return ToText();
        }
    }
}