using System;
using System.IO;

namespace Ega3D
{
    /// <summary>
    /// This class represents a k-vector in the Ega3D frame with arbitrary grade 
    /// (i.e. grade is determined at runtime) based on additive representation of 
    /// the k-vector as a linear combination of basis blades of the same grade.
    /// </summary>
    public sealed partial class Ega3DkVector
        : IEga3DMultivector
    {
        private static int MaxCoefId2(double[] scalars)
        {
            var c = Math.Abs(scalars[0]);
            var maxCoef = c;
            var maxCoefId = 3;
        
            c = Math.Abs(scalars[1]);
            if (c > maxCoef)
            {
                maxCoef = c;
                maxCoefId = 5;
            }
            
            c = Math.Abs(scalars[2]);
            if (c > maxCoef)
                maxCoefId = 6;
        
            return maxCoefId;
        }
        
        private static Ega3DVector[] FactorGrade2(double[] scalars)
        {
            var maxCoefId = MaxCoefId2(scalars);
        
            switch (maxCoefId)
            {
                case 5:
                    return Factor5(scalars);
                case 6:
                    return Factor6(scalars);
            }
        
            throw new InvalidDataException("Internal error. Blade grade not acceptable!");
        }
        
        public Ega3DFactoredBlade Factor()
        {
            if (IsZero)
                return new Ega3DFactoredBlade(0.0D);
        
            switch (Grade)
            {
                case 0:
                    return new Ega3DFactoredBlade(Scalars[0]);
        
                case 1:
                    var vector = ToVector();
                    var norm = vector.Normalize();
                    return new Ega3DFactoredBlade(norm, vector);
        
                case 2:
                    return new Ega3DFactoredBlade(EMag_2(Scalars), FactorGrade2(Scalars));
                
                case 3:
                    return new Ega3DFactoredBlade(Scalars[0],  Ega3DVector.BasisVectors());
            }
        
            throw new InvalidDataException("Internal error. Blade grade not acceptable!");
        }
        
    }
}
