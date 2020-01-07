using System;

namespace GMacBlade.cga0001
{
    /// <summary>
    /// This class represents an immutable blade in the cga0001 frame with arbitrary grade 
    /// (i.e. grade is determined at runtime) based on additive representation of the blade as a 
    /// linear combination of basis blades of the same grade (i.e. it's actually a k-vector representation).
    /// </summary>
    public sealed partial class cga0001Blade
    {
        private static double[] ApplyRotor_355(double[] coefs1, double[] coefs2)
        {
            var c = new double[1];
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:46:39.7602823+02:00
            //Macro: geometry3d.cga.ApplyRotor
            //Input Variables: 0 used, 11 not used, 11 total.
            //Temp Variables: 83 sub-expressions, 0 generated temps, 83 total.
            //Target Temp Variables: 8 total.
            //Output Variables: 1 total.
            //Computations: 1.26190476190476 average, 106 total.
            //Memory Reads: 1.97619047619048 average, 166 total.
            //Memory Writes: 84 total.
            //
            //Macro Binding Data: 
            //   result.#no^e1^e2^e3^ni# <=> <Variable> c[0]
            //   v.#no^e1^e2# <=> <Variable> coefs1[0]
            //   v.#no^e1^e3# <=> <Variable> coefs1[1]
            //   v.#no^e2^e3# <=> <Variable> coefs1[2]
            //   v.#e1^e2^e3# <=> <Variable> coefs1[3]
            //   v.#no^e1^ni# <=> <Variable> coefs1[4]
            //   v.#no^e2^ni# <=> <Variable> coefs1[5]
            //   v.#e1^e2^ni# <=> <Variable> coefs1[6]
            //   v.#no^e3^ni# <=> <Variable> coefs1[7]
            //   v.#e1^e3^ni# <=> <Variable> coefs1[8]
            //   v.#e2^e3^ni# <=> <Variable> coefs1[9]
            //   mv.#no^e1^e2^e3^ni# <=> <Variable> coefs2[0]
            
            double tempVar0000;
            double tempVar0001;
            double tempVar0002;
            double tempVar0003;
            double tempVar0004;
            double tempVar0005;
            double tempVar0006;
            double tempVar0007;
            
            tempVar0000 = (-1 * coefs1[3] * coefs2[0]);
            tempVar0000 = (coefs1[3] * tempVar0000);
            tempVar0001 = (-1 * coefs1[4] * coefs2[0]);
            tempVar0001 = (coefs1[4] * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * coefs1[5] * coefs2[0]);
            tempVar0001 = (-1 * coefs1[5] * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (coefs1[7] * coefs2[0]);
            tempVar0001 = (coefs1[7] * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(2, -0.5);
            tempVar0002 = (coefs1[2] * tempVar0001);
            tempVar0003 = (-1 * coefs1[9] * tempVar0001);
            tempVar0004 = (tempVar0002 + tempVar0003);
            tempVar0004 = (coefs2[0] * tempVar0004);
            tempVar0005 = (-1 * tempVar0001 * tempVar0004);
            tempVar0006 = (-1 * coefs1[2] * tempVar0001);
            tempVar0003 = (tempVar0003 + tempVar0006);
            tempVar0003 = (-1 * coefs2[0] * tempVar0003);
            tempVar0003 = (tempVar0001 * tempVar0003);
            tempVar0005 = (tempVar0005 + tempVar0003);
            tempVar0007 = (tempVar0001 * tempVar0005);
            tempVar0004 = (tempVar0001 * tempVar0004);
            tempVar0003 = (tempVar0003 + tempVar0004);
            tempVar0003 = (tempVar0001 * tempVar0003);
            tempVar0004 = (tempVar0007 + tempVar0003);
            tempVar0007 = (coefs1[9] * tempVar0001);
            tempVar0002 = (tempVar0002 + tempVar0007);
            tempVar0002 = (tempVar0004 * tempVar0002);
            tempVar0000 = (tempVar0000 + tempVar0002);
            tempVar0002 = (-1 * tempVar0001 * tempVar0005);
            tempVar0002 = (tempVar0003 + tempVar0002);
            tempVar0003 = (tempVar0006 + tempVar0007);
            tempVar0002 = (-1 * tempVar0002 * tempVar0003);
            tempVar0000 = (tempVar0000 + tempVar0002);
            tempVar0002 = (coefs1[1] * tempVar0001);
            tempVar0003 = (-1 * coefs1[8] * tempVar0001);
            tempVar0004 = (tempVar0002 + tempVar0003);
            tempVar0004 = (coefs2[0] * tempVar0004);
            tempVar0005 = (-1 * tempVar0001 * tempVar0004);
            tempVar0006 = (-1 * coefs1[1] * tempVar0001);
            tempVar0003 = (tempVar0003 + tempVar0006);
            tempVar0003 = (-1 * coefs2[0] * tempVar0003);
            tempVar0003 = (tempVar0001 * tempVar0003);
            tempVar0005 = (tempVar0005 + tempVar0003);
            tempVar0007 = (tempVar0001 * tempVar0005);
            tempVar0004 = (tempVar0001 * tempVar0004);
            tempVar0003 = (tempVar0003 + tempVar0004);
            tempVar0003 = (tempVar0001 * tempVar0003);
            tempVar0004 = (tempVar0007 + tempVar0003);
            tempVar0007 = (coefs1[8] * tempVar0001);
            tempVar0002 = (tempVar0002 + tempVar0007);
            tempVar0002 = (-1 * tempVar0004 * tempVar0002);
            tempVar0000 = (tempVar0000 + tempVar0002);
            tempVar0002 = (-1 * tempVar0001 * tempVar0005);
            tempVar0002 = (tempVar0003 + tempVar0002);
            tempVar0003 = (tempVar0006 + tempVar0007);
            tempVar0002 = (tempVar0002 * tempVar0003);
            tempVar0000 = (tempVar0000 + tempVar0002);
            tempVar0002 = (coefs1[0] * tempVar0001);
            tempVar0003 = (-1 * coefs1[6] * tempVar0001);
            tempVar0004 = (tempVar0002 + tempVar0003);
            tempVar0004 = (-1 * coefs2[0] * tempVar0004);
            tempVar0005 = (-1 * tempVar0001 * tempVar0004);
            tempVar0006 = (-1 * coefs1[0] * tempVar0001);
            tempVar0003 = (tempVar0003 + tempVar0006);
            tempVar0003 = (coefs2[0] * tempVar0003);
            tempVar0003 = (tempVar0001 * tempVar0003);
            tempVar0005 = (tempVar0005 + tempVar0003);
            tempVar0007 = (tempVar0001 * tempVar0005);
            tempVar0004 = (tempVar0001 * tempVar0004);
            tempVar0003 = (tempVar0003 + tempVar0004);
            tempVar0003 = (tempVar0001 * tempVar0003);
            tempVar0004 = (tempVar0007 + tempVar0003);
            tempVar0007 = (coefs1[6] * tempVar0001);
            tempVar0002 = (tempVar0002 + tempVar0007);
            tempVar0002 = (tempVar0004 * tempVar0002);
            tempVar0000 = (tempVar0000 + tempVar0002);
            tempVar0001 = (-1 * tempVar0001 * tempVar0005);
            tempVar0001 = (tempVar0003 + tempVar0001);
            tempVar0002 = (tempVar0006 + tempVar0007);
            tempVar0001 = (-1 * tempVar0001 * tempVar0002);
            c[0] = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:46:39.7682827+02:00
            
            return c;
        }
        
        
    }
}
