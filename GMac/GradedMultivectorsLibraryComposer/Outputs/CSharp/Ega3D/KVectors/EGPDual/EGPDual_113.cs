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
        private static double[] EGPDual_113(double[] scalars1, double[] scalars2)
        {
            var c = new double[1];
        
            //Begin GMac Macro Code Generation, 2019-09-18T22:41:46.3500817+02:00
            //Macro: geometry3d.Ega3D.EGPDual
            //Input Variables: 6 used, 0 not used, 6 total.
            //Temp Variables: 5 sub-expressions, 0 generated temps, 5 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 1 total.
            //Computations: 1 average, 6 total.
            //Memory Reads: 1.83333333333333 average, 11 total.
            //Memory Writes: 6 total.
            //
            //Macro Binding Data: 
            //   result.#e1^e2^e3# = variable: c[0]
            //   mv1.#e1# = variable: scalars1[0]
            //   mv1.#e2# = variable: scalars1[1]
            //   mv1.#e3# = variable: scalars1[2]
            //   mv2.#e1# = variable: scalars2[0]
            //   mv2.#e2# = variable: scalars2[1]
            //   mv2.#e3# = variable: scalars2[2]
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = scalars1[0] * scalars2[0];
            tempVar0001 = scalars1[1] * scalars2[1];
            tempVar0000 = tempVar0000 + tempVar0001;
            tempVar0001 = scalars1[2] * scalars2[2];
            tempVar0000 = tempVar0000 + tempVar0001;
            c[0] = -tempVar0000;
            
            //Finish GMac Macro Code Generation, 2019-09-18T22:41:46.3510809+02:00
            
            return c;
        }
        
        
    }
}
