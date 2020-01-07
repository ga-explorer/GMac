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
        private static cga5dVector[] Factor31(double[] coefs)
        {
            var vectors = new[] 
            {
                new cga5dVector(),
                new cga5dVector(),
                new cga5dVector(),
                new cga5dVector(),
                new cga5dVector()
            };
        
            //GMac Generated Processing Code, 14/04/2015 09:59:37 ص
            //Macro: main.cga5d.Factor5
            //Input Variables: 1 used, 5 not used, 6 total.
            //Temp Variables: 1 sub-expressions, 0 generated temps, 1 total.
            //Target Temp Variables: 1 total.
            //Output Variables: 25 total.
            //Computations: 0.0769230769230769 average, 2 total.
            //Memory Reads: 0.0769230769230769 average, 2 total.
            //Memory Writes: 26 total.
            
            double tempVar0000;
            
            vectors[0].C2 = 0;
            vectors[0].C3 = 0;
            vectors[0].C4 = 0;
            vectors[0].C5 = 0;
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
            vectors[4].C1 = 0;
            vectors[4].C2 = 0;
            vectors[4].C3 = 0;
            vectors[4].C4 = 0;
            vectors[4].C5 = 0;
            tempVar0000 = Math.Pow(coefs[0], 2);
            vectors[0].C1 = (-1 * tempVar0000);
            
        
            return vectors;
        }
        
    }
}
