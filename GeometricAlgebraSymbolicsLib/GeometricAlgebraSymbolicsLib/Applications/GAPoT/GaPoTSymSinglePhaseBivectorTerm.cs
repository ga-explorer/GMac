using System;
using CodeComposerLib.LaTeX;
using GeometricAlgebraNumericsLib.Applications.GAPoT;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.ExprFactory;
using Wolfram.NETLink;

namespace GeometricAlgebraSymbolicsLib.Applications.GAPoT
{
    public sealed class GaPoTSymSinglePhaseBivectorTerm
    {
        public static GaPoTSymSinglePhaseBivectorTerm operator -(GaPoTSymSinglePhaseBivectorTerm t)
        {
            return new GaPoTSymSinglePhaseBivectorTerm(
                t.TermId1, 
                t.TermId2, 
                Mfs.Minus[t.Value].GaPoTSymSimplify()
            );
        }

        public static GaPoTSymSinglePhaseBivectorTerm operator +(GaPoTSymSinglePhaseBivectorTerm t1, GaPoTSymSinglePhaseBivectorTerm t2)
        {
            if (t1.TermId1 != t2.TermId1 || t1.TermId2 != t2.TermId2)
                throw new InvalidOperationException();

            return new GaPoTSymSinglePhaseBivectorTerm(
                t1.TermId1, 
                t1.TermId2, 
                Mfs.Plus[t1.Value, t2.Value].GaPoTSymSimplify()
            );
        }

        public static GaPoTSymSinglePhaseBivectorTerm operator -(GaPoTSymSinglePhaseBivectorTerm t1, GaPoTSymSinglePhaseBivectorTerm t2)
        {
            if (t1.TermId1 != t2.TermId1 || t1.TermId2 != t2.TermId2)
                throw new InvalidOperationException();

            return new GaPoTSymSinglePhaseBivectorTerm(
                t1.TermId1, 
                t1.TermId2, 
                Mfs.Subtract[t1.Value, t2.Value].GaPoTSymSimplify()
            );
        }


        public int TermId1 { get; }

        public int TermId2 { get; }

        public Expr Value { get; }

        public bool IsScalar 
            => TermId1 == TermId2;

        public bool IsNonScalar
            => TermId1 != TermId2;


        public GaPoTSymSinglePhaseBivectorTerm(int id1, int id2, Expr value)
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
                Value = Mfs.Minus[value].GaPoTSymSimplify();
            }
        }


        public Expr Norm2()
        {
            return Mfs.Times[Value, Value].GaPoTSymSimplify();
        }

        public GaPoTSymSinglePhaseBivectorTerm Reverse()
        {
            return IsScalar 
                ? this 
                : new GaPoTSymSinglePhaseBivectorTerm(
                    TermId1, 
                    TermId2, 
                    Mfs.Minus[Value].GaPoTSymSimplify()
                );
        }

        public GaPoTSymSinglePhaseBivectorTerm ScaledReverse(Expr s)
        {
            var value = IsScalar
                ? Mfs.Times[s, Value]
                : Mfs.Minus[Mfs.Times[s, Value]];

            return new GaPoTSymSinglePhaseBivectorTerm(
                TermId1, 
                TermId2, 
                value.GaPoTSymSimplify()
            );
        }


        public string ToText()
        {
            if (Value.IsZero())
                return "0";

            return IsScalar
                ? $"'{Value}'"
                : $"'{Value}' <{TermId1 + 1},{TermId2 + 1}>";
        }

        public string ToLaTeX()
        {
            if (Value.IsZero())
                return "0";

            var valueText = Value.GetLaTeXScalar().LaTeXMathAddParentheses();
            var basisText = $"{TermId1 + 1},{TermId2 + 1}".GetLaTeXBasisName();

            return IsScalar
                ? $@"{valueText}"
                : $@"{valueText} {basisText}";
        }
 

        public override string ToString()
        {
            return ToText();
        }
    }
}