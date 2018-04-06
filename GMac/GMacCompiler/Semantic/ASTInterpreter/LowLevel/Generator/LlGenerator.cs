using System;
using System.Linq;
using GMac.GMacCompiler.Semantic.AST;
using GMac.GMacCompiler.Semantic.AST.Extensions;
using GMac.GMacMath.Symbolic.Multivectors;
using IronyGrammars.DSLInterpreter;
using IronyGrammars.Semantic.Command;
using IronyGrammars.Semantic.Expression;
using IronyGrammars.Semantic.Expression.Basic;
using IronyGrammars.Semantic.Expression.Value;
using IronyGrammars.Semantic.Expression.ValueAccess;
using IronyGrammars.Semantic.Symbol;
using IronyGrammars.Semantic.Type;
using SymbolicInterface.Mathematica.Expression;
using TextComposerLib;
using TextComposerLib.Logs.Progress;

namespace GMac.GMacCompiler.Semantic.ASTInterpreter.LowLevel.Generator
{
    /// <summary>
    /// This generates low-level statements (assignments on primitive data types and expressions) from high-level
    /// optimized GMac macro code. This is not specific to any target language but just a lower-level intermediate form.
    /// This operation is implemented as a language expression evaluation process on macro code with special 
    /// characteristics for low-level code generation.
    /// </summary>
    internal sealed class LlGenerator : LanguageExpressionEvaluator
    {
        /// <summary>
        /// The base macro for low-level code generation
        /// </summary>
        internal GMacMacro BaseMacro { get; }

        /// <summary>
        /// True if the code generation process is complete
        /// </summary>
        internal bool GenerationComplete { get; private set; }

        /// <summary>
        /// The data table holding final generated low level code
        /// </summary>
        internal LlDataTable DataTable { get; }

        /// <summary>
        /// Parent GMac DSL for this generator
        /// </summary>
        internal GMacAst GMacRootAst => (GMacAst)ParentDsl;

        public override string ProgressSourceId => "GMac Low-level Generator";

        public override ProgressComposer Progress => GMacSystemUtils.Progress;


        internal LlGenerator(GMacMacro baseMacro)
            : base(baseMacro.ChildScope, new GMacValueAccessProcessor())
        {
            BaseMacro = baseMacro;

            DataTable = new LlDataTable(GMacRootAst);
        }

        /// <summary>
        /// Assign a constant value to an input parameter of the base macro
        /// </summary>
        /// <param name="hlValueAccess">The full or partial macro parameter</param>
        /// <param name="assignedValue">The constant value to be assigned to the parameter</param>
        internal void DefineParameter(LanguageValueAccess hlValueAccess, ILanguageValue assignedValue)
        {
            //The parameter must be an input parameter
            if (hlValueAccess.RootSymbolAsParameter.DirectionOut)
                throw new InvalidOperationException("Output parameters cannot be assigned initial values");

            //If the value access is of primitive type just assign the value directly
            var valuePrimitive = assignedValue as ILanguageValuePrimitive;

            if (valuePrimitive != null)
            {
                DataTable.DefineConstantInputParameter(hlValueAccess, valuePrimitive);

                return;
            }

            //If the value access is of compoiste type break it into all primitive components with corresponding 
            //primitive values
            var assignmentsList = hlValueAccess.ExpandAndAssignAll(assignedValue);

            foreach (var assignment in assignmentsList)
                DataTable.DefineConstantInputParameter(assignment.Item1, assignment.Item2);
        }

        /// <summary>
        /// Define an input or output macro parameter as a low-level variable
        /// </summary>
        /// <param name="hlValueAccess">The full or partial macro parameter</param>
        /// <param name="testValueExpr"></param>
        internal void DefineParameter(LanguageValueAccess hlValueAccess)//, Expr testValueExpr = null)
        {
            if (hlValueAccess.RootSymbolAsParameter.DirectionIn)
                DataTable.DefineVariableInputParameter(hlValueAccess);//, testValueExpr);

            else
                DataTable.DefineOutputParameter(hlValueAccess);
        }

        /// <summary>
        /// Define all input and output macro parameters as low-level variables. This is not the usual method for 
        /// low-level code generation because multivectors are sparse in most problems.
        /// </summary>
        internal void DefineAllParameters()
        {
            foreach (var param in BaseMacro.Parameters)
                if (param.DirectionIn)
                    DataTable.DefineVariableInputParameter(param);
                else
                    DataTable.DefineOutputParameter(param);
        }


        //These methods are inherited from LanguageExpressionEvaluator but never used for low-level code generation
        #region Not Implemented Methods

        protected override ILanguageValue ReadSymbolFullValue(LanguageSymbol symbol)
        {
            throw new NotImplementedException();
            //if (symbol is SymbolNamedValue)
            //    return ((SymbolNamedValue)symbol).AssociatedValue;

            ////if (symbol is SymbolLocalVariable || symbol is SymbolProcedureParameter)
            ////    return this.GetSymbolData((SymbolDataStore)symbol);

            //throw new InvalidOperationException("Invalid symbol type");
        }

