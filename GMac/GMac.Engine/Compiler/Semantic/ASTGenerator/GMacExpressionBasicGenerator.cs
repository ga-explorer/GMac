using System.Collections.Generic;
using System.Text;
using CodeComposerLib.Irony.Semantic.Expression;
using CodeComposerLib.Irony.Semantic.Expression.Basic;
using CodeComposerLib.Irony.Semantic.Expression.Value;
using CodeComposerLib.Irony.Semantic.Operator;
using CodeComposerLib.Irony.Semantic.Translator;
using CodeComposerLib.Irony.Semantic.Type;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;
using GMac.Engine.Compiler.Semantic.AST;
using GMac.Engine.Compiler.Semantic.AST.Extensions;
using GMac.Engine.Compiler.Semantic.ASTConstants;

namespace GMac.Engine.Compiler.Semantic.ASTGenerator
{
    internal sealed class GMacExpressionBasicGenerator : GMacAstSymbolGenerator
    {
        private ILanguageExpression _operand1;

        private ILanguageExpression _operand2;

        private ILanguageExpressionAtomic _atomicOperand1;

        private ILanguageExpressionAtomic _atomicOperand2;

        private ILanguageType _resultType;

        private GMacFrameMultivector ResultMultivectorType => _resultType as GMacFrameMultivector;

        private ILanguageType Operand1Type => _operand1.ExpressionType;

        private ILanguageType Operand2Type => _operand2.ExpressionType;

        private string Operand1TypeSignature => _operand1.ExpressionType.TypeSignature;

        private string Operand2TypeSignature => _operand2.ExpressionType.TypeSignature;

        private GMacFrame OperandsFrame 
        { 
            get 
            {
                if (_operand1.ExpressionType.IsFrameMultivector())
                    return ((GMacFrameMultivector)_operand1.ExpressionType).ParentFrame;

                return 
                    _operand2.ExpressionType.IsFrameMultivector() 
                    ? ((GMacFrameMultivector)_operand2.ExpressionType).ParentFrame 
                    : null;
            } 
        }

        private GMacFrameMultivector OperandsMultivectorType
        {
            get
            {
                if (_operand1.ExpressionType.IsFrameMultivector())
                    return ((GMacFrameMultivector)_operand1.ExpressionType);

                if (_operand2.ExpressionType.IsFrameMultivector())
                    return ((GMacFrameMultivector)_operand2.ExpressionType);

                return null;
            }
        }

        private TypePrimitive IntegerType => GMacRootAst.ScalarType;

        private TypePrimitive ScalarType => GMacRootAst.ScalarType;


        //public override void ResetOnAcquire()
        //{
        //    base.ResetOnAcquire();

        //    _operand1 = null;
        //    _operand2 = null;
        //    _atomicOperand1 = null;
        //    _atomicOperand2 = null;
        //    _resultType = null;
        //}


        public void SetContext(GMacSymbolTranslatorContext context)
        {
            SetContext((SymbolTranslatorContext)context);
        }


        private void SetOperands(ILanguageExpression expr)
        {
            _operand1 = expr;

            _operand2 = null;
        }

        private void SetOperands(ILanguageExpression expr1, ILanguageExpression expr2)
        {
            _operand1 = expr1;

            _operand2 = expr2;
        }

        private void ForceAtomicOperands(ILanguageType resultType)
        {
            _resultType = resultType;

            _atomicOperand1 = Context.ActiveParentCommandBlock.ExpressionToAtomicExpression(_operand1);

            _atomicOperand2 = ReferenceEquals(_operand2, null) ? null : Context.ActiveParentCommandBlock.ExpressionToAtomicExpression(_operand2);
        }

        private void ForceAtomicScalarOperands(ILanguageType resultType)
        {
            _resultType = resultType;

            _atomicOperand1 = Context.ActiveParentCommandBlock.ExpressionToScalarAtomicExpression(_operand1);

            _atomicOperand2 = ReferenceEquals(_operand2, null) ? null : Context.ActiveParentCommandBlock.ExpressionToScalarAtomicExpression(_operand2);
        }

