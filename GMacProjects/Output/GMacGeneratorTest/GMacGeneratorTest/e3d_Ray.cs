using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace main
{
   public sealed class e3d_Ray
   {
      public main.e3d.Multivector origin { get; set; }
      
      public main.e3d.Multivector direction { get; set; }
      
      
      public e3d_Ray()
      {
         origin = new main.e3d.Multivector();
         direction = new main.e3d.Multivector();
      }
   }
}
