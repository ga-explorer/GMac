using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraNumericsLib.Multivectors;
using GeometricAlgebraStructuresLib.Frames;

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

        public IReadOnlyList<double> BasisVectorsSignatures { get; }

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
            BasisVectorsSignatures = Enumerable.Repeat(1.0d, vSpaceDim).ToArray();
        }


        public double GetBasisVectorSignature(int index)
        {
            return 1.0d;
        }

        public double GetBasisBladeSignature(int id)
        {
            return 1.0d;
        }

        public GaTerm<double> Gp(int id1, int id2)
        {
            return new GaTerm<double>(
                id1 ^ id2,
                GaFrameUtils.IsNegativeEGp(id1, id2) ? -1.0d : 1.0d
            );
        }

        public GaTerm<double> ScaledGp(int id1, int id2, double scalingFactor)
        {
            return new GaTerm<double>(
                id1 ^ id2,
                GaFrameUtils.IsNegativeEGp(id1, id2) ? -scalingFactor : scalingFactor
            );
        }

        public GaTerm<double> Gp(int id1, int id2, int id3)
        {
            var idXor12 = id1 ^ id2;
            var value =
                (GaFrameUtils.IsNegativeEGp(id1, id2) != GaFrameUtils.IsNegativeEGp(idXor12, id3))
                    ? -1 : 1;

            return new GaTerm<double>(idXor12 ^ id3, value);
        }

        public GaTerm<double> ScaledGp(int id1, int id2, int id3, double scalingFactor)
        {
            var idXor12 = id1 ^ id2;
            var value =
                (GaFrameUtils.IsNegativeEGp(id1, id2) != GaFrameUtils.IsNegativeEGp(idXor12, id3))
                    ? -scalingFactor : scalingFactor;

            return new GaTerm<double>(idXor12 ^ id3, value);
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
