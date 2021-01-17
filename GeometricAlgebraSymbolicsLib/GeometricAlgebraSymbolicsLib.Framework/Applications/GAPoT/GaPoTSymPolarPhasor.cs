using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CodeComposerLib.LaTeX;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.ExprFactory;
using Wolfram.NETLink;

namespace GeometricAlgebraSymbolicsLib.Applications.GAPoT
{
    public sealed class GaPoTSymPolarPhasor
    {
        public static GaPoTSymPolarPhasor operator -(GaPoTSymPolarPhasor p)
        {
            return new GaPoTSymPolarPhasor(
                p.Id, 
                Mfs.Minus[p.Magnitude].GaPoTSymSimplify(),
                p.Phase
            );
        }

        public static GaPoTSymPolarPhasor operator +(GaPoTSymPolarPhasor p1, GaPoTSymPolarPhasor p2)
        {
            var rp = 
                p1.ToRectPhasor() + p2.ToRectPhasor();

            return rp.ToPolarPhasor();
        }

        public static GaPoTSymPolarPhasor operator -(GaPoTSymPolarPhasor p1, GaPoTSymPolarPhasor p2)
        {
            var rp = 
                p1.ToRectPhasor() - p2.ToRectPhasor();

            return rp.ToPolarPhasor();
        }

        public static GaPoTSymPolarPhasor operator *(GaPoTSymPolarPhasor p, Expr s)
        {
            return new GaPoTSymPolarPhasor(
                p.Id, 
                Mfs.Times[p.Magnitude, s].GaPoTSymSimplify(),
                p.Phase
            );
        }

        public static GaPoTSymPolarPhasor operator *(Expr s, GaPoTSymPolarPhasor p)
        {
            return new GaPoTSymPolarPhasor(
                p.Id, 
                Mfs.Times[s, p.Magnitude].GaPoTSymSimplify(),
                p.Phase
            );
        }

        public static GaPoTSymPolarPhasor operator /(GaPoTSymPolarPhasor p, Expr s)
        {
            return new GaPoTSymPolarPhasor(
                p.Id, 
                Mfs.Divide[p.Magnitude, s].GaPoTSymSimplify(),
                p.Phase
            );
        }


        public int Id { get; }

        public Expr Magnitude { get; }

        public Expr Phase { get; }


        internal GaPoTSymPolarPhasor(int id, Expr magnitude, Expr phase)
        {
            Debug.Assert(id > 0 && id % 2 == 1);

            Id = id;
            Magnitude = magnitude;
            Phase = phase;
        }

        internal GaPoTSymPolarPhasor(int id, Expr magnitude)
        {
            Debug.Assert(id > 0 && id % 2 == 1);

            Id = id;
            Magnitude = magnitude;
            Phase = Expr.INT_ZERO;
        }


        public bool IsZero()
        {
            return Magnitude.IsZero();
        }

        public IEnumerable<GaPoTSymVectorTerm> GetTerms()
        {
            return Magnitude.IsZero()
                ? Enumerable.Empty<GaPoTSymVectorTerm>()
                : ToRectPhasor().GetTerms();
        }

        public Expr Norm()
        {
            return Mfs.Abs[Magnitude].GaPoTSymSimplify();
        }

        public Expr Norm2()
        {
            return Mfs.Times[Magnitude, Magnitude].GaPoTSymSimplify();
        }

        public GaPoTSymRectPhasor ToRectPhasor()
        {
            return new GaPoTSymRectPhasor(
                Id,
                Mfs.Times[Magnitude, Mfs.Cos[Phase]].GaPoTSymSimplify(),
                Mfs.Times[Magnitude, Mfs.Sin[Phase]].GaPoTSymSimplify()
            );
        }

        public string ToText()
        {
            // if (Magnitude.IsZero())
            //     return "0";

            var i1 = Id;
            var i2 = Id + 1;

            return $"p('{Magnitude}', '{Phase}') <{i1},{i2}>";
        }

        public string ToLaTeX()
        {
            // if (Magnitude.IsZero())
            //     return "0";

            var i1 = Id;
            var i2 = Id + 1;

            var magnitudeText = Magnitude.GetLaTeX().LaTeXMathAddParentheses();
            var phaseText = Phase.GetLaTeX().LaTeXMathAddParentheses();
            var basisText1 = $"{i1},{i2}".GetLaTeXBasisName();
            var basisText2 = $"{i1}".GetLaTeXBasisName();

            if (Phase.IsZero())
                return $@"\left( {magnitudeText} \right) {basisText2}";

            return $@"{magnitudeText} e^{{ {phaseText} {basisText1} }} {basisText2}";
        }


        public override string ToString()
        {
            return ToText();
        }
    }
}