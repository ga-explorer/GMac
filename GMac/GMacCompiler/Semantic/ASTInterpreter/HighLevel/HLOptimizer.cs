using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMac.GMacCompiler.Semantic.AST;
using GMac.GMacCompiler.Semantic.ASTInterpreter.Evaluator;
using IronyGrammars.Semantic.Command;
using IronyGrammars.Semantic.Expression;
using IronyGrammars.Semantic.Expression.Basic;
using IronyGrammars.Semantic.Expression.ValueAccess;
using IronyGrammars.Semantic.Symbol;
using IronyGrammars.Semantic.Type;

namespace GMac.GMacCompiler.Semantic.ASTInterpreter.HighLevel
{
    /// <summary>
    /// This high-level optimizer accepts only compiled macros with no commands other than declarations and assignments
    /// </summary>
    internal sealed class HlOptimizer
    {
        /// <summary>
        /// Optimize the compiled body of the given macro
        /// </summary>
        /// <param name="baseMacro"></param>
        /// <returns></returns>
        public static CommandBlock OptimizeMacro(GMacMacro baseMacro)
        {
            var optimizer = new HlOptimizer(baseMacro)
            {
                _optimizedBlock = CommandBlock.Create(baseMacro)
            };

            optimizer.Optimize();

            return optimizer._optimizedBlock;
        }


        /// <summary>
        /// The base GMac macro to be optimized
        /// </summary>
        public GMacMacro BaseMacro { get; }

        /// <summary>
        /// This is a helper object for performing value access processes on expressions
        /// </summary>
        private readonly GMacValueAccessProcessor _valueAccessProcessor = new GMacValueAccessProcessor();

        /// <summary>
        /// The list of commands used during high-level optimization
        /// </summary>
        private readonly List<HlCommandInfo> _commandsInfoList = new List<HlCommandInfo>();

        /// <summary>
        /// The l-values table holding information of l-values during high-level optimization
        /// </summary>
        private readonly HlLValuesTable _lValuesTable = new HlLValuesTable();

        /// <summary>
        /// The use information for basic expressions. The key to this dictionary is the text of the basic expression
        /// while the value is a list of all commands with this expression in the RHS
        /// </summary>
        private readonly Dictionary<string, List<HlCommandInfo>> _rhsExpressionsTable = new Dictionary<string, List<HlCommandInfo>>();

        /// <summary>
        /// The optimized macro block
        /// </summary>
        private CommandBlock _optimizedBlock;

        /// <summary>
        /// A list of commands marked for removal
        /// </summary>
        private readonly List<CommandAssign> _markedCommandsList = new List<CommandAssign>();

        /// <summary>
        /// A list of local variables marked for removal
        /// </summary>
        private readonly List<SymbolLocalVariable> _markedLocalVariablesList = new List<SymbolLocalVariable>();

        /// <summary>
        /// A counter for creating additional temporary local variables during optimization
        /// </summary>
        private int _augmentedVariablesIndex;

        /// <summary>
        /// A flag for updating the chain information for the l-values of the optimizer
        /// </summary>
        private bool _updateChainFlag;


        /// <summary>
        /// True if this statements list is in Static Single Assignment Form
        /// </summary>
        public bool IsSsaForm => _lValuesTable.IsSsaForm;


        private HlOptimizer(GMacMacro baseMacro)
        {
            BaseMacro = baseMacro;
        }


        /// <summary>
        /// Update all chaining information for the high-level optimizer
        /// </summary>
        private void UpdateChains()
        {
            //No update is required; just leave
            if (_updateChainFlag == false)
                return;

            //Clear the list of commands
            _commandsInfoList.Clear();

            var commandInfoId = 1;

            //Duplicate assignment statements from compiled body of base macro into optimizer statements table
            var commandInfoList =
                _optimizedBlock
                .CommandsNoDeclare
                .OfType<CommandAssign>()
                .Select(command => new HlCommandInfo(commandInfoId++, command));

            _commandsInfoList.AddRange(commandInfoList);

            //Update information in the l-values table
            UpdateChains_LValuesTable();

            _updateChainFlag = false;
        }

