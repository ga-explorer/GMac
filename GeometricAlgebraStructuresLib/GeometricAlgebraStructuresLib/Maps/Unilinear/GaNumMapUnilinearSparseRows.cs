// using System;
// using System.Collections.Generic;
// using GeometricAlgebraNumericsLib.Exceptions;
// using GeometricAlgebraNumericsLib.Multivectors.Numeric;
// using GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories;
//
// namespace GeometricAlgebraStructuresLib.Maps.Unilinear
// {
//     public sealed class GaNumMapUnilinearSparseRows : GaNumMapUnilinear
//     {
//         public static GaNumMapUnilinearSparseRows Create(int targetVSpaceDim)
//         {
//             return new GaNumMapUnilinearSparseRows(
//                 targetVSpaceDim
//             );
//         }
//
//         public static GaNumMapUnilinearSparseRows Create(int domainVSpaceDim, int targetVSpaceDim)
//         {
//             return new GaNumMapUnilinearSparseRows(
//                 domainVSpaceDim,
//                 targetVSpaceDim
//             );
//         }
//
//
//         private readonly Dictionary<int, GaNumSarMultivector> _rowMultivectors
//             = new Dictionary<int, GaNumSarMultivector>();
//
//
//         public override int TargetVSpaceDimension { get; }
//
//         public override int DomainVSpaceDimension { get; }
//
//         public override IGaNumMultivector this[int id1]
//         {
//             get
//             {
//                 var resultMv = new GaNumSarMultivectorFactory(TargetVSpaceDimension);
//
//                 foreach (var pair in _rowMultivectors)
//                     if (pair.Value.TryGetValue(id1, out var value))
//                         resultMv.AddTerm(pair.Key, value);
//
//                 return resultMv.GetSarMultivector();
//             }
//         }
//
//         public override GaNumSarMultivector this[GaNumSarMultivector mv1]
//         {
//             get
//             {
//                 if (mv1.GaSpaceDimension != DomainGaSpaceDimension)
//                     throw new GaNumericsException("Multivector size mismatch");
//
//                 var resultMv = new GaNumSarMultivectorFactory(TargetVSpaceDimension);
//
//                 foreach (var pair in _rowMultivectors)
//                 {
//                     var value = pair.Value.RowColumnProduct(mv1);
//
//                     if (!value.IsNearZero())
//                         resultMv.AddTerm(pair.Key, value);
//                 }
//
//                 return resultMv.GetSarMultivector();
//             }
//         }
//
//         public override GaNumDgrMultivector this[GaNumDgrMultivector mv1]
//         {
//             get
//             {
//                 if (mv1.GaSpaceDimension != DomainGaSpaceDimension)
//                     throw new GaNumericsException("Multivector size mismatch");
//
//                 var resultMv = new GaNumDgrMultivectorFactory(TargetVSpaceDimension);
//
//                 foreach (var pair in _rowMultivectors)
//                 {
//                     var value = pair.Value.RowColumnProduct(mv1);
//
//                     if (!value.IsNearZero())
//                         resultMv.SetTerm(pair.Key, value);
//                 }
//
//                 return resultMv.GetDgrMultivector();
//             }
//         }
//
//
//         private GaNumMapUnilinearSparseRows(int targetVSpaceDim)
//         {
//             DomainVSpaceDimension = targetVSpaceDim;
//             TargetVSpaceDimension = targetVSpaceDim;
//         }
//
//         private GaNumMapUnilinearSparseRows(int domainVSpaceDim, int targetVSpaceDim)
//         {
//             DomainVSpaceDimension = domainVSpaceDim;
//             TargetVSpaceDimension = targetVSpaceDim;
//         }
//
//
//         public GaNumMapUnilinearSparseRows ClearRows()
//         {
//             _rowMultivectors.Clear();
//
//             return this;
//         }
//
//         public GaNumMapUnilinearSparseRows SetRow(int rowId, GaNumSarMultivector rowMv)
//         {
//             if (rowMv.IsNullOrEmpty())
//             {
//                 if (_rowMultivectors.ContainsKey(rowId))
//                     _rowMultivectors.Remove(rowId);
//
//                 return this;
//             }
//
//             if (rowMv.GaSpaceDimension != DomainGaSpaceDimension)
//                 throw new GaNumericsException("Multivector size mismatch");
//
//             if (_rowMultivectors.ContainsKey(rowId))
//                 _rowMultivectors[rowId] = rowMv;
//             else
//                 _rowMultivectors.Add(rowId, rowMv);
//
//             return this;
//         }
//
//         //public GaNumMapUnilinearSparseRows SetColumn(int columnId, IGaNumMultivector columnMv)
//         //{
//         //    if (columnMv.GaSpaceDimension != TargetGaSpaceDimension)
//         //        throw new GaNumericsException("Multivector size mismatch");
//
//         //    foreach (var columnTerm in columnMv.NonZeroTerms)
//         //    {
//         //        var rowId = columnTerm.BasisBladeId;
//
//         //        if (!_rowMultivectors.TryGetValue(rowId, out var rowMv))
//         //        {
//         //            rowMv = GaNumSarMultivector.CreateZero(DomainVSpaceDimension);
//         //            _rowMultivectors.Add(rowId, rowMv);
//         //        }
//
//         //        rowMv.SetTerm(columnId, columnTerm.ScalarValue);
//         //    }
//
//         //    return this;
//         //}
//
//         public GaNumMapUnilinearSparseRows RemoveRow(int id)
//         {
//             _rowMultivectors.Remove(id);
//
//             return this;
//         }
//
//
//         public override IEnumerable<Tuple<int, IGaNumMultivector>> BasisBladeMaps()
//         {
//             for (var id = 0; id < DomainGaSpaceDimension; id++)
//             {
//                 var mv = this[id];
//
//                 if (!mv.IsNullOrEmpty())
//                     yield return Tuple.Create(id, mv);
//             }
//         }
//     }
// }