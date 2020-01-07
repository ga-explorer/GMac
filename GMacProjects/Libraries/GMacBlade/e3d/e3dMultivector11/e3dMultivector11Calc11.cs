namespace GMacModel.e3d
{
    public sealed partial class e3dMultivector11 : e3dMultivector
    {
        
        public e3dFull OP(e3dMultivector11 mv)
        {
            var result = new e3dFull();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:02.4965880+02:00
            //Macro: geometry3d.e3d.OP
            //Input Variables: 10 used, 0 not used, 10 total.
            //Temp Variables: 14 sub-expressions, 0 generated temps, 14 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 8 total.
            //Computations: 1.54545454545455 average, 34 total.
            //Memory Reads: 2 average, 44 total.
            //Memory Writes: 22 total.
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
            //   mv1.#e1^e2^e3# <=> <Variable> G3I0
            //   mv2.#E0# <=> <Variable> mv.G0I0
            //   mv2.#e1# <=> <Variable> mv.G1I0
            //   mv2.#e2# <=> <Variable> mv.G1I1
            //   mv2.#e3# <=> <Variable> mv.G1I2
            //   mv2.#e1^e2^e3# <=> <Variable> mv.G3I0
            
            double tempVar0000;
            double tempVar0001;
            
            result.G0I0 = (-1 * G0I0 * mv.G0I0);
            tempVar0000 = (-1 * G1I0 * mv.G0I0);
            tempVar0001 = (-1 * G0I0 * mv.G1I0);
            result.G1I0 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G1I1 * mv.G0I0);
            tempVar0001 = (-1 * G0I0 * mv.G1I1);
            result.G1I1 = (tempVar0000 + tempVar0001);
            tempVar0000 = (G1I1 * mv.G1I0);
            tempVar0001 = (-1 * G1I0 * mv.G1I1);
            result.G2I0 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G1I2 * mv.G0I0);
            tempVar0001 = (-1 * G0I0 * mv.G1I2);
            result.G1I2 = (tempVar0000 + tempVar0001);
            tempVar0000 = (G1I2 * mv.G1I0);
            tempVar0001 = (-1 * G1I0 * mv.G1I2);
            result.G2I1 = (tempVar0000 + tempVar0001);
            tempVar0000 = (G1I2 * mv.G1I1);
            tempVar0001 = (-1 * G1I1 * mv.G1I2);
            result.G2I2 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G3I0 * mv.G0I0);
            tempVar0001 = (-1 * G0I0 * mv.G3I0);
            result.G3I0 = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:02.5005882+02:00
            
            return result;
        }
        
        public e3dFull GP(e3dMultivector11 mv)
        {
            var result = new e3dFull();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:02.5055885+02:00
            //Macro: geometry3d.e3d.GP
            //Input Variables: 10 used, 0 not used, 10 total.
            //Temp Variables: 34 sub-expressions, 0 generated temps, 34 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 8 total.
            //Computations: 1.45238095238095 average, 61 total.
            //Memory Reads: 2 average, 84 total.
            //Memory Writes: 42 total.
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
            //   mv1.#e1^e2^e3# <=> <Variable> G3I0
            //   mv2.#E0# <=> <Variable> mv.G0I0
            //   mv2.#e1# <=> <Variable> mv.G1I0
            //   mv2.#e2# <=> <Variable> mv.G1I1
            //   mv2.#e3# <=> <Variable> mv.G1I2
            //   mv2.#e1^e2^e3# <=> <Variable> mv.G3I0
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = (-1 * G1I0 * mv.G0I0);
            tempVar0001 = (-1 * G0I0 * mv.G1I0);
            result.G1I0 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G1I1 * mv.G0I0);
            tempVar0001 = (-1 * G0I0 * mv.G1I1);
            result.G1I1 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G1I2 * mv.G0I0);
            tempVar0001 = (-1 * G0I0 * mv.G1I2);
            result.G1I2 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G3I0 * mv.G0I0);
            tempVar0001 = (-1 * G0I0 * mv.G3I0);
            result.G3I0 = (tempVar0000 + tempVar0001);
            tempVar0000 = (G1I1 * mv.G1I0);
            tempVar0001 = (-1 * G1I0 * mv.G1I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G3I0 * mv.G1I2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G1I2 * mv.G3I0);
            result.G2I0 = (tempVar0000 + tempVar0001);
            tempVar0000 = (G1I2 * mv.G1I0);
            tempVar0001 = (G3I0 * mv.G1I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G1I0 * mv.G1I2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G1I1 * mv.G3I0);
            result.G2I1 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G3I0 * mv.G1I0);
            tempVar0001 = (G1I2 * mv.G1I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G1I1 * mv.G1I2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G1I0 * mv.G3I0);
            result.G2I2 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G0I0 * mv.G0I0);
            tempVar0001 = (-1 * G1I0 * mv.G1I0);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G1I1 * mv.G1I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G1I2 * mv.G1I2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G3I0 * mv.G3I0);
            result.G0I0 = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:02.5105888+02:00
            
            return result;
        }
        
        public e3dFull LCP(e3dMultivector11 mv)
        {
            var result = new e3dFull();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:02.5145890+02:00
            //Macro: geometry3d.e3d.LCP
            //Input Variables: 10 used, 0 not used, 10 total.
            //Temp Variables: 8 sub-expressions, 0 generated temps, 8 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 8 total.
            //Computations: 1.625 average, 26 total.
            //Memory Reads: 2 average, 32 total.
            //Memory Writes: 16 total.
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
            //   mv1.#e1^e2^e3# <=> <Variable> G3I0
            //   mv2.#E0# <=> <Variable> mv.G0I0
            //   mv2.#e1# <=> <Variable> mv.G1I0
            //   mv2.#e2# <=> <Variable> mv.G1I1
            //   mv2.#e3# <=> <Variable> mv.G1I2
            //   mv2.#e1^e2^e3# <=> <Variable> mv.G3I0
            
            double tempVar0000;
            double tempVar0001;
            
            result.G1I0 = (-1 * G0I0 * mv.G1I0);
            result.G1I1 = (-1 * G0I0 * mv.G1I1);
            result.G2I0 = (-1 * G1I2 * mv.G3I0);
            result.G1I2 = (-1 * G0I0 * mv.G1I2);
            result.G2I1 = (G1I1 * mv.G3I0);
            result.G2I2 = (-1 * G1I0 * mv.G3I0);
            result.G3I0 = (-1 * G0I0 * mv.G3I0);
            tempVar0000 = (-1 * G0I0 * mv.G0I0);
            tempVar0001 = (-1 * G1I0 * mv.G1I0);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G1I1 * mv.G1I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G1I2 * mv.G1I2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G3I0 * mv.G3I0);
            result.G0I0 = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:02.5165891+02:00
            
            return result;
        }
        
        public e3dFull RCP(e3dMultivector11 mv)
        {
            var result = new e3dFull();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:02.5215894+02:00
            //Macro: geometry3d.e3d.RCP
            //Input Variables: 10 used, 0 not used, 10 total.
            //Temp Variables: 8 sub-expressions, 0 generated temps, 8 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 8 total.
            //Computations: 1.625 average, 26 total.
            //Memory Reads: 2 average, 32 total.
            //Memory Writes: 16 total.
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
            //   mv1.#e1^e2^e3# <=> <Variable> G3I0
            //   mv2.#E0# <=> <Variable> mv.G0I0
            //   mv2.#e1# <=> <Variable> mv.G1I0
            //   mv2.#e2# <=> <Variable> mv.G1I1
            //   mv2.#e3# <=> <Variable> mv.G1I2
            //   mv2.#e1^e2^e3# <=> <Variable> mv.G3I0
            
            double tempVar0000;
            double tempVar0001;
            
            result.G1I0 = (-1 * G1I0 * mv.G0I0);
            result.G1I1 = (-1 * G1I1 * mv.G0I0);
            result.G2I0 = (-1 * G3I0 * mv.G1I2);
            result.G1I2 = (-1 * G1I2 * mv.G0I0);
            result.G2I1 = (G3I0 * mv.G1I1);
            result.G2I2 = (-1 * G3I0 * mv.G1I0);
            result.G3I0 = (-1 * G3I0 * mv.G0I0);
            tempVar0000 = (-1 * G0I0 * mv.G0I0);
            tempVar0001 = (-1 * G1I0 * mv.G1I0);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G1I1 * mv.G1I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G1I2 * mv.G1I2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G3I0 * mv.G3I0);
            result.G0I0 = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:02.5235895+02:00
            
            return result;
        }
        
        public double SP(e3dMultivector11 mv)
        {
            var result = 0.0D;
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:02.5265897+02:00
            //Macro: geometry3d.e3d.SP
            //Input Variables: 10 used, 0 not used, 10 total.
            //Temp Variables: 8 sub-expressions, 0 generated temps, 8 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 1 total.
            //Computations: 1.44444444444444 average, 13 total.
            //Memory Reads: 2 average, 18 total.
            //Memory Writes: 9 total.
            //
            //Macro Binding Data: 
            //   result <=> <Variable> result
            //   mv1.#E0# <=> <Variable> G0I0
            //   mv1.#e1# <=> <Variable> G1I0
            //   mv1.#e2# <=> <Variable> G1I1
            //   mv1.#e3# <=> <Variable> G1I2
            //   mv1.#e1^e2^e3# <=> <Variable> G3I0
            //   mv2.#E0# <=> <Variable> mv.G0I0
            //   mv2.#e1# <=> <Variable> mv.G1I0
            //   mv2.#e2# <=> <Variable> mv.G1I1
            //   mv2.#e3# <=> <Variable> mv.G1I2
            //   mv2.#e1^e2^e3# <=> <Variable> mv.G3I0
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = (-1 * G0I0 * mv.G0I0);
            tempVar0001 = (-1 * G1I0 * mv.G1I0);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G1I1 * mv.G1I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G1I2 * mv.G1I2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G3I0 * mv.G3I0);
            result = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:02.5275897+02:00
            
            return result;
        }
        
        public e3dMultivector11 Add(e3dMultivector11 mv)
        {
            return new e3dMultivector11()
            {
                G0I0 = G0I0 + mv.G0I0,
                G1I0 = G1I0 + mv.G1I0,
                G1I1 = G1I1 + mv.G1I1,
                G1I2 = G1I2 + mv.G1I2,
                G3I0 = G3I0 + mv.G3I0
            };
        }
        
        public e3dMultivector11 Subtract(e3dMultivector11 mv)
        {
            return new e3dMultivector11()
            {
                G0I0 = G0I0 - mv.G0I0,
                G1I0 = G1I0 - mv.G1I0,
                G1I1 = G1I1 - mv.G1I1,
                G1I2 = G1I2 - mv.G1I2,
                G3I0 = G3I0 - mv.G3I0
            };
        }
        
        public bool IsEqual(e3dMultivector11 mv)
        {
            return !(
                (G0I0 - mv.G0I0) <= -Epsilon || (G0I0 - mv.G0I0) >= Epsilon || 
                (G1I0 - mv.G1I0) <= -Epsilon || (G1I0 - mv.G1I0) >= Epsilon || 
                (G1I1 - mv.G1I1) <= -Epsilon || (G1I1 - mv.G1I1) >= Epsilon || 
                (G1I2 - mv.G1I2) <= -Epsilon || (G1I2 - mv.G1I2) >= Epsilon || 
                (G3I0 - mv.G3I0) <= -Epsilon || (G3I0 - mv.G3I0) >= Epsilon
            );
        }
        
    }
}