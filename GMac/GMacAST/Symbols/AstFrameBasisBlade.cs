using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMac.GMacAST.Expressions;
using GMac.GMacCompiler.Semantic.AST;
using GMac.GMacCompiler.Symbolic;
using GMac.GMacUtils;
using IronyGrammars.Semantic.Expression.Value;
using IronyGrammars.Semantic.Symbol;
using SymbolicInterface.Mathematica.Expression;
using UtilLib.DataStructures.SimpleTree;
using Wolfram.NETLink;
using FrameUtils = GMac.GMacUtils.EuclideanUtils;

namespace GMac.GMacAST.Symbols
{
    /// <summary>
    /// This class holds information about a frame basis blade
    /// </summary>
    public sealed class AstFrameBasisBlade : AstSymbol, IAstObjectWithValue
    {
        #region Static members
        #endregion


        private AstValueMultivectorTerm _associatedMultivectorTermValue;


        internal AstValueMultivectorTerm AssociatedMultivectorTermValue
        {
            get
            {
                if (ReferenceEquals(_associatedMultivectorTermValue, null))
                    _associatedMultivectorTermValue =
                        new AstValueMultivectorTerm(
                            AssociatedFrame.MultivectorType,
                            BasisBladeId,
                            ValuePrimitive<MathematicaScalar>.Create(
                                AssociatedFrame.GMacRootAst.ScalarType,
                                SymbolicUtils.Constants.One
                                )
                            );

                return _associatedMultivectorTermValue;
            }
        }

        internal GMacFrameMultivector AssociatedMultivector { get; }

        internal GMacFrame AssociatedFrame => AssociatedMultivector.ParentFrame;

        internal override LanguageSymbol AssociatedSymbol => AssociatedMultivector;


        /// <summary>
        /// The Basis Blade ID
        /// </summary>
        public int BasisBladeId { get; }

        public override bool IsValid => AssociatedMultivector != null &&
                                        AssociatedFrame.IsValidBasisBladeId(BasisBladeId);

        public override AstRoot Root => new AstRoot(AssociatedMultivector.GMacRootAst);

        public override bool IsValidBasisBlade => AssociatedMultivector != null &&
                                                  AssociatedFrame.IsValidBasisBladeId(BasisBladeId);

        public override bool IsValidDatastore => AssociatedMultivector != null &&
                                                 AssociatedFrame.IsValidBasisBladeId(BasisBladeId);

        public override bool IsValidConstantDatastore => AssociatedMultivector != null &&
                                                         AssociatedFrame.IsValidBasisBladeId(BasisBladeId);

        public AstType GMacType => new AstType(AssociatedMultivector);

        public string GMacTypeSignature => AssociatedFrame.MultivectorType.TypeSignature;

        public AstValue Value => AssociatedMultivectorTermValue;

        public SimpleTreeNode<Expr> ValueSimpleTree => new SimpleTreeBranchDictionaryByIndex<Expr>
        {
            {
                BasisBladeId, 
                "#E" + BasisBladeId + "#", 
                AssociatedFrame.GMacRootAst.ScalarType.TypeSignature,
                SymbolicUtils.Constants.ExprOne
            }
        };

        public AstExpression Expression => AssociatedMultivectorTermValue;

        public override string Name => AssociatedFrame.BasisBladeName(BasisBladeId);

        public override string AccessName => BasisBladeId == 0 
            ? "1" 
            : AssociatedFrame.SymbolAccessName + ".&" + AssociatedFrame.BasisBladeName(BasisBladeId) + "&";

        public override string SymbolicMathName
        {
            get
            {
                var root = ((GMacAst)AssociatedSymbol.RootAst);

                var objectName = AssociatedFrame.BasisBladeName(BasisBladeId);

                var accessName = AssociatedFrame.SymbolAccessName + ".&" + objectName + "&";

                var mathName = root.SymbolicMathNames[accessName];

                return String.IsNullOrEmpty(mathName) ? objectName : mathName;
            }
        }


        public override string RoleName => "basis_blade";

        /// <summary>
        /// The frame multivector type of this basis blade
        /// </summary>
        public AstFrameMultivector FrameMultivector => new AstFrameMultivector(AssociatedMultivector);

        /// <summary>
        /// The grade of this basis blade
        /// </summary>
        public int Grade => BasisBladeId.BasisBladeGrade();

        /// <summary>
        /// The zero-based index of this basis blade among the basis blades of the same grade
        /// </summary>
        public int Index => BasisBladeId.BasisBladeIndex();

        /// <summary>
        /// The name of this basis blade as a coefficient
        /// </summary>
        public string CoefName => new StringBuilder().Append('#').Append(Name).Append('#').ToString();

        /// <summary>
        /// The binary indexed name of this basis blade
        /// </summary>
        public string BinaryIndexedName => AssociatedFrame.BasisBladeBinaryIndexedName(BasisBladeId);

        /// <summary>
        /// The indexed name of this basis blade
        /// </summary>
        public string IndexedName => AssociatedFrame.BasisBladeIndexedName(BasisBladeId);

        /// <summary>
        /// The grade + index name of this basis blade
        /// </summary>
        public string GradeIndexName => AssociatedFrame.BasisBladeGradeIndexName(BasisBladeId);

        /// <summary>
        /// True if this basis blade is of grade zero
        /// </summary>
        public bool IsScalar => Grade == 0;

        /// <summary>
        /// True if this basis blade is of grade 1
        /// </summary>
        public bool IsVector => Grade == 1;

