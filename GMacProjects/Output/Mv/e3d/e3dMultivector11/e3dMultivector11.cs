using System;

namespace GMacModel.e3d
{
    public sealed partial class e3dMultivector11 : e3dMultivector
    {
        public double G0I0 { get; set; }
        public double G1I0 { get; set; }
        public double G1I1 { get; set; }
        public double G1I2 { get; set; }
        public double G3I0 { get; set; }
        
        public override double Coef0 { get { return G0I0; } }
        public override double Coef1 { get { return G1I0; } }
        public override double Coef2 { get { return G1I1; } }
        public override double Coef3 { get { return 0.0D; } }
        public override double Coef4 { get { return G1I2; } }
        public override double Coef5 { get { return 0.0D; } }
        public override double Coef6 { get { return 0.0D; } }
        public override double Coef7 { get { return G3I0; } }
        
        
        public e3dMultivector11()
        {
            ClassId = 11;
        }
        
        
        public override double Norm2
        {
            get
            {
                //Bagin GMac Macro Code Generation, 2016-01-11T22:36:02.1795698+02:00
                //Macro: geometry3d.e3d.Norm2
                //Input Variables: 5 used, 0 not used, 5 total.
                //Temp Variables: 13 sub-expressions, 0 generated temps, 13 total.
                //Target Temp Variables: 2 total.
                //Output Variables: 1 total.
                //Computations: 1 average, 14 total.
                //Memory Reads: 1.28571428571429 average, 18 total.
                //Memory Writes: 14 total.
                //
                //Macro Binding Data: 
                //   result <=> <Variable> result
                //   mv.#E0# <=> <Variable> G0I0
                //   mv.#e1# <=> <Variable> G1I0
                //   mv.#e2# <=> <Variable> G1I1
                //   mv.#e3# <=> <Variable> G1I2
                //   mv.#e1^e2^e3# <=> <Variable> G3I0
                
                double tempVar0000;
                double tempVar0001;
                
                tempVar0000 = Math.Pow(G0I0, 2);
                tempVar0000 = (-1 * tempVar0000);
                tempVar0001 = Math.Pow(G1I0, 2);
                tempVar0001 = (-1 * tempVar0001);
                tempVar0000 = (tempVar0000 + tempVar0001);
                tempVar0001 = Math.Pow(G1I1, 2);
                tempVar0001 = (-1 * tempVar0001);
                tempVar0000 = (tempVar0000 + tempVar0001);
                tempVar0001 = Math.Pow(G1I2, 2);
                tempVar0001 = (-1 * tempVar0001);
                tempVar0000 = (tempVar0000 + tempVar0001);
                tempVar0001 = Math.Pow(G3I0, 2);
                tempVar0001 = (-1 * tempVar0001);
                
                return (tempVar0000 + tempVar0001);
                
                //Finish GMac Macro Code Generation, 2016-01-11T22:36:02.1815700+02:00
                
            }
        }
        
        public override bool IsZero
        {
            get
            {
                return !(
                    G0I0 <= -Epsilon || G0I0 >= Epsilon || 
                    G1I0 <= -Epsilon || G1I0 >= Epsilon || 
                    G1I1 <= -Epsilon || G1I1 >= Epsilon || 
                    G1I2 <= -Epsilon || G1I2 >= Epsilon || 
                    G3I0 <= -Epsilon || G3I0 >= Epsilon
                );
            }
        }
        
        public override bool IsEqual(e3dMultivector mv)
        {
            switch (mv.ClassId)
            {
                case 0: return IsEqual((e3dZero)mv);
                case 1: return IsEqual((e3dScalar)mv);
                case 2: return IsEqual((e3dVector)mv);
                case 3: return IsEqual((e3dMultivector3)mv);
                case 4: return IsEqual((e3dPseudoVector)mv);
                case 5: return IsEqual((e3dMultivector5)mv);
                case 6: return IsEqual((e3dMultivector6)mv);
                case 7: return IsEqual((e3dMultivector7)mv);
                case 8: return IsEqual((e3dPseudoScalar)mv);
                case 9: return IsEqual((e3dMultivector9)mv);
                case 10: return IsEqual((e3dMultivector10)mv);
                case 11: return IsEqual((e3dMultivector11)mv);
                case 12: return IsEqual((e3dMultivector12)mv);
                case 13: return IsEqual((e3dMultivector13)mv);
                case 14: return IsEqual((e3dMultivector14)mv);
                case 15: return IsEqual((e3dFull)mv);
                default: throw new InvalidOperationException();
            }
        }
        
