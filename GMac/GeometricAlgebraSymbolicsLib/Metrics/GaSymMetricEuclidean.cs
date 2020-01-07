using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraNumericsLib.Frames;

namespace GeometricAlgebraSymbolicsLib.Metrics
{
    public sealed class GaSymMetricEuclidean : IReadOnlyList<int>, IGaSymMetricOrthogonal
    {
        public static GaSymMetricEuclidean Create(int vSpaceDim)
        {
            return new GaSymMetricEuclidean(vSpaceDim);
        }


        public int VSpaceDimension { get; }

        public int GaSpaceDimension
            => VSpaceDimension.ToGaSpaceDimension();

        public int Count
            => VSpaceDimension.ToGaSpaceDimension();

        public int this[int index]
        {
            get
            {
                if (index < 0 || index >= GaSpaceDimension)
                    throw new IndexOutOfRangeException();

                return 1;
            }
        }


        private GaSymMetricEuclidean(int vSpaceDim)
        {
            VSpaceDimension = vSpaceDim;
        }


        public IEnumerator<int> GetEnumerator()
        {
            return Enumerable.Repeat(1, GaSpaceDimension).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Enumerable.Repeat(1, GaSpaceDimension).GetEnumerator();
        }
    }
}
