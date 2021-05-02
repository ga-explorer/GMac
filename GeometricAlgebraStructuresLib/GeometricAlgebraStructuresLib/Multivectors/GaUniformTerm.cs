using System;
using System.Collections.Generic;
using System.Text;
using GeometricAlgebraStructuresLib.Frames;

namespace GeometricAlgebraStructuresLib.Multivectors
{
    public sealed class GaUniformTerm<T> : IGaTerm<T>
    {
        public ulong Id { get; }

        public int Grade
            => Id.BasisBladeGrade();

        public ulong Index
            => Id.BasisBladeIndex();

        public T Scalar { get; }

        public bool IsUniform 
            => true;

        public bool IsGraded 
            => false;

        
        public GaUniformTerm(ulong basisBladeId, T scalarValue)
        {
            Id = basisBladeId;
            Scalar = scalarValue;
        }
        

        public GaUniformTerm(int grade, ulong index, T scalarValue)
        {
            Id = GaFrameUtils.BasisBladeId(grade, index);
            Scalar = scalarValue;
        }

        public GaUniformTerm(KeyValuePair<ulong, T> data)
        {
            Id = data.Key;
            Scalar = data.Value;
        }
        

        public GaUniformTerm(Tuple<ulong, T> data)
        {
            Id = data.Item1;
            Scalar = data.Item2;
        }
        
        
        public IGaTerm<T> GetCopy(T newScalar)
        {
            return new GaUniformTerm<T>(Id, newScalar);
        }

        public override string ToString()
        {
            return new StringBuilder()
                .Append(Scalar)
                .Append(" E")
                .Append(Id)
                .ToString();
        }
    }
}