        public override e3dMultivector Simplify()
        {
            throw new NotImplementedException();
        }
        
        public override e3dMultivector OP(e3dMultivector mv)
        {
            switch (mv.ClassId)
            {
                case 0: return OP((e3dZero)mv);
                case 1: return OP((e3dScalar)mv);
                case 2: return OP((e3dVector)mv);
                case 3: return OP((e3dMultivector3)mv);
                case 4: return OP((e3dPseudoVector)mv);
                case 5: return OP((e3dMultivector5)mv);
                case 6: return OP((e3dMultivector6)mv);
                case 7: return OP((e3dMultivector7)mv);
                case 8: return OP((e3dPseudoScalar)mv);
                case 9: return OP((e3dMultivector9)mv);
                case 10: return OP((e3dMultivector10)mv);
                case 11: return OP((e3dMultivector11)mv);
                case 12: return OP((e3dMultivector12)mv);
                case 13: return OP((e3dMultivector13)mv);
                case 14: return OP((e3dMultivector14)mv);
                case 15: return OP((e3dFull)mv);
                default: throw new InvalidOperationException();
            }
        }
        
        public override e3dMultivector GP(e3dMultivector mv)
        {
            switch (mv.ClassId)
            {
                case 0: return GP((e3dZero)mv);
                case 1: return GP((e3dScalar)mv);
                case 2: return GP((e3dVector)mv);
                case 3: return GP((e3dMultivector3)mv);
                case 4: return GP((e3dPseudoVector)mv);
                case 5: return GP((e3dMultivector5)mv);
                case 6: return GP((e3dMultivector6)mv);
                case 7: return GP((e3dMultivector7)mv);
                case 8: return GP((e3dPseudoScalar)mv);
                case 9: return GP((e3dMultivector9)mv);
                case 10: return GP((e3dMultivector10)mv);
                case 11: return GP((e3dMultivector11)mv);
                case 12: return GP((e3dMultivector12)mv);
                case 13: return GP((e3dMultivector13)mv);
                case 14: return GP((e3dMultivector14)mv);
                case 15: return GP((e3dFull)mv);
                default: throw new InvalidOperationException();
            }
        }
        
        public override e3dMultivector LCP(e3dMultivector mv)
        {
            switch (mv.ClassId)
            {
                case 0: return LCP((e3dZero)mv);
                case 1: return LCP((e3dScalar)mv);
                case 2: return LCP((e3dVector)mv);
                case 3: return LCP((e3dMultivector3)mv);
                case 4: return LCP((e3dPseudoVector)mv);
                case 5: return LCP((e3dMultivector5)mv);
                case 6: return LCP((e3dMultivector6)mv);
                case 7: return LCP((e3dMultivector7)mv);
                case 8: return LCP((e3dPseudoScalar)mv);
                case 9: return LCP((e3dMultivector9)mv);
                case 10: return LCP((e3dMultivector10)mv);
                case 11: return LCP((e3dMultivector11)mv);
                case 12: return LCP((e3dMultivector12)mv);
                case 13: return LCP((e3dMultivector13)mv);
                case 14: return LCP((e3dMultivector14)mv);
                case 15: return LCP((e3dFull)mv);
                default: throw new InvalidOperationException();
            }
        }
        