        private void ForceAtomicMultivectorOperands(ILanguageType resultType)
        {
            _resultType = resultType;

            _atomicOperand1 = Context.ActiveParentCommandBlock.ExpressionToMultivectorAtomicExpression(OperandsMultivectorType, _operand1);

            _atomicOperand2 = ReferenceEquals(_operand2, null) ? null : Context.ActiveParentCommandBlock.ExpressionToMultivectorAtomicExpression(OperandsMultivectorType, _operand2);
        }

        
        public void VerifyOperation_TypeCast(ILanguageType targetType)
        {
            //Cast an integer to a scalar
            if (targetType.IsScalar() && Operand1Type.IsInteger())
                ForceAtomicOperands(targetType);

            //Cast a number to a multivector
            else if (targetType.IsFrameMultivector() && Operand1Type.IsNumber())
                ForceAtomicOperands(targetType);

            //Cast multivector to a multivector with the same frame dimension (by diectly copying coeffecients)
            //useful for example when transforming a computation to Euclidean space with the same dimensions
            else if (targetType.IsFrameMultivector() && Operand1Type.IsFrameMultivector() && targetType.GetFrame().VSpaceDimension == Operand1Type.GetFrame().VSpaceDimension)
                ForceAtomicOperands(targetType);

            else
                Context.CreateTypeMismatch("Cannot apply type cast " + targetType.TypeSignature + " to expression of type " + Operand1TypeSignature);
        }

        private void VerifyOperation_PolyadicOperandAssignment(ILanguageType lhsType)
        {
            if (lhsType.IsSameType(Operand1Type))
                ForceAtomicOperands(null);

            else if (lhsType.IsScalar() && Operand1Type.IsNumber())
                ForceAtomicScalarOperands(null);

            else if (lhsType.IsFrameMultivector() && Operand1Type.IsNumber())
                ForceAtomicMultivectorOperands(null);

            else
                Context.CreateTypeMismatch("Cannot assign expression of type " + Operand1TypeSignature + " to parameter of type " + lhsType);
        }

        private void VerifyOperation_TransformApplication(GMacMultivectorTransform transform)
        {
            if (Operand1Type.IsNumber())
                ForceAtomicScalarOperands(transform.TargetFrame.MultivectorType);

            else if (Operand1Type.IsFrameMultivector() && ((GMacFrameMultivector)Operand1Type).ParentFrame.ObjectId == transform.SourceFrame.ObjectId)
                ForceAtomicMultivectorOperands(transform.TargetFrame.MultivectorType);

            else 
                Context.CreateTypeMismatch("Cannot apply transform " + transform.SymbolQualifiedName + " to expression of type " + Operand1TypeSignature);
        }


        private void VerifyOperation_UnaryOperation()
        {
            if (Operand1Type.IsNumber() || Operand1Type.IsFrameMultivector())
                ForceAtomicOperands(Operand1Type);

            else 
                Context.CreateTypeMismatch(@"Cannot apply unary multivector operation to expression of type " + Operand1TypeSignature);
        }

        private void VerifyOperation_UnaryNorm()
        {
            if (Operand1Type.IsNumber() || Operand1Type.IsFrameMultivector())
                ForceAtomicOperands(ScalarType);

            else 
                Context.CreateTypeMismatch(@"Cannot apply multivector norm to expression of type " + Operand1TypeSignature);
        }


        private void VerifyOperation_BinaryPlusMinus()
        {
            if (Operand1Type.IsInteger() && Operand2Type.IsInteger())
                ForceAtomicOperands(IntegerType);

            else if (Operand1Type.IsNumber() && Operand2Type.IsNumber())
                ForceAtomicScalarOperands(ScalarType);

            else if (Operand1Type.IsFrameMultivector() && Operand2Type.IsNumber())
                ForceAtomicMultivectorOperands(Operand1Type);

            else if (Operand2Type.IsFrameMultivector() && Operand1Type.IsNumber())
                ForceAtomicMultivectorOperands(Operand2Type);

            else if (Operand1Type.HasSameFrame(Operand2Type))
                ForceAtomicMultivectorOperands(Operand1Type);

            else 
                Context.CreateTypeMismatch(@"Cannot apply +\- to expressions of type " + Operand1TypeSignature + " and " + Operand2TypeSignature);
        }

