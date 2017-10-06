using System.Collections.Generic;
using System.Linq;
using GMac.GMacCompiler.Semantic.ASTConstants;
using GMac.GMacCompiler.Symbolic.Frame;
using GMac.GMacUtils;
using IronyGrammars.Semantic.Expression.Value;
using IronyGrammars.Semantic.Scope;
using IronyGrammars.Semantic.Symbol;
using UtilLib.DataStructures;

namespace GMac.GMacCompiler.Semantic.AST
{
    /// <summary>
    /// A GMac frame. This is the primary symbolic implementation structure for all GA multivectors and multivector operations
    /// </summary>
    public sealed class GMacFrame : SymbolWithScope, IGMacFrame
    {
        internal GMacAst GMacRootAst => (GMacAst)RootAst;

        /// <summary>
        /// The associated sumbolic GA frame
        /// </summary>
        internal GaFrame AssociatedSymbolicFrame { get; }

        /// <summary>
        /// The GMac base frame if this is a rerived frame
        /// </summary>
        internal GMacFrame BaseFrame { get; private set; }

        internal GMacMultivectorTransform DerivedToBaseTransform { get; private set; }

        internal GMacMultivectorTransform BaseToDerivedTransform { get; private set; }

        /// <summary>
        /// True if this has a base frame. This is not always equivalent to being non-orthogonal
        /// because we can derive a frame that happens to have a diagonal inner product matrix
        /// </summary>
        public bool IsDerivedFrame => BaseFrame != null;

        /// <summary>
        /// True if the default symbols of this frame are defined properly
        /// </summary>
        public bool DefaultSymbolsReady { get; internal set; }

        /// <summary>
        /// All child basis vectors of this frame
        /// </summary>
        internal IEnumerable<GMacFrameBasisVector> FrameBasisVectors { get; }

        /// <summary>
        /// The multivector type associated with this frame
        /// </summary>
        internal GMacFrameMultivector MultivectorType { get; private set; }

        /// <summary>
        /// All child constants of this frame
        /// </summary>
        internal IEnumerable<GMacConstant> ChildConstants { get; private set; }

        /// <summary>
        /// All child macros of this frame
        /// </summary>
        internal IEnumerable<GMacMacro> ChildMacros { get; private set; }

        /// <summary>
        /// All child subspaces of this frame
        /// </summary>
        internal IEnumerable<GMacFrameSubspace> FrameSubspaces { get; }

        /// <summary>
        ///All child structures
        /// </summary>
        internal IEnumerable<GMacStructure> Structures { get; private set; }

        /// <summary>
        ///All child transforms
        /// </summary>
        internal IEnumerable<GMacMultivectorTransform> Transforms { get; private set; }

        /// <summary>
        /// The symbols cache used to speed access to sub-symbols
        /// </summary>
        internal GMacAstSymbolsCache SymbolsCache { get; private set; }


        /// <summary>
        /// The base vector space dimension of this frame
        /// </summary>
        public int VSpaceDimension => AssociatedSymbolicFrame.VSpaceDimension;

        /// <summary>
        /// Get the GA space dimension of the frame
        /// </summary>
        public int GaSpaceDimension => (1 << VSpaceDimension);

        /// <summary>
        /// The maximum possible basis blade ID for this frame
        /// </summary>
        public int MaxBasisBladeId => (1 << VSpaceDimension) - 1;

        /// <summary>
        /// The number of grades in this frame
        /// </summary>
        public int GradesCount => VSpaceDimension + 1;

        /// <summary>
        /// The names of the basis vectors of this frame
        /// </summary>
        internal string[] BasisVectorNames
        {
            get { return FrameBasisVectors.Select(x => x.ObjectName).ToArray(); }
        }


