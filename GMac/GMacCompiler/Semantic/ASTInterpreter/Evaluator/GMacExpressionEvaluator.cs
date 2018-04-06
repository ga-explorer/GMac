using System;
using GMac.GMacCompiler.Semantic.AST;
using GMac.GMacCompiler.Semantic.AST.Extensions;
using GMac.GMacMath.Symbolic;
using GMac.GMacMath.Symbolic.Multivectors;
using IronyGrammars.DSLInterpreter;
using IronyGrammars.Semantic.Command;
using IronyGrammars.Semantic.Expression;
using IronyGrammars.Semantic.Expression.Basic;
using IronyGrammars.Semantic.Expression.Value;
using IronyGrammars.Semantic.Expression.ValueAccess;
using IronyGrammars.Semantic.Scope;
using IronyGrammars.Semantic.Symbol;
using IronyGrammars.Semantic.Type;
using SymbolicInterface.Mathematica.Expression;
using TextComposerLib;
using TextComposerLib.Logs.Progress;

namespace GMac.GMacCompiler.Semantic.ASTInterpreter.Evaluator
{
    /// <summary>
    /// This class is an interpreter for evaluating GMac expressions
    /// </summary>
    public sealed class GMacExpressionEvaluator : LanguageExpressionEvaluator
    {
        /// <summary>
        /// Used for dynamic commands processing. An empty command block is used to create the initial
        /// activation record of the created evaluator. 
        /// 
        /// Automatically generated commands can be evaluated dynamically as needed using the evaluator's 
        /// Visit() methods.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static GMacExpressionEvaluator CreateForDynamicEvaluation(CommandBlock command)
        {
            return new GMacExpressionEvaluator(command.ChildScope);
        }

        /// <summary>
        /// Evaluate the given expression inside the given scope
        /// </summary>
        /// <param name="rootScope"></param>
        /// <param name="expr"></param>
        /// <returns></returns>
        internal static ILanguageValue EvaluateExpression(LanguageScope rootScope, ILanguageExpression expr)
        {
            var evaluator = new GMacExpressionEvaluator(rootScope);

            return expr.AcceptVisitor(evaluator);
        }

        /// <summary>
        /// Evaluates the given expression if it's a simple expression else it returns null
        /// </summary>
        /// <param name="rootScope"></param>
        /// <param name="expr"></param>
        /// <returns></returns>
        internal static ILanguageValue EvaluateExpressionIfSimple(LanguageScope rootScope, ILanguageExpression expr)
        {
            if (!expr.IsSimpleExpression)
                return null;

            var evaluator = new GMacExpressionEvaluator(rootScope);

            return expr.AcceptVisitor(evaluator);
        }


        internal GMacAst GMacRootAst => (GMacAst)ParentDsl;

        public override string ProgressSourceId => "GMac Expression Evaluator";

        public override ProgressComposer Progress => GMacSystemUtils.Progress;


        private GMacExpressionEvaluator(LanguageScope rootScope)
            : base(rootScope, new GMacValueAccessProcessor())
        {
        }


        protected override ILanguageValue ReadSymbolFullValue(LanguageSymbol symbol)
        {
            var symbol1 = symbol as SymbolNamedValue;

            if (symbol1 != null)
                return symbol1.AssociatedValue;

            if (symbol is SymbolLocalVariable || symbol is SymbolProcedureParameter)
                return GetSymbolData((SymbolDataStore)symbol);

            throw new InvalidOperationException("Invalid symbol type");
        }

        protected override bool AllowUpdateSymbolValue(LanguageSymbol symbol)
        {
            return (symbol is SymbolLocalVariable || symbol is SymbolProcedureParameter);
        }

        protected override void UpdateSymbolValue(LanguageValueAccess valueAccess, ILanguageValue value)
        {
            if (GMacCompilerOptions.SimplifyLowLevelRhsValues)
                value.Simplify();

            base.UpdateSymbolValue(valueAccess, value);
        }

        private ILanguageValue EvaluateBasicUnaryCastToScalar(BasicUnary expr)
        {
            var value1 = expr.Operand.AcceptVisitor(this);

            if (value1.ExpressionType.IsInteger())
                return ValuePrimitive<MathematicaScalar>.Create(
                    (TypePrimitive)expr.ExpressionType,
                    MathematicaScalar.Create(SymbolicUtils.Cas, ((ValuePrimitive<int>)value1).Value)
                    );

            throw new InvalidOperationException("Invalid cast operation");
        }

