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
        private static double[] EGPDual_003(double[] scalars1, double[] scalars2)
        {
            var c = new double[1];
        
            //Begin GMac Macro Code Generation, 2019-09-18T22:41:45.6210189+02:00
            //Macro: geometry3d.Ega3D.EGPDual
            //Input Variables: 2 used, 0 not used, 2 total.
            //Temp Variables: 1 sub-expressions, 0 generated temps, 1 total.
            //Target Temp Variables: 1 total.
            //Output Variables: 1 total.
            //Computations: 1 average, 2 total.
            //Memory Reads: 1.5 average, 3 total.
            //Memory Writes: 2 total.
            //
            //Macro Binding Data: 
            //   result.#e1^e2^e3# = variable: c[0]
            //   mv1.#E0# = variable: scalars1[0]
            //   mv2.#E0# = variable: scalars2[0]
            
            double tempVar0000;
            
            tempVar0000 = scalars1[0] * scalars2[0];
            c[0] = -tempVar0000;
            
            //Finish GMac Macro Code Generation, 2019-09-18T22:41:45.6210189+02:00
            
            return c;
        }
        
        
    }
}
