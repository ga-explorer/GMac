using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace main.geometry
{
   public sealed class RayTriangleIntersection
   {
      public double ray_param { get; set; }
      
      public double tri_a { get; set; }
      
      public double tri_b { get; set; }
      
      public double tri_c { get; set; }
      
      public double bc_a { get; set; }
      
      public double bc_b { get; set; }
      
      public double bc_c { get; set; }
      
      
      public RayTriangleIntersection()
      {}
   }
}