        internal GMacFrame(string frameName, LanguageScope parentScope, GaFrame attachedFrame)
            : base(frameName, parentScope, RoleNames.Frame)
        {
            AssociatedSymbolicFrame = attachedFrame;

            FrameBasisVectors = 
                ChildSymbolScope.Symbols(RoleNames.FrameBasisVector).Cast<GMacFrameBasisVector>();

            ChildConstants = 
                ChildSymbolScope.Symbols(RoleNames.Constant).Cast<GMacConstant>();

            ChildMacros = 
                ChildSymbolScope.Symbols(RoleNames.Macro).Cast<GMacMacro>();

            FrameSubspaces = 
                ChildSymbolScope.Symbols(RoleNames.FrameSubspace).Cast<GMacFrameSubspace>();

            Structures =
                ChildSymbolScope.Symbols(RoleNames.Structure).Cast<GMacStructure>();

            Transforms =
                ChildSymbolScope.Symbols(RoleNames.Transform).Cast<GMacMultivectorTransform>();
        }

        public string BasisBladeName(int basisBladeId)
        {
            return BasisVectorNames.ConcatenateUsingPattern(basisBladeId, "E0", "^");
        }

        public string BasisBladeName(int grade, int index)
        {
            return BasisVectorNames.ConcatenateUsingPattern(FrameUtils.BasisBladeId(grade, index), "E0", "^");
        }

        public string BasisBladeName(int basisBladeId, BasisBladeFormat nameFormat)
        {
            switch (nameFormat)
            {
                case BasisBladeFormat.Canonical:
                    return BasisBladeName(basisBladeId);

                case BasisBladeFormat.BinaryIndexed:
                    return FrameUtils.BasisBladeBinaryIndexedName(VSpaceDimension, basisBladeId);

                case BasisBladeFormat.GradePlusIndex:
                    return basisBladeId.BasisBladeGradeIndexName();

                default:
                    return basisBladeId.BasisBladeIndexedName();
            }
        }

        public string BasisBladeName(int grade, int index, BasisBladeFormat nameFormat)
        {
            switch (nameFormat)
            {
                case BasisBladeFormat.Canonical:
                    return BasisBladeName(grade, index);

                case BasisBladeFormat.BinaryIndexed:
                    return FrameUtils.BasisBladeBinaryIndexedName(VSpaceDimension, grade, index);

                case BasisBladeFormat.GradePlusIndex:
                    return FrameUtils.BasisBladeGradeIndexName(grade, index);

                default:
                    return FrameUtils.BasisBladeIndexedName(grade, index);
            }
        }


        /// <summary>
        /// Set the basis frame and transformations between frames for this derived frame
        /// </summary>
        /// <param name="baseFrame"></param>
        /// <param name="d2BOm"></param>
        /// <param name="b2DOm"></param>
        internal void SetDfs(GMacFrame baseFrame, GMacMultivectorTransform d2BOm, GMacMultivectorTransform b2DOm)
        {
            BaseFrame = baseFrame;
            DerivedToBaseTransform = d2BOm;
            BaseToDerivedTransform = b2DOm;
        }

        /// <summary>
        /// Create a child macro
        /// </summary>
        /// <param name="macroName"></param>
        /// <returns></returns>
        internal GMacMacro DefineFrameMacro(string macroName)
        {
            return new GMacMacro(macroName, ChildSymbolScope);
        }

        /// <summary>
        /// Create a child constant
        /// </summary>
        /// <param name="constantName"></param>
        /// <param name="constantValue"></param>
        /// <returns></returns>
        internal GMacConstant DefineFrameConstant(string constantName, ILanguageValue constantValue)
        {
            return new GMacConstant(constantName, ChildSymbolScope, constantValue);
        }

        /// <summary>
        /// Create a child structure
        /// </summary>
        /// <param name="structureName"></param>
        /// <returns></returns>
        internal GMacStructure DefineFrameStructure(string structureName)
        {
            return new GMacStructure(structureName, ChildSymbolScope);
        }


