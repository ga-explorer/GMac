using System.Text;
using GMac.GMacAST.Expressions;
using IronyGrammars.Semantic.Expression.ValueAccess;
using TextComposerLib.Code.SyntaxTree.Expressions;
using Wolfram.NETLink;

namespace GMac.GMacAPI.CodeBlock
{
    /// <summary>
    /// This class represents a low-level computed output variable in the Code Block
    /// </summary>
    public sealed class GMacCbOutputVariable : GMacCbComputedVariable, IGMacCbParameterVariable
    {
        //internal LanguageValueAccess AssociatedValueAccess 
        //{
        //    get { return ValueAccess.AssociatedValueAccess; }
        //}

        /// <summary>
        /// The primitive macro parameter component associated with this variable
        /// </summary>
        public AstDatastoreValueAccess ValueAccess { get; }

        /// <summary>
        /// A Test Value associated with this output variable for debugging purposes
        /// </summary>
        public Expr TestValueExpr { get; internal set; }

        /// <summary>
        /// The name of the primitive macro parameter component associated with this variable
        /// </summary>
        public string ValueAccessName => ValueAccess.ValueAccessName;


        internal GMacCbOutputVariable(string lowLevelName, int lowLevelId, LanguageValueAccess valueAccess, SteExpression rhsExpr)
            : base(lowLevelName, rhsExpr)
        {
            LowLevelId = lowLevelId;
            ValueAccess = valueAccess.ToAstDatastoreValueAccess();
        }

        //internal TlOutputVariable(string lowLevelName, int lowLevelId, LanguageValueAccess valueAccess, Expr rhsExpr)
        //    : base(lowLevelName, rhsExpr, true)
        //{
        //    AssociatedValueAccess = valueAccess;
        //}


        internal override void ClearDependencyData()
        {
            ClearRhsVariablesList();
        }

        public override string ToString()
        {
            var s = new StringBuilder();

            s.Append("Output: ")
                .Append(LowLevelName)
                .Append(" = ")
                .AppendLine(RhsExpr.ToString());

            return s.ToString();
        }
    }
}
