using System.IO;

namespace GMacBlade.cga5d
{
    /// <summary>
    /// This class represents a mutable outermorphism in the cga5d frame by only storing a 5 by 5
    /// matrix of the original vector linear transform and computing the other k-vectors matrices as needed
    /// </summary>
    public sealed partial class cga5dOutermorphism
    {
        public double[,] Coefs { get; private set; }
        
        
        public cga5dOutermorphism()
        {
            Coefs = new double[cga5dBlade.MaxGrade, cga5dBlade.MaxGrade];
        }
        
        private cga5dOutermorphism(double[,] coefs)
        {
            Coefs = coefs;
        }
        
        
        public cga5dOutermorphism Transpose()
        {
            var coefs = new double[cga5dBlade.MaxGrade, cga5dBlade.MaxGrade];
        
            coefs[0, 0] = Coefs[0, 0];
            coefs[0, 1] = Coefs[1, 0];
            coefs[0, 2] = Coefs[2, 0];
            coefs[0, 3] = Coefs[3, 0];
            coefs[0, 4] = Coefs[4, 0];
            coefs[1, 0] = Coefs[0, 1];
            coefs[1, 1] = Coefs[1, 1];
            coefs[1, 2] = Coefs[2, 1];
            coefs[1, 3] = Coefs[3, 1];
            coefs[1, 4] = Coefs[4, 1];
            coefs[2, 0] = Coefs[0, 2];
            coefs[2, 1] = Coefs[1, 2];
            coefs[2, 2] = Coefs[2, 2];
            coefs[2, 3] = Coefs[3, 2];
            coefs[2, 4] = Coefs[4, 2];
            coefs[3, 0] = Coefs[0, 3];
            coefs[3, 1] = Coefs[1, 3];
            coefs[3, 2] = Coefs[2, 3];
            coefs[3, 3] = Coefs[3, 3];
            coefs[3, 4] = Coefs[4, 3];
            coefs[4, 0] = Coefs[0, 4];
            coefs[4, 1] = Coefs[1, 4];
            coefs[4, 2] = Coefs[2, 4];
            coefs[4, 3] = Coefs[3, 4];
            coefs[4, 4] = Coefs[4, 4];
        
            return new cga5dOutermorphism(coefs);
        }
        
