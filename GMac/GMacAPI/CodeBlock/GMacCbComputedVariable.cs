using System.Collections.Generic;
using System.Linq;
using TextComposerLib.Code.SyntaxTree.Expressions;
using UtilLib.DataStructures;

namespace GMac.GMacAPI.CodeBlock
{
    /// <summary>
    /// This abstract class the the base for low-level computed variables of the Code Block 
    /// (temp and output variables)
    /// </summary>
    public abstract class GMacCbComputedVariable : GMacCbVariable, IGMacCbComputedVariable
    {
        private readonly List<IGMacCbRhsVariable> _rhsVariables = new List<IGMacCbRhsVariable>();


        /// <summary>
        /// The RHS expression of the computed variable expressed in text-tree form
        /// </summary>
        public SteExpression RhsExpr { get; internal set; }

        /// <summary>
        /// The order of the computation of this computed variable
        /// </summary>
        public int ComputationOrder { get; internal set; }

        /// <summary>
        /// A list of code block variables used in the RHS expression.
        /// </summary>
        public IEnumerable<IGMacCbRhsVariable> RhsVariables => _rhsVariables;

        /// <summary>
        /// The number of code block variables used in the RHS expression (may include repeated variables).
        /// </summary>
        public int RhsVariablesCount => _rhsVariables.Count;

        /// <summary>
        /// A list of code block input variables used in the RHS expression.
        /// </summary>
        public IEnumerable<GMacCbInputVariable> RhsInputVariables
        {
            get
            {
                return
                    RhsVariables
                    .Select(item => item as GMacCbInputVariable)
                    .Where(item => ReferenceEquals(item, null) == false);
            }
        }
        
        /// <summary>
        /// A list of code block temp variables used in the RHS expression.
        /// </summary>
        public IEnumerable<GMacCbTempVariable> RhsTempVariables
        {
            get
            {
                return 
                    RhsVariables
                    .Select(item => item as GMacCbTempVariable)
                    .Where(item => ReferenceEquals(item, null) == false);
            }
        }

        /// <summary>
        /// All RHS proper sub-expressions of the RHS expression assigned to this variable
        /// </summary>
        public IEnumerable<SteExpression> RhsSubExpressions => RhsExpr
            .ProperSubExpressions
            .Reverse();

        /// <summary>
        /// True if this computed variable does not depend on any low-level variables for computing
        /// its final value
        /// </summary>
        public bool IsConstant => _rhsVariables.Count == 0;

        /// <summary>
        /// True if this is a variable or non-zero constant computed variable
        /// </summary>
        public bool IsNonZero => _rhsVariables.Count > 0 || RhsExpr.IsZero == false;

        /// <summary>
        /// True if this is a computed variable with non-zero constant RHS value
        /// </summary>
        public bool IsNonZeroConstant => _rhsVariables.Count == 0 && RhsExpr.IsZero == false;

        /// <summary>
        /// True if this is a computed variable with zero RHS value
        /// </summary>
        public bool IsZero => _rhsVariables.Count == 0 && RhsExpr.IsZero;


        //protected GMacCbComputedVariable(string lowLevelName, Expr rhsExpr)
        //    : base(lowLevelName)
        //{
        //    RhsExpr = rhsExpr.ToTextExpressionTree();
        //}

        protected GMacCbComputedVariable(string lowLevelName, SteExpression rhsExpr)
            : base(lowLevelName)
        {
            RhsExpr = rhsExpr.CreateCopy();
        }


        /// <summary>
        /// Clear the internal RHS variables list
        /// </summary>
        protected void ClearRhsVariablesList()
        {
            _rhsVariables.Clear();
        }

        /// <summary>
        /// Adds a RHS variable to the internal list
        /// </summary>
        /// <param name="rhsVar"></param>
        internal void AddRhsVariable(IGMacCbRhsVariable rhsVar)
        {
            _rhsVariables.Add(rhsVar);
        }

        /// <summary>
        /// Replace a sub-expression in the RHS expression of this computed variable by a temp variable name
        /// </summary>
        /// <param name="oldSubExpr"></param>
        /// <param name="newTempVarName"></param>
        internal void ReplaceRhsSubExpression(SteExpression oldSubExpr, string newTempVarName)
        {
            RhsExpr.ReplaceAllByVariableInPlace(oldSubExpr, newTempVarName);
        }

        /// <summary>
        /// Replace a given temp variable in the RHS expression of this computed variable by another 
        /// temp variable
        /// </summary>
        /// <param name="oldTempVar"></param>
        /// <param name="newTempVar"></param>
        internal void ReplaceRhsTempVariable(GMacCbTempVariable oldTempVar, GMacCbTempVariable newTempVar)
        {
            RhsExpr.ReplaceAllVariablesInPlace(oldTempVar.LowLevelName, newTempVar.LowLevelName);
        }


