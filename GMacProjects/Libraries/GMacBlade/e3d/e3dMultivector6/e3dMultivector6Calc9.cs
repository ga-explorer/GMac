namespace GMacModel.e3d
{
    public sealed partial class e3dMultivector6 : e3dMultivector
    {
        
        public e3dMultivector6 OP(e3dMultivector9 mv)
        {
            var result = new e3dMultivector6();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:00.1434534+02:00
            //Macro: geometry3d.e3d.OP
            //Input Variables: 7 used, 1 not used, 8 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 8 total.
            //Computations: 1.5 average, 12 total.
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
            //   mv1.#e1# <=> <Variable> G1I0
            //   mv1.#e2# <=> <Variable> G1I1
            //   mv1.#e3# <=> <Variable> G1I2
            //   mv1.#e1^e2# <=> <Variable> G2I0
            //   mv1.#e1^e3# <=> <Variable> G2I1
            //   mv1.#e2^e3# <=> <Variable> G2I2
            //   mv2.#E0# <=> <Variable> mv.G0I0
            //   mv2.#e1^e2^e3# <=> <Variable> mv.G3I0
            
            
            result.G1I0 = (-1 * G1I0 * mv.G0I0);
            result.G1I1 = (-1 * G1I1 * mv.G0I0);
            result.G2I0 = (-1 * G2I0 * mv.G0I0);
            result.G1I2 = (-1 * G1I2 * mv.G0I0);
            result.G2I1 = (-1 * G2I1 * mv.G0I0);
            result.G2I2 = (-1 * G2I2 * mv.G0I0);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:00.1444534+02:00
            
            return result;
        }
        
        public e3dMultivector6 GP(e3dMultivector9 mv)
        {
            var result = new e3dMultivector6();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:00.1494537+02:00
            //Macro: geometry3d.e3d.GP
            //Input Variables: 8 used, 0 not used, 8 total.
            //Temp Variables: 12 sub-expressions, 0 generated temps, 12 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 8 total.
            //Computations: 1.35 average, 27 total.
            //Memory Reads: 1.8 average, 36 total.
            //Memory Writes: 20 total.
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
            //   mv1.#e1^e2# <=> <Variable> G2I0
            //   mv1.#e1^e3# <=> <Variable> G2I1
            //   mv1.#e2^e3# <=> <Variable> G2I2
            //   mv2.#E0# <=> <Variable> mv.G0I0
            //   mv2.#e1^e2^e3# <=> <Variable> mv.G3I0
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = (-1 * G1I0 * mv.G0I0);
            tempVar0001 = (G2I2 * mv.G3I0);
            result.G1I0 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G1I1 * mv.G0I0);
            tempVar0001 = (-1 * G2I1 * mv.G3I0);
            result.G1I1 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G2I0 * mv.G0I0);
            tempVar0001 = (-1 * G1I2 * mv.G3I0);
            result.G2I0 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G1I2 * mv.G0I0);
            tempVar0001 = (G2I0 * mv.G3I0);
            result.G1I2 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G2I1 * mv.G0I0);
            tempVar0001 = (G1I1 * mv.G3I0);
            result.G2I1 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G2I2 * mv.G0I0);
            tempVar0001 = (-1 * G1I0 * mv.G3I0);
            result.G2I2 = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:00.1514538+02:00
            
            return result;
        }
        
        public e3dMultivector6 LCP(e3dMultivector9 mv)
        {
            var result = new e3dMultivector6();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:00.1554541+02:00
            //Macro: geometry3d.e3d.LCP
            //Input Variables: 7 used, 1 not used, 8 total.
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
            //   mv1.#e1# <=> <Variable> G1I0
            //   mv1.#e2# <=> <Variable> G1I1
            //   mv1.#e3# <=> <Variable> G1I2
            //   mv1.#e1^e2# <=> <Variable> G2I0
            //   mv1.#e1^e3# <=> <Variable> G2I1
            //   mv1.#e2^e3# <=> <Variable> G2I2
            //   mv2.#E0# <=> <Variable> mv.G0I0
            //   mv2.#e1^e2^e3# <=> <Variable> mv.G3I0
            
            
            result.G1I0 = (G2I2 * mv.G3I0);
            result.G1I1 = (-1 * G2I1 * mv.G3I0);
            result.G2I0 = (-1 * G1I2 * mv.G3I0);
            result.G1I2 = (G2I0 * mv.G3I0);
            result.G2I1 = (G1I1 * mv.G3I0);
            result.G2I2 = (-1 * G1I0 * mv.G3I0);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:00.1564541+02:00
            
            return result;
        }
        
        public e3dMultivector6 RCP(e3dMultivector9 mv)
        {
            var result = new e3dMultivector6();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:00.1594543+02:00
            //Macro: geometry3d.e3d.RCP
            //Input Variables: 7 used, 1 not used, 8 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 8 total.
            //Computations: 1.5 average, 12 total.
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
            //   mv1.#e1# <=> <Variable> G1I0
            //   mv1.#e2# <=> <Variable> G1I1
            //   mv1.#e3# <=> <Variable> G1I2
            //   mv1.#e1^e2# <=> <Variable> G2I0
            //   mv1.#e1^e3# <=> <Variable> G2I1
            //   mv1.#e2^e3# <=> <Variable> G2I2
            //   mv2.#E0# <=> <Variable> mv.G0I0
            //   mv2.#e1^e2^e3# <=> <Variable> mv.G3I0
            
            
            result.G1I0 = (-1 * G1I0 * mv.G0I0);
            result.G1I1 = (-1 * G1I1 * mv.G0I0);
            result.G2I0 = (-1 * G2I0 * mv.G0I0);
            result.G1I2 = (-1 * G1I2 * mv.G0I0);
            result.G2I1 = (-1 * G2I1 * mv.G0I0);
            result.G2I2 = (-1 * G2I2 * mv.G0I0);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:00.1614544+02:00
            
            return result;
        }
        
        public double SP(e3dMultivector9 mv)
        {
            return 0.0D;
        }
        
        public e3dFull Add(e3dMultivector9 mv)
        {
            return new e3dFull()
            {
                G1I0 = G1I0,
                G1I1 = G1I1,
                G1I2 = G1I2,
                G2I0 = G2I0,
                G2I1 = G2I1,
                G2I2 = G2I2,
                G0I0 = mv.G0I0,
                G3I0 = mv.G3I0
            };
        }
        
        public e3dFull Subtract(e3dMultivector9 mv)
        {
            return new e3dFull()
            {
                G1I0 = G1I0,
                G1I1 = G1I1,
                G1I2 = G1I2,
                G2I0 = G2I0,
                G2I1 = G2I1,
                G2I2 = G2I2,
                G0I0 = -mv.G0I0,
                G3I0 = -mv.G3I0
            };
        }
        
        public bool IsEqual(e3dMultivector9 mv)
        {
            return !(
                G1I0 <= -Epsilon || G1I0 >= Epsilon || 
                G1I1 <= -Epsilon || G1I1 >= Epsilon || 
                G1I2 <= -Epsilon || G1I2 >= Epsilon || 
                G2I0 <= -Epsilon || G2I0 >= Epsilon || 
                G2I1 <= -Epsilon || G2I1 >= Epsilon || 
                G2I2 <= -Epsilon || G2I2 >= Epsilon || 
                mv.G0I0 <= -Epsilon || mv.G0I0 >= Epsilon || 
                mv.G3I0 <= -Epsilon || mv.G3I0 >= Epsilon
            );
        }
        
    }
}