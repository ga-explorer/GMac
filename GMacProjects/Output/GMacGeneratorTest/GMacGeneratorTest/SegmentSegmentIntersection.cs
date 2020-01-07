using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace main
{
   public sealed class SegmentSegmentIntersection
   {
      public double l1 { get; set; }
      
      public main.cga5d.Multivector q3 { get; set; }
      
      public main.cga5d.Multivector q4 { get; set; }
      
      
      public SegmentSegmentIntersection()
      {
         q3 = new main.cga5d.Multivector();
         q4 = new main.cga5d.Multivector();
      }
   }
}
