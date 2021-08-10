using System.Collections.Generic;
using System.Linq;
using CodeComposerLib.Irony.Semantic.Symbol;
using DataStructuresLib;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraStructuresLib.Frames;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;
using GeometricAlgebraSymbolicsLib.Frames;
using GMac.Engine.Compiler.Semantic.AST;
using GMac.Engine.Compiler.Semantic.ASTConstants;

namespace GMac.Engine.AST.Symbols
{
    /// <summary>
    /// This class represents a frame in GMacAST
    /// </summary>
    public sealed class AstFrame : AstSymbol, IGaFrame
    {
        #region Static members
        #endregion


        internal GMacFrame AssociatedFrame { get; }

        public override LanguageSymbol AssociatedSymbol => AssociatedFrame;


        public GaSymFrame AssociatedSymbolicFrame => AssociatedFrame?.SymbolicFrame;

        public override bool IsValidFrame => AssociatedFrame != null;

        /// <summary>
        /// The dimension of this frame (the number of basis vectors)
        /// </summary>
        public int VSpaceDimension => AssociatedFrame.VSpaceDimension;

        /// <summary>
        /// The GA dimension of this frame (2 ^ the number of basis vectors)
        /// </summary>
        public ulong GaSpaceDimension => AssociatedFrame.GaSpaceDimension;

        /// <summary>
        /// The max basis blade ID for this frame (2 ^ the number of basis vectors - 1)
        /// </summary>
        public ulong MaxBasisBladeId => AssociatedFrame.MaxBasisBladeId;

        /// <summary>
        /// The number of grades for this frame (the number of basis vectors + 1)
        /// </summary>
        public int GradesCount => AssociatedFrame.VSpaceDimension + 1;

        /// <summary>
        /// The base frame, if any
        /// </summary>
        public AstFrame BaseFrame => new AstFrame(AssociatedFrame.BaseFrame);

        /// <summary>
        /// The child symbols of this frame
        /// </summary>
        public IEnumerable<AstSymbol> ChildSymbols
        {
            get
            {
                return
                    AssociatedFrame
                    .ChildSymbols
                    .Select(item => item.ToAstSymbol());
            }
        }

        /// <summary>
        /// The main symbols of this frame
        /// </summary>
        public IEnumerable<AstSymbol> MainSymbols
        {
            get
            {
                return
                    AssociatedFrame
                        .SymbolsCache
                        .MainSymbols
                        .Select(pair => pair.Value.ToAstSymbol());

                //return
                //    AssociatedFrame
                //    .MainSymbols()
                //    .Select(symbol => new GMacInfoAstSymbol(symbol));
            }
        }

        /// <summary>
        /// The basis vectors of this frame
        /// </summary>
        public IEnumerable<AstFrameBasisVector> BasisVectors
        {
            get
            {
                return 
                    AssociatedFrame
                    .FrameBasisVectors
                    .Select(basisVector => new AstFrameBasisVector(basisVector));
            }
        }

        /// <summary>
        /// The multivector type of this frame
        /// </summary>
        public AstFrameMultivector FrameMultivector => new AstFrameMultivector(AssociatedFrame.MultivectorType);

        /// <summary>
        /// The constants of this frame
        /// </summary>
        public IEnumerable<AstConstant> Constants
        {
            get
            {
                return
                    AssociatedFrame
                    .ChildConstants
                    .Select(item => new AstConstant(item));
            }
        }

        /// <summary>
        /// The subspaces of this frame
        /// </summary>
        public IEnumerable<AstFrameSubspace> Subspaces
        {
            get
            {
                return
                    AssociatedFrame
                    .FrameSubspaces
                    .Select(item => new AstFrameSubspace(item));
            }
        }

        /// <summary>
        /// The structures of this frame
        /// </summary>
        public IEnumerable<AstStructure> Structures
        {
            get
            {
                return
                    AssociatedFrame
                    .Structures
                    .Select(item => new AstStructure(item));
            }
        }

        /// <summary>
        /// The transforms of this frame
        /// </summary>
        public IEnumerable<AstTransform> Transforms
        {
            get
            {
                return
                    AssociatedFrame
                    .Transforms
                    .Select(item => new AstTransform(item));
            }
        }

        /// <summary>
        /// The macros of this frame
        /// </summary>
        public IEnumerable<AstMacro> Macros
        {
            get
            {
                return
                    AssociatedFrame
                    .SymbolsCache
                    .Macros
                    .Select(pair => new AstMacro(pair.Value));

                //return
                //    AssociatedFrame
                //    .Macros()
                //    .Select(macro => new GMacInfoMacro(macro));
            }
        }

