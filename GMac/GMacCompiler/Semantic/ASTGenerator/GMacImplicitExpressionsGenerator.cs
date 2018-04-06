using System;
using GMac.GMacCompiler.Semantic.AST;
using GMac.GMacCompiler.Semantic.AST.Extensions;
using GMac.GMacMath.Symbolic;
using IronyGrammars.Semantic.Command;
using IronyGrammars.Semantic.Expression;
using IronyGrammars.Semantic.Expression.Basic;
using IronyGrammars.Semantic.Expression.Value;
using IronyGrammars.Semantic.Expression.ValueAccess;
using IronyGrammars.Semantic.Symbol;
using SymbolicInterface.Mathematica.Expression;

namespace GMac.GMacCompiler.Semantic.ASTGenerator
{
    internal static class GMacImplicitExpressionsGenerator
    {
        private static LanguageValueAccess NonAtomicExpressionToValueAccess(this CommandBlock commandBlock, ILanguageExpression oldExpr)
        {
            if (oldExpr is ILanguageExpressionAtomic)
                throw new InvalidOperationException("This should never happen!");

            var localVar = commandBlock.DefineLocalVariable(oldExpr.ExpressionType).DataStore;

            //Create a local variable to hold value and return the local variable as a direct value access object
            var lhsValueAccess = LanguageValueAccess.Create(localVar);

            commandBlock.AddCommand_Assign(lhsValueAccess, oldExpr);

            return lhsValueAccess;
        }

        private static ILanguageExpressionAtomic AtomicExpressionToScalarAtomicExpression(this CommandBlock commandBlock, ILanguageExpressionAtomic oldExpr)
        {
            var scalarType = ((GMacAst)commandBlock.RootAst).ScalarType;

            if (oldExpr.ExpressionType.IsSameType(scalarType))
                return oldExpr;

            if (!oldExpr.ExpressionType.IsInteger())
                throw new InvalidCastException("Cannot convert atomic expression of type " + oldExpr.ExpressionType.TypeSignature + " to a scalar atomic expression");

            var valuePrimitive = oldExpr as ValuePrimitive<int>;

            if (valuePrimitive != null)
                return ValuePrimitive<MathematicaScalar>.Create(
                    scalarType,
                    MathematicaScalar.Create(SymbolicUtils.Cas, valuePrimitive.Value)
                    );

            //This should be a value access of type integer
            if (!(oldExpr is LanguageValueAccess))
                throw new InvalidCastException("Cannot convert atomic expression " + oldExpr + " of type " +
                                               oldExpr.ExpressionType.TypeSignature + " to a scalar atomic expression");

            //Create a cast operation
            var newRhsExpr = BasicUnary.Create(scalarType, scalarType, oldExpr);

            //The new expresssion is not atomic. Create a local variable to hold value and return the local variable 
            //as a direct value access object
            return NonAtomicExpressionToValueAccess(commandBlock, newRhsExpr);
        }

        private static ILanguageExpressionAtomic AtomicExpressionToMultivectorAtomicExpression(this CommandBlock commandBlock, GMacFrameMultivector mvType, ILanguageExpressionAtomic oldExpr)
        {
            if (oldExpr.ExpressionType.IsSameType(mvType))
                return oldExpr;

            if (!oldExpr.ExpressionType.IsNumber())
                throw new InvalidCastException("Cannot convert atomic expression of type " + oldExpr.ExpressionType.TypeSignature + " to a expression of type " + mvType.TypeSignature);

            var valuePrimitive = oldExpr as ValuePrimitive<int>;

            if (valuePrimitive != null)
                return GMacValueMultivector.CreateScalar(
                    mvType,
                    MathematicaScalar.Create(SymbolicUtils.Cas, valuePrimitive.Value)
                    );

            var primitive = oldExpr as ValuePrimitive<MathematicaScalar>;

            if (primitive != null)
                return GMacValueMultivector.CreateScalar(
                    mvType,
                    primitive.Value
                    );

            //This should be a value access of type integer or scalar
            if (!(oldExpr is LanguageValueAccess))
                throw new InvalidCastException(
                    "Cannot convert atomic expression " + oldExpr + " of type " +
                    oldExpr.ExpressionType.TypeSignature + " to a atomic expression of type" +
                    mvType.TypeSignature
                    );

            //Create a cast operation
            var newRhsExpr = BasicUnary.Create(mvType, mvType, oldExpr);

            //The new expresssion is not atomic. Create a local variable to hold value and return the local variable 
            //as a direct value access object
            return NonAtomicExpressionToValueAccess(commandBlock, newRhsExpr);
        }

