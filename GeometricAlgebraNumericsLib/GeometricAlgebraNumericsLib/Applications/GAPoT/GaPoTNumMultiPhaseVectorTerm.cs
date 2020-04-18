using System;
using CodeComposerLib.LaTeX;

namespace GeometricAlgebraNumericsLib.Applications.GAPoT
{
    public sealed class GaPoTNumMultiPhaseVectorTerm
    {
        public static GaPoTNumMultiPhaseVectorTerm operator -(GaPoTNumMultiPhaseVectorTerm t)
        {
            return new GaPoTNumMultiPhaseVectorTerm(t.PhaseId, t.TermId, -t.Value);
        }

        public static GaPoTNumMultiPhaseVectorTerm operator +(GaPoTNumMultiPhaseVectorTerm t1, GaPoTNumMultiPhaseVectorTerm t2)
        {
            if (t1.PhaseId != t2.PhaseId || t1.TermId != t2.TermId)
                throw new InvalidOperationException();

            return new GaPoTNumMultiPhaseVectorTerm(t1.PhaseId, t1.TermId, t1.Value + t2.Value);
        }

        public static GaPoTNumMultiPhaseVectorTerm operator -(GaPoTNumMultiPhaseVectorTerm t1, GaPoTNumMultiPhaseVectorTerm t2)
        {
            if (t1.TermId != t2.TermId)
                throw new InvalidOperationException();

            return new GaPoTNumMultiPhaseVectorTerm(t1.PhaseId, t1.TermId, t1.Value - t2.Value);
        }


        public int PhaseId { get; }

        public int TermId { get; }

        public double Value { get; }


        public GaPoTNumMultiPhaseVectorTerm(int phaseId, int termId, double value)
        {
            PhaseId = phaseId;
            TermId = termId;
            Value = value;
        }

        public GaPoTNumMultiPhaseVectorTerm(int phaseId, GaPoTNumSinglePhaseVectorTerm term)
        {
            PhaseId = phaseId;
            TermId = term.TermId;
            Value = term.Value;
        }


        public bool HasSameId(GaPoTNumMultiPhaseVectorTerm term)
        {
            return PhaseId == term.PhaseId && TermId == term.TermId;
        }

        public double Norm2()
        {
            return Value * Value;
        }

        public string ToText()
        {
            if (Value == 0)
                return "0";

            var termIdText = (TermId + 1).ToString();
            var phaseIdText = ((char)('a' + PhaseId + 1)).ToString();

            return $"[{Value:G} <{termIdText}>] <{phaseIdText}>";
        }

        public string ToLaTeX()
        {
            if (Value == 0)
                return "0";

            var valueText = Value.GetLaTeXNumber().LaTeXMathAddParentheses();
            var phaseBasisText = ((char)('a' + PhaseId + 1)).ToString().GetLaTeXBasisName();
            var termBasisText = (TermId + 1).ToString().GetLaTeXBasisName();

            return $@"\left[ {valueText} {termBasisText} \right] {phaseBasisText}";
        }


        public override string ToString()
        {
            return ToText();
        }
    }
}