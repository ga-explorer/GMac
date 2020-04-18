using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DataStructuresLib;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.ExprFactory;
using GeometricAlgebraSymbolicsLib.Exceptions;
using GeometricAlgebraSymbolicsLib.Frames;
using GeometricAlgebraSymbolicsLib.Maps.Bilinear;
using GeometricAlgebraSymbolicsLib.Maps.Unilinear;
using GeometricAlgebraSymbolicsLib.Multivectors;
using GeometricAlgebraSymbolicsLib.Multivectors.Intermediate;
using GeometricAlgebraSymbolicsLib.Products;
using Wolfram.NETLink;

namespace GeometricAlgebraSymbolicsLib.Maps
{
    public static class GaSymMapUtils
    {
        #region Unilinear Maps
        public static Dictionary<int, IGaSymMultivector> ToDictionary(this IGaSymMapUnilinear linearMap)
        {
            return linearMap
                .BasisBladeMaps()
                .ToDictionary(t => t.Item1, t => t.Item2);
        }

        public static IEnumerable<IGaSymMultivector> ToColumnsList(this IGaSymMapUnilinear linearMap)
        {
            return Enumerable
                .Range(0, linearMap.DomainGaSpaceDimension)
                .Select(id => linearMap[id]);
        }

        public static IEnumerable<MathematicaScalar[]> ToScalarColumnsList(this IGaSymMapUnilinear linearMap)
        {
            return Enumerable
                .Range(0, linearMap.DomainGaSpaceDimension)
                .Select(id => linearMap[id].TermsToArray());
        }

        public static IEnumerable<Expr[]> ToExprScalarColumnsList(this IGaSymMapUnilinear linearMap)
        {
            return Enumerable
                .Range(0, linearMap.DomainGaSpaceDimension)
                .Select(id => linearMap[id].TermsToExprArray());
        }

        public static Expr[,] ToExprScalarsArray(this IGaSymMapUnilinear linearMap)
        {
            var resultArray = new Expr[
                linearMap.TargetGaSpaceDimension, 
                linearMap.DomainGaSpaceDimension
            ];

            foreach (var basisBladeMap in linearMap.BasisBladeMaps())
            {
                var col = basisBladeMap.Item1;

                foreach (var term in basisBladeMap.Item2.NonZeroExprTerms)
                {
                    var row = term.Key;

                    resultArray[row, col] = term.Value;
                }
            }

            return resultArray;
        }

        public static MathematicaScalar[,] ToScalarsArray(this IGaSymMapUnilinear linearMap)
        {
            var resultArray = new MathematicaScalar[
                linearMap.TargetGaSpaceDimension,
                linearMap.DomainGaSpaceDimension
            ];

            foreach (var basisBladeMap in linearMap.BasisBladeMaps())
            {
                var col = basisBladeMap.Item1;

                foreach (var term in basisBladeMap.Item2.NonZeroExprTerms)
                {
                    var row = term.Key;

                    resultArray[row, col] = term.Value.ToMathematicaScalar();
                }
            }

            return resultArray;
        }

        public static MathematicaMatrix ToMatrix(this IGaSymMapUnilinear linearMap)
        {
            return MathematicaMatrix.CreateFullMatrix(
                GaSymbolicsUtils.Cas,
                ToExprScalarsArray(linearMap)
                );
        }


        /// <summary>
        /// Extract the basis vectors mappings from the given linear map and convert them into a
        /// linear map on the base vector space of the frame. This should only be used if basis vectors
        /// of the domain are mapped to vectors of the target domain.
        /// </summary>
        /// <param name="linearMap"></param>
        /// <returns></returns>
        public static Expr[,] BasisVectorMapsToExprScalarsArray(this IGaSymMapUnilinear linearMap)
        {
            var resultArray = new Expr[
                linearMap.TargetVSpaceDimension,
                linearMap.DomainVSpaceDimension
            ];

            foreach (var basisVectorMap in linearMap.BasisVectorMaps())
            {
                var col = basisVectorMap.Item1;
                var terms = 
                    basisVectorMap
                        .Item2
                        .NonZeroExprTerms
                        .Where(t => t.Key.IsValidBasisVectorId());

                foreach (var term in terms)
                {
                    var row = term.Key;

                    resultArray[row, col] = term.Value;
                }
            }

            return resultArray;
        }

