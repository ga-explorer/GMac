namespace GMacBlade.cga5d
{
    /// <summary>
    /// This class represents an immutable blade in the cga5d frame with arbitrary grade 
    /// (i.e. grade is determined at runtime) based on additive representation of the blade as a 
    /// linear combination of basis blades of the same grade (i.e. it's actually a k-vector representation).
    /// </summary>
    public sealed partial class cga5dBlade
    {
        private static double[] ESP_110(double[] coefs1, double[] coefs2)
        {
            var c = new double[1];
        
            //GMac Generated Processing Code, 14/04/2015 09:54:12 ص
            //Macro: main.cga5d.ESP
            //Input Variables: 10 used, 0 not used, 10 total.
            //Temp Variables: 8 sub-expressions, 0 generated temps, 8 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 1 total.
            //Computations: 1.55555555555556 average, 14 total.
            //Memory Reads: 2 average, 18 total.
            //Memory Writes: 9 total.
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = (-1 * coefs1[0] * coefs2[0]);
            tempVar0001 = (-1 * coefs1[1] * coefs2[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * coefs1[2] * coefs2[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * coefs1[3] * coefs2[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * coefs1[4] * coefs2[4]);
            c[0] = (tempVar0000 + tempVar0001);
            
            return c;
        }
        
    }
}