        /// <summary>
        /// The types of this frame
        /// </summary>
        public IEnumerable<AstType> Types
        {
            get
            {
                return
                    AssociatedFrame
                    .SymbolsCache
                    .Types
                    .Select(pair => new AstType(pair.Value));

                //return
                //    AssociatedFrame
                //    .Types()
                //    .Select(symbol => new GMacInfoType(symbol));
            }
        }

        /// <summary>
        /// Finds a symbol with the given qualified name under this frame
        /// </summary>
        /// <param name="accessName"></param>
        /// <returns></returns>
        public AstSymbol Symbol(string accessName)
        {
            AssociatedFrame.SymbolsCache.MainSymbols.TryGetValue(accessName, out var symbol);

            return symbol.ToAstSymbol();
        }

        /// <summary>
        /// Finds a basis vector with the given name under this frame
        /// </summary>
        /// <param name="accessName"></param>
        /// <returns></returns>
        public AstFrameBasisVector BasisVector(string accessName)
        {
            AssociatedFrame.SymbolsCache.FrameBasisVectors.TryGetValue(accessName, out var symbol);

            return new AstFrameBasisVector(symbol);
        }

        /// <summary>
        /// Finds a subspace with the given name under this frame
        /// </summary>
        /// <param name="accessName"></param>
        /// <returns></returns>
        public AstFrameSubspace Subspace(string accessName)
        {
            AssociatedFrame.SymbolsCache.FrameSubspaces.TryGetValue(accessName, out var symbol);

            return new AstFrameSubspace(symbol);
        }

        /// <summary>
        /// Finds a constant with the given name under this frame
        /// </summary>
        /// <param name="accessName"></param>
        /// <returns></returns>
        public AstConstant Constant(string accessName)
        {
            AssociatedFrame.SymbolsCache.Constants.TryGetValue(accessName, out var symbol);

            return new AstConstant(symbol);
        }

        /// <summary>
        /// Finds a structure with the given name under this frame
        /// </summary>
        /// <param name="accessName"></param>
        /// <returns></returns>
        public AstStructure Structure(string accessName)
        {
            AssociatedFrame.SymbolsCache.Structures.TryGetValue(accessName, out var symbol);

            return new AstStructure(symbol);
        }

        /// <summary>
        /// Finds a transform with the given name under this frame
        /// </summary>
        /// <param name="accessName"></param>
        /// <returns></returns>
        public AstTransform Transform(string accessName)
        {
            AssociatedFrame.SymbolsCache.Transforms.TryGetValue(accessName, out var symbol);

            return new AstTransform(symbol);
        }

        /// <summary>
        /// Finds a macro with the given name under this frame
        /// </summary>
        /// <param name="accessName"></param>
        /// <returns></returns>
        public AstMacro Macro(string accessName)
        {
            AssociatedFrame.SymbolsCache.Macros.TryGetValue(accessName, out var symbol);

            return new AstMacro(symbol);
        }

        /// <summary>
        /// Finds a type with the given name under this frame
        /// </summary>
        /// <param name="accessName"></param>
        /// <returns></returns>
        public AstType Type(string accessName)
        {
            AssociatedFrame.SymbolsCache.Types.TryGetValue(accessName, out var symbol);

            return new AstType(symbol);
        }


        internal AstFrame(GMacFrame frame)
        {
            AssociatedFrame = frame;
        }


        /// <summary>
        /// The inner product matrix of this frame
        /// </summary>
        /// <returns></returns>
        public MathematicaScalar[,] GetInnerProductMatrix()
        {
            var ipm = AssociatedFrame.SymbolicFrame.Ipm;

            var rows = ipm.RowCount;
            var cols = ipm.ColumnCount;

            var result = new MathematicaScalar[rows, cols];

            for (var r = 0; r < rows; r++)
                for (var c = 0; c < cols; c++)
                    result[r, c] = ipm[r, c];

            return result;
        }


        /// <summary>
        /// Gets a basis vector by its index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public AstFrameBasisVector BasisVectorFromIndex(ulong index)
        {
            return new AstFrameBasisVector(AssociatedFrame.FrameBasisVectors.ElementAt((int)index));
        }