        /// <summary>
        /// Define a new basis vector for this frame
        /// </summary>
        /// <param name="symbolName"></param>
        /// <returns></returns>
        internal GMacFrameBasisVector DefineBasisVector(string symbolName)
        {
            var basisVectorIndex = FrameBasisVectors.Count();

            var signature = AssociatedSymbolicFrame.BasisVectorSignature(basisVectorIndex);

            return new GMacFrameBasisVector(symbolName, this, basisVectorIndex, signature);
        }

        /// <summary>
        /// Create the full set of basis vectors for this frame
        /// </summary>
        /// <param name="basisVectorsNames"></param>
        internal void DefineBasisVectors(string[] basisVectorsNames)
        {
            foreach (var symbolName in basisVectorsNames)
                DefineBasisVector(symbolName);
        }

        /// <summary>
        /// Create the multivector type associated with this frame
        /// </summary>
        /// <returns></returns>
        internal GMacFrameMultivector DefineFrameMultivector()
        {
            return MultivectorType ?? (MultivectorType = new GMacFrameMultivector(ChildSymbolScope));
        }

        /// <summary>
        /// Create a subspace of this frame
        /// </summary>
        /// <param name="symbolName"></param>
        /// <param name="signature"></param>
        /// <returns></returns>
        internal GMacFrameSubspace DefineSubspace(string symbolName, BooleanPattern signature)
        {
            //new FrameSubspace(symbol_name, this.ChildScope, signature)

            //This is to reduce memory usage for frames with high dimensions
            BooleanPattern newSignature;
            LookupSubspaceSignaturePattern(signature, out newSignature);

            return new GMacFrameSubspace(symbolName, ChildSymbolScope, newSignature);
        }


        internal bool LookupBasisVector(string symbolName, out GMacFrameBasisVector outSymbol)
        {
            return ChildSymbolScope.LookupSymbol(symbolName, RoleNames.FrameBasisVector, out outSymbol);
        }

        internal bool LookupSubspace(string symbolName, out GMacFrameSubspace outSymbol)
        {
            return ChildSymbolScope.LookupSymbol(symbolName, RoleNames.FrameSubspace, out outSymbol);
        }

        internal bool LookupFrameConstant(string symbolName, out GMacConstant outSymbol)
        {
            return ChildSymbolScope.LookupSymbol(symbolName, RoleNames.Constant, out outSymbol);
        }

        internal bool LookupFrameMacro(string symbolName, out GMacMacro outSymbol)
        {
            return ChildSymbolScope.LookupSymbol(symbolName, RoleNames.Macro, out outSymbol);
        }

        internal bool LookupFrameStructure(string symbolName, out GMacStructure outSymbol)
        {
            return ChildSymbolScope.LookupSymbol(symbolName, RoleNames.Structure, out outSymbol);
        }

        internal bool LookupSubspaceBySignature(BooleanPattern signature, out GMacFrameSubspace outSymbol)
        {
            outSymbol =
                FrameSubspaces.FirstOrDefault( x => x.SubspaceSignaturePattern == signature );

            return ReferenceEquals(outSymbol, null) == false;
        }

        /// <summary>
        /// Search for a child subspace in this frame having the given signature. 
        /// If a subspace is found return its signature alse return the given input signature
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="outSignature"></param>
        /// <returns></returns>
        private bool LookupSubspaceSignaturePattern(BooleanPattern signature, out BooleanPattern outSignature)
        {
            var outSymbol =
                FrameSubspaces.FirstOrDefault( x => x.SubspaceSignaturePattern == signature );

            if (ReferenceEquals(outSymbol, null))
            {
                outSignature = signature;
                return false;
            }

            outSignature = outSymbol.SubspaceSignaturePattern;
            return true;
        }

        /// <summary>
        /// Create the symbols cache for this frame
        /// </summary>
        internal void CreateSymbolsCache()
        {
            SymbolsCache = new GMacAstSymbolsCache(this);
        }
    }
}
