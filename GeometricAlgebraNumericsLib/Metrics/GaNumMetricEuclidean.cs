using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraNumericsLib.Frames;

namespace GeometricAlgebraNumericsLib.Metrics
{
    public sealed class GaNumMetricEuclidean : IReadOnlyList<int>, IGaNumMetricOrthogonal
    {
        public static GaNumMetricEuclidean Create(int vSpaceDim)
        {
            return new GaNumMetricEuclidean(vSpaceDim);
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


        private GaNumMetricEuclidean(int vSpaceDim)
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
