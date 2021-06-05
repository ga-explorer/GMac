using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeComposerLib.Irony.Semantic.Expression.Value;
using CodeComposerLib.Irony.Semantic.Symbol;
using DataStructuresLib;
using DataStructuresLib.SimpleTree;
using GeometricAlgebraStructuresLib.Frames;
using GeometricAlgebraSymbolicsLib;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;
using GeometricAlgebraSymbolicsLib.Multivectors;
using GMac.Engine.AST.Expressions;
using GMac.Engine.Compiler.Semantic.AST;
using Wolfram.NETLink;

namespace GMac.Engine.AST.Symbols
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
                                GaSymbolicsUtils.Constants.One
                                )
                            );

                return _associatedMultivectorTermValue;
            }
        }

        internal GMacFrameMultivector AssociatedMultivector { get; }

        internal GMacFrame AssociatedFrame 
            => AssociatedMultivector.ParentFrame;

        public override LanguageSymbol AssociatedSymbol 
            => AssociatedMultivector;


        /// <summary>
        /// The Basis Blade ID
        /// </summary>
        public ulong BasisBladeId { get; }

        public override bool IsValid 
            => AssociatedMultivector != null &&
                AssociatedFrame.IsValidBasisBladeId(BasisBladeId);

        public override AstRoot Root 
            => new AstRoot(AssociatedMultivector.GMacRootAst);

        public override bool IsValidBasisBlade 
            => AssociatedMultivector != null &&
                AssociatedFrame.IsValidBasisBladeId(BasisBladeId);

        public override bool IsValidDatastore 
            => AssociatedMultivector != null &&
                AssociatedFrame.IsValidBasisBladeId(BasisBladeId);

        public override bool IsValidConstantDatastore 
            => AssociatedMultivector != null &&
                AssociatedFrame.IsValidBasisBladeId(BasisBladeId);

        public AstType GMacType 
            => new AstType(AssociatedMultivector);

        public string GMacTypeSignature 
            => AssociatedFrame.MultivectorType.TypeSignature;

        public AstValue Value 
            => AssociatedMultivectorTermValue;

        public SimpleTreeNode<Expr> ValueSimpleTree 
            => new SimpleTreeBranchDictionaryByIndex<Expr>
            {
                {
                    (int)BasisBladeId, 
                    "#E" + BasisBladeId + "#", 
                    AssociatedFrame.GMacRootAst.ScalarType.TypeSignature,
                    GaSymbolicsUtils.Constants.ExprOne
                }
            };

        public AstExpression Expression 
            => AssociatedMultivectorTermValue;

        public override string Name 
            => AssociatedFrame.BasisBladeName(BasisBladeId);

        public override string AccessName 
            => BasisBladeId == 0 
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

                return string.IsNullOrEmpty(mathName) ? objectName : mathName;
            }
        }


        public override string RoleName 
            => "basis_blade";

        /// <summary>
        /// The frame multivector type of this basis blade
        /// </summary>
        public AstFrameMultivector FrameMultivector 
            => new AstFrameMultivector(AssociatedMultivector);

        /// <summary>
        /// The grade of this basis blade
        /// </summary>
        public int Grade 
            => BasisBladeId.BasisBladeGrade();

        /// <summary>
        /// The zero-based index of this basis blade among the basis blades of the same grade
        /// </summary>
        public ulong Index 
            => BasisBladeId.BasisBladeIndex();

        /// <summary>
        /// The name of this basis blade as a coefficient
        /// </summary>
        public string CoefName 
            => new StringBuilder().Append('#').Append(Name).Append('#').ToString();

        /// <summary>
        /// The binary indexed name of this basis blade
        /// </summary>
        public string BinaryIndexedName 
            => AssociatedFrame.BasisBladeBinaryIndexedName(BasisBladeId);

        /// <summary>
        /// The indexed name of this basis blade
        /// </summary>
        public string IndexedName 
            => AssociatedFrame.BasisBladeIndexedName(BasisBladeId);

        /// <summary>
        /// The grade + index name of this basis blade
        /// </summary>
        public string GradeIndexName 
            => AssociatedFrame.BasisBladeGradeIndexName(BasisBladeId);

        /// <summary>
        /// True if this basis blade is of grade zero
        /// </summary>
        public bool IsScalar 
            => Grade == 0;

        /// <summary>
        /// True if this basis blade is of grade 1
        /// </summary>
        public bool IsVector 
            => Grade == 1;

        /// <summary>
        /// True if this basis blade is a pseudo-vector (having grade n-1 where n is the frame dimension)
        /// </summary>
        public bool IsPseudoVector 
            => Grade == (AssociatedFrame.VSpaceDimension - 1);

        /// <summary>
        /// True if this basis blade is a pseudo-scalar (having grade n where n is the frame dimension)
        /// </summary>
        public bool IsPseudoScalar 
            => Grade == AssociatedFrame.VSpaceDimension;


        /// <summary>
        /// The basis vectors ID's whose outer product gives this basis blade
        /// </summary>
        public IEnumerable<ulong> BasisVectorIDs 
            => BasisBladeId.GetBasicPatterns();

        /// <summary>
        /// The basis vectors indexes whose outer product gives this basis blade
        /// </summary>
        public IEnumerable<ulong> BasisVectorIndexes 
            => BasisBladeId.PatternToPositions().Select(i => (ulong)i);

        /// <summary>
        /// The basis vectors whose outer product gives this basis blade
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
        public IEnumerable<ulong> ParentBasisBladeIDs 
            => ParentFrame.BasisBladeIDsContaining(BasisBladeId);

        /// <summary>
        /// Returns a list of basis blades that contain this basis vector
        /// </summary>
        public IEnumerable<AstFrameBasisBlade> ParentBasisBlades 
            => ParentFrame.BasisBladesContaining(BasisBladeId);


        /// <summary>
        /// True if the reverse of this basis blade is negative to itself
        /// </summary>
        public bool HasNegativeReverse 
            => BasisBladeId.BasisBladeIdHasNegativeReverse();

        /// <summary>
        /// True if the grade involution of this basis blade is negative to itself
        /// </summary>
        public bool HasNegativeGradeInv 
            => BasisBladeId.BasisBladeIdHasNegativeGradeInv();

        /// <summary>
        /// True if the Clifford conjugate of this basis blade is negative to itself
        /// </summary>
        public bool HasNegativeClifConj 
            => BasisBladeId.BasisBladeIdHasNegativeCliffConj();


        /// <summary>
        /// Convert this basis blade object into a basis vector object if possible
        /// </summary>
        /// <returns></returns>
        public AstFrameBasisVector ToBasisVector()
        {
            return 
                IsVector 
                ? ParentFrame.BasisVectorFromIndex((ulong)BasisBladeId.FirstOneBitPosition()) 
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
                        GaSymbolicsUtils.Constants.One
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
                        GaSymMultivector.CreateBasisBlade(
                            AssociatedFrame.VSpaceDimension,
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

        
        internal AstFrameBasisBlade(GMacFrameMultivector mvType, ulong id)
        {
            AssociatedMultivector = mvType;
            BasisBladeId = id;
        }

        internal AstFrameBasisBlade(GMacFrame frame, ulong id)
        {
            AssociatedMultivector = frame.MultivectorType;
            BasisBladeId = id;
        }

        internal AstFrameBasisBlade(GMacFrameMultivector mvType, int grade, ulong index)
        {
            AssociatedMultivector = mvType;
            BasisBladeId = GaFrameUtils.BasisBladeId(grade, index);
        }

        internal AstFrameBasisBlade(GMacFrame frame, int grade, ulong index)
        {
            AssociatedMultivector = frame.MultivectorType;
            BasisBladeId = GaFrameUtils.BasisBladeId(grade, index);
        }
    }
}
