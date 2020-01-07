using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace main
{
   public static class e2d
   {
      public sealed class Multivector
      {
         public readonly double[] Coef = new double[4];
         
         
         public Multivector()
         {
         }
         
         public Multivector(params double[] coefs)
         {
             int i = 0;
             foreach (var coef in coefs.Take(4))
                 Coef[i++] = coef;
         }
         
         public Multivector(IEnumerable<double> coefs)
         {
             int i = 0;
             foreach (var coef in coefs.Take(4))
                 Coef[i++] = coef;
         }
         
         
      }
      
      public static readonly main.e2d.Multivector I = new main.e2d.Multivector(0, 0, 0, 1);
      
      public static main.e2d.Multivector UnaryMinus(main.e2d.Multivector mv1)
      {
         var result = new main.e2d.Multivector();
         
         
         result.Coef[0] = (-1 * mv1.Coef[0]);
         result.Coef[1] = (-1 * mv1.Coef[1]);
         result.Coef[2] = (-1 * mv1.Coef[2]);
         result.Coef[3] = (-1 * mv1.Coef[3]);
         
         
         return result;
      }
      
      public static main.e2d.Multivector GradeInversion(main.e2d.Multivector mv1)
      {
         var result = new main.e2d.Multivector();
         
         
         result.Coef[0] = mv1.Coef[0];
         result.Coef[1] = (-1 * mv1.Coef[1]);
         result.Coef[2] = (-1 * mv1.Coef[2]);
         result.Coef[3] = mv1.Coef[3];
         
         
         return result;
      }
      
      public static main.e2d.Multivector Reverse(main.e2d.Multivector mv1)
      {
         var result = new main.e2d.Multivector();
         
         
         result.Coef[0] = mv1.Coef[0];
         result.Coef[1] = mv1.Coef[1];
         result.Coef[2] = mv1.Coef[2];
         result.Coef[3] = (-1 * mv1.Coef[3]);
         
         
         return result;
      }
      
      public static main.e2d.Multivector CliffordConjugate(main.e2d.Multivector mv1)
      {
         var result = new main.e2d.Multivector();
         
         
         result.Coef[0] = mv1.Coef[0];
         result.Coef[1] = (-1 * mv1.Coef[1]);
         result.Coef[2] = (-1 * mv1.Coef[2]);
         result.Coef[3] = (-1 * mv1.Coef[3]);
         
         
         return result;
      }
      
      public static main.e2d.Multivector ACP(main.e2d.Multivector mv1, main.e2d.Multivector mv2)
      {
         var result = new main.e2d.Multivector();
         
         double tempVar0000;
         double tempVar0001;
         double tempVar0002;
         double tempVar0003;
         
         tempVar0000 = (mv1.Coef[0] * mv2.Coef[0]);
         tempVar0001 = (mv1.Coef[1] * mv2.Coef[1]);
         tempVar0002 = (mv1.Coef[2] * mv2.Coef[2]);
         tempVar0003 = (-1 * mv1.Coef[3] * mv2.Coef[3]);
         result.Coef[0] = (tempVar0000 + tempVar0001 + tempVar0002 + tempVar0003);
         tempVar0000 = (mv1.Coef[1] * mv2.Coef[0]);
         tempVar0001 = (mv1.Coef[0] * mv2.Coef[1]);
         result.Coef[1] = (tempVar0000 + tempVar0001);
         tempVar0000 = (mv1.Coef[2] * mv2.Coef[0]);
         tempVar0001 = (mv1.Coef[0] * mv2.Coef[2]);
         result.Coef[2] = (tempVar0000 + tempVar0001);
         tempVar0000 = (mv1.Coef[3] * mv2.Coef[0]);
         tempVar0001 = (mv1.Coef[0] * mv2.Coef[3]);
         result.Coef[3] = (tempVar0000 + tempVar0001);
         
         
         return result;
      }
      
      public static main.e2d.Multivector CP(main.e2d.Multivector mv1, main.e2d.Multivector mv2)
      {
         var result = new main.e2d.Multivector();
         
         double tempVar0000;
         double tempVar0001;
         
         result.Coef[0] = 0;
         tempVar0000 = (mv1.Coef[3] * mv2.Coef[2]);
         tempVar0001 = (-1 * mv1.Coef[2] * mv2.Coef[3]);
         result.Coef[1] = (tempVar0000 + tempVar0001);
         tempVar0000 = (-1 * mv1.Coef[3] * mv2.Coef[1]);
         tempVar0001 = (mv1.Coef[1] * mv2.Coef[3]);
         result.Coef[2] = (tempVar0000 + tempVar0001);
         tempVar0000 = (-1 * mv1.Coef[2] * mv2.Coef[1]);
         tempVar0001 = (mv1.Coef[1] * mv2.Coef[2]);
         result.Coef[3] = (tempVar0000 + tempVar0001);
         
         
         return result;
      }
      
      public static main.e2d.Multivector FDP(main.e2d.Multivector mv1, main.e2d.Multivector mv2)
      {
         var result = new main.e2d.Multivector();
         
         double tempVar0000;
         double tempVar0001;
         double tempVar0002;
         double tempVar0003;
         
         tempVar0000 = (mv1.Coef[0] * mv2.Coef[0]);
         tempVar0001 = (mv1.Coef[1] * mv2.Coef[1]);
         tempVar0002 = (mv1.Coef[2] * mv2.Coef[2]);
         tempVar0003 = (-1 * mv1.Coef[3] * mv2.Coef[3]);
         result.Coef[0] = (tempVar0000 + tempVar0001 + tempVar0002 + tempVar0003);
         tempVar0000 = (mv1.Coef[1] * mv2.Coef[0]);
         tempVar0001 = (mv1.Coef[0] * mv2.Coef[1]);
         tempVar0002 = (mv1.Coef[3] * mv2.Coef[2]);
         tempVar0003 = (-1 * mv1.Coef[2] * mv2.Coef[3]);
         result.Coef[1] = (tempVar0000 + tempVar0001 + tempVar0002 + tempVar0003);
         tempVar0000 = (mv1.Coef[2] * mv2.Coef[0]);
         tempVar0001 = (-1 * mv1.Coef[3] * mv2.Coef[1]);
         tempVar0002 = (mv1.Coef[0] * mv2.Coef[2]);
         tempVar0003 = (mv1.Coef[1] * mv2.Coef[3]);
         result.Coef[2] = (tempVar0000 + tempVar0001 + tempVar0002 + tempVar0003);
         tempVar0000 = (mv1.Coef[3] * mv2.Coef[0]);
         tempVar0001 = (mv1.Coef[0] * mv2.Coef[3]);
         result.Coef[3] = (tempVar0000 + tempVar0001);
         
         
         return result;
      }
      
      public static main.e2d.Multivector GP(main.e2d.Multivector mv1, main.e2d.Multivector mv2)
      {
         var result = new main.e2d.Multivector();
         
         double tempVar0000;
         double tempVar0001;
         double tempVar0002;
         double tempVar0003;
         
         tempVar0000 = (mv1.Coef[0] * mv2.Coef[0]);
         tempVar0001 = (mv1.Coef[1] * mv2.Coef[1]);
         tempVar0002 = (mv1.Coef[2] * mv2.Coef[2]);
         tempVar0003 = (-1 * mv1.Coef[3] * mv2.Coef[3]);
         result.Coef[0] = (tempVar0000 + tempVar0001 + tempVar0002 + tempVar0003);
         tempVar0000 = (mv1.Coef[1] * mv2.Coef[0]);
         tempVar0001 = (mv1.Coef[0] * mv2.Coef[1]);
         tempVar0002 = (mv1.Coef[3] * mv2.Coef[2]);
         tempVar0003 = (-1 * mv1.Coef[2] * mv2.Coef[3]);
         result.Coef[1] = (tempVar0000 + tempVar0001 + tempVar0002 + tempVar0003);
         tempVar0000 = (mv1.Coef[2] * mv2.Coef[0]);
         tempVar0001 = (-1 * mv1.Coef[3] * mv2.Coef[1]);
         tempVar0002 = (mv1.Coef[0] * mv2.Coef[2]);
         tempVar0003 = (mv1.Coef[1] * mv2.Coef[3]);
         result.Coef[2] = (tempVar0000 + tempVar0001 + tempVar0002 + tempVar0003);
         tempVar0000 = (mv1.Coef[3] * mv2.Coef[0]);
         tempVar0001 = (-1 * mv1.Coef[2] * mv2.Coef[1]);
         tempVar0002 = (mv1.Coef[1] * mv2.Coef[2]);
         tempVar0003 = (mv1.Coef[0] * mv2.Coef[3]);
         result.Coef[3] = (tempVar0000 + tempVar0001 + tempVar0002 + tempVar0003);
         
         
         return result;
      }
      
      public static main.e2d.Multivector HIP(main.e2d.Multivector mv1, main.e2d.Multivector mv2)
      {
         var result = new main.e2d.Multivector();
         
         double tempVar0000;
         double tempVar0001;
         double tempVar0002;
         
         result.Coef[3] = 0;
         tempVar0000 = (mv1.Coef[1] * mv2.Coef[1]);
         tempVar0001 = (mv1.Coef[2] * mv2.Coef[2]);
         tempVar0002 = (-1 * mv1.Coef[3] * mv2.Coef[3]);
         result.Coef[0] = (tempVar0000 + tempVar0001 + tempVar0002);
         tempVar0000 = (mv1.Coef[3] * mv2.Coef[2]);
         tempVar0001 = (-1 * mv1.Coef[2] * mv2.Coef[3]);
         result.Coef[1] = (tempVar0000 + tempVar0001);
         tempVar0000 = (-1 * mv1.Coef[3] * mv2.Coef[1]);
         tempVar0001 = (mv1.Coef[1] * mv2.Coef[3]);
         result.Coef[2] = (tempVar0000 + tempVar0001);
         
         
         return result;
      }
      
      public static main.e2d.Multivector LCP(main.e2d.Multivector mv1, main.e2d.Multivector mv2)
      {
         var result = new main.e2d.Multivector();
         
         double tempVar0000;
         double tempVar0001;
         double tempVar0002;
         double tempVar0003;
         
         result.Coef[3] = (mv1.Coef[0] * mv2.Coef[3]);
         tempVar0000 = (mv1.Coef[0] * mv2.Coef[0]);
         tempVar0001 = (mv1.Coef[1] * mv2.Coef[1]);
         tempVar0002 = (mv1.Coef[2] * mv2.Coef[2]);
         tempVar0003 = (-1 * mv1.Coef[3] * mv2.Coef[3]);
         result.Coef[0] = (tempVar0000 + tempVar0001 + tempVar0002 + tempVar0003);
         tempVar0000 = (mv1.Coef[0] * mv2.Coef[1]);
         tempVar0001 = (-1 * mv1.Coef[2] * mv2.Coef[3]);
         result.Coef[1] = (tempVar0000 + tempVar0001);
         tempVar0000 = (mv1.Coef[0] * mv2.Coef[2]);
         tempVar0001 = (mv1.Coef[1] * mv2.Coef[3]);
         result.Coef[2] = (tempVar0000 + tempVar0001);
         
         
         return result;
      }
      
      public static main.e2d.Multivector OP(main.e2d.Multivector mv1, main.e2d.Multivector mv2)
      {
         var result = new main.e2d.Multivector();
         
         double tempVar0000;
         double tempVar0001;
         double tempVar0002;
         double tempVar0003;
         
         result.Coef[0] = (mv1.Coef[0] * mv2.Coef[0]);
         tempVar0000 = (mv1.Coef[1] * mv2.Coef[0]);
         tempVar0001 = (mv1.Coef[0] * mv2.Coef[1]);
         result.Coef[1] = (tempVar0000 + tempVar0001);
         tempVar0000 = (mv1.Coef[2] * mv2.Coef[0]);
         tempVar0001 = (mv1.Coef[0] * mv2.Coef[2]);
         result.Coef[2] = (tempVar0000 + tempVar0001);
         tempVar0000 = (mv1.Coef[3] * mv2.Coef[0]);
         tempVar0001 = (-1 * mv1.Coef[2] * mv2.Coef[1]);
         tempVar0002 = (mv1.Coef[1] * mv2.Coef[2]);
         tempVar0003 = (mv1.Coef[0] * mv2.Coef[3]);
         result.Coef[3] = (tempVar0000 + tempVar0001 + tempVar0002 + tempVar0003);
         
         
         return result;
      }
      
      public static main.e2d.Multivector Plus(main.e2d.Multivector mv1, main.e2d.Multivector mv2)
      {
         var result = new main.e2d.Multivector();
         
         
         result.Coef[0] = (mv1.Coef[0] + mv2.Coef[0]);
         result.Coef[1] = (mv1.Coef[1] + mv2.Coef[1]);
         result.Coef[2] = (mv1.Coef[2] + mv2.Coef[2]);
         result.Coef[3] = (mv1.Coef[3] + mv2.Coef[3]);
         
         
         return result;
      }
      
      public static main.e2d.Multivector RCP(main.e2d.Multivector mv1, main.e2d.Multivector mv2)
      {
         var result = new main.e2d.Multivector();
         
         double tempVar0000;
         double tempVar0001;
         double tempVar0002;
         double tempVar0003;
         
         result.Coef[3] = (mv1.Coef[3] * mv2.Coef[0]);
         tempVar0000 = (mv1.Coef[0] * mv2.Coef[0]);
         tempVar0001 = (mv1.Coef[1] * mv2.Coef[1]);
         tempVar0002 = (mv1.Coef[2] * mv2.Coef[2]);
         tempVar0003 = (-1 * mv1.Coef[3] * mv2.Coef[3]);
         result.Coef[0] = (tempVar0000 + tempVar0001 + tempVar0002 + tempVar0003);
         tempVar0000 = (mv1.Coef[1] * mv2.Coef[0]);
         tempVar0001 = (mv1.Coef[3] * mv2.Coef[2]);
         result.Coef[1] = (tempVar0000 + tempVar0001);
         tempVar0000 = (mv1.Coef[2] * mv2.Coef[0]);
         tempVar0001 = (-1 * mv1.Coef[3] * mv2.Coef[1]);
         result.Coef[2] = (tempVar0000 + tempVar0001);
         
         
         return result;
      }
      
      public static main.e2d.Multivector Subtract(main.e2d.Multivector mv1, main.e2d.Multivector mv2)
      {
         var result = new main.e2d.Multivector();
         
         double tempVar0000;
         
         tempVar0000 = (-1 * mv2.Coef[0]);
         result.Coef[0] = (mv1.Coef[0] + tempVar0000);
         tempVar0000 = (-1 * mv2.Coef[1]);
         result.Coef[1] = (mv1.Coef[1] + tempVar0000);
         tempVar0000 = (-1 * mv2.Coef[2]);
         result.Coef[2] = (mv1.Coef[2] + tempVar0000);
         tempVar0000 = (-1 * mv2.Coef[3]);
         result.Coef[3] = (mv1.Coef[3] + tempVar0000);
         
         
         return result;
      }
      
      public static main.e2d.Multivector Divide(main.e2d.Multivector mv1, double s2)
      {
         var result = new main.e2d.Multivector();
         
         double tempVar0000;
         
         tempVar0000 = Math.Pow(s2, -1);
         result.Coef[0] = (mv1.Coef[0] * tempVar0000);
         result.Coef[1] = (mv1.Coef[1] * tempVar0000);
         result.Coef[2] = (mv1.Coef[2] * tempVar0000);
         result.Coef[3] = (mv1.Coef[3] * tempVar0000);
         
         
         return result;
      }
      
      public static double SP(main.e2d.Multivector mv1, main.e2d.Multivector mv2)
      {
         double result;
         
         double tempVar0000;
         double tempVar0001;
         double tempVar0002;
         double tempVar0003;
         
         tempVar0000 = (mv1.Coef[0] * mv2.Coef[0]);
         tempVar0001 = (mv1.Coef[1] * mv2.Coef[1]);
         tempVar0002 = (mv1.Coef[2] * mv2.Coef[2]);
         tempVar0003 = (-1 * mv1.Coef[3] * mv2.Coef[3]);
         result = (tempVar0000 + tempVar0001 + tempVar0002 + tempVar0003);
         
         
         return result;
      }
      
      public static main.e2d.Multivector vinv(main.e2d.Multivector inMv)
      {
         var result = new main.e2d.Multivector();
         
         double tempVar0000;
         double tempVar0001;
         double tempVar0002;
         double tempVar0003;
         
         tempVar0000 = Math.Pow(inMv.Coef[0], 2);
         tempVar0001 = Math.Pow(inMv.Coef[1], 2);
         tempVar0002 = Math.Pow(inMv.Coef[2], 2);
         tempVar0003 = Math.Pow(inMv.Coef[3], 2);
         tempVar0000 = (tempVar0000 + tempVar0001 + tempVar0002 + tempVar0003);
         tempVar0000 = Math.Pow(tempVar0000, -1);
         result.Coef[0] = (inMv.Coef[0] * tempVar0000);
         result.Coef[1] = (inMv.Coef[1] * tempVar0000);
         result.Coef[2] = (inMv.Coef[2] * tempVar0000);
         result.Coef[3] = (-1 * inMv.Coef[3] * tempVar0000);
         
         
         return result;
      }
      
      public static main.e2d.Multivector div_by_norm(main.e2d.Multivector inMv)
      {
         var result = new main.e2d.Multivector();
         
         double tempVar0000;
         double tempVar0001;
         
         tempVar0000 = Math.Pow(3, -0.5);
         tempVar0001 = Math.Pow(inMv.Coef[0], 0.5);
         result.Coef[0] = (tempVar0000 * tempVar0001);
         tempVar0001 = Math.Pow(inMv.Coef[0], -0.5);
         result.Coef[1] = (inMv.Coef[1] * tempVar0000 * tempVar0001);
         result.Coef[2] = (inMv.Coef[2] * tempVar0000 * tempVar0001);
         result.Coef[3] = (inMv.Coef[3] * tempVar0000 * tempVar0001);
         
         
         return result;
      }
      
   }
}