        protected override bool AllowUpdateSymbolValue(LanguageSymbol symbol)
        {
            throw new NotImplementedException();
        }

        public override ILanguageValue Visit(CompositeExpression expr)
        {
            throw new NotImplementedException();
        }

        public override ILanguageValue Visit(CommandDeclareVariable command)
        {
            throw new NotImplementedException();
        }

        #endregion


        //Methods for processing high-level RHS expressions into values with suitable composition for low-level code generation
        #region High-Level Expression Evaluation and Processing Methods

        /// <summary>
        /// Evaluate a cast to a scalar value operation
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        private ILanguageValue EvaluateBasicUnaryCastToScalar(BasicUnary expr)
        {
            var value1 = expr.Operand.AcceptVisitor(this);

            if (value1.ExpressionType.IsInteger())
                return ValuePrimitive<MathematicaScalar>.Create(
                    (TypePrimitive)expr.ExpressionType,
                    ((ValuePrimitive<MathematicaScalar>)value1).Value
                    );

            throw new InvalidOperationException("Invalid cast operation");
        }

        /// <summary>
        /// Evaluate a cast to multivector value operation
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        private ILanguageValue EvaluateBasicUnaryCastToMultivector(BasicUnary expr)
        {
            var value1 = expr.Operand.AcceptVisitor(this);

            var mvType = (GMacFrameMultivector) expr.Operator;

            if (value1.ExpressionType.IsNumber())
            {
                var scalarValue = ((ValuePrimitive<MathematicaScalar>) value1).Value;

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
                var mvValue = (GMacValueMultivector) value1;

                return GMacValueMultivector.Create(
                    mvType,
                    GaSymMultivector.CreateCopy(mvValue.SymbolicMultivector)
                    );
            }

            throw new InvalidOperationException("Invalid cast operation");
        }

        /// <summary>
        /// Evaluate a multivector transform operation
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        private GMacValueMultivector EvaluateBasicUnaryMultivectorTransform(BasicUnary expr)
        {
            var value1 = (GMacValueMultivector)expr.Operand.AcceptVisitor(this);

            var transform = (GMacMultivectorTransform)expr.Operator;

            return GMacValueMultivector.Create(
                transform.TargetFrame.MultivectorType,
                transform.AssociatedSymbolicTransform[value1.SymbolicMultivector]
                );
        }

        /// <summary>
        /// Evaluate a structure construction operation
        /// </summary>
        /// <param name="structureCons"></param>
        /// <param name="operands"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Evaluate a multivector construction operation
        /// </summary>
        /// <param name="mvTypeCons"></param>
        /// <param name="operands"></param>
        /// <returns></returns>
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
        /// Process a high-level RHS value into a form suitable for low-leve code generation
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override ILanguageValue Visit(ILanguageValue value)
        {
            //If this is a primitive value convert it into a Mathematica Scalar primitive value
            var valuePrimitive = value as ILanguageValuePrimitive;

            return 
                valuePrimitive == null 
                ? value 
                : valuePrimitive.ToScalarValue();
        }

        /// <summary>
        /// Process a high-level RHS value access into a value suitable for low-level code generation
        /// </summary>
        /// <param name="valueAccess"></param>
        /// <returns></returns>
        public override ILanguageValue Visit(LanguageValueAccess valueAccess)
        {
            //This should never happen. All named values (like GMac constants) should be removed from macro code
            //and replaced by their values before low-level code generation begins
            if (valueAccess.RootSymbol is SymbolNamedValue)
                throw new InvalidOperationException();

            //This value access is is of primitive type. Read the corresponding low-level value\variable from the
            //low level data table and return a primitive value
            if (valueAccess.ExpressionType is TypePrimitive)
                return DataTable.ReadRhsPrimitiveValue(valueAccess);

            //This value access is of composite multivector type. Read the corresponding low-level values\variables from the
            //low level data table and return a composite multivector value
            if (valueAccess.ExpressionType is GMacFrameMultivector)
                return DataTable.ReadRhsMultivectorValue(valueAccess);

            //This value access is of composite structure type. Read the corresponding low-level values\variables from the
            //low level data table and return a composite structure value
            if (valueAccess.ExpressionType is GMacStructure)
                return DataTable.ReadRhsStructureValue(valueAccess);

            //This should never happen
            throw new InvalidOperationException();
        }

        /// <summary>
        /// Evaluate a high-level basic unary expression into a value suitable for low-level code generation
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        public override ILanguageValue Visit(BasicUnary expr)
        {
            //Evaluate a cast-to-scalar operation
            var typePrimitive = expr.Operator as TypePrimitive;

            if (typePrimitive != null && typePrimitive.IsScalar())
                return EvaluateBasicUnaryCastToScalar(expr);

            //Evaluate a cast-to-multivector operation
            if (expr.Operator is GMacFrameMultivector)
                return EvaluateBasicUnaryCastToMultivector(expr);

            //Evaluate a multivector linear transform operation
            if (expr.Operator is GMacMultivectorTransform)
                return EvaluateBasicUnaryMultivectorTransform(expr);

            //Evaluate all other types of unary operations using standard GMac unary evaluation process
            var value1 = expr.Operand.AcceptVisitor(this);

            var unaryOp = GMacInterpreterUtils.UnaryEvaluators[expr.Operator.OperatorName];

            return value1.AcceptOperation(unaryOp);
        }

