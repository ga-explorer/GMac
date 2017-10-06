using SymbolicInterface.Mathematica.ExprFactory;
using Wolfram.NETLink;

namespace SymbolicInterface.Mathematica.Expression
{
    public sealed class MathematicaScalar : MathematicaExpression
    {
        public static MathematicaScalar CreateRational(MathematicaInterface parentCas, int numerator, int denominator)
        {
            var e = parentCas[Mfs.Rational[numerator.ToExpr(), denominator.ToExpr()]];

            return new MathematicaScalar(parentCas, e);
        }

        public static MathematicaScalar CreateSymbol(MathematicaInterface parentCas, string symbolName)
        {
            return new MathematicaScalar(parentCas, symbolName.ToSymbolExpr());
        }

        public static MathematicaScalar Create(MathematicaInterface parentCas, int value)
        {
            return new MathematicaScalar(parentCas, value.ToExpr());
        }

        public static MathematicaScalar Create(MathematicaInterface parentCas, float value)
        {
            return new MathematicaScalar(parentCas, value.ToExpr());
        }

        public static MathematicaScalar Create(MathematicaInterface parentCas, double value)
        {
            return new MathematicaScalar(parentCas, value.ToExpr());
        }

        public new static MathematicaScalar Create(MathematicaInterface parentCas, Expr mathExpr)
        {
            return new MathematicaScalar(parentCas, mathExpr);
        }

        public new static MathematicaScalar Create(MathematicaInterface parentCas, string mathExprText)
        {
            return new MathematicaScalar(parentCas, parentCas.Connection.EvaluateToExpr(mathExprText));
        }


        public static MathematicaScalar operator -(MathematicaScalar e1)
        {
            var e = e1.CasInterface[Mfs.Minus[e1.MathExpr]];

            return new MathematicaScalar(e1.CasInterface, e);
        }

        public static MathematicaScalar operator +(MathematicaScalar e1, MathematicaScalar e2)
        {
            var e = e1.CasInterface[Mfs.Plus[e1.MathExpr, e2.MathExpr]];

            return new MathematicaScalar(e1.CasInterface, e);
        }

        public static MathematicaScalar operator -(MathematicaScalar e1, MathematicaScalar e2)
        {
            var e = e1.CasInterface[Mfs.Subtract[e1.MathExpr, e2.MathExpr]];

            return new MathematicaScalar(e1.CasInterface, e);
        }

        public static MathematicaScalar operator *(MathematicaScalar e1, MathematicaScalar e2)
        {
            var e = e1.CasInterface[Mfs.Times[e1.MathExpr, e2.MathExpr]];

            return new MathematicaScalar(e1.CasInterface, e);
        }

        public static MathematicaScalar operator /(MathematicaScalar e1, MathematicaScalar e2)
        {
            var e = e1.CasInterface[Mfs.Divide[e1.MathExpr, e2.MathExpr]];

            return new MathematicaScalar(e1.CasInterface, e);
        }

        public static MathematicaScalar operator ^(MathematicaScalar e1, MathematicaScalar e2)
        {
            var e = e1.CasInterface[Mfs.Power[e1.MathExpr, e2.MathExpr]];

            return new MathematicaScalar(e1.CasInterface, e);
        }


        public static MathematicaCondition operator ==(MathematicaScalar s1, MathematicaScalar s2)
        {
            if (ReferenceEquals(s1, null) || ReferenceEquals(s2, null)) return null;

            var e = s1.CasInterface[Mfs.Equal[s1.MathExpr, s2.MathExpr]];

            return MathematicaCondition.Create(s1.CasInterface, e);
        }

        public static MathematicaCondition operator !=(MathematicaScalar s1, MathematicaScalar s2)
        {
            if (ReferenceEquals(s1, null) || ReferenceEquals(s2, null)) return null;

            var e = s1.CasInterface[Mfs.Unequal[s1.MathExpr, s2.MathExpr]];

            return MathematicaCondition.Create(s1.CasInterface, e);
        }

        public static MathematicaCondition operator >(MathematicaScalar s1, MathematicaScalar s2)
        {
            var e = s1.CasInterface[Mfs.Greater[s1.MathExpr, s2.MathExpr]];

            return MathematicaCondition.Create(s1.CasInterface, e);
        }

        public static MathematicaCondition operator >=(MathematicaScalar s1, MathematicaScalar s2)
        {
            var e = s1.CasInterface[Mfs.GreaterEqual[s1.MathExpr, s2.MathExpr]];

            return MathematicaCondition.Create(s1.CasInterface, e);
        }

        public static MathematicaCondition operator <(MathematicaScalar s1, MathematicaScalar s2)
        {
            var e = s1.CasInterface[Mfs.Less[s1.MathExpr, s2.MathExpr]];

            return MathematicaCondition.Create(s1.CasInterface, e);
        }

