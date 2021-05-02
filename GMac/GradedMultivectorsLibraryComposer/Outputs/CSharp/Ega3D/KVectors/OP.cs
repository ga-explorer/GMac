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
        public Ega3DkVector OP(Ega3DkVector blade2)
        {
            if (IsZero || blade2.IsZero || Grade + blade2.Grade > 3)
                return Ega3DMultivector.Zero;
        
            var id = Grade + blade2.Grade * (Ega3DUtils.VectorSpaceDimensions + 1);
        
            switch (id)
            {
                //grade1: 0, grade2: 0
                case 0:
                    return new Ega3DkVector(0, OP_000(Scalars, blade2.Scalars));
                
                //grade1: 0, grade2: 1
                case 4:
                    return new Ega3DkVector(1, OP_011(Scalars, blade2.Scalars));
                
                //grade1: 0, grade2: 2
                case 8:
                    return new Ega3DkVector(2, OP_022(Scalars, blade2.Scalars));
                
                //grade1: 0, grade2: 3
                case 12:
                    return new Ega3DkVector(3, OP_033(Scalars, blade2.Scalars));
                
                //grade1: 1, grade2: 0
                case 1:
                    return new Ega3DkVector(1, OP_101(Scalars, blade2.Scalars));
                
                //grade1: 1, grade2: 1
                case 5:
                    return new Ega3DkVector(2, OP_112(Scalars, blade2.Scalars));
                
                //grade1: 1, grade2: 2
                case 9:
                    return new Ega3DkVector(3, OP_123(Scalars, blade2.Scalars));
                
                //grade1: 2, grade2: 0
                case 2:
                    return new Ega3DkVector(2, OP_202(Scalars, blade2.Scalars));
                
                //grade1: 2, grade2: 1
                case 6:
                    return new Ega3DkVector(3, OP_213(Scalars, blade2.Scalars));
                
                //grade1: 3, grade2: 0
                case 3:
                    return new Ega3DkVector(3, OP_303(Scalars, blade2.Scalars));
                
            }
        
            throw new InvalidDataException("Internal error. Blade grade not acceptable!");
        }
        
    }
}