        private void VerifyOperation_Times()
        {
            if (Operand1Type.IsNumber() && Operand2Type.IsNumber())
                ForceAtomicScalarOperands(ScalarType);

            else if (Operand1Type.IsFrameMultivector() && Operand2Type.IsNumber())
            {
                _resultType = Operand1Type;

                _atomicOperand1 = Context.ActiveParentCommandBlock.ExpressionToMultivectorAtomicExpression(OperandsMultivectorType, _operand1);

                _atomicOperand2 = Context.ActiveParentCommandBlock.ExpressionToScalarAtomicExpression(_operand2);
            }

            else if (Operand1Type.IsNumber() && Operand2Type.IsFrameMultivector())
            {
                _resultType = Operand2Type;

                _atomicOperand1 = Context.ActiveParentCommandBlock.ExpressionToScalarAtomicExpression(_operand1);

                _atomicOperand2 = Context.ActiveParentCommandBlock.ExpressionToMultivectorAtomicExpression(OperandsMultivectorType, _operand2);
            }

            else
                Context.CreateTypeMismatch("cannot apply Times to expressions of type " + Operand1TypeSignature + " and " + Operand2TypeSignature);
        }

        private void VerifyOperation_Divide()
        {
            if (Operand1Type.IsNumber() && Operand2Type.IsNumber())
                ForceAtomicScalarOperands(ScalarType);

            else if (Operand1Type.IsFrameMultivector() && Operand2Type.IsNumber())
            {
                _resultType = Operand1Type;

                _atomicOperand1 = Context.ActiveParentCommandBlock.ExpressionToMultivectorAtomicExpression(OperandsMultivectorType, _operand1);

                _atomicOperand2 = Context.ActiveParentCommandBlock.ExpressionToScalarAtomicExpression(_operand2);
            }

            else 
                Context.CreateTypeMismatch("cannot apply Divide to expressions of type " + Operand1TypeSignature + " and " + Operand2TypeSignature);
        }

        private void VerifyOperation_ScalarProduct()
        {
            if (Operand1Type.IsInteger() && Operand2Type.IsInteger())
                ForceAtomicOperands(IntegerType);

            else if (Operand1Type.IsNumber() && Operand2Type.IsNumber())
                ForceAtomicScalarOperands(ScalarType);

            else if (Operand1Type.IsFrameMultivector() && Operand2Type.IsNumber())
                ForceAtomicMultivectorOperands(ScalarType);

            else if (Operand2Type.IsFrameMultivector() && Operand1Type.IsNumber())
                ForceAtomicMultivectorOperands(ScalarType);

            else if (Operand1Type.HasSameFrame(Operand2Type))
                ForceAtomicMultivectorOperands(ScalarType);

            else 
                Context.CreateTypeMismatch("cannot apply Times to expressions of type " + Operand1TypeSignature + " and " + Operand2TypeSignature);
        }

        private void VerifyOperation_MultivectorProduct()
        {
            if (Operand1Type.IsNumber() && Operand2Type.IsNumber())
                ForceAtomicScalarOperands(ScalarType);

            else if (Operand1Type.IsFrameMultivector() && Operand2Type.IsNumber())
                ForceAtomicMultivectorOperands(Operand1Type);

            else if (Operand2Type.IsFrameMultivector() && Operand1Type.IsNumber())
                ForceAtomicMultivectorOperands(Operand2Type);

            else if (Operand1Type.HasSameFrame(Operand2Type))
                ForceAtomicMultivectorOperands(Operand1Type);

            else 
                Context.CreateTypeMismatch("cannot apply a multivector product to expressions of type " + Operand1TypeSignature + " and " + Operand2TypeSignature);
        }


