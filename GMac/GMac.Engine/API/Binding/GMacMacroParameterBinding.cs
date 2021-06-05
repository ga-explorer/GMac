using System;
using System.Text;
using CodeComposerLib.Irony.Semantic.Expression.Value;
using GeometricAlgebraSymbolicsLib;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;
using GMac.Engine.AST;
using GMac.Engine.AST.Expressions;
using GMac.Engine.AST.Symbols;
using GMac.Engine.Compiler.Semantic.AST.Extensions;
using Wolfram.NETLink;

namespace GMac.Engine.API.Binding
{
    /// <summary>
    /// This class holds information relating a low-level macro parameter data-store value access binding
    /// with a scalar binding pattern. The macro parameter can be bound to an unspecified variable or to a 
    /// specific constant value.
    /// </summary>
    public sealed class GMacMacroParameterBinding
    {
        internal static GMacMacroParameterBinding CreateConstant(AstDatastoreValueAccess valueAccess, int value)
        {
            return new GMacMacroParameterBinding(valueAccess, value.ToExpr());
        }

        internal static GMacMacroParameterBinding CreateConstant(AstDatastoreValueAccess valueAccess, double value)
        {
            return new GMacMacroParameterBinding(valueAccess, value.ToExpr());
        }

        internal static GMacMacroParameterBinding CreateConstant(AstDatastoreValueAccess valueAccess, float value)
        {
            return new GMacMacroParameterBinding(valueAccess, value.ToExpr());
        }

        internal static GMacMacroParameterBinding CreateConstant(AstDatastoreValueAccess valueAccess, ILanguageValuePrimitive value)
        {
            return new GMacMacroParameterBinding(valueAccess, value.ToExpr());
        }

        internal static GMacMacroParameterBinding CreateConstant(AstDatastoreValueAccess valueAccess, Expr valueExpr)
        {
            return new GMacMacroParameterBinding(valueAccess, valueExpr);
        }

        internal static GMacMacroParameterBinding CreateVariable(AstDatastoreValueAccess valueAccess, Expr testValueExpr = null)
        {
            return new GMacMacroParameterBinding(valueAccess, null, testValueExpr);
        }

        internal static GMacMacroParameterBinding Create(AstDatastoreValueAccess valueAccess, GMacScalarBinding scalarPattern, Expr testValueExpr = null)
        {
            return new GMacMacroParameterBinding(valueAccess, scalarPattern.ConstantExpr, testValueExpr);
        }


        /// <summary>
        /// The value access with a root macro parameter and a leaf primitive type
        /// </summary>
        public AstDatastoreValueAccess ValueAccess { get; }

        /// <summary>
        /// The full name of the macro parameter primitive value access
        /// </summary>
        public string ValueAccessName => ValueAccess.ValueAccessName;

        /// <summary>
        /// Create ascalar binding from this macro parameter binding
        /// </summary>
        public GMacScalarBinding ToScalarBinding 
            => IsVariable
            ? GMacScalarBinding.CreateVariable(Root)
            : GMacScalarBinding.CreateConstant(Root, ConstantExpr);

        /// <summary>
        /// The symbolic constant expression associated with this scalar pattern. If null the pattern is a
        /// variable binding scalar pattern
        /// </summary>
        public Expr ConstantExpr { get; }

        /// <summary>
        /// The name given to this macro parameter when generating final target code.
        /// This property is only used for use within the FormGMacMacroExplorer
        /// </summary>
        public string TargetVariableName { get; set; }

        /// <summary>
        /// The symbolic constant expression associated with this scalar pattern. This should be only
        /// used for variable bindings with macro input parameters
        /// </summary>
        public Expr TestValueExpr { get; }

        /// <summary>
        /// The constarnt symbolic scalar associated with this macro parameter binding
        /// </summary>
        internal MathematicaScalar ConstantSymbolicScalar => IsVariable
            ? null
            : MathematicaScalar.Create(GaSymbolicsUtils.Cas, ConstantExpr);

        /// <summary>
        /// The cosntant value associated with this macro parameter binding
        /// </summary>
        public AstValueScalar ConstantValue => IsVariable
            ? null
            : new AstValueScalar(
                ValuePrimitive<MathematicaScalar>.Create(
                    GMacType.AssociatedPrimitiveType,
                    MathematicaScalar.Create(GaSymbolicsUtils.Cas, ConstantExpr)
                    )
                );

        /// <summary>
        /// The root AST of this macro parameter binding
        /// </summary>
        public AstRoot Root => ValueAccess.Root;

        /// <summary>
        /// The base macro of this macro parameter binding
        /// </summary>
        public AstMacro BaseMacro => ValueAccess.RootAsMacroParameter.ParentMacro;

        /// <summary>
        /// The GMac scalar type of this macro parameter binding
        /// </summary>
        public AstType GMacType => ValueAccess.Root.ScalarType;

        /// <summary>
        /// True if this is a binding with an output macro parameter
        /// </summary>
        public bool IsInput => ValueAccess.IsInputParameter;

        /// <summary>
        /// True if this is a binding with an input macro parameter
        /// </summary>
        public bool IsOutput => ValueAccess.IsOutputParameter;

        /// <summary>
        /// True if this is a variable macro parameter binding
        /// </summary>
        public bool IsVariable => ConstantExpr == null;

        /// <summary>
        /// True if this is a constant macro parameter binding
        /// </summary>
        public bool IsConstant => ConstantExpr != null;

        /// <summary>
        /// True if this is a variable or non-zero constant macro parameter binding
        /// </summary>
        public bool IsNonZero => ConstantExpr == null || IsNonZeroConstant;

        /// <summary>
        /// True if this is a non-zero constant macro parameter binding
        /// </summary>
        public bool IsNonZeroConstant => ReferenceEquals(ConstantExpr, null) == false &&
                                         (ConstantExpr.Args.Length == 0 && ConstantExpr.ToString() == "0") == false;

        /// <summary>
        /// True if this is a zero constant macro parameter binding
        /// </summary>
        public bool IsZeroConstant => ReferenceEquals(ConstantExpr, null) == false &&
                                      ConstantExpr.Args.Length == 0 && ConstantExpr.ToString() == "0";


        private GMacMacroParameterBinding(AstDatastoreValueAccess valueAccess, Expr constExpr, Expr testValueExpr = null)
        {
            if (valueAccess == null)
                throw new ArgumentNullException(nameof(valueAccess));

            if (valueAccess.IsScalar == false)
                throw new InvalidOperationException("Macro parameter must be of type 'scalar'");

            ValueAccess = valueAccess;
            ConstantExpr = constExpr;
            TestValueExpr = testValueExpr;
        }


        public override string ToString()
        {
            var s =
                new StringBuilder()
                .Append(ValueAccessName)
                .Append(" = ");

            if (IsConstant)
                s.Append("constant: '").Append(ConstantExpr).Append("'");

            else if (!string.IsNullOrEmpty(TargetVariableName))
                s.Append("variable: ").Append(TargetVariableName);

            return s.ToString();
        }
    }
}
