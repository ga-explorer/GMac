namespace GMacBlade.cga0001
{
    /// <summary>
    /// This class represents a mutable outermorphism in the cga0001 frame by only storing a 5 by 5
    /// matrix of the original vector linear transform and computing the other k-vectors matrices as needed
    /// </summary>
    public sealed partial class cga0001Outermorphism
    {
        public static double[] Apply_1(double[,] omCoefs, double[] bladeCoefs)
        {
            var coefs = new double[5];
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:47:26.7109677+02:00
            //Macro: geometry3d.cga.ApplyOM
            //Input Variables: 0 used, 30 not used, 30 total.
            //Temp Variables: 40 sub-expressions, 0 generated temps, 40 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 5 total.
            //Computations: 1 average, 45 total.
            //Memory Reads: 2 average, 90 total.
            //Memory Writes: 45 total.
            //
            //Macro Binding Data: 
            //   result.#no# <=> <Variable> coefs[0]
            //   result.#e1# <=> <Variable> coefs[1]
            //   result.#e2# <=> <Variable> coefs[2]
            //   result.#e3# <=> <Variable> coefs[3]
            //   result.#ni# <=> <Variable> coefs[4]
            //   mv.#no# <=> <Variable> bladeCoefs[0]
            //   mv.#e1# <=> <Variable> bladeCoefs[1]
            //   mv.#e2# <=> <Variable> bladeCoefs[2]
            //   mv.#e3# <=> <Variable> bladeCoefs[3]
            //   mv.#ni# <=> <Variable> bladeCoefs[4]
            //   om.ImageV1.#no# <=> <Variable> omCoefs[0, 0]
            //   om.ImageV2.#no# <=> <Variable> omCoefs[0, 1]
            //   om.ImageV3.#no# <=> <Variable> omCoefs[0, 2]
            //   om.ImageV4.#no# <=> <Variable> omCoefs[0, 3]
            //   om.ImageV5.#no# <=> <Variable> omCoefs[0, 4]
            //   om.ImageV1.#e1# <=> <Variable> omCoefs[1, 0]
            //   om.ImageV2.#e1# <=> <Variable> omCoefs[1, 1]
            //   om.ImageV3.#e1# <=> <Variable> omCoefs[1, 2]
            //   om.ImageV4.#e1# <=> <Variable> omCoefs[1, 3]
            //   om.ImageV5.#e1# <=> <Variable> omCoefs[1, 4]
            //   om.ImageV1.#e2# <=> <Variable> omCoefs[2, 0]
            //   om.ImageV2.#e2# <=> <Variable> omCoefs[2, 1]
            //   om.ImageV3.#e2# <=> <Variable> omCoefs[2, 2]
            //   om.ImageV4.#e2# <=> <Variable> omCoefs[2, 3]
            //   om.ImageV5.#e2# <=> <Variable> omCoefs[2, 4]
            //   om.ImageV1.#e3# <=> <Variable> omCoefs[3, 0]
            //   om.ImageV2.#e3# <=> <Variable> omCoefs[3, 1]
            //   om.ImageV3.#e3# <=> <Variable> omCoefs[3, 2]
            //   om.ImageV4.#e3# <=> <Variable> omCoefs[3, 3]
            //   om.ImageV5.#e3# <=> <Variable> omCoefs[3, 4]
            //   om.ImageV1.#ni# <=> <Variable> omCoefs[4, 0]
            //   om.ImageV2.#ni# <=> <Variable> omCoefs[4, 1]
            //   om.ImageV3.#ni# <=> <Variable> omCoefs[4, 2]
            //   om.ImageV4.#ni# <=> <Variable> omCoefs[4, 3]
            //   om.ImageV5.#ni# <=> <Variable> omCoefs[4, 4]
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = (bladeCoefs[0] * omCoefs[0, 0]);
            tempVar0001 = (bladeCoefs[1] * omCoefs[0, 1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (bladeCoefs[2] * omCoefs[0, 2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (bladeCoefs[3] * omCoefs[0, 3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (bladeCoefs[4] * omCoefs[0, 4]);
            coefs[0] = (tempVar0000 + tempVar0001);
            tempVar0000 = (bladeCoefs[0] * omCoefs[1, 0]);
            tempVar0001 = (bladeCoefs[1] * omCoefs[1, 1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (bladeCoefs[2] * omCoefs[1, 2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (bladeCoefs[3] * omCoefs[1, 3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (bladeCoefs[4] * omCoefs[1, 4]);
            coefs[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (bladeCoefs[0] * omCoefs[2, 0]);
            tempVar0001 = (bladeCoefs[1] * omCoefs[2, 1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (bladeCoefs[2] * omCoefs[2, 2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (bladeCoefs[3] * omCoefs[2, 3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (bladeCoefs[4] * omCoefs[2, 4]);
            coefs[2] = (tempVar0000 + tempVar0001);
            tempVar0000 = (bladeCoefs[0] * omCoefs[3, 0]);
            tempVar0001 = (bladeCoefs[1] * omCoefs[3, 1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (bladeCoefs[2] * omCoefs[3, 2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (bladeCoefs[3] * omCoefs[3, 3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (bladeCoefs[4] * omCoefs[3, 4]);
            coefs[3] = (tempVar0000 + tempVar0001);
            tempVar0000 = (bladeCoefs[0] * omCoefs[4, 0]);
            tempVar0001 = (bladeCoefs[1] * omCoefs[4, 1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (bladeCoefs[2] * omCoefs[4, 2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (bladeCoefs[3] * omCoefs[4, 3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (bladeCoefs[4] * omCoefs[4, 4]);
            coefs[4] = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:47:26.7179681+02:00
            
            return coefs;
        }
        
        
    }
}
