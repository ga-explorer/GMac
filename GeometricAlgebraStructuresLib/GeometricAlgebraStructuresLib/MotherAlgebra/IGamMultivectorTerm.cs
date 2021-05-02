using GeometricAlgebraStructuresLib.MotherAlgebra.Basis;

namespace GeometricAlgebraStructuresLib.MotherAlgebra
{
    public interface IGamMultivectorTerm<T>
    {
        GamBasisBlade BasisBlade { get; }

        int Grade { get; }

        T Scalar { get; }
        
        bool IsUniform { get; }
        
        bool IsGraded { get; }

        IGamMultivectorTerm<T> GetCopy(T newScalar);
    }
}