using System;
using GeometricAlgebraNumericsLib.Maps.Bilinear;
using GeometricAlgebraNumericsLib.Metrics;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraNumericsLib.Products;
using GeometricAlgebraNumericsLib.Products.Euclidean;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraNumericsLib.Frames
{
    public sealed class GaNumFrameEuclidean : GaNumFrame
    {
        public GaNumMetricEuclidean EuclideanMetric { get; }

        public override IGaNumMetric Metric => EuclideanMetric;

        public override GaNumFrame BaseOrthogonalFrame => this;

        public override IGaNumMetricOrthogonal BaseOrthogonalMetric => EuclideanMetric;

        public override bool IsEuclidean => true;

        public override bool IsOrthogonal => true;

        public override bool IsOrthonormal => true;

        public override int VSpaceDimension => EuclideanMetric.VSpaceDimension;

        public override IGaNumMapBilinear ComputedOp { get; }

        public override IGaNumMapBilinear ComputedGp { get; }

        public override IGaNumMapBilinear ComputedSp { get; }

        public override IGaNumMapBilinear ComputedLcp { get; }

        public override IGaNumMapBilinear ComputedRcp { get; }

        public override IGaNumMapBilinear ComputedFdp { get; }

        public override IGaNumMapBilinear ComputedHip { get; }

        public override IGaNumMapBilinear ComputedAcp { get; }

        public override IGaNumMapBilinear ComputedCp { get; }


        internal GaNumFrameEuclidean(int vSpaceDim)
        {
            EuclideanMetric = GaNumMetricEuclidean.Create(vSpaceDim);

            Op = ComputedOp = new GaNumOp(VSpaceDimension);
            Gp = ComputedGp = new GaNumEuclideanGp(VSpaceDimension);
            Sp = ComputedSp = new GaNumEuclideanSp(VSpaceDimension);
            Lcp = ComputedLcp = new GaNumEuclideanLcp(VSpaceDimension);
            Rcp = ComputedRcp = new GaNumEuclideanRcp(VSpaceDimension);
            Fdp = ComputedFdp = new GaNumEuclideanFdp(VSpaceDimension);
            Hip = ComputedHip = new GaNumEuclideanHip(VSpaceDimension);
            Acp = ComputedAcp = new GaNumEuclideanAcp(VSpaceDimension);
            Cp = ComputedCp = new GaNumEuclideanCp(VSpaceDimension);

            UnitPseudoScalarCoef = 1.0d;
        }


        protected override void ComputeIpm()
        {
            InnerProductMatrix = DenseMatrix.CreateIdentity(VSpaceDimension);
        }


        public override double BasisVectorSignature(int basisVectorIndex)
        {
            if (basisVectorIndex >= 0 && basisVectorIndex < VSpaceDimension)
                return 1.0d;

            throw new IndexOutOfRangeException();
        }

        public override GaNumSarMultivector BasisBladeSignature(int id)
        {
            if (id >= 0 && id < GaSpaceDimension)
                return GaNumSarMultivector.CreateScalar(VSpaceDimension, 1.0d);

            throw new IndexOutOfRangeException();
        }

        public override double Norm2(IGaNumMultivector mv)
        {
            return mv.ENorm2();
        }
    }
}
