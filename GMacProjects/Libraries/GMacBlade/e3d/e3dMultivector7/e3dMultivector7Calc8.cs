namespace GMacModel.e3d
{
    public sealed partial class e3dMultivector7 : e3dMultivector
    {
        
        public e3dPseudoScalar OP(e3dPseudoScalar mv)
        {
            var result = new e3dPseudoScalar();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:00.7014853+02:00
            //Macro: geometry3d.e3d.OP
            //Input Variables: 2 used, 6 not used, 8 total.
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
            //   mv1.#e1# <=> <Variable> G1I0
            //   mv1.#e2# <=> <Variable> G1I1
            //   mv1.#e3# <=> <Variable> G1I2
            //   mv1.#e1^e2# <=> <Variable> G2I0
            //   mv1.#e1^e3# <=> <Variable> G2I1
            //   mv1.#e2^e3# <=> <Variable> G2I2
            //   mv2.#e1^e2^e3# <=> <Variable> mv.G3I0
            
            
            result.G3I0 = (-1 * G0I0 * mv.G3I0);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:00.7034854+02:00
            
            return result;
        }
        
        public e3dMultivector14 GP(e3dPseudoScalar mv)
        {
            var result = new e3dMultivector14();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:00.7064856+02:00
            //Macro: geometry3d.e3d.GP
            //Input Variables: 8 used, 0 not used, 8 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 8 total.
            //Computations: 1.375 average, 11 total.
            //Memory Reads: 1.75 average, 14 total.
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
            //   mv1.#e1# <=> <Variable> G1I0
            //   mv1.#e2# <=> <Variable> G1I1
            //   mv1.#e3# <=> <Variable> G1I2
            //   mv1.#e1^e2# <=> <Variable> G2I0
            //   mv1.#e1^e3# <=> <Variable> G2I1
            //   mv1.#e2^e3# <=> <Variable> G2I2
            //   mv2.#e1^e2^e3# <=> <Variable> mv.G3I0
            
            
            result.G1I0 = (G2I2 * mv.G3I0);
            result.G1I1 = (-1 * G2I1 * mv.G3I0);
            result.G2I0 = (-1 * G1I2 * mv.G3I0);
            result.G1I2 = (G2I0 * mv.G3I0);
            result.G2I1 = (G1I1 * mv.G3I0);
            result.G2I2 = (-1 * G1I0 * mv.G3I0);
            result.G3I0 = (-1 * G0I0 * mv.G3I0);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:00.7084857+02:00
            
            return result;
        }
        
        public e3dMultivector14 LCP(e3dPseudoScalar mv)
        {
            var result = new e3dMultivector14();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:00.7124859+02:00
            //Macro: geometry3d.e3d.LCP
            //Input Variables: 8 used, 0 not used, 8 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 8 total.
            //Computations: 1.375 average, 11 total.
            //Memory Reads: 1.75 average, 14 total.
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
            //   mv1.#e1# <=> <Variable> G1I0
            //   mv1.#e2# <=> <Variable> G1I1
            //   mv1.#e3# <=> <Variable> G1I2
            //   mv1.#e1^e2# <=> <Variable> G2I0
            //   mv1.#e1^e3# <=> <Variable> G2I1
            //   mv1.#e2^e3# <=> <Variable> G2I2
            //   mv2.#e1^e2^e3# <=> <Variable> mv.G3I0
            
            
            result.G1I0 = (G2I2 * mv.G3I0);
            result.G1I1 = (-1 * G2I1 * mv.G3I0);
            result.G2I0 = (-1 * G1I2 * mv.G3I0);
            result.G1I2 = (G2I0 * mv.G3I0);
            result.G2I1 = (G1I1 * mv.G3I0);
            result.G2I2 = (-1 * G1I0 * mv.G3I0);
            result.G3I0 = (-1 * G0I0 * mv.G3I0);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:00.7134860+02:00
            
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
        
        public e3dFull Add(e3dPseudoScalar mv)
        {
            return new e3dFull()
            {
                G0I0 = G0I0,
                G1I0 = G1I0,
                G1I1 = G1I1,
                G1I2 = G1I2,
                G2I0 = G2I0,
                G2I1 = G2I1,
                G2I2 = G2I2,
                G3I0 = mv.G3I0
            };
        }
        
        public e3dFull Subtract(e3dPseudoScalar mv)
        {
            return new e3dFull()
            {
                G0I0 = G0I0,
                G1I0 = G1I0,
                G1I1 = G1I1,
                G1I2 = G1I2,
                G2I0 = G2I0,
                G2I1 = G2I1,
                G2I2 = G2I2,
                G3I0 = -mv.G3I0
            };
        }
        
        public bool IsEqual(e3dPseudoScalar mv)
        {
            return !(
                G0I0 <= -Epsilon || G0I0 >= Epsilon || 
                G1I0 <= -Epsilon || G1I0 >= Epsilon || 
                G1I1 <= -Epsilon || G1I1 >= Epsilon || 
                G1I2 <= -Epsilon || G1I2 >= Epsilon || 
                G2I0 <= -Epsilon || G2I0 >= Epsilon || 
                G2I1 <= -Epsilon || G2I1 >= Epsilon || 
                G2I2 <= -Epsilon || G2I2 >= Epsilon || 
                mv.G3I0 <= -Epsilon || mv.G3I0 >= Epsilon
            );
        }
        
    }
}