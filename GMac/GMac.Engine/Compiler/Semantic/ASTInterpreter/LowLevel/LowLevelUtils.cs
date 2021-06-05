using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using CodeComposerLib.Irony.Semantic.Expression.Value;
using CodeComposerLib.Irony.Semantic.Type;
using CodeComposerLib.SyntaxTree.Expressions;
using GeometricAlgebraSymbolicsLib;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;
using GMac.Engine.Compiler.Semantic.AST.Extensions;
using GMac.Engine.Compiler.Semantic.ASTInterpreter.LowLevel.Generator;
using Wolfram.NETLink;

namespace GMac.Engine.Compiler.Semantic.ASTInterpreter.LowLevel
{
    internal static class LowLevelUtils
    {
        public static readonly string LlVarNamePrefix = "LLDI";

        private static readonly Regex LlVarRegex = new Regex(@"\b" + LlVarNamePrefix + @"[0-9A-F][0-9A-F][0-9A-F][0-9A-F]\b", RegexOptions.ECMAScript);

        //private static readonly Regex LlVarOnlyRegex = new Regex(@"^\b" + LlVarNamePrefix + @"[0-9A-F][0-9A-F][0-9A-F][0-9A-F]\b$", RegexOptions.ECMAScript);


        /// <summary>
        /// Low level processing requires the use of MathematicaScalar primitive values only. This method extracts
        /// the a primitive value of the given type as a MathematicaScalar primitive value.
        /// </summary>
        /// <param name="langType"></param>
        /// <returns></returns>
        public static ValuePrimitive<MathematicaScalar> GetDefaultScalarValue(this TypePrimitive langType)
        {
            MathematicaScalar scalar;

            if (langType.IsNumber())
                scalar = MathematicaScalar.Create(GaSymbolicsUtils.Cas, 0);

            else if (langType.IsBoolean())
                scalar = MathematicaScalar.Create(GaSymbolicsUtils.Cas, "False");

            else
                throw new InvalidOperationException();

            return ValuePrimitive<MathematicaScalar>.Create(langType, scalar);
        }


        //public static bool IsSimpleConstantOrLowLevelVariable(this Expr expr)
        //{
        //    return expr.NumberQ() || (expr.SymbolQ() && IsLowLevelVariableName(expr.ToString()));
        //}

        public static bool IsSimpleConstantOrLowLevelVariable(this SteExpression expr)
        {
            return expr.IsNumber || (expr.IsSymbolic && IsLowLevelVariableName(expr.HeadText));
        }

        //public static int ComputationsCount(this Expr expr)
        //{
        //    return
        //        expr
        //        .SubExpressions()
        //        .Count(subExpr => !(subExpr.Args.Length == 0 || subExpr.IsSimpleConstantOrLowLevelVariable()));
        //}

        //public static bool IsConstantOrLowLevelVariable(this Expr expr)
        //{
        //    return
        //        LlVarOnlyRegex.IsMatch(expr.ToString()) ||
        //        (LlVarRegex.IsMatch(expr.ToString()) == false);
        //}

        public static bool IsLowLevelVariable(this SteExpression expr)
        {
            return expr.IsSymbolic && IsLowLevelVariableName(expr.HeadText);
        }

        //public static bool IsLowLevelVariable(this Expr expr)
        //{
        //    return expr.SymbolQ() && IsLowLevelVariableName(expr.ToString());
        //}

        /// <summary>
        /// True if the given symbolic scalar is actually a low-level item symbol
        /// </summary>
        /// <param name="symbolicScalar"></param>
        /// <returns></returns>
        public static bool IsLowLevelVariable(this MathematicaExpression symbolicScalar)
        {
            var expr = symbolicScalar.Expression;

            return expr.SymbolQ() && IsLowLevelVariableName(expr.ToString());
        }

        public static bool IsLowLevelVariableName(string exprText)
        {
            return 
                exprText.Length > LlVarNamePrefix.Length 
                && exprText.Substring(0, LlVarNamePrefix.Length) == LlVarNamePrefix;
        }

        public static bool UsesSingleLowLevelVariable(this MathematicaScalar symbolicScalar)
        {
            return GetLowLevelVariablesNames(symbolicScalar.Expression).Distinct().Count() == 1;
        }

        public static IEnumerable<string> GetDistinctLowLevelVariablesNames(this MathematicaScalar symbolicScalar)
        {
            return GetLowLevelVariablesNames(symbolicScalar.Expression).Distinct();
        }

        public static IEnumerable<string> GetLowLevelVariablesNames(this SteExpression expr)
        {
            //var list1 = expr.Variables().Where(IsLowLevelVariableName);

            //var s1 = list1.Distinct().OrderBy(item => item).Aggregate("", (acc, item) => acc + item + ", ");

            //var list2 = GetLowLevelVariablesNames(expr.ToString());

            //var s2 = list2.Distinct().OrderBy(item => item).Aggregate("", (acc, item) => acc + item + ", ");

            //return list1;

            return expr.Variables.Where(IsLowLevelVariableName);
        }

        public static IEnumerable<string> GetLowLevelVariablesNames(string exprText)
        {
            var matches = LlVarRegex.Matches(exprText);

            for (var i = 0; i < matches.Count; i++)
                yield return matches[i].Value;
        }


        /// <summary>
        /// Extract the names of all low-level items used inside the given symbolic scalar
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetLowLevelVariablesNames(this Expr expr)
        {
            //TODO: Is it more efficient to traverse expression tree without conversion to string?
            //return 
            //    expr
            //    .SubExpressions()
            //    .Where(subExpr => subExpr.SymbolQ() && IsLowLevelVariable(subExpr.ToString()))
            //    .Select(subExpr => subExpr.ToString());

            var matches = LlVarRegex.Matches(expr.ToString());

            for (var i = 0; i < matches.Count; i++)
                yield return matches[i].Value;
        }

        public static bool ContainsLowLevelVariables(this MathematicaScalar symbolicScalar)
        {
            return LlVarRegex.IsMatch(symbolicScalar.ExpressionText);
        }


        public static IEnumerable<LlDataItem> OrderByEvaluationOrder(this IEnumerable<LlDataItem> dataItemsList)
        {
            return 
                dataItemsList
                .OrderBy(item => item.EvaluationOrder)
                .ThenBy(item => item.ItemName);
        }
    }
}
