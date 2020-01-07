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
        private static double[] ApplyVersor_155(double[] coefs1, double[] coefs2)
        {
            var c = new double[1];
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:46:14.9998661+02:00
            //Macro: geometry3d.cga.ApplyVersor
            //Input Variables: 0 used, 6 not used, 6 total.
            //Temp Variables: 45 sub-expressions, 0 generated temps, 45 total.
            //Target Temp Variables: 8 total.
            //Output Variables: 1 total.
            //Computations: 1.21739130434783 average, 56 total.
            //Memory Reads: 1.71739130434783 average, 79 total.
            //Memory Writes: 46 total.
            //
            //Macro Binding Data: 
            //   result.#no^e1^e2^e3^ni# <=> <Variable> c[0]
            //   v.#no# <=> <Variable> coefs1[0]
            //   v.#e1# <=> <Variable> coefs1[1]
            //   v.#e2# <=> <Variable> coefs1[2]
            //   v.#e3# <=> <Variable> coefs1[3]
            //   v.#ni# <=> <Variable> coefs1[4]
            //   mv.#no^e1^e2^e3^ni# <=> <Variable> coefs2[0]
            
            double tempVar0000;
            double tempVar0001;
            double tempVar0002;
            double tempVar0003;
            double tempVar0004;
            double tempVar0005;
            double tempVar0006;
            double tempVar0007;
            
            tempVar0000 = (-1 * coefs1[1] * coefs2[0]);
            tempVar0000 = (coefs1[1] * tempVar0000);
            tempVar0001 = (-1 * coefs1[2] * coefs2[0]);
            tempVar0001 = (-1 * coefs1[2] * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (coefs1[3] * coefs2[0]);
            tempVar0001 = (coefs1[3] * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(2, -0.5);
            tempVar0002 = (-1 * coefs1[0] * tempVar0001);
            tempVar0003 = (coefs1[4] * tempVar0001);
            tempVar0002 = (tempVar0002 + tempVar0003);
            tempVar0004 = (-1 * coefs2[0] * tempVar0002);
            tempVar0005 = (tempVar0001 * tempVar0004);
            tempVar0006 = (coefs1[0] * tempVar0001);
            tempVar0003 = (tempVar0003 + tempVar0006);
            tempVar0006 = (coefs2[0] * tempVar0003);
            tempVar0006 = (-1 * tempVar0001 * tempVar0006);
            tempVar0005 = (tempVar0005 + tempVar0006);
            tempVar0007 = (-1 * tempVar0001 * tempVar0005);
            tempVar0004 = (-1 * tempVar0001 * tempVar0004);
            tempVar0004 = (tempVar0006 + tempVar0004);
            tempVar0004 = (-1 * tempVar0001 * tempVar0004);
            tempVar0006 = (tempVar0007 + tempVar0004);
            tempVar0006 = (tempVar0003 * tempVar0006);
            tempVar0000 = (tempVar0000 + tempVar0006);
            tempVar0001 = (tempVar0001 * tempVar0005);
            tempVar0001 = (tempVar0004 + tempVar0001);
            tempVar0001 = (-1 * tempVar0002 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs1[1], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0004 = Math.Pow(coefs1[2], 2);
            tempVar0004 = (-1 * tempVar0004);
            tempVar0001 = (tempVar0001 + tempVar0004);
            tempVar0004 = Math.Pow(coefs1[3], 2);
            tempVar0004 = (-1 * tempVar0004);
            tempVar0001 = (tempVar0001 + tempVar0004);
            tempVar0002 = Math.Pow(tempVar0002, 2);
            tempVar0002 = (-1 * tempVar0002);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0002 = Math.Pow(tempVar0003, 2);
            tempVar0002 = (-1 * tempVar0002);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0001 = Math.Pow(tempVar0001, -1);
            c[0] = (tempVar0000 * tempVar0001);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:46:15.0028662+02:00
            
            return c;
        }
        
        
    }
}
