using System;
using GMac.GMacMath.Symbolic.Maps.Bilinear;
using GMac.GMacMath.Symbolic.Maps.Unilinear;
using GMac.GMacMath.Symbolic.Metrics;
using GMac.GMacMath.Symbolic.Multivectors;
using GMac.GMacMath.Symbolic.Products;
using SymbolicInterface.Mathematica;
using SymbolicInterface.Mathematica.Expression;

namespace GMac.GMacMath.Symbolic.Frames
{
    public sealed class GaSymFrameNonOrthogonal : GaSymFrame
    {
        /// <summary>
        /// The matric holding all information of the derived frame system
        /// </summary>
        public GaSymMetricNonOrthogonal NonOrthogonalMetric { get; }

        public IGaSymMapUnilinear ThisToBaseFrameCba 
            => NonOrthogonalMetric.DerivedToBaseCba;

        public IGaSymMapUnilinear BaseFrameToThisCba 
            => NonOrthogonalMetric.BaseToDerivedCba;

        public override IGaSymMetric Metric 
            => NonOrthogonalMetric;

        public override IGaSymMetricOrthogonal BaseOrthogonalMetric 
            => NonOrthogonalMetric.BaseMetric;

        public override GaSymFrame BaseOrthogonalFrame 
            => NonOrthogonalMetric.BaseFrame;

        public override bool IsEuclidean => false;

        public override bool IsOrthogonal => false;

        public override bool IsOrthonormal => false;

        public override int VSpaceDimension => InnerProductMatrix.RowCount;

        public override IGaSymMapBilinear ComputedOp { get; }

        public override IGaSymMapBilinear ComputedGp { get; }

        public override IGaSymMapBilinear ComputedSp { get; }

        public override IGaSymMapBilinear ComputedLcp { get; }

        public override IGaSymMapBilinear ComputedRcp { get; }

        public override IGaSymMapBilinear ComputedFdp { get; }

        public override IGaSymMapBilinear ComputedHip { get; }

        public override IGaSymMapBilinear ComputedAcp { get; }

        public override IGaSymMapBilinear ComputedCp { get; }


        internal GaSymFrameNonOrthogonal(GaSymFrame baseOrthoFrame, ISymbolicMatrix ipm, GaSymOutermorphism derivedToBaseOm, GaSymOutermorphism baseToDerivedOm)
        {
            if (baseOrthoFrame.IsOrthogonal == false)
                throw new GMacSymbolicException("Base frame must be orthogonal");

            if (ipm.IsSymmetric() == false || ipm.IsDiagonal())
                throw new GMacSymbolicException("Inner product matrix must be symmetric and non-diagonal");

            InnerProductMatrix = ipm.ToMathematicaMatrix();

            NonOrthogonalMetric =
                new GaSymMetricNonOrthogonal(
                    baseOrthoFrame, 
                    this,
                    derivedToBaseOm, 
                    baseToDerivedOm
                );

            Op = ComputedOp = new GaSymOp(VSpaceDimension);
            Gp = ComputedGp = GaSymBilinearProductCba.CreateGp(NonOrthogonalMetric);
            Sp = ComputedSp = GaSymBilinearProductCba.CreateSp(NonOrthogonalMetric);
            Lcp = ComputedLcp = GaSymBilinearProductCba.CreateLcp(NonOrthogonalMetric);
            Rcp = ComputedRcp = GaSymBilinearProductCba.CreateRcp(NonOrthogonalMetric);
            Fdp = ComputedFdp = GaSymBilinearProductCba.CreateFdp(NonOrthogonalMetric);
            Hip = ComputedHip = GaSymBilinearProductCba.CreateHip(NonOrthogonalMetric);
            Acp = ComputedAcp = GaSymBilinearProductCba.CreateAcp(NonOrthogonalMetric);
            Cp = ComputedCp = GaSymBilinearProductCba.CreateCp(NonOrthogonalMetric);

            UnitPseudoScalarCoef =
                MaxBasisBladeId.BasisBladeIdHasNegativeReverse()
                    ? CasConstants.MinusOne / BasisBladeSignature(MaxBasisBladeId)[0].ToMathematicaScalar()
                    : CasConstants.One / BasisBladeSignature(MaxBasisBladeId)[0].ToMathematicaScalar();
        }
        

        protected override void ComputeIpm()
        {
            throw new NotImplementedException();
        }

        public override MathematicaScalar BasisVectorSignature(int basisVectorIndex)
        {
            return InnerProductMatrix[basisVectorIndex, basisVectorIndex];
        }

        public override GaSymMultivector BasisBladeSignature(int id)
        {
            var basisBlade = GaSymMultivector.CreateBasisBlade(GaSpaceDimension, id);

            var sig = Gp[basisBlade, basisBlade];

            return id.BasisBladeIdHasNegativeReverse() ? -sig : sig;
        }

    }
}
