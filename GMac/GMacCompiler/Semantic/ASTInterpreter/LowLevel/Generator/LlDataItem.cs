using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeometricAlgebraSymbolicsLib;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;
using GMac.GMacCompiler.Semantic.AST.Extensions;
using IronyGrammars.Semantic.Expression.Value;
using IronyGrammars.Semantic.Expression.ValueAccess;
using IronyGrammars.Semantic.Symbol;
using IronyGrammars.Semantic.Type;
using TextComposerLib.Helpers;

namespace GMac.GMacCompiler.Semantic.ASTInterpreter.LowLevel.Generator
{
    /// <summary>
    /// This class holds the relevant information of low-level items during low-level code generation and optimization
    /// A low-level item is a primitive item that may be part of a macro input or output parameter, or a local variable.
    /// Each item may have a RHS value that may depend on previous items. Each item may have a
    /// symbolic scalar used for substitution in the RHS values of following items. 
    /// 
    /// For example the item defined as:
    /// LLDI0010 = 'Times[2, Pi, LLDI0009, LLDI0008]' this item: 
    /// 1- Has an assigned RHS value of 'Times[2, Pi, LLDI0009, LLDI0008]'
    /// 2- Is a variable item because it depends for its final value on items LLDI0009 and LLDI0008
    /// 3- Has an RHS symbolic scalar 'LLDI0010' to be used in all following items that depend on it
    /// 
    /// While the item defined as:
    /// LLDI0011 = 'Times[2, Pi]'
    /// 1- Has an assigned RHS value of 'Times[2, Pi]'
    /// 2- Is a constant item because it depends for its final value on no previously computed items
    /// 3- Has an RHS symbolic scalar 'Times[2, Pi]' to be used in all following items that depend on it
    /// </summary>
    internal sealed class LlDataItem
    {
        private static readonly IntegerSequenceGenerator IdCounter = new IntegerSequenceGenerator();

        private static int CreateNewId()
        {
            return IdCounter.GetNewCountId();
        }


        /// <summary>
        /// True if this item is marked for delete
        /// </summary>
        public bool IsDeleted { get; private set; }

        /// <summary>
        /// Evaluation order associated with this item
        /// </summary>
        public int EvaluationOrder { get; private set; }

        /// <summary>
        /// A unique ID for this item
        /// </summary>
        public int ItemId { get; private set; }

        /// <summary>
        /// Low-level item name (automatically generated)
        /// </summary>
        public string ItemName { get; }

        /// <summary>
        /// High-level value access associated with this low-level item
        /// </summary>
        public LanguageValueAccess AssociatedValueAccess { get; }

        /// <summary>
        /// Primitive RHS value assigned to this low-level item. Temp items, constant input items and output items 
        /// always have a value assigned to them. Variable input items are kept as parameters and never have a 
        /// value assigned to them.
        /// </summary>
        public ValuePrimitive<MathematicaScalar> AssignedRhsValue { get; private set; }

        /// <summary>
        /// The symbolic scalar of this item when used inside the RHS of following items' assigned values. 
        /// All items must have such symbolic scalar except for output items because they are never used in the RHS
        /// of any other item (i.e. they are write-only items).
        /// </summary>
        public MathematicaScalar RhsUsableSymbolicScalar { get; private set; }

        /// <summary>
        /// True if this item is a constant
        /// </summary>
        public bool IsConstant { get; }

        /// <summary>
        /// A test value associated with this item for debugging
        /// </summary>
        //public Expr TestValueExpr { get; }

        /// <summary>
        /// True if this item is associated with an input macro parameter
        /// </summary>
        public bool IsInput 
        { 
            get 
            {
                if (!(AssociatedValueAccess.RootSymbol is SymbolProcedureParameter))
                    return false;

                return ((SymbolProcedureParameter)AssociatedValueAccess.RootSymbol).DirectionIn;
            } 
        }

