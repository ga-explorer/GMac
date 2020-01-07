namespace GMacBlade.cga5dTest
{
    //public static class TestInputsGenerator
    //{
    //    private static readonly Random _randomGenerator = new Random(DateTime.Now.Millisecond);

    //    private static double GenerateDouble(double minValue = -1.0D, double maxValue = 1.0D)
    //    {
    //        return minValue + _randomGenerator.NextDouble() * (maxValue - minValue);
    //    }

    //    private static double[] GenerateDoubleArray(int length, double minValue = -1.0D, double maxValue = 1.0D)
    //    {
    //        var array = new double[length];

    //        for (var i = 0; i < length; i++)
    //            array[i] = GenerateDouble(minValue, maxValue);

    //        return array;
    //    }

    //    public static int GenerateInteger(int maxValue)
    //    {
    //        return _randomGenerator.Next() % (maxValue + 1);
    //    }

    //    public static int GenerateInteger(int minValue, int maxValue)
    //    {
    //        return minValue + _randomGenerator.Next() % (maxValue - minValue + 1);
    //    }

    //    public static cga5dVector GenerateVector(double minValue = -1.0D, double maxValue = 1.0D)
    //    {
    //        return new cga5dVector(
    //            GenerateDoubleArray(cga5dBlade.MaxGrade, minValue, maxValue)
    //            ); 
    //    }

    //    public static cga5dVector[] GenerateVectorArray(int length, double minValue = -1.0D, double maxValue = 1.0D)
    //    {
    //        var array = new cga5dVector[length];

    //        for (var i = 0; i < length; i++)
    //            array[i] = GenerateVector(minValue, maxValue);

    //        return array;
    //    }

    //    public static bool IsLID(cga5dVector[] vectors)
    //    {
    //        if (vectors.Length < 1 || vectors.Length > cga5dBlade.MaxGrade)
    //            return false;

    //        return cga5dBlade.OP(vectors).IsZero == false;
    //    }

    //    public static cga5dBlade GenerateBlade(int grade, double minValue = -1.0D, double maxValue = 1.0D)
    //    {
    //        while (true)
    //        {
    //            var vectors = GenerateVectorArray(grade, minValue, maxValue);

    //            var blade = cga5dBlade.OP(vectors);

    //            if (blade.IsZero == false)
    //                return blade;
    //        }
    //    }

    //    //TODO: Complete this to a full testing class for cga5d blades
    //}
}