        /// <summary>
        /// Evaluate a high-level basic binary expression into a value suitable for low-level code generation
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        public override ILanguageValue Visit(BasicBinary expr)
        {
            //Evaluate binary operations using standard GMac binary evaluation process
            var value1 = expr.Operand1.AcceptVisitor(this);

            var value2 = expr.Operand2.AcceptVisitor(this);

            var binaryOp = GMacInterpreterUtils.BinaryEvaluators[expr.Operator.OperatorName];

            return value1.AcceptOperation(binaryOp, value2);
        }

        /// <summary>
        /// Evaluate a high-level basic polyadic expression into a value suitable for low-level code generation
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        public override ILanguageValue Visit(BasicPolyadic expr)
        {
            //Evaluate a structure construction operation
            var structureConstructor = expr.Operator as GMacStructureConstructor;

            if (structureConstructor != null)
                return EvaluateBasicPolyadicStructureConstructor(structureConstructor, (OperandsByValueAccess)expr.Operands);

            //Evaluate a multivector construction operation
            var multivectorConstructor = expr.Operator as GMacFrameMultivectorConstructor;

            if (multivectorConstructor != null)
                return EvaluateBasicPolyadicMultivectorConstructor(multivectorConstructor, (OperandsByIndex)expr.Operands);

            //Evaluate a parametric symbolic expression call operation
            var symbolicExpression = expr.Operator as GMacParametricSymbolicExpression;

            if (symbolicExpression != null)
                return EvaluateBasicPolyadicSymbolicExpressionCall(symbolicExpression, (OperandsByName)expr.Operands);

            //This should never happen because all macro calles should be replaced by called macro code during
            //high-level macro code optimization before low-level generation starts
            //if (expr.Operator is GMacMacro)
            //    return this.EvaluateBasicPolyadicMacroCall((GMacMacro)expr.Operator, (OperandsByValueAccess)expr.Operands);

            throw new InvalidOperationException("Invalid polyadic operation");
        }

        #endregion


        /// <summary>
        /// Update the low-level variables related to the given high-level variable using the given RHS value
        /// </summary>
        /// <param name="lhsValueAccess">The high-level LHS variable</param>
        /// <param name="rhsValue">The high-level RHS value</param>
        private void WriteToLhsVariables(LanguageValueAccess lhsValueAccess, ILanguageValue rhsValue)
        {
            //The high-level variable is of primitive type
            if (lhsValueAccess.ExpressionType is TypePrimitive)
                DataTable.WriteLhsPrimitiveValue(lhsValueAccess, (ValuePrimitive<MathematicaScalar>)rhsValue);

            //The high-level variable is of composite multivector type
            else if (lhsValueAccess.ExpressionType is GMacFrameMultivector)
                DataTable.WriteLhsMultivectorValue(lhsValueAccess, (GMacValueMultivector)rhsValue);

            //The high-level variable is of composite structure type
            else if (lhsValueAccess.ExpressionType is GMacStructure)
                DataTable.WriteLhsStructureValue(lhsValueAccess, (ValueStructureSparse)rhsValue);

            //This should never happen
            else
                throw new InvalidOperationException();
        }

        /// <summary>
        /// Visit a high-level assignment command to convert into one or more low-level assignment commands
        /// </summary>
        /// <param name="command">Th high-level assignment command to be processed</param>
        /// <returns>Null</returns>
        public override ILanguageValue Visit(CommandAssign command)
        {
            //Process the RHS expression into a suitable language value
            var rhsValue = command.RhsExpression.AcceptVisitor(this);

            //Update all low-level variables related to the LHS of the assignment using the processed RHS value
            WriteToLhsVariables(command.LhsValueAccess, rhsValue);

            return null;
        }

        /// <summary>
        /// Visit a command block to precess its high-level commands into low-level commands
        /// </summary>
        /// <param name="command">The command block to be processed</param>
        /// <returns>Null</returns>
        public override ILanguageValue Visit(CommandBlock command)
        {
            foreach (var childCommand in command.CommandsNoDeclare.OfType<CommandAssign>())
                Visit(childCommand);

            return null;
        }

        /// <summary>
        /// Generate the low-level code by filling the low-level data table from the optimized code body of 
        /// the base macro
        /// </summary>
        internal LlDataTable GenerateLowLevelItems()
        {
            if (GenerationComplete)
            {
                this.ReportNormal("Low Level Generator Output", DataTable);

                return DataTable;
            }

            BaseMacro.OptimizedCompiledBody.AcceptVisitor(this);

            GenerationComplete = true;

            this.ReportNormal("Low Level Generator Output", DataTable);

            return DataTable;
        }


        public override string ToString()
        {
            return DataTable.ToString();
        }
    }
}
