﻿using System;
using System.Collections.Generic;
using System.Linq;
using SymbolicInterface.Mathematica.Expression;
using SymbolicInterface.Mathematica.ExprFactory;
using Wolfram.NETLink;

namespace SymbolicInterface.Mathematica
{
    public static class MathematicaUtils
    {
        /// <summary>
        /// Create a Mathematica Expr object from the given value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Expr ToExpr(this bool value)
        {
            return new Expr(ExpressionType.Boolean, value ? "True" : "False");
        }

        /// <summary>
        /// Create a Mathematica Expr object from the given value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Expr ToExpr(this int value)
        {
            return new Expr(value);
        }

        /// <summary>
        /// Create a Mathematica Expr object from the given value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Expr ToExpr(this float value)
        {
            return new Expr(value);
        }

        /// <summary>
        /// Create a Mathematica Expr object from the given value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Expr ToExpr(this double value)
        {
            return new Expr(value);
        }

        /// <summary>
        /// Create a Mathematica Expr object from the given text expression using the Mathematica interface evaluator
        /// </summary>
        /// <param name="value"></param>
        /// <param name="mathematicaInterface"></param>
        /// <returns></returns>
        public static Expr ToExpr(this string value, MathematicaInterface mathematicaInterface)
        {
            return mathematicaInterface.Connection.EvaluateToExpr(value);
        }

        /// <summary>
        /// Create a Mathematica Expr object from the given symbol name
        /// </summary>
        /// <param name="symbolName"></param>
        /// <returns></returns>
        public static Expr ToSymbolExpr(this string symbolName)
        {
            return new Expr(ExpressionType.Symbol, symbolName);
        }

        /// <summary>
        /// Create a list of Mathematica Expr objects from the given symbol names
        /// </summary>
        /// <param name="symbolNames"></param>
        /// <returns></returns>
        public static IEnumerable<Expr> ToSymbolExprList(this IEnumerable<string> symbolNames)
        {
            return symbolNames.Select(symbolName => new Expr(ExpressionType.Symbol, symbolName));
        }

        /// <summary>
        /// Construct an Expr object from a head expression and some arguments
        /// </summary>
        /// <param name="funcNameSymbol"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static Expr ApplyTo(this Expr funcNameSymbol, params object[] args)
        {
            return new Expr(funcNameSymbol, args);
        }

        /// <summary>
        /// Construct an Expr object from a head symbol string and some arguments
        /// </summary>
        /// <param name="funcName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static Expr ApplyTo(this string funcName, params object[] args)
        {
            return new Expr(new Expr(ExpressionType.Symbol, funcName), args);
        }

        /// <summary>
        /// Get a list of all sub-expressions of the given expression using depth-first order. The original
        /// expression is the first on the list
        /// </summary>
        /// <param name="rootExpr"></param>
        /// <returns></returns>
        public static IEnumerable<Expr> SubExpressions(this Expr rootExpr)
        {
            if (ReferenceEquals(rootExpr, null))
                throw new ArgumentNullException(nameof(rootExpr));

            yield return rootExpr;

            if (rootExpr.Args.Length == 0)
                yield break;

            var stack = new Stack<Expr>(rootExpr.Args);

            while (stack.Count > 0)
            {
                var expr = stack.Pop();

                yield return expr;

                if (expr.Args.Length == 0)
                    continue;

                foreach (var subExpr in expr.Args)
                    stack.Push(subExpr);
            }
        }

        /// <summary>
        /// Get a list of all sub-expressions of the given expression using depth-first order. 
        /// The original expression is not included in the list
        /// </summary>
        /// <param name="rootExpr"></param>
        /// <returns></returns>
        public static IEnumerable<Expr> ProperSubExpressions(this Expr rootExpr)
        {
            if (ReferenceEquals(rootExpr, null))
                throw new ArgumentNullException(nameof(rootExpr));

            if (rootExpr.Args.Length == 0)
                yield break;

            var stack = new Stack<Expr>(rootExpr.Args);

            while (stack.Count > 0)
            {
                var expr = stack.Pop();

                yield return expr;

                if (expr.Args.Length == 0)
                    continue;

                foreach (var subExpr in expr.Args)
                    stack.Push(subExpr);
            }
        }

