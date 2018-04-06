using System.Collections.Generic;
using System.Linq;
using GMac.GMacMath.Numeric.Maps.Bilinear;
using GMac.GMacMath.Numeric.Metrics;
using GMac.GMacMath.Numeric.Multivectors;
using GMac.GMacMath.Numeric.Products;
using GMac.GMacMath.Numeric.Products.Orthogonal;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GMac.GMacMath.Numeric.Frames
{
    public sealed class GaNumFrameOrthogonal : GaNumFrame
    {
        public IReadOnlyList<double> BasisVectorsSignatures { get; }

        public GaNumMetricOrthogonal OrthogonalMetric { get; }

        public override IGaNumMetric Metric => OrthogonalMetric;

        public override GaNumFrame BaseOrthogonalFrame => this;

        public override IGaNumMetricOrthogonal BaseOrthogonalMetric => OrthogonalMetric;

        public override bool IsEuclidean => false;

        public override bool IsOrthogonal => true;

        public override bool IsOrthonormal => false;

        public override int VSpaceDimension
            => BasisVectorsSignatures.Count;

        public override IGaNumMapBilinear ComputedOp { get; }

        public override IGaNumMapBilinear ComputedGp { get; }

        public override IGaNumMapBilinear ComputedSp { get; }

        public override IGaNumMapBilinear ComputedLcp { get; }

        public override IGaNumMapBilinear ComputedRcp { get; }

        public override IGaNumMapBilinear ComputedFdp { get; }

        public override IGaNumMapBilinear ComputedHip { get; }

        public override IGaNumMapBilinear ComputedAcp { get; }

        public override IGaNumMapBilinear ComputedCp { get; }


        internal GaNumFrameOrthogonal(IEnumerable<double> basisVectorsSignaturesList)
        {
            BasisVectorsSignatures = basisVectorsSignaturesList.ToArray();

            OrthogonalMetric = GaNumMetricOrthogonal.Create(BasisVectorsSignatures);

            Op = ComputedOp = new GaNumOp(VSpaceDimension);
            Gp = ComputedGp = new GaNumOrthogonalGp(OrthogonalMetric);
            Sp = ComputedSp = new GaNumOrthogonalSp(OrthogonalMetric);
            Lcp = ComputedLcp = new GaNumOrthogonalLcp(OrthogonalMetric);
            Rcp = ComputedRcp = new GaNumOrthogonalRcp(OrthogonalMetric);
            Fdp = ComputedFdp = new GaNumOrthogonalFdp(OrthogonalMetric);
            Hip = ComputedHip = new GaNumOrthogonalHip(OrthogonalMetric);
            Acp = ComputedAcp = new GaNumOrthogonalAcp(OrthogonalMetric);
            Cp = ComputedCp = new GaNumOrthogonalCp(OrthogonalMetric);

            UnitPseudoScalarCoef =
                (MaxBasisBladeId.BasisBladeIdHasNegativeReverse() ? -1.0d : 1.0d) /
                OrthogonalMetric[MaxBasisBladeId];
        }

        
        protected override void ComputeIpm()
        {
            InnerProductMatrix = DenseMatrix.CreateDiagonal(
                BasisVectorsSignatures.Count,
                BasisVectorsSignatures.Count,
                i => BasisVectorsSignatures[i]
            );
        }


        public override double BasisVectorSignature(int basisVectorIndex)
        {
            return BasisVectorsSignatures[basisVectorIndex];
        }

        public override GaNumMultivector BasisBladeSignature(int id)
        {
            return GaNumMultivector.CreateScalar(GaSpaceDimension, OrthogonalMetric[id]);
        }
    }
}
