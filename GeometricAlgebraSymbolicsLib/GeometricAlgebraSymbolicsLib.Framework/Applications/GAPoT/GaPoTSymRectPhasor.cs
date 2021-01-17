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
                Mfs.Minus[p.XValue].GaPoTSymSimplify(),
                Mfs.Minus[p.YValue].GaPoTSymSimplify()
            );
        }

        public static GaPoTSymRectPhasor operator +(GaPoTSymRectPhasor p1, GaPoTSymRectPhasor p2)
        {
            if (p1.Id != p2.Id)
                throw new InvalidOperationException();

            return new GaPoTSymRectPhasor(
                p1.Id,
                Mfs.Plus[p1.XValue, p2.XValue].GaPoTSymSimplify(),
                Mfs.Plus[p1.YValue, p2.YValue].GaPoTSymSimplify()
            );
        }

        public static GaPoTSymRectPhasor operator -(GaPoTSymRectPhasor p1, GaPoTSymRectPhasor p2)
        {
            if (p1.Id != p2.Id)
                throw new InvalidOperationException();

            return new GaPoTSymRectPhasor(
                p1.Id,
                Mfs.Subtract[p1.XValue, p2.XValue].GaPoTSymSimplify(),
                Mfs.Subtract[p1.YValue, p2.YValue].GaPoTSymSimplify()
            );
        }

        public static GaPoTSymRectPhasor operator *(GaPoTSymRectPhasor p, Expr s)
        {
            return new GaPoTSymRectPhasor(
                p.Id,
                Mfs.Times[p.XValue, s].GaPoTSymSimplify(),
                Mfs.Times[p.YValue, s].GaPoTSymSimplify()
            );
        }

        public static GaPoTSymRectPhasor operator *(Expr s, GaPoTSymRectPhasor p)
        {
            return new GaPoTSymRectPhasor(
                p.Id,
                Mfs.Times[s, p.XValue].GaPoTSymSimplify(),
                Mfs.Times[s, p.YValue].GaPoTSymSimplify()
            );
        }

        public static GaPoTSymRectPhasor operator /(GaPoTSymRectPhasor p, Expr s)
        {
            return new GaPoTSymRectPhasor(
                p.Id,
                Mfs.Divide[p.XValue, s].GaPoTSymSimplify(),
                Mfs.Divide[p.YValue, s].GaPoTSymSimplify()
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
                    Mfs.Minus[YValue].GaPoTSymSimplify()
                );
        }

        public Expr Norm()
        {
            return Mfs.Sqrt[Mfs.Plus[
                Mfs.Times[XValue, XValue],
                Mfs.Times[YValue, YValue]
            ]].GaPoTSymSimplify();
        }

        public Expr Norm2()
        {
            return Mfs.Plus[
                Mfs.Times[XValue, XValue],
                Mfs.Times[YValue, YValue]
            ].GaPoTSymSimplify();
        }

        public GaPoTSymPolarPhasor ToPolarPhasor()
        {
            var magnitude = 
                Mfs.Sqrt[Mfs.Plus[
                    Mfs.Times[XValue, XValue],
                    Mfs.Times[YValue, YValue]
                ]].GaPoTSymSimplify();

            var phase = 
                Mfs.ArcTan[XValue, YValue].GaPoTSymSimplify();

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

            var xValueText = XValue.GetLaTeX().LaTeXMathAddParentheses();
            var yValueText = YValue.GetLaTeX().LaTeXMathAddParentheses();
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