        /// <summary>
        /// Gets a basis vector by its basis blade ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AstFrameBasisVector BasisVectorFromId(ulong id)
        {
            return new AstFrameBasisVector(AssociatedFrame.FrameBasisVectors.ElementAt(id.FirstOneBitPosition()));
        }

        /// <summary>
        /// Gets a basis blade by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AstFrameBasisBlade BasisBlade(ulong id)
        {
            return new AstFrameBasisBlade(AssociatedFrame, id);
        }

        /// <summary>
        /// Gets a basis blade by its grade and index
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public AstFrameBasisBlade BasisBlade(int grade, ulong index)
        {
            return new AstFrameBasisBlade(AssociatedFrame, grade, index);
        }


        public string BasisBladeName(ulong basisBladeId)
        {
            return AssociatedFrame.BasisVectorNames.ConcatenateUsingPattern(
                basisBladeId, "E0", "^"
                );
        }

        public string BasisBladeName(int grade, ulong index)
        {
            return AssociatedFrame.BasisVectorNames.ConcatenateUsingPattern(
                GaFrameUtils.BasisBladeId(grade, index), "E0", "^"
                );
        }

        public string BasisBladeName(ulong basisBladeId, BasisBladeFormat nameFormat)
        {
            switch (nameFormat)
            {
                case BasisBladeFormat.Canonical:
                    return BasisBladeName(basisBladeId);

                case BasisBladeFormat.BinaryIndexed:
                    return GaFrameUtils.BasisBladeBinaryIndexedName(VSpaceDimension, basisBladeId);

                case BasisBladeFormat.GradePlusIndex:
                    return basisBladeId.BasisBladeGradeIndexName();

                default:
                    return basisBladeId.BasisBladeIndexedName();
            }
        }

        public string BasisBladeName(int grade, ulong index, BasisBladeFormat nameFormat)
        {
            switch (nameFormat)
            {
                case BasisBladeFormat.Canonical:
                    return BasisBladeName(grade, index);

                case BasisBladeFormat.BinaryIndexed:
                    return GaFrameUtils.BasisBladeBinaryIndexedName(VSpaceDimension, grade, index);

                case BasisBladeFormat.GradePlusIndex:
                    return GaFrameUtils.BasisBladeGradeIndexName(grade, index);

                default:
                    return GaFrameUtils.BasisBladeIndexedName(grade, index);
            }
        }


        /// <summary>
        /// Returns a list of basis blades given their IDs
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public IEnumerable<AstFrameBasisBlade> BasisBlades(IEnumerable<ulong> ids)
        {
            return ids.Select(id => new AstFrameBasisBlade(AssociatedFrame, id));
        }

        /// <summary>
        /// Returns a list of basis blades given their IDs
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public IEnumerable<AstFrameBasisBlade> BasisBlades(params ulong[] ids)
        {
            return ids.Select(id => new AstFrameBasisBlade(AssociatedFrame, id));
        }

        /// <summary>
        /// The basis blades of this frame ordered by their IDs
        /// </summary>
        public IEnumerable<AstFrameBasisBlade> BasisBlades()
        {
            return BasisBlades(AssociatedFrame.BasisBladeIDs());
        }

        /// <summary>
        /// Returns a list of basis blades given their grade
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        public IEnumerable<AstFrameBasisBlade> BasisBladesOfGrade(int grade)
        {
            return BasisBlades(this.BasisBladeIDsOfGrade(grade));
        }

        /// <summary>
        /// Returns a list of basis blades given their grade and indexes
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="indexSeq"></param>
        /// <returns></returns>
        public IEnumerable<AstFrameBasisBlade> BasisBladesOfGrade(int grade, IEnumerable<ulong> indexSeq)
        {
            return BasisBlades(this.BasisBladeIDsOfGrade(grade, indexSeq));
        }

        /// <summary>
        /// Returns a list of basis blades given their grade and indexes
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="indexSeq"></param>
        /// <returns></returns>
        public IEnumerable<AstFrameBasisBlade> BasisBladesOfGrade(int grade, params ulong[] indexSeq)
        {
            return BasisBlades(this.BasisBladeIDsOfGradeIndex(grade, indexSeq));
        }

        /// <summary>
        /// The basis blades sorted by their grade and index
        /// </summary>
        /// <param name="startGrade"></param>
        /// <returns></returns>
        public IEnumerable<AstFrameBasisBlade> BasisBladesSortedByGrade(int startGrade = 0)
        {
            return BasisBlades(this.BasisBladeIDsSortedByGrade(startGrade));
        }

