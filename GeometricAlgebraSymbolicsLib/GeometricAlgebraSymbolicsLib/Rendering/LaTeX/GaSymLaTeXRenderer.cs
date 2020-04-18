using System.Collections.Generic;
using System.Drawing;
using GeometricAlgebraNumericsLib.Rendering.LaTeX;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.ExprFactory;
using GeometricAlgebraSymbolicsLib.Multivectors;
using TextComposerLib.Text;
using Wolfram.NETLink;

namespace GeometricAlgebraSymbolicsLib.Rendering.LaTeX
{
    public class GaSymLaTeXRenderer : GaNumLaTeXRenderer
    {
        public GaSymLaTeXRenderer(params string[] basisNames) 
            : base(basisNames)
        {
        }

        public GaSymLaTeXRenderer(GaLaTeXBasisBladeForm basisBladeForm, params string[] basisNames)
            : base(basisBladeForm, basisNames)
        {
        }


        public string FormatTerm(int id, Expr coef)
        {
            var basisBlade = FormatBasisBlade(id);
            var scalar = GaSymbolicsUtils.Cas.Connection.EvaluateToString(
                Mfs.EToString[Mfs.TeXForm[coef]]
            );

            return @" \left( " + scalar + @" \right) " + basisBlade;
        }

        public string FormatMultivector(GaSymMultivector mv)
        {
            var composer = new List<string>();

            foreach (var term in mv.NonZeroExprTerms)
            {
                var basisBlade = FormatBasisBlade(term.Key);
                var scalar = GaSymbolicsUtils.Cas.Connection.EvaluateToString(
                    Mfs.EToString[Mfs.TeXForm[term.Value]]
                );

                composer.Add(@" \left( " + scalar + @" \right) " + basisBlade);
            }

            return composer.Concatenate(" + ");
        }

        public Image RenderMultivector(GaSymMultivector mv)
        {
            return RenderMathToImage(FormatMultivector(mv));
        } 
    }
}
