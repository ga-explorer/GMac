using System;
using System.Collections.Generic;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Multivectors;
using GeometricAlgebraNumericsLib.Multivectors.Intermediate;

namespace GeometricAlgebraNumericsLib.Maps.Bilinear
{
    public abstract class GaNumMapBilinear : GaNumMap, IGaNumMapBilinear
    {
        public abstract int DomainVSpaceDimension { get; }

        public int DomainGaSpaceDimension
            => DomainVSpaceDimension.ToGaSpaceDimension();

        public abstract IGaNumMultivector this[int id1, int id2] { get; }

        public virtual GaNumMultivector this[GaNumMultivector mv1, GaNumMultivector mv2]
            => MapToTemp(mv1, mv2).ToMultivector();

        public abstract IGaNumMultivectorTemp MapToTemp(GaNumMultivector mv1, GaNumMultivector mv2);

        public abstract IEnumerable<Tuple<int, int, IGaNumMultivector>> BasisBladesMaps();

        public virtual IEnumerable<Tuple<int, int, IGaNumMultivector>> BasisVectorsMaps()
        {
            for (var index1 = 0; index1 < DomainVSpaceDimension; index1++)
            for (var index2 = 0; index2 < DomainVSpaceDimension; index2++)
            {
                var id1 = GaNumFrameUtils.BasisBladeId(1, index1);
                var id2 = GaNumFrameUtils.BasisBladeId(1, index2);
                var mv = this[id1, id2];

                if (!mv.IsNullOrZero())
                    yield return new Tuple<int, int, IGaNumMultivector>(index1, index2, mv);
            }
        }
    }
}
