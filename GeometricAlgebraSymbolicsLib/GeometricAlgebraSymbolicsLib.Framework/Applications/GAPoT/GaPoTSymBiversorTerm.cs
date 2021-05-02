using System;
using System.Diagnostics;
using CodeComposerLib.LaTeX;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.ExprFactory;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.NETLink;

namespace GeometricAlgebraSymbolicsLib.Applications.GAPoT
{
    public sealed class GaPoTSymBiversorTerm
    {
        public static GaPoTSymBiversorTerm operator -(GaPoTSymBiversorTerm t)
        {
            return new GaPoTSymBiversorTerm(
                t.TermId1, 
                t.TermId2, 
                Mfs.Minus[t.Value].Evaluate()
            );
        }

        public static GaPoTSymBiversorTerm operator +(GaPoTSymBiversorTerm t1, GaPoTSymBiversorTerm t2)
        {
            if (t1.TermId1 != t2.TermId1 || t1.TermId2 != t2.TermId2)
                throw new InvalidOperationException();

            return new GaPoTSymBiversorTerm(
                t1.TermId1, 
                t1.TermId2, 
                Mfs.Plus[t1.Value, t2.Value].Evaluate()
            );
        }

        public static GaPoTSymBiversorTerm operator -(GaPoTSymBiversorTerm t1, GaPoTSymBiversorTerm t2)
        {
            if (t1.TermId1 != t2.TermId1 || t1.TermId2 != t2.TermId2)
                throw new InvalidOperationException();

            return new GaPoTSymBiversorTerm(
                t1.TermId1, 
                t1.TermId2, 
                Mfs.Subtract[t1.Value, t2.Value].Evaluate()
            );
        }

        public static GaPoTSymBiversorTerm operator *(GaPoTSymBiversorTerm t, Expr s)
        {
            return new GaPoTSymBiversorTerm(
                t.TermId1, 
                t.TermId2, 
                Mfs.Times[t.Value, s].Evaluate()
            );
        }

        public static GaPoTSymBiversorTerm operator *(Expr s, GaPoTSymBiversorTerm t)
        {
            return new GaPoTSymBiversorTerm(
                t.TermId1, 
                t.TermId2, 
                Mfs.Times[s, t.Value].Evaluate()
            );
        }

        public static GaPoTSymBiversorTerm operator /(GaPoTSymBiversorTerm t, Expr s)
        {
            return new GaPoTSymBiversorTerm(
                t.TermId1, 
                t.TermId2, 
                Mfs.Divide[t.Value, s].Evaluate()
            );
        }


        public int TermId1 { get; }

        public int TermId2 { get; }

        public Expr Value { get; }

        public bool IsScalar 
            => TermId1 == TermId2;

        public bool IsNonScalar
            => TermId1 != TermId2;

        public bool IsPhasor
            => TermId1 % 2 == 1 && TermId2 == TermId1 + 1;


        internal GaPoTSymBiversorTerm(Expr value)
        {
            TermId1 = 0;
            TermId2 = 0;
            Value = value;
        }

        internal GaPoTSymBiversorTerm(int id1, int id2, Expr value)
        {
            Debug.Assert(id1 == id2 || (id1 > 0 && id2 > 0));

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
                Value = Mfs.Minus[value].Evaluate();
            }
        }


        public Expr Norm()
        {
            return Mfs.Abs[Value].Evaluate();
        }

        public Expr Norm2()
        {
            return Mfs.Times[Value, Value].Evaluate();
        }

        public GaPoTSymBiversorTerm Reverse()
        {
            return IsScalar 
                ? this 
                : new GaPoTSymBiversorTerm(
                    TermId1, 
                    TermId2, 
                    Mfs.Minus[Value].Evaluate()
                );
        }

        public GaPoTSymBiversorTerm ScaledReverse(Expr s)
        {
            var value = IsScalar
                ? Mfs.Times[s, Value]
                : Mfs.Minus[Mfs.Times[s, Value]];

            return new GaPoTSymBiversorTerm(
                TermId1, 
                TermId2, 
                value.Evaluate()
            );
        }

        public GaPoTSymMultivectorTerm ToMultivectorTerm()
        {
            if (TermId1 == TermId2)
                return new GaPoTSymMultivectorTerm(0, Value);

            var idsPattern = (1UL << (TermId1 - 1)) + (1UL << (TermId2 - 1));

            return new GaPoTSymMultivectorTerm(
                idsPattern,
                Value
            );
        }

        public string ToText()
        {
            // if (Value.IsZero())
            //     return "0";

            return IsScalar
                ? $"'{Value}' <>"
                : $"'{Value}' <{TermId1},{TermId2}>";
        }

        public string ToLaTeX()
        {
            // if (Value.IsZero())
            //     return "0";

            var valueText = Value.GetLaTeX().LaTeXMathRoundParentheses();
            var basisText = $"{TermId1},{TermId2}".GetLaTeXBasisName();

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