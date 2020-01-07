namespace GMacBlade.cga5d
{
    /// <summary>
    /// This class represents an immutable blade in the cga5d frame with arbitrary grade 
    /// (i.e. grade is determined at runtime) based on additive representation of the blade as a 
    /// linear combination of basis blades of the same grade (i.e. it's actually a k-vector representation).
    /// </summary>
    public sealed partial class cga5dBlade
    {
        private static double[] ApplyERotor_522(double[] coefs1, double[] coefs2)
        {
            var c = new double[10];
        
            //GMac Generated Processing Code, 14/04/2015 09:58:19 ص
            //Macro: main.cga5d.ApplyERotor
            //Input Variables: 11 used, 0 not used, 11 total.
            //Temp Variables: 10 sub-expressions, 0 generated temps, 10 total.
            //Target Temp Variables: 1 total.
            //Output Variables: 10 total.
            //Computations: 1.4 average, 28 total.
            //Memory Reads: 2 average, 40 total.
            //Memory Writes: 20 total.
            
            double tempVar0000;
            
            tempVar0000 = (coefs1[0] * coefs2[0]);
            c[0] = (coefs1[0] * tempVar0000);
            tempVar0000 = (-1 * coefs1[0] * coefs2[1]);
            c[1] = (-1 * coefs1[0] * tempVar0000);
            tempVar0000 = (coefs1[0] * coefs2[2]);
            c[2] = (coefs1[0] * tempVar0000);
            tempVar0000 = (coefs1[0] * coefs2[3]);
            c[3] = (coefs1[0] * tempVar0000);
            tempVar0000 = (-1 * coefs1[0] * coefs2[4]);
            c[4] = (-1 * coefs1[0] * tempVar0000);
            tempVar0000 = (coefs1[0] * coefs2[5]);
            c[5] = (coefs1[0] * tempVar0000);
            tempVar0000 = (-1 * coefs1[0] * coefs2[6]);
            c[6] = (-1 * coefs1[0] * tempVar0000);
            tempVar0000 = (coefs1[0] * coefs2[7]);
            c[7] = (coefs1[0] * tempVar0000);
            tempVar0000 = (-1 * coefs1[0] * coefs2[8]);
            c[8] = (-1 * coefs1[0] * tempVar0000);
            tempVar0000 = (coefs1[0] * coefs2[9]);
            c[9] = (coefs1[0] * tempVar0000);
            
            return c;
        }
        
    }
}
