using System.Collections.Generic;
using System.Linq;
using GMac.GMacAPI.CodeBlock;
using TextComposerLib.Code.SyntaxTree.Expressions;

namespace GMac.GMacCompiler.Semantic.ASTInterpreter.LowLevel.Optimizer
{
    internal sealed class TcbReduceRhsExpressions : TcbProcessor
    {
        public static GMacCodeBlock Process(GMacCodeBlock codeBlock)
        {
            var processor = new TcbReduceRhsExpressions(codeBlock);

            processor.BeginProcessing();

            return codeBlock;
        }


        /// <summary>
        /// A dictionary holding all temp variables created during this stage of optimization where the key
        /// is the final RHS expression of the temp variable
        /// </summary>
        private readonly Dictionary<string, GMacCbTempVariable> _subExpressionsDictionary =
            new Dictionary<string, GMacCbTempVariable>();

        /// <summary>
        /// A cache for speeding up the ReduceSubExpression() method
        /// </summary>
        private readonly Dictionary<string, SteExpression> _reducedSubExpressionsCache =
            new Dictionary<string, SteExpression>();

        /// <summary>
        /// The input variables of the code block
        /// </summary>
        private readonly Dictionary<string, GMacCbInputVariable> _inputVars =
            new Dictionary<string, GMacCbInputVariable>();

        /// <summary>
        /// The old temp variables of the code block
        /// </summary>
        private readonly Dictionary<string, GMacCbTempVariable> _oldTempVars =
            new Dictionary<string, GMacCbTempVariable>();

        /// <summary>
        /// The new temp variables of the code block
        /// </summary>
        private readonly Dictionary<string, GMacCbTempVariable> _newTempVars =
            new Dictionary<string, GMacCbTempVariable>();

        /// <summary>
        /// The output variables of the code block
        /// </summary>
        private readonly List<GMacCbOutputVariable> _outputVars = 
            new List<GMacCbOutputVariable>();


        private TcbReduceRhsExpressions(GMacCodeBlock codeBlock)
            : base(codeBlock)
        {
            
        }


        /// <summary>
        /// An atomic expression is a simple constant or an input variable symbol
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        private bool IsAtomicExpression(SteExpression expr)
        {
            if (expr.IsNumber)
                return true;

            if (expr.IsSymbolic == false)
                return false;

            //return LlUtils.IsLowLevelVariableName(expr.Head) && _inputVars.ContainsKey(expr.Head);
            return _inputVars.ContainsKey(expr.HeadText);
        }

        /// <summary>
        /// Analyze the contents of the initial expression. 
        /// If an atomic expression is found stop and return true.
        /// If a single temp variable is found analyze its RHS expression and iterate.
        /// If any other type of expression is found stop and return false.
        /// </summary>
        /// <param name="initialExpr"></param>
        /// <param name="finalExpr"></param>
        /// <returns></returns>
        private bool FollowExpression(SteExpression initialExpr, out SteExpression finalExpr)
        {
            //Start at the initial expression
            finalExpr = initialExpr;

            while (true)
            {
                //If the current expression is atomic return true
                if (IsAtomicExpression(finalExpr))
                    return true;

                //If the current expression is not a symbol return false
                if (finalExpr.IsSymbolic == false)
                    return false;

                //If the current expression is not a variable name symbol return false
                //if (LlUtils.IsLowLevelVariableName(finalExpr.Head) == false) 
                //    return false;

                //If the current expression is not a temp variable name symbol return false
                GMacCbTempVariable tempVar;

                if (_oldTempVars.TryGetValue(finalExpr.HeadText, out tempVar) == false) 
                    return false;

                //Make the current expression the RHS of the temp variable and continue the loop
                finalExpr = tempVar.RhsExpr;
            }
        }

        /// <summary>
        /// Find or create a temp variable holding the given expression as its RHS
        /// </summary>
        /// <param name="subExpr"></param>
        /// <returns></returns>
        private GMacCbTempVariable GetTempVariable(SteExpression subExpr)
        {
            GMacCbTempVariable tempVar;
            var subExprText = subExpr.ToString();

            //A temp is found; return it
            if (_subExpressionsDictionary.TryGetValue(subExprText, out tempVar))
                return tempVar;
            
            //A temp is not found; create it and return it
            tempVar = new GMacCbTempVariable(CodeBlock.GetNewVarName(), subExpr, true);

            _subExpressionsDictionary.Add(subExprText, tempVar);

            _newTempVars.Add(tempVar.LowLevelName, tempVar);

            return tempVar;
        }

