using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraStructuresLib.Frames;

namespace GeometricAlgebraSymbolicsLib.Metrics
{
    public sealed class GaSymMetricEuclidean : IReadOnlyList<int>, IGaSymMetricOrthogonal
    {
        public static GaSymMetricEuclidean Create(int vSpaceDim)
        {
            return new GaSymMetricEuclidean(vSpaceDim);
        }


        public int VSpaceDimension { get; }

        public ulong GaSpaceDimension
            => VSpaceDimension.ToGaSpaceDimension();

        public int Count
            => (int)VSpaceDimension.ToGaSpaceDimension();

        public int this[int index]
        {
            get
            {
                if (index < 0 || index >= (int)GaSpaceDimension)
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
            return Enumerable.Repeat(1, (int)GaSpaceDimension).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Enumerable.Repeat(1, (int)GaSpaceDimension).GetEnumerator();
        }
    }
}
