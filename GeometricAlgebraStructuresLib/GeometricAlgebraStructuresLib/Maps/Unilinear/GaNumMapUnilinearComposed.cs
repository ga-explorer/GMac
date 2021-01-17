// using System;
// using System.Collections.Generic;
// using System.Linq;
// using GeometricAlgebraNumericsLib.Exceptions;
// using GeometricAlgebraNumericsLib.Multivectors.Numeric;
//
// namespace GeometricAlgebraStructuresLib.Maps.Unilinear
// {
//     public sealed class GaNumMapUnilinearComposed : GaNumMapUnilinear
//     {
//         public static GaNumMapUnilinearComposed CreateIdentity(int vSpaceDim)
//         {
//             return new GaNumMapUnilinearComposed(vSpaceDim);
//         }
//
//         public static GaNumMapUnilinearComposed Create(IGaNumMapUnilinear firstMappings)
//         {
//             return new GaNumMapUnilinearComposed(firstMappings);
//         }
//
//         public static GaNumMapUnilinearComposed Create(params IGaNumMapUnilinear[] mappingsList)
//         {
//             var resultMap = new GaNumMapUnilinearComposed(mappingsList[0]);
//
//             for (var i = 1; i < mappingsList.Length; i++)
//                 resultMap.AddMapping(mappingsList[i]);
//
//             return resultMap;
//         }
//
//         public static GaNumMapUnilinearComposed Create(IEnumerable<IGaNumMapUnilinear> mappingsList)
//         {
//             var mappingsArray = mappingsList.ToArray();
//             var resultMap = new GaNumMapUnilinearComposed(mappingsArray[0]);
//
//             for (var i = 1; i < mappingsArray.Length; i++)
//                 resultMap.AddMapping(mappingsArray[i]);
//
//             return resultMap;
//         }
//
//
//         private readonly List<IGaNumMapUnilinear> _mappingsList
//             = new List<IGaNumMapUnilinear>();
//
//
//         public override int TargetVSpaceDimension
//             => _mappingsList[_mappingsList.Count - 1].TargetVSpaceDimension;
//
//         public override int DomainVSpaceDimension
//             => _mappingsList[0].DomainVSpaceDimension;
//
//         public IEnumerable<IGaNumMapUnilinear> Mappings
//             => _mappingsList;
//
//         public override IGaNumMultivector this[int id1]
//         {
//             get
//             {
//                 var resultMv = _mappingsList[0][id1].GetSarMultivector();
//
//                 for (var i = 1; i < _mappingsList.Count; i++)
//                     resultMv = _mappingsList[i][resultMv];
//
//                 return resultMv;
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
//                 var resultMv = _mappingsList[0][mv];
//
//                 for (var i = 1; i < _mappingsList.Count - 1; i++)
//                     resultMv = _mappingsList[i][resultMv];
//
//                 return _mappingsList[_mappingsList.Count - 1][resultMv];
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
//                 var resultMv = _mappingsList[0][mv];
//
//                 for (var i = 1; i < _mappingsList.Count - 1; i++)
//                     resultMv = _mappingsList[i][resultMv];
//
//                 return _mappingsList[_mappingsList.Count - 1][resultMv];
//             }
//         }
//
//
//         private GaNumMapUnilinearComposed(int vSpaceDim)
//         {
//             _mappingsList.Add(GaNumMapUnilinearIdentity.Create(vSpaceDim));
//         }
//
//         private GaNumMapUnilinearComposed(IGaNumMapUnilinear firstMapping)
//         {
//             _mappingsList.Add(firstMapping);
//         }
//
//
//         public GaNumMapUnilinearComposed ResetToIdentity(int vSpaceDim)
//         {
//             _mappingsList.Clear();
//             _mappingsList.Add(GaNumMapUnilinearIdentity.Create(vSpaceDim));
//
//             return this;
//         }
//
//         public GaNumMapUnilinearComposed ResetToMapping(IGaNumMapUnilinear firstMapping)
//         {
//             _mappingsList.Clear();
//             _mappingsList.Add(firstMapping);
//
//             return this;
//         }
//
//         public GaNumMapUnilinearComposed AddMappings(params IGaNumMapUnilinear[] mappingsArray)
//         {
//             foreach (var map in mappingsArray)
//                 AddMapping(map);
//
//             return this;
//         }
//
//         public GaNumMapUnilinearComposed AddMappings(IEnumerable<IGaNumMapUnilinear> mappingsList)
//         {
//             foreach (var map in mappingsList)
//                 AddMapping(map);
//
//             return this;
//         }
//
//         public GaNumMapUnilinearComposed AddMapping(IGaNumMapUnilinear map)
//         {
//             if (_mappingsList.Count > 0 && TargetVSpaceDimension != map.DomainVSpaceDimension)
//                 throw new InvalidOperationException("Mapping space dimensions mismatch");
//
//             _mappingsList.Add(map);
//
//             return this;
//         }
//
//
//         public override IEnumerable<Tuple<int, IGaNumMultivector>> BasisBladeMaps()
//         {
//             return
//                 Enumerable
//                 .Range(0, DomainGaSpaceDimension)
//                 .Select(id => new Tuple<int, IGaNumMultivector>(id, this[id]))
//                 .Where(t => !t.Item2.IsNullOrEmpty());
//         }
//     }
// }
