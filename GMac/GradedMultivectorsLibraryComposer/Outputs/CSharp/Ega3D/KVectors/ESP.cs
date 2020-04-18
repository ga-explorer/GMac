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
        public Ega3DkVector ESP(Ega3DkVector blade2)
        {
            if (IsZero || blade2.IsZero || Grade != blade2.Grade)
                return Ega3DMultivector.Zero;
        
            var id = Grade + blade2.Grade * (Ega3DUtils.VectorSpaceDimensions + 1);
        
            switch (id)
            {
                //grade1: 0, grade2: 0
                case 0:
                    return new Ega3DkVector(0, ESP_000(Scalars, blade2.Scalars));
                
                //grade1: 1, grade2: 1
                case 5:
                    return new Ega3DkVector(0, ESP_110(Scalars, blade2.Scalars));
                
                //grade1: 2, grade2: 2
                case 10:
                    return new Ega3DkVector(0, ESP_220(Scalars, blade2.Scalars));
                
                //grade1: 3, grade2: 3
                case 15:
                    return new Ega3DkVector(0, ESP_330(Scalars, blade2.Scalars));
                
            }
        
            throw new InvalidDataException("Internal error. Blade grade not acceptable!");
        }
        
    }
}
