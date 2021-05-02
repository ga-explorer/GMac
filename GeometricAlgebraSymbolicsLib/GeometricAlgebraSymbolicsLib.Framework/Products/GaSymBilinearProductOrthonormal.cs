using System;
using System.Collections.Generic;
using GeometricAlgebraStructuresLib.Frames;
using GeometricAlgebraSymbolicsLib.Metrics;
using GeometricAlgebraSymbolicsLib.Multivectors;

namespace GeometricAlgebraSymbolicsLib.Products
{
    public abstract class GaSymBilinearProductOrthonormal 
        : GaSymBilinearProduct, IGaSymBilinearOrthogonalProduct
    {
        public GaSymMetricOrthonormal OrthonormalMetric { get; }


        public override IGaSymMetric Metric
            => OrthonormalMetric;

        public override IGaSymMultivector this[ulong id1, ulong id2]
            => MapToTerm(id1, id2);


        protected GaSymBilinearProductOrthonormal(GaSymMetricOrthonormal basisBladesSignatures)
        {
            OrthonormalMetric = basisBladesSignatures;
        }


        public abstract GaSymMultivectorTerm MapToTerm(ulong id1, ulong id2);

        public override IEnumerable<Tuple<ulong, ulong, IGaSymMultivector>> BasisBladesMaps()
        {
            for (var id1 = 0UL; id1 < DomainGaSpaceDimension; id1++)
            for (var id2 = 0UL; id2 < DomainGaSpaceDimension2; id2++)
            {
                var mv = MapToTerm(id1, id2);

                if (!mv.IsNullOrZero())
                    yield return new Tuple<ulong, ulong, IGaSymMultivector>(id1, id2, mv);
            }
        }

        public override IEnumerable<Tuple<ulong, ulong, IGaSymMultivector>> BasisVectorsMaps()
        {
            for (var index1 = 0; index1 < DomainVSpaceDimension; index1++)
            for (var index2 = 0; index2 < DomainVSpaceDimension2; index2++)
            {
                var id1 = GaFrameUtils.BasisBladeId(1, (ulong)index1);
                var id2 = GaFrameUtils.BasisBladeId(1, (ulong)index2);

                var mv = MapToTerm(id1, id2);

                if (!mv.IsNullOrZero())
                    yield return new Tuple<ulong, ulong, IGaSymMultivector>((ulong)index1, (ulong)index2, mv);
            }
        }
    }
}