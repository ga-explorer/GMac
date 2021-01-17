namespace GeometricAlgebraStructuresLib.Multivectors
{
    public interface IGaTerm<T>
    {
        int Id { get; }

        int Grade { get; }

        int Index { get; }

        T Scalar { get; }
        
        bool IsUniform { get; }
        
        bool IsGraded { get; }

        IGaTerm<T> GetCopy(T newScalar);
    }
}