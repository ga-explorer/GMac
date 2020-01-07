using System;

namespace GMacModel.e3d
{
    public sealed partial class e3dScalar : e3dMultivector
    {
        
        public e3dPseudoScalar OP(e3dPseudoScalar mv)
        {
            var result = new e3dPseudoScalar();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:35:57.8693233+02:00
            //Macro: geometry3d.e3d.OP
            //Input Variables: 2 used, 0 not used, 2 total.
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
            //   mv2.#e1^e2^e3# <=> <Variable> mv.G3I0
            
            
            result.G3I0 = (-1 * G0I0 * mv.G3I0);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:35:57.8703234+02:00
            
            return result;
        }
        
        public e3dPseudoScalar GP(e3dPseudoScalar mv)
        {
            var result = new e3dPseudoScalar();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:35:57.8733235+02:00
            //Macro: geometry3d.e3d.GP
            //Input Variables: 2 used, 0 not used, 2 total.
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
            //   mv2.#e1^e2^e3# <=> <Variable> mv.G3I0
            
            
            result.G3I0 = (-1 * G0I0 * mv.G3I0);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:35:57.8743236+02:00
            
            return result;
        }
        
        public e3dPseudoScalar LCP(e3dPseudoScalar mv)
        {
            var result = new e3dPseudoScalar();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:35:57.8763237+02:00
            //Macro: geometry3d.e3d.LCP
            //Input Variables: 2 used, 0 not used, 2 total.
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
            //   mv2.#e1^e2^e3# <=> <Variable> mv.G3I0
            
            
            result.G3I0 = (-1 * G0I0 * mv.G3I0);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:35:57.8773238+02:00
            
            return result;
        }
        
        public e3dZero RCP(e3dPseudoScalar mv)
        {
            return Zero;
        }
        
        public double SP(e3dPseudoScalar mv)
        {
            return 0.0D;
        }
        
        public e3dMultivector9 Add(e3dPseudoScalar mv)
        {
            return new e3dMultivector9()
            {
                G0I0 = G0I0,
                G3I0 = mv.G3I0
            };
        }
        
        public e3dMultivector9 Subtract(e3dPseudoScalar mv)
        {
            return new e3dMultivector9()
            {
                G0I0 = G0I0,
                G3I0 = -mv.G3I0
            };
        }
        
        public bool IsEqual(e3dPseudoScalar mv)
        {
            return !(
                G0I0 <= -Epsilon || G0I0 >= Epsilon || 
                mv.G3I0 <= -Epsilon || mv.G3I0 >= Epsilon
            );
        }
        
    }
}