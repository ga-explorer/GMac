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
        private static double[] Negative1(double[] scalars)
        {
            return new[]
            {
                -scalars[0]
            };
        }
        
        
        private static double[] Negative3(double[] scalars)
        {
            return new[]
            {
                -scalars[0],
                -scalars[1],
                -scalars[2]
            };
        }
        
        
        public Ega3DkVector Negative
        {
            get
            {
                if (IsZero)
                    return this;
        
                switch (Grade)
                {
                    case 0:
                        return new Ega3DkVector(0, Negative1(Scalars));
                    
                    case 1:
                        return new Ega3DkVector(1, Negative3(Scalars));
                    
                    case 2:
                        return new Ega3DkVector(2, Negative3(Scalars));
                    
                    case 3:
                        return new Ega3DkVector(3, Negative1(Scalars));
                    
                }
        
                throw new InvalidDataException("Internal error. Blade grade not acceptable!");
            }
        }
        
        
        public Ega3DkVector Reverse
        {
            get
            {
                if (IsZero)
                    return this;
        
                switch (Grade)
                {
                    case 0:
                        return this;
                    
                    case 1:
                        return this;
                    
                    case 2:
                        return new Ega3DkVector(2, Negative3(Scalars));
                    
                    case 3:
                        return new Ega3DkVector(3, Negative1(Scalars));
                    
                }
        
                throw new InvalidDataException("Internal error. Blade grade not acceptable!");
            }
        }
        
        
        public Ega3DkVector GradeInv
        {
            get
            {
                if (IsZero)
                    return this;
        
                switch (Grade)
                {
                    case 0:
                        return this;
                    
                    case 1:
                        return new Ega3DkVector(1, Negative3(Scalars));
                    
                    case 2:
                        return this;
                    
                    case 3:
                        return new Ega3DkVector(3, Negative1(Scalars));
                    
                }
        
                throw new InvalidDataException("Internal error. Blade grade not acceptable!");
            }
        }
        
        
        public Ega3DkVector CliffConj
        {
            get
            {
                if (IsZero)
                    return this;
        
                switch (Grade)
                {
                    case 0:
                        return this;
                    
                    case 1:
                        return new Ega3DkVector(1, Negative3(Scalars));
                    
                    case 2:
                        return new Ega3DkVector(2, Negative3(Scalars));
                    
                    case 3:
                        return this;
                    
                }
        
                throw new InvalidDataException("Internal error. Blade grade not acceptable!");
            }
        }
        
        
    }
}