        /// <summary>
        /// True if this item is associated with an output macro parameter
        /// </summary>
        public bool IsOutput
        { 
            get 
            {
                if (!(AssociatedValueAccess.RootSymbol is SymbolProcedureParameter))
                    return false;

                return ((SymbolProcedureParameter)AssociatedValueAccess.RootSymbol).DirectionOut;
            } 
        }

        /// <summary>
        /// True if this item is associated with a sub-expression or a local variable inside the macro code body
        /// </summary>
        public bool IsTemp => AssociatedValueAccess.RootSymbol is SymbolLocalVariable;

        /// <summary>
        /// True is this item is an output or temp item
        /// </summary>
        public bool IsNonInput => !IsInput;

        /// <summary>
        /// True if this item is a variable
        /// </summary>
        public bool IsVariable => !IsConstant;

        /// <summary>
        /// True if this item is a variable input
        /// </summary>
        public bool IsVariableInput => IsVariable && IsInput;

        /// <summary>
        /// True if this item is a constant input
        /// </summary>
        public bool IsConstantInput => IsConstant && IsInput;

        /// <summary>
        /// True if this item is a variable temp
        /// </summary>
        public bool IsVariableTemp => IsVariable && IsTemp;

        /// <summary>
        /// True if this item is a constant temp
        /// </summary>
        public bool IsConstantTemp => IsConstant && IsTemp;

        /// <summary>
        /// Return this item's type. For items with high-level associations this is the type of the value-access.
        /// For sub-expressions this is the type of the RHS expression.
        /// </summary>
        public TypePrimitive DataItemType => (TypePrimitive)AssociatedValueAccess.ExpressionType;

        /// <summary>
        /// The name of the high-level value-access associated with this item; if any
        /// </summary>
        public string HlItemName => AssociatedValueAccess.GetName();

        /// <summary>
        /// Symbolic scalar assigned to this low-level item (typically null for variable input items)
        /// </summary>
        public MathematicaScalar AssignedRhsSymbolicScalar => ReferenceEquals(AssignedRhsValue, null) ? null : AssignedRhsValue.Value;


        /// <summary>
        /// Create a macro parameter variable low-level item
        /// </summary>
        /// <param name="llName"></param>
        /// <param name="hlValueAccess"></param>
        /// <param name="testValueExpr"></param>
        private LlDataItem(string llName, LanguageValueAccess hlValueAccess)//, Expr testValueExpr)
        {
            var isOutput = hlValueAccess.IsOutputParameter;

            EvaluationOrder = -1;
            IsConstant = false;
            ItemId = CreateNewId();
            ItemName = llName;
            AssociatedValueAccess = hlValueAccess;
            //TestValueExpr = testValueExpr;

            //Input variable low-level items have no assigned values to them, while output items get assigned 
            //a default value
            AssignedRhsValue = 
                (!isOutput) ? null :
                ((TypePrimitive)AssociatedValueAccess.ExpressionType).GetDefaultScalarValue();

            //Output variable low-level items can never be used in the RHS of any other item so they have no usable 
            //symbolic scalar (i.e. for reading purposes)
            RhsUsableSymbolicScalar =
                isOutput ? null :
                MathematicaScalar.CreateSymbol(GaSymbolicsUtils.Cas, llName);
        }

