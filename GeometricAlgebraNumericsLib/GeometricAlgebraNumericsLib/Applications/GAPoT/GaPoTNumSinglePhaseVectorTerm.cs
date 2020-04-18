using System;

namespace GeometricAlgebraNumericsLib.Applications.GAPoT
{
    public sealed class GaPoTNumSinglePhaseVectorTerm
    {
        public static GaPoTNumSinglePhaseVectorTerm operator -(GaPoTNumSinglePhaseVectorTerm t)
        {
            return new GaPoTNumSinglePhaseVectorTerm(t.TermId, -t.Value);
        }

        public static GaPoTNumSinglePhaseVectorTerm operator +(GaPoTNumSinglePhaseVectorTerm t1, GaPoTNumSinglePhaseVectorTerm t2)
        {
            if (t1.TermId != t2.TermId)
                throw new InvalidOperationException();

            return new GaPoTNumSinglePhaseVectorTerm(t1.TermId, t1.Value + t2.Value);
        }

        public static GaPoTNumSinglePhaseVectorTerm operator -(GaPoTNumSinglePhaseVectorTerm t1, GaPoTNumSinglePhaseVectorTerm t2)
        {
            if (t1.TermId != t2.TermId)
                throw new InvalidOperationException();

            return new GaPoTNumSinglePhaseVectorTerm(t1.TermId, t1.Value - t2.Value);
        }


        public int TermId { get; }

        public double Value { get; }


        public GaPoTNumSinglePhaseVectorTerm(int id, double value)
        {
            TermId = id;
            Value = value;
        }


        public double Norm2()
        {
            return Value * Value;
        }

        public string ToText()
        {
            if (Value == 0)
                return "0";

            return $"{Value:G} <{TermId + 1}>";
        }

        public string ToLaTeX()
        {
            if (Value == 0)
                return "0";

            var valueText = GaPoTNumUtils.GetLaTeXNumber(Value);
            var basisText = GaPoTNumUtils.GetLaTeXBasisName((TermId + 1).ToString());

            return $@"\left( {valueText} \right) {basisText}";
        }
 

        public override string ToString()
        {
            return ToText();
        }
    }
}