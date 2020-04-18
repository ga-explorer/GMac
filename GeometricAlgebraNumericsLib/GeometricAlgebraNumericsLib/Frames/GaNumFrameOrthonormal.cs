using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraNumericsLib.Maps.Bilinear;
using GeometricAlgebraNumericsLib.Metrics;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraNumericsLib.Products;
using GeometricAlgebraNumericsLib.Products.Orthonormal;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraNumericsLib.Frames
{
    public sealed class GaNumFrameOrthonormal : GaNumFrame
    {
        private int[] BasisVectorsSignatures { get; }

        public GaNumMetricOrthonormal OrthonormalMetric { get; }


        /// <summary>
        /// Number of positive signature basis vectors
        /// </summary>
        public int PsvCount
        {
            get { return BasisVectorsSignatures.Count(s => s > 0); }
        }

        /// <summary>
        /// Number of nigative signature basis vectors
        /// </summary>
        public int NsvCount
        {
            get { return BasisVectorsSignatures.Count(s => s < 0); }
        }

        public override IGaNumMetric Metric => OrthonormalMetric;

        public override GaNumFrame BaseOrthogonalFrame => this;

        public override IGaNumMetricOrthogonal BaseOrthogonalMetric => OrthonormalMetric;

        public override bool IsEuclidean
            => BasisVectorsSignatures.All(s => s > 0);

        public override bool IsOrthogonal => true;

        public override bool IsOrthonormal => true;

        public override int VSpaceDimension
            => BasisVectorsSignatures.Length;

        public override IGaNumMapBilinear ComputedOp { get; }

        public override IGaNumMapBilinear ComputedGp { get; }

        public override IGaNumMapBilinear ComputedSp { get; }

        public override IGaNumMapBilinear ComputedLcp { get; }

        public override IGaNumMapBilinear ComputedRcp { get; }

        public override IGaNumMapBilinear ComputedFdp { get; }

        public override IGaNumMapBilinear ComputedHip { get; }

        public override IGaNumMapBilinear ComputedAcp { get; }

        public override IGaNumMapBilinear ComputedCp { get; }


        /// <summary>
        /// frameSigList must have all items equal to 1 (false) or -1 (true) with at least one negative item
        /// </summary>
        /// <param name="frameSigList"></param>
        internal GaNumFrameOrthonormal(IEnumerable<bool> frameSigList)
        {
            BasisVectorsSignatures = frameSigList.Select(b => b ? 1 : -1).ToArray();

            OrthonormalMetric = GaNumMetricOrthonormal.Create(frameSigList);

            Op = ComputedOp = new GaNumOp(VSpaceDimension);
            Gp = ComputedGp = new GaNumOrthonormalGp(OrthonormalMetric);
            Sp = ComputedSp = new GaNumOrthonormalSp(OrthonormalMetric);
            Lcp = ComputedLcp = new GaNumOrthonormalLcp(OrthonormalMetric);
            Rcp = ComputedRcp = new GaNumOrthonormalRcp(OrthonormalMetric);
            Fdp = ComputedFdp = new GaNumOrthonormalFdp(OrthonormalMetric);
            Hip = ComputedHip = new GaNumOrthonormalHip(OrthonormalMetric);
            Acp = ComputedAcp = new GaNumOrthonormalAcp(OrthonormalMetric);
            Cp = ComputedCp = new GaNumOrthonormalCp(OrthonormalMetric);

            UnitPseudoScalarCoef =
                (MaxBasisBladeId.BasisBladeIdHasNegativeReverse() ? -1.0d : 1.0d) / 
                OrthonormalMetric[MaxBasisBladeId];
        }

        
        protected override void ComputeIpm()
        {
            InnerProductMatrix = DenseMatrix.CreateDiagonal(
                BasisVectorsSignatures.Length,
                BasisVectorsSignatures.Length,
                i => BasisVectorsSignatures[i]
            );
        }


        public override double BasisVectorSignature(int basisVectorIndex)
        {
            if (basisVectorIndex >= 0 && basisVectorIndex < VSpaceDimension)
                return BasisVectorsSignatures[basisVectorIndex] < 0
                    ? -1.0d : 1.0d;

            throw new IndexOutOfRangeException();
        }

        public override GaNumSarMultivector BasisBladeSignature(int id)
        {
            if (id < 0 || id >= GaSpaceDimension)
                throw new IndexOutOfRangeException();

            return GaNumSarMultivector.CreateScalar(
                VSpaceDimension,
                OrthonormalMetric[id] < 0 ? -1.0d : 1.0d
            );
        }

        public override double Norm2(IGaNumMultivector mv)
        {
            return mv.Norm2(OrthonormalMetric);
        }
    }
}
