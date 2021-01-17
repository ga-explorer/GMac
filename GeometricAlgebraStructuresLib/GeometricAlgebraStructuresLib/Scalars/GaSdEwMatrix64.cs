using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraStructuresLib.Scalars
{
    /// <summary>
    /// Each scalar in this domain is a dense matrix of predefined size. Addition and multiplication operations
    /// are done elementwise.
    /// </summary>
    public sealed class GaSdEwDenseMatrix64 : IGaScalarDomain<Matrix>
    {
        public double ZeroEpsilon { get; set; }
            = 1e-13d;
        
        public int RowCount { get; }
        
        public int ColumnCount { get; }

        
        public GaSdEwDenseMatrix64(int rowCount, int columnCount)
        {
            RowCount = rowCount;
            ColumnCount = columnCount;
        }
        
        
        public Matrix GetZero()
        {
            return DenseMatrix.Create(RowCount, ColumnCount, 0.0d);
        }

        public Matrix GetOne()
        {
            return DenseMatrix.Create(RowCount, ColumnCount, 1.0d);
        }
        
        public Matrix Add(Matrix scalar1, Matrix scalar2)
        {
            Debug.Assert(
                scalar1.RowCount == RowCount && scalar1.ColumnCount == ColumnCount &&
                scalar2.RowCount == RowCount && scalar2.ColumnCount == ColumnCount
            );
            
            return (Matrix)(scalar1 + scalar2);
        }

        public Matrix Add(params Matrix[] scalarsList)
        {
            Debug.Assert(scalarsList.All(m => 
                m.RowCount == RowCount && m.ColumnCount == ColumnCount
            ));
            
            var sum = scalarsList[0];

            return scalarsList
                .Skip(1)
                .Aggregate(sum, (current, scalar) => (Matrix) (current + scalar));
        }

        public Matrix Add(IEnumerable<Matrix> scalarsList)
        {
            Debug.Assert(scalarsList.All(m => 
                m.RowCount == RowCount && m.ColumnCount == ColumnCount
            ));
            
            var sum = GetZero();

            return scalarsList
                .Aggregate(sum, (current, scalar) => (Matrix) (current + scalar));
        }

        public Matrix Subtract(Matrix scalar1, Matrix scalar2)
        {
            Debug.Assert(
                scalar1.RowCount == RowCount && scalar1.ColumnCount == ColumnCount &&
                scalar2.RowCount == RowCount && scalar2.ColumnCount == ColumnCount
            );
            
            return (Matrix)(scalar1 - scalar2);
        }

        public Matrix Times(Matrix scalar1, Matrix scalar2)
        {
            Debug.Assert(
                scalar1.RowCount == RowCount && scalar1.ColumnCount == ColumnCount &&
                scalar2.RowCount == RowCount && scalar2.ColumnCount == ColumnCount
            );
            
            return DenseMatrix.Create(
                RowCount, 
                ColumnCount, 
                (i, j) => scalar1[i, j] * scalar2[i, j]
            );
        }

        public Matrix Times(params Matrix[] scalarsList)
        {
            Debug.Assert(scalarsList.All(m => 
                m.RowCount == RowCount && m.ColumnCount == ColumnCount
            ));

            var result = scalarsList[0];
            
            return DenseMatrix.Create(
                RowCount, 
                ColumnCount, 
                (i, j) => 
                    scalarsList.Skip(1).Aggregate(result[i, j], (current, matrix) => current * matrix[i, j])
            );
        }

        public Matrix Times(IEnumerable<Matrix> scalarsList)
        {
            Debug.Assert(scalarsList.All(m => 
                m.RowCount == RowCount && m.ColumnCount == ColumnCount
            ));

            var result = GetOne();
            
            return DenseMatrix.Create(
                RowCount, 
                ColumnCount, 
                (i, j) => 
                    scalarsList.Aggregate(result[i, j], (current, matrix) => current * matrix[i, j])
            );
        }

        public Matrix Divide(Matrix scalar1, Matrix scalar2)
        {
            Debug.Assert(
                scalar1.RowCount == RowCount && scalar1.ColumnCount == ColumnCount &&
                scalar2.RowCount == RowCount && scalar2.ColumnCount == ColumnCount
            );
            
            return DenseMatrix.Create(
                RowCount, 
                ColumnCount, 
                (i, j) => scalar1[i, j] / scalar2[i, j]
            );
        }

        public Matrix Positive(Matrix scalar)
        {
            return DenseMatrix.Create(
                RowCount, 
                ColumnCount, 
                (i, j) => scalar[i, j]
            );
        }

        public Matrix Negative(Matrix scalar)
        {
            Debug.Assert(
                scalar.RowCount == RowCount && scalar.ColumnCount == ColumnCount
            );

            return (Matrix)(-scalar);
        }

        public Matrix Inverse(Matrix scalar)
        {
            Debug.Assert(
                scalar.RowCount == RowCount && scalar.ColumnCount == ColumnCount
            );
            
            return DenseMatrix.Create(
                RowCount, 
                ColumnCount, 
                (i, j) => 1.0d / scalar[i, j]
            );
        }

        public bool IsZero(Matrix scalar)
        {
            return scalar.Enumerate().All(s => s == 0.0d);
        }

        public bool IsZero(Matrix scalar, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? scalar.Enumerate().All(s => s > -ZeroEpsilon && s < ZeroEpsilon)
                : scalar.Enumerate().All(s => s == 0d);
        }

        public bool IsNearZero(Matrix scalar)
        {
            return scalar.Enumerate().All(s => s > -ZeroEpsilon && s < ZeroEpsilon);
        }

        public bool IsNotZero(Matrix scalar)
        {
            return scalar.Enumerate().All(s => s != 0d);
        }

        public bool IsNotZero(Matrix scalar, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? scalar.Enumerate().All(s => s <= -ZeroEpsilon || s >= ZeroEpsilon)
                : scalar.Enumerate().All(s => s != 0d);
        }

        public bool IsNotNearZero(Matrix scalar)
        {
            return scalar.Enumerate().All(s => s <= -ZeroEpsilon || s >= ZeroEpsilon);
        }
    }
}