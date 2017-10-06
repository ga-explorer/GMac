using System;
using GMac.GMacCompiler.Semantic.AST;
using IronyGrammars.Semantic.Expression;
using IronyGrammars.Semantic.Expression.Basic;
using IronyGrammars.Semantic.Expression.Value;
using IronyGrammars.Semantic.Expression.ValueAccess;
using IronyGrammars.Semantic.Operator;
using IronyGrammars.Semantic.Type;
using SymbolicInterface.Mathematica.Expression;

namespace GMac.GMacAST.Expressions
{
    public static class ExpressionsUtils
    {
        internal static AstDatastoreValueAccess ToAstDatastoreValueAccess(this LanguageValueAccess expr)
        {
            return new AstDatastoreValueAccess(expr);
        }

        internal static AstValueScalar ToAstValueScalar(this ValuePrimitive<MathematicaScalar> expr)
        {
            return new AstValueScalar(expr);
        }

        internal static AstValueMultivector ToAstValueMultivector(this GMacValueMultivector expr)
        {
            return new AstValueMultivector(expr);
        }

        internal static AstValueStructure ToAstValueStructure(this ValueStructureSparse expr)
        {
            return new AstValueStructure(expr);
        }

        internal static AstValue ToAstValue(this ILanguageValue expr)
        {
            var s2 = expr as ValuePrimitive<MathematicaScalar>;
            if (ReferenceEquals(s2, null) == false) return new AstValueScalar(s2);

            var s3 = expr as GMacValueMultivector;
            if (ReferenceEquals(s3, null) == false) return new AstValueMultivector(s3);

            var s4 = expr as ValueStructureSparse;
            if (ReferenceEquals(s4, null) == false) return new AstValueStructure(s4);

            return null;
        }

        internal static AstUnaryExpression ToAstUnaryExpression(this BasicUnary expr)
        {
            if (expr.Operator is OperatorPrimitive)
                return new AstUnaryExpression(expr);

            throw new InvalidCastException();
        }

        internal static AstTransformCall ToAstTransformCall(this BasicUnary expr)
        {
            if (expr.Operator is GMacMultivectorTransform)
                return new AstTransformCall(expr);

            throw new InvalidCastException();
        }

        internal static AstTypeCast ToAstTypeCast(this BasicUnary expr)
        {
            if (expr.Operator is GMacFrameMultivector)
                return new AstTypeCast(expr);

            if (expr.Operator is TypePrimitive)
                return new AstTypeCast(expr);

            throw new InvalidCastException();
        }

        internal static AstBinaryExpression ToAstBinaryExpression(this BasicBinary expr)
        {
            return new AstBinaryExpression(expr);
        }

        internal static AstCompositeExpression ToAstCompositeExpression(this CompositeExpression expr)
        {
            return new AstCompositeExpression(expr);
        }

        internal static AstExpression ToAstExpression(this BasicUnary expr)
        {
            var op1 = expr.Operator as OperatorPrimitive;
            if (ReferenceEquals(op1, null) == false) return new AstUnaryExpression(expr);

            var op2 = expr.Operator as GMacMultivectorTransform;
            if (ReferenceEquals(op2, null) == false) return new AstTransformCall(expr);

            var op3 = expr.Operator as TypePrimitive;
            if (ReferenceEquals(op3, null) == false) return new AstTypeCast(expr);

            var op4 = expr.Operator as GMacFrameMultivector;
            if (ReferenceEquals(op4, null) == false) return new AstTypeCast(expr);

            return null;
        }

        internal static AstExpression ToAstExpression(this BasicPolyadic expr)
        {
            var op1 = expr.Operator as GMacMacro;
            if (ReferenceEquals(op1, null) == false) return new AstMacroCall(expr);

            var op2 = expr.Operator as GMacFrameMultivectorConstructor;
            if (ReferenceEquals(op2, null) == false) return new AstMultivectorConstructor(expr);

            var op3 = expr.Operator as GMacStructureConstructor;
            if (ReferenceEquals(op3, null) == false) return new AstStructureConstructor(expr);

            var op4 = expr.Operator as GMacParametricSymbolicExpression;
            if (ReferenceEquals(op4, null) == false) return new AstParametricSymbolicExpression(expr);

            return null;
        }

        internal static AstExpression ToAstExpression(this ILanguageExpression expr)
        {
            var s1 = expr as LanguageValueAccess;
            if (ReferenceEquals(s1, null) == false) return new AstDatastoreValueAccess(s1);

            var s2 = expr as ValuePrimitive<MathematicaScalar>;
            if (ReferenceEquals(s2, null) == false) return new AstValueScalar(s2);

            var s3 = expr as GMacValueMultivector;
            if (ReferenceEquals(s3, null) == false) return new AstValueMultivector(s3);

            var s4 = expr as ValueStructureSparse;
            if (ReferenceEquals(s4, null) == false) return new AstValueStructure(s4);

            var s5 = expr as BasicUnary;
            if (ReferenceEquals(s5, null) == false) return s5.ToAstExpression();

            var s6 = expr as BasicBinary;
            if (ReferenceEquals(s6, null) == false) return new AstBinaryExpression(s6);

            var s7 = expr as BasicPolyadic;
            if (ReferenceEquals(s7, null) == false) return s7.ToAstExpression();

            var s8 = expr as CompositeExpression;
            if (ReferenceEquals(s8, null) == false) return new AstCompositeExpression(s8);

            return null;
        }
    }
}
