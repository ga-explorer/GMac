using System;
using System.Collections.Generic;
using System.Diagnostics;
using CodeComposerLib.LaTeX;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.ExprFactory;
using Wolfram.NETLink;

namespace GeometricAlgebraSymbolicsLib.Applications.GAPoT
{
    public sealed class GaPoTSymRectPhasor
    {
        public static GaPoTSymRectPhasor operator -(GaPoTSymRectPhasor p)
        {
            return new GaPoTSymRectPhasor(
                p.Id,
                Mfs.Minus[p.XValue].Evaluate(),
                Mfs.Minus[p.YValue].Evaluate()
            );
        }

        public static GaPoTSymRectPhasor operator +(GaPoTSymRectPhasor p1, GaPoTSymRectPhasor p2)
        {
            if (p1.Id != p2.Id)
                throw new InvalidOperationException();

            return new GaPoTSymRectPhasor(
                p1.Id,
                Mfs.Plus[p1.XValue, p2.XValue].Evaluate(),
                Mfs.Plus[p1.YValue, p2.YValue].Evaluate()
            );
        }

        public static GaPoTSymRectPhasor operator -(GaPoTSymRectPhasor p1, GaPoTSymRectPhasor p2)
        {
            if (p1.Id != p2.Id)
                throw new InvalidOperationException();

            return new GaPoTSymRectPhasor(
                p1.Id,
                Mfs.Subtract[p1.XValue, p2.XValue].Evaluate(),
                Mfs.Subtract[p1.YValue, p2.YValue].Evaluate()
            );
        }

        public static GaPoTSymRectPhasor operator *(GaPoTSymRectPhasor p, Expr s)
        {
            return new GaPoTSymRectPhasor(
                p.Id,
                Mfs.Times[p.XValue, s].Evaluate(),
                Mfs.Times[p.YValue, s].Evaluate()
            );
        }

        public static GaPoTSymRectPhasor operator *(Expr s, GaPoTSymRectPhasor p)
        {
            return new GaPoTSymRectPhasor(
                p.Id,
                Mfs.Times[s, p.XValue].Evaluate(),
                Mfs.Times[s, p.YValue].Evaluate()
            );
        }

        public static GaPoTSymRectPhasor operator /(GaPoTSymRectPhasor p, Expr s)
        {
            return new GaPoTSymRectPhasor(
                p.Id,
                Mfs.Divide[p.XValue, s].Evaluate(),
                Mfs.Divide[p.YValue, s].Evaluate()
            );
        }


        public int Id { get; }

        public Expr XValue { get; set; }

        public Expr YValue { get; set; }


        internal GaPoTSymRectPhasor(int id, Expr x, Expr y)
        {
            Debug.Assert(id > 0 && id % 2 == 1);

            Id = id;
            XValue = x;
            YValue = y;
        }

        internal GaPoTSymRectPhasor(int id, Expr x)
        {
            Debug.Assert(id > 0 && id % 2 == 1);

            Id = id;
            XValue = x;
            YValue = Expr.INT_ZERO;
        }

        
        public bool IsZero()
        {
            return XValue.IsZero() && YValue.IsZero();
        }

        public IEnumerable<GaPoTSymVectorTerm> GetTerms()
        {
            if (!XValue.IsZero())
                yield return new GaPoTSymVectorTerm(
                    Id,
                    XValue
                );

            if (!YValue.IsZero())
                yield return new GaPoTSymVectorTerm(
                    Id + 1,
                    Mfs.Minus[YValue].Evaluate()
                );
        }

        public Expr Norm()
        {
            return Mfs.Sqrt[Mfs.Plus[
                Mfs.Times[XValue, XValue],
                Mfs.Times[YValue, YValue]
            ]].Evaluate();
        }

        public Expr Norm2()
        {
            return Mfs.Plus[
                Mfs.Times[XValue, XValue],
                Mfs.Times[YValue, YValue]
            ].Evaluate();
        }

        public GaPoTSymPolarPhasor ToPolarPhasor()
        {
            var magnitude = 
                Mfs.Sqrt[Mfs.Plus[
                    Mfs.Times[XValue, XValue],
                    Mfs.Times[YValue, YValue]
                ]].Evaluate();

            var phase = 
                Mfs.ArcTan[XValue, YValue].Evaluate();

            return new GaPoTSymPolarPhasor(Id, magnitude, phase);
        }
        
        public string ToText()
        {
            // if (XValue.IsZero() && YValue.IsZero())
            //     return "0";

            var i1 = Id;
            var i2 = Id + 1;

            return $"r('{XValue}', '{YValue}') <{i1},{i2}>";
        }

        public string ToLaTeX()
        {
            // if (XValue.IsZero() && YValue.IsZero())
            //     return "0";

            var i1 = Id;
            var i2 = Id + 1;

            var xValueText = XValue.GetLaTeX().LaTeXMathRoundParentheses();
            var yValueText = YValue.GetLaTeX().LaTeXMathRoundParentheses();
            var basisText1 = $"{i1},{i2}".GetLaTeXBasisName();
            var basisText2 = $"{i1}".GetLaTeXBasisName();

            // if (XValue.IsZero())
            //     return $@"\left( {yValueText} \right) {basisText1} {basisText2}";
            //
            // if (YValue.IsZero())
            //     return $@"{xValueText} {basisText2}";

            return $@"\left[ {xValueText} + {yValueText} {basisText1} \right] {basisText2}";
        }


        public override string ToString()
        {
            return ToText();
        }
    }
}