using Wolfram.NETLink;

namespace SymbolicInterface.Mathematica.ExprFactory
{
    public static class OptionSymbols
    {
        public static readonly Expr All = new Expr(ExpressionType.Symbol, "All");
    }
}
