﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraNumericsLib.Multivectors;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories;
using GeometricAlgebraNumericsLib.Structures.BinaryTrees;
using GeometricAlgebraStructuresLib.Frames;

namespace GeometricAlgebraNumericsLib.Metrics
{
    public class GaNumMetricOrthogonal : IReadOnlyList<double>, IGaNumMetricOrthogonal
    {
        public static GaNumMetricOrthogonal Create(IReadOnlyList<double> basisVectorsSignaturesList)
        {
            return new GaNumMetricOrthogonal(basisVectorsSignaturesList);
        }

        public static GaNumMetricOrthogonal Create(params double[] basisVectorsSignaturesList)
        {
            return new GaNumMetricOrthogonal(basisVectorsSignaturesList);
        }

        public static GaNumMetricOrthogonal Create(IEnumerable<double> basisVectorsSignaturesList)
        {
            return new GaNumMetricOrthogonal(basisVectorsSignaturesList.ToArray());
        }


        public IReadOnlyList<double> BasisVectorsSignatures { get; }

        public GaBtrInternalNode<double> BasisSignaturesTermsTree
            => GetMetricSignatureMultivector().BtrRootNode;

        public int VSpaceDimension { get; }

        public ulong GaSpaceDimension
            => VSpaceDimension.ToGaSpaceDimension();

        public int Count
            => (int)VSpaceDimension.ToGaSpaceDimension();

        public double this[int index] 
            => GetBasisBladeSignature((ulong)index);


        private GaNumMetricOrthogonal(IReadOnlyList<double> basisVectorsSignaturesList)
        {
            BasisVectorsSignatures = basisVectorsSignaturesList;

            VSpaceDimension = basisVectorsSignaturesList.Count;
        }


        public GaNumSarMultivector GetMetricSignatureMultivector()
        {
            var basisSignaturesMultivector = new GaNumSarMultivectorFactory(VSpaceDimension);

            for (var m = 0; m < VSpaceDimension; m++)
            {
                var basisVectorSignature = BasisVectorsSignatures[m];

                if (basisVectorSignature != 0.0d)
                    basisSignaturesMultivector.SetTerm(1UL << m, basisVectorSignature);
            }

            var idsSeq = GaFrameUtils.BasisBladeIDsSortedByGrade(VSpaceDimension, 2);
            foreach (var id in idsSeq)
            {
                id.SplitBySmallestBasisVectorId(out var id1, out var id2);

                var bvs1 = basisSignaturesMultivector[id1];
                var bvs2 = basisSignaturesMultivector[id2];
                var bvs = bvs1 * bvs2;

                if (bvs != 0.0d)
                    basisSignaturesMultivector.SetTerm(id, bvs1 * bvs2);
            }

            return basisSignaturesMultivector.GetSarMultivectorCopy();
        }

        public double GetBasisVectorSignature(int index)
        {
            return BasisVectorsSignatures[1 << index];
        }

        public double GetBasisBladeSignature(ulong id)
        {
            var signature = 1.0d;

            var index = 0;
            while (id != 0)
            {
                if ((id & 1) != 0)
                    signature *= BasisVectorsSignatures[index];

                id >>= 1;
                index++;
            }

            return signature;
        }

        public GaTerm<double> Gp(ulong id1, ulong id2)
        {
            var metricValue = this[(int)(id1 & id2)];

            return new GaTerm<double>(
                id1 ^ id2,
                GaFrameUtils.IsNegativeEGp(id1, id2) ? -metricValue : metricValue
            );
        }

        public GaTerm<double> ScaledGp(ulong id1, ulong id2, double scalingFactor)
        {
            var metricValue = scalingFactor * this[(int)(id1 & id2)];

            return new GaTerm<double>(
                id1 ^ id2,
                GaFrameUtils.IsNegativeEGp(id1, id2) ? -metricValue : metricValue
            );
        }

        public GaTerm<double> Gp(ulong id1, ulong id2, ulong id3)
        {
            var idXor12 = id1 ^ id2;
            var metricValue = this[(int)(id1 & id2)] * this[(int)(idXor12 & id3)];

            if (GaFrameUtils.IsNegativeEGp(id1, id2) != GaFrameUtils.IsNegativeEGp(idXor12, id3))
                metricValue = -metricValue;

            return new GaTerm<double>(idXor12 ^ id3, metricValue);
        }

        public GaTerm<double> ScaledGp(ulong id1, ulong id2, ulong id3, double scalingFactor)
        {
            var idXor12 = id1 ^ id2;
            var metricValue = scalingFactor * this[(int)(id1 & id2)] * this[(int)(idXor12 & id3)];

            if (GaFrameUtils.IsNegativeEGp(id1, id2) != GaFrameUtils.IsNegativeEGp(idXor12, id3))
                metricValue = -metricValue;

            return new GaTerm<double>(idXor12 ^ id3, metricValue);
        }

        public IEnumerator<double> GetEnumerator()
        {
            //TODO: This is not the most efficient way
            for (var i = 0UL; i < GaSpaceDimension; i++)
                yield return this[(int)i];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