        /// <summary>
        /// Get a list of all sub-expressions of the given expression using depth-first order. 
        /// The sub-expression is accepted for output and traversal if skipFunc is false for that sub-expression.
        /// If a sub-expression is skiped so is all its sub-expressions
        /// </summary>
        /// <param name="rootExpr"></param>
        /// <param name="skipFunc"></param>
        /// <returns></returns>
        public static IEnumerable<Expr> SubExpressions(this Expr rootExpr, Func<Expr, bool> skipFunc)
        {
            if (ReferenceEquals(rootExpr, null))
                throw new ArgumentNullException(nameof(rootExpr));

            if (skipFunc(rootExpr))
                yield break;

            yield return rootExpr;

            var stack = new Stack<Expr>(rootExpr.Args);

            while (stack.Count > 0)
            {
                var expr = stack.Pop();

                if (skipFunc(rootExpr))
                    continue;

                yield return expr;

                foreach (var subExpr in expr.Args)
                    stack.Push(subExpr);
            }
        }

        /// <summary>
        /// Get a list of all sub-expressions of the given expression using depth-first order. 
        /// The root expression is not included in the list.
        /// The sub-expression is accepted for output and traversal if skipFunc is false for that sub-expression.
        /// If a sub-expression is skiped so is all its sub-expressions
        /// </summary>
        /// <param name="rootExpr"></param>
        /// <param name="skipFunc"></param>
        /// <returns></returns>
        public static IEnumerable<Expr> ProperSubExpressions(this Expr rootExpr, Func<Expr, bool> skipFunc)
        {
            if (ReferenceEquals(rootExpr, null))
                throw new ArgumentNullException(nameof(rootExpr));

            if (skipFunc(rootExpr))
                yield break;

            var stack = new Stack<Expr>(rootExpr.Args);

            while (stack.Count > 0)
            {
                var expr = stack.Pop();

                if (skipFunc(rootExpr))
                    continue;

                yield return expr;

                foreach (var subExpr in expr.Args)
                    stack.Push(subExpr);
            }
        }

        
        
        /// <summary>
        /// Evaluates the given expression and if the result is 'True' it returns true
        /// If the result is anything else it returns false and raises no errors
        /// </summary>
        /// <param name="casInterface"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public static bool EvalTrueQ(this MathematicaInterface casInterface, Expr e)
        {
            return casInterface.Connection.EvaluateToExpr(e).ToString() == "True";
        }

        /// <summary>
        /// Evaluates the given expression and if the result is 'False' it returns true
        /// If the result is anything else it returns false and raises no errors
        /// </summary>
        /// <param name="casInterface"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public static bool EvalFalseQ(this MathematicaInterface casInterface, Expr e)
        {
            return casInterface.Connection.EvaluateToExpr(e).ToString() == "False";
        }

        /// <summary>
        /// Evaluates the given expression and if the result is 'True' it returns true
        /// If the result is 'False' it returns false
        /// If the result is anything else it raises an error
        /// </summary>
        /// <param name="casInterface"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public static bool EvalIsTrue(this MathematicaInterface casInterface, Expr e)
        {
            var result = casInterface.Connection.EvaluateToExpr(e).ToString();

            switch (result)
            {
                case "True":
                    return true;

                case "False":
                    return false;
            }

            throw new InvalidOperationException("Expression did not evaluate to a constant boolean value");
        }

        /// <summary>
        /// Evaluates the given expression and if the result is 'False' it returns true
        /// If the result is 'True' it returns false
        /// If the result is anything else it raises an error
        /// </summary>
        /// <param name="casInterface"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public static bool EvalIsFalse(this MathematicaInterface casInterface, Expr e)
        {
            var result = casInterface.Connection.EvaluateToExpr(e).ToString();

            switch (result)
            {
                case "True":
                    return false;

                case "False":
                    return true;
            }

            throw new InvalidOperationException("Expression did not evaluate to a constant boolean value");
        }