        /// <summary>
        /// Extract the basis vectors mappings from the given linear map and convert them into a
        /// linear map on the base vector space of the frame. This should only be used if basis vectors
        /// of the domain are mapped to vectors of the target domain.
        /// </summary>
        /// <param name="linearMap"></param>
        /// <returns></returns>
        public static MathematicaMatrix BasisVectorMapsToSymbolicMatrix(this IGaSymMapUnilinear linearMap)
        {
            return MathematicaMatrix.CreateFullMatrix(
                GaSymbolicsUtils.Cas,
                BasisVectorMapsToExprScalarsArray(linearMap)
            );
        }


        public static IGaSymMultivectorTemp MapToTemp(this ISymbolicMatrix mappingMatrix, GaSymMultivector mv1)
        {
            if (mv1.GaSpaceDimension != mappingMatrix.ColumnCount)
                throw new GaSymbolicsException("Multivector size mismatch");

            var tempMv = GaSymMultivector.CreateZeroTemp(mappingMatrix.RowCount);

            foreach (var term in mv1.NonZeroExprTerms)
            {
                var id = term.Key;
                var coef = term.Value;

                for (var row = 0; row < mappingMatrix.RowCount; row++)
                    tempMv.AddFactor(
                        row, 
                        Mfs.Times[coef, mappingMatrix[row, id].Expression]
                    );
            }

            return tempMv;
        }


        public static GaSymMapUnilinearHash OddVersorProductToHashMap(this GaSymFrame frame, GaSymMultivector oddVersor)
        {
            return frame
                .OddVersorProduct(oddVersor, frame.BasisVectorIDs())
                .ToOutermorphismDictionary()
                .ToHashMap(frame.VSpaceDimension, frame.VSpaceDimension);
        }

        public static GaSymMapUnilinearTree OddVersorProductToTreeMap(this GaSymFrame frame, GaSymMultivector oddVersor)
        {
            return frame
                .OddVersorProduct(oddVersor, frame.BasisVectorIDs())
                .ToOutermorphismDictionary()
                .ToTreeMap(frame.VSpaceDimension, frame.VSpaceDimension);
        }

        public static GaSymMapUnilinearArray OddVersorProductToArrayMap(this GaSymFrame frame, GaSymMultivector oddVersor)
        {
            return frame
                .OddVersorProduct(oddVersor, frame.BasisVectorIDs())
                .ToOutermorphismDictionary()
                .ToArrayMap(frame.VSpaceDimension, frame.VSpaceDimension);
        }

        public static GaSymMapUnilinearCoefSums OddVersorProductToCoefSumsMap(this GaSymFrame frame, GaSymMultivector oddVersor)
        {
            return frame
                .OddVersorProduct(oddVersor, frame.BasisVectorIDs())
                .ToOutermorphismDictionary()
                .ToCoefSumsMap(frame.VSpaceDimension, frame.VSpaceDimension);
        }


        public static GaSymMapUnilinearHash EvenVersorProductToHashMap(this GaSymFrame frame, GaSymMultivector evenVersor)
        {
            return frame
                .EvenVersorProduct(evenVersor, frame.BasisVectorIDs())
                .ToOutermorphismDictionary()
                .ToHashMap(frame.VSpaceDimension, frame.VSpaceDimension);
        }

        public static GaSymMapUnilinearTree EvenVersorProductToTreeMap(this GaSymFrame frame, GaSymMultivector evenVersor)
        {
            return frame
                .EvenVersorProduct(evenVersor, frame.BasisVectorIDs())
                .ToOutermorphismDictionary()
                .ToTreeMap(frame.VSpaceDimension, frame.VSpaceDimension);
        }

        public static GaSymMapUnilinearArray EvenVersorProductToArrayMap(this GaSymFrame frame, GaSymMultivector evenVersor)
        {
            return frame
                .EvenVersorProduct(evenVersor, frame.BasisVectorIDs())
                .ToOutermorphismDictionary()
                .ToArrayMap(frame.VSpaceDimension, frame.VSpaceDimension);
        }

        public static GaSymMapUnilinearCoefSums EvenVersorProductToCoefSumsMap(this GaSymFrame frame, GaSymMultivector evenVersor)
        {
            return frame
                .EvenVersorProduct(evenVersor, frame.BasisVectorIDs())
                .ToOutermorphismDictionary()
                .ToCoefSumsMap(frame.VSpaceDimension, frame.VSpaceDimension);
        }


