using System.Text;
using CodeComposerLib.Irony.Semantic.Expression.Value;
using CodeComposerLib.SyntaxTree.Expressions;
using DataStructuresLib.SimpleTree;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.NETLink;
using GMac.GMacCompiler.Semantic.AST;

namespace GMac.GMacScripting
{
    public static class ScriptUtils
    {
        internal static SimpleTreeLeaf<Expr> ToSimpleExprTree(this ValuePrimitive<MathematicaScalar> value)
        {
            return new SimpleTreeLeaf<Expr>(value.Value.Expression);
        }

        internal static SimpleTreeBranchDictionaryByIndex<Expr> ToSimpleExprTree(this GMacValueMultivector value)
        {
            var scalarTypeName = value.CoefficientType.SymbolAccessName;
            var tree = new SimpleTreeBranchDictionaryByIndex<Expr>();

            foreach (var pair in value.SymbolicMultivector.NonZeroExprTerms)
                tree.Add((int)pair.Key, "#E" + pair.Key + "#", scalarTypeName, pair.Value);

            return tree;
        }

        internal static SimpleTreeBranchDictionaryByName<Expr> ToSimpleExprTree(this ValueStructureSparse value)
        {
            var structure = value.ValueStructureType;
            var tree = new SimpleTreeBranchDictionaryByName<Expr>();

            foreach (var pair in value)
            {
                var dataMember = structure.GetDataMember(pair.Key);
                var node = ToSimpleExprTree(pair.Value);

                tree.Add(dataMember.DefinitionIndex, dataMember.ObjectName, dataMember.SymbolTypeSignature, node);
            }

            return tree;
        }

        internal static SimpleTreeNode<Expr> ToSimpleExprTree(this ILanguageValue value)
        {
            var scalarValue = value as ValuePrimitive<MathematicaScalar>;

            if (ReferenceEquals(scalarValue, null) == false)
                return ToSimpleExprTree(scalarValue);

            var mvValue = value as GMacValueMultivector;

            if (ReferenceEquals(mvValue, null) == false)
                return ToSimpleExprTree(mvValue);

            var structValue = value as ValueStructureSparse;

            if (ReferenceEquals(structValue, null) == false)
                return ToSimpleExprTree(structValue);

            return null;
        }


        internal static SimpleTreeLeaf<string> ToSimpleStringTree(this ValuePrimitive<MathematicaScalar> value)
        {
            return new SimpleTreeLeaf<string>(value.Value.Expression.ToString());
        }

        internal static SimpleTreeBranchDictionaryByIndex<string> ToSimpleStringTree(this GMacValueMultivector value)
        {
            var scalarTypeName = value.CoefficientType.SymbolAccessName;
            var tree = new SimpleTreeBranchDictionaryByIndex<string>();

            foreach (var pair in value.SymbolicMultivector.NonZeroExprTerms)
                tree.Add((int)pair.Key, "#E" + pair.Key + "#", scalarTypeName, pair.Value.ToString());

            return tree;
        }

        internal static SimpleTreeBranchDictionaryByName<string> ToSimpleStringTree(this ValueStructureSparse value)
        {
            var structure = value.ValueStructureType;
            var tree = new SimpleTreeBranchDictionaryByName<string>();

            foreach (var pair in value)
            {
                var dataMember = structure.GetDataMember(pair.Key);
                var node = ToSimpleStringTree(pair.Value);

                tree.Add(dataMember.DefinitionIndex, dataMember.ObjectName, dataMember.SymbolTypeSignature, node);
            }

            return tree;
        }

        internal static SimpleTreeNode<string> ToSimpleStringTree(this ILanguageValue value)
        {
            var scalarValue = value as ValuePrimitive<MathematicaScalar>;

            if (ReferenceEquals(scalarValue, null) == false)
                return ToSimpleStringTree(scalarValue);

            var mvValue = value as GMacValueMultivector;

            if (ReferenceEquals(mvValue, null) == false)
                return ToSimpleStringTree(mvValue);

            var structValue = value as ValueStructureSparse;

            if (ReferenceEquals(structValue, null) == false)
                return ToSimpleStringTree(structValue);

            return null;
        }


        /// <summary>
        /// Converts a symbolic text expression tree into a method call in C# syntax
        /// </summary>
        /// <param name="textExpr"></param>
        /// <returns></returns>
        public static string ToCSharpCodeMethodCall(this SteExpression textExpr)
        {
            var s = new StringBuilder();

            s.Append(textExpr.HeadText).Append('(');

            if (textExpr.ArgumentsCount > 0)
            {
                foreach (var argExpr in textExpr.Arguments)
                    s.Append(argExpr.ToCSharpCodeMethodCall()).Append(", ");

                s.Length -= 2;
            }

            s.Append(')');

            return s.ToString();
        }

        /// <summary>
        /// onverts a symbolic text expression tree into a method call in GMac Script syntax
        /// </summary>
        /// <param name="textExpr"></param>
        /// <returns></returns>
        public static string ToGMacScriptMethodCall(this SteExpression textExpr)
        {
            var s = new StringBuilder();

            s.Append(textExpr.HeadText).Append('(');

            if (textExpr.ArgumentsCount > 0)
            {
                foreach (var argExpr in textExpr.Arguments)
                    s.Append(argExpr.ToGMacScriptMethodCall()).Append(", ");

                s.Length -= 2;
            }

            s.Append(") |> ");

            s.Append(textExpr.HeadText);

            return s.ToString();
        }

    }
}
