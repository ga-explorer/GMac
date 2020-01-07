using Wolfram.NETLink;

namespace GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression
{
    public sealed class MathematicaPattern : MathematicaExpression
    {
        private MathematicaPattern(MathematicaInterface parentCas, Expr mathExpr)
            : base(parentCas, mathExpr)
        {
        }

    }
}
