using System;
using System.Linq;
using GMac.GMacCompiler.Semantic.AST;
using IronyGrammars.DSLInterpreter;
using IronyGrammars.Semantic.Command;
using IronyGrammars.Semantic.Expression;
using IronyGrammars.Semantic.Expression.Basic;
using IronyGrammars.Semantic.Expression.Value;
using IronyGrammars.Semantic.Expression.ValueAccess;
using IronyGrammars.Semantic.Symbol;
using TextComposerLib;

namespace GMac.GMacCompiler.Semantic.ASTInterpreter.HighLevel
{
    /// <summary>
    /// Create a copy of a given macro command block body. The copy only differs from the original body in replacing
    /// all macro calls by the bodies of the called macros so the final compiled macro code has no macro calls.
    /// Also all command block and composite expressions are flattened to get a single command block. This is possible
    /// because GMac does not allow recursion.
    /// </summary>
    public sealed class HlMacroBodyCompiler : LanguageInterpreter<SymbolDataStore>
    {
        internal static CommandBlock Compile(CommandBlock macroCommandBlock)
        {
            if (!(macroCommandBlock.ParentLanguageSymbol is GMacMacro))
                throw new InvalidOperationException("The given command block is not a macro body");

            var baseMacro = (GMacMacro)macroCommandBlock.ParentLanguageSymbol;

            var compiler = new HlMacroBodyCompiler(baseMacro);

            compiler._compiledBlock = CommandBlock.Create(compiler.BaseMacro);

            foreach (var param in compiler.BaseMacro.Parameters)
                compiler.ActiveAr.AddSymbolData(param, param);

            macroCommandBlock.AcceptVisitor(compiler);

            return compiler._compiledBlock;
        }


        internal GMacAst GMacRootAst => (GMacAst)ParentDsl;

        internal GMacMacro BaseMacro { get; }

        private int _tempVariableIndex;

        private CommandBlock _compiledBlock;


        private HlMacroBodyCompiler(GMacMacro baseMacro)
            : base(baseMacro.ChildScope, new GMacValueAccessProcessor())
        {
            BaseMacro = baseMacro;
        }


        private SymbolLocalVariable CompileVariable(SymbolDataStore variable)
        {
            var varName = "CV" + (_tempVariableIndex++).ToString("X4");

            var declareCommand = _compiledBlock.DefineLocalVariable(varName, variable.SymbolType);

            var compiledVariable = (SymbolLocalVariable)declareCommand.DataStore;

            ActiveAr.AddSymbolData(variable, compiledVariable);

            return compiledVariable;
        }

        private LanguageValueAccess CompileLhsValueAccess(LanguageValueAccess valueAccess)
        {
            var compiledRootSymbol = valueAccess.RootSymbol;

            if (compiledRootSymbol is SymbolLocalVariable || compiledRootSymbol is SymbolProcedureParameter)
                compiledRootSymbol = GetSymbolData((SymbolDataStore)compiledRootSymbol);

            var compiledValueAccess = LanguageValueAccess.Create(compiledRootSymbol);

            if (valueAccess.IsPartialAccess)
            {
                //Note: This will not be correct for components of class ValueAccessStepBySymbol
                //because the component symbol must be compiled too. But GMac doesn't use these components anyway
                compiledValueAccess.Append(valueAccess.PartialAccessSteps);
            }

            return compiledValueAccess;
        }

        private ILanguageExpressionAtomic CompileRhsValueAccess(LanguageValueAccess valueAccess)
        {
            var compiledRootSymbol = valueAccess.RootSymbol;

            //Replace a reference to a constant by its value
            var namedValue = compiledRootSymbol as SymbolNamedValue;

            if (namedValue != null)
            {
                var compiledFullValue = namedValue.AssociatedValue;

                return 
                    valueAccess.IsFullAccess 
                    ? compiledFullValue.DuplicateValue(true) 
                    : ValueAccessProcessor.ReadPartialValue(compiledFullValue, valueAccess).DuplicateValue(true);
            }

            if (compiledRootSymbol is SymbolLocalVariable || compiledRootSymbol is SymbolProcedureParameter)
                compiledRootSymbol = GetSymbolData((SymbolDataStore)compiledRootSymbol);

            var compiledValueAccess = LanguageValueAccess.Create(compiledRootSymbol);

            if (valueAccess.IsPartialAccess)
            {
                //Note: This will not be correct for components of class ValueAccessStepBySymbol
                //because the component symbol must be compiled too. But GMac doesn't use these components anyway
                compiledValueAccess.Append(valueAccess.PartialAccessSteps);
            }

            return compiledValueAccess;
        }

