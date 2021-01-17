using System;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib;
using GeometricAlgebraNumericsLib.Exceptions;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraStructuresLib.Frames;

namespace GeometricAlgebraNumericsLib.Multivectors.VectorKVectorOp
{
    public static partial class GaNumVectorKVectorOpUtils
    {
        private static int[][][] ActiveIndexLookupTable { get; set; }

        private static Func<IReadOnlyList<double>, IReadOnlyList<double>, IReadOnlyList<double>>[] ActiveFunctionLookupTable { get; set; }


        public static void SelectVectorKVectorOpIndexLookupTable(int vSpaceDim)
        {
            const int minVSpaceDim = 2;

            ActiveIndexLookupTable =
                vSpaceDim < GaLookupTables.VectorKVectorOpIndexLookupTables.Length + minVSpaceDim
                    ? GaLookupTables.VectorKVectorOpIndexLookupTables[vSpaceDim - minVSpaceDim]
                    : null;
        }

        public static void SetActiveVSpaceDimension(int vSpaceDim)
        {
            if (vSpaceDim < 12)
            {
                ActiveFunctionLookupTable = VectorKVectorOpFunctionTablesArray[vSpaceDim - 2];
                ActiveIndexLookupTable = null;
                return;
            }

            if (vSpaceDim < 16)
            {
                ActiveFunctionLookupTable = null;
                ActiveIndexLookupTable = GaLookupTables.VectorKVectorOpIndexLookupTables[vSpaceDim - 12];
                return;
            }

            ActiveFunctionLookupTable = null;
            ActiveIndexLookupTable = null;
        }

        public static GaNumDarKVector ComputeOp(this IGaNumVector vector, IGaNumKVector kVector)
        {
            var resultScalarValues = new double[
                GaFrameUtils.KvSpaceDimension(vector.VSpaceDimension, 1 + kVector.Grade)
            ];

            var maxId = vector.GaSpaceDimension - 1;
            for (var index2 = 0; index2 < kVector.StoredTermsCount; index2++)
            {
                var id2 = GaFrameUtils.BasisBladeId(kVector.Grade, index2);
                var value2 = kVector.ScalarValuesArray[index2];

                if (value2 == 0)
                    continue;

                var indexList = (id2 ^ maxId).PatternToPositions();
                foreach (var index1 in indexList)
                {
                    var id1 = 1 << index1;
                    var value1 = vector.ScalarValuesArray[index1];

                    var index = (id1 | id2).BasisBladeIndex();
                    var value = GaFrameUtils.IsNegativeVectorEGp(index1, id2)
                        ? (-value1 * value2)
                        : (value1 * value2);

                    resultScalarValues[index] = value;
                }
            }

            return new GaNumDarKVector(
                vector.VSpaceDimension, 
                1 + kVector.Grade, 
                resultScalarValues
            );
        }

        public static GaNumDarKVector Op(this IGaNumVector[] mvArray)
        {
            return mvArray.Skip(1).Aggregate(
                mvArray[0].ToDarKVector(),
                (current, mv) => mv.Op(current)
            );
        }

        public static GaNumDarKVector Op(this IGaNumVector vector, IGaNumKVector kVector)
        {
            if (vector.GaSpaceDimension != kVector.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            //Use code-generated functions to compute the vector-kvector outer product
            if (ActiveFunctionLookupTable != null)
            {
                var n = vector.ScalarValuesArray.Count;
                var k = kVector.Grade;

                var outputKVectorArray =
                    ActiveFunctionLookupTable[k - 1](
                        vector.ScalarValuesArray,
                        kVector.ScalarValuesArray
                    );

                return GaNumDarKVector.Create(1 << n, k + 1, outputKVectorArray);
            }

            //Use index arrays to compute the vector-kvector outer product
            if (ActiveIndexLookupTable != null)
            {
                var n = vector.ScalarValuesArray.Count;
                var k = kVector.Grade;
                var vectorArray = vector.ScalarValuesArray;
                var kVectorArray = kVector.ScalarValuesArray;

                var vectorKVectorOpIndexArray = ActiveIndexLookupTable[k - 1];
                var outputKVectorSize = vectorKVectorOpIndexArray.Length;

                var outputKVectorArray = new double[outputKVectorSize];

                for (var i = 0; i < outputKVectorSize; i++)
                {
                    var termsArray = vectorKVectorOpIndexArray[i];
                    var termSize = termsArray.Length;

                    var value = 0.0d;
                    var sign = 1.0d;

                    for (var j = 0; j < termSize; j += 2)
                    {
                        value += sign * vectorArray[termsArray[j]] * kVectorArray[termsArray[j + 1]];
                        sign = -sign;
                    }

                    outputKVectorArray[i] = value;
                }

                return GaNumDarKVector.Create(1 << n, k + 1, outputKVectorArray);
            }

            //Use generic loop-based function to compute the vector-kvector outer product
            return vector.ComputeOp(kVector);
        }
    }
}
