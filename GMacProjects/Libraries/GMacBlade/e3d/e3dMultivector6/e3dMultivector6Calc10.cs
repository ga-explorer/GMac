namespace GMacModel.e3d
{
    public sealed partial class e3dMultivector6 : e3dMultivector
    {
        
        public e3dMultivector12 OP(e3dMultivector10 mv)
        {
            var result = new e3dMultivector12();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:00.1674547+02:00
            //Macro: geometry3d.e3d.OP
            //Input Variables: 9 used, 1 not used, 10 total.
            //Temp Variables: 10 sub-expressions, 0 generated temps, 10 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 8 total.
            //Computations: 1.05555555555556 average, 19 total.
            //Memory Reads: 1.55555555555556 average, 28 total.
            //Memory Writes: 18 total.
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
            //   mv2.#e1# <=> <Variable> mv.G1I0
            //   mv2.#e2# <=> <Variable> mv.G1I1
            //   mv2.#e3# <=> <Variable> mv.G1I2
            //   mv2.#e1^e2^e3# <=> <Variable> mv.G3I0
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = (G1I1 * mv.G1I0);
            tempVar0001 = (-1 * G1I0 * mv.G1I1);
            result.G2I0 = (tempVar0000 + tempVar0001);
            tempVar0000 = (G1I2 * mv.G1I0);
            tempVar0001 = (-1 * G1I0 * mv.G1I2);
            result.G2I1 = (tempVar0000 + tempVar0001);
            tempVar0000 = (G1I2 * mv.G1I1);
            tempVar0001 = (-1 * G1I1 * mv.G1I2);
            result.G2I2 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G2I2 * mv.G1I0);
            tempVar0001 = (G2I1 * mv.G1I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G2I0 * mv.G1I2);
            result.G3I0 = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:00.1694549+02:00
            
            return result;
        }
        
        public e3dFull GP(e3dMultivector10 mv)
        {
            var result = new e3dFull();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:00.1754552+02:00
            //Macro: geometry3d.e3d.GP
            //Input Variables: 10 used, 0 not used, 10 total.
            //Temp Variables: 32 sub-expressions, 0 generated temps, 32 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 8 total.
            //Computations: 1.35 average, 54 total.
            //Memory Reads: 2 average, 80 total.
            //Memory Writes: 40 total.
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
            //   mv2.#e1# <=> <Variable> mv.G1I0
            //   mv2.#e2# <=> <Variable> mv.G1I1
            //   mv2.#e3# <=> <Variable> mv.G1I2
            //   mv2.#e1^e2^e3# <=> <Variable> mv.G3I0
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = (-1 * G1I0 * mv.G1I0);
            tempVar0001 = (-1 * G1I1 * mv.G1I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G1I2 * mv.G1I2);
            result.G0I0 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G2I0 * mv.G1I1);
            tempVar0001 = (-1 * G2I1 * mv.G1I2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G2I2 * mv.G3I0);
            result.G1I0 = (tempVar0000 + tempVar0001);
            tempVar0000 = (G2I0 * mv.G1I0);
            tempVar0001 = (-1 * G2I2 * mv.G1I2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G2I1 * mv.G3I0);
            result.G1I1 = (tempVar0000 + tempVar0001);
            tempVar0000 = (G1I1 * mv.G1I0);
            tempVar0001 = (-1 * G1I0 * mv.G1I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G1I2 * mv.G3I0);
            result.G2I0 = (tempVar0000 + tempVar0001);
            tempVar0000 = (G2I1 * mv.G1I0);
            tempVar0001 = (G2I2 * mv.G1I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G2I0 * mv.G3I0);
            result.G1I2 = (tempVar0000 + tempVar0001);
            tempVar0000 = (G1I2 * mv.G1I0);
            tempVar0001 = (-1 * G1I0 * mv.G1I2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G1I1 * mv.G3I0);
            result.G2I1 = (tempVar0000 + tempVar0001);
            tempVar0000 = (G1I2 * mv.G1I1);
            tempVar0001 = (-1 * G1I1 * mv.G1I2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G1I0 * mv.G3I0);
            result.G2I2 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G2I2 * mv.G1I0);
            tempVar0001 = (G2I1 * mv.G1I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G2I0 * mv.G1I2);
            result.G3I0 = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:00.1804555+02:00
            
            return result;
        }
        
        public e3dMultivector7 LCP(e3dMultivector10 mv)
        {
            var result = new e3dMultivector7();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:00.1844557+02:00
            //Macro: geometry3d.e3d.LCP
            //Input Variables: 10 used, 0 not used, 10 total.
            //Temp Variables: 4 sub-expressions, 0 generated temps, 4 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 8 total.
            //Computations: 1.41666666666667 average, 17 total.
            //Memory Reads: 1.83333333333333 average, 22 total.
            //Memory Writes: 12 total.
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
            //   mv2.#e1# <=> <Variable> mv.G1I0
            //   mv2.#e2# <=> <Variable> mv.G1I1
            //   mv2.#e3# <=> <Variable> mv.G1I2
            //   mv2.#e1^e2^e3# <=> <Variable> mv.G3I0
            
            double tempVar0000;
            double tempVar0001;
            
            result.G1I0 = (G2I2 * mv.G3I0);
            result.G1I1 = (-1 * G2I1 * mv.G3I0);
            result.G2I0 = (-1 * G1I2 * mv.G3I0);
            result.G1I2 = (G2I0 * mv.G3I0);
            result.G2I1 = (G1I1 * mv.G3I0);
            result.G2I2 = (-1 * G1I0 * mv.G3I0);
            tempVar0000 = (-1 * G1I0 * mv.G1I0);
            tempVar0001 = (-1 * G1I1 * mv.G1I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G1I2 * mv.G1I2);
            result.G0I0 = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:00.1864558+02:00
            
            return result;
        }
        
        public e3dMultivector3 RCP(e3dMultivector10 mv)
        {
            var result = new e3dMultivector3();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:00.1904561+02:00
            //Macro: geometry3d.e3d.RCP
            //Input Variables: 9 used, 1 not used, 10 total.
            //Temp Variables: 10 sub-expressions, 0 generated temps, 10 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 8 total.
            //Computations: 1.11111111111111 average, 20 total.
            //Memory Reads: 1.55555555555556 average, 28 total.
            //Memory Writes: 18 total.
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
            //   mv2.#e1# <=> <Variable> mv.G1I0
            //   mv2.#e2# <=> <Variable> mv.G1I1
            //   mv2.#e3# <=> <Variable> mv.G1I2
            //   mv2.#e1^e2^e3# <=> <Variable> mv.G3I0
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = (-1 * G2I0 * mv.G1I1);
            tempVar0001 = (-1 * G2I1 * mv.G1I2);
            result.G1I0 = (tempVar0000 + tempVar0001);
            tempVar0000 = (G2I0 * mv.G1I0);
            tempVar0001 = (-1 * G2I2 * mv.G1I2);
            result.G1I1 = (tempVar0000 + tempVar0001);
            tempVar0000 = (G2I1 * mv.G1I0);
            tempVar0001 = (G2I2 * mv.G1I1);
            result.G1I2 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G1I0 * mv.G1I0);
            tempVar0001 = (-1 * G1I1 * mv.G1I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G1I2 * mv.G1I2);
            result.G0I0 = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:00.1954564+02:00
            
            return result;
        }
        
        public double SP(e3dMultivector10 mv)
        {
            var result = 0.0D;
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:00.1974565+02:00
            //Macro: geometry3d.e3d.SP
            //Input Variables: 6 used, 4 not used, 10 total.
            //Temp Variables: 4 sub-expressions, 0 generated temps, 4 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 1 total.
            //Computations: 1.6 average, 8 total.
            //Memory Reads: 2 average, 10 total.
            //Memory Writes: 5 total.
            //
            //Macro Binding Data: 
            //   result <=> <Variable> result
            //   mv1.#e1# <=> <Variable> G1I0
            //   mv1.#e2# <=> <Variable> G1I1
            //   mv1.#e3# <=> <Variable> G1I2
            //   mv1.#e1^e2# <=> <Variable> G2I0
            //   mv1.#e1^e3# <=> <Variable> G2I1
            //   mv1.#e2^e3# <=> <Variable> G2I2
            //   mv2.#e1# <=> <Variable> mv.G1I0
            //   mv2.#e2# <=> <Variable> mv.G1I1
            //   mv2.#e3# <=> <Variable> mv.G1I2
            //   mv2.#e1^e2^e3# <=> <Variable> mv.G3I0
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = (-1 * G1I0 * mv.G1I0);
            tempVar0001 = (-1 * G1I1 * mv.G1I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G1I2 * mv.G1I2);
            result = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:00.1984565+02:00
            
            return result;
        }
        
        public e3dMultivector14 Add(e3dMultivector10 mv)
        {
            return new e3dMultivector14()
            {
                G2I0 = G2I0,
                G2I1 = G2I1,
                G2I2 = G2I2,
                G3I0 = mv.G3I0,
                G1I0 = G1I0 + mv.G1I0,
                G1I1 = G1I1 + mv.G1I1,
                G1I2 = G1I2 + mv.G1I2
            };
        }
        
        public e3dMultivector14 Subtract(e3dMultivector10 mv)
        {
            return new e3dMultivector14()
            {
                G2I0 = G2I0,
                G2I1 = G2I1,
                G2I2 = G2I2,
                G3I0 = -mv.G3I0,
                G1I0 = G1I0 - mv.G1I0,
                G1I1 = G1I1 - mv.G1I1,
                G1I2 = G1I2 - mv.G1I2
            };
        }
        
        public bool IsEqual(e3dMultivector10 mv)
        {
            return !(
                G2I0 <= -Epsilon || G2I0 >= Epsilon || 
                G2I1 <= -Epsilon || G2I1 >= Epsilon || 
                G2I2 <= -Epsilon || G2I2 >= Epsilon || 
                mv.G3I0 <= -Epsilon || mv.G3I0 >= Epsilon || 
                (G1I0 - mv.G1I0) <= -Epsilon || (G1I0 - mv.G1I0) >= Epsilon || 
                (G1I1 - mv.G1I1) <= -Epsilon || (G1I1 - mv.G1I1) >= Epsilon || 
                (G1I2 - mv.G1I2) <= -Epsilon || (G1I2 - mv.G1I2) >= Epsilon
            );
        }
        
    }
}