using System.Collections;
using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraNumericsLib.Structures.Collections
{
    public sealed class GaNumReadOnlyMatrixColumn : IReadOnlyList<double>
    {
        public Matrix BaseMatrix { get; }

        public int BaseMatrixColumn { get; }

        public int Count 
            => BaseMatrix.RowCount;

        public double this[int index] 
            => BaseMatrix[index, BaseMatrixColumn];


        public GaNumReadOnlyMatrixColumn(Matrix baseMatrix, int baseMatrixColumn)
        {
            BaseMatrix = baseMatrix;
            BaseMatrixColumn = baseMatrixColumn;
        }


        public IEnumerator<double> GetEnumerator()
        {
            for (var i = 0; i < BaseMatrix.RowCount; i++) 
                yield return BaseMatrix[i, BaseMatrixColumn];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (var i = 0; i < BaseMatrix.RowCount; i++)
                yield return BaseMatrix[i, BaseMatrixColumn];
        }
    }
}
