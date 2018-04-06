using System.Text;
using GMac.GMacAST;
using GMac.GMacAST.Expressions;
using GMac.GMacMath.Symbolic;
using IronyGrammars.Semantic.Expression.Value;
using SymbolicInterface.Mathematica.Expression;
using TextComposerLib.Text.Parametric;
using Wolfram.NETLink;

namespace GMac.GMacAPI.Binding
{
    /// <summary>
    /// This immutable class represents an abstract binding pattern of a primitive sub-component of some
    /// GMac data-store to a scalar constant or variable
    /// </summary>
    public sealed class GMacScalarBinding : IGMacPrimitiveBinding, IGMacTypedBinding
    {
        internal static GMacScalarBinding CreateVariable(AstRoot rootAst)
        {
            return new GMacScalarBinding(rootAst, null);
        }

        internal static GMacScalarBinding CreateConstant(AstRoot rootAst, Expr valueExpr)
        {
            return new GMacScalarBinding(rootAst, valueExpr);
        }


        /// <summary>
        /// The GMac type of this scalar pattern
        /// </summary>
        public AstType GMacType { get; }

        /// <summary>
        /// The symbolic constant expression associated with this scalar pattern. If null the pattern is a
        /// variable binding scalar pattern
        /// </summary>
        public Expr ConstantExpr { get; }

        /// <summary>
        /// The constarnt symbolic scalar associated with this scalar pattern
        /// </summary>
        internal MathematicaScalar ConstantSymbolicScalar => IsVariable
            ? null
            : MathematicaScalar.Create(SymbolicUtils.Cas, ConstantExpr);

        /// <summary>
        /// The cosntant value associated with this scalar pattern. If null the pattern is a variable 
        /// binding scalar pattern
        /// </summary>
        public AstValueScalar ConstantValue => IsVariable
            ? null
            : new AstValueScalar(
                ValuePrimitive<MathematicaScalar>.Create(
                    GMacType.AssociatedPrimitiveType,
                    MathematicaScalar.Create(SymbolicUtils.Cas, ConstantExpr)
                    )
                );

        /// <summary>
        /// True if this is a variable scalar binding pattern
        /// </summary>
        public bool IsVariable => ConstantExpr == null;

        /// <summary>
        /// True if this is a constant scalar binding pattern
        /// </summary>
        public bool IsConstant => ConstantExpr != null;

        /// <summary>
        /// True if this is a variable or non-zero constant scalar binding pattern
        /// </summary>
        public bool IsNonZero => ConstantExpr == null || IsNonZeroConstant;

        /// <summary>
        /// True if this is a non-zero constant scalar binding pattern
        /// </summary>
        public bool IsNonZeroConstant => ReferenceEquals(ConstantExpr, null) == false &&
                                         (ConstantExpr.Args.Length == 0 && ConstantExpr.ToString() == "0") == false;

        /// <summary>
        /// True if this is a zero constant scalar binding pattern
        /// </summary>
        public bool IsZeroConstant => ReferenceEquals(ConstantExpr, null) == false &&
                                      ConstantExpr.Args.Length == 0 && ConstantExpr.ToString() == "0";

        /// <summary>
        /// True if this is a constant scalar binding pattern
        /// </summary>
        public bool HasConstantComponent => IsConstant;

        /// <summary>
        /// True if this is a variable scalar binding pattern
        /// </summary>
        public bool HasVariableComponent => IsVariable;


        private GMacScalarBinding(AstRoot rootAst, Expr valueExpr)
        {
            GMacType = rootAst.ScalarType;
            ConstantExpr = valueExpr;
        }


        /// <summary>
        /// Convert this pattern into a variable scalar pattern
        /// </summary>
        /// <returns></returns>
        public IGMacBinding ToConstantsFreePattern()
        {
            return 
                IsVariable 
                ? this 
                : new GMacScalarBinding(GMacType.Root, null);
        }

        /// <summary>
        /// Create a symbolic expression from this pattern. If the pattern is a constant its internal
        /// expression is returned, else a symbolic expression with a single variable is created and returned
        /// </summary>
        /// <param name="varNameTemplate"></param>
        /// <returns></returns>
        public Expr ToExpr(StringSequenceTemplate varNameTemplate)
        {
            return
                IsConstant
                ? ConstantExpr
                : SymbolicUtils.Cas[varNameTemplate.GenerateNextString()];
        }

        /// <summary>
        /// Create a symbolic scalar from this pattern. If the pattern is a constant its internal
        /// expression is returned, else a symbolic expression with a single variable is created and returned
        /// </summary>
        /// <param name="varNameTemplate"></param>
        /// <returns></returns>
        internal MathematicaScalar ToMathematicaScalar(StringSequenceTemplate varNameTemplate)
        {
            return 
                IsConstant 
                ? ConstantSymbolicScalar 
                : MathematicaScalar.Create(SymbolicUtils.Cas, varNameTemplate.GenerateNextString());
        }

        /// <summary>
        /// Create a symbolic value from this pattern. If the pattern is a constant its internal
        /// expression is returned, else a symbolic expression with a single variable is created and returned
        /// </summary>
        /// <param name="varNameTemplate"></param>
        /// <returns></returns>
        public AstValueScalar ToValue(StringSequenceTemplate varNameTemplate)
        {
            return 
                IsConstant
                ? ConstantValue
                : new AstValueScalar(
                    ValuePrimitive<MathematicaScalar>.Create(
                        GMacType.AssociatedPrimitiveType,
                        MathematicaScalar.Create(SymbolicUtils.Cas, varNameTemplate.GenerateNextString())
                        )
                    );
        }

        public override string ToString()
        {
            var s = new StringBuilder();

            if (IsVariable)
                s.Append("<Variable>");
            else
                s.Append("<Constant> ").Append(ConstantExpr);

            return s.ToString();
        }
    }
}