        /// <summary>
        /// Upate use information and l-values table from the given command information
        /// </summary>
        /// <param name="commandInfo"></param>
        private void UpdateChains_CommandInfo(HlCommandInfo commandInfo)
        {
            //Get all l-values in RHS expression of statement
            var rhsLvaluesList = _valueAccessProcessor.GetLValues(commandInfo.AssociatedCommand.RhsExpression);

            //Set dependency information (definition information) for all RHS l-values
            foreach (var rhsLvalue in rhsLvaluesList)
            {
                //Get the last definition command for this RHS l-value from the l-values table
                var rhsLastLvalueDefInfo = _lValuesTable.GetLastDefinitionInfo(rhsLvalue);
                var rhsLastCommandInfo = rhsLastLvalueDefInfo.DefiningCommand;

                //Add use information in the last definition command of this RHS l-value
                if (!ReferenceEquals(rhsLastCommandInfo, null))
                    rhsLastCommandInfo.LhslValueUses.Add(commandInfo);

                //Set dependency information (definition information) for this RHS l-value
                commandInfo.RhsVariablesInfo.Add(rhsLastLvalueDefInfo);
            }

            //Never add a definition for a macro output parameter because it will never be used in any RHS expression
            if (commandInfo.LhslValueIsOutputParameter == false)
            {
                //Update dependency information (definition information) of LHS l-value in the l-values table
                _lValuesTable.AddDefinition(commandInfo);
            }
        }

        /// <summary>
        /// Update information in the table of l-values based on the list of commands information table
        /// </summary>
        private void UpdateChains_LValuesTable()
        {
            //Initialize symbol table for variables definition statements information
            _lValuesTable.Clear();

            //Add input parameters to l-values table
            foreach (var param in BaseMacro.Parameters.Where(param => param.DirectionIn))
                _lValuesTable.AddDefinition(param);

            //Compute all dependency information (definition and use information for l-values) for all commands
            foreach (var commandInfo in _commandsInfoList)
                UpdateChains_CommandInfo(commandInfo);
        }


        /// <summary>
        /// Perform some debugging tasks for checking public state of optimizer
        /// </summary>
        private void CheckIntegrity()
        {
            var flag =
                _optimizedBlock
                .CommandsNoDeclare
                .OfType<CommandAssign>()
                .SelectMany(assign => _valueAccessProcessor.GetLValues(assign.RhsExpression))
                .Any(
                    lvalue =>
                        lvalue is SymbolLocalVariable &&
                        _optimizedBlock.ContainsLocalVariable(lvalue.ObjectName) == false
                    );

            if (flag)
                throw new InvalidOperationException();
        }

        /// <summary>
        /// Mark a command information entry (and its LHS local variable if present) for removal
        /// </summary>
        /// <param name="commandInfo"></param>
        private void MarkCommandInfoForRemoval(HlCommandInfo commandInfo)
        {
            _updateChainFlag = true;

            _markedCommandsList.Add(commandInfo.AssociatedCommand);

            if (commandInfo.LhslValueIsLocalVariable)
                _markedLocalVariablesList.Add((SymbolLocalVariable)commandInfo.LhslValue);
        }

        /// <summary>
        /// Remove marked commands information and marked local variables from optimizer tables
        /// </summary>
        private void RemoveMarkedObjects()
        {
            if (_markedCommandsList.Count <= 0 && _markedLocalVariablesList.Count <= 0) 
                return;

            _updateChainFlag = true;

            //Remove all marked commands from optimized block
            //_markedCommandsList.ForEach(command => _optimizedBlock.RemoveCommand(command));
            _markedCommandsList.ForEach(_optimizedBlock.RemoveCommand);

            //Remove all marked local variables from optimized block
            _markedLocalVariablesList.ForEach(variable => _optimizedBlock.UndefineLocalVariable(variable));

            //Clear mark lists
            _markedCommandsList.Clear();

            _markedLocalVariablesList.Clear();
        }

        /// <summary>
        /// Define a new temporary local variable in the optimized block
        /// </summary>
        /// <param name="varType"></param>
        /// <returns></returns>
        private SymbolLocalVariable DefineNewLocalVariable(ILanguageType varType)
        {
            _updateChainFlag = true;

            var varName = "AV" + (_augmentedVariablesIndex++).ToString("X4");

            var declareCommand = _optimizedBlock.DefineLocalVariable(varName, varType);

            return (SymbolLocalVariable)declareCommand.DataStore;
        }

