using System;

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
        private static Ega3DVector[] Factor7(double[] scalars)
        {
            var vectors = new[] 
            {
                new Ega3DVector(),
                new Ega3DVector(),
                new Ega3DVector()
            };
        
            //Begin GMac Macro Code Generation, 2019-09-18T22:41:52.1271868+02:00
            //Macro: geometry3d.Ega3D.Factor3
            //Input Variables: 1 used, 3 not used, 4 total.
            //Temp Variables: 2 sub-expressions, 0 generated temps, 2 total.
            //Target Temp Variables: 1 total.
            //Output Variables: 9 total.
            //Computations: 0.454545454545455 average, 5 total.
            //Memory Reads: 0.454545454545455 average, 5 total.
            //Memory Writes: 11 total.
            //
            //Macro Binding Data: 
            //   B.#e1^e2^e3# = variable: scalars[0]
            //   inputVectors.f1.#e1# = constant: '1'
            //   result.f1.#e1# = variable: vectors[0].C1
            //   result.f1.#e2# = variable: vectors[0].C2
            //   result.f1.#e3# = variable: vectors[0].C3
            //   inputVectors.f2.#e2# = constant: '1'
            //   result.f2.#e1# = variable: vectors[1].C1
            //   result.f2.#e2# = variable: vectors[1].C2
            //   result.f2.#e3# = variable: vectors[1].C3
            //   inputVectors.f3.#e3# = constant: '1'
            //   result.f3.#e1# = variable: vectors[2].C1
            //   result.f3.#e2# = variable: vectors[2].C2
            //   result.f3.#e3# = variable: vectors[2].C3
            
            double tempVar0000;
            
            vectors[0].C2 = 0;
            
            vectors[0].C3 = 0;
            
            vectors[1].C1 = 0;
            
            vectors[1].C3 = 0;
            
            vectors[2].C1 = 0;
            
            vectors[2].C2 = 0;
            
            vectors[2].C3 = Math.Pow(scalars[0], 9);
            
            tempVar0000 = scalars[0] * scalars[0];
            vectors[0].C1 = -tempVar0000;
            
            tempVar0000 = Math.Pow(scalars[0], 6);
            vectors[1].C2 = -tempVar0000;
            
            //Finish GMac Macro Code Generation, 2019-09-18T22:41:52.1281859+02:00
            
        
            return vectors;
        }
        
        
    }
}