        private ILanguageValue EvaluateBasicUnaryCastToMultivector(BasicUnary expr)
        {
            var value1 = expr.Operand.AcceptVisitor(this);

            var mvType = (GMacFrameMultivector)expr.Operator;

            if (value1.ExpressionType.IsInteger())
            {
                var intValue = ((ValuePrimitive<int>)value1).Value;

                return GMacValueMultivector.Create(
                    mvType,
                    GaSymMultivector.CreateScalar(
                        mvType.ParentFrame.GaSpaceDimension,
                        MathematicaScalar.Create(SymbolicUtils.Cas, intValue)
                        )
                    );
            }

            if (value1.ExpressionType.IsScalar())
            {
                var scalarValue = ((ValuePrimitive<MathematicaScalar>)value1).Value;

                return GMacValueMultivector.Create(
                    mvType,
                    GaSymMultivector.CreateScalar(
                        mvType.ParentFrame.GaSpaceDimension,
                        scalarValue
                        )
                    );
            }

            if (value1.ExpressionType.IsFrameMultivector() &&
                value1.ExpressionType.GetFrame().VSpaceDimension == mvType.ParentFrame.VSpaceDimension)
            {
                var mvValue = ((GMacValueMultivector)value1);

                return GMacValueMultivector.Create(
                    mvType,
                    GaSymMultivector.CreateCopy(mvValue.SymbolicMultivector)
                    );
            }

            throw new InvalidOperationException("Invalid cast operation");
        }

        private GMacValueMultivector EvaluateBasicUnaryMultivectorTransform(BasicUnary expr)
        {
            var value1 = (GMacValueMultivector)expr.Operand.AcceptVisitor(this);

            var transform = (GMacMultivectorTransform)expr.Operator;

            return GMacValueMultivector.Create(
                transform.TargetFrame.MultivectorType,
                transform.AssociatedSymbolicTransform[value1.SymbolicMultivector]
                );
        }


        private ValueStructureSparse EvaluateBasicPolyadicStructureConstructor(GMacStructureConstructor structureCons, OperandsByValueAccess operands)
        {
            ValueStructureSparse value;

            if (structureCons.HasDefaultValueSource)
                value = 
                    (ValueStructureSparse)structureCons
                    .DefaultValueSource
                    .AcceptVisitor(this)
                    .DuplicateValue(true);

            else
                value = 
                    (ValueStructureSparse)GMacRootAst
                    .CreateDefaultValue(structureCons.Structure);

            foreach (var command in operands.AssignmentsList)
            {
                var rhsValue = command.RhsExpression.AcceptVisitor(this);

                if (command.LhsValueAccess.IsFullAccess)
                    value[command.LhsValueAccess.RootSymbol.ObjectName] = rhsValue.DuplicateValue(true);

                else
                {
                    var sourceValue = value[command.LhsValueAccess.RootSymbol.ObjectName];

                    ValueAccessProcessor.WritePartialValue(sourceValue, command.LhsValueAccess, rhsValue.DuplicateValue(true));
                }
            }

            return value;
        }

        private GMacValueMultivector EvaluateBasicPolyadicMultivectorConstructor(GMacFrameMultivectorConstructor mvTypeCons, OperandsByIndex operands)
        {
            GMacValueMultivector value;

            if (mvTypeCons.HasDefaultValueSource)
                value =
                    (GMacValueMultivector)mvTypeCons
                    .DefaultValueSource
                    .AcceptVisitor(this)
                    .DuplicateValue(true);

            else
                value =
                    (GMacValueMultivector)GMacRootAst
                    .CreateDefaultValue(mvTypeCons.MultivectorType);

            foreach (var pair in operands.OperandsDictionary)
            {
                var rhsValue = (ValuePrimitive<MathematicaScalar>)pair.Value.AcceptVisitor(this);

                //TODO: Is it necessary to make a copy of the RHS value in all cases?
                value[pair.Key] = (ValuePrimitive<MathematicaScalar>)rhsValue.DuplicateValue(true);
            }

            return value;
        }

        private ILanguageValue EvaluateBasicPolyadicMacroCall(GMacMacro macro, OperandsByValueAccess operands)
        {
            PushRecord(macro.ChildScope, false);

            //Initialize parameters to their default values
            foreach (var param in macro.Parameters)
            {
                var paramValue = GMacRootAst.CreateDefaultValue(param.SymbolType);

                ActiveAr.AddSymbolData(param, paramValue);
            }

            //Modify assigned parameters values from macro call operands
            foreach (var command in operands.AssignmentsList)
                Visit(command);

            //Execute macro body
            macro.SymbolBody.AcceptVisitor(this);
            //macro.OptimizedCompiledBody.AcceptVisitor(this);

            //Read output parameter value
            var value = ActiveAr.GetSymbolData(macro.FirstOutputParameter);

            PopRecord();

            return value;
        }

