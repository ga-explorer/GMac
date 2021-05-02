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
        private static double[] GP_323(double[] scalars1, double[] scalars2)
        {
            var c = new double[1];
        
            //Begin GMac Macro Code Generation, 2019-09-18T22:41:45.4930965+02:00
            //Macro: geometry3d.Ega3D.GP
            //Input Variables: 0 used, 4 not used, 4 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 1 total.
            //Computations: 0 average, 0 total.
            //Memory Reads: 0 average, 0 total.
            //Memory Writes: 1 total.
            //
            //Macro Binding Data: 
            //   result.#e1^e2^e3# = variable: c[0]
            //   mv1.#e1^e2^e3# = variable: scalars1[0]
            //   mv2.#e1^e2# = variable: scalars2[0]
            //   mv2.#e1^e3# = variable: scalars2[1]
            //   mv2.#e2^e3# = variable: scalars2[2]
            
            
            c[0] = 0;
            
            //Finish GMac Macro Code Generation, 2019-09-18T22:41:45.4940957+02:00
            
            return c;
        }
        
        
    }
}