        public static GaSymMapUnilinearHash RotorProductToHashMap(this GaSymFrame frame, GaSymMultivector rotorVersor)
        {
            return frame
                .RotorProduct(rotorVersor, frame.BasisVectorIDs())
                .ToOutermorphismDictionary()
                .ToHashMap(frame.VSpaceDimension, frame.VSpaceDimension);
        }

        public static GaSymMapUnilinearTree RotorProductToTreeMap(this GaSymFrame frame, GaSymMultivector rotorVersor)
        {
            return frame
                .RotorProduct(rotorVersor, frame.BasisVectorIDs())
                .ToOutermorphismDictionary()
                .ToTreeMap(frame.VSpaceDimension, frame.VSpaceDimension);
        }

        public static GaSymMapUnilinearArray RotorProductToArrayMap(this GaSymFrame frame, GaSymMultivector rotorVersor)
        {
            return frame
                .RotorProduct(rotorVersor, frame.BasisVectorIDs())
                .ToOutermorphismDictionary()
                .ToArrayMap(frame.VSpaceDimension, frame.VSpaceDimension);
        }

        public static GaSymMapUnilinearCoefSums RotorProductToCoefSumsMap(this GaSymFrame frame, GaSymMultivector rotorVersor)
        {
            return frame
                .RotorProduct(rotorVersor, frame.BasisVectorIDs())
                .ToOutermorphismDictionary()
                .ToCoefSumsMap(frame.VSpaceDimension, frame.VSpaceDimension);
        }


        public static Dictionary<int, GaSymMultivector> ToOutermorphismDictionary(this Dictionary<int, IGaSymMultivector> linearVectorMaps, int domainVSpaceDim, int targetVSpaceDim)
        {
            var domainGaSpaceDim =
                domainVSpaceDim.ToGaSpaceDimension();

            var targetGaSpaceDim =
                targetVSpaceDim.ToGaSpaceDimension();

            var omMapDict = new Dictionary<int, GaSymMultivector>();

            //Add unit scalar as the image of the 0-basis blade
            omMapDict.Add(0, GaSymMultivector.CreateUnitScalar(targetGaSpaceDim));

            for (var id = 1; id <= domainGaSpaceDim - 1; id++)
            {
                GaSymMultivector basisBladeImage;

                if (id.IsValidBasisVectorId())
                {
                    //Add images of vector basis blades
                    linearVectorMaps.TryGetValue(id.BasisBladeIndex(), out var mv);

                    basisBladeImage = mv?.GetVectorPart()
                                      ?? GaSymMultivector.CreateZero(targetGaSpaceDim);
                }
                else
                {
                    //Add images of a higher dimensional basis blade using the outer product
                    //of the images of two lower dimensional basis blades
                    id.SplitBySmallestBasicPattern(out var id1, out var id2);

                    basisBladeImage =
                        omMapDict[id1].Op(omMapDict[id2]);
                }

                if (!basisBladeImage.IsZero())
                    omMapDict.Add(id, basisBladeImage);
            }

            return omMapDict;
        }

        public static Dictionary<int, GaSymMultivector> ToOutermorphismDictionary(this IEnumerable<Expr[]> linearVectorMaps)
        {
            var linearVectorMapsArray = linearVectorMaps.ToArray();

            var domainGaSpaceDim =
                linearVectorMapsArray.Length.ToGaSpaceDimension();

            var targetGaSpaceDim =
                linearVectorMapsArray[0].Length;

            var omMapDict = new Dictionary<int, GaSymMultivector>();

            //Add unit scalar as the image of the 0-basis blade
            omMapDict.Add(0, GaSymMultivector.CreateUnitScalar(targetGaSpaceDim));

            for (var id = 1; id <= domainGaSpaceDim - 1; id++)
            {
                GaSymMultivector basisBladeImage;

                if (id.IsValidBasisVectorId())
                {
                    //Add images of vector basis blades
                    basisBladeImage = GaSymMultivector.CreateVectorFromScalars(
                        linearVectorMapsArray[id.BasisBladeIndex()]
                        );
                }
                else
                {
                    //Add images of a higher dimensional basis blade using the outer product
                    //of the images of two lower dimensional basis blades
                    id.SplitBySmallestBasicPattern(out var id1, out var id2);

                    basisBladeImage =
                        omMapDict[id1].Op(omMapDict[id2]);
                }

                if (!basisBladeImage.IsZero())
                    omMapDict.Add(id, basisBladeImage);
            }

            return omMapDict;
        }