        /// <summary>
        /// Create a low-level item with an assigned value (not a variable input item)
        /// </summary>
        /// <param name="llName"></param>
        /// <param name="hlValueAccess"></param>
        /// <param name="assignedValue"></param>
        private LlDataItem(string llName, LanguageValueAccess hlValueAccess, ValuePrimitive<MathematicaScalar> assignedValue)//, Expr testValueExpr)
        {
            var llVarNamesCount = assignedValue.Value.GetDistinctLowLevelVariablesNames().Count();
            var isLlVar = assignedValue.Value.IsLowLevelVariable();

            EvaluationOrder = -1;

            //A constant item is independent of any other low-level item
            IsConstant = (llVarNamesCount == 0);

            ItemId = CreateNewId();
            ItemName = llName;

            AssociatedValueAccess = hlValueAccess;

            AssignedRhsValue = assignedValue;
            //TestValueExpr = testValueExpr;

            //Apply a propagation step on the value of this item using its associated symbolic scalar 
            //(that is used in the RHS values of following items)
            //Only propagate constants
            switch (GMacCompilerOptions.LowLevelPropagationMethod)
            {
                case GMacCompilerOptions.LowLevelPropagation.PropagateConstant:
                    RhsUsableSymbolicScalar =
                        IsConstant 
                        ? assignedValue.Value 
                        : MathematicaScalar.CreateSymbol(GaSymbolicsUtils.Cas, llName);
                    break;

                case GMacCompilerOptions.LowLevelPropagation.PropagateSingleVariable:
                    RhsUsableSymbolicScalar =
                        (IsConstant || isLlVar) 
                        ? assignedValue.Value 
                        : MathematicaScalar.CreateSymbol(GaSymbolicsUtils.Cas, llName);
                    break;

                default:
                    RhsUsableSymbolicScalar =
                        (IsConstant || (llVarNamesCount == 1)) 
                        ? assignedValue.Value 
                        : MathematicaScalar.CreateSymbol(GaSymbolicsUtils.Cas, llName);
                    break;
            }
        }


        /// <summary>
        /// Mark this item as a deleted item
        /// </summary>
        public void MarkAsDeleted()
        {
            IsDeleted = true;
        }

        /// <summary>
        /// Create an exact copy of this item
        /// </summary>
        /// <returns></returns>
        public LlDataItem Duplicate()
        {
            if (ReferenceEquals(AssignedRhsValue, null))
                return new LlDataItem(ItemName, AssociatedValueAccess)//, TestValueExpr) 
                { 
                    EvaluationOrder = EvaluationOrder,
                    IsDeleted = IsDeleted
                };

            return new LlDataItem(
                ItemName, 
                AssociatedValueAccess, 
                (ValuePrimitive<MathematicaScalar>)AssignedRhsValue.DuplicateValue(true))//, TestValueExpr)
                { 
                    EvaluationOrder = EvaluationOrder,
                    IsDeleted = IsDeleted
                };
        }

        /// <summary>
        /// Change the assigned value for an output item only
        /// </summary>
        /// <param name="assignedValue"></param>
        public void SetAssignedRhsValue(ValuePrimitive<MathematicaScalar> assignedValue)
        {
            if (IsOutput)
                AssignedRhsValue = assignedValue;

            else
                throw new InvalidOperationException("Only output macro parameters can be assigned value after their creation");
        }

        /// <summary>
        /// Change the assigned value and evaluation order for an output item only
        /// </summary>
        /// <param name="assignedValue"></param>
        /// <param name="evaluationOrder"></param>
        public void SetAssignedRhsValue(ValuePrimitive<MathematicaScalar> assignedValue, int evaluationOrder)
        {
            if (IsOutput)
            {
                AssignedRhsValue = assignedValue;
                EvaluationOrder = evaluationOrder;
            }
            else
                throw new InvalidOperationException("Only output macro parameters can be assigned value after their creation");
        }

        /// <summary>
        /// Get a list of distinct names of low-level items used in the RHS assigned value of this item
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetDistinctRhsItemsNames()
        {
            return
                AssignedRhsSymbolicScalar
                .Expression
                .GetLowLevelVariablesNames()
                .Distinct();
        }

        /// <summary>
        /// Get a list of names of low-level items used in the RHS assigned value of this item. 
        /// Some names may repeat
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetRhsItemsNames()
        {
            return
                AssignedRhsSymbolicScalar
                .Expression
                .GetLowLevelVariablesNames();
        }


