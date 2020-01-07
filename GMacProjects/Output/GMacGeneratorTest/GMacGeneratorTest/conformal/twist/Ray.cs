using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace main.conformal.twist
{
   public sealed class Ray
   {
      public main.cga5d.Multivector pos { get; set; }
      
      public main.cga5d.Multivector dir { get; set; }
      
      
      public Ray()
      {
         pos = new main.cga5d.Multivector();
         dir = new main.cga5d.Multivector();
      }
   }
}
