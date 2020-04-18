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
        private static double[] GP_220(double[] scalars1, double[] scalars2)
        {
            var c = new double[1];
        
            //Begin GMac Macro Code Generation, 2019-09-18T22:41:45.4651144+02:00
            //Macro: geometry3d.Ega3D.GP
            //Input Variables: 6 used, 0 not used, 6 total.
            //Temp Variables: 4 sub-expressions, 0 generated temps, 4 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 1 total.
            //Computations: 1.6 average, 8 total.
            //Memory Reads: 2 average, 10 total.
            //Memory Writes: 5 total.
            //
            //Macro Binding Data: 
            //   result.#E0# = variable: c[0]
            //   mv1.#e1^e2# = variable: scalars1[0]
            //   mv1.#e1^e3# = variable: scalars1[1]
            //   mv1.#e2^e3# = variable: scalars1[2]
            //   mv2.#e1^e2# = variable: scalars2[0]
            //   mv2.#e1^e3# = variable: scalars2[1]
            //   mv2.#e2^e3# = variable: scalars2[2]
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = -1 * scalars1[0] * scalars2[0];
            tempVar0001 = -1 * scalars1[1] * scalars2[1];
            tempVar0000 = tempVar0000 + tempVar0001;
            tempVar0001 = -1 * scalars1[2] * scalars2[2];
            c[0] = tempVar0000 + tempVar0001;
            
            //Finish GMac Macro Code Generation, 2019-09-18T22:41:45.4661130+02:00
            
            return c;
        }
        
        
    }
}
