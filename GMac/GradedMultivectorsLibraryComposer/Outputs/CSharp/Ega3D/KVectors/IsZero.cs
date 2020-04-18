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
        private static bool IsNearZero1(double[] scalars)
        {
            return !(
                !scalars[0].IsNearZero()
                );
        }
        
        private static double[] TrimNearZero1(double[] scalars)
        {
            return new[]
            {
                scalars[0].IsNearZero() ? 0.0D : scalars[0]
            };
        }
        
        
        private static bool IsNearZero3(double[] scalars)
        {
            return !(
                !scalars[0].IsNearZero() ||
                !scalars[1].IsNearZero() ||
                !scalars[2].IsNearZero()
                );
        }
        
        private static double[] TrimNearZero3(double[] scalars)
        {
            return new[]
            {
                scalars[0].IsNearZero() ? 0.0D : scalars[0],
                scalars[1].IsNearZero() ? 0.0D : scalars[1],
                scalars[2].IsNearZero() ? 0.0D : scalars[2]
            };
        }
        
        
        public bool IsNearZero
        {
            get
            {
                if (IsZero)
                    return true;
        
                switch (Grade)
                {
                    case 0:
                        return IsNearZero1(Scalars);
                    case 1:
                        return IsNearZero3(Scalars);
                    case 2:
                        return IsNearZero3(Scalars);
                    case 3:
                        return IsNearZero1(Scalars);
                }
        
                throw new InvalidDataException("Internal error. Blade grade not acceptable!");
            }
        }
        
        /// <summary>
        /// Set all near-zero coefficients to zero. If all coefficients are near zero a Zero Multivector is returned
        /// </summary>
        public Ega3DkVector TrimNearZero
        {
            get
            {
                if (IsZero)
                    return Ega3DMultivector.Zero;
        
                switch (Grade)
                {
                    case 0:
                        return new Ega3DkVector(0, TrimNearZero1(Scalars));
                    case 1:
                        return new Ega3DkVector(1, TrimNearZero3(Scalars));
                    case 2:
                        return new Ega3DkVector(2, TrimNearZero3(Scalars));
                    case 3:
                        return new Ega3DkVector(3, TrimNearZero1(Scalars));
                }
        
                throw new InvalidDataException("Internal error. Blade grade not acceptable!");
            }
        }
        
    }
}
