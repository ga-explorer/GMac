using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace main
{
   public sealed class AnalyzeRotation_struct
   {
      public main.cga5d.Multivector axis { get; set; }
      
      public double sin_angle { get; set; }
      
      public double cos_angle { get; set; }
      
      public double scale { get; set; }
      
      
      public AnalyzeRotation_struct()
      {
         axis = new main.cga5d.Multivector();
      }
   }
}
