using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMac.GMacAST.Expressions;
using GMac.GMacCompiler.Semantic.AST;
using GMac.GMacMath;
using GMac.GMacMath.Symbolic;
using GMac.GMacScripting;
using IronyGrammars.Semantic.Expression.Value;
using IronyGrammars.Semantic.Expression.ValueAccess;
using IronyGrammars.Semantic.Symbol;
using SymbolicInterface.Mathematica.Expression;
using UtilLib.DataStructures.SimpleTree;
using Wolfram.NETLink;

namespace GMac.GMacAST.Symbols
{
    /// <summary>
    /// This class represents a frame's basis vector in GMacAST
    /// </summary>
    public sealed class AstFrameBasisVector : AstSymbol, IAstObjectWithValue, IAstObjectWithDatastoreValueAccess
    {
        #region Static members
        #endregion


        internal GMacFrameBasisVector AssociatedBasisVector { get; }

        internal override LanguageSymbol AssociatedSymbol => AssociatedBasisVector;


        public override bool IsValidBasisVector => AssociatedBasisVector != null;

        public override bool IsValidDatastore => AssociatedBasisVector != null;

        public override bool IsValidConstantDatastore => AssociatedBasisVector != null;

        public AstType GMacType => new AstType(AssociatedBasisVector.SymbolType);

        public AstValue Value => AssociatedBasisVector.MultivectorValue.ToAstValue();

        public SimpleTreeNode<Expr> ValueSimpleTree => AssociatedBasisVector.MultivectorValue.ToSimpleExprTree();

        public string GMacTypeSignature => AssociatedBasisVector.ParentFrame.MultivectorType.TypeSignature;

        public AstDatastoreValueAccess DatastoreValueAccess => new AstDatastoreValueAccess(LanguageValueAccess.Create(AssociatedBasisVector));

        public AstExpression Expression => AssociatedBasisVector.MultivectorValue.ToAstExpression();


        /// <summary>
        /// Convert this basis vector into a basis blade
        /// </summary>
        public AstFrameBasisBlade ToBasisBlade => new AstFrameBasisBlade(AssociatedBasisVector.ParentFrame, AssociatedBasisVector.BasisVectorId);

        /// <summary>
        /// Create a multivector term value with unity coefficient from this basis vector
        /// </summary>
        public AstValueMultivectorTerm ToMultivectorTermValue()
        {
            return new AstValueMultivectorTerm(
                AssociatedBasisVector.ParentFrame.MultivectorType,
                AssociatedBasisVector.BasisVectorId,
                ValuePrimitive<MathematicaScalar>.Create(
                        AssociatedBasisVector.GMacRootAst.ScalarType,
                        SymbolicUtils.Constants.One
                    )
                );
        }

        /// <summary>
        /// Create a multivector value having a single term with unity coefficient from this basis vector
        /// </summary>
        public AstValueMultivector ToMultivectorValue()
        {
            return new AstValueMultivector(AssociatedBasisVector.MultivectorValue);
        }

        /// <summary>
        /// The multivector type of the basis vector
        /// </summary>
        public AstFrameMultivector FrameMultivector => new AstFrameMultivector(AssociatedBasisVector.ParentFrame.MultivectorType);

        /// <summary>
        /// The scalar signature of this basis vector (the scalar product of this vector with itself)
        /// </summary>
        public MathematicaScalar GaSignatureScalar => AssociatedBasisVector.Signature;

        /// <summary>
        /// The scalar signature value of this basis vector (the scalar product of this vector with itself)
        /// </summary>
        public AstValueScalar GaSignatureValue => new AstValueScalar(
            ValuePrimitive<MathematicaScalar>.Create(
                AssociatedBasisVector.GMacRootAst.ScalarType, 
                AssociatedBasisVector.Signature
                )
            );

        /// <summary>
        /// The ID of this basis vector taken as a basis blade
        /// </summary>
        public int BasisBladeId => ParentFrame.BasisBladeId(1, AssociatedBasisVector.BasisVectorIndex);

        /// <summary>
        /// The grade of this basis vector (always 1)
        /// </summary>
        public int Grade => 1;

        /// <summary>
        /// The index of this basis vector
        /// </summary>
        public int Index => AssociatedBasisVector.BasisVectorIndex;

        /// <summary>
        /// The binary indexed name of this basis vector
        /// </summary>
        public string BinaryIndexedName => AssociatedBasisVector
            .ParentFrame
            .BasisBladeBinaryIndexedName(AssociatedBasisVector.BasisVectorId);

        /// <summary>
        /// The indexed name of this basis vector
        /// </summary>
        public string IndexedName => AssociatedBasisVector
            .ParentFrame
            .BasisBladeIndexedName(AssociatedBasisVector.BasisVectorId);

        /// <summary>
        /// The grade + index name of this basis vector
        /// </summary>
        public string GradeIndexName => AssociatedBasisVector
            .ParentFrame
            .BasisBladeGradeIndexName(AssociatedBasisVector.BasisVectorId);

        /// <summary>
        /// The name of this basis vector as a coefficient
        /// </summary>
        public string CoefName => new StringBuilder().Append('#').Append(Name).Append('#').ToString();

        /// <summary>
        /// Returns a list of basis blade IDs that contain this basis vector
        /// </summary>
        public IEnumerable<int> ParentBasisBladeIDs => ParentFrame.BasisBladeIDsContaining(1, AssociatedBasisVector.BasisVectorIndex);

        /// <summary>
        /// Returns a list of basis blades that contain this basis vector
        /// </summary>
        public IEnumerable<AstFrameBasisBlade> ParentBasisBlades
        {
            get
            {
                return
                    ParentFrame
                    .BasisBladeIDsContaining(1, AssociatedBasisVector.BasisVectorIndex)
                    .Select(id => ParentFrame.BasisBlade(id));
            }
        }


        internal AstFrameBasisVector(GMacFrameBasisVector basisVector)
        {
            AssociatedBasisVector = basisVector;
        }
    }
}
