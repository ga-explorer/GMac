using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraNumericsLib.Outermorphisms
{
    public static class GaNumOutermorphismUtils
    {

        public static GaNumOutermorphism ToOutermorphism(this Matrix linearVectorMapMatrix)
        {
            return GaNumOutermorphism.Create(linearVectorMapMatrix);
        }

        public static GaNumStoredOutermorphism ToOutermorphismSparseColumns(this Matrix linearVectorMapMatrix)
        {
            return GaNumStoredOutermorphism.CreateSparseColumns(linearVectorMapMatrix);
        }

        //public static GaNumStoredOutermorphism ToOutermorphismSparseRows(this Matrix linearVectorMapMatrix)
        //{
        //    return GaNumStoredOutermorphism.CreateSparseRows(linearVectorMapMatrix);
        //}

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


        public static GaNumStoredOutermorphism ToStoredOutermorphism(this Matrix linearVectorMapMatrix)
        {
            return GaNumStoredOutermorphism.Create(linearVectorMapMatrix);
        }
    }
}