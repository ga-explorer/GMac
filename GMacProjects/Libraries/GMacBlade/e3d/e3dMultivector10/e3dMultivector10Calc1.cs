namespace GMacModel.e3d
{
    public sealed partial class e3dMultivector10 : e3dMultivector
    {
        
        public e3dMultivector10 OP(e3dScalar mv)
        {
            var result = new e3dMultivector10();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:01.7125431+02:00
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
            //   mv1.#e1# <=> <Variable> G1I0
            //   mv1.#e2# <=> <Variable> G1I1
            //   mv1.#e3# <=> <Variable> G1I2
            //   mv1.#e1^e2^e3# <=> <Variable> G3I0
            //   mv2.#E0# <=> <Variable> mv.G0I0
            
            
            result.G1I0 = (-1 * G1I0 * mv.G0I0);
            result.G1I1 = (-1 * G1I1 * mv.G0I0);
            result.G1I2 = (-1 * G1I2 * mv.G0I0);
            result.G3I0 = (-1 * G3I0 * mv.G0I0);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:01.7135432+02:00
            
            return result;
        }
        
        public e3dMultivector10 GP(e3dScalar mv)
        {
            var result = new e3dMultivector10();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:01.7165434+02:00
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
            //   mv1.#e1# <=> <Variable> G1I0
            //   mv1.#e2# <=> <Variable> G1I1
            //   mv1.#e3# <=> <Variable> G1I2
            //   mv1.#e1^e2^e3# <=> <Variable> G3I0
            //   mv2.#E0# <=> <Variable> mv.G0I0
            
            
            result.G1I0 = (-1 * G1I0 * mv.G0I0);
            result.G1I1 = (-1 * G1I1 * mv.G0I0);
            result.G1I2 = (-1 * G1I2 * mv.G0I0);
            result.G3I0 = (-1 * G3I0 * mv.G0I0);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:01.7175434+02:00
            
            return result;
        }
        
        public e3dZero LCP(e3dScalar mv)
        {
            return Zero;
        }
        
        public e3dMultivector10 RCP(e3dScalar mv)
        {
            var result = new e3dMultivector10();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:01.7235438+02:00
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
            //   mv1.#e1# <=> <Variable> G1I0
            //   mv1.#e2# <=> <Variable> G1I1
            //   mv1.#e3# <=> <Variable> G1I2
            //   mv1.#e1^e2^e3# <=> <Variable> G3I0
            //   mv2.#E0# <=> <Variable> mv.G0I0
            
            
            result.G1I0 = (-1 * G1I0 * mv.G0I0);
            result.G1I1 = (-1 * G1I1 * mv.G0I0);
            result.G1I2 = (-1 * G1I2 * mv.G0I0);
            result.G3I0 = (-1 * G3I0 * mv.G0I0);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:01.7245438+02:00
            
            return result;
        }
        
        public double SP(e3dScalar mv)
        {
            return 0.0D;
        }
        
        public e3dMultivector11 Add(e3dScalar mv)
        {
            return new e3dMultivector11()
            {
                G1I0 = G1I0,
                G1I1 = G1I1,
                G1I2 = G1I2,
                G3I0 = G3I0,
                G0I0 = mv.G0I0
            };
        }
        
        public e3dMultivector11 Subtract(e3dScalar mv)
        {
            return new e3dMultivector11()
            {
                G1I0 = G1I0,
                G1I1 = G1I1,
                G1I2 = G1I2,
                G3I0 = G3I0,
                G0I0 = -mv.G0I0
            };
        }
        
        public bool IsEqual(e3dScalar mv)
        {
            return !(
                G1I0 <= -Epsilon || G1I0 >= Epsilon || 
                G1I1 <= -Epsilon || G1I1 >= Epsilon || 
                G1I2 <= -Epsilon || G1I2 >= Epsilon || 
                G3I0 <= -Epsilon || G3I0 >= Epsilon || 
                mv.G0I0 <= -Epsilon || mv.G0I0 >= Epsilon
            );
        }
        
    }
}