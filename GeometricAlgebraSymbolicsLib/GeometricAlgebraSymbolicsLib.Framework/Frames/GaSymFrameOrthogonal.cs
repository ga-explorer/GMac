using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraStructuresLib.Frames;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;
using GeometricAlgebraSymbolicsLib.Maps.Bilinear;
using GeometricAlgebraSymbolicsLib.Metrics;
using GeometricAlgebraSymbolicsLib.Multivectors;
using GeometricAlgebraSymbolicsLib.Products;
using GeometricAlgebraSymbolicsLib.Products.Orthogonal;

namespace GeometricAlgebraSymbolicsLib.Frames
{
    public sealed class GaSymFrameOrthogonal : GaSymFrame
    {
        public IReadOnlyList<MathematicaScalar> BasisVectorsSignatures { get; }

        public GaSymMetricOrthogonal OrthogonalMetric { get; }

        public override IGaSymMetric Metric => OrthogonalMetric;

        public override GaSymFrame BaseOrthogonalFrame => this;

        public override IGaSymMetricOrthogonal BaseOrthogonalMetric => OrthogonalMetric;

        public override bool IsEuclidean => false;

        public override bool IsOrthogonal => true;

        public override bool IsOrthonormal => false;

        public override int VSpaceDimension 
            => BasisVectorsSignatures.Count;

        public override IGaSymMapBilinear ComputedOp { get; }

        public override IGaSymMapBilinear ComputedGp { get; }

        public override IGaSymMapBilinear ComputedSp { get; }

        public override IGaSymMapBilinear ComputedLcp { get; }

        public override IGaSymMapBilinear ComputedRcp { get; }

        public override IGaSymMapBilinear ComputedFdp { get; }

        public override IGaSymMapBilinear ComputedHip { get; }

        public override IGaSymMapBilinear ComputedAcp { get; }

        public override IGaSymMapBilinear ComputedCp { get; }


        internal GaSymFrameOrthogonal(IEnumerable<MathematicaScalar> basisVectorsSignaturesList)
        {
            BasisVectorsSignatures = basisVectorsSignaturesList.ToArray();

            OrthogonalMetric = GaSymMetricOrthogonal.Create(BasisVectorsSignatures);

            Op = ComputedOp = new GaSymOp(VSpaceDimension);
            Gp = ComputedGp = new GaSymOrthogonalGp(OrthogonalMetric);
            Sp = ComputedSp = new GaSymOrthogonalSp(OrthogonalMetric);
            Lcp = ComputedLcp = new GaSymOrthogonalLcp(OrthogonalMetric);
            Rcp = ComputedRcp = new GaSymOrthogonalRcp(OrthogonalMetric);
            Fdp = ComputedFdp = new GaSymOrthogonalFdp(OrthogonalMetric);
            Hip = ComputedHip = new GaSymOrthogonalHip(OrthogonalMetric);
            Acp = ComputedAcp = new GaSymOrthogonalAcp(OrthogonalMetric);
            Cp = ComputedCp = new GaSymOrthogonalCp(OrthogonalMetric);

            UnitPseudoScalarCoef = (
                MaxBasisBladeId.BasisBladeIdHasNegativeReverse() 
                    ? CasConstants.MinusOne 
                    : CasConstants.One
                ) / OrthogonalMetric.GetSignature(MaxBasisBladeId);
        }


        protected override void ComputeIpm()
        {
            var v = MathematicaVector.CreateFullVector(CasInterface, BasisVectorsSignatures);

            InnerProductMatrix = MathematicaMatrix.CreateDiagonal(v);
        }

        public override MathematicaScalar BasisVectorSignature(int basisVectorIndex)
        {
            return BasisVectorsSignatures[basisVectorIndex];
        }

        public override GaSymMultivector BasisBladeSignature(int id)
        {
            return GaSymMultivector.CreateScalar(GaSpaceDimension, OrthogonalMetric[id]);
        }
    }
}
