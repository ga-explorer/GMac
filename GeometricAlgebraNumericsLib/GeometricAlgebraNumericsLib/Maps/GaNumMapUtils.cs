using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DataStructuresLib;
using GeometricAlgebraNumericsLib.Exceptions;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Maps.Bilinear;
using GeometricAlgebraNumericsLib.Maps.Outermorphisms;
using GeometricAlgebraNumericsLib.Maps.Unilinear;
using GeometricAlgebraNumericsLib.Multivectors;
using GeometricAlgebraNumericsLib.Products;
using GeometricAlgebraNumericsLib.Structures;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraNumericsLib.Maps
{
    public static class GaNumMapUtils
    {
        #region Unilinear Maps
        public static Dictionary<int, IGaNumMultivector> ToDictionary(this IGaNumMapUnilinear linearMap)
        {
            return linearMap
                .BasisBladeMaps()
                .ToDictionary(t => t.Item1, t => t.Item2);
        }

        public static IEnumerable<IGaNumMultivector> ToColumnsList(this IGaNumMapUnilinear linearMap)
        {
            return Enumerable
                .Range(0, linearMap.DomainGaSpaceDimension)
                .Select(id => linearMap[id]);
        }

        public static IEnumerable<double[]> ToScalarColumnsList(this IGaNumMapUnilinear linearMap)
        {
            return Enumerable
                .Range(0, linearMap.DomainGaSpaceDimension)
                .Select(id => linearMap[id].TermsToArray());
        }

        public static double[,] ToScalarsArray(this IGaNumMapUnilinear linearMap)
        {
            var resultArray = new double[
                linearMap.TargetGaSpaceDimension,
                linearMap.DomainGaSpaceDimension
            ];

            foreach (var basisBladeMap in linearMap.BasisBladeMaps())
            {
                var col = basisBladeMap.Item1;

                foreach (var term in basisBladeMap.Item2.NonZeroTerms)
                {
                    var row = term.Key;

                    resultArray[row, col] = term.Value;
                }
            }

            return resultArray;
        }

        public static Matrix ToMatrix(this IGaNumMapUnilinear linearMap)
        {
            return DenseMatrix.OfArray(
                ToScalarsArray(linearMap)
            );
        }


        /// <summary>
        /// Extract the basis vectors mappings from the given linear map and convert them into a
        /// linear map on the base vector space of the frame. This should only be used if basis vectors
        /// of the domain are mapped to vectors of the target domain.
        /// </summary>
        /// <param name="linearMap"></param>
        /// <returns></returns>
        public static double[,] BasisVectorMapsToScalarsArray(this IGaNumMapUnilinear linearMap)
        {
            var resultArray = new double[
                linearMap.TargetVSpaceDimension,
                linearMap.DomainVSpaceDimension
            ];

            foreach (var basisVectorMap in linearMap.BasisVectorMaps())
            {
                var col = basisVectorMap.Item1;
                var terms =
                    basisVectorMap
                        .Item2
                        .NonZeroTerms
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
        public static Matrix BasisVectorMapsToMatrix(this IGaNumMapUnilinear linearMap)
        {
            return DenseMatrix.OfArray(
                BasisVectorMapsToScalarsArray(linearMap)
            );
        }


        public static GaNumMultivector MapMultivector(this Matrix mappingMatrix, IGaNumMultivector mv1)
        {
            if (mv1.GaSpaceDimension != mappingMatrix.ColumnCount)
                throw new GaNumericsException("Multivector size mismatch");

            var resultMv = GaNumMultivector.CreateZero(mappingMatrix.RowCount);

            if (mappingMatrix is SparseMatrix)
            {
                var vector = Vector.Build.SparseOfIndexed(
                    mv1.GaSpaceDimension,
                    mv1.Terms.Select(p => Tuple.Create(p.Key, p.Value))
                );

                vector = mappingMatrix.Multiply(vector);

                foreach (var term in vector.EnumerateIndexed()) 
                    resultMv.SetTerm(term.Item1, term.Item2);
            }
            else
            {
                foreach (var term in mv1.NonZeroTerms)
                {
                    var id = term.Key;
                    var scalarValue = term.Value;

                    for (var row = 0; row < mappingMatrix.RowCount; row++)
                        resultMv.UpdateTerm(
                            row, 
                            mappingMatrix[row, id] * scalarValue
                        );
                }
            }

            return resultMv;
        }


        public static GaNumMapUnilinearSparseColumns OddVersorProductToSparseColumnsMap(this GaNumFrame frame, GaNumMultivector oddVersor)
        {
            return frame
                .OddVersorProduct(oddVersor, frame.BasisVectorIDs())
                .ToOutermorphismDictionary()
                .ToSparseColumnsMap(frame.VSpaceDimension, frame.VSpaceDimension);
        }

        public static GaNumMapUnilinearTree OddVersorProductToTreeMap(this GaNumFrame frame, GaNumMultivector oddVersor)
        {
            return frame
                .OddVersorProduct(oddVersor, frame.BasisVectorIDs())
                .ToOutermorphismDictionary()
                .ToTreeMap(frame.VSpaceDimension, frame.VSpaceDimension);
        }

        public static GaNumMapUnilinearArray OddVersorProductToArrayMap(this GaNumFrame frame, GaNumMultivector oddVersor)
        {
            return frame
                .OddVersorProduct(oddVersor, frame.BasisVectorIDs())
                .ToOutermorphismDictionary()
                .ToArrayMap(frame.VSpaceDimension, frame.VSpaceDimension);
        }

        public static GaNumMapUnilinearCoefSums OddVersorProductToCoefSumsMap(this GaNumFrame frame, GaNumMultivector oddVersor)
        {
            return frame
                .OddVersorProduct(oddVersor, frame.BasisVectorIDs())
                .ToOutermorphismDictionary()
                .ToCoefSumsMap(frame.VSpaceDimension, frame.VSpaceDimension);
        }


        public static GaNumMapUnilinearSparseColumns EvenVersorProductToSparseColumnsMap(this GaNumFrame frame, GaNumMultivector evenVersor)
        {
            return frame
                .EvenVersorProduct(evenVersor, frame.BasisVectorIDs())
                .ToOutermorphismDictionary()
                .ToSparseColumnsMap(frame.VSpaceDimension, frame.VSpaceDimension);
        }

        public static GaNumMapUnilinearTree EvenVersorProductToTreeMap(this GaNumFrame frame, GaNumMultivector evenVersor)
        {
            return frame
                .EvenVersorProduct(evenVersor, frame.BasisVectorIDs())
                .ToOutermorphismDictionary()
                .ToTreeMap(frame.VSpaceDimension, frame.VSpaceDimension);
        }

        public static GaNumMapUnilinearArray EvenVersorProductToArrayMap(this GaNumFrame frame, GaNumMultivector evenVersor)
        {
            return frame
                .EvenVersorProduct(evenVersor, frame.BasisVectorIDs())
                .ToOutermorphismDictionary()
                .ToArrayMap(frame.VSpaceDimension, frame.VSpaceDimension);
        }

        public static GaNumMapUnilinearCoefSums EvenVersorProductToCoefSumsMap(this GaNumFrame frame, GaNumMultivector evenVersor)
        {
            return frame
                .EvenVersorProduct(evenVersor, frame.BasisVectorIDs())
                .ToOutermorphismDictionary()
                .ToCoefSumsMap(frame.VSpaceDimension, frame.VSpaceDimension);
        }


        public static GaNumMapUnilinearSparseColumns RotorProductToSparseColumnsMap(this GaNumFrame frame, GaNumMultivector rotorVersor)
        {
            return frame
                .RotorProduct(rotorVersor, frame.BasisVectorIDs())
                .ToOutermorphismDictionary()
                .ToSparseColumnsMap(frame.VSpaceDimension, frame.VSpaceDimension);
        }

        public static GaNumMapUnilinearTree RotorProductToTreeMap(this GaNumFrame frame, GaNumMultivector rotorVersor)
        {
            return frame
                .RotorProduct(rotorVersor, frame.BasisVectorIDs())
                .ToOutermorphismDictionary()
                .ToTreeMap(frame.VSpaceDimension, frame.VSpaceDimension);
        }

        public static GaNumMapUnilinearArray RotorProductToArrayMap(this GaNumFrame frame, GaNumMultivector rotorVersor)
        {
            return frame
                .RotorProduct(rotorVersor, frame.BasisVectorIDs())
                .ToOutermorphismDictionary()
                .ToArrayMap(frame.VSpaceDimension, frame.VSpaceDimension);
        }

        public static GaNumMapUnilinearCoefSums RotorProductToCoefSumsMap(this GaNumFrame frame, GaNumMultivector rotorVersor)
        {
            return frame
                .RotorProduct(rotorVersor, frame.BasisVectorIDs())
                .ToOutermorphismDictionary()
                .ToCoefSumsMap(frame.VSpaceDimension, frame.VSpaceDimension);
        }


        public static Dictionary<int, GaNumMultivector> ToOutermorphismDictionary(this Dictionary<int, IGaNumMultivector> linearVectorMaps, int domainVSpaceDim, int targetVSpaceDim)
        {
            var domainGaSpaceDim =
                domainVSpaceDim.ToGaSpaceDimension();

            var targetGaSpaceDim =
                targetVSpaceDim.ToGaSpaceDimension();

            var omMapDict = new Dictionary<int, GaNumMultivector>();

            //Add unit scalar as the image of the 0-basis blade
            omMapDict.Add(0, GaNumMultivector.CreateUnitScalar(targetGaSpaceDim));

            for (var id = 1; id <= domainGaSpaceDim - 1; id++)
            {
                GaNumMultivector basisBladeImage;

                if (id.IsValidBasisVectorId())
                {
                    //Add images of vector basis blades
                    linearVectorMaps.TryGetValue(id.BasisBladeIndex(), out var mv);

                    basisBladeImage = mv?.GetVectorPart() 
                                      ?? GaNumMultivector.CreateZero(targetGaSpaceDim);
                }
                else
                {
                    //Add images of a higher dimensional basis blade using the outer product
                    //of the images of two lower dimensional basis blades
                    id.SplitBySmallestBasicPattern(out var id1, out var id2);

                    basisBladeImage =
                        omMapDict[id1].Op(omMapDict[id2]);
                }

                if (!basisBladeImage.IsNullOrEmpty())
                    omMapDict.Add(id, basisBladeImage);
            }

            return omMapDict;
        }

        public static Dictionary<int, GaNumMultivector> ToOutermorphismDictionary(this IEnumerable<IGaNumMultivector> linearVectorMaps)
        {
            var linearVectorMapsArray = linearVectorMaps.ToArray();

            var domainGaSpaceDim =
                linearVectorMapsArray.Length.ToGaSpaceDimension();

            var targetGaSpaceDim =
                linearVectorMapsArray[0].GaSpaceDimension;

            var omMapDict = new Dictionary<int, GaNumMultivector>();

            //Add unit scalar as the image of the 0-basis blade
            omMapDict.Add(0, GaNumMultivector.CreateUnitScalar(targetGaSpaceDim));

            for (var id = 1; id <= domainGaSpaceDim - 1; id++)
            {
                GaNumMultivector basisBladeImage;

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

                if (!basisBladeImage.IsNullOrEmpty())
                    omMapDict.Add(id, basisBladeImage);
            }

            return omMapDict;
        }

        public static Dictionary<int, GaNumMultivector> ToOutermorphismDictionary(this IEnumerable<double[]> linearVectorMaps)
        {
            var linearVectorMapsArray = linearVectorMaps.ToArray();

            var domainGaSpaceDim =
                linearVectorMapsArray.Length.ToGaSpaceDimension();

            var targetGaSpaceDim =
                linearVectorMapsArray[0].Length;

            var omMapDict = new Dictionary<int, GaNumMultivector>();

            //Add unit scalar as the image of the 0-basis blade
            omMapDict.Add(0, GaNumMultivector.CreateUnitScalar(targetGaSpaceDim));

            for (var id = 1; id <= domainGaSpaceDim - 1; id++)
            {
                GaNumMultivector basisBladeImage;

                if (id.IsValidBasisVectorId())
                {
                    //Add images of vector basis blades
                    basisBladeImage = GaNumMultivector.CreateVectorFromScalars(
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

                if (!basisBladeImage.IsNullOrEmpty())
                    omMapDict.Add(id, basisBladeImage);
            }

            return omMapDict;
        }

        public static Dictionary<int, GaNumMultivector> ToOutermorphismDictionary(this double[,] linearVectorMapsArray)
        {
            var domainGaSpaceDim =
                linearVectorMapsArray.GetLength(1).ToGaSpaceDimension();

            var targetGaSpaceDim =
                linearVectorMapsArray.GetLength(0).ToGaSpaceDimension();

            var omMapDict = new Dictionary<int, GaNumMultivector>();

            //Add unit scalar as the image of the 0-basis blade
            omMapDict.Add(0, GaNumMultivector.CreateUnitScalar(targetGaSpaceDim));

            for (var id = 1; id <= domainGaSpaceDim - 1; id++)
            {
                GaNumMultivector basisBladeImage;

                if (id.IsValidBasisVectorId())
                {
                    //Add images of vector basis blades
                    basisBladeImage = GaNumMultivector.CreateVectorFromColumn(
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

                if (!basisBladeImage.IsNullOrEmpty())
                    omMapDict.Add(id, basisBladeImage);
            }

            return omMapDict;
        }

        public static Dictionary<int, GaNumMultivector> ToOutermorphismDictionary(this Matrix linearVectorMapsMatrix)
        {
            var domainGaSpaceDim =
                linearVectorMapsMatrix.ColumnCount.ToGaSpaceDimension();

            var targetGaSpaceDim =
                linearVectorMapsMatrix.RowCount.ToGaSpaceDimension();

            var omMapDict = new Dictionary<int, GaNumMultivector>();

            //Add unit scalar as the image of the 0-basis blade
            omMapDict.Add(0, GaNumMultivector.CreateUnitScalar(targetGaSpaceDim));

            for (var id = 1; id <= domainGaSpaceDim - 1; id++)
            {
                GaNumMultivector basisBladeImage;

                if (id.IsValidBasisVectorId())
                {
                    //Add images of vector basis blades
                    basisBladeImage =
                        GaNumMultivector.CreateVectorFromColumn(
                            linearVectorMapsMatrix,
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

                if (!basisBladeImage.IsNullOrEmpty())
                    omMapDict.Add(id, basisBladeImage);
            }

            return omMapDict;
        }


        public static GaNumMapUnilinearSparseColumns ToSparseColumnsMap(this IGaNumMapUnilinear linearMap)
        {
            var resultMap = GaNumMapUnilinearSparseColumns.Create(
                linearMap.DomainVSpaceDimension,
                linearMap.TargetVSpaceDimension
            );

            foreach (var basisBladeMap in linearMap.BasisBladeMaps())
                resultMap.SetColumn(basisBladeMap.Item1, basisBladeMap.Item2);

            return resultMap;
        }

        public static GaNumMapUnilinearSparseRows ToSparseRowsMap(this IGaNumMapUnilinear linearMap)
        {
            var resultMap = GaNumMapUnilinearSparseRows.Create(
                linearMap.DomainVSpaceDimension,
                linearMap.TargetVSpaceDimension
            );

            foreach (var basisBladeMap in linearMap.BasisBladeMaps())
                resultMap.SetColumn(basisBladeMap.Item1, basisBladeMap.Item2);

            return resultMap;
        }

        public static GaNumMapUnilinearTree ToTreeMap(this IGaNumMapUnilinear linearMap)
        {
            var resultMap = GaNumMapUnilinearTree.Create(
                linearMap.DomainVSpaceDimension,
                linearMap.TargetVSpaceDimension
            );

            foreach (var basisBladeMap in linearMap.BasisBladeMaps())
                resultMap.SetBasisBladeMap(basisBladeMap.Item1, basisBladeMap.Item2);

            return resultMap;
        }

        public static GaNumMapUnilinearArray ToArrayMap(this IGaNumMapUnilinear linearMap)
        {
            var resultMap = GaNumMapUnilinearArray.Create(
                linearMap.DomainVSpaceDimension,
                linearMap.TargetVSpaceDimension
            );

            foreach (var basisBladeMap in linearMap.BasisBladeMaps())
                resultMap.SetBasisBladeMap(basisBladeMap.Item1, basisBladeMap.Item2);

            return resultMap;
        }

        public static GaNumMapUnilinearCoefSums ToCoefSumsMap(this IGaNumMapUnilinear linearMap)
        {
            var resultMap = GaNumMapUnilinearCoefSums.Create(
                linearMap.DomainVSpaceDimension,
                linearMap.TargetVSpaceDimension
            );

            foreach (var basisBladeMap in linearMap.BasisBladeMaps())
                resultMap.SetBasisBladeMap(basisBladeMap.Item1, basisBladeMap.Item2);

            return resultMap;
        }

        public static GaNumMapUnilinearMatrix ToMatrixMap(this IGaNumMapUnilinear linearMap)
        {
            return GaNumMapUnilinearMatrix.Create(linearMap.ToMatrix());
        }


        public static GaNumMapUnilinearSparseColumns ToSparseColumnsMap(this Matrix linearMapMatrix)
        {
            Debug.Assert(linearMapMatrix.RowCount.IsValidGaSpaceDimension() && linearMapMatrix.ColumnCount.IsValidGaSpaceDimension());

            var resultMap = GaNumMapUnilinearSparseColumns.Create(
                linearMapMatrix.ColumnCount.ToVSpaceDimension(),
                linearMapMatrix.RowCount.ToVSpaceDimension()
            );

            for (var id = 0; id < linearMapMatrix.ColumnCount; id++)
            {
                var mv = GaNumMultivector.CreateFromColumn(linearMapMatrix, id);

                if (!mv.IsNullOrEmpty())
                    resultMap.SetColumn(id, mv);
            }

            return resultMap;
        }

        public static GaNumMapUnilinearSparseRows ToSparseRowsMap(this Matrix linearMapMatrix)
        {
            Debug.Assert(linearMapMatrix.RowCount.IsValidGaSpaceDimension() && linearMapMatrix.ColumnCount.IsValidGaSpaceDimension());

            var resultMap = GaNumMapUnilinearSparseRows.Create(
                linearMapMatrix.ColumnCount.ToVSpaceDimension(),
                linearMapMatrix.RowCount.ToVSpaceDimension()
            );

            for (var rowId = 0; rowId < linearMapMatrix.RowCount; rowId++)
            {
                var mv = GaNumMultivector.CreateFromRow(linearMapMatrix, rowId);

                if (!mv.IsNullOrEmpty())
                    resultMap.SetRow(rowId, mv);
            }

            return resultMap;
        }

        public static GaNumMapUnilinearTree ToTreeMap(this Matrix linearMapMatrix)
        {
            Debug.Assert(linearMapMatrix.RowCount.IsValidGaSpaceDimension() && linearMapMatrix.ColumnCount.IsValidGaSpaceDimension());

            var resultMap = GaNumMapUnilinearTree.Create(
                linearMapMatrix.ColumnCount.ToVSpaceDimension(),
                linearMapMatrix.RowCount.ToVSpaceDimension()
            );

            for (var id = 0; id < linearMapMatrix.ColumnCount; id++)
            {
                var mv = GaNumMultivector.CreateFromColumn(linearMapMatrix, id);

                if (!mv.IsNullOrEmpty())
                    resultMap.SetBasisBladeMap(id, mv);
            }

            return resultMap;
        }

        public static GaNumMapUnilinearArray ToArrayMap(this Matrix linearMapMatrix)
        {
            Debug.Assert(linearMapMatrix.RowCount.IsValidGaSpaceDimension() && linearMapMatrix.ColumnCount.IsValidGaSpaceDimension());

            var resultMap = GaNumMapUnilinearArray.Create(
                linearMapMatrix.ColumnCount.ToVSpaceDimension(),
                linearMapMatrix.RowCount.ToVSpaceDimension()
            );

            for (var id = 0; id < linearMapMatrix.ColumnCount; id++)
            {
                var mv = GaNumMultivector.CreateFromColumn(linearMapMatrix, id);

                if (!mv.IsNullOrEmpty())
                    resultMap.SetBasisBladeMap(id, mv);
            }

            return resultMap;
        }

        public static GaNumMapUnilinearCoefSums ToCoefSumsMap(this Matrix linearMapMatrix)
        {
            Debug.Assert(linearMapMatrix.RowCount.IsValidGaSpaceDimension() && linearMapMatrix.ColumnCount.IsValidGaSpaceDimension());

            var resultMap = GaNumMapUnilinearCoefSums.Create(
                linearMapMatrix.ColumnCount.ToVSpaceDimension(),
                linearMapMatrix.RowCount.ToVSpaceDimension()
            );

            for (var id = 0; id < linearMapMatrix.ColumnCount; id++)
            {
                var mv = GaNumMultivector.CreateFromColumn(linearMapMatrix, id);

                if (!mv.IsNullOrEmpty())
                    resultMap.SetBasisBladeMap(id, mv);
            }

            return resultMap;
        }

        public static GaNumMapUnilinearMatrix ToMatrixMap(this Matrix linearMapMatrix)
        {
            return GaNumMapUnilinearMatrix.Create(linearMapMatrix);
        }


        public static GaNumMapUnilinearSparseColumns ToSparseColumnsMap(this Dictionary<int, GaNumMultivector> basisBladeMaps, int domainVSpaceDim, int targetVSpaceDim)
        {
            var resultMap = GaNumMapUnilinearSparseColumns.Create(domainVSpaceDim, targetVSpaceDim);

            foreach (var term in basisBladeMaps.Where(p => !p.Value.IsNullOrEmpty()))
                resultMap.SetColumn(term.Key, term.Value);

            return resultMap;
        }

        public static GaNumMapUnilinearSparseRows ToSparseRowsMap(this Dictionary<int, GaNumMultivector> basisBladeMaps, int domainVSpaceDim, int targetVSpaceDim)
        {
            var resultMap = GaNumMapUnilinearSparseRows.Create(domainVSpaceDim, targetVSpaceDim);

            foreach (var term in basisBladeMaps.Where(p => !p.Value.IsNullOrEmpty()))
                resultMap.SetColumn(term.Key, term.Value);

            return resultMap;
        }

        public static GaNumMapUnilinearTree ToTreeMap(this Dictionary<int, GaNumMultivector> basisBladeMaps, int domainVSpaceDim, int targetVSpaceDim)
        {
            var resultMap = GaNumMapUnilinearTree.Create(domainVSpaceDim, targetVSpaceDim);

            foreach (var term in basisBladeMaps.Where(p => !p.Value.IsNullOrEmpty()))
                resultMap.SetBasisBladeMap(term.Key, term.Value);

            return resultMap;
        }

        public static GaNumMapUnilinearArray ToArrayMap(this Dictionary<int, GaNumMultivector> basisBladeMaps, int domainVSpaceDim, int targetVSpaceDim)
        {
            var resultMap = GaNumMapUnilinearArray.Create(domainVSpaceDim, targetVSpaceDim);

            foreach (var term in basisBladeMaps.Where(p => !p.Value.IsNullOrEmpty()))
                resultMap.SetBasisBladeMap(term.Key, term.Value);

            return resultMap;
        }
        
        public static GaNumMapUnilinearCoefSums ToCoefSumsMap(this Dictionary<int, GaNumMultivector> basisBladeMaps, int domainVSpaceDim, int targetVSpaceDim)
        {
            var resultMap = GaNumMapUnilinearCoefSums.Create(domainVSpaceDim, targetVSpaceDim);

            foreach (var term in basisBladeMaps.Where(p => !p.Value.IsNullOrEmpty()))
                resultMap.SetBasisBladeMap(term.Key, term.Value);

            return resultMap;
        }

        public static GaNumMapUnilinearMatrix ToDenseMatrixMap(this Dictionary<int, GaNumMultivector> basisBladeMaps, int domainVSpaceDim, int targetVSpaceDim)
        {
            var targetGaSpaceDim = targetVSpaceDim.ToGaSpaceDimension();
            var zeroMvArray = new double[targetGaSpaceDim];
            var columns = Enumerable
                .Range(0, domainVSpaceDim.ToGaSpaceDimension())
                .Select(
                    id =>
                    {
                        basisBladeMaps.TryGetValue(id, out var mv);

                        return mv?.TermsToArray()
                               ?? zeroMvArray;
                    }
                );

            return GaNumMapUnilinearMatrix.Create(
                DenseMatrix.OfColumnArrays(columns)
            );
        }

        public static GaNumMapUnilinearMatrix ToSparseMatrixMap(this Dictionary<int, GaNumMultivector> basisBladeMaps, int domainVSpaceDim, int targetVSpaceDim)
        {
            var targetGaSpaceDim = targetVSpaceDim.ToGaSpaceDimension();
            var zeroMvArray = new double[targetGaSpaceDim];
            var columns = Enumerable
                .Range(0, domainVSpaceDim.ToGaSpaceDimension())
                .Select(
                    id =>
                    {
                        basisBladeMaps.TryGetValue(id, out var mv);

                        return mv?.TermsToArray()
                               ?? zeroMvArray;
                    }
                );

            return GaNumMapUnilinearMatrix.Create(
                SparseMatrix.OfColumnArrays(columns)
            );
        }

        public static GaNumOutermorphism ToComputedOutermorphism(this Matrix linearVectorMapMatrix)
        {
            return GaNumOutermorphism.Create(linearVectorMapMatrix);
        }

        public static GaNumStoredOutermorphism ToOutermorphismSparseColumns(this Matrix linearVectorMapMatrix)
        {
            return GaNumStoredOutermorphism.CreateSparseColumns(linearVectorMapMatrix);
        }

        public static GaNumStoredOutermorphism ToOutermorphismSparseRows(this Matrix linearVectorMapMatrix)
        {
            return GaNumStoredOutermorphism.CreateSparseRows(linearVectorMapMatrix);
        }

        public static GaNumStoredOutermorphism ToOutermorphismTree(this Matrix linearVectorMapMatrix)
        {
            return GaNumStoredOutermorphism.CreateTree(linearVectorMapMatrix);
        }

        public static GaNumStoredOutermorphism ToOutermorphismArray(this Matrix linearVectorMapMatrix)
        {
            return GaNumStoredOutermorphism.CreateArray(linearVectorMapMatrix);
        }

        public static GaNumStoredOutermorphism ToOutermorphismCoefSums(this Matrix linearVectorMapMatrix)
        {
            return GaNumStoredOutermorphism.CreateCoefSums(linearVectorMapMatrix);
        }

        public static GaNumStoredOutermorphism ToOutermorphismMatrix(this Matrix linearVectorMapMatrix)
        {
            return GaNumStoredOutermorphism.CreateMatrix(linearVectorMapMatrix); 
        }


        public static GaNumStoredOutermorphism ToOutermorphism(this Matrix linearVectorMapMatrix)
        {
            return GaNumStoredOutermorphism.Create(linearVectorMapMatrix);
        }

        #endregion


        #region Bilinear Maps
        public static GaNumMultivector MapToMultivector(this GaBinaryTreeInternalNode<IGaNumMapUnilinear> mappingTree, GaNumMultivector mv1, GaNumMultivector mv2, int targetGaSpaceDimension)
        {
            if (mv1.GaSpaceDimension != mappingTree.TreeDepth.ToGaSpaceDimension())
                throw new GaNumericsException("Multivector size mismatch");

            var resultMv = GaNumMultivector.CreateZero(targetGaSpaceDimension);

            var nodeStack1 = mappingTree.CreateNodesStack();
            var nodeStack2 = mv1.TermsTree.CreateNodesStack();

            while (nodeStack1.Count > 0)
            {
                var node1 = nodeStack1.Pop();
                var node2 = nodeStack2.Pop();

                if (node1.IsLeafNode && !ReferenceEquals(node1.Value, null))
                {
                    var leafScalar = node2.Value;
                    var leafMv = node1.Value[mv2];

                    resultMv.AddFactors(
                        leafScalar,
                        leafMv
                    );

                    continue;
                }

                if (node1.HasChildNode0 && node2.HasChildNode0)
                {
                    nodeStack1.Push(node1.ChildNode0);
                    nodeStack2.Push(node2.ChildNode0);
                }

                if (node1.HasChildNode1 && node2.HasChildNode1)
                {
                    nodeStack1.Push(node1.ChildNode1);
                    nodeStack2.Push(node2.ChildNode1);
                }
            }

            return resultMv;
        }


        public static GaNumMapBilinearArray ToArrayMap(this IGaNumMapBilinear bilinearMap)
        {
            var resultMap = 
                bilinearMap.DomainVSpaceDimension == bilinearMap.TargetVSpaceDimension
                    ? GaNumMapBilinearArray.Create(
                        bilinearMap.DomainVSpaceDimension,
                        bilinearMap.Associativity
                    )
                    : GaNumMapBilinearArray.Create(
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

        public static GaNumMapBilinearCoefSums ToCoefSumsMap(this IGaNumMapBilinear bilinearMap)
        {
            var resultMap = 
                bilinearMap.DomainVSpaceDimension == bilinearMap.TargetVSpaceDimension
                    ? GaNumMapBilinearCoefSums.Create(
                        bilinearMap.DomainVSpaceDimension,
                        bilinearMap.Associativity
                    )
                    : GaNumMapBilinearCoefSums.Create(
                        bilinearMap.DomainVSpaceDimension,
                        bilinearMap.TargetVSpaceDimension
                    );

            foreach (var basisBladeMapping in bilinearMap.BasisBladesMaps())
            {
                var id1 = basisBladeMapping.Item1;
                var id2 = basisBladeMapping.Item2;
                var mv = basisBladeMapping.Item3;

                foreach (var term in mv.NonZeroTerms)
                    resultMap.SetFactor(term.Key, id1, id2, term.Value);
            }

            return resultMap;
        }

        public static GaNumMapBilinearHash ToHashMap(this IGaNumMapBilinear bilinearMap)
        {
            var resultMap = 
                bilinearMap.DomainVSpaceDimension == bilinearMap.TargetVSpaceDimension
                    ? GaNumMapBilinearHash.Create(
                        bilinearMap.DomainVSpaceDimension,
                        bilinearMap.Associativity
                    )
                    : GaNumMapBilinearHash.Create(
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

        public static GaNumMapBilinearTree ToTreeMap(this IGaNumMapBilinear bilinearMap)
        {
            var resultMap = 
                bilinearMap.DomainVSpaceDimension == bilinearMap.TargetVSpaceDimension
                    ? GaNumMapBilinearTree.Create(
                        bilinearMap.DomainVSpaceDimension,
                        bilinearMap.Associativity
                    )
                    : GaNumMapBilinearTree.Create(
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
