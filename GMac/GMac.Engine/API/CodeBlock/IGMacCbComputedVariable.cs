using System.Collections.Generic;
using CodeComposerLib.SyntaxTree.Expressions;

namespace GMac.Engine.API.CodeBlock
{
    public interface IGMacCbComputedVariable : IGMacCbVariable
    {
        /// <summary>
        /// True if this computed variable is a constant (i.e. independent of any other variables)
        /// </summary>
        bool IsConstant { get; }

        /// <summary>
        /// The RHS expression of the computed variable expressed in text-tree form
        /// </summary>
        SteExpression RhsExpr { get; }

        /// <summary>
        /// The order of the computation of this computed variable
        /// </summary>
        int ComputationOrder { get; }

        /// <summary>
        /// A list of code block variables used in the RHS expression.
        /// </summary>
        IEnumerable<IGMacCbRhsVariable> RhsVariables { get; }

        /// <summary>
        /// A list of code block input variables used in the RHS expression.
        /// </summary>
        IEnumerable<GMacCbInputVariable> RhsInputVariables { get; }
        
        /// <summary>
        /// A list of code block temp variables used in the RHS expression.
        /// </summary>
        IEnumerable<GMacCbTempVariable> RhsTempVariables { get; }

        /// <summary>
        /// All RHS proper sub-expressions of the RHS expression assigned to this variable.
        /// </summary>
        IEnumerable<SteExpression> RhsSubExpressions { get; }

        /// <summary>
        /// The number of RHS variables for this computed variable (may include repeated ones).
        /// </summary>
        int RhsVariablesCount { get; }
    }
}