namespace GMacBlade.cga5d
{
    /// <summary>
    /// This class represents a mutable outermorphism in the cga5d frame by only storing a 5 by 5
    /// matrix of the original vector linear transform and computing the other k-vectors matrices as needed
    /// </summary>
    public sealed partial class cga5dOutermorphism
    {
        public static double[] Apply_5(double[,] omCoefs, double[] bladeCoefs)
        {
            var coefs = new double[1];
        
            //GMac Generated Processing Code, 14/04/2015 09:59:46 ص
            //Macro: main.cga5d.ApplyOM
            //Input Variables: 26 used, 0 not used, 26 total.
            //Temp Variables: 124 sub-expressions, 0 generated temps, 124 total.
            //Target Temp Variables: 15 total.
            //Output Variables: 1 total.
            //Computations: 1.344 average, 168 total.
            //Memory Reads: 2 average, 250 total.
            //Memory Writes: 125 total.
            
            double[] tempArray = new double[15];
            
            tempArray[0] = (omCoefs[0, 4] * omCoefs[1, 3]);
            tempArray[1] = (-1 * omCoefs[0, 3] * omCoefs[1, 4]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * omCoefs[2, 2] * tempArray[0]);
            tempArray[2] = (omCoefs[0, 4] * omCoefs[2, 3]);
            tempArray[3] = (-1 * omCoefs[0, 3] * omCoefs[2, 4]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (omCoefs[1, 2] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[3] = (omCoefs[1, 4] * omCoefs[2, 3]);
            tempArray[4] = (-1 * omCoefs[1, 3] * omCoefs[2, 4]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * omCoefs[0, 2] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[4]);
            tempArray[4] = (omCoefs[3, 1] * tempArray[1]);
            tempArray[5] = (-1 * omCoefs[3, 2] * tempArray[0]);
            tempArray[6] = (omCoefs[0, 4] * omCoefs[3, 3]);
            tempArray[7] = (-1 * omCoefs[0, 3] * omCoefs[3, 4]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (omCoefs[1, 2] * tempArray[6]);
            tempArray[5] = (tempArray[5] + tempArray[7]);
            tempArray[7] = (omCoefs[1, 4] * omCoefs[3, 3]);
            tempArray[8] = (-1 * omCoefs[1, 3] * omCoefs[3, 4]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * omCoefs[0, 2] * tempArray[7]);
            tempArray[5] = (tempArray[5] + tempArray[8]);
            tempArray[8] = (-1 * omCoefs[2, 1] * tempArray[5]);
            tempArray[4] = (tempArray[4] + tempArray[8]);
            tempArray[8] = (-1 * omCoefs[3, 2] * tempArray[2]);
            tempArray[9] = (omCoefs[2, 2] * tempArray[6]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (omCoefs[2, 4] * omCoefs[3, 3]);
            tempArray[10] = (-1 * omCoefs[2, 3] * omCoefs[3, 4]);
            tempArray[9] = (tempArray[9] + tempArray[10]);
            tempArray[10] = (-1 * omCoefs[0, 2] * tempArray[9]);
            tempArray[8] = (tempArray[8] + tempArray[10]);
            tempArray[10] = (omCoefs[1, 1] * tempArray[8]);
            tempArray[4] = (tempArray[4] + tempArray[10]);
            tempArray[10] = (-1 * omCoefs[3, 2] * tempArray[3]);
            tempArray[11] = (omCoefs[2, 2] * tempArray[7]);
            tempArray[10] = (tempArray[10] + tempArray[11]);
            tempArray[11] = (-1 * omCoefs[1, 2] * tempArray[9]);
            tempArray[10] = (tempArray[10] + tempArray[11]);
            tempArray[11] = (-1 * omCoefs[0, 1] * tempArray[10]);
            tempArray[4] = (tempArray[4] + tempArray[11]);
            tempArray[4] = (-1 * omCoefs[4, 0] * tempArray[4]);
            tempArray[1] = (omCoefs[4, 1] * tempArray[1]);
            tempArray[0] = (-1 * omCoefs[4, 2] * tempArray[0]);
            tempArray[11] = (omCoefs[0, 4] * omCoefs[4, 3]);
            tempArray[12] = (-1 * omCoefs[0, 3] * omCoefs[4, 4]);
            tempArray[11] = (tempArray[11] + tempArray[12]);
            tempArray[12] = (omCoefs[1, 2] * tempArray[11]);
            tempArray[0] = (tempArray[0] + tempArray[12]);
            tempArray[12] = (omCoefs[1, 4] * omCoefs[4, 3]);
            tempArray[13] = (-1 * omCoefs[1, 3] * omCoefs[4, 4]);
            tempArray[12] = (tempArray[12] + tempArray[13]);
            tempArray[13] = (-1 * omCoefs[0, 2] * tempArray[12]);
            tempArray[0] = (tempArray[0] + tempArray[13]);
            tempArray[13] = (-1 * omCoefs[2, 1] * tempArray[0]);
            tempArray[1] = (tempArray[1] + tempArray[13]);
            tempArray[2] = (-1 * omCoefs[4, 2] * tempArray[2]);
            tempArray[13] = (omCoefs[2, 2] * tempArray[11]);
            tempArray[2] = (tempArray[2] + tempArray[13]);
            tempArray[13] = (omCoefs[2, 4] * omCoefs[4, 3]);
            tempArray[14] = (-1 * omCoefs[2, 3] * omCoefs[4, 4]);
            tempArray[13] = (tempArray[13] + tempArray[14]);
            tempArray[14] = (-1 * omCoefs[0, 2] * tempArray[13]);
            tempArray[2] = (tempArray[2] + tempArray[14]);
            tempArray[14] = (omCoefs[1, 1] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[14]);
            tempArray[3] = (-1 * omCoefs[4, 2] * tempArray[3]);
            tempArray[14] = (omCoefs[2, 2] * tempArray[12]);
            tempArray[3] = (tempArray[3] + tempArray[14]);
            tempArray[14] = (-1 * omCoefs[1, 2] * tempArray[13]);
            tempArray[3] = (tempArray[3] + tempArray[14]);
            tempArray[14] = (-1 * omCoefs[0, 1] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[14]);
            tempArray[1] = (omCoefs[3, 0] * tempArray[1]);
            tempArray[1] = (tempArray[4] + tempArray[1]);
            tempArray[4] = (omCoefs[4, 1] * tempArray[5]);
            tempArray[0] = (-1 * omCoefs[3, 1] * tempArray[0]);
            tempArray[0] = (tempArray[4] + tempArray[0]);
            tempArray[4] = (-1 * omCoefs[4, 2] * tempArray[6]);
            tempArray[5] = (omCoefs[3, 2] * tempArray[11]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (omCoefs[3, 4] * omCoefs[4, 3]);
            tempArray[6] = (-1 * omCoefs[3, 3] * omCoefs[4, 4]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (-1 * omCoefs[0, 2] * tempArray[5]);
            tempArray[4] = (tempArray[4] + tempArray[6]);
            tempArray[6] = (omCoefs[1, 1] * tempArray[4]);
            tempArray[0] = (tempArray[0] + tempArray[6]);
            tempArray[6] = (-1 * omCoefs[4, 2] * tempArray[7]);
            tempArray[7] = (omCoefs[3, 2] * tempArray[12]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * omCoefs[1, 2] * tempArray[5]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * omCoefs[0, 1] * tempArray[6]);
            tempArray[0] = (tempArray[0] + tempArray[7]);
            tempArray[0] = (-1 * omCoefs[2, 0] * tempArray[0]);
            tempArray[0] = (tempArray[1] + tempArray[0]);
            tempArray[1] = (omCoefs[4, 1] * tempArray[8]);
            tempArray[2] = (-1 * omCoefs[3, 1] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (omCoefs[2, 1] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (-1 * omCoefs[4, 2] * tempArray[9]);
            tempArray[4] = (omCoefs[3, 2] * tempArray[13]);
            tempArray[2] = (tempArray[2] + tempArray[4]);
            tempArray[4] = (-1 * omCoefs[2, 2] * tempArray[5]);
            tempArray[2] = (tempArray[2] + tempArray[4]);
            tempArray[4] = (-1 * omCoefs[0, 1] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[4]);
            tempArray[1] = (omCoefs[1, 0] * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (omCoefs[4, 1] * tempArray[10]);
            tempArray[3] = (-1 * omCoefs[3, 1] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[3] = (omCoefs[2, 1] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[2] = (-1 * omCoefs[1, 1] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[1] = (-1 * omCoefs[0, 0] * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            coefs[0] = (bladeCoefs[0] * tempArray[0]);
            
            return coefs;
        }
        
    }
}