        /// <summary>
        /// True if this basis blade is a pseudo-vector (having grade n-1 where n is the frame dimension)
        /// </summary>
        public bool IsPseudoVector => Grade == (AssociatedFrame.VSpaceDimension - 1);

        /// <summary>
        /// True if this basis blade is a pseudo-scalar (having grade n where n is the frame dimension)
        /// </summary>
        public bool IsPseudoScalar => Grade == AssociatedFrame.VSpaceDimension;


        /// <summary>
        /// The basis vectors ID's whos outer product gives this basis blade
        /// </summary>
        public IEnumerable<int> BasisVectorIDs => BasisBladeId.GetBasicPatterns();

        /// <summary>
        /// The basis vectors indexes whos outer product gives this basis blade
        /// </summary>
        public IEnumerable<int> BasisVectorIndexes => BasisBladeId.PatternToPositions();

        /// <summary>
        /// The basis vectors whos outer product gives this basis blade
        /// </summary>
        public IEnumerable<AstFrameBasisVector> BasisVectors 
        { 
            get 
            {
                var basisVectors = AssociatedFrame.FrameBasisVectors.ToArray();

                return
                    BasisBladeId
                    .PatternToPositions()
                    .Select(
                        index => new AstFrameBasisVector(basisVectors[index])
                        ); 
            } 
        }

        /// <summary>
        /// Returns a list of basis blade IDs that contain this basis blade
        /// </summary>
        public IEnumerable<int> ParentBasisBladeIDs => ParentFrame.BasisBladeIDsContaining(BasisBladeId);

        /// <summary>
        /// Returns a list of basis blades that contain this basis vector
        /// </summary>
        public IEnumerable<AstFrameBasisBlade> ParentBasisBlades => ParentFrame.BasisBladesContaining(BasisBladeId);


        /// <summary>
        /// True if the reverse of this basis blade is negative to itself
        /// </summary>
        public bool HasNegativeReverse => BasisBladeId.BasisBladeIdHasNegativeReverse();

        /// <summary>
        /// True if the grade involution of this basis blade is negative to itself
        /// </summary>
        public bool HasNegativeGradeInv => BasisBladeId.BasisBladeIdHasNegativeGradeInv();

        /// <summary>
        /// True if the Clifford conjugate of this basis blade is negative to itself
        /// </summary>
        public bool HasNegativeClifConj => BasisBladeId.BasisBladeIdHasNegativeClifConj();


        /// <summary>
        /// Convert this basis blade object into a basis vector object if possible
        /// </summary>
        /// <returns></returns>
        public AstFrameBasisVector ToBasisVector()
        {
            return 
                IsVector 
                ? ParentFrame.BasisVectorFromIndex(BasisBladeId.FirstOneBitPosition()) 
                : null;
        }

        /// <summary>
        /// Convert this basis blade into a multivector term value with unity coefficient
        /// </summary>
        public AstValueMultivectorTerm ToMultivectorTermValue()
        {
            return new AstValueMultivectorTerm(
                AssociatedFrame.MultivectorType,
                BasisBladeId,
                ValuePrimitive<MathematicaScalar>.Create(
                        AssociatedFrame.GMacRootAst.ScalarType,
                        SymbolicUtils.Constants.One
                    )
                );
        }

        /// <summary>
        /// Convert this basis blade into a multivector value with unity coefficient of its single term
        /// </summary>
        /// <returns></returns>
        public AstValueMultivector ToMultivectorValue()
        {
            return new AstValueMultivector(
                GMacValueMultivector.Create(
                        AssociatedFrame.MultivectorType,
                        GaMultivector.CreateBasisBlade(
                            AssociatedFrame.GaSpaceDimension,
                            BasisBladeId
                            )
                    )
                );
        }


        /// <summary>
        /// Sets the symbolic math name of this GMacAST symbol
        /// </summary>
        /// <param name="mathName"></param>
        public override void SetSymbolicMathName(string mathName)
        {
            var root = ((GMacAst)AssociatedSymbol.RootAst);

            var accessName =
                AssociatedFrame.SymbolAccessName + ".&" + AssociatedFrame.BasisBladeName(BasisBladeId) + "&";

            root.SymbolicMathNames[accessName] = mathName;
        }

        /// <summary>
        /// Sets the symbolic math name of this GMacAST symbol to nothing
        /// </summary>
        public override void SetSymbolicMathName()
        {
            var root = ((GMacAst)AssociatedSymbol.RootAst);

            var accessName = 
                AssociatedFrame.SymbolAccessName + ".&" + AssociatedFrame.BasisBladeName(BasisBladeId) + "&";

            root.SymbolicMathNames.RemoveName(accessName);
        }

        
        internal AstFrameBasisBlade(GMacFrameMultivector mvType, int id)
        {
            AssociatedMultivector = mvType;
            BasisBladeId = id;
        }

        internal AstFrameBasisBlade(GMacFrame frame, int id)
        {
            AssociatedMultivector = frame.MultivectorType;
            BasisBladeId = id;
        }

        internal AstFrameBasisBlade(GMacFrameMultivector mvType, int grade, int index)
        {
            AssociatedMultivector = mvType;
            BasisBladeId = GMacUtils.FrameUtils.BasisBladeId(grade, index);
        }

        internal AstFrameBasisBlade(GMacFrame frame, int grade, int index)
        {
            AssociatedMultivector = frame.MultivectorType;
            BasisBladeId = GMacUtils.FrameUtils.BasisBladeId(grade, index);
        }
    }
}
