using GMac.GMacCompiler.Semantic.AST.Extensions;
using IronyGrammars.Semantic.Expression;

namespace GMac.GMacAST.Expressions
{
    public abstract class AstExpression : AstObject, IAstObjectWithExpression
    {
        #region Static members
        #endregion


        internal abstract ILanguageExpression AssociatedExpression { get; }


        public AstType GMacType => new AstType(AssociatedExpression.ExpressionType);

        public string GMacTypeSignature => AssociatedExpression.ExpressionType.TypeSignature;

        public override bool IsValid => AssociatedExpression != null;

        public override AstRoot Root => new AstRoot(AssociatedExpression.GMacRootAst());

        public override bool IsValidExpression => AssociatedExpression != null;

        public AstExpression Expression => this;

        /// <summary>
        /// True if this is a valid data store value access expression
        /// </summary>
        public virtual bool IsValidDatastoreValueAccess => false;

        /// <summary>
        /// True if this is a valid value expression
        /// </summary>
        public virtual bool IsValidValue => false;

        /// <summary>
        /// True if this is a valid composite expression
        /// </summary>
        public virtual bool IsValidCompositeExpression => false;

        /// <summary>
        /// True if this is a valid binary expression
        /// </summary>
        public virtual bool IsValidBinary => false;

        /// <summary>
        /// True if this is a valid unary expression
        /// </summary>
        public virtual bool IsValidUnary => false;

        /// <summary>
        /// True if this is a valid transform call expression
        /// </summary>
        public virtual bool IsValidTransformCall => false;

        /// <summary>
        /// True if this is a valid macro call expression
        /// </summary>
        public virtual bool IsValidMacroCall => false;

        /// <summary>
        /// True if this is a valid parametric symbolic expression
        /// </summary>
        public virtual bool IsValidParametricSymbolic => false;

        /// <summary>
        /// True if this is a valid multivector constructor expression
        /// </summary>
        public virtual bool IsValidMultivectorConstructor => false;

        /// <summary>
        /// True if this is a valid structure constructor expression
        /// </summary>
        public virtual bool IsValidStructureConstructor => false;

        /// <summary>
        /// True if this is a valid type cast expression
        /// </summary>
        public virtual bool IsValidTypeCast => false;

        //public bool IsFrameBasisVector
        //{
        //    get
        //    {
        //        var valueAccess = AssociatedExpression as LanguageValueAccess;

        //        return
        //            ReferenceEquals(valueAccess, null) == false &&
        //            valueAccess.IsFullAccess &&
        //            valueAccess.RootSymbol is GMacFrameBasisVector;
        //    }
        //}

        //public bool IsConstant
        //{
        //    get
        //    {
        //        var valueAccess = AssociatedExpression as LanguageValueAccess;

        //        return
        //            ReferenceEquals(valueAccess, null) == false &&
        //            valueAccess.IsFullAccess && 
        //            valueAccess.RootSymbol is GMacConstant;
        //    }
        //}

        //public bool IsMacroParameter
        //{
        //    get
        //    {
        //        var valueAccess = AssociatedExpression as LanguageValueAccess;

        //        return 
        //            (ReferenceEquals(valueAccess, null) == false && 
        //            valueAccess.IsFullAccessProcedureParameter);
        //    }
        //}

        //public bool IsLocalVariable
        //{
        //    get
        //    {
        //        var valueAccess = AssociatedExpression as LanguageValueAccess;

        //        return 
        //            (ReferenceEquals(valueAccess, null) == false && 
        //            valueAccess.IsFullAccessLocalVariable);
        //    }
        //}

        //public bool IsBooleanValue { get { return AssociatedExpression is ValuePrimitive<bool>; } }

        //public bool IsIntegerValue { get { return AssociatedExpression is ValuePrimitive<int>; } }

        //public bool IsScalarValue { get { return AssociatedExpression is ValuePrimitive<MathematicaScalar>; } }

        //public bool IsMultivectorValue { get { return AssociatedExpression is GMacValueMultivector; } }

        //public bool IsStructureValue { get { return AssociatedExpression is ValueStructureSparse; } }

        //public bool IsPrimitiveValue { get { return AssociatedExpression is ILanguageValuePrimitive; } }

        //public bool IsCompositeValue { get { return AssociatedExpression is ILanguageValueComposite; } }


        //public AstDatastoreValueAccess AsValueAccess
        //{
        //    get
        //    {
        //        var value = AssociatedExpression as LanguageValueAccess;

        //        return
        //            value == null ?
        //            new AstDatastoreValueAccess() :
        //            new AstDatastoreValueAccess(value);
        //    }
        //}

        //public AstFrameBasisVector AsFrameBasisVector
        //{
        //    get
        //    {
        //        if (IsFrameBasisVector == false)
        //            return new AstFrameBasisVector();

        //        var value = (LanguageValueAccess)AssociatedExpression;

        //        return new AstFrameBasisVector(value.RootSymbol as GMacFrameBasisVector);
        //    }
        //}

        //public AstConstant AsConstant
        //{
        //    get
        //    {
        //        if (IsConstant == false)
        //            return new AstConstant();

        //        var value = (LanguageValueAccess)AssociatedExpression;

        //        return new AstConstant(value.RootSymbol as GMacConstant);
        //    }
        //}

        //public AstMacroParameter AsMacroParameter
        //{
        //    get
        //    {
        //        if (IsMacroParameter == false)
        //            return new AstMacroParameter();

        //        var value = (LanguageValueAccess)AssociatedExpression;

        //        return new AstMacroParameter(value.RootSymbol as SymbolProcedureParameter);
        //    }
        //}

        //public AstLocalVariable AsLocalVariable
        //{
        //    get
        //    {
        //        if (IsLocalVariable == false)
        //            return new AstLocalVariable();

        //        var value = (LanguageValueAccess)AssociatedExpression;

        //        return new AstLocalVariable(value.RootSymbol as SymbolLocalVariable);
        //    }
        //}

        //public AstValueBoolean AsBooleanValue
        //{
        //    get
        //    {
        //        var valuePrimitive = AssociatedExpression as ValuePrimitive<bool>;

        //        return
        //            valuePrimitive == null
        //            ? new AstValueBoolean()
        //            : new AstValueBoolean(valuePrimitive);
        //    }
        //}

        //public AstValueInteger AsIntegerValue
        //{
        //    get
        //    {
        //        var valuePrimitive = AssociatedExpression as ValuePrimitive<int>;

        //        return
        //            valuePrimitive == null
        //            ? new AstValueInteger()
        //            : new AstValueInteger(valuePrimitive);
        //    }
        //}

        //public AstValueScalar AsScalarValue
        //{
        //    get
        //    {
        //        var valuePrimitive = AssociatedExpression as ValuePrimitive<MathematicaScalar>;

        //        return
        //            valuePrimitive == null
        //            ? new AstValueScalar()
        //            : new AstValueScalar(valuePrimitive);
        //    }
        //}

        //public AstValueMultivector AsMultivectorValue
        //{
        //    get
        //    {
        //        var valueMultivector = AssociatedExpression as GMacValueMultivector;

        //        return
        //            valueMultivector == null
        //            ? new AstValueMultivector()
        //            : new AstValueMultivector(valueMultivector);
        //    }
        //}

        //public AstValueStructure AsStructureValue
        //{
        //    get
        //    {
        //        var value = AssociatedExpression as ValueStructureSparse;

        //        return
        //            value == null ?
        //            new AstValueStructure() :
        //            new AstValueStructure(value);
        //    }
        //}


        public override string ToString()
        {
            return AssociatedExpression.ToString();
        }
    }
}
