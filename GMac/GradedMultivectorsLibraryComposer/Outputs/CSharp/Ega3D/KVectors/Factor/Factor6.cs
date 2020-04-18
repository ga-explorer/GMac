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
        private static Ega3DVector[] Factor6(double[] scalars)
        {
            var vectors = new[] 
            {
                new Ega3DVector(),
                new Ega3DVector()
            };
        
            //Begin GMac Macro Code Generation, 2019-09-18T22:41:52.0002640+02:00
            //Macro: geometry3d.Ega3D.Factor2
            //Input Variables: 3 used, 2 not used, 5 total.
            //Temp Variables: 13 sub-expressions, 0 generated temps, 13 total.
            //Target Temp Variables: 6 total.
            //Output Variables: 6 total.
            //Computations: 1.36842105263158 average, 26 total.
            //Memory Reads: 1.78947368421053 average, 34 total.
            //Memory Writes: 19 total.
            //
            //Macro Binding Data: 
            //   B.#e1^e2# = variable: scalars[0]
            //   B.#e1^e3# = variable: scalars[1]
            //   B.#e2^e3# = variable: scalars[2]
            //   inputVectors.f1.#e2# = constant: '1'
            //   result.f1.#e1# = variable: vectors[0].C1
            //   result.f1.#e2# = variable: vectors[0].C2
            //   result.f1.#e3# = variable: vectors[0].C3
            //   inputVectors.f2.#e3# = constant: '1'
            //   result.f2.#e1# = variable: vectors[1].C1
            //   result.f2.#e2# = variable: vectors[1].C2
            //   result.f2.#e3# = variable: vectors[1].C3
            
            double tempVar0000;
            double tempVar0001;
            double tempVar0002;
            double tempVar0003;
            double tempVar0004;
            double tempVar0005;
            
            vectors[0].C1 = -1 * scalars[1] * scalars[2];
            
            vectors[0].C3 = -1 * scalars[0] * scalars[1];
            
            tempVar0000 = scalars[0] * scalars[0];
            tempVar0000 = -tempVar0000;
            tempVar0001 = scalars[2] * scalars[2];
            tempVar0001 = -tempVar0001;
            vectors[0].C2 = tempVar0000 + tempVar0001;
            
            tempVar0002 = -1 * scalars[0] * scalars[1];
            tempVar0003 = -1 * scalars[1] * scalars[2];
            tempVar0004 = scalars[0] * tempVar0003;
            tempVar0005 = -1 * scalars[2] * tempVar0002;
            vectors[1].C2 = tempVar0004 + tempVar0005;
            
            tempVar0000 = tempVar0000 + tempVar0001;
            tempVar0001 = -1 * scalars[0] * tempVar0000;
            tempVar0002 = -1 * scalars[1] * tempVar0002;
            vectors[1].C1 = tempVar0001 + tempVar0002;
            
            tempVar0001 = scalars[1] * tempVar0003;
            tempVar0000 = scalars[2] * tempVar0000;
            vectors[1].C3 = tempVar0001 + tempVar0000;
            
            //Finish GMac Macro Code Generation, 2019-09-18T22:41:52.0012622+02:00
            
        
            return vectors;
        }
        
        
    }
}