        public static Dictionary<int, GaSymMultivector> ToOutermorphismDictionary(this Expr[,] linearVectorMapsArray)
        {
            var domainGaSpaceDim =
                linearVectorMapsArray.GetLength(1).ToGaSpaceDimension();

            var targetGaSpaceDim =
                linearVectorMapsArray.GetLength(0).ToGaSpaceDimension();

            var omMapDict = new Dictionary<int, GaSymMultivector>();

            //Add unit scalar as the image of the 0-basis blade
            omMapDict.Add(0, GaSymMultivector.CreateUnitScalar(targetGaSpaceDim));

            for (var id = 1; id <= domainGaSpaceDim - 1; id++)
            {
                GaSymMultivector basisBladeImage;

                if (id.IsValidBasisVectorId())
                {
                    //Add images of vector basis blades
                    basisBladeImage = GaSymMultivector.CreateVectorFromColumn(
                        linearVectorMapsArray,
                        id.BasisBladeIndex()
                    );
                }
                else
                {
                    //Add images of a higher dimensional basis blade using the outer product
                    //of the images of two lower dimensional basis blades
                    id.SplitBySmallestBasicPattern(out var id1, out var id2);

                    basisBladeImage =
                        omMapDict[id1].Op(omMapDict[id2]);
                }

                if (!basisBladeImage.IsZero())
                    omMapDict.Add(id, basisBladeImage);
            }

            return omMapDict;
        }

        public static Dictionary<int, GaSymMultivector> ToOutermorphismDictionary(this ISymbolicMatrix linearVectorMapMatrix)
        {
            var domainGaSpaceDim =
                linearVectorMapMatrix.ColumnCount.ToGaSpaceDimension();

            var targetGaSpaceDim =
                linearVectorMapMatrix.RowCount.ToGaSpaceDimension();

            var omMapDict = new Dictionary<int, GaSymMultivector>();

            //Add unit scalar as the image of the 0-basis blade
            omMapDict.Add(0, GaSymMultivector.CreateUnitScalar(targetGaSpaceDim));

            for (var id = 1; id <= domainGaSpaceDim - 1; id++)
            {
                GaSymMultivector basisBladeImage;

                if (id.IsValidBasisVectorId())
                {
                    //Add images of vector basis blades
                    basisBladeImage =
                        GaSymMultivector.CreateVectorFromColumn(
                            linearVectorMapMatrix,
                            id.BasisBladeIndex()
                        );
                }
                else
                {
                    //Add images of a higher dimensional basis blade using the outer product
                    //of the images of two lower dimensional basis blades
                    id.SplitBySmallestBasicPattern(out var id1, out var id2);

                    basisBladeImage =
                        omMapDict[id1].Op(omMapDict[id2]);
                }

                if (!basisBladeImage.IsZero())
                    omMapDict.Add(id, basisBladeImage);
            }

            return omMapDict;
        }

        public static Dictionary<int, GaSymMultivector> ToOutermorphismDictionary(this IEnumerable<IGaSymMultivector> linearVectorMaps)
        {
            var linearVectorMapsArray = linearVectorMaps.ToArray();

            var domainGaSpaceDim =
                linearVectorMapsArray.Length.ToGaSpaceDimension();

            var targetGaSpaceDim =
                linearVectorMapsArray[0].GaSpaceDimension;

            var omMapDict = new Dictionary<int, GaSymMultivector>();

            //Add unit scalar as the image of the 0-basis blade
            omMapDict.Add(0, GaSymMultivector.CreateUnitScalar(targetGaSpaceDim));

            for (var id = 1; id <= domainGaSpaceDim - 1; id++)
            {
                GaSymMultivector basisBladeImage;

                if (id.IsValidBasisVectorId())
                {
                    //Add images of vector basis blades
                    basisBladeImage =
                        linearVectorMapsArray[id.BasisBladeIndex()].GetVectorPart();
                }
                else
                {
                    //Add images of a higher dimensional basis blade using the outer product
                    //of the images of two lower dimensional basis blades
                    id.SplitBySmallestBasicPattern(out var id1, out var id2);

                    basisBladeImage =
                        omMapDict[id1].Op(omMapDict[id2]);
                }

                if (!basisBladeImage.IsZero())
                    omMapDict.Add(id, basisBladeImage);
            }

            return omMapDict;
        }