        /// <summary>
        /// Create a composite type initialized constructor RHS expression based on the given command with
        /// partial LHS value access and a new temporary local variable as explained in Optimize_ReplaceAllLHSPartialAccess
        /// </summary>
        /// <param name="commandInfo"></param>
        /// <param name="newLvalue"></param>
        /// <returns></returns>
        private BasicPolyadic CreateConstructorExpression(HlCommandInfo commandInfo, SymbolLValue newLvalue)
        {
            var oldValueAccess = commandInfo.AssociatedCommand.LhsValueAccess;

            var oldLvalue = oldValueAccess.RootSymbolAsLValue;

            var firstDefStId = _lValuesTable.GetFirstDefiningCommandInfo(oldLvalue).CommandInfoId;

            var useDefaultSource =
                firstDefStId < 0 || firstDefStId != commandInfo.CommandInfoId;

            var typeStructure = oldValueAccess.RootSymbolAsLValue.SymbolType as GMacStructure;

            if (typeStructure != null)
            {
                var structure = typeStructure;

                var operands = OperandsByValueAccess.Create();

                var dataMemberName = ((ValueAccessStepByKey<string>)oldValueAccess.LastAccessStep).AccessKey;

                var dataMember = structure.GetDataMember(dataMemberName);

                var operandLhsValueAccess = LanguageValueAccess.Create(dataMember);

                var operandRhsExpr = LanguageValueAccess.Create(newLvalue);

                operandLhsValueAccess.Append(oldValueAccess.AccessSteps.Skip(2));

                operands.AddOperand(operandLhsValueAccess, operandRhsExpr);

                if (useDefaultSource)
                    return structure.CreateConstructorExpression(
                        LanguageValueAccess.Create(oldLvalue), 
                        operands
                        );

                return structure.CreateConstructorExpression(operands);
            }

            if (!(oldValueAccess.RootSymbolAsLValue.SymbolType is GMacFrameMultivector))
                throw new InvalidOperationException("Unknown composite type to be constructed");

            var mvType = (GMacFrameMultivector)oldValueAccess.RootSymbolAsLValue.SymbolType;

            var operandsByIndex = OperandsByIndex.Create();

            var stepByKey = oldValueAccess.LastAccessStep as ValueAccessStepByKey<int>;

            if (stepByKey != null)
            {
                var id = stepByKey.AccessKey;

                operandsByIndex.AddOperand(id, LanguageValueAccess.Create(newLvalue));
            }
            else
            {
                var stepByKeyList = oldValueAccess.LastAccessStep as ValueAccessStepByKeyList<int>;

                if (stepByKeyList == null)
                    throw new InvalidOperationException("Invalid access step for a multivector");

                var idsList = stepByKeyList.AccessKeyList;

                foreach (var id in idsList)
                    operandsByIndex.AddOperand(id,
                        LanguageValueAccess.Create(newLvalue).Append(id, ((GMacAst) BaseMacro.RootAst).ScalarType));
            }

            if (useDefaultSource)
                return mvType.CreateConstructorExpression(
                    LanguageValueAccess.Create(oldLvalue), 
                    operandsByIndex
                    );

            return mvType.CreateConstructorExpression(operandsByIndex);
        }

        /// <summary>
        /// Replace all LHS partial access processes with full access processes by updating the RHS to be
        /// composite type initialized constructions on full variables. For example the command
        ///    let x.point.#E1# = y gp z
        /// is replaced by the two commands:
        ///    let temp = y gp z
        ///    let x = Multivector{x}(#E1# = temp)
        /// </summary>
        private void Optimize_ReplaceAllLHSPartialAccess()
        {
            _updateChainFlag = false;

            foreach (var commandInfo in _commandsInfoList.Where(commandInfo => commandInfo.IsPartialLhsDefinition))
            {
                //Assume command is 'let x.point.#E1# = y gp z'
                _updateChainFlag = true;

                //old_root_lvalue is 'x'
                var oldRootLvalue = commandInfo.AssociatedCommand.LhsValueAccess.RootSymbolAsLValue;

                //new_lvalue is 'temp'
                var newLvalue = DefineNewLocalVariable(commandInfo.AssociatedCommand.RhsExpression.ExpressionType);

                //Add command 'let temp = y gp z'
                _optimizedBlock.AddCommandBeforeCommand_Assign(
                    commandInfo.AssociatedCommand,
                    LanguageValueAccess.Create(newLvalue),
                    commandInfo.AssociatedCommand.RhsExpression
                    );

                //Update command 'let x.point.#E1# = y gp z' to become let x = Multivector{x}(#E1# = temp)
                commandInfo.AssociatedCommand.SetCommandSides(
                    LanguageValueAccess.Create(oldRootLvalue),
                    CreateConstructorExpression(commandInfo, newLvalue)
                    );
            }

            UpdateChains();
        }


