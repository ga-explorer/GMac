using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;
using GeometricAlgebraSymbolicsLib.Maps.Bilinear;
using GeometricAlgebraSymbolicsLib.Metrics;
using GeometricAlgebraSymbolicsLib.Multivectors;
using GeometricAlgebraSymbolicsLib.Products;
using GeometricAlgebraSymbolicsLib.Products.Orthonormal;

namespace GeometricAlgebraSymbolicsLib.Frames
{
    public sealed class GaSymFrameOrthonormal : GaSymFrame
    {
        private int[] BasisVectorsSignatures { get; }

        public GaSymMetricOrthonormal OrthonormalMetric { get; }


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

        public override IGaSymMetric Metric => OrthonormalMetric;

        public override GaSymFrame BaseOrthogonalFrame => this;

        public override IGaSymMetricOrthogonal BaseOrthogonalMetric => OrthonormalMetric;

        public override bool IsEuclidean 
            => BasisVectorsSignatures.All(s => s > 0);

        public override bool IsOrthogonal => true;

        public override bool IsOrthonormal => true;

        public override int VSpaceDimension 
            => BasisVectorsSignatures.Length;

        public override IGaSymMapBilinear ComputedOp { get; }

        public override IGaSymMapBilinear ComputedGp { get; }

        public override IGaSymMapBilinear ComputedSp { get; }

        public override IGaSymMapBilinear ComputedLcp { get; }

        public override IGaSymMapBilinear ComputedRcp { get; }

        public override IGaSymMapBilinear ComputedFdp { get; }

        public override IGaSymMapBilinear ComputedHip { get; }

        public override IGaSymMapBilinear ComputedAcp { get; }

        public override IGaSymMapBilinear ComputedCp { get; }


        /// <summary>
        /// frameSigList must have all items equal to 1 (false) or -1 (true) with at least one negative item
        /// </summary>
        /// <param name="frameSigList"></param>
        internal GaSymFrameOrthonormal(IEnumerable<bool> frameSigList)
        {
            BasisVectorsSignatures = frameSigList.Select(b => b ? -1 : 1).ToArray();

            OrthonormalMetric = GaSymMetricOrthonormal.Create(BasisVectorsSignatures);

            Op = ComputedOp = new GaSymOp(VSpaceDimension);
            Gp = ComputedGp = new GaSymOrthonormalGp(OrthonormalMetric);
            Sp = ComputedSp = new GaSymOrthonormalSp(OrthonormalMetric);
            Lcp = ComputedLcp = new GaSymOrthonormalLcp(OrthonormalMetric);
            Rcp = ComputedRcp = new GaSymOrthonormalRcp(OrthonormalMetric);
            Fdp = ComputedFdp = new GaSymOrthonormalFdp(OrthonormalMetric);
            Hip = ComputedHip = new GaSymOrthonormalHip(OrthonormalMetric);
            Acp = ComputedAcp = new GaSymOrthonormalAcp(OrthonormalMetric);
            Cp = ComputedCp = new GaSymOrthonormalCp(OrthonormalMetric);

            UnitPseudoScalarCoef = 
                MathematicaScalar.Create(
                    GaSymbolicsUtils.Cas,
                    MaxBasisBladeId.BasisBladeIdHasNegativeReverse()
                        ? -1.0d / OrthonormalMetric[MaxBasisBladeId]
                        : 1.0d / OrthonormalMetric[MaxBasisBladeId]
                );
        }

        
        protected override void ComputeIpm()
        {
            var bvSig = BasisVectorsSignatures.Select(
                i => MathematicaScalar.Create(CasInterface, i)
                );

            var v = MathematicaVector.CreateFullVector(CasInterface, bvSig);

            InnerProductMatrix = MathematicaMatrix.CreateDiagonal(v);
        }

        public override MathematicaScalar BasisVectorSignature(int basisVectorIndex)
        {
            if (basisVectorIndex >= 0 && basisVectorIndex < VSpaceDimension)
                return BasisVectorsSignatures[basisVectorIndex] < 0
                    ? CasConstants.MinusOne
                    : CasConstants.One;

            throw new IndexOutOfRangeException();
        }

        public override GaSymMultivector BasisBladeSignature(int id)
        {
            if (id < 0 || id >= GaSpaceDimension) 
                throw new IndexOutOfRangeException();

            return GaSymMultivector.CreateScalar(
                GaSpaceDimension,
                OrthonormalMetric[id] < 0
                    ? CasConstants.MinusOne 
                    : CasConstants.One
                );
        }
    }
}
