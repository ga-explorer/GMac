using System;
using System.Collections.Generic;
using GeometricAlgebraNumericsLib.Maps.Unilinear;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;
using GeometricAlgebraSymbolicsLib.Multivectors;
using GeometricAlgebraSymbolicsLib.Multivectors.Intermediate;

namespace GeometricAlgebraSymbolicsLib.Maps.Unilinear
{
    public sealed class GaSymOutermorphism : GaSymMapUnilinear
    {
        public static GaSymOutermorphism Create(ISymbolicMatrix vectorsMappingMatrix)
        {
            var multivectorsMap =
                vectorsMappingMatrix
                    .ToOutermorphismDictionary()
                    .ToTreeMap(
                        vectorsMappingMatrix.ColumnCount,
                        vectorsMappingMatrix.RowCount
                    );

            return new GaSymOutermorphism(
                vectorsMappingMatrix,
                multivectorsMap,
                GaUnilinearMapImplementation.Tree
            );
        }

        public static GaSymOutermorphism Create(ISymbolicMatrix vectorsMappingMatrix, GaUnilinearMapImplementation multivectorsMappingKind)
        {
            if (multivectorsMappingKind == GaUnilinearMapImplementation.Identity)
            {
                var vSpaceDim = vectorsMappingMatrix.ColumnCount;

                return new GaSymOutermorphism(
                    MathematicaMatrix.CreateIdentity(GaSymbolicsUtils.Cas, vSpaceDim),
                    GaSymMapUnilinearIdentity.Create(vSpaceDim),
                    GaUnilinearMapImplementation.Identity
                );
            }

            var mvMappingDictionaryDict = vectorsMappingMatrix.ToOutermorphismDictionary();
            GaSymMapUnilinear multivectorsMap;

            switch (multivectorsMappingKind)
            {
                case GaUnilinearMapImplementation.Array:
                    multivectorsMap = mvMappingDictionaryDict.ToArrayMap(
                        vectorsMappingMatrix.ColumnCount,
                        vectorsMappingMatrix.RowCount
                    );
                    break;

                case GaUnilinearMapImplementation.SparseColumns:
                    multivectorsMap = mvMappingDictionaryDict.ToHashMap(
                        vectorsMappingMatrix.ColumnCount,
                        vectorsMappingMatrix.RowCount
                    );
                    break;

                case GaUnilinearMapImplementation.Matrix:
                    multivectorsMap = mvMappingDictionaryDict.ToMatrixMap(
                        vectorsMappingMatrix.ColumnCount,
                        vectorsMappingMatrix.RowCount
                    );
                    break;

                case GaUnilinearMapImplementation.CoefSums:
                    multivectorsMap = mvMappingDictionaryDict.ToCoefSumsMap(
                        vectorsMappingMatrix.ColumnCount,
                        vectorsMappingMatrix.RowCount
                    );
                    break;

                default:
                    multivectorsMap = mvMappingDictionaryDict.ToTreeMap(
                        vectorsMappingMatrix.ColumnCount,
                        vectorsMappingMatrix.RowCount
                    );
                    break;
            }

            return new GaSymOutermorphism(
                vectorsMappingMatrix,
                multivectorsMap,
                multivectorsMappingKind
            );
        }

        public static GaSymOutermorphism CreateIdentity(int vSpaceDim)
        {
            return new GaSymOutermorphism(
                    MathematicaMatrix.CreateIdentity(GaSymbolicsUtils.Cas, vSpaceDim),
                    GaSymMapUnilinearIdentity.Create(vSpaceDim),
                    GaUnilinearMapImplementation.Identity
                );
        }

        public static GaSymOutermorphism CreateArray(ISymbolicMatrix vectorsMappingMatrix)
        {
            var multivectorsMap =
                vectorsMappingMatrix
                    .ToOutermorphismDictionary()
                    .ToArrayMap(
                        vectorsMappingMatrix.ColumnCount,
                        vectorsMappingMatrix.RowCount
                    );

            return new GaSymOutermorphism(
                vectorsMappingMatrix,
                multivectorsMap,
                GaUnilinearMapImplementation.Array
            );
        }

        public static GaSymOutermorphism CreateHash(ISymbolicMatrix vectorsMappingMatrix)
        {
            var multivectorsMap =
                vectorsMappingMatrix
                    .ToOutermorphismDictionary()
                    .ToHashMap(
                        vectorsMappingMatrix.ColumnCount,
                        vectorsMappingMatrix.RowCount
                    );

            return new GaSymOutermorphism(
                vectorsMappingMatrix,
                multivectorsMap,
                GaUnilinearMapImplementation.SparseColumns
            );
        }

