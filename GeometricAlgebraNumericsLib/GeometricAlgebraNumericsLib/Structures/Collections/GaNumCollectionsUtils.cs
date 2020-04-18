using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraNumericsLib.Structures.Collections
{
    public static class GaNumCollectionsUtils
    {
        public static GaNumReadOnlyMatrixColumn CreateReadOnlyColumn(this Matrix baseMatrix, int baseMatrixColumn)
        {
            return new GaNumReadOnlyMatrixColumn(baseMatrix, baseMatrixColumn);
        }

        public static GaNumReadOnlyMatrixRow CreateReadOnlyRow(this Matrix baseMatrix, int baseMatrixRow)
        {
            return new GaNumReadOnlyMatrixRow(baseMatrix, baseMatrixRow);
        }

    }
}
