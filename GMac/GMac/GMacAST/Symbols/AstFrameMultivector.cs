using CodeComposerLib.Irony.Semantic.Symbol;
using GMac.GMacAST.Expressions;
using GMac.GMacCompiler.Semantic.AST;

namespace GMac.GMacAST.Symbols
{
    /// <summary>
    /// This class represent a multivector type in GMacAST
    /// </summary>
    public sealed class AstFrameMultivector : AstSymbol, IAstObjectWithType
    {
        #region Static members
        #endregion


        internal GMacFrameMultivector AssociatedFrameMultivector { get; }

        internal override LanguageSymbol AssociatedSymbol => AssociatedFrameMultivector;


        public override bool IsValidFrameMultivector => AssociatedFrameMultivector != null;

        public override bool IsValidType => AssociatedFrameMultivector != null;

        public AstType GMacType => new AstType(AssociatedFrameMultivector);

        public string GMacTypeSignature => AssociatedFrameMultivector.TypeSignature;

        /// <summary>
        /// True if the given type is the same as this multivector type
        /// </summary>
        /// <param name="typeInfo"></param>
        /// <returns></returns>
        public bool IsSameType(AstType typeInfo)
        {
            return AssociatedFrameMultivector.IsSameType(typeInfo.AssociatedType);
        }

        /// <summary>
        /// True if the given type is the same as this multivector type
        /// </summary>
        /// <param name="typeInfo"></param>
        /// <returns></returns>
        public bool IsSameType(AstFrameMultivector typeInfo)
        {
            return AssociatedFrameMultivector.IsSameType(typeInfo.AssociatedFrameMultivector);
        }

        /// <summary>
        /// The default value of this type
        /// </summary>
        public AstValueMultivector DefaultValue => new AstValueMultivector(
            (GMacValueMultivector)
                AssociatedFrameMultivector
                    .RootAst
                    .CreateDefaultValue(AssociatedFrameMultivector)
            );

        /// <summary>
        /// The name of the parent frame of this multivector type
        /// </summary>
        public string ParentFrameName => AssociatedFrameMultivector.ParentFrame.ObjectName;

        /// <summary>
        /// The qualified access name of the parent frame of this multivector type
        /// </summary>
        public string ParentFrameAccessName => AssociatedFrameMultivector.ParentFrame.SymbolAccessName;


        internal AstFrameMultivector(GMacFrameMultivector mvClass)
        {
            AssociatedFrameMultivector = mvClass;
        }
    }
}
