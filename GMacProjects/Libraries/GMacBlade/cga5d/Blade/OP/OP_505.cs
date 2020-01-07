namespace GMacBlade.cga5d
{
    /// <summary>
    /// This class represents an immutable blade in the cga5d frame with arbitrary grade 
    /// (i.e. grade is determined at runtime) based on additive representation of the blade as a 
    /// linear combination of basis blades of the same grade (i.e. it's actually a k-vector representation).
    /// </summary>
    public sealed partial class cga5dBlade
    {
        private static double[] OP_505(double[] coefs1, double[] coefs2)
        {
            var c = new double[1];
        
            //GMac Generated Processing Code, 14/04/2015 09:53:39 ص
            //Macro: main.cga5d.OP
            //Input Variables: 2 used, 0 not used, 2 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 1 total.
            //Computations: 2 average, 2 total.
            //Memory Reads: 2 average, 2 total.
            //Memory Writes: 1 total.
            
            
            c[0] = (-1 * coefs1[0] * coefs2[0]);
            
            return c;
        }
        
    }
}
