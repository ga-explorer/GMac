// using System;
// using System.Collections.Generic;
// using System.Diagnostics;
// using GeometricAlgebraStructuresLib.Multivectors;
//
// namespace GeometricAlgebraStructuresLib.Maps.Unilinear
// {
//     public sealed class GaMapUnilinearArray<T> : GaMapUnilinear<T>
//     {
//         public static GaMapUnilinearArray<T> Create(int targetVSpaceDim)
//         {
//             return new GaMapUnilinearArray<T>(
//                 targetVSpaceDim
//             );
//         }
//
//         public static GaMapUnilinearArray<> Create(int domainVSpaceDim, int targetVSpaceDim)
//         {
//             return new GaMapUnilinearArray<>(
//                 domainVSpaceDim,
//                 targetVSpaceDim
//             );
//         }
//
//         public static GaMapUnilinearArray<T> Create(IGaMapUnilinear<T> linearMap, IEnumerable<int> idsList)
//         {
//             var table = new GaMapUnilinearArray<T>(
//                 linearMap.DomainVSpaceDimension,
//                 linearMap.TargetVSpaceDimension
//             );
//
//             foreach (var id1 in idsList)
//             {
//                 var resultMv = linearMap[id1];
//
//                 if (!resultMv.IsZero())
//                     table._basisBladeMaps[id1] = resultMv;
//             }
//
//             return table;
//         }
//
//
//         private readonly IGaMultivector<T>[] _basisBladeMaps;
//
//
//         public override int DomainVSpaceDimension { get; }
//
//         public override int TargetVSpaceDimension { get; }
//
//         public override IGaMultivector<T> this[int id1] 
//             => _basisBladeMaps[id1]
//                 ?? GaNumTerm.CreateZero(TargetGaSpaceDimension);
//
//         public override IGaMultivector<T> this[IGaMultivector<T> mv]
//         {
//             get
//             {
//                 if (mv.GaSpaceDimension != DomainGaSpaceDimension)
//                     throw new GaNumericsException("Multivector size mismatch");
//
//                 var resultMv = new GaNumSarMultivectorFactory(TargetVSpaceDimension);
//
//                 foreach (var term1 in mv.Storage.GetNonZeroTerms())
//                 {
//                     var basisBladeMv = _basisBladeMaps[term1.Id];
//                     if (ReferenceEquals(basisBladeMv, null))
//                         continue;
//
//                     resultMv.AddTerms(term1.Scalar, basisBladeMv);
//                 }
//
//                 return resultMv.GetSarMultivector();
//             }
//         }
//
//         public override GaNumDgrMultivector this[GaNumDgrMultivector mv]
//         {
//             get
//             {
//                 if (mv.GaSpaceDimension != DomainGaSpaceDimension)
//                     throw new GaNumericsException("Multivector size mismatch");
//
//                 var resultMv = new GaNumDgrMultivectorFactory(TargetVSpaceDimension);
//
//                 foreach (var term1 in mv.GetNonZeroTerms())
//                 {
//                     var basisBladeMv = _basisBladeMaps[term1.BasisBladeId];
//                     if (ReferenceEquals(basisBladeMv, null))
//                         continue;
//
//                     resultMv.AddTerms(term1.ScalarValue, basisBladeMv);
//                 }
//
//                 return resultMv.GetDgrMultivector();
//             }
//         }
//
//
//         private GaMapUnilinearArray(int targetVSpaceDim)
//         {
//             DomainVSpaceDimension = targetVSpaceDim;
//             TargetVSpaceDimension = targetVSpaceDim;
//
//             _basisBladeMaps = new IGaNumMultivector[DomainGaSpaceDimension];
//         }
//
//         private GaMapUnilinearArray(int domainVSpaceDim, int targetVSpaceDim)
//         {
//             DomainVSpaceDimension = domainVSpaceDim;
//             TargetVSpaceDimension = targetVSpaceDim;
//
//             _basisBladeMaps = new IGaNumMultivector[DomainGaSpaceDimension];
//         }
//
//
//         public GaMapUnilinearArray<T> ClearBasisBladesMaps()
//         {
//             for (var id = 0; id < DomainGaSpaceDimension; id++)
//                 _basisBladeMaps[id] = null;
//
//             return this;
//         }
//
//         public GaMapUnilinearArray<T> SetBasisBladeMap(int basisBladeId, IGaMultivector<T> targetMv)
//         {
//             Debug.Assert(ReferenceEquals(targetMv, null) || targetMv.VSpaceDimension == TargetVSpaceDimension);
//
//             _basisBladeMaps[basisBladeId] = targetMv.Compactify(true);
//
//             return this;
//         }
//
//         public GaMapUnilinearArray<T> RemoveBasisBladesMap(int id1)
//         {
//             _basisBladeMaps[id1] = null;
//
//             return this;
//         }
//
//
//         public override IEnumerable<Tuple<int, IGaMultivector<T>>> BasisBladeMaps()
//         {
//             for (var id = 0; id < DomainGaSpaceDimension; id++)
//             {
//                 var basisBladeMv = _basisBladeMaps[id];
//                 if (!ReferenceEquals(basisBladeMv, null))
//                     yield return new Tuple<int, IGaMultivector<T>>(id, basisBladeMv);
//             }
//         }
//     }
// }
