namespace GMacBlade.cga5d
{
    /// <summary>
    /// This class represents an immutable blade in the cga5d frame with arbitrary grade 
    /// (i.e. grade is determined at runtime) based on additive representation of the blade as a 
    /// linear combination of basis blades of the same grade (i.e. it's actually a k-vector representation).
    /// </summary>
    public sealed partial class cga5dBlade
    {
        private static double[] SelfEGP_334(double[] coefs1, double[] coefs2)
        {
            var c = new double[5];
        
            //GMac Generated Processing Code, 14/04/2015 09:55:47 ص
            //Macro: main.cga5d.SelfEGP
            //Input Variables: 10 used, 0 not used, 10 total.
            //Temp Variables: 20 sub-expressions, 0 generated temps, 20 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 5 total.
            //Computations: 1.6 average, 40 total.
            //Memory Reads: 2 average, 50 total.
            //Memory Writes: 25 total.
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = (-2 * coefs1[6] * coefs1[7]);
            tempVar0001 = (2 * coefs1[5] * coefs1[8]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-2 * coefs1[4] * coefs1[9]);
            c[0] = (tempVar0000 + tempVar0001);
            tempVar0000 = (2 * coefs1[3] * coefs1[7]);
            tempVar0001 = (-2 * coefs1[2] * coefs1[8]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (2 * coefs1[1] * coefs1[9]);
            c[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-2 * coefs1[3] * coefs1[5]);
            tempVar0001 = (2 * coefs1[2] * coefs1[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-2 * coefs1[0] * coefs1[9]);
            c[2] = (tempVar0000 + tempVar0001);
            tempVar0000 = (2 * coefs1[3] * coefs1[4]);
            tempVar0001 = (-2 * coefs1[1] * coefs1[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (2 * coefs1[0] * coefs1[8]);
            c[3] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-2 * coefs1[2] * coefs1[4]);
            tempVar0001 = (2 * coefs1[1] * coefs1[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-2 * coefs1[0] * coefs1[7]);
            c[4] = (tempVar0000 + tempVar0001);
            
            return c;
        }
        
    }
}