        public double Determinant()
        {
            double det;
        
            //GMac Generated Processing Code, 14/04/2015 09:59:37 ص
            //Macro: main.cga5d.DetOM
            //Input Variables: 25 used, 0 not used, 25 total.
            //Temp Variables: 123 sub-expressions, 0 generated temps, 123 total.
            //Target Temp Variables: 15 total.
            //Output Variables: 1 total.
            //Computations: 1.34677419354839 average, 167 total.
            //Memory Reads: 2 average, 248 total.
            //Memory Writes: 124 total.
            
            double[] tempArray = new double[15];
            
            tempArray[0] = (Coefs[0, 1] * Coefs[1, 0]);
            tempArray[1] = (-1 * Coefs[0, 0] * Coefs[1, 1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * Coefs[2, 2] * tempArray[0]);
            tempArray[2] = (Coefs[0, 1] * Coefs[2, 0]);
            tempArray[3] = (-1 * Coefs[0, 0] * Coefs[2, 1]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (Coefs[1, 2] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[3] = (Coefs[1, 1] * Coefs[2, 0]);
            tempArray[4] = (-1 * Coefs[1, 0] * Coefs[2, 1]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * Coefs[0, 2] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[4]);
            tempArray[4] = (-1 * Coefs[3, 3] * tempArray[1]);
            tempArray[5] = (-1 * Coefs[3, 2] * tempArray[0]);
            tempArray[6] = (Coefs[0, 1] * Coefs[3, 0]);
            tempArray[7] = (-1 * Coefs[0, 0] * Coefs[3, 1]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (Coefs[1, 2] * tempArray[6]);
            tempArray[5] = (tempArray[5] + tempArray[7]);
            tempArray[7] = (Coefs[1, 1] * Coefs[3, 0]);
            tempArray[8] = (-1 * Coefs[1, 0] * Coefs[3, 1]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * Coefs[0, 2] * tempArray[7]);
            tempArray[5] = (tempArray[5] + tempArray[8]);
            tempArray[8] = (Coefs[2, 3] * tempArray[5]);
            tempArray[4] = (tempArray[4] + tempArray[8]);
            tempArray[8] = (-1 * Coefs[3, 2] * tempArray[2]);
            tempArray[9] = (Coefs[2, 2] * tempArray[6]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (Coefs[2, 1] * Coefs[3, 0]);
            tempArray[10] = (-1 * Coefs[2, 0] * Coefs[3, 1]);
            tempArray[9] = (tempArray[9] + tempArray[10]);
            tempArray[10] = (-1 * Coefs[0, 2] * tempArray[9]);
            tempArray[8] = (tempArray[8] + tempArray[10]);
            tempArray[10] = (-1 * Coefs[1, 3] * tempArray[8]);
            tempArray[4] = (tempArray[4] + tempArray[10]);
            tempArray[10] = (-1 * Coefs[3, 2] * tempArray[3]);
            tempArray[11] = (Coefs[2, 2] * tempArray[7]);
            tempArray[10] = (tempArray[10] + tempArray[11]);
            tempArray[11] = (-1 * Coefs[1, 2] * tempArray[9]);
            tempArray[10] = (tempArray[10] + tempArray[11]);
            tempArray[11] = (Coefs[0, 3] * tempArray[10]);
            tempArray[4] = (tempArray[4] + tempArray[11]);
            tempArray[4] = (-1 * Coefs[4, 4] * tempArray[4]);
            tempArray[1] = (-1 * Coefs[4, 3] * tempArray[1]);
            tempArray[0] = (-1 * Coefs[4, 2] * tempArray[0]);
            tempArray[11] = (Coefs[0, 1] * Coefs[4, 0]);
            tempArray[12] = (-1 * Coefs[0, 0] * Coefs[4, 1]);
            tempArray[11] = (tempArray[11] + tempArray[12]);
            tempArray[12] = (Coefs[1, 2] * tempArray[11]);
            tempArray[0] = (tempArray[0] + tempArray[12]);
            tempArray[12] = (Coefs[1, 1] * Coefs[4, 0]);
            tempArray[13] = (-1 * Coefs[1, 0] * Coefs[4, 1]);
            tempArray[12] = (tempArray[12] + tempArray[13]);
            tempArray[13] = (-1 * Coefs[0, 2] * tempArray[12]);
            tempArray[0] = (tempArray[0] + tempArray[13]);
            tempArray[13] = (Coefs[2, 3] * tempArray[0]);
            tempArray[1] = (tempArray[1] + tempArray[13]);
            tempArray[2] = (-1 * Coefs[4, 2] * tempArray[2]);
            tempArray[13] = (Coefs[2, 2] * tempArray[11]);
            tempArray[2] = (tempArray[2] + tempArray[13]);
            tempArray[13] = (Coefs[2, 1] * Coefs[4, 0]);
            tempArray[14] = (-1 * Coefs[2, 0] * Coefs[4, 1]);
            tempArray[13] = (tempArray[13] + tempArray[14]);
            tempArray[14] = (-1 * Coefs[0, 2] * tempArray[13]);
            tempArray[2] = (tempArray[2] + tempArray[14]);
            tempArray[14] = (-1 * Coefs[1, 3] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[14]);
            tempArray[3] = (-1 * Coefs[4, 2] * tempArray[3]);
            tempArray[14] = (Coefs[2, 2] * tempArray[12]);
            tempArray[3] = (tempArray[3] + tempArray[14]);
            tempArray[14] = (-1 * Coefs[1, 2] * tempArray[13]);
            tempArray[3] = (tempArray[3] + tempArray[14]);
            tempArray[14] = (Coefs[0, 3] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[14]);
            tempArray[1] = (Coefs[3, 4] * tempArray[1]);
            tempArray[1] = (tempArray[4] + tempArray[1]);
            tempArray[4] = (-1 * Coefs[4, 3] * tempArray[5]);
            tempArray[0] = (Coefs[3, 3] * tempArray[0]);
            tempArray[0] = (tempArray[4] + tempArray[0]);
            tempArray[4] = (-1 * Coefs[4, 2] * tempArray[6]);
            tempArray[5] = (Coefs[3, 2] * tempArray[11]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (Coefs[3, 1] * Coefs[4, 0]);
            tempArray[6] = (-1 * Coefs[3, 0] * Coefs[4, 1]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (-1 * Coefs[0, 2] * tempArray[5]);
            tempArray[4] = (tempArray[4] + tempArray[6]);
            tempArray[6] = (-1 * Coefs[1, 3] * tempArray[4]);
            tempArray[0] = (tempArray[0] + tempArray[6]);
            tempArray[6] = (-1 * Coefs[4, 2] * tempArray[7]);
            tempArray[7] = (Coefs[3, 2] * tempArray[12]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * Coefs[1, 2] * tempArray[5]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (Coefs[0, 3] * tempArray[6]);
            tempArray[0] = (tempArray[0] + tempArray[7]);
            tempArray[0] = (-1 * Coefs[2, 4] * tempArray[0]);
            tempArray[0] = (tempArray[1] + tempArray[0]);
            tempArray[1] = (-1 * Coefs[4, 3] * tempArray[8]);
            tempArray[2] = (Coefs[3, 3] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (-1 * Coefs[2, 3] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (-1 * Coefs[4, 2] * tempArray[9]);
            tempArray[4] = (Coefs[3, 2] * tempArray[13]);
            tempArray[2] = (tempArray[2] + tempArray[4]);
            tempArray[4] = (-1 * Coefs[2, 2] * tempArray[5]);
            tempArray[2] = (tempArray[2] + tempArray[4]);
            tempArray[4] = (Coefs[0, 3] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[4]);
            tempArray[1] = (Coefs[1, 4] * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * Coefs[4, 3] * tempArray[10]);
            tempArray[3] = (Coefs[3, 3] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[3] = (-1 * Coefs[2, 3] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[2] = (Coefs[1, 3] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[1] = (-1 * Coefs[0, 4] * tempArray[1]);
            det = (tempArray[0] + tempArray[1]);
            
        
            return det;
        }
        
        public cga5dBlade Apply(cga5dBlade blade)
        {
            if (blade.IsZero)
                return cga5dBlade.ZeroBlade;
        
            switch (blade.Grade)
            {
                case 0:
                    return blade;
                case 1:
                    return new cga5dBlade(1, Apply_1(Coefs, blade.Coefs));
                case 2:
                    return new cga5dBlade(2, Apply_2(Coefs, blade.Coefs));
                case 3:
                    return new cga5dBlade(3, Apply_3(Coefs, blade.Coefs));
                case 4:
                    return new cga5dBlade(4, Apply_4(Coefs, blade.Coefs));
                case 5:
                    return new cga5dBlade(5, Apply_5(Coefs, blade.Coefs));
            }
        
            throw new InvalidDataException("Internal error. Blade grade not acceptable!");
        }
        
        
        public static cga5dOutermorphism operator +(cga5dOutermorphism om1, cga5dOutermorphism om2)
        {
            var coefs = new double[cga5dBlade.MaxGrade, cga5dBlade.MaxGrade];
        
            coefs[0, 0] = om1.Coefs[0, 0] + om2.Coefs[0, 0];
            coefs[0, 1] = om1.Coefs[0, 1] + om2.Coefs[0, 1];
            coefs[0, 2] = om1.Coefs[0, 2] + om2.Coefs[0, 2];
            coefs[0, 3] = om1.Coefs[0, 3] + om2.Coefs[0, 3];
            coefs[0, 4] = om1.Coefs[0, 4] + om2.Coefs[0, 4];
            coefs[1, 0] = om1.Coefs[1, 0] + om2.Coefs[1, 0];
            coefs[1, 1] = om1.Coefs[1, 1] + om2.Coefs[1, 1];
            coefs[1, 2] = om1.Coefs[1, 2] + om2.Coefs[1, 2];
            coefs[1, 3] = om1.Coefs[1, 3] + om2.Coefs[1, 3];
            coefs[1, 4] = om1.Coefs[1, 4] + om2.Coefs[1, 4];
            coefs[2, 0] = om1.Coefs[2, 0] + om2.Coefs[2, 0];
            coefs[2, 1] = om1.Coefs[2, 1] + om2.Coefs[2, 1];
            coefs[2, 2] = om1.Coefs[2, 2] + om2.Coefs[2, 2];
            coefs[2, 3] = om1.Coefs[2, 3] + om2.Coefs[2, 3];
            coefs[2, 4] = om1.Coefs[2, 4] + om2.Coefs[2, 4];
            coefs[3, 0] = om1.Coefs[3, 0] + om2.Coefs[3, 0];
            coefs[3, 1] = om1.Coefs[3, 1] + om2.Coefs[3, 1];
            coefs[3, 2] = om1.Coefs[3, 2] + om2.Coefs[3, 2];
            coefs[3, 3] = om1.Coefs[3, 3] + om2.Coefs[3, 3];
            coefs[3, 4] = om1.Coefs[3, 4] + om2.Coefs[3, 4];
            coefs[4, 0] = om1.Coefs[4, 0] + om2.Coefs[4, 0];
            coefs[4, 1] = om1.Coefs[4, 1] + om2.Coefs[4, 1];
            coefs[4, 2] = om1.Coefs[4, 2] + om2.Coefs[4, 2];
            coefs[4, 3] = om1.Coefs[4, 3] + om2.Coefs[4, 3];
            coefs[4, 4] = om1.Coefs[4, 4] + om2.Coefs[4, 4];
        
            return new cga5dOutermorphism(coefs);
        }
        
        public static cga5dOutermorphism operator -(cga5dOutermorphism om1, cga5dOutermorphism om2)
        {
            var coefs = new double[cga5dBlade.MaxGrade, cga5dBlade.MaxGrade];
        
            coefs[0, 0] = om1.Coefs[0, 0] - om2.Coefs[0, 0];
            coefs[0, 1] = om1.Coefs[0, 1] - om2.Coefs[0, 1];
            coefs[0, 2] = om1.Coefs[0, 2] - om2.Coefs[0, 2];
            coefs[0, 3] = om1.Coefs[0, 3] - om2.Coefs[0, 3];
            coefs[0, 4] = om1.Coefs[0, 4] - om2.Coefs[0, 4];
            coefs[1, 0] = om1.Coefs[1, 0] - om2.Coefs[1, 0];
            coefs[1, 1] = om1.Coefs[1, 1] - om2.Coefs[1, 1];
            coefs[1, 2] = om1.Coefs[1, 2] - om2.Coefs[1, 2];
            coefs[1, 3] = om1.Coefs[1, 3] - om2.Coefs[1, 3];
            coefs[1, 4] = om1.Coefs[1, 4] - om2.Coefs[1, 4];
            coefs[2, 0] = om1.Coefs[2, 0] - om2.Coefs[2, 0];
            coefs[2, 1] = om1.Coefs[2, 1] - om2.Coefs[2, 1];
            coefs[2, 2] = om1.Coefs[2, 2] - om2.Coefs[2, 2];
            coefs[2, 3] = om1.Coefs[2, 3] - om2.Coefs[2, 3];
            coefs[2, 4] = om1.Coefs[2, 4] - om2.Coefs[2, 4];
            coefs[3, 0] = om1.Coefs[3, 0] - om2.Coefs[3, 0];
            coefs[3, 1] = om1.Coefs[3, 1] - om2.Coefs[3, 1];
            coefs[3, 2] = om1.Coefs[3, 2] - om2.Coefs[3, 2];
            coefs[3, 3] = om1.Coefs[3, 3] - om2.Coefs[3, 3];
            coefs[3, 4] = om1.Coefs[3, 4] - om2.Coefs[3, 4];
            coefs[4, 0] = om1.Coefs[4, 0] - om2.Coefs[4, 0];
            coefs[4, 1] = om1.Coefs[4, 1] - om2.Coefs[4, 1];
            coefs[4, 2] = om1.Coefs[4, 2] - om2.Coefs[4, 2];
            coefs[4, 3] = om1.Coefs[4, 3] - om2.Coefs[4, 3];
            coefs[4, 4] = om1.Coefs[4, 4] - om2.Coefs[4, 4];
        
            return new cga5dOutermorphism(coefs);
        }
        
        public static cga5dOutermorphism operator *(cga5dOutermorphism om1, cga5dOutermorphism om2)
        {
            var coefs = new double[cga5dBlade.MaxGrade, cga5dBlade.MaxGrade];
        
            coefs[0, 0] = om1.Coefs[0, 0] * om2.Coefs[0, 0] + om1.Coefs[0, 1] * om2.Coefs[1, 0] + om1.Coefs[0, 2] * om2.Coefs[2, 0] + om1.Coefs[0, 3] * om2.Coefs[3, 0] + om1.Coefs[0, 4] * om2.Coefs[4, 0];
            coefs[0, 1] = om1.Coefs[0, 0] * om2.Coefs[0, 1] + om1.Coefs[0, 1] * om2.Coefs[1, 1] + om1.Coefs[0, 2] * om2.Coefs[2, 1] + om1.Coefs[0, 3] * om2.Coefs[3, 1] + om1.Coefs[0, 4] * om2.Coefs[4, 1];
            coefs[0, 2] = om1.Coefs[0, 0] * om2.Coefs[0, 2] + om1.Coefs[0, 1] * om2.Coefs[1, 2] + om1.Coefs[0, 2] * om2.Coefs[2, 2] + om1.Coefs[0, 3] * om2.Coefs[3, 2] + om1.Coefs[0, 4] * om2.Coefs[4, 2];
            coefs[0, 3] = om1.Coefs[0, 0] * om2.Coefs[0, 3] + om1.Coefs[0, 1] * om2.Coefs[1, 3] + om1.Coefs[0, 2] * om2.Coefs[2, 3] + om1.Coefs[0, 3] * om2.Coefs[3, 3] + om1.Coefs[0, 4] * om2.Coefs[4, 3];
            coefs[0, 4] = om1.Coefs[0, 0] * om2.Coefs[0, 4] + om1.Coefs[0, 1] * om2.Coefs[1, 4] + om1.Coefs[0, 2] * om2.Coefs[2, 4] + om1.Coefs[0, 3] * om2.Coefs[3, 4] + om1.Coefs[0, 4] * om2.Coefs[4, 4];
            coefs[1, 0] = om1.Coefs[1, 0] * om2.Coefs[0, 0] + om1.Coefs[1, 1] * om2.Coefs[1, 0] + om1.Coefs[1, 2] * om2.Coefs[2, 0] + om1.Coefs[1, 3] * om2.Coefs[3, 0] + om1.Coefs[1, 4] * om2.Coefs[4, 0];
            coefs[1, 1] = om1.Coefs[1, 0] * om2.Coefs[0, 1] + om1.Coefs[1, 1] * om2.Coefs[1, 1] + om1.Coefs[1, 2] * om2.Coefs[2, 1] + om1.Coefs[1, 3] * om2.Coefs[3, 1] + om1.Coefs[1, 4] * om2.Coefs[4, 1];
            coefs[1, 2] = om1.Coefs[1, 0] * om2.Coefs[0, 2] + om1.Coefs[1, 1] * om2.Coefs[1, 2] + om1.Coefs[1, 2] * om2.Coefs[2, 2] + om1.Coefs[1, 3] * om2.Coefs[3, 2] + om1.Coefs[1, 4] * om2.Coefs[4, 2];
            coefs[1, 3] = om1.Coefs[1, 0] * om2.Coefs[0, 3] + om1.Coefs[1, 1] * om2.Coefs[1, 3] + om1.Coefs[1, 2] * om2.Coefs[2, 3] + om1.Coefs[1, 3] * om2.Coefs[3, 3] + om1.Coefs[1, 4] * om2.Coefs[4, 3];
            coefs[1, 4] = om1.Coefs[1, 0] * om2.Coefs[0, 4] + om1.Coefs[1, 1] * om2.Coefs[1, 4] + om1.Coefs[1, 2] * om2.Coefs[2, 4] + om1.Coefs[1, 3] * om2.Coefs[3, 4] + om1.Coefs[1, 4] * om2.Coefs[4, 4];
            coefs[2, 0] = om1.Coefs[2, 0] * om2.Coefs[0, 0] + om1.Coefs[2, 1] * om2.Coefs[1, 0] + om1.Coefs[2, 2] * om2.Coefs[2, 0] + om1.Coefs[2, 3] * om2.Coefs[3, 0] + om1.Coefs[2, 4] * om2.Coefs[4, 0];
            coefs[2, 1] = om1.Coefs[2, 0] * om2.Coefs[0, 1] + om1.Coefs[2, 1] * om2.Coefs[1, 1] + om1.Coefs[2, 2] * om2.Coefs[2, 1] + om1.Coefs[2, 3] * om2.Coefs[3, 1] + om1.Coefs[2, 4] * om2.Coefs[4, 1];
            coefs[2, 2] = om1.Coefs[2, 0] * om2.Coefs[0, 2] + om1.Coefs[2, 1] * om2.Coefs[1, 2] + om1.Coefs[2, 2] * om2.Coefs[2, 2] + om1.Coefs[2, 3] * om2.Coefs[3, 2] + om1.Coefs[2, 4] * om2.Coefs[4, 2];
            coefs[2, 3] = om1.Coefs[2, 0] * om2.Coefs[0, 3] + om1.Coefs[2, 1] * om2.Coefs[1, 3] + om1.Coefs[2, 2] * om2.Coefs[2, 3] + om1.Coefs[2, 3] * om2.Coefs[3, 3] + om1.Coefs[2, 4] * om2.Coefs[4, 3];
            coefs[2, 4] = om1.Coefs[2, 0] * om2.Coefs[0, 4] + om1.Coefs[2, 1] * om2.Coefs[1, 4] + om1.Coefs[2, 2] * om2.Coefs[2, 4] + om1.Coefs[2, 3] * om2.Coefs[3, 4] + om1.Coefs[2, 4] * om2.Coefs[4, 4];
            coefs[3, 0] = om1.Coefs[3, 0] * om2.Coefs[0, 0] + om1.Coefs[3, 1] * om2.Coefs[1, 0] + om1.Coefs[3, 2] * om2.Coefs[2, 0] + om1.Coefs[3, 3] * om2.Coefs[3, 0] + om1.Coefs[3, 4] * om2.Coefs[4, 0];
            coefs[3, 1] = om1.Coefs[3, 0] * om2.Coefs[0, 1] + om1.Coefs[3, 1] * om2.Coefs[1, 1] + om1.Coefs[3, 2] * om2.Coefs[2, 1] + om1.Coefs[3, 3] * om2.Coefs[3, 1] + om1.Coefs[3, 4] * om2.Coefs[4, 1];
            coefs[3, 2] = om1.Coefs[3, 0] * om2.Coefs[0, 2] + om1.Coefs[3, 1] * om2.Coefs[1, 2] + om1.Coefs[3, 2] * om2.Coefs[2, 2] + om1.Coefs[3, 3] * om2.Coefs[3, 2] + om1.Coefs[3, 4] * om2.Coefs[4, 2];
            coefs[3, 3] = om1.Coefs[3, 0] * om2.Coefs[0, 3] + om1.Coefs[3, 1] * om2.Coefs[1, 3] + om1.Coefs[3, 2] * om2.Coefs[2, 3] + om1.Coefs[3, 3] * om2.Coefs[3, 3] + om1.Coefs[3, 4] * om2.Coefs[4, 3];
            coefs[3, 4] = om1.Coefs[3, 0] * om2.Coefs[0, 4] + om1.Coefs[3, 1] * om2.Coefs[1, 4] + om1.Coefs[3, 2] * om2.Coefs[2, 4] + om1.Coefs[3, 3] * om2.Coefs[3, 4] + om1.Coefs[3, 4] * om2.Coefs[4, 4];
            coefs[4, 0] = om1.Coefs[4, 0] * om2.Coefs[0, 0] + om1.Coefs[4, 1] * om2.Coefs[1, 0] + om1.Coefs[4, 2] * om2.Coefs[2, 0] + om1.Coefs[4, 3] * om2.Coefs[3, 0] + om1.Coefs[4, 4] * om2.Coefs[4, 0];
            coefs[4, 1] = om1.Coefs[4, 0] * om2.Coefs[0, 1] + om1.Coefs[4, 1] * om2.Coefs[1, 1] + om1.Coefs[4, 2] * om2.Coefs[2, 1] + om1.Coefs[4, 3] * om2.Coefs[3, 1] + om1.Coefs[4, 4] * om2.Coefs[4, 1];
            coefs[4, 2] = om1.Coefs[4, 0] * om2.Coefs[0, 2] + om1.Coefs[4, 1] * om2.Coefs[1, 2] + om1.Coefs[4, 2] * om2.Coefs[2, 2] + om1.Coefs[4, 3] * om2.Coefs[3, 2] + om1.Coefs[4, 4] * om2.Coefs[4, 2];
            coefs[4, 3] = om1.Coefs[4, 0] * om2.Coefs[0, 3] + om1.Coefs[4, 1] * om2.Coefs[1, 3] + om1.Coefs[4, 2] * om2.Coefs[2, 3] + om1.Coefs[4, 3] * om2.Coefs[3, 3] + om1.Coefs[4, 4] * om2.Coefs[4, 3];
            coefs[4, 4] = om1.Coefs[4, 0] * om2.Coefs[0, 4] + om1.Coefs[4, 1] * om2.Coefs[1, 4] + om1.Coefs[4, 2] * om2.Coefs[2, 4] + om1.Coefs[4, 3] * om2.Coefs[3, 4] + om1.Coefs[4, 4] * om2.Coefs[4, 4];
        
            return new cga5dOutermorphism(coefs);
        }
        
        public static cga5dOutermorphism operator *(double scalar, cga5dOutermorphism om)
        {
            var coefs = new double[cga5dBlade.MaxGrade, cga5dBlade.MaxGrade];
        
            coefs[0, 0] = om.Coefs[0, 0] * scalar;
            coefs[0, 1] = om.Coefs[0, 1] * scalar;
            coefs[0, 2] = om.Coefs[0, 2] * scalar;
            coefs[0, 3] = om.Coefs[0, 3] * scalar;
            coefs[0, 4] = om.Coefs[0, 4] * scalar;
            coefs[1, 0] = om.Coefs[1, 0] * scalar;
            coefs[1, 1] = om.Coefs[1, 1] * scalar;
            coefs[1, 2] = om.Coefs[1, 2] * scalar;
            coefs[1, 3] = om.Coefs[1, 3] * scalar;
            coefs[1, 4] = om.Coefs[1, 4] * scalar;
            coefs[2, 0] = om.Coefs[2, 0] * scalar;
            coefs[2, 1] = om.Coefs[2, 1] * scalar;
            coefs[2, 2] = om.Coefs[2, 2] * scalar;
            coefs[2, 3] = om.Coefs[2, 3] * scalar;
            coefs[2, 4] = om.Coefs[2, 4] * scalar;
            coefs[3, 0] = om.Coefs[3, 0] * scalar;
            coefs[3, 1] = om.Coefs[3, 1] * scalar;
            coefs[3, 2] = om.Coefs[3, 2] * scalar;
            coefs[3, 3] = om.Coefs[3, 3] * scalar;
            coefs[3, 4] = om.Coefs[3, 4] * scalar;
            coefs[4, 0] = om.Coefs[4, 0] * scalar;
            coefs[4, 1] = om.Coefs[4, 1] * scalar;
            coefs[4, 2] = om.Coefs[4, 2] * scalar;
            coefs[4, 3] = om.Coefs[4, 3] * scalar;
            coefs[4, 4] = om.Coefs[4, 4] * scalar;
        
            return new cga5dOutermorphism(coefs);
        }
        
        public static cga5dOutermorphism operator *(cga5dOutermorphism om, double scalar)
        {
            var coefs = new double[cga5dBlade.MaxGrade, cga5dBlade.MaxGrade];
        
            coefs[0, 0] = om.Coefs[0, 0] * scalar;
            coefs[0, 1] = om.Coefs[0, 1] * scalar;
            coefs[0, 2] = om.Coefs[0, 2] * scalar;
            coefs[0, 3] = om.Coefs[0, 3] * scalar;
            coefs[0, 4] = om.Coefs[0, 4] * scalar;
            coefs[1, 0] = om.Coefs[1, 0] * scalar;
            coefs[1, 1] = om.Coefs[1, 1] * scalar;
            coefs[1, 2] = om.Coefs[1, 2] * scalar;
            coefs[1, 3] = om.Coefs[1, 3] * scalar;
            coefs[1, 4] = om.Coefs[1, 4] * scalar;
            coefs[2, 0] = om.Coefs[2, 0] * scalar;
            coefs[2, 1] = om.Coefs[2, 1] * scalar;
            coefs[2, 2] = om.Coefs[2, 2] * scalar;
            coefs[2, 3] = om.Coefs[2, 3] * scalar;
            coefs[2, 4] = om.Coefs[2, 4] * scalar;
            coefs[3, 0] = om.Coefs[3, 0] * scalar;
            coefs[3, 1] = om.Coefs[3, 1] * scalar;
            coefs[3, 2] = om.Coefs[3, 2] * scalar;
            coefs[3, 3] = om.Coefs[3, 3] * scalar;
            coefs[3, 4] = om.Coefs[3, 4] * scalar;
            coefs[4, 0] = om.Coefs[4, 0] * scalar;
            coefs[4, 1] = om.Coefs[4, 1] * scalar;
            coefs[4, 2] = om.Coefs[4, 2] * scalar;
            coefs[4, 3] = om.Coefs[4, 3] * scalar;
            coefs[4, 4] = om.Coefs[4, 4] * scalar;
        
            return new cga5dOutermorphism(coefs);
        }
        
        public static cga5dOutermorphism operator /(cga5dOutermorphism om, double scalar)
        {
            var coefs = new double[cga5dBlade.MaxGrade, cga5dBlade.MaxGrade];
        
            coefs[0, 0] = om.Coefs[0, 0] / scalar;
            coefs[0, 1] = om.Coefs[0, 1] / scalar;
            coefs[0, 2] = om.Coefs[0, 2] / scalar;
            coefs[0, 3] = om.Coefs[0, 3] / scalar;
            coefs[0, 4] = om.Coefs[0, 4] / scalar;
            coefs[1, 0] = om.Coefs[1, 0] / scalar;
            coefs[1, 1] = om.Coefs[1, 1] / scalar;
            coefs[1, 2] = om.Coefs[1, 2] / scalar;
            coefs[1, 3] = om.Coefs[1, 3] / scalar;
            coefs[1, 4] = om.Coefs[1, 4] / scalar;
            coefs[2, 0] = om.Coefs[2, 0] / scalar;
            coefs[2, 1] = om.Coefs[2, 1] / scalar;
            coefs[2, 2] = om.Coefs[2, 2] / scalar;
            coefs[2, 3] = om.Coefs[2, 3] / scalar;
            coefs[2, 4] = om.Coefs[2, 4] / scalar;
            coefs[3, 0] = om.Coefs[3, 0] / scalar;
            coefs[3, 1] = om.Coefs[3, 1] / scalar;
            coefs[3, 2] = om.Coefs[3, 2] / scalar;
            coefs[3, 3] = om.Coefs[3, 3] / scalar;
            coefs[3, 4] = om.Coefs[3, 4] / scalar;
            coefs[4, 0] = om.Coefs[4, 0] / scalar;
            coefs[4, 1] = om.Coefs[4, 1] / scalar;
            coefs[4, 2] = om.Coefs[4, 2] / scalar;
            coefs[4, 3] = om.Coefs[4, 3] / scalar;
            coefs[4, 4] = om.Coefs[4, 4] / scalar;
        
            return new cga5dOutermorphism(coefs);
        }
        
        public static cga5dOutermorphism operator -(cga5dOutermorphism om)
        {
            var coefs = new double[cga5dBlade.MaxGrade, cga5dBlade.MaxGrade];
        
            coefs[0, 0] = -om.Coefs[0, 0];
            coefs[0, 1] = -om.Coefs[0, 1];
            coefs[0, 2] = -om.Coefs[0, 2];
            coefs[0, 3] = -om.Coefs[0, 3];
            coefs[0, 4] = -om.Coefs[0, 4];
            coefs[1, 0] = -om.Coefs[1, 0];
            coefs[1, 1] = -om.Coefs[1, 1];
            coefs[1, 2] = -om.Coefs[1, 2];
            coefs[1, 3] = -om.Coefs[1, 3];
            coefs[1, 4] = -om.Coefs[1, 4];
            coefs[2, 0] = -om.Coefs[2, 0];
            coefs[2, 1] = -om.Coefs[2, 1];
            coefs[2, 2] = -om.Coefs[2, 2];
            coefs[2, 3] = -om.Coefs[2, 3];
            coefs[2, 4] = -om.Coefs[2, 4];
            coefs[3, 0] = -om.Coefs[3, 0];
            coefs[3, 1] = -om.Coefs[3, 1];
            coefs[3, 2] = -om.Coefs[3, 2];
            coefs[3, 3] = -om.Coefs[3, 3];
            coefs[3, 4] = -om.Coefs[3, 4];
            coefs[4, 0] = -om.Coefs[4, 0];
            coefs[4, 1] = -om.Coefs[4, 1];
            coefs[4, 2] = -om.Coefs[4, 2];
            coefs[4, 3] = -om.Coefs[4, 3];
            coefs[4, 4] = -om.Coefs[4, 4];
        
            return new cga5dOutermorphism(coefs);
        }
    }
}