        /// <summary>
        /// Returns a list of basis blades given their grades
        /// </summary>
        /// <param name="grades"></param>
        /// <returns></returns>
        public IEnumerable<AstFrameBasisBlade> BasisBladesOfGrades(IEnumerable<int> grades)
        {
            return BasisBlades(this.BasisBladeIDsOfGrades(grades));
        }

        /// <summary>
        /// Returns a list of basis blades given their grades
        /// </summary>
        /// <param name="grades"></param>
        /// <returns></returns>
        public IEnumerable<AstFrameBasisBlade> BasisBladesOfGrades(params int[] grades)
        {
            return BasisBlades(this.BasisBladeIDsOfGrades(grades));
        }

        /// <summary>
        /// Returns the basis blades having the given grades grouped by their grade
        /// </summary>
        /// <param name="startGrade"></param>
        /// <returns></returns>
        public Dictionary<int, List<AstFrameBasisBlade>> BasisBladesGroupedByGrade(int startGrade = 0)
        {
            var result = new Dictionary<int, List<AstFrameBasisBlade>>();

            for (var grade = startGrade; grade <= VSpaceDimension; grade++)
            {
                var kvSpaceDim = GaFrameUtils.KvSpaceDimension(VSpaceDimension, grade);

                var newList = new List<AstFrameBasisBlade>((int)kvSpaceDim);

                for (var index = 0UL; index < kvSpaceDim; index++)
                    newList.Add(BasisBlade(grade, index));

                result.Add(grade, newList);
            }

            return result;
        }

        /// <summary>
        /// Returns the basis blades having the given grades grouped by their grade
        /// </summary>
        /// <param name="gradesSeq"></param>
        /// <returns></returns>
        public Dictionary<int, List<AstFrameBasisBlade>> BasisBladesGroupedByGrade(IEnumerable<int> gradesSeq)
        {
            var result = new Dictionary<int, List<AstFrameBasisBlade>>();

            foreach (var grade in gradesSeq)
            {
                var kvSpaceDim = GaFrameUtils.KvSpaceDimension(VSpaceDimension, grade);

                var newList = new List<AstFrameBasisBlade>((int)kvSpaceDim);

                for (var index = 0UL; index < kvSpaceDim; index++)
                    newList.Add(BasisBlade(grade, index));

                result.Add(grade, newList);
            }

            return result;
        }

        /// <summary>
        /// Returns the basis blades having the given grades grouped by their grade
        /// </summary>
        /// <param name="gradesSeq"></param>
        /// <returns></returns>
        public Dictionary<int, List<AstFrameBasisBlade>> BasisBladesGroupedByGrade(params int[] gradesSeq)
        {
            var result = new Dictionary<int, List<AstFrameBasisBlade>>();

            foreach (var grade in gradesSeq)
            {
                var kvSpaceDim = GaFrameUtils.KvSpaceDimension(VSpaceDimension, grade);

                var newList = new List<AstFrameBasisBlade>((int)kvSpaceDim);

                for (var index = 0UL; index < kvSpaceDim; index++)
                    newList.Add(BasisBlade(grade, index));

                result.Add(grade, newList);
            }

            return result;
        }

        public IEnumerable<AstFrameBasisVector> BasisVectorsInside(ulong basisBladeId)
        {
            return this.BasisVectorIndexesInside(basisBladeId).Select(BasisVectorFromIndex);
        }

        public IEnumerable<AstFrameBasisVector> BasisVectorsInside(int grade, ulong index)
        {
            return this.BasisVectorIndexesInside(grade, index).Select(BasisVectorFromIndex);
        }

        public IEnumerable<AstFrameBasisBlade> BasisBladesInside(ulong basisBladeId)
        {
            return BasisBlades(this.BasisVectorIDsInside(basisBladeId));
        }

        public IEnumerable<AstFrameBasisBlade> BasisBladesInside(int grade, ulong index)
        {
            return BasisBlades(this.BasisVectorIDsInside(grade, index));
        }

        public IEnumerable<AstFrameBasisBlade> BasisBladesContaining(ulong basisBladeId)
        {
            return BasisBlades(this.BasisBladeIDsContaining(basisBladeId));
        }

        public IEnumerable<AstFrameBasisBlade> BasisBladesContaining(int grade, ulong index)
        {
            return BasisBlades(this.BasisBladeIDsContaining(grade, index));
        }
    }
}
