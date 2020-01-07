namespace GMacBlade.cga5d
{
    /// <summary>
    /// This class represents an immutable blade in the cga5d frame with arbitrary grade 
    /// (i.e. grade is determined at runtime) based on additive representation of the blade as a 
    /// linear combination of basis blades of the same grade (i.e. it's actually a k-vector representation).
    /// </summary>
    public sealed partial class cga5dBlade
    {
        private static double[] EGPDual_203(double[] coefs1, double[] coefs2)
        {
            var c = new double[10];
        
            //GMac Generated Processing Code, 14/04/2015 09:55:15 ص
            //Macro: main.cga5d.EGPDual
            //Input Variables: 11 used, 0 not used, 11 total.
            //Temp Variables: 6 sub-expressions, 0 generated temps, 6 total.
            //Target Temp Variables: 1 total.
            //Output Variables: 10 total.
            //Computations: 1.625 average, 26 total.
            //Memory Reads: 1.625 average, 26 total.
            //Memory Writes: 16 total.
            
            double tempVar0000;
            
            c[1] = (-1 * coefs1[8] * coefs2[0]);
            c[3] = (-1 * coefs1[6] * coefs2[0]);
            c[5] = (-1 * coefs1[4] * coefs2[0]);
            c[8] = (-1 * coefs1[1] * coefs2[0]);
            tempVar0000 = (-1 * coefs1[9] * coefs2[0]);
            c[0] = (-1 * tempVar0000);
            tempVar0000 = (-1 * coefs1[7] * coefs2[0]);
            c[2] = (-1 * tempVar0000);
            tempVar0000 = (-1 * coefs1[5] * coefs2[0]);
            c[4] = (-1 * tempVar0000);
            tempVar0000 = (-1 * coefs1[3] * coefs2[0]);
            c[6] = (-1 * tempVar0000);
            tempVar0000 = (-1 * coefs1[2] * coefs2[0]);
            c[7] = (-1 * tempVar0000);
            tempVar0000 = (-1 * coefs1[0] * coefs2[0]);
            c[9] = (-1 * tempVar0000);
            
            return c;
        }
        
    }
}
