using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace main
{
   public sealed class e3d_HitInfo
   {
      public main.e3d_Ray ray { get; set; }
      
      public main.e3d.Multivector hit_point { get; set; }
      
      public main.e3d.Multivector normal { get; set; }
      
      
      public e3d_HitInfo()
      {
         ray = new main.e3d_Ray();
         hit_point = new main.e3d.Multivector();
         normal = new main.e3d.Multivector();
      }
   }
}
