namespace GMacBlade.cga5d
{
    /// <summary>
    /// This class represents a mutable outermorphism in the cga5d frame by only storing a 5 by 5
    /// matrix of the original vector linear transform and computing the other k-vectors matrices as needed
    /// </summary>
    public sealed partial class cga5dOutermorphism
    {
        public static double[] Apply_1(double[,] omCoefs, double[] bladeCoefs)
        {
            var coefs = new double[5];
        
            //GMac Generated Processing Code, 14/04/2015 09:59:39 ص
            //Macro: main.cga5d.ApplyOM
            //Input Variables: 30 used, 0 not used, 30 total.
            //Temp Variables: 40 sub-expressions, 0 generated temps, 40 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 5 total.
            //Computations: 1 average, 45 total.
            //Memory Reads: 2 average, 90 total.
            //Memory Writes: 45 total.
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = (bladeCoefs[0] * omCoefs[0, 0]);
            tempVar0001 = (bladeCoefs[1] * omCoefs[0, 1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (bladeCoefs[2] * omCoefs[0, 2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (bladeCoefs[3] * omCoefs[0, 3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (bladeCoefs[4] * omCoefs[0, 4]);
            coefs[0] = (tempVar0000 + tempVar0001);
            tempVar0000 = (bladeCoefs[0] * omCoefs[1, 0]);
            tempVar0001 = (bladeCoefs[1] * omCoefs[1, 1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (bladeCoefs[2] * omCoefs[1, 2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (bladeCoefs[3] * omCoefs[1, 3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (bladeCoefs[4] * omCoefs[1, 4]);
            coefs[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (bladeCoefs[0] * omCoefs[2, 0]);
            tempVar0001 = (bladeCoefs[1] * omCoefs[2, 1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (bladeCoefs[2] * omCoefs[2, 2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (bladeCoefs[3] * omCoefs[2, 3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (bladeCoefs[4] * omCoefs[2, 4]);
            coefs[2] = (tempVar0000 + tempVar0001);
            tempVar0000 = (bladeCoefs[0] * omCoefs[3, 0]);
            tempVar0001 = (bladeCoefs[1] * omCoefs[3, 1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (bladeCoefs[2] * omCoefs[3, 2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (bladeCoefs[3] * omCoefs[3, 3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (bladeCoefs[4] * omCoefs[3, 4]);
            coefs[3] = (tempVar0000 + tempVar0001);
            tempVar0000 = (bladeCoefs[0] * omCoefs[4, 0]);
            tempVar0001 = (bladeCoefs[1] * omCoefs[4, 1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (bladeCoefs[2] * omCoefs[4, 2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (bladeCoefs[3] * omCoefs[4, 3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (bladeCoefs[4] * omCoefs[4, 4]);
            coefs[4] = (tempVar0000 + tempVar0001);
            
            return coefs;
        }
        
    }
}
