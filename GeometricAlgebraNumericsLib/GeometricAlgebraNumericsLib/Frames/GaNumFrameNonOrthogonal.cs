using System;
using GeometricAlgebraNumericsLib.Exceptions;
using GeometricAlgebraNumericsLib.Maps.Bilinear;
using GeometricAlgebraNumericsLib.Maps.Unilinear;
using GeometricAlgebraNumericsLib.Metrics;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraNumericsLib.Outermorphisms;
using GeometricAlgebraNumericsLib.Products;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraNumericsLib.Frames
{
    public sealed class GaNumFrameNonOrthogonal : GaNumFrame
    {
        /// <summary>
        /// The metric holding all information of the derived frame system
        /// </summary>
        public GaNumMetricNonOrthogonal NonOrthogonalMetric { get; }

        public IGaNumMapUnilinear ThisToBaseFrameCba
            => NonOrthogonalMetric.DerivedToBaseCba;

        public IGaNumMapUnilinear BaseFrameToThisCba
            => NonOrthogonalMetric.BaseToDerivedCba;

        public override IGaNumMetric Metric
            => NonOrthogonalMetric;

        public override IGaNumMetricOrthogonal BaseOrthogonalMetric
            => NonOrthogonalMetric.BaseMetric;

        public override GaNumFrame BaseOrthogonalFrame
            => NonOrthogonalMetric.BaseFrame;

        public override bool IsEuclidean => false;

        public override bool IsOrthogonal => false;

        public override bool IsOrthonormal => false;

        public override int VSpaceDimension => InnerProductMatrix.RowCount;

        public override IGaNumMapBilinear ComputedOp { get; }

        public override IGaNumMapBilinear ComputedGp { get; }

        public override IGaNumMapBilinear ComputedSp { get; }

        public override IGaNumMapBilinear ComputedLcp { get; }

        public override IGaNumMapBilinear ComputedRcp { get; }

        public override IGaNumMapBilinear ComputedFdp { get; }

        public override IGaNumMapBilinear ComputedHip { get; }

        public override IGaNumMapBilinear ComputedAcp { get; }

        public override IGaNumMapBilinear ComputedCp { get; }


        internal GaNumFrameNonOrthogonal(GaNumFrame baseOrthoFrame, Matrix ipm, GaNumOutermorphism derivedToBaseOm, GaNumOutermorphism baseToDerivedOm)
        {
            if (baseOrthoFrame.IsOrthogonal == false)
                throw new GaNumericsException("Base frame must be orthogonal");

            if (ipm.IsDiagonal())
                throw new GaNumericsException("Inner product matrix must be non-diagonal");

            InnerProductMatrix = ipm;

            NonOrthogonalMetric =
                new GaNumMetricNonOrthogonal(
                    baseOrthoFrame,
                    this,
                    derivedToBaseOm,
                    baseToDerivedOm
                );

            Op = ComputedOp = new GaNumOp(VSpaceDimension);
            Gp = ComputedGp = GaNumBilinearProductCba.CreateGp(NonOrthogonalMetric);
            //The following method is much less efficient than the above for less sparse and full
            //multivectors while marginally efficient for single-term multivectors
            //Gp = ComputedGp = new GaNumNonOrthogonalGp(NonOrthogonalMetric); 
            Sp = ComputedSp = GaNumBilinearProductCba.CreateSp(NonOrthogonalMetric);
            Lcp = ComputedLcp = GaNumBilinearProductCba.CreateLcp(NonOrthogonalMetric);
            Rcp = ComputedRcp = GaNumBilinearProductCba.CreateRcp(NonOrthogonalMetric);
            Fdp = ComputedFdp = GaNumBilinearProductCba.CreateFdp(NonOrthogonalMetric);
            Hip = ComputedHip = GaNumBilinearProductCba.CreateHip(NonOrthogonalMetric);
            Acp = ComputedAcp = GaNumBilinearProductCba.CreateAcp(NonOrthogonalMetric);
            Cp = ComputedCp = GaNumBilinearProductCba.CreateCp(NonOrthogonalMetric);

            UnitPseudoScalarCoef =
                (MaxBasisBladeId.BasisBladeIdHasNegativeReverse() ? -1.0d : 1.0d) /
                BasisBladeSignature(MaxBasisBladeId)[0];
        }


        protected override void ComputeIpm()
        {
            throw new NotImplementedException();
        }

        public override double BasisVectorSignature(int basisVectorIndex)
        {
            return InnerProductMatrix[basisVectorIndex, basisVectorIndex];
        }

        public override GaNumSarMultivector BasisBladeSignature(int id)
        {
            var basisBlade = GaNumSarMultivector.CreateBasisBlade(VSpaceDimension, id);

            var sig = Gp[basisBlade, basisBlade];

            return id.BasisBladeIdHasNegativeReverse() ? -sig : sig;
        }

        public override double Norm2(IGaNumMultivector mv)
        {
            return mv.Norm2(NonOrthogonalMetric);
        }
    }
}
