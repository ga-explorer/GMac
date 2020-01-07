using System.Collections.Generic;
using System.Linq;

namespace GMacModel
{
    public static partial class main
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

            public static Multivector vinv(Multivector inMv)
            {
                Multivector result = new Multivector();

                //Put processing code here

                return result;
            }
        }
    }
}
