using GMac.GMacCompiler.Semantic.ASTConstants;
using IronyGrammars.Semantic.Scope;
using IronyGrammars.Semantic.Symbol;
using UtilLib.DataStructures;

namespace GMac.GMacCompiler.Semantic.AST
{
    /// <summary>
    /// A specification of a subspace of a given frame. The subspace is any linear combination of a fixed set of basis blades of the frame.
    /// </summary>
    public sealed class GMacFrameSubspace : LanguageSymbol
    {
        /// <summary>
        /// The signature boolean pattern of this subspace. The boolean pattern holds 1's where basis blades are
        /// present in the subspace spanning set of basis and 0's otherwise; where each basis blade is defined by its ID in the frame
        /// </summary>
        internal BooleanPattern SubspaceSignaturePattern { get; private set; }

        internal GMacFrame ParentFrame => (GMacFrame)ParentLanguageSymbol;

        internal GMacAst GMacRootAst => (GMacAst)RootAst;


        internal GMacFrameSubspace(string subspaceName, LanguageScope frameScope, BooleanPattern signature)
            : base(subspaceName, frameScope, RoleNames.FrameSubspace)
        {
            SubspaceSignaturePattern = signature;
        }
    }
}
