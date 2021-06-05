using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeComposerLib.SyntaxTree.Expressions;
using GMac.Engine.Compiler.Semantic.ASTInterpreter.LowLevel;

namespace GMac.Engine.API.CodeBlock
{
    /// <summary>
    /// This class represents a low-level temporary (intermediate) variable in the Code Block
    /// </summary>
    public sealed class GMacCbTempVariable : GMacCbComputedVariable, IGMacCbRhsVariable
    {
        private readonly List<GMacCbComputedVariable> _userVariables = new List<GMacCbComputedVariable>();


        /// <summary>
        /// The number of times this sub-expression is used in later computations (if it's a sub-expression temp)
        /// </summary>
        public int SubExpressionUseCount { get; internal set; }

        /// <summary>
        /// True if this temp computation is a re-use of a previous temp
        /// </summary>
        public bool IsReused { get; private set; }

        /// <summary>
        /// The use index of this temp
        /// </summary>
        public int NameIndex { get; private set; }

        public override string MidLevelName 
            => NameIndex < 0 
                ? LowLevelName 
                : "temp" + NameIndex;

        /// <summary>
        /// True if this temp variable is relevant. A relevant temp is either not a factored sub-expression or
        /// is a factored, non-constant, and not a single variable sub-expression with more than one use in 
        /// following computed variables
        /// </summary>
        internal bool HasRelevantSubExpression
        {
            get
            {
                if (IsFactoredSubExpression == false)
                    return true;

                ////This condition prodeces simplest rhs expressions per computation but take a lot of time
                ////during sub-expression substitution
                //if (RhsExpr.IsSimpleConstantOrLowLevelVariable() == false)
                //    return true;

                //This condition produces arbitrarily complex rhs expressions per computation but take much less time
                //during sub-expression substitution
                if (SubExpressionUseCount > 1 && !RhsExpr.IsSimpleConstantOrLowLevelVariable())
                    return true;

                //var computationsCount = RhsExpr.ComputationsCount();

                //if (computationsCount >= 5 && computationsCount <= 15)
                //    return true;

                return
                    false;
            }
        }


        /// <summary>
        /// True if this temp variable is a factored sub-expression
        /// </summary>
        public bool IsFactoredSubExpression { get; }

        /// <summary>
        /// True if this temp variable is used for computing other variables
        /// </summary>
        public bool IsUsed => _userVariables.Count > 0;

        /// <summary>
        /// A list of other variables using this temp variable in their computations
        /// </summary>
        public IEnumerable<GMacCbComputedVariable> UserVariables => _userVariables;

        /// <summary>
        /// The last variable that used this temp variable in its computation
        /// </summary>
        public GMacCbComputedVariable LastUsingVariable => 
            _userVariables?
            .OrderByDescending(v => v.ComputationOrder)
            .FirstOrDefault();

        /// <summary>
        /// The order of the last computation that uses this temp variable
        /// </summary>
        public int LastUsingComputationOrder
        {
            get
            {
                var computedVar = LastUsingVariable;

                if (computedVar == null) return -1;

                return computedVar.ComputationOrder;
            }
        }


        internal GMacCbTempVariable(string lowLevelName, SteExpression rhsExpr, bool isFactoreSubExpression)
            : base(lowLevelName, rhsExpr)
        {
            LowLevelId = -1;
            IsFactoredSubExpression = isFactoreSubExpression;
            NameIndex = -1;
        }

        //internal GMacCbTempVariable(string lowLevelName, Expr rhsExpr, bool isFactoreSubExpression)
        //    : base(lowLevelName, rhsExpr)
        //{
        //    LowLevelId = -1;
        //    IsFactoredSubExpression = isFactoreSubExpression;
        //    NameIndex = -1;
        //}



        internal void AddUserVariable(GMacCbComputedVariable computedVar)
        {
            _userVariables.Add(computedVar);
        }

        internal void SetReuseInfo(bool isReused, int nameIndex)
        {
            IsReused = isReused;
            NameIndex = nameIndex;
        }

        internal override void ClearDependencyData()
        {
            _userVariables.Clear();

            ClearRhsVariablesList();
        }


        public override string ToString()
        {
            var s = new StringBuilder();

            s.Append(IsFactoredSubExpression ? "Sub-expression: " : "Temp: ");

            //s.Append("<")
            //    .Append(ReuseIndex)
            //    .Append("> ");

            s.Append(LowLevelName)
                .Append(" = ")
                .Append(RhsExpr);

            return s.ToString();
        }
    }
}
