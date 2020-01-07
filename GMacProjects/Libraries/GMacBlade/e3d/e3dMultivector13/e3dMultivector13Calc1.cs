namespace GMacModel.e3d
{
    public sealed partial class e3dMultivector13 : e3dMultivector
    {
        
        public e3dMultivector13 OP(e3dScalar mv)
        {
            var result = new e3dMultivector13();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:03.1636261+02:00
            //Macro: geometry3d.e3d.OP
            //Input Variables: 6 used, 0 not used, 6 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 8 total.
            //Computations: 1.25 average, 10 total.
            //Memory Reads: 1.25 average, 10 total.
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
            //   mv1.#e1^e2# <=> <Variable> G2I0
            //   mv1.#e1^e3# <=> <Variable> G2I1
            //   mv1.#e2^e3# <=> <Variable> G2I2
            //   mv1.#e1^e2^e3# <=> <Variable> G3I0
            //   mv2.#E0# <=> <Variable> mv.G0I0
            
            
            result.G0I0 = (-1 * G0I0 * mv.G0I0);
            result.G2I0 = (-1 * G2I0 * mv.G0I0);
            result.G2I1 = (-1 * G2I1 * mv.G0I0);
            result.G2I2 = (-1 * G2I2 * mv.G0I0);
            result.G3I0 = (-1 * G3I0 * mv.G0I0);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:03.1646262+02:00
            
            return result;
        }
        
        public e3dMultivector13 GP(e3dScalar mv)
        {
            var result = new e3dMultivector13();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:03.1676263+02:00
            //Macro: geometry3d.e3d.GP
            //Input Variables: 6 used, 0 not used, 6 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 8 total.
            //Computations: 1.25 average, 10 total.
            //Memory Reads: 1.25 average, 10 total.
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
            //   mv1.#e1^e2# <=> <Variable> G2I0
            //   mv1.#e1^e3# <=> <Variable> G2I1
            //   mv1.#e2^e3# <=> <Variable> G2I2
            //   mv1.#e1^e2^e3# <=> <Variable> G3I0
            //   mv2.#E0# <=> <Variable> mv.G0I0
            
            
            result.G0I0 = (-1 * G0I0 * mv.G0I0);
            result.G2I0 = (-1 * G2I0 * mv.G0I0);
            result.G2I1 = (-1 * G2I1 * mv.G0I0);
            result.G2I2 = (-1 * G2I2 * mv.G0I0);
            result.G3I0 = (-1 * G3I0 * mv.G0I0);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:03.1686264+02:00
            
            return result;
        }
        
        public e3dScalar LCP(e3dScalar mv)
        {
            var result = new e3dScalar();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:03.1716266+02:00
            //Macro: geometry3d.e3d.LCP
            //Input Variables: 2 used, 4 not used, 6 total.
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
            //   mv1.#e1^e2# <=> <Variable> G2I0
            //   mv1.#e1^e3# <=> <Variable> G2I1
            //   mv1.#e2^e3# <=> <Variable> G2I2
            //   mv1.#e1^e2^e3# <=> <Variable> G3I0
            //   mv2.#E0# <=> <Variable> mv.G0I0
            
            
            result.G0I0 = (-1 * G0I0 * mv.G0I0);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:03.1726266+02:00
            
            return result;
        }
        
        public e3dMultivector13 RCP(e3dScalar mv)
        {
            var result = new e3dMultivector13();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:03.1756268+02:00
            //Macro: geometry3d.e3d.RCP
            //Input Variables: 6 used, 0 not used, 6 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 8 total.
            //Computations: 1.25 average, 10 total.
            //Memory Reads: 1.25 average, 10 total.
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
            //   mv1.#e1^e2# <=> <Variable> G2I0
            //   mv1.#e1^e3# <=> <Variable> G2I1
            //   mv1.#e2^e3# <=> <Variable> G2I2
            //   mv1.#e1^e2^e3# <=> <Variable> G3I0
            //   mv2.#E0# <=> <Variable> mv.G0I0
            
            
            result.G0I0 = (-1 * G0I0 * mv.G0I0);
            result.G2I0 = (-1 * G2I0 * mv.G0I0);
            result.G2I1 = (-1 * G2I1 * mv.G0I0);
            result.G2I2 = (-1 * G2I2 * mv.G0I0);
            result.G3I0 = (-1 * G3I0 * mv.G0I0);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:03.1766269+02:00
            
            return result;
        }
        
        public double SP(e3dScalar mv)
        {
            var result = 0.0D;
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:03.1776269+02:00
            //Macro: geometry3d.e3d.SP
            //Input Variables: 2 used, 4 not used, 6 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 1 total.
            //Computations: 2 average, 2 total.
            //Memory Reads: 2 average, 2 total.
            //Memory Writes: 1 total.
            //
            //Macro Binding Data: 
            //   result <=> <Variable> result
            //   mv1.#E0# <=> <Variable> G0I0
            //   mv1.#e1^e2# <=> <Variable> G2I0
            //   mv1.#e1^e3# <=> <Variable> G2I1
            //   mv1.#e2^e3# <=> <Variable> G2I2
            //   mv1.#e1^e2^e3# <=> <Variable> G3I0
            //   mv2.#E0# <=> <Variable> mv.G0I0
            
            
            result = (-1 * G0I0 * mv.G0I0);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:03.1786270+02:00
            
            return result;
        }
        
        public e3dMultivector13 Add(e3dScalar mv)
        {
            return new e3dMultivector13()
            {
                G2I0 = G2I0,
                G2I1 = G2I1,
                G2I2 = G2I2,
                G3I0 = G3I0,
                G0I0 = G0I0 + mv.G0I0
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
                G0I0 = G0I0 - mv.G0I0
            };
        }
        
        public bool IsEqual(e3dScalar mv)
        {
            return !(
                G2I0 <= -Epsilon || G2I0 >= Epsilon || 
                G2I1 <= -Epsilon || G2I1 >= Epsilon || 
                G2I2 <= -Epsilon || G2I2 >= Epsilon || 
                G3I0 <= -Epsilon || G3I0 >= Epsilon || 
                (G0I0 - mv.G0I0) <= -Epsilon || (G0I0 - mv.G0I0) >= Epsilon
            );
        }
        
    }
}