        public override string ToString()
        {
            var s = new StringBuilder();

            if (IsVariableInput)
                s.Append("Variable input");

            else if (IsConstantInput)
                s.Append("Constant input");

            else if (IsOutput)
                s.Append("Output");

            else if (IsVariableTemp)
                s.Append("Variable temp");

            else if (IsConstantTemp)
                s.Append("Constant temp");

            else
                s.Append("Unknown data item type ");

            s.Append(" data item ")
                .Append(ItemName)
                .Append("<")
                .Append(HlItemName)
                .Append(">")
                .Append(" : ")
                .Append(DataItemType.TypeSignature);

            //if (TestValueExpr != null)
            //    s.Append(" <Test Value = ")
            //        .Append(TestValueExpr)
            //        .Append(">");

            if (ReferenceEquals(AssignedRhsValue, null) == false)
                s.Append(" = ")
                    .AppendLine(AssignedRhsValue.ToString());

            return s.ToString();
        }


        /// <summary>
        /// Create a variable read-only low-level item associated with a macro input parameter
        /// </summary>
        /// <param name="llName"></param>
        /// <param name="hlValueAccess"></param>
        /// <returns></returns>
        public static LlDataItem CreateVariableInputParameter(string llName, LanguageValueAccess hlValueAccess)//, Expr testValueExpr = null)
        {
            if (!(hlValueAccess.RootSymbol is SymbolProcedureParameter))
                throw new InvalidOperationException("The given value access is not an input macro parameter");

            var hlParam = (SymbolProcedureParameter)hlValueAccess.RootSymbol;

            if (hlParam.DirectionIn)
                return new LlDataItem(llName, hlValueAccess);//, testValueExpr);

            throw new InvalidOperationException("The given value access is not an input macro parameter");
        }

        /// <summary>
        /// Create a constant read-only low-level item associated with a macro input parameter
        /// </summary>
        /// <param name="llName"></param>
        /// <param name="hlValueAccess"></param>
        /// <param name="assignedValue"></param>
        /// <returns></returns>
        public static LlDataItem CreateConstantInputParameter(string llName, LanguageValueAccess hlValueAccess, ValuePrimitive<MathematicaScalar> assignedValue)
        {
            if (!(hlValueAccess.RootSymbol is SymbolProcedureParameter))
                throw new InvalidOperationException("The given value access is not an input macro parameter");

            var hlParam = (SymbolProcedureParameter)hlValueAccess.RootSymbol;

            if (hlParam.DirectionIn)
                return new LlDataItem(llName, hlValueAccess, assignedValue);//, null);

            throw new InvalidOperationException("The given value access is not an input macro parameter");
        }

        /// <summary>
        /// Create a variable write-only low-level item associated with a macro output parameter
        /// </summary>
        /// <param name="llName"></param>
        /// <param name="hlValueAccess"></param>
        /// <returns></returns>
        public static LlDataItem CreateOutputParameter(string llName, LanguageValueAccess hlValueAccess)
        {
            if (!(hlValueAccess.RootSymbol is SymbolProcedureParameter))
                throw new InvalidOperationException("The given value access is not an output macro parameter");
            
            var hlParam = (SymbolProcedureParameter)hlValueAccess.RootSymbol;

            if (hlParam.DirectionOut)
                return new LlDataItem(llName, hlValueAccess);

            throw new InvalidOperationException("The given value access is not an output macro parameter");
        }

        /// <summary>
        /// Create a temp low-level item associated with a local variable in the code body of the macro
        /// </summary>
        /// <param name="llName"></param>
        /// <param name="hlValueAccess"></param>
        /// <param name="assignedValue"></param>
        /// <param name="evaluationOrder"></param>
        /// <returns></returns>
        public static LlDataItem CreateTemp(string llName, LanguageValueAccess hlValueAccess, ValuePrimitive<MathematicaScalar> assignedValue, int evaluationOrder)
        {
            if (hlValueAccess.RootSymbol is SymbolLocalVariable)
                return new LlDataItem(llName, hlValueAccess, assignedValue) { EvaluationOrder = evaluationOrder };

            throw new InvalidOperationException("The given value access is not a local variable");
        }
    }
}
