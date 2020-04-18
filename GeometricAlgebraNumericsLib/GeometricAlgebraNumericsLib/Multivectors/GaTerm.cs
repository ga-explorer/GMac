using System;
using System.Collections.Generic;
using System.Text;
using GeometricAlgebraNumericsLib.Frames;

namespace GeometricAlgebraNumericsLib.Multivectors
{
    public sealed class GaTerm<T>
    {
        public int BasisBladeId { get; }

        public int BasisBladeGrade
            => BasisBladeId.BasisBladeGrade();

        public int BasisBladeIndex
            => BasisBladeId.BasisBladeIndex();

        public T ScalarValue { get; }


        public GaTerm(int basisBladeId, T scalarValue)
        {
            BasisBladeId = basisBladeId;
            ScalarValue = scalarValue;
        }

        public GaTerm(ulong basisBladeId, T scalarValue)
        {
            BasisBladeId = (int)basisBladeId;
            ScalarValue = scalarValue;
        }

        public GaTerm(int grade, int index, T scalarValue)
        {
            BasisBladeId = GaNumFrameUtils.BasisBladeId(grade, index);
            ScalarValue = scalarValue;
        }

        public GaTerm(KeyValuePair<int, T> data)
        {
            BasisBladeId = data.Key;
            ScalarValue = data.Value;
        }

        public GaTerm(KeyValuePair<ulong, T> data)
        {
            BasisBladeId = (int)data.Key;
            ScalarValue = data.Value;
        }

        public GaTerm(Tuple<int, T> data)
        {
            BasisBladeId = data.Item1;
            ScalarValue = data.Item2;
        }

        public GaTerm(Tuple<ulong, T> data)
        {
            BasisBladeId = (int)data.Item1;
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