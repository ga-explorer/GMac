using System;

namespace GeometricAlgebraNumericsLib.Interop.MATLAB
{
    /// <summary>
    /// Used for exchanging data with MATLAB
    /// </summary>
    public sealed class MatlabSparseColumnMatrixData
    {
        public int MaxSize { get; }

        public int ItemsCount 
            => IndicesArray.Length;

        public int[] IndicesArray { get; }

        public double[] ValuesArray { get; }


        public MatlabSparseColumnMatrixData(int maxSize, int itemsCount)
        {
            if (maxSize < itemsCount)
                throw new InvalidOperationException();

            MaxSize = maxSize;
            IndicesArray = new int[itemsCount];
            ValuesArray = new double[itemsCount];
        }

        public MatlabSparseColumnMatrixData(int maxSize, int[] indicesArray, double[] valuesArray)
        {
            if (maxSize < indicesArray.Length)
                throw new InvalidOperationException();

            if (indicesArray.Length != valuesArray.Length)
                throw new InvalidOperationException();

            MaxSize = maxSize;
            IndicesArray = indicesArray;
            ValuesArray = valuesArray;
        }


        public MatlabSparseColumnMatrixData SetItem(int sparseIndex, int index, double value)
        {
            IndicesArray[sparseIndex] = index;
            ValuesArray[sparseIndex] = value;

            return this;
        }
    }
}