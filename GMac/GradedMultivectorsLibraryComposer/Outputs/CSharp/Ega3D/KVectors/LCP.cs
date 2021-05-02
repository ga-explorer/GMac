using System.IO;

namespace GradedMultivectorsLibraryComposer.Outputs.CSharp.Ega3D
{
    /// <summary>
    /// This class represents a k-vector in the Ega3D frame with arbitrary grade 
    /// (i.e. grade is determined at runtime) based on additive representation of 
    /// the k-vector as a linear combination of basis blades of the same grade.
    /// </summary>
    public sealed partial class Ega3DkVector
        : IEga3DMultivector
    {
        public Ega3DkVector LCP(Ega3DkVector blade2)
        {
            if (IsZero || blade2.IsZero || Grade > blade2.Grade)
                return Ega3DMultivector.Zero;
        
            var id = Grade + blade2.Grade * (Ega3DUtils.VectorSpaceDimensions + 1);
        
            switch (id)
            {
                //grade1: 0, grade2: 0
                case 0:
                    return new Ega3DkVector(0, LCP_000(Scalars, blade2.Scalars));
                
                //grade1: 0, grade2: 1
                case 4:
                    return new Ega3DkVector(1, LCP_011(Scalars, blade2.Scalars));
                
                //grade1: 0, grade2: 2
                case 8:
                    return new Ega3DkVector(2, LCP_022(Scalars, blade2.Scalars));
                
                //grade1: 0, grade2: 3
                case 12:
                    return new Ega3DkVector(3, LCP_033(Scalars, blade2.Scalars));
                
                //grade1: 1, grade2: 1
                case 5:
                    return new Ega3DkVector(0, LCP_110(Scalars, blade2.Scalars));
                
                //grade1: 1, grade2: 2
                case 9:
                    return new Ega3DkVector(1, LCP_121(Scalars, blade2.Scalars));
                
                //grade1: 1, grade2: 3
                case 13:
                    return new Ega3DkVector(2, LCP_132(Scalars, blade2.Scalars));
                
                //grade1: 2, grade2: 2
                case 10:
                    return new Ega3DkVector(0, LCP_220(Scalars, blade2.Scalars));
                
                //grade1: 2, grade2: 3
                case 14:
                    return new Ega3DkVector(1, LCP_231(Scalars, blade2.Scalars));
                
                //grade1: 3, grade2: 3
                case 15:
                    return new Ega3DkVector(0, LCP_330(Scalars, blade2.Scalars));
                
            }
        
            throw new InvalidDataException("Internal error. Blade grade not acceptable!");
        }
        
    }
}