        /// <summary>
        /// Convert code to Single Static Assignment form where each l-value is only used once as an LHS in assignments
        /// </summary>
        private void Optimize_ConvertToSSAForm()
        {
            if (IsSsaForm)
                return;

            //Find definitions of all l-values not in SSA form
            foreach (var lhsLvalueInfo in _lValuesTable.GetNonSsaFormLValuesDefinitionsInfo())
            {
                _updateChainFlag = true;

                var oldLvalue = lhsLvalueInfo.LValue;

                var newLvalue = 
                    _optimizedBlock
                    .DefineLocalVariable(lhsLvalueInfo.CurrentSsaFormName, oldLvalue.SymbolType)
                    .DataStore;

                var commandInfo = lhsLvalueInfo.DefiningCommand;

                //Create a new local variable with a suitable name and change the LHS of this command into the new variable
                var newLhsValueAccess = 
                    commandInfo.AssociatedCommand.LhsValueAccess.Duplicate().ReplaceRootSymbol(newLvalue);

                commandInfo.AssociatedCommand.SetLhsValueAccess(newLhsValueAccess);

                //Update all uses of the LHS in following commands to use the new variable
                foreach (var useCommandInfo in commandInfo.LhslValueUses)
                {
                    var oldExpr = useCommandInfo.AssociatedCommand.RhsExpression;
                    var newExpr = commandInfo.AssociatedCommand.LhsValueAccess;

                    var newRhsExpr =
                        _valueAccessProcessor.ReplaceLValueByExpression(oldExpr, oldLvalue, newExpr);

                    useCommandInfo.AssociatedCommand.SetRhsExpression(newRhsExpr);
                }
            }

            UpdateChains();

            if (IsSsaForm == false)
                throw new InvalidOperationException("This should never happen!");
        }

        /// <summary>
        /// Add the definition commands for the RHS l-values of the given command to the list of active commands
        /// and add the given command itself to the list if indicated by the given flag
        /// </summary>
        /// <param name="activeCommandsList">The list of active commands</param>
        /// <param name="commandInfo">The command information object</param>
        /// <param name="addCommandToList">If true, add the command information object to the list</param>
        private static void UpdateActiveCommandList(List<HlCommandInfo> activeCommandsList, HlCommandInfo commandInfo, bool addCommandToList)
        {
            if (addCommandToList)
                activeCommandsList.Add(commandInfo);

            var commandsInfoList =
                commandInfo
                .RhsVariablesInfo
                .Select(rhsLvalueInfo => rhsLvalueInfo.DefiningCommand)
                .Where(
                    rhsLvalueDefCommandInfo => 
                        !ReferenceEquals(rhsLvalueDefCommandInfo, null) &&
                        !rhsLvalueDefCommandInfo.IsActive(activeCommandsList)
                );

            activeCommandsList.AddRange(commandsInfoList);
        }

        /// <summary>
        /// Remove all dead commands from optimized code
        /// </summary>
        private void Optimize_RemoveDeadCommands()
        {
            var activeCommandsList = new List<HlCommandInfo>();

            //Iterate through commands information list in reverse order
            for (var i = _commandsInfoList.Count - 1; i >= 0; i--)
            {
                var commandInfo = _commandsInfoList[i];

                //If the LHS of this command is an output parameter or is marked as an active command add the
                //definition commands of its RHS l-values to the active commands list
                if (commandInfo.LhslValueIsOutputParameter)
                    UpdateActiveCommandList(activeCommandsList, commandInfo, true);

                else if (commandInfo.IsActive(activeCommandsList))
                    UpdateActiveCommandList(activeCommandsList, commandInfo, false);
            }

            //Remove all commands that are not in the list of active commands
            var commandsInfoList =
                _commandsInfoList.Where(commandInfo => commandInfo.IsActive(activeCommandsList) == false);

            foreach (var commandInfo in commandsInfoList)
                MarkCommandInfoForRemoval(commandInfo);

            RemoveMarkedObjects();

            //CheckIntegrity();

            UpdateChains();
        }