        public static GaSymOutermorphism CreateTree(ISymbolicMatrix vectorsMappingMatrix)
        {
            var multivectorsMap =
                vectorsMappingMatrix
                    .ToOutermorphismDictionary()
                    .ToTreeMap(
                        vectorsMappingMatrix.ColumnCount,
                        vectorsMappingMatrix.RowCount
                    );

            return new GaSymOutermorphism(
                vectorsMappingMatrix,
                multivectorsMap,
                GaUnilinearMapImplementation.Tree
            );
        }

        public static GaSymOutermorphism CreateMatrix(ISymbolicMatrix vectorsMappingMatrix)
        {
            var multivectorsMap =
                vectorsMappingMatrix
                    .ToOutermorphismDictionary()
                    .ToMatrixMap(
                        vectorsMappingMatrix.ColumnCount,
                        vectorsMappingMatrix.RowCount
                    );

            return new GaSymOutermorphism(
                vectorsMappingMatrix,
                multivectorsMap,
                GaUnilinearMapImplementation.Matrix
            );
        }

        public static GaSymOutermorphism CreateCoefSums(ISymbolicMatrix vectorsMappingMatrix)
        {
            var multivectorsMap =
                vectorsMappingMatrix
                    .ToOutermorphismDictionary()
                    .ToCoefSumsMap(
                        vectorsMappingMatrix.ColumnCount,
                        vectorsMappingMatrix.RowCount
                    );

            return new GaSymOutermorphism(
                vectorsMappingMatrix,
                multivectorsMap,
                GaUnilinearMapImplementation.CoefSums
            );
        }


        private readonly GaSymMapUnilinear _multivectorsMap;


        public override int DomainVSpaceDimension
            => VectorsMappingMatrix.ColumnCount;

        public override int TargetVSpaceDimension
            => VectorsMappingMatrix.RowCount;

        public ISymbolicMatrix VectorsMappingMatrix { get; }

        public GaUnilinearMapImplementation MultivectorsMappingKind { get; }

        public MathematicaScalar Determinant
            => _multivectorsMap[TargetGaSpaceDimension - 1][0].ToMathematicaScalar();

        public override IGaSymMultivector this[int id1]
            => _multivectorsMap[id1];


        private GaSymOutermorphism(ISymbolicMatrix vectorsMappingMatrix, GaSymMapUnilinear multivectorsMap, GaUnilinearMapImplementation multivectorsMappingKind)
        {
            _multivectorsMap = multivectorsMap;

            VectorsMappingMatrix = vectorsMappingMatrix;
            MultivectorsMappingKind = multivectorsMappingKind;
        }


        protected override void ComputeMappingMatrix()
        {
            InternalMappingMatrix = _multivectorsMap.MappingMatrix;
        }


        public override GaSymMapUnilinear Adjoint()
        {
            if (MultivectorsMappingKind == GaUnilinearMapImplementation.Identity)
                return this;

            return Create(
                    VectorsMappingMatrix.Transpose(),
                    MultivectorsMappingKind
                );
        }

        public override GaSymMapUnilinear Inverse()
        {
            if (MultivectorsMappingKind == GaUnilinearMapImplementation.Identity)
                return this;

            return Create(
                VectorsMappingMatrix.Inverse(),
                MultivectorsMappingKind
            );
        }

        public override GaSymMapUnilinear InverseAdjoint()
        {
            if (MultivectorsMappingKind == GaUnilinearMapImplementation.Identity)
                return this;

            return Create(
                VectorsMappingMatrix.InverseTranspose(),
                MultivectorsMappingKind
            );
        }

        public override IGaSymMultivectorTemp MapToTemp(int id1)
        {
            return _multivectorsMap.MapToTemp(id1);
        }

        public override IGaSymMultivectorTemp MapToTemp(GaSymMultivector mv1)
        {
            return _multivectorsMap.MapToTemp(mv1);
        }

        public override IEnumerable<Tuple<int, IGaSymMultivector>> BasisBladeMaps()
        {
            return _multivectorsMap.BasisBladeMaps();
        }

        public override IEnumerable<Tuple<int, IGaSymMultivector>> BasisVectorMaps()
        {
            for (var index = 0; index < DomainVSpaceDimension; index++)
            {
                var mv = GaSymMultivector.CreateFromColumn(VectorsMappingMatrix, index);

                if (!mv.IsNullOrZero())
                    yield return Tuple.Create(index, (IGaSymMultivector)mv);
            }
        }
    }
}
