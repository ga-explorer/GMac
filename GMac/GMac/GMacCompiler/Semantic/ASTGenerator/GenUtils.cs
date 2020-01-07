using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using DataStructuresLib;
using GMac.GMacCompiler.Syntax;
using Irony.Parsing;
using TextComposerLib;

namespace GMac.GMacCompiler.Semantic.ASTGenerator
{
    /// <summary>
    /// GMac AST symbol translation tools
    /// </summary>
    internal static class GenUtils
    {
        /// <summary>
        /// This regex finds all substrings delimited by $$ signs inside a given text for example:
        /// 'Sin[$p1.x$] + Cos[$p1.x + p2.y$]' will get '$p1.x$' and '$p1.x + p2.y$'
        /// </summary>
        private static readonly Regex CasInternalExpressionRegex = new Regex(@"\$.*?\$", RegexOptions.Singleline);

        /// <summary>
        /// This regex finds all symbolic variables from the given text on the form AUG000000, CAS000000, or S000CAS000000 
        /// where the 0's can be replaced by any digit form 0 to 9
        /// </summary>
        private static readonly Regex CasVarRegex = new Regex(@"\b(AUG[0-9][0-9][0-9][0-9][0-9][0-9])|(CAS[0-9][0-9][0-9][0-9][0-9][0-9])|(S[0-9][0-9][0-9]CAS[0-9][0-9][0-9][0-9][0-9][0-9])\b");


        /// <summary>
        /// Assert that the given parse tree node has the given term name
        /// </summary>
        /// <param name="node"></param>
        /// <param name="termName"></param>
        public static void Assert(this ParseTreeNode node, string termName)
        {
            if (node.Term.ToString() != termName)
                throw new InvalidOperationException("Expecting a node of type " + termName);
        }

        /// <summary>
        /// Find all substrings inside the given string delimited by $$. For example:
        /// 'Sin[$p1.x$] + Cos[$p1.x + p2.y$]' will get '$p1.x$' and '$p1.x + p2.y$'
        /// </summary>
        /// <param name="expressionText"></param>
        /// <returns></returns>
        private static IEnumerable<Match> ExtractAllInternalExpressions(string expressionText)
        {
            var allMatches = CasInternalExpressionRegex.Matches(expressionText);

            for (var i = 0; i < allMatches.Count; i++)
                yield return allMatches[i];
        }

        /// <summary>
        /// Find all distinct substrings inside the given string delimited by $$. For example:
        /// 'Sin[$p1.x$] + Cos[$p1.x + p2.y$]' will get '$p1.x$' and '$p1.x + p2.y$'
        /// </summary>
        /// <param name="expressionText"></param>
        /// <returns></returns>
        public static IEnumerable<Match> ExtractDistinctInternalExpressions(string expressionText)
        {
            return ExtractAllInternalExpressions(expressionText).Distinct();
        }

        /// <summary>
        /// Extract all symbolic variables from the given text on the form AUG000000, CAS000000, or S000CAS000000 
        /// where the 0's can be replaced by any digit form 0 to 9
        /// </summary>
        /// <param name="expressionText"></param>
        /// <returns></returns>
        private static IEnumerable<string> ExtractAllCasVariables(string expressionText)
        {
            var matches = CasVarRegex.Matches(expressionText);

            for (var i = 0; i < matches.Count; i++)
                yield return matches[i].Value;
        }

        /// <summary>
        /// Extract all distinct symbolic variables from the given text on the form AUG000000, CAS000000, or S000CAS000000 
        /// where the 0's can be replaced by any digit form 0 to 9
        /// </summary>
        /// <param name="expressionText"></param>
        /// <returns></returns>
        public static IEnumerable<string> ExtractDistinctCasVariables(string expressionText)
        {
            return ExtractAllCasVariables(expressionText).Distinct();
        }

        /// <summary>
        /// True if the expression text is a single CAS variable (e.g. 'CAS000111' or 'AUG000123')
        /// </summary>
        /// <param name="exprText"></param>
        /// <returns></returns>
        public static bool IsSingleCasVariable(string exprText)
        {
            return CasVarRegex.Match(exprText).Value == exprText;
        }

        /// <summary>
        /// True if the expression depends on a single CAS variable 
        /// (e.g. 'Power[Sin[CAS000111], 2] + Power[Cos[CAS000111], 2]')
        /// </summary>
        /// <param name="expressionText"></param>
        /// <returns></returns>
        public static bool IsSingleCasVariableDependent(string expressionText)
        {
            return ExtractDistinctCasVariables(expressionText).Count() == 1;
        }

        /// <summary>
        /// Read an identifier from the given parse tree node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static string Translate_Identifier(ParseTreeNode node)
        {
            node.Assert(GMacParseNodeNames.Identifier);

            return node.FindTokenAndGetText();
        }

        /// <summary>
        /// Read a list of identifiers from the given parse tree node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static List<string> Translate_Identifier_List(ParseTreeNode node)
        {
            node.Assert(GMacParseNodeNames.IdentifierList);

            return node.ChildNodes.Select(Translate_Identifier).ToList();
        }

        /// <summary>
        /// Read an identifier qualified list from the given parse tree node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static TrimmedList<string> Translate_Qualified_Identifier(ParseTreeNode node)
        {
            node.Assert(GMacParseNodeNames.QualifiedIdentifier);

            var identList = new TrimmedList<string>(
                node.ChildNodes.Select(Translate_Identifier)
                );

            identList.ResetActiveRange();

            return identList;
        }

        /// <summary>
        /// Read a list of identifier qualified lists from the given parse tree node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static List<TrimmedList<string>> Translate_Qualified_Identifier_List(ParseTreeNode node)
        {
            node.Assert(GMacParseNodeNames.QualifiedIdentifierList);

            return node.ChildNodes.Select(Translate_Qualified_Identifier).ToList();
        }

        /// <summary>
        /// Read an mathematica expression text from the given parse tree node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static string Translate_StringLiteral(ParseTreeNode node)
        {
            node.Assert(GMacParseNodeNames.StringLiteral);

            var expression = node.ChildNodes[0].FindTokenAndGetText();

            return expression.QuoteLiteralToValue();

            //TODO: Use this instead?
            //return
            //    expression[0] == '@'
            //    ? expression.Substring(2, expression.Length - 3)
            //    : expression.Substring(1, expression.Length - 2);
        }
    }
}