        /// <summary>
        /// Remove the given command after propagating its RHS expression to any other command using its LHS
        /// </summary>
        /// <param name="commandInfo"></param>
        private void PropagateCommand(HlCommandInfo commandInfo)
        {
            MarkCommandInfoForRemoval(commandInfo);

            foreach (var useCommandInfo in commandInfo.LhslValueUses)
            {
                var oldExpr = useCommandInfo.AssociatedCommand.RhsExpression;
                var oldLvalue = commandInfo.LhslValue;
                var newExpr = (ILanguageExpressionAtomic)commandInfo.AssociatedCommand.RhsExpression;

                var newRhsExpr =
                    _valueAccessProcessor.ReplaceLValueByExpression(oldExpr, oldLvalue, newExpr);

                useCommandInfo.AssociatedCommand.SetRhsExpression(newRhsExpr);

                //CheckIntegrity();
            }
        }

        /// <summary>
        /// Propagate commands having atomic RHS expressions
        /// </summary>
        private void Optimize_PropagateRHSAtomicExpressions()
        {
            _updateChainFlag = false;

            foreach (var commandInfo in _commandsInfoList)
            {
                //If the LHS of this command is an output parameter and the RHS is a full access of a local variable
                //replace the RHS by the value of the RHS of the local variable in its last defining command and
                //remove its defining command if not needed elsewhere
                if (commandInfo.LhslValueIsOutputParameter && commandInfo.IsRhsExpressionFullAccessLocalVariable)
                {
                    var definingCommandInfo = commandInfo.RhsVariablesInfo[0].DefiningCommand;

                    if (definingCommandInfo.LhslValueUses.Count == 1)
                    {
                        MarkCommandInfoForRemoval(definingCommandInfo);

                        commandInfo.AssociatedCommand.SetRhsExpression(
                            definingCommandInfo.AssociatedCommand.RhsExpression
                            );
                    }
                }

                //If the LHS of this command is not an output parameter and its RHS is an atomic expression and its
                //LHS is a full access and its LHS is used in later commands, propagate the RHS of this command
                if (
                    !commandInfo.LhslValueIsOutputParameter && 
                    commandInfo.IsRhsExpressionAtomic && 
                    commandInfo.IsFullLhsDefinition && 
                    commandInfo.LhslValueIsUsedLater
                    )
                    PropagateCommand(commandInfo);
            }

            RemoveMarkedObjects();
                
            UpdateChains();
        }

        /// <summary>
        /// Fill table of RHS expressions use information
        /// </summary>
        private void FillRhsExpressionsTable()
        {
            _rhsExpressionsTable.Clear();

            foreach (var commandInfo in _commandsInfoList)
                if (commandInfo.IsRhsExpressionBasic)
                {
                    var exprText = commandInfo.AssociatedCommand.RhsExpression.ToString();

                    List<HlCommandInfo> exprInfo;

                    if (_rhsExpressionsTable.TryGetValue(exprText, out exprInfo) == false)
                    {
                        exprInfo = new List<HlCommandInfo>();

                        _rhsExpressionsTable.Add(exprText, exprInfo);
                    }

                    exprInfo.Add(commandInfo);
                }
        }

        /// <summary>
        /// Propagate a redundant RHS expression
        /// </summary>
        /// <param name="exprUseInfo"></param>
        private void PropagateRhsExpression(List<HlCommandInfo> exprUseInfo)
        {
            _updateChainFlag = true;

            var firstUsingCommand = exprUseInfo[0];

            var newLhsValueAccess =
                LanguageValueAccess.Create(
                    DefineNewLocalVariable(
                        firstUsingCommand.AssociatedCommand.RhsExpression.ExpressionType
                    )
                );

            //Create a new command to assign the RHS expreesion to a new local LHS variable
            _optimizedBlock.AddCommandBeforeCommand_Assign(
                firstUsingCommand.AssociatedCommand,
                newLhsValueAccess,
                firstUsingCommand.AssociatedCommand.RhsExpression
                );

            //Replace the RHS expression of each statement using this expression by the LHS value access
            //of the new statement
            foreach (var useSt in exprUseInfo)
                useSt.AssociatedCommand.SetRhsExpression(newLhsValueAccess);
        }

