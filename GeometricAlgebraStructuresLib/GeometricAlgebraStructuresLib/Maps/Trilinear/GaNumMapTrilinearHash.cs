// using System;
// using System.Collections.Generic;
// using System.Diagnostics;
// using GeometricAlgebraNumericsLib.Exceptions;
// using GeometricAlgebraNumericsLib.Multivectors.Numeric;
// using GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories;
// using GeometricAlgebraNumericsLib.Structures.Collections;
//
// namespace GeometricAlgebraStructuresLib.Maps.Trilinear
// {
//     public sealed class GaNumMapTrilinearHash : GaNumMapTrilinear
//     {
//         public static GaNumMapTrilinearHash Create(int targetVSpaceDim)
//         {
//             return new GaNumMapTrilinearHash(
//                 targetVSpaceDim
//             );
//         }
//
//         public static GaNumMapTrilinearHash Create(int domainVSpaceDim, int targetVSpaceDim)
//         {
//             return new GaNumMapTrilinearHash(
//                 domainVSpaceDim,
//                 targetVSpaceDim
//             );
//         }
//
//
//         private readonly GaNumMultivectorHashTable3D _basisBladesMaps
//             = new GaNumMultivectorHashTable3D();
//
//
//         public override int TargetVSpaceDimension { get; }
//
//         public override int DomainVSpaceDimension { get; }
//
//         public override IGaNumMultivector this[int id1, int id2, int id3]
//         {
//             get
//             {
//                 return
//                     !_basisBladesMaps.TryGetValue(id1, id2, id3, out var basisBladeMv) || ReferenceEquals(basisBladeMv, null)
//                         ? GaNumSarMultivector.CreateZero(TargetVSpaceDimension)
//                         : basisBladeMv;
//             }
//         }
//
//
//         private GaNumMapTrilinearHash(int targetVSpaceDim)
//         {
//             DomainVSpaceDimension = targetVSpaceDim;
//             TargetVSpaceDimension = targetVSpaceDim;
//         }
//
//         private GaNumMapTrilinearHash(int domainVSpaceDim, int targetVSpaceDim)
//         {
//             DomainVSpaceDimension = domainVSpaceDim;
//             TargetVSpaceDimension = targetVSpaceDim;
//         }
//
//
//         public GaNumMapTrilinearHash ClearBasisBladesMaps()
//         {
//             _basisBladesMaps.Clear();
//             return this;
//         }
//
//         public GaNumMapTrilinearHash SetBasisBladesMap(int id1, int id2, int id3, IGaNumMultivector value)
//         {
//             Debug.Assert(ReferenceEquals(value, null) || value.VSpaceDimension == TargetVSpaceDimension);
//
//             _basisBladesMaps[id1, id2, id3] = value.Compactify(true);
//
//             return this;
//         }
//
//         public GaNumMapTrilinearHash RemoveBasisBladesMap(int id1, int id2, int id3)
//         {
//             _basisBladesMaps.Remove(id1, id2, id3);
//             return this;
//         }
//
//
//         public override GaNumSarMultivector this[GaNumSarMultivector mv1, GaNumSarMultivector mv2, GaNumSarMultivector mv3]
//         {
//             get
//             {
//                 if (mv1.GaSpaceDimension != DomainGaSpaceDimension || mv2.GaSpaceDimension != DomainGaSpaceDimension ||
//                     mv3.GaSpaceDimension != DomainGaSpaceDimension)
//                     throw new GaNumericsException("Multivector size mismatch");
//
//                 var tempMv = new GaNumSarMultivectorFactory(TargetVSpaceDimension);
//
//                 foreach (var term1 in mv1.GetNonZeroTerms())
//                 {
//                     var id1 = term1.BasisBladeId;
//                     var coef1 = term1.ScalarValue;
//
//                     foreach (var term2 in mv2.GetNonZeroTerms())
//                     {
//                         var id2 = term2.BasisBladeId;
//                         var coef2 = term2.ScalarValue;
//
//                         foreach (var term3 in mv3.GetNonZeroTerms())
//                         {
//                             var id3 = term3.BasisBladeId;
//                             var coef3 = term3.ScalarValue;
//
//                             if (!_basisBladesMaps.TryGetValue(id1, id2, id3, out var basisBladeMv) ||
//                                 basisBladeMv == null)
//                                 continue;
//
//                             foreach (var basisBladeMvTerm in basisBladeMv.GetNonZeroTerms())
//                                 tempMv.AddTerm(
//                                     basisBladeMvTerm.BasisBladeId,
//                                     basisBladeMvTerm.ScalarValue * coef1 * coef2 * coef3
//                                 );
//                         }
//                     }
//                 }
//
//                 return tempMv.GetSarMultivector();
//             }
//         }
//
//         public override IEnumerable<Tuple<int, int, int, IGaNumMultivector>> BasisBladesMaps()
//         {
//             foreach (var pair in _basisBladesMaps)
//             {
//                 var id1 = pair.Key.Item1;
//                 var id2 = pair.Key.Item2;
//                 var id3 = pair.Key.Item3;
//                 var mv = pair.Value;
//
//                 if (!mv.IsNullOrEmpty())
//                     yield return new Tuple<int, int, int, IGaNumMultivector>(id1, id2, id3, mv);
//             }
//         }
//     }
// }