        public static GaSymMapUnilinearHash ToHashMap(this IGaSymMapUnilinear linearMap)
        {
            var resultMap = GaSymMapUnilinearHash.Create(
                linearMap.DomainVSpaceDimension,
                linearMap.TargetVSpaceDimension
            );

            foreach (var basisBladeMap in linearMap.BasisBladeMaps())
                resultMap.SetBasisBladeMap(basisBladeMap.Item1, basisBladeMap.Item2);

            return resultMap;
        }

        public static GaSymMapUnilinearTree ToTreeMap(this IGaSymMapUnilinear linearMap)
        {
            var resultMap = GaSymMapUnilinearTree.Create(
                linearMap.DomainVSpaceDimension,
                linearMap.TargetVSpaceDimension
            );

            foreach (var basisBladeMap in linearMap.BasisBladeMaps())
                resultMap.SetBasisBladeMap(basisBladeMap.Item1, basisBladeMap.Item2);

            return resultMap;
        }

        public static GaSymMapUnilinearArray ToArrayMap(this IGaSymMapUnilinear linearMap)
        {
            var resultMap = GaSymMapUnilinearArray.Create(
                linearMap.DomainVSpaceDimension,
                linearMap.TargetVSpaceDimension
            );

            foreach (var basisBladeMap in linearMap.BasisBladeMaps())
                resultMap.SetBasisBladeMap(basisBladeMap.Item1, basisBladeMap.Item2);

            return resultMap;
        }

        public static GaSymMapUnilinearCoefSums ToCoefSumsMap(this IGaSymMapUnilinear linearMap)
        {
            var resultMap = GaSymMapUnilinearCoefSums.Create(
                linearMap.DomainVSpaceDimension,
                linearMap.TargetVSpaceDimension
            );

            foreach (var basisBladeMap in linearMap.BasisBladeMaps())
                resultMap.SetBasisBladeMap(basisBladeMap.Item1, basisBladeMap.Item2);

            return resultMap;
        }

        public static GaSymMapUnilinearMatrix ToMatrixMap(this IGaSymMapUnilinear linearMap)
        {
            return GaSymMapUnilinearMatrix.Create(linearMap.ToMatrix());
        }



        public static GaSymMapUnilinearHash ToHashMap(this ISymbolicMatrix linearMapMatrix)
        {
            Debug.Assert(linearMapMatrix.RowCount.IsValidGaSpaceDimension() && linearMapMatrix.ColumnCount.IsValidGaSpaceDimension());

            var resultMap = GaSymMapUnilinearHash.Create(
                linearMapMatrix.ColumnCount.ToVSpaceDimension(),
                linearMapMatrix.RowCount.ToVSpaceDimension()
            );

            for (var id = 0; id < linearMapMatrix.ColumnCount; id++)
            {
                var mv = GaSymMultivector.CreateFromColumn(linearMapMatrix, id);

                if (!mv.IsNullOrZero())
                    resultMap.SetBasisBladeMap(id, mv);
            }

            return resultMap;
        }

        public static GaSymMapUnilinearTree ToTreeMap(this ISymbolicMatrix linearMapMatrix)
        {
            Debug.Assert(linearMapMatrix.RowCount.IsValidGaSpaceDimension() && linearMapMatrix.ColumnCount.IsValidGaSpaceDimension());

            var resultMap = GaSymMapUnilinearTree.Create(
                linearMapMatrix.ColumnCount.ToVSpaceDimension(),
                linearMapMatrix.RowCount.ToVSpaceDimension()
            );

            for (var id = 0; id < linearMapMatrix.ColumnCount; id++)
            {
                var mv = GaSymMultivector.CreateFromColumn(linearMapMatrix, id);

                if (!mv.IsNullOrZero())
                    resultMap.SetBasisBladeMap(id, mv);
            }

            return resultMap;
        }

