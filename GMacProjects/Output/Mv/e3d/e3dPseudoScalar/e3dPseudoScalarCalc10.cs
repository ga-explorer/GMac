using System;

namespace GMacModel.e3d
{
    public sealed partial class e3dPseudoScalar : e3dMultivector
    {
        
        public e3dZero OP(e3dMultivector10 mv)
        {
            return Zero;
        }
        
        public e3dMultivector5 GP(e3dMultivector10 mv)
        {
            var result = new e3dMultivector5();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:01.2065142+02:00
            //Macro: geometry3d.e3d.GP
            //Input Variables: 5 used, 0 not used, 5 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 8 total.
            //Computations: 0.75 average, 6 total.
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
            //   mv1.#e1^e2^e3# <=> <Variable> G3I0
            //   mv2.#e1# <=> <Variable> mv.G1I0
            //   mv2.#e2# <=> <Variable> mv.G1I1
            //   mv2.#e3# <=> <Variable> mv.G1I2
            //   mv2.#e1^e2^e3# <=> <Variable> mv.G3I0
            
            
            result.G0I0 = (G3I0 * mv.G3I0);
            result.G2I0 = (-1 * G3I0 * mv.G1I2);
            result.G2I1 = (G3I0 * mv.G1I1);
            result.G2I2 = (-1 * G3I0 * mv.G1I0);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:01.2075142+02:00
            
            return result;
        }
        
        public e3dScalar LCP(e3dMultivector10 mv)
        {
            var result = new e3dScalar();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:01.2095144+02:00
            //Macro: geometry3d.e3d.LCP
            //Input Variables: 2 used, 3 not used, 5 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 8 total.
            //Computations: 0.125 average, 1 total.
            //Memory Reads: 0.25 average, 2 total.
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
            //   mv1.#e1^e2^e3# <=> <Variable> G3I0
            //   mv2.#e1# <=> <Variable> mv.G1I0
            //   mv2.#e2# <=> <Variable> mv.G1I1
            //   mv2.#e3# <=> <Variable> mv.G1I2
            //   mv2.#e1^e2^e3# <=> <Variable> mv.G3I0
            
            
            result.G0I0 = (G3I0 * mv.G3I0);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:01.2105144+02:00
            
            return result;
        }
        
        public e3dMultivector5 RCP(e3dMultivector10 mv)
        {
            var result = new e3dMultivector5();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:01.2135146+02:00
            //Macro: geometry3d.e3d.RCP
            //Input Variables: 5 used, 0 not used, 5 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 8 total.
            //Computations: 0.75 average, 6 total.
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
            //   mv1.#e1^e2^e3# <=> <Variable> G3I0
            //   mv2.#e1# <=> <Variable> mv.G1I0
            //   mv2.#e2# <=> <Variable> mv.G1I1
            //   mv2.#e3# <=> <Variable> mv.G1I2
            //   mv2.#e1^e2^e3# <=> <Variable> mv.G3I0
            
            
            result.G0I0 = (G3I0 * mv.G3I0);
            result.G2I0 = (-1 * G3I0 * mv.G1I2);
            result.G2I1 = (G3I0 * mv.G1I1);
            result.G2I2 = (-1 * G3I0 * mv.G1I0);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:01.2155147+02:00
            
            return result;
        }
        
        public double SP(e3dMultivector10 mv)
        {
            var result = 0.0D;
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:01.2165148+02:00
            //Macro: geometry3d.e3d.SP
            //Input Variables: 2 used, 3 not used, 5 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 1 total.
            //Computations: 1 average, 1 total.
            //Memory Reads: 2 average, 2 total.
            //Memory Writes: 1 total.
            //
            //Macro Binding Data: 
            //   result <=> <Variable> result
            //   mv1.#e1^e2^e3# <=> <Variable> G3I0
            //   mv2.#e1# <=> <Variable> mv.G1I0
            //   mv2.#e2# <=> <Variable> mv.G1I1
            //   mv2.#e3# <=> <Variable> mv.G1I2
            //   mv2.#e1^e2^e3# <=> <Variable> mv.G3I0
            
            
            result = (G3I0 * mv.G3I0);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:01.2175148+02:00
            
            return result;
        }
        
        public e3dMultivector10 Add(e3dMultivector10 mv)
        {
            return new e3dMultivector10()
            {
                G1I0 = mv.G1I0,
                G1I1 = mv.G1I1,
                G1I2 = mv.G1I2,
                G3I0 = G3I0 + mv.G3I0
            };
        }
        
        public e3dMultivector10 Subtract(e3dMultivector10 mv)
        {
            return new e3dMultivector10()
            {
                G1I0 = -mv.G1I0,
                G1I1 = -mv.G1I1,
                G1I2 = -mv.G1I2,
                G3I0 = G3I0 - mv.G3I0
            };
        }
        
        public bool IsEqual(e3dMultivector10 mv)
        {
            return !(
                mv.G1I0 <= -Epsilon || mv.G1I0 >= Epsilon || 
                mv.G1I1 <= -Epsilon || mv.G1I1 >= Epsilon || 
                mv.G1I2 <= -Epsilon || mv.G1I2 >= Epsilon || 
                (G3I0 - mv.G3I0) <= -Epsilon || (G3I0 - mv.G3I0) >= Epsilon
            );
        }
        
    }
}