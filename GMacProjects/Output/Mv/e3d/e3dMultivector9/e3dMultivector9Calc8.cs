using System;

namespace GMacModel.e3d
{
    public sealed partial class e3dMultivector9 : e3dMultivector
    {
        
        public e3dPseudoScalar OP(e3dPseudoScalar mv)
        {
            var result = new e3dPseudoScalar();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:01.4945307+02:00
            //Macro: geometry3d.e3d.OP
            //Input Variables: 2 used, 1 not used, 3 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 8 total.
            //Computations: 0.25 average, 2 total.
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
            //   mv1.#E0# <=> <Variable> G0I0
            //   mv1.#e1^e2^e3# <=> <Variable> G3I0
            //   mv2.#e1^e2^e3# <=> <Variable> mv.G3I0
            
            
            result.G3I0 = (-1 * G0I0 * mv.G3I0);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:01.4955307+02:00
            
            return result;
        }
        
        public e3dMultivector9 GP(e3dPseudoScalar mv)
        {
            var result = new e3dMultivector9();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:01.4975308+02:00
            //Macro: geometry3d.e3d.GP
            //Input Variables: 3 used, 0 not used, 3 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 8 total.
            //Computations: 0.375 average, 3 total.
            //Memory Reads: 0.5 average, 4 total.
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
            //   mv1.#E0# <=> <Variable> G0I0
            //   mv1.#e1^e2^e3# <=> <Variable> G3I0
            //   mv2.#e1^e2^e3# <=> <Variable> mv.G3I0
            
            
            result.G0I0 = (G3I0 * mv.G3I0);
            result.G3I0 = (-1 * G0I0 * mv.G3I0);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:01.4985309+02:00
            
            return result;
        }
        
        public e3dMultivector9 LCP(e3dPseudoScalar mv)
        {
            var result = new e3dMultivector9();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:01.5015311+02:00
            //Macro: geometry3d.e3d.LCP
            //Input Variables: 3 used, 0 not used, 3 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 8 total.
            //Computations: 0.375 average, 3 total.
            //Memory Reads: 0.5 average, 4 total.
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
            //   mv1.#E0# <=> <Variable> G0I0
            //   mv1.#e1^e2^e3# <=> <Variable> G3I0
            //   mv2.#e1^e2^e3# <=> <Variable> mv.G3I0
            
            
            result.G0I0 = (G3I0 * mv.G3I0);
            result.G3I0 = (-1 * G0I0 * mv.G3I0);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:01.5025311+02:00
            
            return result;
        }
        
        public e3dScalar RCP(e3dPseudoScalar mv)
        {
            var result = new e3dScalar();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:01.5055313+02:00
            //Macro: geometry3d.e3d.RCP
            //Input Variables: 2 used, 1 not used, 3 total.
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
            //   mv1.#E0# <=> <Variable> G0I0
            //   mv1.#e1^e2^e3# <=> <Variable> G3I0
            //   mv2.#e1^e2^e3# <=> <Variable> mv.G3I0
            
            
            result.G0I0 = (G3I0 * mv.G3I0);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:01.5065313+02:00
            
            return result;
        }
        
        public double SP(e3dPseudoScalar mv)
        {
            var result = 0.0D;
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:01.5085315+02:00
            //Macro: geometry3d.e3d.SP
            //Input Variables: 2 used, 1 not used, 3 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 1 total.
            //Computations: 1 average, 1 total.
            //Memory Reads: 2 average, 2 total.
            //Memory Writes: 1 total.
            //
            //Macro Binding Data: 
            //   result <=> <Variable> result
            //   mv1.#E0# <=> <Variable> G0I0
            //   mv1.#e1^e2^e3# <=> <Variable> G3I0
            //   mv2.#e1^e2^e3# <=> <Variable> mv.G3I0
            
            
            result = (G3I0 * mv.G3I0);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:01.5085315+02:00
            
            return result;
        }
        
        public e3dMultivector9 Add(e3dPseudoScalar mv)
        {
            return new e3dMultivector9()
            {
                G0I0 = G0I0,
                G3I0 = G3I0 + mv.G3I0
            };
        }
        
        public e3dMultivector9 Subtract(e3dPseudoScalar mv)
        {
            return new e3dMultivector9()
            {
                G0I0 = G0I0,
                G3I0 = G3I0 - mv.G3I0
            };
        }
        
        public bool IsEqual(e3dPseudoScalar mv)
        {
            return !(
                G0I0 <= -Epsilon || G0I0 >= Epsilon || 
                (G3I0 - mv.G3I0) <= -Epsilon || (G3I0 - mv.G3I0) >= Epsilon
            );
        }
        
    }
}