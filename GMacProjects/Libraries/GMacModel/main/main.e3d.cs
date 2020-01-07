using System.Collections.Generic;
using System.Linq;

namespace GMacModel
{
    public static partial class main
    {
        public static class e3d
        {
            public static readonly Multivector Ii = new Multivector(0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, -1.0D);

            public sealed class Multivector
            {
                public readonly double[] Coef = new double[8];


                public Multivector()
                {
                }

                public Multivector(params double[] coefs)
                {
                    int i = 0;
                    foreach (var coef in coefs.Take(8))
                        Coef[i++] = coef;
                }

                public Multivector(IEnumerable<double> coefs)
                {
                    int i = 0;
                    foreach (var coef in coefs.Take(8))
                        Coef[i++] = coef;
                }


            }

        }
    }
}
