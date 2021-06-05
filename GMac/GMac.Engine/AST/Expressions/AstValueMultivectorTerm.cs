using CodeComposerLib.Irony.Semantic.Expression.Value;
using GeometricAlgebraStructuresLib.Frames;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;
using GeometricAlgebraSymbolicsLib.Multivectors;
using GMac.Engine.AST.Symbols;
using GMac.Engine.Compiler.Semantic.AST;
using Wolfram.NETLink;

namespace GMac.Engine.AST.Expressions
{
    public sealed class AstValueMultivectorTerm : AstValue
    {
        #region Static members
        #endregion


        private GMacValueMultivector _associatedMultivectorValue;

        internal GMacValueMultivector AssociatedMultivectorValue
        {
            get
            {
                if (ReferenceEquals(_associatedMultivectorValue, null))
                    _associatedMultivectorValue =
                        GMacValueMultivector.Create(
                            AssociatedFrameMultivector,
                            GaSymMultivector.CreateTerm(
                                AssociatedFrameMultivector.ParentFrame.VSpaceDimension,
                                TermBasisBladeId,
                                TermCoef.Value
                                )
                            );

                return _associatedMultivectorValue;
            }
        }

        internal GMacFrameMultivector AssociatedFrameMultivector { get; }

        internal override ILanguageValue AssociatedValue => AssociatedMultivectorValue;

        /// <summary>
        /// The basis blade scalar coefficient value of this multivector term
        /// </summary>
        internal ValuePrimitive<MathematicaScalar> TermCoef { get; }


        public override bool IsValidMultivectorTermValue => AssociatedFrameMultivector != null;

        public override bool IsValidCompositeValue => AssociatedFrameMultivector != null;

        /// <summary>
        /// The basis blade ID of this multivector term
        /// </summary>
        public ulong TermBasisBladeId { get; }

        /// <summary>
        /// The basis blade grade of this multivector term
        /// </summary>
        public int TermBasisBladeGrade => TermBasisBladeId.BasisBladeGrade();

        /// <summary>
        /// The basis blade index of this multivector term
        /// </summary>
        public ulong TermBasisBladeIndex => TermBasisBladeId.BasisBladeIndex();

        /// <summary>
        /// The basis blade scalar coefficient symbolic scalar of this multivector term
        /// </summary>
        public MathematicaScalar TermCoefScalar => TermCoef.Value;

        /// <summary>
        /// The basis blade scalar coefficient Mathematica expression of this multivector term
        /// </summary>
        public Expr TermCoefExpr => TermCoef.Value.Expression;

        //public SimpleTreeNode<Expr> GetAstValueSimpleTree
        //{
        //    get
        //    {
        //        return new SimpleTreeBranchDictionaryByIndex<Expr>
        //        {
        //            {
        //                TermId, 
        //                "#E" + TermId + "#", 
        //                AssociatedFrameMultivector.GMacRootAst.ScalarType.TypeSignature,
        //                TermCoef.Value.MathExpr
        //            }
        //        };
        //    }
        //}

        /// <summary>
        /// The multivector type of this term
        /// </summary>
        public AstFrameMultivector FrameMultivector => new AstFrameMultivector(AssociatedFrameMultivector);

        /// <summary>
        /// The frame of the multivector type of this term
        /// </summary>
        public AstFrame Frame => new AstFrame(AssociatedFrameMultivector.ParentFrame);

        /// <summary>
        /// The basis blade of this term
        /// </summary>
        public AstFrameBasisBlade BasisBlade => new AstFrameBasisBlade(AssociatedFrameMultivector.ParentFrame, TermBasisBladeId);

        /// <summary>
        /// The basis blade coefficient value of this multivector term
        /// </summary>
        public AstValueScalar CoefValue => new AstValueScalar(TermCoef);

        /// <summary>
        /// Convert this term into a multivector value
        /// </summary>
        public AstValueMultivector ToMultivectorValue => AssociatedMultivectorValue.ToAstValueMultivector();


        internal AstValueMultivectorTerm(GMacFrameMultivector mvClass, ulong termId, ValuePrimitive<MathematicaScalar> coef)
        {
            AssociatedFrameMultivector = mvClass;
            TermBasisBladeId = termId;
            TermCoef = coef;   
        }
    }
}
