namespace GeometricAlgebraStructuresLib.Multivectors
{
    public interface IGaTerm<T>
    {
        ulong Id { get; }

        int Grade { get; }

        ulong Index { get; }

        T Scalar { get; }
        
        bool IsUniform { get; }
        
        bool IsGraded { get; }

        IGaTerm<T> GetCopy(T newScalar);
    }
}