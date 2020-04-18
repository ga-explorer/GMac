using System;
using System.Collections.Generic;
using GeometricAlgebraNumericsLib.Maps;
using GeometricAlgebraNumericsLib.Maps.Unilinear;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraNumericsLib.Outermorphisms
{
    public sealed class GaNumStoredOutermorphism : GaNumMapUnilinear
    {
        public static GaNumStoredOutermorphism Create(Matrix vectorsMappingMatrix)
        {
            var multivectorsMap =
                vectorsMappingMatrix
                    .ToOutermorphismDictionary()
                    .ToTreeMap(
                        vectorsMappingMatrix.ColumnCount,
                        vectorsMappingMatrix.RowCount
                    );

            return new GaNumStoredOutermorphism(
                vectorsMappingMatrix,
                multivectorsMap,
                GaUnilinearMapImplementation.Tree
            );
        }

        public static GaNumStoredOutermorphism Create(Matrix vectorsMappingMatrix, GaUnilinearMapImplementation multivectorsMappingKind)
        {
            if (multivectorsMappingKind == GaUnilinearMapImplementation.Identity)
            {
                var vSpaceDim = vectorsMappingMatrix.ColumnCount;

                return new GaNumStoredOutermorphism(
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

                case GaUnilinearMapImplementation.SparseColumns:
                    multivectorsMap = mvMappingDictionaryDict.ToSparseColumnsMap(
                        vectorsMappingMatrix.ColumnCount,
                        vectorsMappingMatrix.RowCount
                    );
                    break;

                //case GaUnilinearMapImplementation.SparseRows:
                //    multivectorsMap = mvMappingDictionaryDict.ToSparseRowsMap(
                //        vectorsMappingMatrix.ColumnCount,
                //        vectorsMappingMatrix.RowCount
                //    );
                //    break;

                case GaUnilinearMapImplementation.Matrix:
                    multivectorsMap = mvMappingDictionaryDict.ToSparseMatrixMap(
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

            return new GaNumStoredOutermorphism(
                vectorsMappingMatrix,
                multivectorsMap,
                multivectorsMappingKind
            );
        }

        public static GaNumStoredOutermorphism CreateIdentity(int vSpaceDim)
        {
            return new GaNumStoredOutermorphism(
                    DenseMatrix.CreateIdentity(vSpaceDim),
                    GaNumMapUnilinearIdentity.Create(vSpaceDim),
                    GaUnilinearMapImplementation.Identity
                );
        }

        public static GaNumStoredOutermorphism CreateArray(Matrix vectorsMappingMatrix)
        {
            var multivectorsMap =
                vectorsMappingMatrix
                    .ToOutermorphismDictionary()
                    .ToArrayMap(
                        vectorsMappingMatrix.ColumnCount,
                        vectorsMappingMatrix.RowCount
                    );

            return new GaNumStoredOutermorphism(
                vectorsMappingMatrix,
                multivectorsMap,
                GaUnilinearMapImplementation.Array
            );
        }

        public static GaNumStoredOutermorphism CreateSparseColumns(Matrix vectorsMappingMatrix)
        {
            var multivectorsMap =
                vectorsMappingMatrix
                    .ToOutermorphismDictionary()
                    .ToSparseColumnsMap(
                        vectorsMappingMatrix.ColumnCount,
                        vectorsMappingMatrix.RowCount
                    );

            return new GaNumStoredOutermorphism(
                vectorsMappingMatrix,
                multivectorsMap,
                GaUnilinearMapImplementation.SparseColumns
            );
        }

        //public static GaNumStoredOutermorphism CreateSparseRows(Matrix vectorsMappingMatrix)
        //{
        //    var multivectorsMap =
        //        vectorsMappingMatrix
        //            .ToOutermorphismDictionary()
        //            .ToSparseRowsMap(
        //                vectorsMappingMatrix.ColumnCount,
        //                vectorsMappingMatrix.RowCount
        //            );

        //    return new GaNumStoredOutermorphism(
        //        vectorsMappingMatrix,
        //        multivectorsMap,
        //        GaUnilinearMapImplementation.SparseRows
        //    );
        //}

        public static GaNumStoredOutermorphism CreateTree(Matrix vectorsMappingMatrix)
        {
            var multivectorsMap =
                vectorsMappingMatrix
                    .ToOutermorphismDictionary()
                    .ToTreeMap(
                        vectorsMappingMatrix.ColumnCount,
                        vectorsMappingMatrix.RowCount
                    );

            return new GaNumStoredOutermorphism(
                vectorsMappingMatrix,
                multivectorsMap,
                GaUnilinearMapImplementation.Tree
            );
        }

        public static GaNumStoredOutermorphism CreateMatrix(Matrix vectorsMappingMatrix)
        {
            var multivectorsMap =
                vectorsMappingMatrix
                    .ToOutermorphismDictionary()
                    .ToSparseMatrixMap(
                        vectorsMappingMatrix.ColumnCount,
                        vectorsMappingMatrix.RowCount
                    );

            return new GaNumStoredOutermorphism(
                vectorsMappingMatrix,
                multivectorsMap,
                GaUnilinearMapImplementation.Matrix
            );
        }

        public static GaNumStoredOutermorphism CreateCoefSums(Matrix vectorsMappingMatrix)
        {
            var multivectorsMap =
                vectorsMappingMatrix
                    .ToOutermorphismDictionary()
                    .ToCoefSumsMap(
                        vectorsMappingMatrix.ColumnCount,
                        vectorsMappingMatrix.RowCount
                    );

            return new GaNumStoredOutermorphism(
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

        public override GaNumSarMultivector this[GaNumSarMultivector mv1]
            => _multivectorsMap[mv1];

        public override GaNumDgrMultivector this[GaNumDgrMultivector mv1]
            => _multivectorsMap[mv1];


        private GaNumStoredOutermorphism(Matrix vectorsMappingMatrix, GaNumMapUnilinear multivectorsMap, GaUnilinearMapImplementation multivectorsMappingKind)
        {
            _multivectorsMap = multivectorsMap;

            VectorsMappingMatrix = vectorsMappingMatrix;
            MultivectorsMappingKind = multivectorsMappingKind;
        }


        public override Matrix GetMappingMatrix()
        {
            InternalMappingMatrix = _multivectorsMap.MappingMatrix;

            return InternalMappingMatrix;
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
        

        public override IEnumerable<Tuple<int, IGaNumMultivector>> BasisBladeMaps()
        {
            return _multivectorsMap.BasisBladeMaps();
        }

        public override IEnumerable<Tuple<int, IGaNumMultivector>> BasisVectorMaps()
        {
            for (var index = 0; index < DomainVSpaceDimension; index++)
            {
                var mv = GaNumSarMultivector.CreateFromColumn(VectorsMappingMatrix, index);

                if (!mv.IsNullOrEmpty())
                    yield return Tuple.Create(index, (IGaNumMultivector)mv);
            }
        }
    }
}
