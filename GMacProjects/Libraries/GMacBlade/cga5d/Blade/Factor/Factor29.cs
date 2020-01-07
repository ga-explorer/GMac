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
        private static cga5dVector[] Factor29(double[] coefs)
        {
            var vectors = new[] 
            {
                new cga5dVector(),
                new cga5dVector(),
                new cga5dVector(),
                new cga5dVector()
            };
        
            //GMac Generated Processing Code, 14/04/2015 09:59:31 ص
            //Macro: main.cga5d.Factor4
            //Input Variables: 5 used, 4 not used, 9 total.
            //Temp Variables: 35 sub-expressions, 0 generated temps, 35 total.
            //Target Temp Variables: 7 total.
            //Output Variables: 20 total.
            //Computations: 1.03636363636364 average, 57 total.
            //Memory Reads: 1.47272727272727 average, 81 total.
            //Memory Writes: 55 total.
            
            double[] tempArray = new double[7];
            
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
            vectors[3].C1 = 0;
            vectors[3].C2 = 0;
            vectors[3].C3 = 0;
            vectors[3].C4 = 0;
            vectors[3].C5 = 0;
            tempArray[0] = Math.Pow(2, -0.5);
            tempArray[1] = (coefs[0] * tempArray[0]);
            tempArray[2] = (-1 * coefs[4] * tempArray[0]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[3] = (-1 * coefs[0] * tempArray[0]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (coefs[3] * tempArray[0] * tempArray[1]);
            tempArray[4] = (coefs[3] * tempArray[0] * tempArray[2]);
            vectors[0].C2 = (tempArray[3] + tempArray[4]);
            tempArray[3] = (-1 * coefs[2] * tempArray[0] * tempArray[1]);
            tempArray[4] = (-1 * coefs[2] * tempArray[0] * tempArray[2]);
            vectors[0].C3 = (tempArray[3] + tempArray[4]);
            tempArray[1] = (coefs[1] * tempArray[0] * tempArray[1]);
            tempArray[2] = (coefs[1] * tempArray[0] * tempArray[2]);
            vectors[0].C4 = (tempArray[1] + tempArray[2]);
            tempArray[1] = Math.Pow(coefs[1], 2);
            tempArray[2] = (-1 * tempArray[0] * tempArray[1]);
            tempArray[3] = Math.Pow(coefs[2], 2);
            tempArray[4] = (-1 * tempArray[0] * tempArray[3]);
            tempArray[2] = (tempArray[2] + tempArray[4]);
            tempArray[4] = Math.Pow(coefs[3], 2);
            tempArray[5] = (tempArray[0] * tempArray[4]);
            tempArray[2] = (tempArray[2] + tempArray[5]);
            tempArray[5] = (tempArray[0] * tempArray[1]);
            tempArray[6] = (-1 * tempArray[0] * tempArray[2]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (-1 * tempArray[1] * tempArray[5]);
            tempArray[2] = (tempArray[2] + tempArray[6]);
            tempArray[2] = (tempArray[0] * tempArray[2]);
            tempArray[1] = (tempArray[0] * tempArray[1]);
            tempArray[3] = (tempArray[0] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[3] = (-1 * tempArray[0] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[3] = (-1 * tempArray[2] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[3] = (-1 * tempArray[0] * tempArray[1]);
            vectors[0].C1 = (tempArray[2] + tempArray[3]);
            tempArray[0] = (tempArray[0] * tempArray[1]);
            vectors[0].C5 = (tempArray[2] + tempArray[0]);
            
        
            return vectors;
        }
        
    }
}