        public override e3dMultivector RCP(e3dMultivector mv)
        {
            switch (mv.ClassId)
            {
                case 0: return RCP((e3dZero)mv);
                case 1: return RCP((e3dScalar)mv);
                case 2: return RCP((e3dVector)mv);
                case 3: return RCP((e3dMultivector3)mv);
                case 4: return RCP((e3dPseudoVector)mv);
                case 5: return RCP((e3dMultivector5)mv);
                case 6: return RCP((e3dMultivector6)mv);
                case 7: return RCP((e3dMultivector7)mv);
                case 8: return RCP((e3dPseudoScalar)mv);
                case 9: return RCP((e3dMultivector9)mv);
                case 10: return RCP((e3dMultivector10)mv);
                case 11: return RCP((e3dMultivector11)mv);
                case 12: return RCP((e3dMultivector12)mv);
                case 13: return RCP((e3dMultivector13)mv);
                case 14: return RCP((e3dMultivector14)mv);
                case 15: return RCP((e3dFull)mv);
                default: throw new InvalidOperationException();
            }
        }
        
        public override double SP(e3dMultivector mv)
        {
            switch (mv.ClassId)
            {
                case 0: return SP((e3dZero)mv);
                case 1: return SP((e3dScalar)mv);
                case 2: return SP((e3dVector)mv);
                case 3: return SP((e3dMultivector3)mv);
                case 4: return SP((e3dPseudoVector)mv);
                case 5: return SP((e3dMultivector5)mv);
                case 6: return SP((e3dMultivector6)mv);
                case 7: return SP((e3dMultivector7)mv);
                case 8: return SP((e3dPseudoScalar)mv);
                case 9: return SP((e3dMultivector9)mv);
                case 10: return SP((e3dMultivector10)mv);
                case 11: return SP((e3dMultivector11)mv);
                case 12: return SP((e3dMultivector12)mv);
                case 13: return SP((e3dMultivector13)mv);
                case 14: return SP((e3dMultivector14)mv);
                case 15: return SP((e3dFull)mv);
                default: throw new InvalidOperationException();
            }
        }
        
        public override e3dMultivector Add(e3dMultivector mv)
        {
            switch (mv.ClassId)
            {
                case 0: return Add((e3dZero)mv);
                case 1: return Add((e3dScalar)mv);
                case 2: return Add((e3dVector)mv);
                case 3: return Add((e3dMultivector3)mv);
                case 4: return Add((e3dPseudoVector)mv);
                case 5: return Add((e3dMultivector5)mv);
                case 6: return Add((e3dMultivector6)mv);
                case 7: return Add((e3dMultivector7)mv);
                case 8: return Add((e3dPseudoScalar)mv);
                case 9: return Add((e3dMultivector9)mv);
                case 10: return Add((e3dMultivector10)mv);
                case 11: return Add((e3dMultivector11)mv);
                case 12: return Add((e3dMultivector12)mv);
                case 13: return Add((e3dMultivector13)mv);
                case 14: return Add((e3dMultivector14)mv);
                case 15: return Add((e3dFull)mv);
                default: throw new InvalidOperationException();
            }
        }
        
        public override e3dMultivector Subtract(e3dMultivector mv)
        {
            switch (mv.ClassId)
            {
                case 0: return Subtract((e3dZero)mv);
                case 1: return Subtract((e3dScalar)mv);
                case 2: return Subtract((e3dVector)mv);
                case 3: return Subtract((e3dMultivector3)mv);
                case 4: return Subtract((e3dPseudoVector)mv);
                case 5: return Subtract((e3dMultivector5)mv);
                case 6: return Subtract((e3dMultivector6)mv);
                case 7: return Subtract((e3dMultivector7)mv);
                case 8: return Subtract((e3dPseudoScalar)mv);
                case 9: return Subtract((e3dMultivector9)mv);
                case 10: return Subtract((e3dMultivector10)mv);
                case 11: return Subtract((e3dMultivector11)mv);
                case 12: return Subtract((e3dMultivector12)mv);
                case 13: return Subtract((e3dMultivector13)mv);
                case 14: return Subtract((e3dMultivector14)mv);
                case 15: return Subtract((e3dFull)mv);
                default: throw new InvalidOperationException();
            }
        }
        
    }
}