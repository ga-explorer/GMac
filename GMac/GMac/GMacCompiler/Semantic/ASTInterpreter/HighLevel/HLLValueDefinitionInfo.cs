using System;
using System.Text;
using CodeComposerLib.Irony.Semantic.Symbol;

namespace GMac.GMacCompiler.Semantic.ASTInterpreter.HighLevel
{
    /// <summary>
    /// This class is used during high-level optimization to hold various definions information
    /// of l-values (i.e. assignments to macro parameters and local variables)
    /// </summary>
    internal sealed class HlLValueDefinitionInfo
    {
        /// <summary>
        /// A counter to keep track of number of definitions for the same l-value
        /// </summary>
        public int DefinitionIndex { get; }

        /// <summary>
        /// The actual l-value (variable)
        /// </summary>
        public SymbolLValue LValue { get; }

        /// <summary>
        /// The statement where the l-value is defined (assigned a value)
        /// </summary>
        public HlCommandInfo DefiningCommand { get; }

        /// <summary>
        /// True if this l-value has a defining command (only input macro peremeters may have no defining commands)
        /// </summary>
        public bool HasDefiningCommand => DefiningCommand != null;

        /// <summary>
        /// True if this l-value is a macro parameter
        /// </summary>
        public bool IsMacroParameter => LValue is SymbolProcedureParameter;

        /// <summary>
        /// True if this l-value is a local variable
        /// </summary>
        public bool IsLocalVariable => LValue is SymbolLocalVariable;

        /// <summary>
        /// True if this l-value definition is not the first one for this l-value
        /// </summary>
        public bool HasPreviousSsaFormName => !string.IsNullOrEmpty(PreviousSsaFormName);

        /// <summary>
        /// The name associated with the previous definition for this l-value
        /// </summary>
        public string PreviousSsaFormName { get; }

        /// <summary>
        /// The name associated with this definition for this l-value
        /// </summary>
        public string CurrentSsaFormName { get; private set; }


        public HlLValueDefinitionInfo(int defIndex, SymbolLValue lvalue, HlCommandInfo commandInfo)
        {
            //Output macro parameters are not accepted here
            var parameter = lvalue as SymbolProcedureParameter;

            if (parameter != null && parameter.DirectionOut)
                throw new InvalidOperationException("Cannot generate definition for macro output parameter");

            //A local variable l-value must have a defining assignment command
            if (lvalue is SymbolLocalVariable && ReferenceEquals(commandInfo, null))
                throw new InvalidOperationException("A local variable must have a defining assignment command");

            DefinitionIndex = defIndex;
            LValue = lvalue;
            DefiningCommand = commandInfo;

            switch (DefinitionIndex)
            {
                case 0:
                    //This is the first use of this l-value as the LHS of an assignment command
                    CurrentSsaFormName = LValue.ObjectName;
                    PreviousSsaFormName = "";
                    break;

                case 1:
                    //This is the second use of this l-value as the LHS of an assignment command
                    CurrentSsaFormName = LValue.ObjectName + "SSA" + DefinitionIndex;
                    PreviousSsaFormName = LValue.ObjectName;
                    break;

                default:
                    CurrentSsaFormName = LValue.ObjectName + "SSA" + DefinitionIndex;
                    PreviousSsaFormName = LValue.ObjectName + "SSA" + (DefinitionIndex - 1);
                    break;
            }
        }


        public override string ToString()
        {
            var s = new StringBuilder();

            if (LValue is SymbolLocalVariable)
                s.Append("Local variable <");
            else
                s.Append("Macro parameter <");

            s.Append(LValue.ObjectName);

            s.Append(">");

            if (!ReferenceEquals(DefiningCommand, null))
                s.Append(" defined at statement " + DefiningCommand.CommandInfoId);
            else
                s.Append(" with no defining statement");

            return s.ToString();
        }
    }
}
