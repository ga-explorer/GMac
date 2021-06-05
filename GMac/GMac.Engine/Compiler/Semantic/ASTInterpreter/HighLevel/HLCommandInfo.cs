using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeComposerLib.Irony.Semantic.Command;
using CodeComposerLib.Irony.Semantic.Expression;
using CodeComposerLib.Irony.Semantic.Expression.ValueAccess;
using CodeComposerLib.Irony.Semantic.Symbol;

namespace GMac.Engine.Compiler.Semantic.ASTInterpreter.HighLevel
{
    /// <summary>
    /// Information relevant to an assignment command during high-level optimization process
    /// </summary>
    internal sealed class HlCommandInfo
    {
        /// <summary>
        /// Command ID
        /// </summary>
        public int CommandInfoId { get; }

        /// <summary>
        /// The associated assignment command
        /// </summary>
        public CommandAssign AssociatedCommand { get; }

        /// <summary>
        /// A list of all l-values definitions information in the rhs of this assignment command (may contain repeated entries)
        /// </summary>
        public List<HlLValueDefinitionInfo> RhsVariablesInfo { get; } = new List<HlLValueDefinitionInfo>();

        /// <summary>
        /// A list of all commands following this assignment command and depending on the l-value of its lhs.
        /// </summary>
        public List<HlCommandInfo> LhslValueUses { get; } = new List<HlCommandInfo>();

        /// <summary>
        /// The l-value in the LHS of this command
        /// </summary>
        public SymbolLValue LhslValue => (SymbolLValue)AssociatedCommand.LhsValueAccess.RootSymbol;

        /// <summary>
        /// The name of the l-value in the LHS of this command
        /// </summary>
        public string LhslValueName => AssociatedCommand.LhsValueAccess.RootSymbol.ObjectName;

        /// <summary>
        /// True if there is a following command using the LHS of this command in its RHS
        /// </summary>
        public bool LhslValueIsUsedLater => (LhslValueUses.Count > 0);

        /// <summary>
        /// True if the LHS of this command is a local variable
        /// </summary>
        public bool LhslValueIsLocalVariable => (LhslValue is SymbolLocalVariable);

        /// <summary>
        /// True if the LHS of this command is a macro parameter
        /// </summary>
        public bool LhslValueIsParameter => (LhslValue is SymbolProcedureParameter);

        /// <summary>
        /// True if the LHS of this command is a macro output parameter
        /// </summary>
        public bool LhslValueIsOutputParameter => (LhslValue is SymbolProcedureParameter && ((SymbolProcedureParameter)LhslValue).DirectionOut);


        /// <summary>
        /// True if this command uses a full LHS value access 
        /// (i.e. the l-value is fully changed not partially changed)
        /// </summary>
        public bool IsFullLhsDefinition => AssociatedCommand.LhsValueAccess.IsFullAccess;

        /// <summary>
        /// True if this command uses a partial LHS value access 
        /// (i.e. the l-value is partially changed not fully changed)
        /// </summary>
        public bool IsPartialLhsDefinition => AssociatedCommand.LhsValueAccess.IsPartialAccess;

        /// <summary>
        /// True if the RHS of this statement is an atomic expression
        /// </summary>
        public bool IsRhsExpressionAtomic => AssociatedCommand.RhsExpression is ILanguageExpressionAtomic;

        /// <summary>
        /// True if the RHS of this statement is a basic expression
        /// </summary>
        public bool IsRhsExpressionBasic => AssociatedCommand.RhsExpression is ILanguageExpressionBasic;

        /// <summary>
        /// True if the RHS of this command is a full access of a local variable
        /// </summary>
        public bool IsRhsExpressionFullAccessLocalVariable 
        { 
            get 
            { 
                if (!(AssociatedCommand.RhsExpression is LanguageValueAccess)) 
                    return false;

                return ((LanguageValueAccess)AssociatedCommand.RhsExpression).IsFullAccessLocalVariable;
            } 
        }

        public HlCommandInfo(int statementId, CommandAssign command)
        {
            CommandInfoId = statementId;
            AssociatedCommand = command;
        }


        /// <summary>
        /// Retrieve the definition information associated with an l-value in the RHS of this command
        /// </summary>
        /// <param name="lvalue"></param>
        /// <returns></returns>
        public HlLValueDefinitionInfo GetRhslValueInfo(SymbolLValue lvalue)
        {
            return RhsVariablesInfo.FirstOrDefault(lvalueInfo => lvalueInfo.LValue.ObjectId == lvalue.ObjectId);
        }

        /// <summary>
        /// Searches a list of commands information for the presence of this command
        /// </summary>
        /// <param name="activeCommandInfoList"></param>
        /// <returns></returns>
        public bool IsActive(List<HlCommandInfo> activeCommandInfoList)
        {
            return
                activeCommandInfoList
                .Exists(
                    activeCommandInfo =>
                        activeCommandInfo.AssociatedCommand.ObjectId == AssociatedCommand.ObjectId
                    );
        }

        
        public override string ToString()
        {
            var s = new StringBuilder();

            s.Append(CommandInfoId.ToString().PadLeft(3));
            s.Append(": ");
            s.AppendLine(AssociatedCommand.ToString());

            s.Append("   LHS ");
            s.Append(LhslValueName);

            if (LhslValueUses.Count > 0)
            {
                s.Append(" Used later at {");

                foreach (var useSt in LhslValueUses)
                    s.Append(useSt.CommandInfoId.ToString()).Append(", ");

                s.Length -= 2;

                s.AppendLine("}");
            }
            else
            {
                s.AppendLine(" not used later");
            }

            if (RhsVariablesInfo.Count > 0)
                foreach (var varInfo in RhsVariablesInfo)
                    s.AppendLine("   " + varInfo);
            else
                s.AppendLine("   No RHS variables");

            return s.ToString();
        }
    }
}
