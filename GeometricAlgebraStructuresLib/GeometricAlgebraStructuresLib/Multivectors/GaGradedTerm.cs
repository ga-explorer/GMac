using System.Text;
using GeometricAlgebraStructuresLib.Frames;

namespace GeometricAlgebraStructuresLib.Multivectors
{
    public sealed class GaGradedTerm<T> : IGaTerm<T>
    {
        public ulong Id 
            => GaFrameUtils.BasisBladeId(Grade, Index);

        public int Grade { get; }

        public ulong Index { get; }

        public T Scalar { get; }

        public bool IsUniform 
            => false;

        public bool IsGraded 
            => true;


        public GaGradedTerm(int basisBladeGrade, ulong basisBladeIndex, T scalarValue)
        {
            Grade = basisBladeGrade;
            Index = basisBladeIndex;
            Scalar = scalarValue;
        }
        

        public IGaTerm<T> GetCopy(T newScalar)
        {
            return new GaGradedTerm<T>(Grade, Index, newScalar);
        }
        
        public override string ToString()
        {
            return new StringBuilder()
                .Append(Scalar)
                .Append(" G")
                .Append(Grade)
                .Append(" I")
                .Append(Index)
                .ToString();
        }
    }
}