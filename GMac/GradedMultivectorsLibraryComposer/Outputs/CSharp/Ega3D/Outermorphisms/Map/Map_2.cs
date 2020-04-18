namespace Ega3D
{
    /// <summary>
    /// This class represents a mutable outermorphism in the Ega3D frame by only storing a 3 by 3
    /// matrix of the original vector linear transform and computing the other k-vectors matrices as needed
    /// </summary>
    public sealed partial class Ega3DOutermorphism
    {
        public static double[] Map_2(double[,] omScalars, double[] kVectorScalars)
        {
            var mappedKVectorScalars = new double[3];
        
            //Begin GMac Macro Code Generation, 2019-09-18T22:42:14.5716240+02:00
            //Macro: geometry3d.Ega3D.ApplyOM
            //Input Variables: 12 used, 0 not used, 12 total.
            //Temp Variables: 39 sub-expressions, 0 generated temps, 39 total.
            //Target Temp Variables: 3 total.
            //Output Variables: 3 total.
            //Computations: 1.21428571428571 average, 51 total.
            //Memory Reads: 2 average, 84 total.
            //Memory Writes: 42 total.
            //
            //Macro Binding Data: 
            //   result.#e1^e2# = variable: mappedKVectorScalars[0]
            //   result.#e1^e3# = variable: mappedKVectorScalars[1]
            //   result.#e2^e3# = variable: mappedKVectorScalars[2]
            //   mv.#e1^e2# = variable: kVectorScalars[0]
            //   mv.#e1^e3# = variable: kVectorScalars[1]
            //   mv.#e2^e3# = variable: kVectorScalars[2]
            //   om.ImageV1.#e1# = variable: omScalars[0, 0]
            //   om.ImageV2.#e1# = variable: omScalars[0, 1]
            //   om.ImageV3.#e1# = variable: omScalars[0, 2]
            //   om.ImageV1.#e2# = variable: omScalars[1, 0]
            //   om.ImageV2.#e2# = variable: omScalars[1, 1]
            //   om.ImageV3.#e2# = variable: omScalars[1, 2]
            //   om.ImageV1.#e3# = variable: omScalars[2, 0]
            //   om.ImageV2.#e3# = variable: omScalars[2, 1]
            //   om.ImageV3.#e3# = variable: omScalars[2, 2]
            
            double tempVar0000;
            double tempVar0001;
            double tempVar0002;
            
            tempVar0000 = -1 * omScalars[0, 1] * omScalars[1, 0];
            tempVar0001 = omScalars[0, 0] * omScalars[1, 1];
            tempVar0000 = tempVar0000 + tempVar0001;
            tempVar0000 = kVectorScalars[0] * tempVar0000;
            tempVar0001 = -1 * omScalars[0, 2] * omScalars[1, 0];
            tempVar0002 = omScalars[0, 0] * omScalars[1, 2];
            tempVar0001 = tempVar0001 + tempVar0002;
            tempVar0001 = kVectorScalars[1] * tempVar0001;
            tempVar0000 = tempVar0000 + tempVar0001;
            tempVar0001 = -1 * omScalars[0, 2] * omScalars[1, 1];
            tempVar0002 = omScalars[0, 1] * omScalars[1, 2];
            tempVar0001 = tempVar0001 + tempVar0002;
            tempVar0001 = kVectorScalars[2] * tempVar0001;
            mappedKVectorScalars[0] = tempVar0000 + tempVar0001;
            
            tempVar0000 = -1 * omScalars[0, 1] * omScalars[2, 0];
            tempVar0001 = omScalars[0, 0] * omScalars[2, 1];
            tempVar0000 = tempVar0000 + tempVar0001;
            tempVar0000 = kVectorScalars[0] * tempVar0000;
            tempVar0001 = -1 * omScalars[0, 2] * omScalars[2, 0];
            tempVar0002 = omScalars[0, 0] * omScalars[2, 2];
            tempVar0001 = tempVar0001 + tempVar0002;
            tempVar0001 = kVectorScalars[1] * tempVar0001;
            tempVar0000 = tempVar0000 + tempVar0001;
            tempVar0001 = -1 * omScalars[0, 2] * omScalars[2, 1];
            tempVar0002 = omScalars[0, 1] * omScalars[2, 2];
            tempVar0001 = tempVar0001 + tempVar0002;
            tempVar0001 = kVectorScalars[2] * tempVar0001;
            mappedKVectorScalars[1] = tempVar0000 + tempVar0001;
            
            tempVar0000 = -1 * omScalars[1, 1] * omScalars[2, 0];
            tempVar0001 = omScalars[1, 0] * omScalars[2, 1];
            tempVar0000 = tempVar0000 + tempVar0001;
            tempVar0000 = kVectorScalars[0] * tempVar0000;
            tempVar0001 = -1 * omScalars[1, 2] * omScalars[2, 0];
            tempVar0002 = omScalars[1, 0] * omScalars[2, 2];
            tempVar0001 = tempVar0001 + tempVar0002;
            tempVar0001 = kVectorScalars[1] * tempVar0001;
            tempVar0000 = tempVar0000 + tempVar0001;
            tempVar0001 = -1 * omScalars[1, 2] * omScalars[2, 1];
            tempVar0002 = omScalars[1, 1] * omScalars[2, 2];
            tempVar0001 = tempVar0001 + tempVar0002;
            tempVar0001 = kVectorScalars[2] * tempVar0001;
            mappedKVectorScalars[2] = tempVar0000 + tempVar0001;
            
            //Finish GMac Macro Code Generation, 2019-09-18T22:42:14.5726230+02:00
            
            return mappedKVectorScalars;
        }
        
        
    }
}
