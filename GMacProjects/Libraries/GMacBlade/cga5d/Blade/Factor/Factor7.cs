using System;

namespace GMacBlade.cga5d
{
    /// <summary>
    /// This class represents an immutable blade in the cga5d frame with arbitrary grade 
    /// (i.e. grade is determined at runtime) based on additive representation of the blade as a 
    /// linear combination of basis blades of the same grade (i.e. it's actually a k-vector representation).
    /// </summary>
    public sealed partial class cga5dBlade
    {
        private static cga5dVector[] Factor7(double[] coefs)
        {
            var vectors = new[] 
            {
                new cga5dVector(),
                new cga5dVector(),
                new cga5dVector()
            };
        
            //GMac Generated Processing Code, 14/04/2015 09:58:43 ص
            //Macro: main.cga5d.Factor3
            //Input Variables: 10 used, 3 not used, 13 total.
            //Temp Variables: 77 sub-expressions, 0 generated temps, 77 total.
            //Target Temp Variables: 10 total.
            //Output Variables: 15 total.
            //Computations: 1.23913043478261 average, 114 total.
            //Memory Reads: 1.85869565217391 average, 171 total.
            //Memory Writes: 92 total.
            
            double[] tempArray = new double[10];
            
            vectors[1].C1 = 0;
            vectors[1].C2 = 0;
            vectors[1].C3 = 0;
            vectors[1].C4 = 0;
            vectors[1].C5 = 0;
            vectors[2].C1 = 0;
            vectors[2].C2 = 0;
            vectors[2].C3 = 0;
            vectors[2].C4 = 0;
            vectors[2].C5 = 0;
            tempArray[0] = Math.Pow(2, -0.5);
            tempArray[1] = (-1 * coefs[2] * tempArray[0]);
            tempArray[2] = (-1 * coefs[9] * tempArray[0]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[3] = (-1 * tempArray[0] * tempArray[1]);
            tempArray[4] = (coefs[2] * tempArray[0]);
            tempArray[2] = (tempArray[2] + tempArray[4]);
            tempArray[4] = (tempArray[0] * tempArray[2]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * coefs[1] * tempArray[0]);
            tempArray[5] = (-1 * coefs[8] * tempArray[0]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[6] = (coefs[1] * tempArray[0]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (-1 * coefs[0] * tempArray[0]);
            tempArray[7] = (-1 * coefs[6] * tempArray[0]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[8] = (coefs[0] * tempArray[0]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * coefs[7] * tempArray[0] * tempArray[4]);
            tempArray[9] = (-1 * coefs[7] * tempArray[0] * tempArray[5]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * coefs[5] * tempArray[0] * tempArray[6]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * coefs[5] * tempArray[0] * tempArray[7]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[3] = (coefs[3] * tempArray[3]);
            vectors[0].C2 = (tempArray[8] + tempArray[3]);
            tempArray[3] = (tempArray[0] * tempArray[4]);
            tempArray[8] = (-1 * tempArray[0] * tempArray[5]);
            tempArray[3] = (tempArray[3] + tempArray[8]);
            tempArray[8] = (coefs[7] * tempArray[0] * tempArray[1]);
            tempArray[9] = (coefs[7] * tempArray[0] * tempArray[2]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[6] = (coefs[4] * tempArray[0] * tempArray[6]);
            tempArray[6] = (tempArray[8] + tempArray[6]);
            tempArray[7] = (coefs[4] * tempArray[0] * tempArray[7]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[3] = (coefs[3] * tempArray[3]);
            vectors[0].C3 = (tempArray[6] + tempArray[3]);
            tempArray[3] = (tempArray[0] * tempArray[6]);
            tempArray[6] = (-1 * tempArray[0] * tempArray[7]);
            tempArray[3] = (tempArray[3] + tempArray[6]);
            tempArray[1] = (-1 * coefs[5] * tempArray[0] * tempArray[1]);
            tempArray[2] = (-1 * coefs[5] * tempArray[0] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (coefs[4] * tempArray[0] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (coefs[4] * tempArray[0] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (-1 * coefs[3] * tempArray[3]);
            vectors[0].C4 = (tempArray[1] + tempArray[2]);
            tempArray[1] = Math.Pow(coefs[4], 2);
            tempArray[2] = (tempArray[0] * tempArray[1]);
            tempArray[3] = Math.Pow(coefs[5], 2);
            tempArray[4] = (-1 * tempArray[0] * tempArray[3]);
            tempArray[2] = (tempArray[2] + tempArray[4]);
            tempArray[4] = Math.Pow(coefs[7], 2);
            tempArray[5] = (-1 * tempArray[0] * tempArray[4]);
            tempArray[2] = (tempArray[2] + tempArray[5]);
            tempArray[5] = (tempArray[1] * tempArray[3]);
            tempArray[2] = (tempArray[2] + tempArray[5]);
            tempArray[5] = (tempArray[4] * tempArray[3]);
            tempArray[2] = (tempArray[2] + tempArray[5]);
            tempArray[5] = (tempArray[6] * tempArray[3]);
            tempArray[2] = (tempArray[2] + tempArray[5]);
            tempArray[2] = (tempArray[0] * tempArray[2]);
            tempArray[1] = (-1 * tempArray[0] * tempArray[1]);
            tempArray[3] = (tempArray[0] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[3] = (tempArray[0] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[3] = (tempArray[2] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[3] = (tempArray[5] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[3] = (tempArray[7] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[3] = (-1 * tempArray[0] * tempArray[1]);
            vectors[0].C1 = (tempArray[2] + tempArray[3]);
            tempArray[0] = (tempArray[0] * tempArray[1]);
            vectors[0].C5 = (tempArray[2] + tempArray[0]);
            
        
            return vectors;
        }
        
    }
}
