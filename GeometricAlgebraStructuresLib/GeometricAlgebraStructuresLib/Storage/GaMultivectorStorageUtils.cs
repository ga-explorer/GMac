namespace GeometricAlgebraStructuresLib.Storage
{
    public static class GaMultivectorStorageUtils
    {
        public static T[] GetCopyOfArray<T>(this T[] inputArray)
        {
            var outputArray = new T[inputArray.Length];

            inputArray.CopyTo(outputArray, 0);
            
            return outputArray;
        }
    }
}