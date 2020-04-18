using System.IO;

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
        private static Ega3DkVector OP2(Ega3DVector[] vectors)
        {
            var scalars = new double[3];
        
            //Begin GMac Macro Code Generation, 2019-09-18T22:41:45.0473713+02:00
            //Macro: geometry3d.Ega3D.VectorsOP
            //Input Variables: 6 used, 1 not used, 7 total.
            //Temp Variables: 6 sub-expressions, 0 generated temps, 6 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 3 total.
            //Computations: 1.33333333333333 average, 12 total.
            //Memory Reads: 2 average, 18 total.
            //Memory Writes: 9 total.
            //
            //Macro Binding Data: 
            //   result.#e1^e2# = variable: scalars[0]
            //   result.#e1^e3# = variable: scalars[1]
            //   result.#e2^e3# = variable: scalars[2]
            //   v0.#e1# = variable: vectors[0].C1
            //   v0.#e2# = variable: vectors[0].C2
            //   v0.#e3# = variable: vectors[0].C3
            //   v1.#e1# = variable: vectors[1].C1
            //   v1.#e2# = variable: vectors[1].C2
            //   v1.#e3# = variable: vectors[1].C3
            //   v2.#E0# = constant: '1'
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = -1 * vectors[0].C2 * vectors[1].C1;
            tempVar0001 = vectors[0].C1 * vectors[1].C2;
            scalars[0] = tempVar0000 + tempVar0001;
            
            tempVar0000 = -1 * vectors[0].C3 * vectors[1].C1;
            tempVar0001 = vectors[0].C1 * vectors[1].C3;
            scalars[1] = tempVar0000 + tempVar0001;
            
            tempVar0000 = -1 * vectors[0].C3 * vectors[1].C2;
            tempVar0001 = vectors[0].C2 * vectors[1].C3;
            scalars[2] = tempVar0000 + tempVar0001;
            
            //Finish GMac Macro Code Generation, 2019-09-18T22:41:45.0493699+02:00
            
            return new Ega3DkVector(2, scalars);
        }
        
        private static Ega3DkVector OP3(Ega3DVector[] vectors)
        {
            var scalars = new double[1];
        
            //Begin GMac Macro Code Generation, 2019-09-18T22:41:45.0643604+02:00
            //Macro: geometry3d.Ega3D.VectorsOP
            //Input Variables: 9 used, 0 not used, 9 total.
            //Temp Variables: 13 sub-expressions, 0 generated temps, 13 total.
            //Target Temp Variables: 3 total.
            //Output Variables: 1 total.
            //Computations: 1.28571428571429 average, 18 total.
            //Memory Reads: 2 average, 28 total.
            //Memory Writes: 14 total.
            //
            //Macro Binding Data: 
            //   result.#e1^e2^e3# = variable: scalars[0]
            //   v0.#e1# = variable: vectors[0].C1
            //   v0.#e2# = variable: vectors[0].C2
            //   v0.#e3# = variable: vectors[0].C3
            //   v1.#e1# = variable: vectors[1].C1
            //   v1.#e2# = variable: vectors[1].C2
            //   v1.#e3# = variable: vectors[1].C3
            //   v2.#e1# = variable: vectors[2].C1
            //   v2.#e2# = variable: vectors[2].C2
            //   v2.#e3# = variable: vectors[2].C3
            
            double tempVar0000;
            double tempVar0001;
            double tempVar0002;
            
            tempVar0000 = -1 * vectors[0].C2 * vectors[1].C1;
            tempVar0001 = vectors[0].C1 * vectors[1].C2;
            tempVar0000 = tempVar0000 + tempVar0001;
            tempVar0000 = vectors[2].C3 * tempVar0000;
            tempVar0001 = -1 * vectors[0].C3 * vectors[1].C1;
            tempVar0002 = vectors[0].C1 * vectors[1].C3;
            tempVar0001 = tempVar0001 + tempVar0002;
            tempVar0001 = -1 * vectors[2].C2 * tempVar0001;
            tempVar0000 = tempVar0000 + tempVar0001;
            tempVar0001 = -1 * vectors[0].C3 * vectors[1].C2;
            tempVar0002 = vectors[0].C2 * vectors[1].C3;
            tempVar0001 = tempVar0001 + tempVar0002;
            tempVar0001 = vectors[2].C1 * tempVar0001;
            scalars[0] = tempVar0000 + tempVar0001;
            
            //Finish GMac Macro Code Generation, 2019-09-18T22:41:45.0643604+02:00
            
            return new Ega3DkVector(3, scalars);
        }
        
        public static Ega3DkVector OP(Ega3DVector[] vectors)
        {
            switch (vectors.Length)
            {
                case 0:
                    return Ega3DMultivector.Zero;
                case 1:
                    return vectors[0].ToBlade();
                case 2:
                    return OP2(vectors);
                case 3:
                    return OP3(vectors);
            }
        
            throw new InvalidDataException("Internal error. Blade grade not acceptable!");
        }
        
        
    }
}
