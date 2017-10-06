using SymbolicInterface.Mathematica.ExprFactory;
using Wolfram.NETLink;

namespace SymbolicInterface.Mathematica.Expression
{
    public sealed class MathematicaRule : MathematicaExpression
    {
        public static MathematicaRule Create(MathematicaInterface parentCas, Expr lhsMathExpr, Expr rhsMathExpr)
        {
            var e = parentCas[Mfs.Rule[lhsMathExpr, rhsMathExpr]];

            return new MathematicaRule(parentCas, e);
        }

        public static MathematicaRule CreateDelayed(MathematicaInterface parentCas, Expr lhsMathExpr, Expr rhsMathExpr)
        {
            var e = parentCas[Mfs.RuleDelayed[lhsMathExpr, rhsMathExpr]];

            return new MathematicaRule(parentCas, e);
        }


        public bool IsImmediate => MathExpr.Head.ToString() == Mfs.Rule.ToString();

        public bool IsDelayed => MathExpr.Head.ToString() == Mfs.RuleDelayed.ToString();

        public Expr LhsExpr => MathExpr.Args[0];

        public Expr RhsExpr => MathExpr.Args[1];


        private MathematicaRule(MathematicaInterface parentCas, Expr mathExpr)
            : base(parentCas, mathExpr)
        {
        }


    }
}
