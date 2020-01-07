namespace GMacModel.e3d
{
    public sealed partial class e3dVector : e3dMultivector
    {
        
        public e3dVector OP(e3dScalar mv)
        {
            var result = new e3dVector();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:35:58.0453334+02:00
            //Macro: geometry3d.e3d.OP
            //Input Variables: 4 used, 0 not used, 4 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 8 total.
            //Computations: 0.75 average, 6 total.
            //Memory Reads: 0.75 average, 6 total.
            //Memory Writes: 8 total.
            //
            //Macro Binding Data: 
            //   result.#E0# <=> <Variable> result.G0I0
            //   result.#e1# <=> <Variable> result.G1I0
            //   result.#e2# <=> <Variable> result.G1I1
            //   result.#e1^e2# <=> <Variable> result.G2I0
            //   result.#e3# <=> <Variable> result.G1I2
            //   result.#e1^e3# <=> <Variable> result.G2I1
            //   result.#e2^e3# <=> <Variable> result.G2I2
            //   result.#e1^e2^e3# <=> <Variable> result.G3I0
            //   mv1.#e1# <=> <Variable> G1I0
            //   mv1.#e2# <=> <Variable> G1I1
            //   mv1.#e3# <=> <Variable> G1I2
            //   mv2.#E0# <=> <Variable> mv.G0I0
            
            
            result.G1I0 = (-1 * G1I0 * mv.G0I0);
            result.G1I1 = (-1 * G1I1 * mv.G0I0);
            result.G1I2 = (-1 * G1I2 * mv.G0I0);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:35:58.0463334+02:00
            
            return result;
        }
        
        public e3dVector GP(e3dScalar mv)
        {
            var result = new e3dVector();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:35:58.0493336+02:00
            //Macro: geometry3d.e3d.GP
            //Input Variables: 4 used, 0 not used, 4 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 8 total.
            //Computations: 0.75 average, 6 total.
            //Memory Reads: 0.75 average, 6 total.
            //Memory Writes: 8 total.
            //
            //Macro Binding Data: 
            //   result.#E0# <=> <Variable> result.G0I0
            //   result.#e1# <=> <Variable> result.G1I0
            //   result.#e2# <=> <Variable> result.G1I1
            //   result.#e1^e2# <=> <Variable> result.G2I0
            //   result.#e3# <=> <Variable> result.G1I2
            //   result.#e1^e3# <=> <Variable> result.G2I1
            //   result.#e2^e3# <=> <Variable> result.G2I2
            //   result.#e1^e2^e3# <=> <Variable> result.G3I0
            //   mv1.#e1# <=> <Variable> G1I0
            //   mv1.#e2# <=> <Variable> G1I1
            //   mv1.#e3# <=> <Variable> G1I2
            //   mv2.#E0# <=> <Variable> mv.G0I0
            
            
            result.G1I0 = (-1 * G1I0 * mv.G0I0);
            result.G1I1 = (-1 * G1I1 * mv.G0I0);
            result.G1I2 = (-1 * G1I2 * mv.G0I0);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:35:58.0503337+02:00
            
            return result;
        }
        
        public e3dZero LCP(e3dScalar mv)
        {
            return Zero;
        }
        
        public e3dVector RCP(e3dScalar mv)
        {
            var result = new e3dVector();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:35:58.0563340+02:00
            //Macro: geometry3d.e3d.RCP
            //Input Variables: 4 used, 0 not used, 4 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 8 total.
            //Computations: 0.75 average, 6 total.
            //Memory Reads: 0.75 average, 6 total.
            //Memory Writes: 8 total.
            //
            //Macro Binding Data: 
            //   result.#E0# <=> <Variable> result.G0I0
            //   result.#e1# <=> <Variable> result.G1I0
            //   result.#e2# <=> <Variable> result.G1I1
            //   result.#e1^e2# <=> <Variable> result.G2I0
            //   result.#e3# <=> <Variable> result.G1I2
            //   result.#e1^e3# <=> <Variable> result.G2I1
            //   result.#e2^e3# <=> <Variable> result.G2I2
            //   result.#e1^e2^e3# <=> <Variable> result.G3I0
            //   mv1.#e1# <=> <Variable> G1I0
            //   mv1.#e2# <=> <Variable> G1I1
            //   mv1.#e3# <=> <Variable> G1I2
            //   mv2.#E0# <=> <Variable> mv.G0I0
            
            
            result.G1I0 = (-1 * G1I0 * mv.G0I0);
            result.G1I1 = (-1 * G1I1 * mv.G0I0);
            result.G1I2 = (-1 * G1I2 * mv.G0I0);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:35:58.0573341+02:00
            
            return result;
        }
        
        public double SP(e3dScalar mv)
        {
            return 0.0D;
        }
        
        public e3dMultivector3 Add(e3dScalar mv)
        {
            return new e3dMultivector3()
            {
                G1I0 = G1I0,
                G1I1 = G1I1,
                G1I2 = G1I2,
                G0I0 = mv.G0I0
            };
        }
        
        public e3dMultivector3 Subtract(e3dScalar mv)
        {
            return new e3dMultivector3()
            {
                G1I0 = G1I0,
                G1I1 = G1I1,
                G1I2 = G1I2,
                G0I0 = -mv.G0I0
            };
        }
        
        public bool IsEqual(e3dScalar mv)
        {
            return !(
                G1I0 <= -Epsilon || G1I0 >= Epsilon || 
                G1I1 <= -Epsilon || G1I1 >= Epsilon || 
                G1I2 <= -Epsilon || G1I2 >= Epsilon || 
                mv.G0I0 <= -Epsilon || mv.G0I0 >= Epsilon
            );
        }
        
    }
}