        private ILanguageExpression CompileStructureConstructor(GMacStructureConstructor structureCons, OperandsByValueAccess operands)
        {
            ILanguageExpressionAtomic compiledDefaultValueSource = null;

            if (structureCons.HasDefaultValueSource)
                compiledDefaultValueSource =
                    (ILanguageExpressionAtomic)CompileExpression(structureCons.DefaultValueSource);

            var compiledOperands = OperandsByValueAccess.Create();

            foreach (var command in operands.AssignmentsList)
            {
                var compiledLhsValue = CompileLhsValueAccess(command.LhsValueAccess);

                var compiledRhsExpr = 
                    (ILanguageExpressionAtomic)CompileExpression(command.RhsExpression);

                compiledOperands.AddOperand(compiledLhsValue, compiledRhsExpr);
            }

            return structureCons.Structure.CreateConstructorExpression(compiledDefaultValueSource, compiledOperands);
        }

        private ILanguageExpression CompileMultivectorConstructor(GMacFrameMultivectorConstructor mvTypeCons, OperandsByIndex operands)
        {
            ILanguageExpressionAtomic compiledDefaultValueSource = null;

            if (mvTypeCons.HasDefaultValueSource)
                compiledDefaultValueSource =
                    (ILanguageExpressionAtomic)CompileExpression(mvTypeCons.DefaultValueSource);

            var compiledOperands = OperandsByIndex.Create();

            foreach (var pair in operands.OperandsDictionary)
            {
                var compiledRhsExpr = 
                    (ILanguageExpressionAtomic)CompileExpression(pair.Value);

                compiledOperands.AddOperand(pair.Key, compiledRhsExpr);
            }

            return mvTypeCons.MultivectorType.CreateConstructorExpression(compiledDefaultValueSource, compiledOperands);
        }

        private ILanguageExpression CompileSymbolicExpressionCall(GMacParametricSymbolicExpression expr, OperandsByName operands)
        {
            var compiledOperands = OperandsByName.Create();

            foreach (var pair in operands.OperandsDictionary)
            {
                var compiledRhsExpr = 
                    (ILanguageExpressionAtomic)CompileExpression(pair.Value);

                compiledOperands.AddOperand(pair.Key, compiledRhsExpr);
            }

            var compiledExpr = BasicPolyadic.Create(GMacRootAst.ScalarType, expr);

            compiledExpr.Operands = compiledOperands;

            return compiledExpr;
        }

        internal void CompileParameterAssignment(OperandsByValueAccessAssignment assignment)
        {
            //Compile the parameter in the active activation record
            var compiledLhsValue = CompileLhsValueAccess(assignment.LhsValueAccess);

            //Compile the rhs expression in the upper dynamic activation record
            var topAr = ActiveAr;

            ActiveAr = ActiveAr.UpperDynamicAr;

            var compiledRhsExpr = CompileExpression(assignment.RhsExpression);

            ActiveAr = topAr;

            _compiledBlock.AddCommand_Assign(compiledLhsValue, compiledRhsExpr);
        }

        private ILanguageExpression CompileMacroCall(GMacMacro macro, OperandsByValueAccess operands)
        {
            PushRecord(macro.ChildScope, false);

            foreach (var param in macro.Parameters)
            {
                CompileVariable(param);

                //Set the initial values of all parameters of called macro to their default
                var defaultAssignmentCommand = 
                    new OperandsByValueAccessAssignment(
                        LanguageValueAccess.Create(param), 
                        GMacRootAst.CreateDefaultValue(param.SymbolType)
                        );

                CompileParameterAssignment(defaultAssignmentCommand);
            }

            foreach (var command in operands.AssignmentsList)
                CompileParameterAssignment(command);

            //this.Visit(macro.ProcedureBody);
            Visit(macro.OptimizedCompiledBody);

            var compiledOutputVariable = GetSymbolData(macro.OutputParameter);

            PopRecord();

            return LanguageValueAccess.Create(compiledOutputVariable);
        }