        //TODO: Review the types in this function
        public void VerifyOperation_Diff()
        {
            if (Operand1Type.IsScalar() && Operand2Type.IsScalar())
                ForceAtomicOperands(ScalarType);

            else if (Operand1Type.IsFrameMultivector() && Operand2Type.IsScalar())
                ForceAtomicOperands(Operand1Type);

            else if (Operand2Type.IsFrameMultivector() && Operand1Type.IsScalar())
                ForceAtomicOperands(Operand2Type);

            else if (Operand1Type.HasSameFrame(Operand2Type))
                ForceAtomicOperands(Operand1Type);

            else 
                Context.CreateTypeMismatch("Cannot apply Diff to expressions of type " + Operand1TypeSignature + " and " + Operand2TypeSignature);
        }

        public ILanguageType VerifyType_SymbolicExpression(MathematicaScalar sc, OperandsByName operands)
        {
            var s = new StringBuilder();

            var varTypes = new Dictionary<string, MathematicaAtomicType>();

            foreach (var pair in operands.OperandsDictionary)
            {
                //var lhs_type = this.GMacSymbolTable.Scalar
                var rhsType = pair.Value.ExpressionType;

                if (rhsType.IsBoolean())
                    varTypes.Add(pair.Key, MathematicaAtomicType.Boolean);

                if (rhsType.IsInteger())
                    varTypes.Add(pair.Key, MathematicaAtomicType.Integer);

                if (rhsType.IsScalar())
                    varTypes.Add(pair.Key, MathematicaAtomicType.Real);

                else
                {
                    s.Append("cannot assign RHS expression of type ");
                    s.Append(rhsType.TypeSignature);
                    s.Append(" to symbolic expression parameter ");
                    s.Append(pair.Key);
                }
            }

            if (s.Length > 0)
                return Context.CreateTypeMismatch(s.ToString());

            var assumeExpr = Cas.CreateAssumeExpr(varTypes);

            if (sc.IsBooleanScalar(assumeExpr))
                return GMacRootAst.BooleanType;

            //if (sc.IsIntegerScalar(assumeExpr))
            //    return GMacRootAst.IntegerType;

            if (sc.IsRealScalar(assumeExpr))
                return GMacRootAst.ScalarType;

            return 
                sc.IsComplexScalar(assumeExpr) 
                ? GMacRootAst.ScalarType 
                : Context.CreateTypeMismatch("Symbolic expression type cannot be determined");
        }


        public ILanguageExpressionBasic Generate_UnaryPlus(ILanguageExpression expr)
        {
            SetOperands(expr);

            VerifyOperation_UnaryOperation();

            return BasicUnary.CreatePrimitive(_resultType, GMacOpInfo.UnaryPlus.OpName, _atomicOperand1);
        }

        public ILanguageExpressionBasic Generate_UnaryMinus(ILanguageExpression expr)
        {
            SetOperands(expr);

            VerifyOperation_UnaryOperation();

            return BasicUnary.CreatePrimitive(_resultType, GMacOpInfo.UnaryMinus.OpName, _atomicOperand1);
        }

        public ILanguageExpressionBasic Generate_UnaryNorm(GMacOpInfo opInfo, ILanguageExpression expr)
        {
            SetOperands(expr);

            VerifyOperation_UnaryNorm();

            return BasicUnary.CreatePrimitive(_resultType, opInfo.OpName, _atomicOperand1);
        }

        public ILanguageExpressionBasic Generate_UnaryReverse(ILanguageExpression expr)
        {
            SetOperands(expr);

            VerifyOperation_UnaryOperation();

            return BasicUnary.CreatePrimitive(_resultType, GMacOpInfo.UnaryReverse.OpName, _atomicOperand1);
        }

        public ILanguageExpressionBasic Generate_UnaryCliffordConjugate(ILanguageExpression expr)
        {
            SetOperands(expr);

            VerifyOperation_UnaryOperation();

            return BasicUnary.CreatePrimitive(_resultType, GMacOpInfo.UnaryCliffordConjugate.OpName, _atomicOperand1);
        }

        public ILanguageExpressionBasic Generate_UnaryGradeInversion(ILanguageExpression expr)
        {
            SetOperands(expr);

            VerifyOperation_UnaryOperation();

            return BasicUnary.CreatePrimitive(_resultType, GMacOpInfo.UnaryGradeInvolution.OpName, _atomicOperand1);
        }


