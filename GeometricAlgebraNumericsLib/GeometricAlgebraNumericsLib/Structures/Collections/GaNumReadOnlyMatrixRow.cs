using System.Collections;
using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraNumericsLib.Structures.Collections
{
    public sealed class GaNumReadOnlyMatrixRow : IReadOnlyList<double>
    {
        public Matrix BaseMatrix { get; }

        public int BaseMatrixRow { get; }

        public int Count
            => BaseMatrix.RowCount;

        public double this[int index]
            => BaseMatrix[BaseMatrixRow, index];


        public GaNumReadOnlyMatrixRow(Matrix baseMatrix, int baseMatrixRow)
        {
            BaseMatrix = baseMatrix;
            BaseMatrixRow = baseMatrixRow;
        }


        public IEnumerator<double> GetEnumerator()
        {
            for (var i = 0; i < BaseMatrix.RowCount; i++)
                yield return BaseMatrix[BaseMatrixRow, i];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (var i = 0; i < BaseMatrix.RowCount; i++)
                yield return BaseMatrix[BaseMatrixRow, i];
        }
    }
}