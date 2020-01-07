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
        private static cga5dVector[] Factor5(double[] coefs)
        {
            var vectors = new[] 
            {
                new cga5dVector(),
                new cga5dVector()
            };
        
            //GMac Generated Processing Code, 14/04/2015 09:58:23 ص
            //Macro: main.cga5d.Factor2
            //Input Variables: 10 used, 2 not used, 12 total.
            //Temp Variables: 106 sub-expressions, 0 generated temps, 106 total.
            //Target Temp Variables: 13 total.
            //Output Variables: 10 total.
            //Computations: 1.23275862068966 average, 143 total.
            //Memory Reads: 2.02586206896552 average, 235 total.
            //Memory Writes: 116 total.
            
            double[] tempArray = new double[13];
            
            tempArray[0] = Math.Pow(2, -0.5);
            tempArray[1] = (-1 * coefs[3] * tempArray[0]);
            tempArray[2] = (coefs[9] * tempArray[0]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[3] = (-1 * tempArray[0] * tempArray[1]);
            tempArray[4] = (coefs[3] * tempArray[0]);
            tempArray[2] = (tempArray[2] + tempArray[4]);
            tempArray[4] = (tempArray[0] * tempArray[2]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * coefs[1] * tempArray[0]);
            tempArray[5] = (coefs[8] * tempArray[0]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[6] = (-1 * tempArray[0] * tempArray[4]);
            tempArray[7] = (coefs[1] * tempArray[0]);
            tempArray[5] = (tempArray[5] + tempArray[7]);
            tempArray[7] = (tempArray[0] * tempArray[5]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * coefs[0] * tempArray[0]);
            tempArray[8] = (coefs[7] * tempArray[0]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[9] = (coefs[0] * tempArray[0]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (coefs[6] * tempArray[0] * tempArray[7]);
            tempArray[10] = (coefs[6] * tempArray[0] * tempArray[8]);
            tempArray[9] = (tempArray[9] + tempArray[10]);
            tempArray[10] = (coefs[4] * tempArray[3]);
            tempArray[9] = (tempArray[9] + tempArray[10]);
            tempArray[10] = (coefs[2] * tempArray[6]);
            vectors[0].C2 = (tempArray[9] + tempArray[10]);
            tempArray[9] = (tempArray[0] * tempArray[7]);
            tempArray[10] = (-1 * tempArray[0] * tempArray[8]);
            tempArray[9] = (tempArray[9] + tempArray[10]);
            tempArray[10] = (-1 * coefs[6] * tempArray[0] * tempArray[4]);
            tempArray[11] = (-1 * coefs[6] * tempArray[0] * tempArray[5]);
            tempArray[10] = (tempArray[10] + tempArray[11]);
            tempArray[3] = (-1 * coefs[5] * tempArray[3]);
            tempArray[3] = (tempArray[10] + tempArray[3]);
            tempArray[10] = (coefs[2] * tempArray[9]);
            vectors[0].C3 = (tempArray[3] + tempArray[10]);
            tempArray[3] = (-1 * coefs[6] * tempArray[0] * tempArray[1]);
            tempArray[10] = (-1 * coefs[6] * tempArray[0] * tempArray[2]);
            tempArray[3] = (tempArray[3] + tempArray[10]);
            tempArray[6] = (coefs[5] * tempArray[6]);
            tempArray[3] = (tempArray[3] + tempArray[6]);
            tempArray[6] = (coefs[4] * tempArray[9]);
            vectors[0].C4 = (tempArray[3] + tempArray[6]);
            tempArray[3] = Math.Pow(coefs[6], 2);
            tempArray[6] = (-1 * tempArray[0] * tempArray[3]);
            tempArray[9] = (tempArray[1] * tempArray[3]);
            tempArray[6] = (tempArray[6] + tempArray[9]);
            tempArray[9] = (tempArray[4] * tempArray[6]);
            tempArray[6] = (tempArray[6] + tempArray[9]);
            tempArray[9] = (tempArray[7] * tempArray[9]);
            tempArray[6] = (tempArray[6] + tempArray[9]);
            tempArray[6] = (tempArray[0] * tempArray[6]);
            tempArray[3] = (tempArray[0] * tempArray[3]);
            tempArray[9] = (tempArray[2] * tempArray[3]);
            tempArray[3] = (tempArray[3] + tempArray[9]);
            tempArray[9] = (tempArray[5] * tempArray[6]);
            tempArray[3] = (tempArray[3] + tempArray[9]);
            tempArray[9] = (tempArray[8] * tempArray[9]);
            tempArray[3] = (tempArray[3] + tempArray[9]);
            tempArray[9] = (-1 * tempArray[0] * tempArray[3]);
            vectors[0].C1 = (tempArray[6] + tempArray[9]);
            tempArray[3] = (tempArray[0] * tempArray[3]);
            vectors[0].C5 = (tempArray[6] + tempArray[3]);
            tempArray[3] = (tempArray[3] + tempArray[6]);
            tempArray[6] = (tempArray[3] + tempArray[10]);
            tempArray[9] = (tempArray[6] + tempArray[9]);
            tempArray[10] = (-1 * tempArray[0] * tempArray[9]);
            tempArray[11] = (tempArray[6] + tempArray[3]);
            tempArray[11] = (tempArray[0] * tempArray[11]);
            tempArray[10] = (tempArray[10] + tempArray[11]);
            tempArray[9] = (tempArray[0] * tempArray[9]);
            tempArray[9] = (tempArray[11] + tempArray[9]);
            tempArray[11] = (coefs[4] * tempArray[3]);
            tempArray[12] = (coefs[2] * tempArray[6]);
            tempArray[11] = (tempArray[11] + tempArray[12]);
            tempArray[7] = (tempArray[7] * tempArray[9]);
            tempArray[7] = (tempArray[11] + tempArray[7]);
            tempArray[8] = (tempArray[8] * tempArray[10]);
            vectors[1].C2 = (tempArray[7] + tempArray[8]);
            tempArray[7] = (tempArray[9] + tempArray[10]);
            tempArray[3] = (-1 * coefs[5] * tempArray[3]);
            tempArray[8] = (coefs[2] * tempArray[7]);
            tempArray[3] = (tempArray[3] + tempArray[8]);
            tempArray[4] = (-1 * tempArray[4] * tempArray[9]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * tempArray[5] * tempArray[10]);
            vectors[1].C3 = (tempArray[3] + tempArray[4]);
            tempArray[3] = (coefs[5] * tempArray[6]);
            tempArray[4] = (coefs[4] * tempArray[7]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[1] = (-1 * tempArray[1] * tempArray[9]);
            tempArray[1] = (tempArray[3] + tempArray[1]);
            tempArray[2] = (-1 * tempArray[2] * tempArray[10]);
            vectors[1].C4 = (tempArray[1] + tempArray[2]);
            tempArray[1] = (tempArray[1] * tempArray[3]);
            tempArray[2] = (tempArray[4] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (tempArray[7] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (-1 * coefs[6] * tempArray[10]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[1] = (tempArray[0] * tempArray[1]);
            tempArray[2] = (tempArray[2] * tempArray[3]);
            tempArray[3] = (tempArray[5] * tempArray[6]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (tempArray[8] * tempArray[7]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (coefs[6] * tempArray[9]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * tempArray[0] * tempArray[2]);
            vectors[1].C1 = (tempArray[1] + tempArray[3]);
            tempArray[0] = (tempArray[0] * tempArray[2]);
            vectors[1].C5 = (tempArray[1] + tempArray[0]);
            
        
            return vectors;
        }
        
    }
}
