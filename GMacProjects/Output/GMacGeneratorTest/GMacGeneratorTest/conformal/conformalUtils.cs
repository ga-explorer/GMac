using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace main.conformal
{
   public static class conformalUtils
   {
      public static double RayPlaneIntersect(main.cga5d.Multivector ray_orgn, main.cga5d.Multivector ray_dir, main.cga5d.Multivector plane_normal, main.cga5d.Multivector plane_orgn)
      {
         double result;
         
         double[] tempArray = new double[9];
         
         tempArray[0] = (-1 * ray_orgn.Coef[0]);
         tempArray[0] = (plane_orgn.Coef[0] + tempArray[0]);
         tempArray[0] = (plane_normal.Coef[0] * tempArray[0]);
         tempArray[1] = (-1 * ray_orgn.Coef[2]);
         tempArray[1] = (plane_orgn.Coef[2] + tempArray[1]);
         tempArray[1] = (plane_normal.Coef[2] * tempArray[1]);
         tempArray[2] = (-1 * ray_orgn.Coef[4]);
         tempArray[2] = (plane_orgn.Coef[4] + tempArray[2]);
         tempArray[2] = (plane_normal.Coef[4] * tempArray[2]);
         tempArray[3] = (-1 * ray_orgn.Coef[6]);
         tempArray[3] = (plane_orgn.Coef[6] + tempArray[3]);
         tempArray[3] = (-1 * plane_normal.Coef[6] * tempArray[3]);
         tempArray[4] = (-1 * ray_orgn.Coef[8]);
         tempArray[4] = (plane_orgn.Coef[8] + tempArray[4]);
         tempArray[4] = (plane_normal.Coef[8] * tempArray[4]);
         tempArray[5] = (-1 * ray_orgn.Coef[10]);
         tempArray[5] = (plane_orgn.Coef[10] + tempArray[5]);
         tempArray[5] = (-1 * plane_normal.Coef[10] * tempArray[5]);
         tempArray[6] = (-1 * ray_orgn.Coef[12]);
         tempArray[6] = (plane_orgn.Coef[12] + tempArray[6]);
         tempArray[6] = (-1 * plane_normal.Coef[12] * tempArray[6]);
         tempArray[7] = (-1 * ray_orgn.Coef[14]);
         tempArray[7] = (plane_orgn.Coef[14] + tempArray[7]);
         tempArray[7] = (-1 * plane_normal.Coef[14] * tempArray[7]);
         tempArray[0] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         tempArray[1] = (ray_dir.Coef[0] * plane_normal.Coef[0]);
         tempArray[2] = (ray_dir.Coef[2] * plane_normal.Coef[2]);
         tempArray[3] = (ray_dir.Coef[4] * plane_normal.Coef[4]);
         tempArray[4] = (-1 * ray_dir.Coef[6] * plane_normal.Coef[6]);
         tempArray[5] = (ray_dir.Coef[8] * plane_normal.Coef[8]);
         tempArray[6] = (-1 * ray_dir.Coef[10] * plane_normal.Coef[10]);
         tempArray[7] = (-1 * ray_dir.Coef[12] * plane_normal.Coef[12]);
         tempArray[8] = (-1 * ray_dir.Coef[14] * plane_normal.Coef[14]);
         tempArray[1] = (tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7] + tempArray[8]);
         tempArray[1] = Math.Pow(tempArray[1], -1);
         result = (tempArray[0] * tempArray[1]);
         
         
         return result;
      }
      
   }
}
