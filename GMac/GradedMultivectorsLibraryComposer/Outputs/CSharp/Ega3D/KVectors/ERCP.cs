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
        public Ega3DkVector ERCP(Ega3DkVector blade2)
        {
            if (IsZero || blade2.IsZero || Grade < blade2.Grade)
                return Ega3DMultivector.Zero;
        
            var id = Grade + blade2.Grade * (Ega3DUtils.VectorSpaceDimensions + 1);
        
            switch (id)
            {
                //grade1: 0, grade2: 0
                case 0:
                    return new Ega3DkVector(0, ERCP_000(Scalars, blade2.Scalars));
                
                //grade1: 1, grade2: 0
                case 1:
                    return new Ega3DkVector(1, ERCP_101(Scalars, blade2.Scalars));
                
                //grade1: 1, grade2: 1
                case 5:
                    return new Ega3DkVector(0, ERCP_110(Scalars, blade2.Scalars));
                
                //grade1: 2, grade2: 0
                case 2:
                    return new Ega3DkVector(2, ERCP_202(Scalars, blade2.Scalars));
                
                //grade1: 2, grade2: 1
                case 6:
                    return new Ega3DkVector(1, ERCP_211(Scalars, blade2.Scalars));
                
                //grade1: 2, grade2: 2
                case 10:
                    return new Ega3DkVector(0, ERCP_220(Scalars, blade2.Scalars));
                
                //grade1: 3, grade2: 0
                case 3:
                    return new Ega3DkVector(3, ERCP_303(Scalars, blade2.Scalars));
                
                //grade1: 3, grade2: 1
                case 7:
                    return new Ega3DkVector(2, ERCP_312(Scalars, blade2.Scalars));
                
                //grade1: 3, grade2: 2
                case 11:
                    return new Ega3DkVector(1, ERCP_321(Scalars, blade2.Scalars));
                
                //grade1: 3, grade2: 3
                case 15:
                    return new Ega3DkVector(0, ERCP_330(Scalars, blade2.Scalars));
                
            }
        
            throw new InvalidDataException("Internal error. Blade grade not acceptable!");
        }
        
    }
}
