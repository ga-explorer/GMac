// using System;
// using System.Collections.Generic;
// using System.Linq;
// using GeometricAlgebraNumericsLib.Exceptions;
// using GeometricAlgebraNumericsLib.Multivectors.Numeric;
// using GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories;
//
// namespace GeometricAlgebraStructuresLib.Maps.Unilinear
// {
//     /// <summary>
//     /// A linear map expressed as a linear combination of other linear maps
//     /// </summary>
//     public sealed class GaNumMapUnilinearCombined : GaNumMapUnilinear
//     {
//         private sealed class GaNumMapUnilinearCombinedTerm
//         {
//             public double Coef { get; }
//
//             public IGaNumMapUnilinear LinearMap { get; }
//
//
//             internal GaNumMapUnilinearCombinedTerm(double coef, IGaNumMapUnilinear linearMap)
//             {
//                 Coef = coef;
//                 LinearMap = linearMap;
//             }
//         }
//
//
//         public static GaNumMapUnilinearCombined Create(int targetVSpaceDim)
//         {
//             return new GaNumMapUnilinearCombined(
//                 targetVSpaceDim
//             );
//         }
//
//         public static GaNumMapUnilinearCombined Create(int domainVSpaceDim, int targetVSpaceDim)
//         {
//             return new GaNumMapUnilinearCombined(
//                 domainVSpaceDim,
//                 targetVSpaceDim
//             );
//         }
//
//
//         private readonly List<GaNumMapUnilinearCombinedTerm> _mappingsList
//             = new List<GaNumMapUnilinearCombinedTerm>();
//
//
//         public override int DomainVSpaceDimension { get; }
//
//         public override int TargetVSpaceDimension { get; }
//
//         public override IGaNumMultivector this[int id1]
//         {
//             get
//             {
//                 var resultMv = new GaNumSarMultivectorFactory(TargetVSpaceDimension);
//
//                 foreach (var term in _mappingsList)
//                 {
//                     var mv = term.LinearMap[id1];
//
//                     foreach (var mvTerm in mv.GetNonZeroTerms())
//                         resultMv.AddTerm(
//                             mvTerm.BasisBladeId, 
//                             mvTerm.ScalarValue * term.Coef
//                         );
//                 }
//
//                 return resultMv.GetSarMultivector();
//             }
//         }
//
//         public override GaNumSarMultivector this[GaNumSarMultivector mv]
//         {
//             get
//             {
//                 if (mv.GaSpaceDimension != DomainGaSpaceDimension)
//                     throw new GaNumericsException("Multivector size mismatch");
//
//                 var tempMv = new GaNumSarMultivectorFactory(TargetVSpaceDimension);
//
//                 foreach (var term in _mappingsList)
//                     tempMv.AddTerms(
//                         term.Coef,
//                         term.LinearMap[mv]
//                     );
//
//                 return tempMv.GetSarMultivector();
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
//                 var tempMv = new GaNumDgrMultivectorFactory(TargetVSpaceDimension);
//
//                 foreach (var term in _mappingsList)
//                     tempMv.AddTerms(
//                         term.Coef,
//                         term.LinearMap[mv]
//                     );
//
//                 return tempMv.GetDgrMultivector();
//             }
//         }
//
//         private GaNumMapUnilinearCombined(int targetVSpaceDim)
//         {
//             DomainVSpaceDimension = targetVSpaceDim;
//             TargetVSpaceDimension = targetVSpaceDim;
//         }
//
//         private GaNumMapUnilinearCombined(int domainVSpaceDim, int targetVSpaceDim)
//         {
//             DomainVSpaceDimension = domainVSpaceDim;
//             TargetVSpaceDimension = targetVSpaceDim;
//         }
//
//
//         public GaNumMapUnilinearCombined ClearMaps()
//         {
//             _mappingsList.Clear();
//
//             return this;
//         }
//
//         public GaNumMapUnilinearCombined AddMap(double coef, IGaNumMapUnilinear linearMap)
//         {
//             if (
//                 linearMap.DomainVSpaceDimension != DomainVSpaceDimension ||
//                 linearMap.TargetVSpaceDimension != TargetVSpaceDimension
//             )
//                 throw new InvalidOperationException("Linear map dimensions mismatch");
//
//             _mappingsList.Add(new GaNumMapUnilinearCombinedTerm(coef, linearMap));
//
//             return this;
//         }
//
//
//         public override IEnumerable<Tuple<int, IGaNumMultivector>> BasisBladeMaps()
//         {
//             return
//                 Enumerable
//                     .Range(0, DomainGaSpaceDimension)
//                     .Select(id => new Tuple<int, IGaNumMultivector>(id, this[id]))
//                     .Where(t => !t.Item2.IsNullOrEmpty());
//         }
//     }
// }