        private static Expr CreateElementExpr(List<Expr> items, Expr domainNameSymbol)
        {
            if (items.Count == 1)
                return Mfs.Element[items[0], domainNameSymbol];

            return
                items.Count > 1
                ? Mfs.Element[Mfs.Alternatives[items.Cast<object>().ToArray()], domainNameSymbol]
                : null;
        }

        public static Expr CreateAssumeExpr(this MathematicaInterface parentCas, Dictionary<string, MathematicaAtomicType> varTypes)
        {
            var complexesList = new List<Expr>();
            var realsList = new List<Expr>();
            var rationalsList = new List<Expr>();
            var integersList = new List<Expr>();
            var booleansList = new List<Expr>();

            foreach (var pair in varTypes)
            {
                switch (pair.Value)
                {
                    case MathematicaAtomicType.Complex:
                        complexesList.Add(pair.Key.ToSymbolExpr());
                        break;

                    case MathematicaAtomicType.Real:
                        realsList.Add(pair.Key.ToSymbolExpr());
                        break;

                    case MathematicaAtomicType.Rational:
                        rationalsList.Add(pair.Key.ToSymbolExpr());
                        break;

                    case MathematicaAtomicType.Integer:
                        integersList.Add(pair.Key.ToSymbolExpr());
                        break;

                    case MathematicaAtomicType.Boolean:
                        booleansList.Add(pair.Key.ToSymbolExpr());
                        break;
                }
            }

            var domainElementsExpr = new List<Expr>(4);

            if (complexesList.Count > 0)
                domainElementsExpr.Add(CreateElementExpr(complexesList, DomainSymbols.Complexes));

            if (realsList.Count > 0)
                domainElementsExpr.Add(CreateElementExpr(realsList, DomainSymbols.Reals));

            if (rationalsList.Count > 0)
                domainElementsExpr.Add(CreateElementExpr(rationalsList, DomainSymbols.Rationals));

            if (integersList.Count > 0)
                domainElementsExpr.Add(CreateElementExpr(integersList, DomainSymbols.Integers));

            if (booleansList.Count > 0)
                domainElementsExpr.Add(CreateElementExpr(booleansList, DomainSymbols.Booleans));

            if (domainElementsExpr.Count == 0)
                return null;

            var expr = domainElementsExpr.Count == 1
                ? parentCas[domainElementsExpr[0]]
                : parentCas[Mfs.And[domainElementsExpr.Cast<object>().ToArray()]];

            return expr;
        }

        public static bool IsBooleanScalar(this MathematicaScalar sc, Expr assumeExpr)
        {
            var cond = MathematicaCondition.CreateIsDomainMemberTest(sc, DomainSymbols.Booleans, assumeExpr);

            return cond.IsConstantTrue();
        }

        public static bool IsIntegerScalar(this MathematicaScalar sc, Expr assumeExpr)
        {
            var cond = MathematicaCondition.CreateIsDomainMemberTest(sc, DomainSymbols.Integers, assumeExpr);

            return cond.IsConstantTrue();
        }

        public static bool IsRealScalar(this MathematicaScalar sc, Expr assumeExpr)
        {
            var cond = MathematicaCondition.CreateIsDomainMemberTest(sc, DomainSymbols.Reals, assumeExpr);

            return cond.IsConstantTrue();
        }

        public static bool IsComplexScalar(this MathematicaScalar sc, Expr assumeExpr)
        {
            var cond = MathematicaCondition.CreateIsDomainMemberTest(sc, DomainSymbols.Complexes, assumeExpr);

            return cond.IsConstantTrue();
        }

        public static bool IsRationalScalar(this MathematicaScalar sc, Expr assumeExpr)
        {
            var cond = MathematicaCondition.CreateIsDomainMemberTest(sc, DomainSymbols.Rationals, assumeExpr);

            return cond.IsConstantTrue();
        }
    }
}
