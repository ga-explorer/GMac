namespace GMacBlade.cga5d
{
    /// <summary>
    /// This class represents an immutable blade in the cga5d frame with arbitrary grade 
    /// (i.e. grade is determined at runtime) based on additive representation of the blade as a 
    /// linear combination of basis blades of the same grade (i.e. it's actually a k-vector representation).
    /// </summary>
    public sealed partial class cga5dBlade
    {
        private static cga5dVector[] Factor10(double[] coefs)
        {
            var vectors = new[] 
            {
                new cga5dVector(),
                new cga5dVector()
            };
        
            //GMac Generated Processing Code, 14/04/2015 09:58:29 ص
            //Macro: main.cga5d.Factor2
            //Input Variables: 0 used, 12 not used, 12 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 10 total.
            //Computations: 0 average, 0 total.
            //Memory Reads: 0 average, 0 total.
            //Memory Writes: 10 total.
            
            
            vectors[0].C1 = 0;
            vectors[0].C2 = 0;
            vectors[0].C3 = 0;
            vectors[0].C4 = 0;
            vectors[0].C5 = 0;
            vectors[1].C1 = 0;
            vectors[1].C2 = 0;
            vectors[1].C3 = 0;
            vectors[1].C4 = 0;
            vectors[1].C5 = 0;
            
        
            return vectors;
        }
        
    }
}
