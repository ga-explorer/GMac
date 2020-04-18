using System;
using CodeComposerLib.LaTeX;
using GeometricAlgebraNumericsLib.Applications.GAPoT;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.ExprFactory;
using Wolfram.NETLink;

namespace GeometricAlgebraSymbolicsLib.Applications.GAPoT
{
    public sealed class GaPoTSymMultiPhaseVectorTerm
    {
        public static GaPoTSymMultiPhaseVectorTerm operator -(GaPoTSymMultiPhaseVectorTerm t)
        {
            return new GaPoTSymMultiPhaseVectorTerm(
                t.PhaseId, 
                t.TermId, 
                Mfs.Minus[t.Value].GaPoTSymSimplify()
            );
        }

        public static GaPoTSymMultiPhaseVectorTerm operator +(GaPoTSymMultiPhaseVectorTerm t1, GaPoTSymMultiPhaseVectorTerm t2)
        {
            if (t1.PhaseId != t2.PhaseId || t1.TermId != t2.TermId)
                throw new InvalidOperationException();

            return new GaPoTSymMultiPhaseVectorTerm(
                t1.PhaseId, 
                t1.TermId, 
                Mfs.Plus[t1.Value, t2.Value].GaPoTSymSimplify()
            );
        }

        public static GaPoTSymMultiPhaseVectorTerm operator -(GaPoTSymMultiPhaseVectorTerm t1, GaPoTSymMultiPhaseVectorTerm t2)
        {
            if (t1.TermId != t2.TermId)
                throw new InvalidOperationException();

            return new GaPoTSymMultiPhaseVectorTerm(
                t1.PhaseId, 
                t1.TermId, 
                Mfs.Subtract[t1.Value, t2.Value].GaPoTSymSimplify()
            );
        }


        public int PhaseId { get; }

        public int TermId { get; }

        public Expr Value { get; }


        public GaPoTSymMultiPhaseVectorTerm(int phaseId, int termId, Expr value)
        {
            PhaseId = phaseId;
            TermId = termId;
            Value = value;
        }

        public GaPoTSymMultiPhaseVectorTerm(int phaseId, GaPoTSymSinglePhaseVectorTerm term)
        {
            PhaseId = phaseId;
            TermId = term.TermId;
            Value = term.Value;
        }


        public bool HasSameId(GaPoTSymMultiPhaseVectorTerm term)
        {
            return PhaseId == term.PhaseId && TermId == term.TermId;
        }

        public Expr Norm2()
        {
            return Mfs.Times[Value, Value].GaPoTSymSimplify();
        }

        public string ToText()
        {
            if (Value.IsZero())
                return "0";

            var termIdText = (TermId + 1).ToString();
            var phaseIdText = ((char)('a' + PhaseId + 1)).ToString();

            return $"[{Value} <{termIdText}>] <{phaseIdText}>";
        }

        public string ToLaTeX()
        {
            if (Value.IsZero())
                return "0";

            var valueText = Value.GetLaTeXScalar().LaTeXMathAddParentheses();
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