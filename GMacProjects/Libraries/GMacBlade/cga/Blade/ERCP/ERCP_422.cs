namespace GMacBlade.cga0001
{
    /// <summary>
    /// This class represents an immutable blade in the cga0001 frame with arbitrary grade 
    /// (i.e. grade is determined at runtime) based on additive representation of the blade as a 
    /// linear combination of basis blades of the same grade (i.e. it's actually a k-vector representation).
    /// </summary>
    public sealed partial class cga0001Blade
    {
        private static double[] ERCP_422(double[] coefs1, double[] coefs2)
        {
            var c = new double[10];
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:45:44.1561019+02:00
            //Macro: geometry3d.cga.ERCP
            //Input Variables: 0 used, 15 not used, 15 total.
            //Temp Variables: 40 sub-expressions, 0 generated temps, 40 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 10 total.
            //Computations: 1.2 average, 60 total.
            //Memory Reads: 2 average, 100 total.
            //Memory Writes: 50 total.
            //
            //Macro Binding Data: 
            //   result.#no^e1# <=> <Variable> c[0]
            //   result.#no^e2# <=> <Variable> c[1]
            //   result.#e1^e2# <=> <Variable> c[2]
            //   result.#no^e3# <=> <Variable> c[3]
            //   result.#e1^e3# <=> <Variable> c[4]
            //   result.#e2^e3# <=> <Variable> c[5]
            //   result.#no^ni# <=> <Variable> c[6]
            //   result.#e1^ni# <=> <Variable> c[7]
            //   result.#e2^ni# <=> <Variable> c[8]
            //   result.#e3^ni# <=> <Variable> c[9]
            //   mv1.#no^e1^e2^e3# <=> <Variable> coefs1[0]
            //   mv1.#no^e1^e2^ni# <=> <Variable> coefs1[1]
            //   mv1.#no^e1^e3^ni# <=> <Variable> coefs1[2]
            //   mv1.#no^e2^e3^ni# <=> <Variable> coefs1[3]
            //   mv1.#e1^e2^e3^ni# <=> <Variable> coefs1[4]
            //   mv2.#no^e1# <=> <Variable> coefs2[0]
            //   mv2.#no^e2# <=> <Variable> coefs2[1]
            //   mv2.#e1^e2# <=> <Variable> coefs2[2]
            //   mv2.#no^e3# <=> <Variable> coefs2[3]
            //   mv2.#e1^e3# <=> <Variable> coefs2[4]
            //   mv2.#e2^e3# <=> <Variable> coefs2[5]
            //   mv2.#no^ni# <=> <Variable> coefs2[6]
            //   mv2.#e1^ni# <=> <Variable> coefs2[7]
            //   mv2.#e2^ni# <=> <Variable> coefs2[8]
            //   mv2.#e3^ni# <=> <Variable> coefs2[9]
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = (coefs1[0] * coefs2[5]);
            tempVar0001 = (coefs1[1] * coefs2[8]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (coefs1[2] * coefs2[9]);
            c[0] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * coefs1[0] * coefs2[4]);
            tempVar0001 = (-1 * coefs1[1] * coefs2[7]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (coefs1[3] * coefs2[9]);
            c[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (coefs1[0] * coefs2[3]);
            tempVar0001 = (coefs1[1] * coefs2[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (coefs1[4] * coefs2[9]);
            c[2] = (tempVar0000 + tempVar0001);
            tempVar0000 = (coefs1[0] * coefs2[2]);
            tempVar0001 = (-1 * coefs1[2] * coefs2[7]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * coefs1[3] * coefs2[8]);
            c[3] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * coefs1[0] * coefs2[1]);
            tempVar0001 = (coefs1[2] * coefs2[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * coefs1[4] * coefs2[8]);
            c[4] = (tempVar0000 + tempVar0001);
            tempVar0000 = (coefs1[0] * coefs2[0]);
            tempVar0001 = (coefs1[3] * coefs2[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (coefs1[4] * coefs2[7]);
            c[5] = (tempVar0000 + tempVar0001);
            tempVar0000 = (coefs1[1] * coefs2[2]);
            tempVar0001 = (coefs1[2] * coefs2[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (coefs1[3] * coefs2[5]);
            c[6] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * coefs1[1] * coefs2[1]);
            tempVar0001 = (-1 * coefs1[2] * coefs2[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (coefs1[4] * coefs2[5]);
            c[7] = (tempVar0000 + tempVar0001);
            tempVar0000 = (coefs1[1] * coefs2[0]);
            tempVar0001 = (-1 * coefs1[3] * coefs2[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * coefs1[4] * coefs2[4]);
            c[8] = (tempVar0000 + tempVar0001);
            tempVar0000 = (coefs1[2] * coefs2[0]);
            tempVar0001 = (coefs1[3] * coefs2[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (coefs1[4] * coefs2[2]);
            c[9] = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:45:44.1611022+02:00
            
            return c;
        }
        
        
    }
}
