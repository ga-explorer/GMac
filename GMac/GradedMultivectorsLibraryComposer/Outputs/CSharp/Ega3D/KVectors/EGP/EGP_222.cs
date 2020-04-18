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
        private static double[] EGP_222(double[] scalars1, double[] scalars2)
        {
            var c = new double[3];
        
            //Begin GMac Macro Code Generation, 2019-09-18T22:41:45.5680509+02:00
            //Macro: geometry3d.Ega3D.EGP
            //Input Variables: 6 used, 0 not used, 6 total.
            //Temp Variables: 6 sub-expressions, 0 generated temps, 6 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 3 total.
            //Computations: 1.33333333333333 average, 12 total.
            //Memory Reads: 2 average, 18 total.
            //Memory Writes: 9 total.
            //
            //Macro Binding Data: 
            //   result.#e1^e2# = variable: c[0]
            //   result.#e1^e3# = variable: c[1]
            //   result.#e2^e3# = variable: c[2]
            //   mv1.#e1^e2# = variable: scalars1[0]
            //   mv1.#e1^e3# = variable: scalars1[1]
            //   mv1.#e2^e3# = variable: scalars1[2]
            //   mv2.#e1^e2# = variable: scalars2[0]
            //   mv2.#e1^e3# = variable: scalars2[1]
            //   mv2.#e2^e3# = variable: scalars2[2]
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = scalars1[2] * scalars2[1];
            tempVar0001 = -1 * scalars1[1] * scalars2[2];
            c[0] = tempVar0000 + tempVar0001;
            
            tempVar0000 = -1 * scalars1[2] * scalars2[0];
            tempVar0001 = scalars1[0] * scalars2[2];
            c[1] = tempVar0000 + tempVar0001;
            
            tempVar0000 = scalars1[1] * scalars2[0];
            tempVar0001 = -1 * scalars1[0] * scalars2[1];
            c[2] = tempVar0000 + tempVar0001;
            
            //Finish GMac Macro Code Generation, 2019-09-18T22:41:45.5690503+02:00
            
            return c;
        }
        
        
    }
}
