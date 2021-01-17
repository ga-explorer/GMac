using System.Text;
using GeometricAlgebraStructuresLib.Frames;

namespace GeometricAlgebraStructuresLib.Multivectors
{
    public sealed class GaTerm<T> : IGaTerm<T>
    {
        public int Id { get; }

        public int Grade { get; }

        public int Index { get; }

        public T Scalar { get; }

        public bool IsUniform 
            => true;

        public bool IsGraded 
            => true;


        public GaTerm(int basisBladeGrade, int basisBladeIndex, T scalarValue)
        {
            Id = GaFrameUtils.BasisBladeId(basisBladeGrade, basisBladeIndex);
            Grade = basisBladeGrade;
            Index = basisBladeIndex;
            Scalar = scalarValue;
        }

        public GaTerm(int basisBladeId, T scalarValue)
        {
            Id = basisBladeId;
            basisBladeId.BasisBladeGradeIndex(out var basisBladeGrade, out var basisBladeIndex);
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