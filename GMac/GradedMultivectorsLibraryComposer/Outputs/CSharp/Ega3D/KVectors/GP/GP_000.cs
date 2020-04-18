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
        private static double[] GP_000(double[] scalars1, double[] scalars2)
        {
            var c = new double[1];
        
            //Begin GMac Macro Code Generation, 2019-09-18T22:41:45.3901600+02:00
            //Macro: geometry3d.Ega3D.GP
            //Input Variables: 2 used, 0 not used, 2 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 1 total.
            //Computations: 1 average, 1 total.
            //Memory Reads: 2 average, 2 total.
            //Memory Writes: 1 total.
            //
            //Macro Binding Data: 
            //   result.#E0# = variable: c[0]
            //   mv1.#E0# = variable: scalars1[0]
            //   mv2.#E0# = variable: scalars2[0]
            
            
            c[0] = scalars1[0] * scalars2[0];
            
            //Finish GMac Macro Code Generation, 2019-09-18T22:41:45.3911598+02:00
            
            return c;
        }
        
        
    }
}
