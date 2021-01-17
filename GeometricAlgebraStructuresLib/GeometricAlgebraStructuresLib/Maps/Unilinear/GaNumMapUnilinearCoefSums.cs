// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using GeometricAlgebraNumericsLib.Exceptions;
// using GeometricAlgebraNumericsLib.Multivectors.Numeric;
// using GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories;
// using GeometricAlgebraNumericsLib.Structures;
// using GeometricAlgebraStructuresLib.Frames;
// using TextComposerLib.Text.Structured;
//
// namespace GeometricAlgebraStructuresLib.Maps.Unilinear
// {
//     public sealed class GaNumMapUnilinearCoefSums : GaNumMapUnilinear
//     {
//         private sealed class GaNumMapUnilinearCoefSumsTerm : IEnumerable<Tuple<int, double>>
//         {
//             private readonly List<Tuple<int, double>> _factorsList
//                 = new List<Tuple<int, double>>();
//
//
//             public int TargetBasisBladeId { get; private set; }
//
//             public IEnumerable<string> TermsText
//             {
//                 get
//                 {
//                     var termBuilder = new StringBuilder();
//
//                     foreach (var pair in _factorsList)
//                         yield return termBuilder
//                             .Clear()
//                             .Append(pair.Item2)
//                             .Append(" * (")
//                             .Append(pair.Item1.BasisBladeIndexedName())
//                             .Append(")")
//                             .ToString();
//                 }
//             }
//
//             public double this[int domainBasisBladeId]
//                 => _factorsList
//                        .FirstOrDefault(
//                            factor =>
//                                factor.Item1 == domainBasisBladeId
//                        )?.Item2 ?? 0.0d;
//
//             public double this[IGaNumMultivector mv1]
//                 => _factorsList.Count == 0
//                     ? 0.0d
//                     : _factorsList
//                         .Select(factor => mv1[factor.Item1] * factor.Item2)
//                         .Sum();
//
//
//             internal GaNumMapUnilinearCoefSumsTerm(int targetBasisBladeId)
//             {
//                 TargetBasisBladeId = targetBasisBladeId;
//             }
//
//
//             internal GaNumMapUnilinearCoefSumsTerm ClearFactors()
//             {
//                 _factorsList.Clear();
//
//                 return this;
//             }
//
//             internal GaNumMapUnilinearCoefSumsTerm AddFactor(int domainBasisBladeId, bool isNegative = false)
//             {
//                 _factorsList.Add(
//                     Tuple.Create(
//                         domainBasisBladeId,
//                         isNegative ? -1.0d : 1.0d
//                     ));
//
//                 return this;
//             }
//
//             internal GaNumMapUnilinearCoefSumsTerm AddFactor(int domainBasisBladeId, double factorValue)
//             {
//                 _factorsList.Add(
//                     Tuple.Create(
//                         domainBasisBladeId,
//                         factorValue
//                     ));
//
//                 return this;
//             }
//
//             internal GaNumMapUnilinearCoefSumsTerm RemoveFactor(int domainBasisBladeId)
//             {
//                 var index = _factorsList.FindIndex(
//                     factor =>
//                         factor.Item1 == domainBasisBladeId
//                 );
//
//                 if (index >= 0)
//                     _factorsList.RemoveAt(index);
//
//                 return this;
//             }
//
//             public IEnumerator<Tuple<int, double>> GetEnumerator()
//             {
//                 return _factorsList.GetEnumerator();
//             }
//
//             IEnumerator IEnumerable.GetEnumerator()
//             {
//                 return _factorsList.GetEnumerator();
//             }
//
//
//             public override string ToString()
//             {
//                 var composer = new ListTextComposer(" + ")
//                 {
//                     FinalPrefix = TargetBasisBladeId.BasisBladeIndexedName() + ": { ",
//                     FinalSuffix = " }"
//                 };
//
//                 composer.AddRange(TermsText);
//
//                 return composer.ToString();
//             }
//         }
//
//
//         public static GaNumMapUnilinearCoefSums Create(int targetVSpaceDim)
//         {
//             return new GaNumMapUnilinearCoefSums(
//                 targetVSpaceDim
//             );
//         }
//
//         public static GaNumMapUnilinearCoefSums Create(int domainVSpaceDim, int targetVSpaceDim)
//         {
//             return new GaNumMapUnilinearCoefSums(
//                 domainVSpaceDim,
//                 targetVSpaceDim
//             );
//         }
//         
//
//         private readonly GaSparseTable1D<int, GaNumMapUnilinearCoefSumsTerm> _coefSumsTable
//             = new GaSparseTable1D<int, GaNumMapUnilinearCoefSumsTerm>();
//
//
//         public override int TargetVSpaceDimension { get; }
//
//         public override int DomainVSpaceDimension { get; }
//
//         public override IGaNumMultivector this[int domainBasisBladeId]
//         {
//             get
//             {
//                 var resultMv = new GaNumSarMultivectorFactory(TargetVSpaceDimension);
//
//                 foreach (var terms in _coefSumsTable.Values)
//                     resultMv.AddTerm(
//                         terms.TargetBasisBladeId,
//                         terms[domainBasisBladeId]
//                     );
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
//                 foreach (var terms in _coefSumsTable.Values)
//                     tempMv.AddTerm(
//                         terms.TargetBasisBladeId,
//                         terms[mv]
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
//                 var tempMv = new GaNumDgrMultivectorFactory(TargetGaSpaceDimension);
//
//                 foreach (var terms in _coefSumsTable.Values)
//                     tempMv.AddTerm(
//                         terms.TargetBasisBladeId,
//                         terms[mv]
//                     );
//
//                 return tempMv.GetDgrMultivector();
//             }
//         }
//
//         private GaNumMapUnilinearCoefSums(int targetVSpaceDim)
//         {
//             DomainVSpaceDimension = targetVSpaceDim;
//             TargetVSpaceDimension = targetVSpaceDim;
//         }
//
//         private GaNumMapUnilinearCoefSums(int domainVSpaceDim, int targetVSpaceDim)
//         {
//             DomainVSpaceDimension = domainVSpaceDim;
//             TargetVSpaceDimension = targetVSpaceDim;
//         }
//
//
//         public GaNumMapUnilinearCoefSums SetBasisBladeMap(int domainBasisBladeId, IGaNumMultivector targetMv)
//         {
//             if (ReferenceEquals(targetMv, null))
//                 return this;
//
//             foreach (var term in targetMv.GetNonZeroTerms())
//                 SetFactor(term.BasisBladeId, domainBasisBladeId, term.ScalarValue);
//
//             return this;
//         }
//
//         public GaNumMapUnilinearCoefSums SetFactor(int targetBasisBladeId, int domainBasisBladeId, bool isNegative = false)
//         {
//             if (!_coefSumsTable.TryGetValue(targetBasisBladeId, out var sum))
//             {
//                 sum = new GaNumMapUnilinearCoefSumsTerm(targetBasisBladeId);
//                 _coefSumsTable[targetBasisBladeId] = sum;
//             }
//
//             sum.AddFactor(domainBasisBladeId, isNegative);
//
//             return this;
//         }
//
//         public GaNumMapUnilinearCoefSums SetFactor(int targetBasisBladeId, int domainBasisBladeId, double factorValue)
//         {
//             if (!_coefSumsTable.TryGetValue(targetBasisBladeId, out var sum))
//             {
//                 sum = new GaNumMapUnilinearCoefSumsTerm(targetBasisBladeId);
//                 _coefSumsTable[targetBasisBladeId] = sum;
//             }
//
//             sum.AddFactor(domainBasisBladeId, factorValue);
//
//             return this;
//         }
//
//         public GaNumMapUnilinearCoefSums RemoveFactor(int targetBasisBladeId, int domainBasisBladeId)
//         {
//             if (!_coefSumsTable.TryGetValue(targetBasisBladeId, out var sum))
//                 return this;
//
//             sum.RemoveFactor(domainBasisBladeId);
//
//             return this;
//         }
//
//         public GaNumMapUnilinearCoefSums RemoveFactors(int targetBasisBladeId)
//         {
//             _coefSumsTable.Remove(targetBasisBladeId);
//
//             return this;
//         }
//
//         
//         public override IEnumerable<Tuple<int, IGaNumMultivector>> BasisBladeMaps()
//         {
//             for (var id1 = 0; id1 < DomainGaSpaceDimension; id1++)
//             {
//                 var mv = this[id1];
//
//                 if (!mv.IsNullOrEmpty())
//                     yield return new Tuple<int, IGaNumMultivector>(id1, mv);
//             }
//         }
//     }
// }