        public ILanguageExpressionBasic Generate_BinaryPlus(ILanguageExpression expr1, ILanguageExpression expr2)
        {
            SetOperands(expr1, expr2);

            VerifyOperation_BinaryPlusMinus();

            return BasicBinary.CreatePrimitive(_resultType, GMacOpInfo.BinaryPlus.OpName, _atomicOperand1, _atomicOperand2);
        }

        public ILanguageExpressionBasic Generate_BinarySubtract(ILanguageExpression expr1, ILanguageExpression expr2)
        {
            SetOperands(expr1, expr2);

            VerifyOperation_BinaryPlusMinus();

            return BasicBinary.CreatePrimitive(_resultType, GMacOpInfo.BinarySubtract.OpName, _atomicOperand1, _atomicOperand2);
        }

        public ILanguageExpressionBasic Generate_BinaryTimesWithScalar(ILanguageExpression expr1, ILanguageExpression expr2)
        {
            SetOperands(expr1, expr2);

            VerifyOperation_Times();

            return BasicBinary.CreatePrimitive(_resultType, GMacOpInfo.BinaryTimesWithScalar.OpName, _atomicOperand1, _atomicOperand2);
        }

        public ILanguageExpressionBasic Generate_BinaryDivideByScalar(ILanguageExpression expr1, ILanguageExpression expr2)
        {
            SetOperands(expr1, expr2);

            VerifyOperation_Divide();

            return BasicBinary.CreatePrimitive(_resultType, GMacOpInfo.BinaryDivideByScalar.OpName, _atomicOperand1, _atomicOperand2);
        }


        public ILanguageExpressionBasic Generate_OrthogonalBinaryProduct(GMacOpInfo opInfo)
        {
            var operandsMvType = _atomicOperand1.ExpressionType as GMacFrameMultivector;

            if (!(
                    opInfo.IsMetricIndependent == false &&
                    GMacCompilerOptions.ForceOrthogonalMetricProducts &&
                    ReferenceEquals(operandsMvType, null) == false &&
                    operandsMvType.ParentFrame.IsDerivedFrame &&
                    operandsMvType.ParentFrame.SymbolicFrame.IsNonOrthogonal
                ))
                return BasicBinary.CreatePrimitive(_resultType, opInfo.OpName, _atomicOperand1, _atomicOperand2);

            var omv1 =
                Context.ActiveParentCommandBlock.NonOrthogonalToOrthogonalMultivector(
                    operandsMvType.ParentFrame,
                    _atomicOperand1
                    );

            var omv2 =
                Context.ActiveParentCommandBlock.NonOrthogonalToOrthogonalMultivector(
                    operandsMvType.ParentFrame,
                    _atomicOperand2
                    );

            var mv =
                Context.ActiveParentCommandBlock.ExpressionToAtomicExpression(
                    BasicBinary.CreatePrimitive(
                        operandsMvType.ParentFrame.BaseFrame.MultivectorType,
                        opInfo.OpName,
                        omv1,
                        omv2
                        )
                    );

            return
                Context.ActiveParentCommandBlock.OrthogonalToNonOrthogonalMultivectorTransform(
                    operandsMvType.ParentFrame,
                    mv
                    );
        }

        public ILanguageExpression Generate_OrthogonalScalarProduct(GMacOpInfo opInfo)
        {
            var operandsMvType = _atomicOperand1.ExpressionType as GMacFrameMultivector;

            if (!(
                    opInfo.IsMetricIndependent == false &&
                    GMacCompilerOptions.ForceOrthogonalMetricProducts &&
                    ReferenceEquals(operandsMvType, null) == false &&
                    operandsMvType.ParentFrame.IsDerivedFrame &&
                    operandsMvType.ParentFrame.SymbolicFrame.IsNonOrthogonal
                ))
                return BasicBinary.CreatePrimitive(_resultType, opInfo.OpName, _atomicOperand1, _atomicOperand2);

            var omv1 =
                Context.ActiveParentCommandBlock.NonOrthogonalToOrthogonalMultivector(
                    operandsMvType.ParentFrame,
                    _atomicOperand1
                    );

            var omv2 =
                Context.ActiveParentCommandBlock.NonOrthogonalToOrthogonalMultivector(
                    operandsMvType.ParentFrame,
                    _atomicOperand2
                    );

            //There is no need to apply the base-to-derived outermorphsm because the scalar product always gives a scalar
            // and the outer morphism of any scalar is itself
            return
                Context.ActiveParentCommandBlock.ExpressionToAtomicExpression(
                    BasicBinary.CreatePrimitive(
                        ScalarType,
                        opInfo.OpName,
                        omv1,
                        omv2
                        )
                    );
        }


