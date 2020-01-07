using System;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;
using GeometricAlgebraSymbolicsLib.Maps.Bilinear;
using GeometricAlgebraSymbolicsLib.Matrices;
using GeometricAlgebraSymbolicsLib.Metrics;
using GeometricAlgebraSymbolicsLib.Multivectors;
using GeometricAlgebraSymbolicsLib.Products;
using GeometricAlgebraSymbolicsLib.Products.Euclidean;

namespace GeometricAlgebraSymbolicsLib.Frames
{
    public sealed class GaSymFrameEuclidean : GaSymFrame
    {
        public GaSymMetricEuclidean EuclideanMetric { get; }

        public override IGaSymMetric Metric => EuclideanMetric;

        public override GaSymFrame BaseOrthogonalFrame => this;

        public override IGaSymMetricOrthogonal BaseOrthogonalMetric => EuclideanMetric;

        public override bool IsEuclidean => true;

        public override bool IsOrthogonal => true;

        public override bool IsOrthonormal => true;

        public override int VSpaceDimension => EuclideanMetric.VSpaceDimension;

        public override IGaSymMapBilinear ComputedOp { get; }

        public override IGaSymMapBilinear ComputedGp { get; }

        public override IGaSymMapBilinear ComputedSp { get; }

        public override IGaSymMapBilinear ComputedLcp { get; }

        public override IGaSymMapBilinear ComputedRcp { get; }

        public override IGaSymMapBilinear ComputedFdp { get; }

        public override IGaSymMapBilinear ComputedHip { get; }

        public override IGaSymMapBilinear ComputedAcp { get; }

        public override IGaSymMapBilinear ComputedCp { get; }


        internal GaSymFrameEuclidean(int vSpaceDim)
        {
            EuclideanMetric = GaSymMetricEuclidean.Create(vSpaceDim);

            Op = ComputedOp = new GaSymOp(VSpaceDimension);
            Gp = ComputedGp = new GaSymEuclideanGp(VSpaceDimension);
            Sp = ComputedSp = new GaSymEuclideanSp(VSpaceDimension);
            Lcp = ComputedLcp = new GaSymEuclideanLcp(VSpaceDimension);
            Rcp = ComputedRcp = new GaSymEuclideanRcp(VSpaceDimension);
            Fdp = ComputedFdp = new GaSymEuclideanFdp(VSpaceDimension);
            Hip = ComputedHip = new GaSymEuclideanHip(VSpaceDimension);
            Acp = ComputedAcp = new GaSymEuclideanAcp(VSpaceDimension);
            Cp = ComputedCp = new GaSymEuclideanCp(VSpaceDimension);

            UnitPseudoScalarCoef = CasConstants.One;
        }


        protected override void ComputeIpm()
        {
            InnerProductMatrix = new GaSymMatrixIdentity(VSpaceDimension);
        }


        public override MathematicaScalar BasisVectorSignature(int basisVectorIndex)
        {
            if (basisVectorIndex >= 0 && basisVectorIndex < VSpaceDimension)
                return CasConstants.One;

            throw new IndexOutOfRangeException();
        }

        public override GaSymMultivector BasisBladeSignature(int id)
        {
            if (id >= 0 && id < GaSpaceDimension)
                return GaSymMultivector.CreateScalar(GaSpaceDimension, CasConstants.One);

            throw new IndexOutOfRangeException();
        }
    }
}