        /// <summary>
        /// Get an ordered list of all required input variables and  temp variables (i.e. intermediate computations) 
        /// necessary for this computed variable's RHS computation in whole the code block
        /// </summary>
        /// <returns></returns>
        internal IEnumerable<IGMacCbRhsVariable> GetUsedVariables()
        {
            //The final list containing the ordered temp variables
            var finalVarsList = new Dictionary<string, IGMacCbRhsVariable>();

            //Get the variables used in the RHS expression of this computed variable
            var rhsVarsList = RhsVariables.Distinct();

            var queue = new PriorityQueue<int, GMacCbTempVariable>();

            //Add the temp variables in a priority queue according to their computation order in the
            //code block
            foreach (var rhsVar in rhsVarsList)
            {
                var rhsTempVar = rhsVar as GMacCbTempVariable;

                if (ReferenceEquals(rhsTempVar, null) == false)
                {
                    queue.Enqueue(-rhsTempVar.ComputationOrder, rhsTempVar);
                    continue;
                }

                finalVarsList.Add(rhsVar.LowLevelName, rhsVar);
            }
                

            while (queue.Count > 0)
            {
                //Get the next temp variable from the queue
                var tempVar = queue.Dequeue().Value;

                //If the temp variable already exists in the final list do nothing
                if (finalVarsList.ContainsKey(tempVar.LowLevelName))
                    continue;

                //Add tempVar to the final list 
                finalVarsList.Add(tempVar.LowLevelName, tempVar);

                //Create a list of variables in the RHS of tempVar's expression but not yet
                //present in the final list
                rhsVarsList =
                    tempVar
                    .RhsVariables
                    .Distinct()
                    .Where(
                        item =>
                            finalVarsList.ContainsKey(item.LowLevelName) == false
                        );

                //Add the temp variables in the list to the priority queue and the inputs variables to
                //the final result
                foreach (var rhsVar in rhsVarsList)
                {
                    var rhsTempVar = rhsVar as GMacCbTempVariable;

                    if (ReferenceEquals(rhsTempVar, null) == false)
                    {
                        queue.Enqueue(-rhsTempVar.ComputationOrder, rhsTempVar);
                        continue;
                    }

                    finalVarsList.Add(rhsVar.LowLevelName, rhsVar);
                }
            }

            //Return the final list after reversing so that the ordering of computations is correct
            return
                finalVarsList
                    .Select(pair => pair.Value)
                    .Reverse();
        }

        /// <summary>
        /// Get an ordered list of all required input variables necessary for this computation
        /// in whole the code block
        /// </summary>
        /// <returns></returns>
        internal IEnumerable<GMacCbInputVariable> GetUsedInputVariables()
        {
            return
                GetUsedVariables()
                .Select(item => item as GMacCbInputVariable)
                .Where(item => ReferenceEquals(item, null) == false);
        }

        /// <summary>
        /// Get an ordered list of all required temp variables (i.e. intermediate computations) 
        /// necessary for this computed variable's RHS computation in whole the code block
        /// </summary>
        internal IEnumerable<GMacCbTempVariable> GetUsedTempVariables()
        {
            //The final list containing the ordered temp variables
            var tempsList = new Dictionary<string, GMacCbTempVariable>();

            //Get the temp variables used in the RHS expression of this computed variable
            var rhsTempVarsList = RhsTempVariables.Distinct();

            var queue = new PriorityQueue<int, GMacCbTempVariable>();

            //Add the temp variables in a priority queue according to their computation order in the
            //code block
            foreach (var rhsTempVar in rhsTempVarsList)
                queue.Enqueue(-rhsTempVar.ComputationOrder, rhsTempVar);

            while (queue.Count > 0)
            {
                //Get the next temp variable from the queue
                var tempVar = queue.Dequeue().Value;

                //If the temp variable already exists in the final list do nothing
                if (tempsList.ContainsKey(tempVar.LowLevelName))
                    continue;

                //Add tempVar to the final list 
                tempsList.Add(tempVar.LowLevelName, tempVar);

                //Create a list of temp variables in the RHS of tempVar's expression not yet
                //present in the final list
                rhsTempVarsList =
                    tempVar
                    .RhsTempVariables
                    .Distinct()
                    .Where(
                        item =>
                            tempsList.ContainsKey(item.LowLevelName) == false
                        );

                //Add the list to the priority queue
                foreach (var rhsTempVar in rhsTempVarsList)
                    queue.Enqueue(-rhsTempVar.ComputationOrder, rhsTempVar);
            }

            //Return the final list after reversing so that the ordering of computations is correct
            return
                tempsList
                    .Select(pair => pair.Value)
                    .Reverse();
        }
    }
}
