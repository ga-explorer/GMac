using System;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraNumericsLib
{
    public static class GaNumericsUtils
    {
        public static double Epsilon { get; internal set; } 
            = Math.Pow(2, -25);

        public static bool IsNearZero(this double d)
        {
            return Math.Abs(d) <= Epsilon;
        }

        public static bool IsNearZero(this double d, double epsilon)
        {
            return Math.Abs(d) <= epsilon;
        }

        public static bool IsNearEqual(this double d1, double d2)
        {
            return Math.Abs(d2 - d1) <= Epsilon;
        }


        public static bool IsDiagonal(this Matrix matrix)
        {
            if (ReferenceEquals(matrix, null) || matrix.RowCount != matrix.ColumnCount)
                return false;
            
            var diagonalMatrix = matrix as DiagonalMatrix;
            if (!ReferenceEquals(diagonalMatrix, null))
                return true;

            for (var row = 0; row < matrix.RowCount - 1; row++)
            for (var col = row + 1; col < matrix.ColumnCount; col++)
                if (!matrix[row, col].IsNearZero() || !matrix[col, row].IsNearZero())
                        return false;

            return true;
        }

        public static bool IsIdentity(this Matrix matrix)
        {
            if (ReferenceEquals(matrix, null) || matrix.RowCount != matrix.ColumnCount)
                return false;

            var diagonalMatrix = matrix as DiagonalMatrix;
            if (ReferenceEquals(diagonalMatrix, null))
            {
                for (var row = 0; row < matrix.RowCount - 1; row++)
                for (var col = row + 1; col < matrix.ColumnCount; col++)
                    if (!matrix[row, col].IsNearZero() || !matrix[col, row].IsNearZero())
                        return false;
            }

            for (var row = 0; row < matrix.RowCount; row++)
                if (!matrix[row, row].IsNearEqual(1.0d))
                    return false;

            return true;
        }


    }
}
