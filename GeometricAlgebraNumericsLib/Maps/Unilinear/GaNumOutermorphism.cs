using System;
using System.Collections.Generic;
using GeometricAlgebraNumericsLib.Multivectors;
using GeometricAlgebraNumericsLib.Multivectors.Intermediate;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraNumericsLib.Maps.Unilinear
{
    public sealed class GaNumOutermorphism : GaNumMapUnilinear
    {
        public static GaNumOutermorphism Create(Matrix vectorsMappingMatrix)
        {
            var multivectorsMap =
                vectorsMappingMatrix
                    .ToOutermorphismDictionary()
                    .ToTreeMap(
                        vectorsMappingMatrix.ColumnCount,
                        vectorsMappingMatrix.RowCount
                    );

            return new GaNumOutermorphism(
                vectorsMappingMatrix,
                multivectorsMap,
                GaUnilinearMapImplementation.Tree
            );
        }

        public static GaNumOutermorphism Create(Matrix vectorsMappingMatrix, GaUnilinearMapImplementation multivectorsMappingKind)
        {
            if (multivectorsMappingKind == GaUnilinearMapImplementation.Identity)
            {
                var vSpaceDim = vectorsMappingMatrix.ColumnCount;

                return new GaNumOutermorphism(
                    DenseMatrix.CreateIdentity(vSpaceDim),
                    GaNumMapUnilinearIdentity.Create(vSpaceDim),
                    GaUnilinearMapImplementation.Identity
                );
            }

            var mvMappingDictionaryDict = vectorsMappingMatrix.ToOutermorphismDictionary();
            GaNumMapUnilinear multivectorsMap;

            switch (multivectorsMappingKind)
            {
                case GaUnilinearMapImplementation.Array:
                    multivectorsMap = mvMappingDictionaryDict.ToArrayMap(
                        vectorsMappingMatrix.ColumnCount,
                        vectorsMappingMatrix.RowCount
                    );
                    break;

                case GaUnilinearMapImplementation.Hash:
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

            return new GaNumOutermorphism(
                vectorsMappingMatrix,
                multivectorsMap,
                multivectorsMappingKind
            );
        }

        public static GaNumOutermorphism CreateIdentity(int vSpaceDim)
        {
            return new GaNumOutermorphism(
                    DenseMatrix.CreateIdentity(vSpaceDim),
                    GaNumMapUnilinearIdentity.Create(vSpaceDim),
                    GaUnilinearMapImplementation.Identity
                );
        }

        public static GaNumOutermorphism CreateArray(Matrix vectorsMappingMatrix)
        {
            var multivectorsMap =
                vectorsMappingMatrix
                    .ToOutermorphismDictionary()
                    .ToArrayMap(
                        vectorsMappingMatrix.ColumnCount,
                        vectorsMappingMatrix.RowCount
                    );

            return new GaNumOutermorphism(
                vectorsMappingMatrix,
                multivectorsMap,
                GaUnilinearMapImplementation.Array
            );
        }

        public static GaNumOutermorphism CreateHash(Matrix vectorsMappingMatrix)
        {
            var multivectorsMap =
                vectorsMappingMatrix
                    .ToOutermorphismDictionary()
                    .ToHashMap(
                        vectorsMappingMatrix.ColumnCount,
                        vectorsMappingMatrix.RowCount
                    );

            return new GaNumOutermorphism(
                vectorsMappingMatrix,
                multivectorsMap,
                GaUnilinearMapImplementation.Hash
            );
        }

        public static GaNumOutermorphism CreateTree(Matrix vectorsMappingMatrix)
        {
            var multivectorsMap =
                vectorsMappingMatrix
                    .ToOutermorphismDictionary()
                    .ToTreeMap(
                        vectorsMappingMatrix.ColumnCount,
                        vectorsMappingMatrix.RowCount
                    );

            return new GaNumOutermorphism(
                vectorsMappingMatrix,
                multivectorsMap,
                GaUnilinearMapImplementation.Tree
            );
        }

        public static GaNumOutermorphism CreateMatrix(Matrix vectorsMappingMatrix)
        {
            var multivectorsMap =
                vectorsMappingMatrix
                    .ToOutermorphismDictionary()
                    .ToMatrixMap(
                        vectorsMappingMatrix.ColumnCount,
                        vectorsMappingMatrix.RowCount
                    );

            return new GaNumOutermorphism(
                vectorsMappingMatrix,
                multivectorsMap,
                GaUnilinearMapImplementation.Matrix
            );
        }

        public static GaNumOutermorphism CreateCoefSums(Matrix vectorsMappingMatrix)
        {
            var multivectorsMap =
                vectorsMappingMatrix
                    .ToOutermorphismDictionary()
                    .ToCoefSumsMap(
                        vectorsMappingMatrix.ColumnCount,
                        vectorsMappingMatrix.RowCount
                    );

            return new GaNumOutermorphism(
                vectorsMappingMatrix,
                multivectorsMap,
                GaUnilinearMapImplementation.CoefSums
            );
        }


        private readonly GaNumMapUnilinear _multivectorsMap;


        public override int DomainVSpaceDimension
            => VectorsMappingMatrix.ColumnCount;

        public override int TargetVSpaceDimension
            => VectorsMappingMatrix.RowCount;

        public Matrix VectorsMappingMatrix { get; }

        public GaUnilinearMapImplementation MultivectorsMappingKind { get; }

        public double Determinant 
            => _multivectorsMap[TargetGaSpaceDimension - 1][0];

        public override IGaNumMultivector this[int id1]
            => _multivectorsMap[id1];


        private GaNumOutermorphism(Matrix vectorsMappingMatrix, GaNumMapUnilinear multivectorsMap, GaUnilinearMapImplementation multivectorsMappingKind)
        {
            _multivectorsMap = multivectorsMap;

            VectorsMappingMatrix = vectorsMappingMatrix;
            MultivectorsMappingKind = multivectorsMappingKind;
        }


        protected override void ComputeMappingMatrix()
        {
            InternalMappingMatrix = _multivectorsMap.MappingMatrix;
        }


        public override GaNumMapUnilinear Adjoint()
        {
            if (MultivectorsMappingKind == GaUnilinearMapImplementation.Identity)
                return this;

            return Create(
                    VectorsMappingMatrix.Transpose() as Matrix,
                    MultivectorsMappingKind
                );
        }

        public override GaNumMapUnilinear Inverse()
        {
            if (MultivectorsMappingKind == GaUnilinearMapImplementation.Identity)
                return this;

            return Create(
                VectorsMappingMatrix.Inverse() as Matrix,
                MultivectorsMappingKind
            );
        }

        public override GaNumMapUnilinear InverseAdjoint()
        {
            if (MultivectorsMappingKind == GaUnilinearMapImplementation.Identity)
                return this;

            return Create(
                VectorsMappingMatrix.Inverse().Transpose() as Matrix,
                MultivectorsMappingKind
            );
        }

        public override IGaNumMultivectorTemp MapToTemp(GaNumMultivector mv1)
        {
            return _multivectorsMap.MapToTemp(mv1);
        }

        public override IEnumerable<Tuple<int, IGaNumMultivector>> BasisBladeMaps()
        {
            return _multivectorsMap.BasisBladeMaps();
        }

        public override IEnumerable<Tuple<int, IGaNumMultivector>> BasisVectorMaps()
        {
            for (var index = 0; index < DomainVSpaceDimension; index++)
            {
                var mv = GaNumMultivector.CreateFromColumn(VectorsMappingMatrix, index);

                if (!mv.IsNullOrZero())
                    yield return Tuple.Create(index, (IGaNumMultivector)mv);
            }
        }
    }
}
