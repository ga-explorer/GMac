using System;
using GeometricAlgebraNumericsLib.Exceptions;
using GeometricAlgebraNumericsLib.Products;

namespace GeometricAlgebraNumericsLib.Multivectors.VectorKVectorOp
{
    public static partial class GaNumVectorKVectorOpUtils
    {
        private static int[][][] ActiveOpIndexTable { get; set; }

        private static Func<double[], double[], double[]>[] ActiveOpFunctionTable { get; set; }


        public static void SetActiveVSpaceDimension(int vSpaceDim)
        {
            var n = vSpaceDim - 2;

            ActiveOpIndexTable = 
                n < VectorKVectorOpIndexTablesArray.Length
                ? VectorKVectorOpIndexTablesArray[vSpaceDim - 2] 
                : null;

            ActiveOpFunctionTable =
                n < VectorKVectorOpFunctionTablesArray.Length
                ? VectorKVectorOpFunctionTablesArray[vSpaceDim - 2] 
                : null;
        }

        public static GaNumKVector VectorKVectorOp1(this GaNumKVector vector, GaNumKVector kVector)
        {
            if (vector.GaSpaceDimension != kVector.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            if (vector.Grade != 1)
                throw new InvalidOperationException();

            if (ActiveOpIndexTable == null)
                return vector.ComputeVectorKVectorOp(kVector); 

            var n = vector.ScalarValuesArray.Length;
            var k = kVector.Grade;
            var vectorArray = vector.ScalarValuesArray;
            var kVectorArray = kVector.ScalarValuesArray;

            var vectorKVectorOpIndexArray = ActiveOpIndexTable[k - 1];
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

            return GaNumKVector.Create(1 << n, k + 1, outputKVectorArray);
        }

        public static GaNumKVector VectorKVectorOp2(this GaNumKVector vector, GaNumKVector kVector)
        {
            if (vector.GaSpaceDimension != kVector.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            if (vector.Grade != 1)
                throw new InvalidOperationException();

            if (ActiveOpFunctionTable == null)
                return vector.ComputeVectorKVectorOp(kVector); 

            var n = vector.ScalarValuesArray.Length;
            var k = kVector.Grade;

            var outputKVectorArray = 
                ActiveOpFunctionTable[k - 1](
                    vector.ScalarValuesArray,
                    kVector.ScalarValuesArray
                );

            return GaNumKVector.Create(1 << n, k + 1, outputKVectorArray);
        }
    }
}
