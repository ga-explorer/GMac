using GMac.GMacAST.Expressions;
using GMac.GMacAST.Symbols;
using GMac.GMacCompiler.Semantic.AST;
using GMac.GMacCompiler.Semantic.AST.Extensions;
using IronyGrammars.Semantic.Type;

namespace GMac.GMacAST
{
    /// <summary>
    /// This is a wrapper around the IronyGrammars.Semantic.Type.ILanguageType interface to represent
    /// a type (primitive or composite) in GMacAST
    /// </summary>
    public sealed class AstType : AstObject, IAstObjectWithType
    {
        internal ILanguageType AssociatedType { get; }

        internal TypePrimitive AssociatedPrimitiveType => AssociatedType as TypePrimitive;


        public override bool IsValid => AssociatedType != null;

        public override AstRoot Root => new AstRoot((GMacAst)AssociatedType.RootAst);

        public override bool IsValidType => AssociatedType != null;

        public AstType GMacType => this;

        public string GMacTypeSignature => AssociatedType.TypeSignature;


        /// <summary>
        /// True for primitive types
        /// </summary>
        public bool IsValidPrimitiveType => IsValid && AssociatedType is TypePrimitive;

        /// <summary>
        /// True for composite types (structures and multivector types)
        /// </summary>
        public bool IsValidCompositeType => IsValid && (AssociatedType is TypePrimitive) == false;

        public bool IsValidIntegerType => IsValid && AssociatedType.IsInteger();

        /// <summary>
        /// True if this is a valid scalar type
        /// </summary>
        public bool IsValidScalarType => IsValid && AssociatedType.IsScalar();

        public bool IsValidBooleanType => IsValid && AssociatedType.IsBoolean();

        public bool IsValidNumberType => IsValid && AssociatedType.IsNumber();

        /// <summary>
        /// True if this is a valid multivector type
        /// </summary>
        public bool IsValidMultivectorType => IsValid && AssociatedType.IsFrameMultivector();

        /// <summary>
        /// True if this is a valid structure type
        /// </summary>
        public bool IsValidStructureType => IsValid && AssociatedType.IsStructure();

        /// <summary>
        /// True if this is a valid type exactly like the given type
        /// </summary>
        /// <param name="astType"></param>
        /// <returns></returns>
        public bool IsSameType(AstType astType)
        {
            return IsValid && astType.IsNotNullAndValid() && AssociatedType.IsSameType(astType.AssociatedType);
        }

        /// <summary>
        /// True if this is a valid type exactly like the given structure type
        /// </summary>
        /// <param name="astType"></param>
        /// <returns></returns>
        public bool IsSameType(AstStructure astType)
        {
            return IsValid && astType.IsNotNullAndValid() && AssociatedType.IsSameType(astType.AssociatedStructure);
        }

        /// <summary>
        /// True if this is a valid type exactly like the given multivector type
        /// </summary>
        /// <param name="astType"></param>
        /// <returns></returns>
        public bool IsSameType(AstFrameMultivector astType)
        {
            return IsValid && astType.IsNotNullAndValid() && AssociatedType.IsSameType(astType.AssociatedFrameMultivector);
        }


        /// <summary>
        /// Convert this AstType object into a AstFrameMultivector object if the internal object allowes
        /// this conversion
        /// </summary>
        public AstFrameMultivector ToFrameMultivector
        {
            get
            {
                var mvType = AssociatedType as GMacFrameMultivector;

                return
                    ReferenceEquals(mvType, null)
                    ? null
                    : new AstFrameMultivector(mvType);
            }
        }

        /// <summary>
        /// Convert this AstType object into a AstStructure object if the internal object allowes
        /// this conversion
        /// </summary>
        public AstStructure ToStructure
        {
            get
            {
                var typeStructure = AssociatedType as GMacStructure;

                return
                    ReferenceEquals(typeStructure, null)
                    ? null
                    : new AstStructure(typeStructure);
            }
        }

        /// <summary>
        /// The default value associated with this type
        /// </summary>
        public AstValue DefaultValue => AssociatedType.RootAst.CreateDefaultValue(AssociatedType).ToAstValue();


        internal AstType(ILanguageType langType)
        {
            AssociatedType = langType;
        }


        public override string ToString()
        {
            return AssociatedType.TypeSignature;
        }
    }
}
