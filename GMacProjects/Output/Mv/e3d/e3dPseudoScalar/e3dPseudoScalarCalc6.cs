using System;

namespace GMacModel.e3d
{
    public sealed partial class e3dPseudoScalar : e3dMultivector
    {
        
        public e3dZero OP(e3dMultivector6 mv)
        {
            return Zero;
        }
        
        public e3dMultivector6 GP(e3dMultivector6 mv)
        {
            var result = new e3dMultivector6();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:01.1355101+02:00
            //Macro: geometry3d.e3d.GP
            //Input Variables: 7 used, 0 not used, 7 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 8 total.
            //Computations: 1.125 average, 9 total.
            //Memory Reads: 1.5 average, 12 total.
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
            //   mv2.#e1^e2# <=> <Variable> mv.G2I0
            //   mv2.#e1^e3# <=> <Variable> mv.G2I1
            //   mv2.#e2^e3# <=> <Variable> mv.G2I2
            
            
            result.G1I0 = (G3I0 * mv.G2I2);
            result.G1I1 = (-1 * G3I0 * mv.G2I1);
            result.G2I0 = (-1 * G3I0 * mv.G1I2);
            result.G1I2 = (G3I0 * mv.G2I0);
            result.G2I1 = (G3I0 * mv.G1I1);
            result.G2I2 = (-1 * G3I0 * mv.G1I0);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:01.1365102+02:00
            
            return result;
        }
        
        public e3dZero LCP(e3dMultivector6 mv)
        {
            return Zero;
        }
        
        public e3dMultivector6 RCP(e3dMultivector6 mv)
        {
            var result = new e3dMultivector6();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:01.1435106+02:00
            //Macro: geometry3d.e3d.RCP
            //Input Variables: 7 used, 0 not used, 7 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 8 total.
            //Computations: 1.125 average, 9 total.
            //Memory Reads: 1.5 average, 12 total.
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
            //   mv2.#e1^e2# <=> <Variable> mv.G2I0
            //   mv2.#e1^e3# <=> <Variable> mv.G2I1
            //   mv2.#e2^e3# <=> <Variable> mv.G2I2
            
            
            result.G1I0 = (G3I0 * mv.G2I2);
            result.G1I1 = (-1 * G3I0 * mv.G2I1);
            result.G2I0 = (-1 * G3I0 * mv.G1I2);
            result.G1I2 = (G3I0 * mv.G2I0);
            result.G2I1 = (G3I0 * mv.G1I1);
            result.G2I2 = (-1 * G3I0 * mv.G1I0);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:01.1445106+02:00
            
            return result;
        }
        
        public double SP(e3dMultivector6 mv)
        {
            return 0.0D;
        }
        
        public e3dMultivector14 Add(e3dMultivector6 mv)
        {
            return new e3dMultivector14()
            {
                G3I0 = G3I0,
                G1I0 = mv.G1I0,
                G1I1 = mv.G1I1,
                G1I2 = mv.G1I2,
                G2I0 = mv.G2I0,
                G2I1 = mv.G2I1,
                G2I2 = mv.G2I2
            };
        }
        
        public e3dMultivector14 Subtract(e3dMultivector6 mv)
        {
            return new e3dMultivector14()
            {
                G3I0 = G3I0,
                G1I0 = -mv.G1I0,
                G1I1 = -mv.G1I1,
                G1I2 = -mv.G1I2,
                G2I0 = -mv.G2I0,
                G2I1 = -mv.G2I1,
                G2I2 = -mv.G2I2
            };
        }
        
        public bool IsEqual(e3dMultivector6 mv)
        {
            return !(
                G3I0 <= -Epsilon || G3I0 >= Epsilon || 
                mv.G1I0 <= -Epsilon || mv.G1I0 >= Epsilon || 
                mv.G1I1 <= -Epsilon || mv.G1I1 >= Epsilon || 
                mv.G1I2 <= -Epsilon || mv.G1I2 >= Epsilon || 
                mv.G2I0 <= -Epsilon || mv.G2I0 >= Epsilon || 
                mv.G2I1 <= -Epsilon || mv.G2I1 >= Epsilon || 
                mv.G2I2 <= -Epsilon || mv.G2I2 >= Epsilon
            );
        }
        
    }
}