        /// <summary>
        /// Remove redundant RHS expresions
        /// </summary>
        private void Optimize_PropagateCommonRHSBasicExpressions()
        {
            FillRhsExpressionsTable();

            _updateChainFlag = false;

            foreach (var pair in _rhsExpressionsTable.Where(pair => pair.Value.Count > 1))
                PropagateRhsExpression(pair.Value);

            UpdateChains();
        }

        /// <summary>
        /// Evaluate all simple RHS expressions (the RHS is a constant value or a basic expressions on constant values)
        /// then propagate the constant values in the forward direction of commands if possible
        /// </summary>
        private void Optimize_FoldSimpleExpressions()
        {
            foreach (var commandInfo in _commandsInfoList)
            {
                //Try to evaluate the RHS of each command
                var rhsValue = 
                    GMacExpressionEvaluator.EvaluateExpressionIfSimple(
                        BaseMacro.ChildScope, 
                        commandInfo.AssociatedCommand.RhsExpression
                        );

                if (ReferenceEquals(rhsValue, null))
                    continue;

                //If evaluation is possible change the RHS of the command into the evaluated constant value
                commandInfo.AssociatedCommand.SetRhsExpression(rhsValue);

                //If the LHS of this command is a full definition of an input macro parameter or local variable
                //used in following commands propagate the RHS constant value and remove this command from optimized code
                if (!commandInfo.LhslValueIsOutputParameter && commandInfo.IsFullLhsDefinition && commandInfo.LhslValueIsUsedLater)
                    PropagateCommand(commandInfo);
            }

            RemoveMarkedObjects();

            UpdateChains();
        }

        /// <summary>
        /// Fill final optimized command block with optimized code
        /// </summary>
        private void FillOptimizedCommandBlock()
        {
            //Remove all statements from optimized block
            _optimizedBlock.ClearCommands();

            //Add optimized statements from stetements table to optimized block
            foreach (var commandInfo in _commandsInfoList)
            {
                if (commandInfo.AssociatedCommand.LhsValueAccess.IsLocalVariable)
                    _optimizedBlock.AddCommand_Declare(commandInfo.AssociatedCommand.LhsValueAccess.RootSymbolAsDataStore);

                _optimizedBlock.AddCommand_Assign(
                    commandInfo.AssociatedCommand.LhsValueAccess, 
                    commandInfo.AssociatedCommand.RhsExpression
                    );
            }
        }


        private void Optimize()
        {
            _augmentedVariablesIndex = 1;

            //Make a copy of the compiled body of the macro for manipulation during optimization
            _optimizedBlock = HlMacroBodyCompiler.Compile(BaseMacro.CompiledBody);

            //Update use chains information
            _updateChainFlag = true;

            UpdateChains();

            //Replace all partial value access in the LHS of assignment commands with equivalent code on full l-values
            Optimize_ReplaceAllLHSPartialAccess();

            //Convert code to SSA form (Static Single-Assignment form)
            Optimize_ConvertToSSAForm();

            //Remove dead code
            Optimize_RemoveDeadCommands();

            //Perform several passes of value propagation process
            _updateChainFlag = true;

            while (_updateChainFlag)
            {
                _updateChainFlag = false;

                //Evaluate simple expressions (i.e. basic expressions on constant values) and propagate the values
                Optimize_FoldSimpleExpressions();

                //Remove redundant basic expressions
                Optimize_PropagateCommonRHSBasicExpressions();

                //Propagate assignmets of atomic expressions to l-values
                Optimize_PropagateRHSAtomicExpressions();
            }

            //Fill the final optimized macro command block body
            FillOptimizedCommandBlock();
        }


        public override string ToString()
        {
            var s = new StringBuilder();

            s.AppendLine("Begin GMac High Level Optimizer State");

            s.AppendLine("   " + (IsSsaForm ? "In SSA form" : "Not in SSA form"));

            foreach (var statementInfo in _commandsInfoList)
                s.AppendLine(statementInfo.ToString());

            s.AppendLine("End GMac High Level Optimizer State");

            return s.ToString();
        }
    }
}