        public static GaSymMapUnilinearArray ToArrayMap(this ISymbolicMatrix linearMapMatrix)
        {
            Debug.Assert(linearMapMatrix.RowCount.IsValidGaSpaceDimension() && linearMapMatrix.ColumnCount.IsValidGaSpaceDimension());

            var resultMap = GaSymMapUnilinearArray.Create(
                linearMapMatrix.ColumnCount.ToVSpaceDimension(),
                linearMapMatrix.RowCount.ToVSpaceDimension()
            );

            for (var id = 0; id < linearMapMatrix.ColumnCount; id++)
            {
                var mv = GaSymMultivector.CreateFromColumn(linearMapMatrix, id);

                if (!mv.IsNullOrZero())
                    resultMap.SetBasisBladeMap(id, mv);
            }

            return resultMap;
        }

        public static GaSymMapUnilinearCoefSums ToCoefSumsMap(this ISymbolicMatrix linearMapMatrix)
        {
            Debug.Assert(linearMapMatrix.RowCount.IsValidGaSpaceDimension() && linearMapMatrix.ColumnCount.IsValidGaSpaceDimension());

            var resultMap = GaSymMapUnilinearCoefSums.Create(
                linearMapMatrix.ColumnCount.ToVSpaceDimension(),
                linearMapMatrix.RowCount.ToVSpaceDimension()
            );

            for (var id = 0; id < linearMapMatrix.ColumnCount; id++)
            {
                var mv = GaSymMultivector.CreateFromColumn(linearMapMatrix, id);

                if (!mv.IsNullOrZero())
                    resultMap.SetBasisBladeMap(id, mv);
            }

            return resultMap;
        }

        public static GaSymMapUnilinearMatrix ToMatrixMap(this ISymbolicMatrix linearMapMatrix)
        {
            return GaSymMapUnilinearMatrix.Create(linearMapMatrix);
        }


        public static GaSymMapUnilinearHash ToHashMap(this Dictionary<int, GaSymMultivector> basisBladeMaps, int domainVSpaceDim, int targetVSpaceDim)
        {
            var resultMap = GaSymMapUnilinearHash.Create(domainVSpaceDim, targetVSpaceDim);

            foreach (var term in basisBladeMaps.Where(p => !p.Value.IsNullOrZero()))
                resultMap.SetBasisBladeMap(term.Key, term.Value);

            return resultMap;
        }

        public static GaSymMapUnilinearTree ToTreeMap(this Dictionary<int, GaSymMultivector> basisBladeMaps, int domainVSpaceDim, int targetVSpaceDim)
        {
            var resultMap = GaSymMapUnilinearTree.Create(domainVSpaceDim, targetVSpaceDim);

            foreach (var term in basisBladeMaps.Where(p => !p.Value.IsNullOrZero()))
                resultMap.SetBasisBladeMap(term.Key, term.Value);

            return resultMap;
        }

        public static GaSymMapUnilinearArray ToArrayMap(this Dictionary<int, GaSymMultivector> basisBladeMaps, int domainVSpaceDim, int targetVSpaceDim)
        {
            var resultMap = GaSymMapUnilinearArray.Create(domainVSpaceDim, targetVSpaceDim);

            foreach (var term in basisBladeMaps.Where(p => !p.Value.IsNullOrZero()))
                resultMap.SetBasisBladeMap(term.Key, term.Value);

            return resultMap;
        }

        public static GaSymMapUnilinearCoefSums ToCoefSumsMap(this Dictionary<int, GaSymMultivector> basisBladeMaps, int domainVSpaceDim, int targetVSpaceDim)
        {
            var resultMap = GaSymMapUnilinearCoefSums.Create(domainVSpaceDim, targetVSpaceDim);

            foreach (var term in basisBladeMaps.Where(p => !p.Value.IsNullOrZero()))
                resultMap.SetBasisBladeMap(term.Key, term.Value);

            return resultMap;
        }

        public static GaSymMapUnilinearMatrix ToMatrixMap(this Dictionary<int, GaSymMultivector> basisBladeMaps, int domainVSpaceDim, int targetVSpaceDim)
        {
            var targetGaSpaceDim = targetVSpaceDim.ToGaSpaceDimension();
            var zeroMvArray = new Expr[targetGaSpaceDim];

            return GaSymMapUnilinearMatrix.Create(
                MathematicaMatrix.CreateFullMatrixFromColumns(
                    GaSymbolicsUtils.Cas,
                    Enumerable
                        .Range(0, domainVSpaceDim.ToGaSpaceDimension())
                        .Select(
                            id =>
                            {
                                basisBladeMaps.TryGetValue(id, out var mv);

                                return mv?.TermsToExprArray()
                                       ?? zeroMvArray;
                            }
                        )
                )
            );
        }


