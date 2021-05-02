namespace GradedMultivectorsLibraryComposer.Outputs.CSharp.Ega3D
{
    /// <summary>
    /// This class represents a k-vector in the Ega3D frame with arbitrary grade 
    /// (i.e. grade is determined at runtime) based on additive representation of 
    /// the k-vector as a linear combination of basis blades of the same grade.
    /// </summary>
    public sealed partial class Ega3DkVector
        : IEga3DMultivector
    {
        private static double[] EGPDual_331(double[] scalars1, double[] scalars2)
        {
            var c = new double[3];
        
            //Begin GMac Macro Code Generation, 2019-09-18T22:41:49.3222773+02:00
            //Macro: geometry3d.Ega3D.EGPDual
            //Input Variables: 0 used, 2 not used, 2 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 3 total.
            //Computations: 0 average, 0 total.
            //Memory Reads: 0 average, 0 total.
            //Memory Writes: 3 total.
            //
            //Macro Binding Data: 
            //   result.#e1# = variable: c[0]
            //   result.#e2# = variable: c[1]
            //   result.#e3# = variable: c[2]
            //   mv1.#e1^e2^e3# = variable: scalars1[0]
            //   mv2.#e1^e2^e3# = variable: scalars2[0]
            
            
            c[0] = 0;
            
            c[1] = 0;
            
            c[2] = 0;
            
            //Finish GMac Macro Code Generation, 2019-09-18T22:41:49.3222773+02:00
            
            return c;
        }
        
        
    }
}
