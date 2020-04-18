using System;
using CodeComposerLib.LaTeX;
using GeometricAlgebraNumericsLib.Applications.GAPoT;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.ExprFactory;
using Wolfram.NETLink;

namespace GeometricAlgebraSymbolicsLib.Applications.GAPoT
{
    public sealed class GaPoTSymSinglePhaseVectorTerm
    {
        public static GaPoTSymSinglePhaseVectorTerm operator -(GaPoTSymSinglePhaseVectorTerm t)
        {
            return new GaPoTSymSinglePhaseVectorTerm(
                t.TermId, 
                Mfs.Minus[t.Value].GaPoTSymSimplify()
            );
        }

        public static GaPoTSymSinglePhaseVectorTerm operator +(GaPoTSymSinglePhaseVectorTerm t1, GaPoTSymSinglePhaseVectorTerm t2)
        {
            if (t1.TermId != t2.TermId)
                throw new InvalidOperationException();

            return new GaPoTSymSinglePhaseVectorTerm(
                t1.TermId, 
                Mfs.Plus[t1.Value, t2.Value].GaPoTSymSimplify()
            );
        }

        public static GaPoTSymSinglePhaseVectorTerm operator -(GaPoTSymSinglePhaseVectorTerm t1, GaPoTSymSinglePhaseVectorTerm t2)
        {
            if (t1.TermId != t2.TermId)
                throw new InvalidOperationException();

            return new GaPoTSymSinglePhaseVectorTerm(
                t1.TermId, 
                Mfs.Subtract[t1.Value, t2.Value].GaPoTSymSimplify()
            );
        }


        public int TermId { get; }

        public Expr Value { get; }


        internal GaPoTSymSinglePhaseVectorTerm(int id, Expr value)
        {
            TermId = id;
            Value = value;
        }


        public Expr Norm2()
        {
            return Mfs.Times[Value, Value].GaPoTSymSimplify();
        }

        public string ToText()
        {
            if (Value.IsZero())
                return "0";

            return $"'{Value}' <{TermId + 1}>";
        }

        public string ToLaTeX()
        {
            if (Value.IsZero())
                return "0";

            var valueText = Value.GetLaTeXScalar().LaTeXMathAddParentheses();
            var basisText = (TermId + 1).ToString().GetLaTeXBasisName();

            return $@"{valueText} {basisText}";
        }
 

        public override string ToString()
        {
            return ToText();
        }
    }
}