        private ValuePrimitive<MathematicaScalar> EvaluateBasicPolyadicSymbolicExpressionCall(GMacParametricSymbolicExpression symbolicExpr, OperandsByName operands)
        {
            var exprText = symbolicExpr.AssociatedMathematicaScalar.ExpressionText;

            foreach (var pair in operands.OperandsDictionary)
            {
                var rhsValue = pair.Value.AcceptVisitor(this);

                exprText = exprText.Replace(pair.Key, ((ValuePrimitive<MathematicaScalar>)rhsValue).Value.ExpressionText);
            }

            var scalar =
                MathematicaScalar.Create(symbolicExpr.AssociatedMathematicaScalar.CasInterface, exprText);

            //May be required
            //scalar = MathematicaScalar.Create(scalar.CAS, scalar.CASEvaluator.FullySimplify(scalar.MathExpr));

            return
                ValuePrimitive<MathematicaScalar>.Create(
                    GMacRootAst.ScalarType,
                    scalar
                    );
        }


        /// <summary>
        /// Evaluate a basic unary expression
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        public override ILanguageValue Visit(BasicUnary expr)
        {
            var typePrimitive = expr.Operator as TypePrimitive;

            if (typePrimitive != null && typePrimitive.IsScalar())
                return EvaluateBasicUnaryCastToScalar(expr);

            if (expr.Operator is GMacFrameMultivector)
                return EvaluateBasicUnaryCastToMultivector(expr);

            if (expr.Operator is GMacMultivectorTransform)
                return EvaluateBasicUnaryMultivectorTransform(expr);

            var value1 = expr.Operand.AcceptVisitor(this);

            var unaryOp = GMacInterpreterUtils.UnaryEvaluators[expr.Operator.OperatorName];

            return value1.AcceptOperation(unaryOp);
        }

        /// <summary>
        /// Evaluate a basic binary expression
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        public override ILanguageValue Visit(BasicBinary expr)
        {
            var value1 = expr.Operand1.AcceptVisitor(this);

            var value2 = expr.Operand2.AcceptVisitor(this);

            var binaryOp = GMacInterpreterUtils.BinaryEvaluators[expr.Operator.OperatorName];

            return value1.AcceptOperation(binaryOp, value2);
        }

        /// <summary>
        /// Evaluate a basic polyadic expression
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        public override ILanguageValue Visit(BasicPolyadic expr)
        {
            var structureConstructor = expr.Operator as GMacStructureConstructor;

            if (structureConstructor != null)
                return EvaluateBasicPolyadicStructureConstructor(structureConstructor, (OperandsByValueAccess)expr.Operands);

            var multivectorConstructor = expr.Operator as GMacFrameMultivectorConstructor;

            if (multivectorConstructor != null)
                return EvaluateBasicPolyadicMultivectorConstructor(multivectorConstructor, (OperandsByIndex)expr.Operands);

            var macro = expr.Operator as GMacMacro;

            if (macro != null)
                return EvaluateBasicPolyadicMacroCall(macro, (OperandsByValueAccess)expr.Operands);

            var symbolicExpr = expr.Operator as GMacParametricSymbolicExpression;

            if (symbolicExpr != null)
                return EvaluateBasicPolyadicSymbolicExpressionCall(symbolicExpr, (OperandsByName)expr.Operands);

            throw new InvalidOperationException("Invalid operation");
        }

        /// <summary>
        /// Evaluate a composite expression
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        public override ILanguageValue Visit(CompositeExpression expr)
        {
            PushRecord(expr.ChildScope, true);

            foreach (var subCommand in expr.Commands)
                subCommand.AcceptVisitor(this);

            var value = ActiveAr.GetSymbolData(expr.OutputVariable);

            PopRecord();

            return value;
        }

        /// <summary>
        /// Execute a variable declaration command
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public override ILanguageValue Visit(CommandDeclareVariable command)
        {
            var symbolValue = GMacRootAst.CreateDefaultValue(command.DataStore.SymbolType);

            ActiveAr.AddSymbolData(command.DataStore, symbolValue);

            return null;
        }

        /// <summary>
        /// Assign values to parameters or structure members
        /// </summary>
        /// <param name="assignment"></param>
        /// <returns></returns>
        public ILanguageValue Visit(OperandsByValueAccessAssignment assignment)
        {
            var topAr = ActiveAr;

            ActiveAr = ActiveAr.UpperDynamicAr;

            var rhsValue = assignment.RhsExpression.AcceptVisitor(this);

            ActiveAr = topAr;

            //TODO: Is it necessary to make a copy of the RHS value in all cases?

            UpdateSymbolValue(assignment.LhsValueAccess, rhsValue.DuplicateValue(true));

            return null;
        }

        /// <summary>
        /// Execute an assignment command
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public override ILanguageValue Visit(CommandAssign command)
        {
            var rhsValue = command.RhsExpression.AcceptVisitor(this);

            //TODO: Is it necessary to make a copy of the RHS value in all cases?

            UpdateSymbolValue(command.LhsValueAccess, rhsValue.DuplicateValue(true));
            
            return null;
        }

        /// <summary>
        /// Execute a command block
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public override ILanguageValue Visit(CommandBlock command)
        {
            PushRecord(command.ChildScope, true);

            foreach (var subCommand in command.Commands)
                subCommand.AcceptVisitor(this);

            PopRecord();

            return null;
        }
    }
}
