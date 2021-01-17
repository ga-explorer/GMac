// using System;
// using System.Collections.Generic;
// using GeometricAlgebraNumericsLib.Maps.Unilinear;
// using GeometricAlgebraStructuresLib.Frames;
// using GeometricAlgebraStructuresLib.Multivectors;
// using MathNet.Numerics.LinearAlgebra.Double;
//
// namespace GeometricAlgebraStructuresLib.Maps.Unilinear
// {
//     public abstract class GaMapUnilinear<T> : GaMap, IGaMapUnilinear<T>
//     {
//         protected Matrix InternalMappingMatrix;
//
//
//         public abstract int DomainVSpaceDimension { get; }
//
//         public int DomainGaSpaceDimension
//             => DomainVSpaceDimension.ToGaSpaceDimension();
//
//         public IGaMultivector<T> this[int grade1, int index1]
//             => this[GaFrameUtils.BasisBladeId(grade1, index1)];
//
//         public abstract IGaMultivector<T> this[int id1] { get; }
//
//         public abstract IGaMultivector<T> this[IGaMultivector<T> mv1] { get; }
//
//         public virtual IGaMultivector<T> DomainPseudoScalarMap 
//             => this[DomainGaSpaceDimension - 1];
//
//         public Matrix MappingMatrix
//         {
//             get
//             {
//                 if (ReferenceEquals(InternalMappingMatrix, null))
//                     GetMappingMatrix();
//
//                 return InternalMappingMatrix;
//             }
//         }
//
//
//         public virtual T[,] GetMappingMatrix()
//         {
//             var matrixItems = new T[TargetGaSpaceDimension, DomainGaSpaceDimension];
//
//             for (var col = 0; col < DomainGaSpaceDimension; col++)
//             {
//                 var mv = this[col];
//
//                 if (mv.IsZero())
//                     continue;
//
//                 for (var row = 0; row < TargetGaSpaceDimension; row++)
//                     matrixItems[row, col] = mv[row];
//             }
//
//             return matrixItems;
//         }
//
//         public virtual T[,] GetVectorsMappingMatrix()
//         {
//             var matrixItems = new T[TargetVSpaceDimension, DomainVSpaceDimension];
//
//             foreach (var pair in BasisVectorMaps())
//             {
//                 var col = pair.Item1;
//
//                 foreach (var term in pair.Item2.Storage.GetNonZeroTerms())
//                 {
//                     var row = term.Id.BasisBladeIndex();
//
//                     matrixItems[row, col] = term.Scalar;
//                 }
//             }
//
//             return matrixItems;
//         }
//
//
//         public virtual GaMapUnilinear<T> Adjoint()
//         {
//             var exprArray = this.ToScalarsArray();
//
//             var resultMap = GaNumMapUnilinearTree.Create(
//                 TargetVSpaceDimension,
//                 DomainVSpaceDimension
//             );
//
//             for (var id = 0; id < TargetGaSpaceDimension; id++)
//             {
//                 var mv = GaNumSarMultivector.CreateFromRow(exprArray, id);
//
//                 if (!mv.IsNullOrEmpty())
//                     resultMap.SetBasisBladeMap(id, mv);
//             }
//
//             return resultMap;
//         }
//
//         public virtual GaMapUnilinear<T> Inverse()
//         {
//             return (this.ToDenseMatrix().Inverse() as Matrix).ToTreeMap();
//         }
//
//         public virtual GaMapUnilinear<T> InverseAdjoint()
//         {
//             return (this.ToDenseMatrix().Inverse().Transpose() as Matrix).ToTreeMap();
//         }
//
//
//         public abstract IEnumerable<Tuple<int, IGaMultivector<T>>> BasisBladeMaps();
//
//         public virtual IEnumerable<Tuple<int, IGaMultivector<T>>> BasisVectorMaps()
//         {
//             for (var index = 0; index < DomainVSpaceDimension; index++)
//             {
//                 var mv = this[GaFrameUtils.BasisBladeId(1, index)];
//
//                 if (!mv.IsZero())
//                     yield return new Tuple<int, IGaMultivector<T>>(index, mv);
//             }
//         }
//     }
// }