        private ILanguageExpression CompileBasicPolyadic(BasicPolyadic expr)
        {
            var compiledOperator = expr.Operator.DuplicateOperator();

            if (expr.Operator is GMacStructureConstructor)
                return CompileStructureConstructor((GMacStructureConstructor)compiledOperator, expr.Operands.AsByValueAccess);

            if (expr.Operator is GMacFrameMultivectorConstructor)
                return CompileMultivectorConstructor((GMacFrameMultivectorConstructor)compiledOperator, expr.Operands.AsByIndex);

            if (expr.Operator is GMacMacro)
                return CompileMacroCall((GMacMacro)compiledOperator, expr.Operands.AsByValueAccess);

            if (expr.Operator is GMacParametricSymbolicExpression)
                return CompileSymbolicExpressionCall((GMacParametricSymbolicExpression)compiledOperator, expr.Operands.AsByName);

            throw new InvalidOperationException("Expression not recognized");
        }

        private ILanguageExpression CompileBasicBinary(BasicBinary expr)
        {
            var compiledOperator = expr.Operator.DuplicateOperator();

            var compiledOperand1 = (ILanguageExpressionAtomic)CompileExpression(expr.Operand1);

            var compiledOperand2 = (ILanguageExpressionAtomic)CompileExpression(expr.Operand2);

            return BasicBinary.Create(expr.ExpressionType, compiledOperator, compiledOperand1, compiledOperand2);
        }

        private ILanguageExpression CompileBasicUnary(BasicUnary expr)
        {
            var compiledOperator = expr.Operator.DuplicateOperator();

            var compiledOperand = (ILanguageExpressionAtomic)CompileExpression(expr.Operand);

            return BasicUnary.Create(expr.ExpressionType, compiledOperator, compiledOperand);
        }

        private ILanguageExpression CompileCompositeExpression(CompositeExpression expr)
        {
            PushRecord(expr.ChildScope, true);

            foreach (var subCommand in expr.Commands)
                subCommand.AcceptVisitor(this);

            var compiledOutputVariable = GetSymbolData(expr.OutputVariable);

            PopRecord();

            return LanguageValueAccess.Create(compiledOutputVariable);
        }

        private ILanguageExpression CompileExpression(ILanguageExpression expr)
        {
            var value = expr as ILanguageValue;

            if (value != null)
                return value.DuplicateValue(true);

            var valueAccess = expr as LanguageValueAccess;

            if (valueAccess != null)
                return CompileRhsValueAccess(valueAccess);

            var basicUnary = expr as BasicUnary;

            if (basicUnary != null)
                return CompileBasicUnary(basicUnary);

            var basicBinary = expr as BasicBinary;

            if (basicBinary != null)
                return CompileBasicBinary(basicBinary);

            var basicPolyadic = expr as BasicPolyadic;

            if (basicPolyadic != null)
                return CompileBasicPolyadic(basicPolyadic);

            var compositeExpression = expr as CompositeExpression;

            if (compositeExpression != null)
                return CompileCompositeExpression(compositeExpression);

            throw new InvalidOperationException("Expression not recognized");
        }

        public void Visit(CommandComment command)
        {
            //_CompiledBlock.AddCommand_Comment(command.ToString());
        }

        public void Visit(CommandDeclareVariable command)
        {
            CompileVariable(command.DataStore);
        }

        public void Visit(CommandAssign command)
        {
            //_CompiledBlock.AddCommand_Comment(command.ToString());

            var compiledLhsValue = CompileLhsValueAccess(command.LhsValueAccess);

            var compiledRhsExpr = CompileExpression(command.RhsExpression);
            
            _compiledBlock.AddCommand_Assign(compiledLhsValue, compiledRhsExpr);
        }

        public void Visit(CommandBlock command)
        {
            PushRecord(command.ChildScope, true);

            foreach (var subCommand in command.Commands)
                subCommand.AcceptVisitor(this);

            PopRecord();
        }
    }
}
