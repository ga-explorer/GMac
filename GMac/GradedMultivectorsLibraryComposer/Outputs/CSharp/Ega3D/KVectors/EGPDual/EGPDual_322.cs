namespace Ega3D
{
    /// <summary>
    /// This class represents a k-vector in the Ega3D frame with arbitrary grade 
    /// (i.e. grade is determined at runtime) based on additive representation of 
    /// the k-vector as a linear combination of basis blades of the same grade.
    /// </summary>
    public sealed partial class Ega3DkVector
        : IEga3DMultivector
    {
        private static double[] EGPDual_322(double[] scalars1, double[] scalars2)
        {
            var c = new double[3];
        
            //Begin GMac Macro Code Generation, 2019-09-18T22:41:49.2543197+02:00
            //Macro: geometry3d.Ega3D.EGPDual
            //Input Variables: 4 used, 0 not used, 4 total.
            //Temp Variables: 2 sub-expressions, 0 generated temps, 2 total.
            //Target Temp Variables: 1 total.
            //Output Variables: 3 total.
            //Computations: 1.4 average, 7 total.
            //Memory Reads: 1.6 average, 8 total.
            //Memory Writes: 5 total.
            //
            //Macro Binding Data: 
            //   result.#e1^e2# = variable: c[0]
            //   result.#e1^e3# = variable: c[1]
            //   result.#e2^e3# = variable: c[2]
            //   mv1.#e1^e2^e3# = variable: scalars1[0]
            //   mv2.#e1^e2# = variable: scalars2[0]
            //   mv2.#e1^e3# = variable: scalars2[1]
            //   mv2.#e2^e3# = variable: scalars2[2]
            
            double tempVar0000;
            
            c[1] = scalars1[0] * scalars2[1];
            
            tempVar0000 = -1 * scalars1[0] * scalars2[0];
            c[0] = -tempVar0000;
            
            tempVar0000 = -1 * scalars1[0] * scalars2[2];
            c[2] = -tempVar0000;
            
            //Finish GMac Macro Code Generation, 2019-09-18T22:41:49.2543197+02:00
            
            return c;
        }
        
        
    }
}