        /// <summary>
        /// For example, convert Plus[a, b, c, d] expression into Plus[Plus[a, b, c], d]
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        private SteExpression ReshapePlus(SteExpression expr)
        {
            if (expr.ArgumentsCount < 3 || expr.HeadText != "Plus")
                return expr;

            var arg1 = SteExpressionUtils.CreateFunction(expr.HeadText, expr.Arguments.Take(expr.ArgumentsCount - 1).ToArray());
            var arg2 = expr.LastArgument;

            return SteExpressionUtils.CreateFunction(expr.HeadText, arg1, arg2);
        }

        /// <summary>
        /// Reduce a complex expression into a simpler one by refactoring all of the sub-expressions into
        /// temp variables. If the initial expression is already simple just return it as is.
        /// </summary>
        /// <param name="initialExpr"></param>
        /// <returns></returns>
        private SteExpression ReduceSubExpression(SteExpression initialExpr)
        {
            string initialExprText = initialExpr.ToString();
            SteExpression reducedExpr;

            //Try to find the initial expression in the cache
            if (_reducedSubExpressionsCache.TryGetValue(initialExprText, out reducedExpr))
                return reducedExpr;

            //An atomic expression or an undefined symbol is found for the sub-expression
            if (FollowExpression(initialExpr, out reducedExpr) || reducedExpr.IsAtomic)
            {
                _reducedSubExpressionsCache.Add(initialExprText, reducedExpr);

                return reducedExpr;
            }

            //A compound expression is found for the sub-expression
            reducedExpr = ReshapePlus(reducedExpr);

            var rhsExprNewArgs = new SteExpression[reducedExpr.ArgumentsCount];
            
            //Convert all arguments into simple constants, undefined symbols, or temp variable symbols
            for (var i = 0; i < reducedExpr.ArgumentsCount; i++)
                rhsExprNewArgs[i] = ReduceSubExpression(reducedExpr[i]);

            //Create a new RHS expression from the converted arguments
            reducedExpr = SteExpressionUtils.CreateFunction(reducedExpr.HeadText, rhsExprNewArgs);

            //Find or create a temp variable to hold the new RHS expression
            var tempVar = GetTempVariable(reducedExpr);

            //Return the final temp variable symbol expression
            reducedExpr = SteExpressionUtils.CreateVariable(tempVar.LowLevelName);

            //Add reduced expression to cache
            _reducedSubExpressionsCache.Add(initialExprText, reducedExpr);

            return reducedExpr;
        }

        private void AddOutputVariable(GMacCbOutputVariable outputVar)
        {
            SteExpression rhsExpr;

            //An atomic expression or an undefined symbol is found for the variable's RHS side
            if (FollowExpression(outputVar.RhsExpr, out rhsExpr) || rhsExpr.IsAtomic)
            {
                outputVar.RhsExpr = rhsExpr;

                _outputVars.Add(outputVar);

                return;
            }

            //A compound expression is found for the variable's RHS side
            rhsExpr = ReshapePlus(rhsExpr);

            var rhsExprNewArgs = new SteExpression[rhsExpr.ArgumentsCount];

            for (var i = 0; i < rhsExpr.ArgumentsCount; i++)
                rhsExprNewArgs[i] = ReduceSubExpression(rhsExpr[i]);

            outputVar.RhsExpr = SteExpressionUtils.CreateFunction(rhsExpr.HeadText, rhsExprNewArgs);

            _outputVars.Add(outputVar);
        }

        protected override void BeginProcessing()
        {
            //Initialize dictionaries for speeding up searches
            foreach (var inputVar in CodeBlock.InputVariables)
                _inputVars.Add(inputVar.LowLevelName, inputVar);

            foreach (var tempVar in CodeBlock.TempVariables)
                _oldTempVars.Add(tempVar.LowLevelName, tempVar);

            //Add output variables and create new temp variables
            foreach (var outputVar in CodeBlock.OutputVariables)
                AddOutputVariable(outputVar);

            CodeBlock.ComputedVariables.Clear();

            CodeBlock.ComputedVariables.AddRange(_newTempVars.Select(pair => pair.Value));
            
            CodeBlock.ComputedVariables.AddRange(_outputVars);

            TcbDependencyUpdate.Process(CodeBlock);
        }
    }
}