        public static MathematicaCondition operator <=(MathematicaScalar s1, MathematicaScalar s2)
        {
            var e = s1.CasInterface[Mfs.LessEqual[s1.MathExpr, s2.MathExpr]];

            return MathematicaCondition.Create(s1.CasInterface, e);
        }


        private MathematicaScalar(MathematicaInterface parentCas, Expr mathExpr)
            : base(parentCas, mathExpr)
        {
        }


        public bool IsPossibleZero()
        {
            return CasInterface.EvalIsTrue(Mfs.PossibleZeroQ[MathExpr]);
        }

        public bool IsZero()
        {
            return MathExpr.NumberQ() && MathExpr.ToString() == "0";
        }

        public bool IsOne()
        {
            return MathExpr.NumberQ() && MathExpr.ToString() == "1";
        }

        public bool IsMinusOne()
        {
            return MathExpr.NumberQ() && MathExpr.ToString() == "-1";
        }

        public bool IsEqualZero()
        {
            return CasInterface.EvalTrueQ(Mfs.Equal[MathExpr, CasConstants.ExprZero]);
        }

        public bool IsPossibleScalar(MathematicaScalar expr)
        {
            return CasInterface.EvalIsTrue(Mfs.PossibleZeroQ[Mfs.Subtract[MathExpr, expr.MathExpr]]);
        }

        //TODO: Add more overloads to accept integers
        public bool IsEqualScalar(MathematicaScalar expr)
        {
            return CasInterface.EvalTrueQ(Mfs.Equal[MathExpr, expr.MathExpr]);
        }

        public bool IsConstant()
        {
            return CasInterface.EvalTrueQ(Mfs.NumericQ[MathExpr]);
        }

        public bool IsNonZeroRealConstant()
        {
            return CasInterface.EvalTrueQ(
                Mfs.And[
                    Mfs.NumericQ[MathExpr],
                    Mfs.Element[MathExpr, DomainSymbols.Reals],
                    Mfs.Not[Mfs.PossibleZeroQ[MathExpr]]
                ]
            );
        }


        public MathematicaScalar N()
        {
            var e = CasInterface[Mfs.N[MathExpr]];

            return new MathematicaScalar(CasInterface, e);
        }

        public MathematicaScalar N(int percision)
        {
            var e = CasInterface[Mfs.N[MathExpr, percision.ToExpr()]];

            return new MathematicaScalar(CasInterface, e);
        }

        public MathematicaScalar Abs()
        {
            var e = CasInterface[Mfs.Abs[MathExpr]];

            return new MathematicaScalar(CasInterface, e);
        }

        public MathematicaScalar Sqrt()
        {
            var e = CasInterface[Mfs.Sqrt[MathExpr]];

            return new MathematicaScalar(CasInterface, e);
        }

        public MathematicaScalar Sin()
        {
            var e = CasInterface[Mfs.Sin[MathExpr]];

            return new MathematicaScalar(CasInterface, e);
        }

        public MathematicaScalar Cos()
        {
            var e = CasInterface[Mfs.Cos[MathExpr]];

            return new MathematicaScalar(CasInterface, e);
        }

        public MathematicaScalar Tan()
        {
            var e = CasInterface[Mfs.Tan[MathExpr]];

            return new MathematicaScalar(CasInterface, e);
        }

        public MathematicaScalar Sinh()
        {
            var e = CasInterface[Mfs.Sinh[MathExpr]];

            return new MathematicaScalar(CasInterface, e);
        }

        public MathematicaScalar Cosh()
        {
            var e = CasInterface[Mfs.Cosh[MathExpr]];

            return new MathematicaScalar(CasInterface, e);
        }

        public MathematicaScalar Tanh()
        {
            var e = CasInterface[Mfs.Tanh[MathExpr]];

            return new MathematicaScalar(CasInterface, e);
        }

        public MathematicaScalar Log()
        {
            var e = CasInterface[Mfs.Log[MathExpr]];

            return new MathematicaScalar(CasInterface, e);
        }

        public MathematicaScalar Log10()
        {
            var e = CasInterface[Mfs.Log10[MathExpr]];

            return new MathematicaScalar(CasInterface, e);
        }

        public MathematicaScalar Log2()
        {
            var e = CasInterface[Mfs.Log2[MathExpr]];

            return new MathematicaScalar(CasInterface, e);
        }

        public MathematicaScalar Exp()
        {
            var e = CasInterface[Mfs.Exp[MathExpr]];

            return new MathematicaScalar(CasInterface, e);
        }

        public MathematicaScalar DiffBy(MathematicaScalar expr2)
        {
            var e = CasInterface[Mfs.D[MathExpr, expr2.MathExpr]];

            return new MathematicaScalar(CasInterface, e);
        }
    }
}
