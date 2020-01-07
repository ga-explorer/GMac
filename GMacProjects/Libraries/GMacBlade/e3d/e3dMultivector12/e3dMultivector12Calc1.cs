namespace GMacModel.e3d
{
    public sealed partial class e3dMultivector12 : e3dMultivector
    {
        
        public e3dMultivector12 OP(e3dScalar mv)
        {
            var result = new e3dMultivector12();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:02.7126003+02:00
            //Macro: geometry3d.e3d.OP
            //Input Variables: 5 used, 0 not used, 5 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 8 total.
            //Computations: 1 average, 8 total.
            //Memory Reads: 1 average, 8 total.
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
            //   mv1.#e1^e2# <=> <Variable> G2I0
            //   mv1.#e1^e3# <=> <Variable> G2I1
            //   mv1.#e2^e3# <=> <Variable> G2I2
            //   mv1.#e1^e2^e3# <=> <Variable> G3I0
            //   mv2.#E0# <=> <Variable> mv.G0I0
            
            
            result.G2I0 = (-1 * G2I0 * mv.G0I0);
            result.G2I1 = (-1 * G2I1 * mv.G0I0);
            result.G2I2 = (-1 * G2I2 * mv.G0I0);
            result.G3I0 = (-1 * G3I0 * mv.G0I0);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:02.7136004+02:00
            
            return result;
        }
        
        public e3dMultivector12 GP(e3dScalar mv)
        {
            var result = new e3dMultivector12();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:02.7166006+02:00
            //Macro: geometry3d.e3d.GP
            //Input Variables: 5 used, 0 not used, 5 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 8 total.
            //Computations: 1 average, 8 total.
            //Memory Reads: 1 average, 8 total.
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
            //   mv1.#e1^e2# <=> <Variable> G2I0
            //   mv1.#e1^e3# <=> <Variable> G2I1
            //   mv1.#e2^e3# <=> <Variable> G2I2
            //   mv1.#e1^e2^e3# <=> <Variable> G3I0
            //   mv2.#E0# <=> <Variable> mv.G0I0
            
            
            result.G2I0 = (-1 * G2I0 * mv.G0I0);
            result.G2I1 = (-1 * G2I1 * mv.G0I0);
            result.G2I2 = (-1 * G2I2 * mv.G0I0);
            result.G3I0 = (-1 * G3I0 * mv.G0I0);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:02.7176006+02:00
            
            return result;
        }
        
        public e3dZero LCP(e3dScalar mv)
        {
            return Zero;
        }
        
        public e3dMultivector12 RCP(e3dScalar mv)
        {
            var result = new e3dMultivector12();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:02.7246010+02:00
            //Macro: geometry3d.e3d.RCP
            //Input Variables: 5 used, 0 not used, 5 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 8 total.
            //Computations: 1 average, 8 total.
            //Memory Reads: 1 average, 8 total.
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
            //   mv1.#e1^e2# <=> <Variable> G2I0
            //   mv1.#e1^e3# <=> <Variable> G2I1
            //   mv1.#e2^e3# <=> <Variable> G2I2
            //   mv1.#e1^e2^e3# <=> <Variable> G3I0
            //   mv2.#E0# <=> <Variable> mv.G0I0
            
            
            result.G2I0 = (-1 * G2I0 * mv.G0I0);
            result.G2I1 = (-1 * G2I1 * mv.G0I0);
            result.G2I2 = (-1 * G2I2 * mv.G0I0);
            result.G3I0 = (-1 * G3I0 * mv.G0I0);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:02.7256011+02:00
            
            return result;
        }
        
        public double SP(e3dScalar mv)
        {
            return 0.0D;
        }
        
        public e3dMultivector13 Add(e3dScalar mv)
        {
            return new e3dMultivector13()
            {
                G2I0 = G2I0,
                G2I1 = G2I1,
                G2I2 = G2I2,
                G3I0 = G3I0,
                G0I0 = mv.G0I0
            };
        }
        
        public e3dMultivector13 Subtract(e3dScalar mv)
        {
            return new e3dMultivector13()
            {
                G2I0 = G2I0,
                G2I1 = G2I1,
                G2I2 = G2I2,
                G3I0 = G3I0,
                G0I0 = -mv.G0I0
            };
        }
        
        public bool IsEqual(e3dScalar mv)
        {
            return !(
                G2I0 <= -Epsilon || G2I0 >= Epsilon || 
                G2I1 <= -Epsilon || G2I1 >= Epsilon || 
                G2I2 <= -Epsilon || G2I2 >= Epsilon || 
                G3I0 <= -Epsilon || G3I0 >= Epsilon || 
                mv.G0I0 <= -Epsilon || mv.G0I0 >= Epsilon
            );
        }
        
    }
}