        public ILanguageExpressionBasic Generate_BinaryProduct(GMacOpInfo opInfo, ILanguageExpression expr1, ILanguageExpression expr2)
        {
            SetOperands(expr1, expr2);

            VerifyOperation_MultivectorProduct();

            return Generate_OrthogonalBinaryProduct(opInfo);
        }

        public ILanguageExpressionBasic Generate_BinaryEuclideanProduct(GMacOpInfo opInfo, ILanguageExpression expr1, ILanguageExpression expr2)
        {
            SetOperands(expr1, expr2);

            VerifyOperation_MultivectorProduct();

            //The Euclidean products are metric-independent and the result is the same for orthogonal and non-orthogonal frames of the same dimension
            return
                BasicBinary
                .CreatePrimitive(_resultType, opInfo.OpName, _atomicOperand1, _atomicOperand2);
        }

        public ILanguageExpression Generate_BinaryScalarProduct(GMacOpInfo opInfo, ILanguageExpression expr1, ILanguageExpression expr2)
        {
            SetOperands(expr1, expr2);

            VerifyOperation_ScalarProduct();

            return Generate_OrthogonalScalarProduct(opInfo);
        }

        public ILanguageExpressionBasic Generate_BinaryEuclideanScalarProduct(GMacOpInfo opInfo, ILanguageExpression expr1, ILanguageExpression expr2)
        {
            SetOperands(expr1, expr2);

            VerifyOperation_ScalarProduct();

            //The Euclidean products are metric-independent and the result is the same for orthogonal and non-orthogonal frames of the same dimension
            return
                BasicBinary
                .CreatePrimitive(_resultType, opInfo.OpName, _atomicOperand1, _atomicOperand2);
        }


        public ILanguageExpressionBasic Generate_Diff(ILanguageExpression expr1, ILanguageExpression expr2)
        {
            SetOperands(expr1, expr2);

            VerifyOperation_Diff();

            return BasicBinary.CreatePrimitive(_resultType, GMacOpInfo.BinaryDiff.OpName, _atomicOperand1, _atomicOperand2);
        }

        public ILanguageExpressionBasic Generate_TypeCast(ILanguageType targetType, ILanguageOperator castOp, ILanguageExpression expr)
        {
            SetOperands(expr);

            VerifyOperation_TypeCast(targetType);

            return BasicUnary.CreateTypeCast(targetType, castOp, _atomicOperand1);
        }

        public ILanguageExpressionAtomic Generate_PolyadicOperand(ILanguageType lhsType, ILanguageExpression rhsExpr)
        {
            SetOperands(rhsExpr);

            VerifyOperation_PolyadicOperandAssignment(lhsType);

            return _atomicOperand1;
        }

        public ILanguageExpressionBasic Generate_TransformApplication(GMacMultivectorTransform transform, ILanguageExpression expr)
        {
            SetOperands(expr);

            VerifyOperation_TransformApplication(transform);

            return BasicUnary.Create(_resultType, transform, _atomicOperand1);
        }

        public ILanguageExpression Generate_SymbolicExpression(MathematicaScalar casExpr, OperandsByName operands)
        {
            var exprType = ScalarType;
            //var expr_type = this.VerifyType_SymbolicExpression(cas_expr, operands) as TypePrimitive;

            if (operands.OperandsDictionary.Count == 0)
                return ValuePrimitive<MathematicaScalar>.Create(exprType, casExpr);

            var basicExpr = BasicPolyadic.Create(exprType, GMacParametricSymbolicExpression.Create(casExpr));

            basicExpr.Operands = operands;

            return basicExpr;
        }

        protected override void Translate()
        {
        }
    }
}