        /// <summary>
        /// Convert the given expression into a local variable by adding an assignment command if necessary 
        /// and returning the lhs local variable of the assignment as the new atomic expression. If the given expression 
        /// is a local variable of this command block it is returned directly.
        /// </summary>
        /// <param name="commandBlock"></param>
        /// <param name="expr"></param>
        /// <returns></returns>
        public static SymbolLocalVariable ExpressionToLocalVariable(this CompositeExpression commandBlock, ILanguageExpression expr)
        {
            SymbolLocalVariable localVariable;

            //If expr is a local variable of the command block's child scope return the local variable
            var valueAccess = expr as LanguageValueAccess;

            if (valueAccess != null && valueAccess.IsFullAccessLocalVariable)
            {
                localVariable = valueAccess.RootSymbolAsLocalVariable;

                if (localVariable.ParentScope.ObjectId == commandBlock.ChildScope.ObjectId)
                    return localVariable;
            }

            //Else create a loical variable to hold value and return the local variable as a direct value access object
            localVariable = commandBlock.DefineLocalVariable(expr.ExpressionType).LocalVariable;

            var lhs = LanguageValueAccess.Create(localVariable);

            commandBlock.AddCommand_Assign(lhs, expr);

            return localVariable;
        }

        public static ILanguageExpressionAtomic ExpressionToAtomicExpression(this CommandBlock commandBlock, ILanguageExpression oldExpr)
        {
            //If the given expression is atomic cast it into a scalar atomic expression
            var atomicExpression = oldExpr as ILanguageExpressionAtomic;

            return 
                atomicExpression ?? NonAtomicExpressionToValueAccess(commandBlock, oldExpr);
        }

        public static ILanguageExpressionAtomic ExpressionToScalarAtomicExpression(this CommandBlock commandBlock, ILanguageExpression oldExpr)
        {
            //If the given expression is atomic cast it into a scalar atomic expression
            var expressionAtomic = oldExpr as ILanguageExpressionAtomic;

            if (expressionAtomic != null)
                return 
                    AtomicExpressionToScalarAtomicExpression(commandBlock, expressionAtomic);

            //The expresssion is not atomic. Create a local variable to hold value and return the local variable 
            //as a direct value access object
            var newValueAccess = 
                NonAtomicExpressionToValueAccess(commandBlock, oldExpr);

            //Cast the value access object into a scalar atomic expression
            return 
                AtomicExpressionToScalarAtomicExpression(commandBlock, newValueAccess);
        }

        public static ILanguageExpressionAtomic ExpressionToMultivectorAtomicExpression(this CommandBlock commandBlock, GMacFrameMultivector mvType, ILanguageExpression oldExpr)
        {
            //If the given expression is atomic cast it into a multivector atomic expression
            var expressionAtomic = oldExpr as ILanguageExpressionAtomic;

            if (expressionAtomic != null)
                return 
                    AtomicExpressionToMultivectorAtomicExpression(
                        commandBlock, 
                        mvType, 
                        expressionAtomic
                        );

            //The expresssion is not atomic. Create a local variable to hold value and return the local variable 
            //as a direct value access object
            var newValueAccess = 
                NonAtomicExpressionToValueAccess(commandBlock, oldExpr);

            //Cast the value access object into a multivector atomic expression
            return 
                AtomicExpressionToMultivectorAtomicExpression(commandBlock, mvType, newValueAccess);
        }

        public static LanguageValueAccess NonOrthogonalToOrthogonalMultivector(this CommandBlock commandBlock, GMacFrame derivedFrame, ILanguageExpressionAtomic expr)
        {
            //var derived_frame = ((GMacFrameMultivector)expr.ExpressionType).ParentFrame;

            var newRhsExpr = derivedFrame.DerivedToBaseTransform.CreateTransformExpression(expr);

            return NonAtomicExpressionToValueAccess(commandBlock, newRhsExpr);
        }

        public static BasicUnary OrthogonalToNonOrthogonalMultivectorTransform(this CommandBlock commandBlock, GMacFrame derivedFrame, ILanguageExpressionAtomic expr)
        {
            //var derived_frame = ((GMacFrameMultivector)expr.ExpressionType).ParentFrame;

            var newRhsExpr = derivedFrame.BaseToDerivedTransform.CreateTransformExpression(expr);

            return newRhsExpr;
            //return NonAtomicExpressionToValueAccess(command_block, new_rhs_expr);
        }


    }
}
