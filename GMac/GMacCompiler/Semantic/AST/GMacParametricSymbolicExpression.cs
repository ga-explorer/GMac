using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;
using IronyGrammars.Semantic.Operator;

namespace GMac.GMacCompiler.Semantic.AST
{
    /// <summary>
    /// Represents a polyadic operator applyed through a Mathematica scalar expression on a set of parameters
    /// for example: @'Sin[var1] * Cos[var2]' (var1 : 3.2, var2 : x). This is similar to a simplified (inline) version of a GMac macro.
    /// </summary>
    public sealed class GMacParametricSymbolicExpression : ILanguageOperator
    {
        /// <summary>
        /// The associated Mathematica scalar
        /// </summary>
        internal MathematicaScalar AssociatedMathematicaScalar { get; }


        /// <summary>
        /// The name of the operator is the mathematica scalar expression
        /// </summary>
        public string OperatorName => "@'" + AssociatedMathematicaScalar.ExpressionText + "'";


        private GMacParametricSymbolicExpression(MathematicaScalar associatedExpr)
        {
            AssociatedMathematicaScalar = associatedExpr;
        }


        public ILanguageOperator DuplicateOperator()
        {
            return this;
        }


        public override string ToString()
        {
            return "@'" + AssociatedMathematicaScalar.ExpressionText + "'";
        }


        internal static GMacParametricSymbolicExpression Create(MathematicaScalar associatedExpr)
        {
            return new GMacParametricSymbolicExpression(associatedExpr);
        }
    }
}
