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
        private static bool Equals1(double[] scalars1, double[] scalars2)
        {
            double c;
        
            c = scalars1[0] - scalars2[0];
            if (!c.IsNearZero()) 
                return false;
            
            return true;
        }
        
        
        private static bool Equals3(double[] scalars1, double[] scalars2)
        {
            double c;
        
            c = scalars1[0] - scalars2[0];
            if (!c.IsNearZero()) 
                return false;
            
            c = scalars1[1] - scalars2[1];
            if (!c.IsNearZero()) 
                return false;
            
            c = scalars1[2] - scalars2[2];
            if (!c.IsNearZero()) 
                return false;
            
            return true;
        }
        
        
        public bool Equals(Ega3DkVector blade2)
        {
            if ((object)blade2 == null) 
                return false;
        
            if (ReferenceEquals(this, blade2)) 
                return true;
        
            if (IsZero) 
                return blade2.IsZero;
        
            if (blade2.IsZero) 
                return IsZero;
        
            if (Grade != blade2.Grade) 
                return false;
        
            switch (Grade)
            {
                case 0:
                    return Equals1(Scalars, blade2.Scalars);
                case 1:
                    return Equals3(Scalars, blade2.Scalars);
                case 2:
                    return Equals3(Scalars, blade2.Scalars);
                case 3:
                    return Equals1(Scalars, blade2.Scalars);
            }
        
            throw new InvalidDataException("Internal error. Blade grade not acceptable!");
        }
        
    }
}
