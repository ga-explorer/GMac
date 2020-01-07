namespace GMacBlade.cga0001
{
    /// <summary>
    /// This class represents an immutable blade in the cga0001 frame with arbitrary grade 
    /// (i.e. grade is determined at runtime) based on additive representation of the blade as a 
    /// linear combination of basis blades of the same grade (i.e. it's actually a k-vector representation).
    /// </summary>
    public sealed partial class cga0001Blade
    {
        private static double[] ApplyEReflector_144(double[] coefs1, double[] coefs2)
        {
            var c = new double[5];
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:47:03.8886623+02:00
            //Macro: geometry3d.cga.ApplyEReflector
            //Input Variables: 0 used, 10 not used, 10 total.
            //Temp Variables: 84 sub-expressions, 0 generated temps, 84 total.
            //Target Temp Variables: 9 total.
            //Output Variables: 5 total.
            //Computations: 1.29213483146067 average, 115 total.
            //Memory Reads: 1.9438202247191 average, 173 total.
            //Memory Writes: 89 total.
            //
            //Macro Binding Data: 
            //   result.#no^e1^e2^e3# <=> <Variable> c[0]
            //   result.#no^e1^e2^ni# <=> <Variable> c[1]
            //   result.#no^e1^e3^ni# <=> <Variable> c[2]
            //   result.#no^e2^e3^ni# <=> <Variable> c[3]
            //   result.#e1^e2^e3^ni# <=> <Variable> c[4]
            //   v.#no# <=> <Variable> coefs1[0]
            //   v.#e1# <=> <Variable> coefs1[1]
            //   v.#e2# <=> <Variable> coefs1[2]
            //   v.#e3# <=> <Variable> coefs1[3]
            //   v.#ni# <=> <Variable> coefs1[4]
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
            
            tempVar0000 = (coefs1[3] * coefs2[0]);
            tempVar0001 = (coefs1[4] * coefs2[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * coefs1[3] * tempVar0000);
            tempVar0002 = (-1 * coefs1[2] * coefs2[0]);
            tempVar0003 = (coefs1[4] * coefs2[2]);
            tempVar0002 = (tempVar0002 + tempVar0003);
            tempVar0003 = (coefs1[2] * tempVar0002);
            tempVar0001 = (tempVar0001 + tempVar0003);
            tempVar0003 = (coefs1[1] * coefs2[0]);
            tempVar0004 = (coefs1[4] * coefs2[3]);
            tempVar0003 = (tempVar0003 + tempVar0004);
            tempVar0004 = (-1 * coefs1[1] * tempVar0003);
            tempVar0001 = (tempVar0001 + tempVar0004);
            tempVar0004 = (-1 * coefs1[0] * coefs2[0]);
            tempVar0005 = (coefs1[4] * coefs2[4]);
            tempVar0004 = (tempVar0004 + tempVar0005);
            tempVar0005 = (coefs1[0] * tempVar0004);
            tempVar0001 = (tempVar0001 + tempVar0005);
            tempVar0005 = (-1 * coefs1[4] * coefs2[0]);
            tempVar0006 = (coefs1[3] * coefs2[1]);
            tempVar0005 = (tempVar0005 + tempVar0006);
            tempVar0006 = (-1 * coefs1[2] * coefs2[2]);
            tempVar0005 = (tempVar0005 + tempVar0006);
            tempVar0006 = (coefs1[1] * coefs2[3]);
            tempVar0005 = (tempVar0005 + tempVar0006);
            tempVar0006 = (-1 * coefs1[0] * coefs2[4]);
            tempVar0005 = (tempVar0005 + tempVar0006);
            tempVar0006 = (-1 * coefs1[4] * tempVar0005);
            tempVar0001 = (tempVar0001 + tempVar0006);
            c[0] = (-1 * tempVar0001);
            tempVar0000 = (-1 * coefs1[4] * tempVar0000);
            tempVar0001 = (-1 * coefs1[2] * coefs2[1]);
            tempVar0006 = (-1 * coefs1[3] * coefs2[2]);
            tempVar0001 = (tempVar0001 + tempVar0006);
            tempVar0006 = (coefs1[2] * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0006);
            tempVar0006 = (coefs1[1] * coefs2[1]);
            tempVar0007 = (-1 * coefs1[3] * coefs2[3]);
            tempVar0006 = (tempVar0006 + tempVar0007);
            tempVar0007 = (-1 * coefs1[1] * tempVar0006);
            tempVar0000 = (tempVar0000 + tempVar0007);
            tempVar0007 = (-1 * coefs1[0] * coefs2[1]);
            tempVar0008 = (-1 * coefs1[3] * coefs2[4]);
            tempVar0007 = (tempVar0007 + tempVar0008);
            tempVar0008 = (coefs1[0] * tempVar0007);
            tempVar0000 = (tempVar0000 + tempVar0008);
            tempVar0008 = (coefs1[3] * tempVar0005);
            tempVar0000 = (tempVar0000 + tempVar0008);
            c[1] = (-1 * tempVar0000);
            tempVar0000 = (-1 * coefs1[4] * tempVar0002);
            tempVar0001 = (coefs1[3] * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (coefs1[1] * coefs2[2]);
            tempVar0002 = (coefs1[2] * coefs2[3]);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0002 = (-1 * coefs1[1] * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0002);
            tempVar0002 = (-1 * coefs1[0] * coefs2[2]);
            tempVar0008 = (coefs1[2] * coefs2[4]);
            tempVar0002 = (tempVar0002 + tempVar0008);
            tempVar0008 = (coefs1[0] * tempVar0002);
            tempVar0000 = (tempVar0000 + tempVar0008);
            tempVar0008 = (-1 * coefs1[2] * tempVar0005);
            tempVar0000 = (tempVar0000 + tempVar0008);
            c[2] = (-1 * tempVar0000);
            tempVar0000 = (-1 * coefs1[4] * tempVar0003);
            tempVar0003 = (coefs1[3] * tempVar0006);
            tempVar0000 = (tempVar0000 + tempVar0003);
            tempVar0001 = (-1 * coefs1[2] * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * coefs1[0] * coefs2[3]);
            tempVar0003 = (-1 * coefs1[1] * coefs2[4]);
            tempVar0001 = (tempVar0001 + tempVar0003);
            tempVar0003 = (coefs1[0] * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0003);
            tempVar0003 = (coefs1[1] * tempVar0005);
            tempVar0000 = (tempVar0000 + tempVar0003);
            c[3] = (-1 * tempVar0000);
            tempVar0000 = (-1 * coefs1[4] * tempVar0004);
            tempVar0003 = (coefs1[3] * tempVar0007);
            tempVar0000 = (tempVar0000 + tempVar0003);
            tempVar0002 = (-1 * coefs1[2] * tempVar0002);
            tempVar0000 = (tempVar0000 + tempVar0002);
            tempVar0001 = (coefs1[1] * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * coefs1[0] * tempVar0005);
            tempVar0000 = (tempVar0000 + tempVar0001);
            c[4] = (-1 * tempVar0000);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:47:03.8986629+02:00
            
            return c;
        }
        
        
    }
}
