namespace GMacBlade.cga0001
{
    /// <summary>
    /// This class represents an immutable blade in the cga0001 frame with arbitrary grade 
    /// (i.e. grade is determined at runtime) based on additive representation of the blade as a 
    /// linear combination of basis blades of the same grade (i.e. it's actually a k-vector representation).
    /// </summary>
    public sealed partial class cga0001Blade
    {
        private static double[] ApplyERotor_444(double[] coefs1, double[] coefs2)
        {
            var c = new double[5];
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:47:03.2626265+02:00
            //Macro: geometry3d.cga.ApplyERotor
            //Input Variables: 0 used, 10 not used, 10 total.
            //Temp Variables: 79 sub-expressions, 0 generated temps, 79 total.
            //Target Temp Variables: 11 total.
            //Output Variables: 5 total.
            //Computations: 1.35714285714286 average, 114 total.
            //Memory Reads: 2 average, 168 total.
            //Memory Writes: 84 total.
            //
            //Macro Binding Data: 
            //   result.#no^e1^e2^e3# <=> <Variable> c[0]
            //   result.#no^e1^e2^ni# <=> <Variable> c[1]
            //   result.#no^e1^e3^ni# <=> <Variable> c[2]
            //   result.#no^e2^e3^ni# <=> <Variable> c[3]
            //   result.#e1^e2^e3^ni# <=> <Variable> c[4]
            //   v.#no^e1^e2^e3# <=> <Variable> coefs1[0]
            //   v.#no^e1^e2^ni# <=> <Variable> coefs1[1]
            //   v.#no^e1^e3^ni# <=> <Variable> coefs1[2]
            //   v.#no^e2^e3^ni# <=> <Variable> coefs1[3]
            //   v.#e1^e2^e3^ni# <=> <Variable> coefs1[4]
            //   mv.#no^e1^e2^e3# <=> <Variable> coefs2[0]
            //   mv.#no^e1^e2^ni# <=> <Variable> coefs2[1]
            //   mv.#no^e1^e3^ni# <=> <Variable> coefs2[2]
            //   mv.#no^e2^e3^ni# <=> <Variable> coefs2[3]
            //   mv.#e1^e2^e3^ni# <=> <Variable> coefs2[4]
            
            double tempVar0000;
            double tempVar0001;
            double tempVar0002;
            double tempVar0003;
            double tempVar0004;
            double tempVar0005;
            double tempVar0006;
            double tempVar0007;
            double tempVar0008;
            double tempVar0009;
            double tempVar000A;
            
            tempVar0000 = (-1 * coefs1[0] * coefs2[0]);
            tempVar0001 = (-1 * coefs1[1] * coefs2[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * coefs1[2] * coefs2[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * coefs1[3] * coefs2[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * coefs1[4] * coefs2[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * coefs1[0] * tempVar0000);
            tempVar0002 = (-1 * coefs1[4] * coefs2[0]);
            tempVar0003 = (coefs1[0] * coefs2[4]);
            tempVar0002 = (tempVar0002 + tempVar0003);
            tempVar0003 = (coefs1[4] * tempVar0002);
            tempVar0001 = (tempVar0001 + tempVar0003);
            tempVar0003 = (coefs1[3] * coefs2[0]);
            tempVar0004 = (-1 * coefs1[0] * coefs2[3]);
            tempVar0003 = (tempVar0003 + tempVar0004);
            tempVar0004 = (-1 * coefs1[3] * tempVar0003);
            tempVar0001 = (tempVar0001 + tempVar0004);
            tempVar0004 = (-1 * coefs1[2] * coefs2[0]);
            tempVar0005 = (coefs1[0] * coefs2[2]);
            tempVar0004 = (tempVar0004 + tempVar0005);
            tempVar0005 = (coefs1[2] * tempVar0004);
            tempVar0001 = (tempVar0001 + tempVar0005);
            tempVar0005 = (coefs1[1] * coefs2[0]);
            tempVar0006 = (-1 * coefs1[0] * coefs2[1]);
            tempVar0005 = (tempVar0005 + tempVar0006);
            tempVar0006 = (-1 * coefs1[1] * tempVar0005);
            c[0] = (tempVar0001 + tempVar0006);
            tempVar0001 = (-1 * coefs1[1] * tempVar0000);
            tempVar0006 = (coefs1[4] * coefs2[1]);
            tempVar0007 = (-1 * coefs1[1] * coefs2[4]);
            tempVar0006 = (tempVar0006 + tempVar0007);
            tempVar0007 = (-1 * coefs1[4] * tempVar0006);
            tempVar0001 = (tempVar0001 + tempVar0007);
            tempVar0007 = (-1 * coefs1[3] * coefs2[1]);
            tempVar0008 = (coefs1[1] * coefs2[3]);
            tempVar0007 = (tempVar0007 + tempVar0008);
            tempVar0008 = (coefs1[3] * tempVar0007);
            tempVar0001 = (tempVar0001 + tempVar0008);
            tempVar0008 = (coefs1[2] * coefs2[1]);
            tempVar0009 = (-1 * coefs1[1] * coefs2[2]);
            tempVar0008 = (tempVar0008 + tempVar0009);
            tempVar0009 = (-1 * coefs1[2] * tempVar0008);
            tempVar0001 = (tempVar0001 + tempVar0009);
            tempVar0005 = (coefs1[0] * tempVar0005);
            c[1] = (tempVar0001 + tempVar0005);
            tempVar0001 = (-1 * coefs1[2] * tempVar0000);
            tempVar0005 = (-1 * coefs1[4] * coefs2[2]);
            tempVar0009 = (coefs1[2] * coefs2[4]);
            tempVar0005 = (tempVar0005 + tempVar0009);
            tempVar0009 = (coefs1[4] * tempVar0005);
            tempVar0001 = (tempVar0001 + tempVar0009);
            tempVar0009 = (coefs1[3] * coefs2[2]);
            tempVar000A = (-1 * coefs1[2] * coefs2[3]);
            tempVar0009 = (tempVar0009 + tempVar000A);
            tempVar000A = (-1 * coefs1[3] * tempVar0009);
            tempVar0001 = (tempVar0001 + tempVar000A);
            tempVar0008 = (coefs1[1] * tempVar0008);
            tempVar0001 = (tempVar0001 + tempVar0008);
            tempVar0004 = (-1 * coefs1[0] * tempVar0004);
            c[2] = (tempVar0001 + tempVar0004);
            tempVar0001 = (-1 * coefs1[3] * tempVar0000);
            tempVar0004 = (coefs1[4] * coefs2[3]);
            tempVar0008 = (-1 * coefs1[3] * coefs2[4]);
            tempVar0004 = (tempVar0004 + tempVar0008);
            tempVar0008 = (-1 * coefs1[4] * tempVar0004);
            tempVar0001 = (tempVar0001 + tempVar0008);
            tempVar0008 = (coefs1[2] * tempVar0009);
            tempVar0001 = (tempVar0001 + tempVar0008);
            tempVar0007 = (-1 * coefs1[1] * tempVar0007);
            tempVar0001 = (tempVar0001 + tempVar0007);
            tempVar0003 = (coefs1[0] * tempVar0003);
            c[3] = (tempVar0001 + tempVar0003);
            tempVar0000 = (-1 * coefs1[4] * tempVar0000);
            tempVar0001 = (coefs1[3] * tempVar0004);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * coefs1[2] * tempVar0005);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (coefs1[1] * tempVar0006);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * coefs1[0] * tempVar0002);
            c[4] = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:47:03.2736272+02:00
            
            return c;
        }
        
        
    }
}
