using System;
using System.Collections.Generic;
using System.Text;
using GeometricAlgebraStructuresLib.Frames;

namespace GeometricAlgebraNumericsLib.Multivectors
{
    public sealed class GaTerm<T>
    {
        public ulong BasisBladeId { get; }

        public int BasisBladeGrade
            => BasisBladeId.BasisBladeGrade();

        public ulong BasisBladeIndex
            => BasisBladeId.BasisBladeIndex();

        public T ScalarValue { get; }


        public GaTerm(ulong basisBladeId, T scalarValue)
        {
            BasisBladeId = basisBladeId;
            ScalarValue = scalarValue;
        }
        
        public GaTerm(int grade, ulong index, T scalarValue)
        {
            BasisBladeId = GaFrameUtils.BasisBladeId(grade, index);
            ScalarValue = scalarValue;
        }
        
        public GaTerm(KeyValuePair<ulong, T> data)
        {
            BasisBladeId = data.Key;
            ScalarValue = data.Value;
        }

        public GaTerm(Tuple<ulong, T> data)
        {
            BasisBladeId = data.Item1;
            ScalarValue = data.Item2;
        }


        public override string ToString()
        {
            return new StringBuilder()
                .Append(ScalarValue)
                .Append(" E")
                .Append(BasisBladeId)
                .ToString();
        }
    }
}