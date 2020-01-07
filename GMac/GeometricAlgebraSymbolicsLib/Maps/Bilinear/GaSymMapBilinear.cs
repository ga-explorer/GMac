using System;
using System.Collections.Generic;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraSymbolicsLib.Multivectors;
using GeometricAlgebraSymbolicsLib.Multivectors.Intermediate;

namespace GeometricAlgebraSymbolicsLib.Maps.Bilinear
{
    public abstract class GaSymMapBilinear : GaSymMap, IGaSymMapBilinear
    {
        public abstract int DomainVSpaceDimension { get; }

        public int DomainGaSpaceDimension
            => DomainVSpaceDimension.ToGaSpaceDimension();

        public abstract IGaSymMultivector this[int id1, int id2] { get; }

        public virtual GaSymMultivector this[GaSymMultivector mv1, GaSymMultivector mv2]
            => MapToTemp(mv1, mv2).ToMultivector();

        public abstract IGaSymMultivectorTemp MapToTemp(int id1, int id2);

        public abstract IGaSymMultivectorTemp MapToTemp(GaSymMultivector mv1, GaSymMultivector mv2);

        public abstract IEnumerable<Tuple<int, int, IGaSymMultivector>> BasisBladesMaps();

        public virtual IEnumerable<Tuple<int, int, IGaSymMultivector>> BasisVectorsMaps()
        {
            for (var index1 = 0; index1 < DomainVSpaceDimension; index1++)
            for (var index2 = 0; index2 < DomainVSpaceDimension; index2++)
            {
                var id1 = GaNumFrameUtils.BasisBladeId(1, index1);
                var id2 = GaNumFrameUtils.BasisBladeId(1, index2);
                var mv = this[id1, id2];

                if (!mv.IsNullOrZero())
                    yield return new Tuple<int, int, IGaSymMultivector>(index1, index2, mv);
            }
        }
    }
}