        public static GaSymOutermorphism ToOutermorphismHash(this ISymbolicMatrix linearVectorMapMatrix)
        {
            return GaSymOutermorphism.CreateHash(linearVectorMapMatrix);
        }

        public static GaSymOutermorphism ToOutermorphismTree(this ISymbolicMatrix linearVectorMapMatrix)
        {
            return GaSymOutermorphism.CreateTree(linearVectorMapMatrix);
        }

        public static GaSymOutermorphism ToOutermorphismArray(this ISymbolicMatrix linearVectorMapMatrix)
        {
            return GaSymOutermorphism.CreateArray(linearVectorMapMatrix);
        }

        public static GaSymOutermorphism ToOutermorphismCoefSums(this ISymbolicMatrix linearVectorMapMatrix)
        {
            return GaSymOutermorphism.CreateCoefSums(linearVectorMapMatrix);
        }

        public static GaSymOutermorphism ToOutermorphismMatrix(this ISymbolicMatrix linearVectorMapMatrix)
        {
            return GaSymOutermorphism.CreateMatrix(linearVectorMapMatrix);
        }

        public static GaSymOutermorphism ToOutermorphism(this ISymbolicMatrix linearVectorMapMatrix)
        {
            return GaSymOutermorphism.Create(linearVectorMapMatrix);
        }

        #endregion


        #region Bilinear Maps
        public static GaSymMapBilinearArray ToArrayMap(this IGaSymMapBilinear bilinearMap)
        {
            var resultMap = GaSymMapBilinearArray.Create(
                bilinearMap.DomainVSpaceDimension,
                bilinearMap.TargetVSpaceDimension
            );

            foreach (var basisBladeMapping in bilinearMap.BasisBladesMaps())
            {
                var id1 = basisBladeMapping.Item1;
                var id2 = basisBladeMapping.Item2;
                var mv = basisBladeMapping.Item3;

                resultMap.SetBasisBladesMap(id1, id2, mv);
            }

            return resultMap;
        }

        public static GaSymMapBilinearCoefSums ToCoefSumsMap(this IGaSymMapBilinear bilinearMap)
        {
            var resultMap = GaSymMapBilinearCoefSums.Create(
                bilinearMap.DomainVSpaceDimension,
                bilinearMap.TargetVSpaceDimension
            );

            foreach (var basisBladeMapping in bilinearMap.BasisBladesMaps())
            {
                var id1 = basisBladeMapping.Item1;
                var id2 = basisBladeMapping.Item2;
                var mv = basisBladeMapping.Item3;

                foreach (var term in mv.NonZeroExprTerms)
                    resultMap.SetFactor(term.Key, id1, id2, term.Value);
            }

            return resultMap;
        }

        public static GaSymMapBilinearHash ToHashMap(this IGaSymMapBilinear bilinearMap)
        {
            var resultMap = GaSymMapBilinearHash.Create(
                bilinearMap.DomainVSpaceDimension,
                bilinearMap.TargetVSpaceDimension
            );

            foreach (var basisBladeMapping in bilinearMap.BasisBladesMaps())
            {
                var id1 = basisBladeMapping.Item1;
                var id2 = basisBladeMapping.Item2;
                var mv = basisBladeMapping.Item3;

                resultMap.SetBasisBladesMap(id1, id2, mv);
            }

            return resultMap;
        }

        public static GaSymMapBilinearTree ToTreeMap(this IGaSymMapBilinear bilinearMap)
        {
            var resultMap = GaSymMapBilinearTree.Create(
                bilinearMap.DomainVSpaceDimension,
                bilinearMap.TargetVSpaceDimension
            );

            foreach (var basisBladeMapping in bilinearMap.BasisBladesMaps())
            {
                var id1 = basisBladeMapping.Item1;
                var id2 = basisBladeMapping.Item2;
                var mv = basisBladeMapping.Item3;

                resultMap.SetBasisBladesMap(id1, id2, mv);
            }

            return resultMap;
        }
        #endregion
    }
}
