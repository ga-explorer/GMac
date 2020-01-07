namespace GMacBlade.cga5d
{
    /// <summary>
    /// This class represents an immutable blade in the cga5d frame with arbitrary grade 
    /// (i.e. grade is determined at runtime) based on additive representation of the blade as a 
    /// linear combination of basis blades of the same grade (i.e. it's actually a k-vector representation).
    /// </summary>
    public sealed partial class cga5dBlade
    {
        private static double[] EGP_314(double[] coefs1, double[] coefs2)
        {
            var c = new double[5];
        
            //GMac Generated Processing Code, 14/04/2015 09:55:07 ص
            //Macro: main.cga5d.EGP
            //Input Variables: 15 used, 0 not used, 15 total.
            //Temp Variables: 30 sub-expressions, 0 generated temps, 30 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 5 total.
            //Computations: 1.28571428571429 average, 45 total.
            //Memory Reads: 2 average, 70 total.
            //Memory Writes: 35 total.
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = (coefs1[3] * coefs2[0]);
            tempVar0001 = (-1 * coefs1[2] * coefs2[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (coefs1[1] * coefs2[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * coefs1[0] * coefs2[3]);
            c[0] = (tempVar0000 + tempVar0001);
            tempVar0000 = (coefs1[6] * coefs2[0]);
            tempVar0001 = (-1 * coefs1[5] * coefs2[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (coefs1[4] * coefs2[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * coefs1[0] * coefs2[4]);
            c[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (coefs1[8] * coefs2[0]);
            tempVar0001 = (-1 * coefs1[7] * coefs2[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (coefs1[4] * coefs2[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * coefs1[1] * coefs2[4]);
            c[2] = (tempVar0000 + tempVar0001);
            tempVar0000 = (coefs1[9] * coefs2[0]);
            tempVar0001 = (-1 * coefs1[7] * coefs2[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (coefs1[5] * coefs2[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * coefs1[2] * coefs2[4]);
            c[3] = (tempVar0000 + tempVar0001);
            tempVar0000 = (coefs1[9] * coefs2[1]);
            tempVar0001 = (-1 * coefs1[8] * coefs2[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (coefs1[6] * coefs2[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * coefs1[3] * coefs2[4]);
            c[4] = (tempVar0000 + tempVar0001);
            
            return c;
        }
        
    }
}
