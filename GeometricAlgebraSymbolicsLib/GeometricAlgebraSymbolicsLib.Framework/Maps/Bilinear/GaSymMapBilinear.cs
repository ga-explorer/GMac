using System;
using System.Collections.Generic;
using GeometricAlgebraStructuresLib.Frames;
using GeometricAlgebraSymbolicsLib.Multivectors;
using GeometricAlgebraSymbolicsLib.Multivectors.Intermediate;

namespace GeometricAlgebraSymbolicsLib.Maps.Bilinear
{
    public abstract class GaSymMapBilinear : GaSymMap, IGaSymMapBilinear
    {
        public abstract int DomainVSpaceDimension { get; }

        public ulong DomainGaSpaceDimension
            => DomainVSpaceDimension.ToGaSpaceDimension();

        public abstract IGaSymMultivector this[ulong id1, ulong id2] { get; }

        public virtual GaSymMultivector this[GaSymMultivector mv1, GaSymMultivector mv2]
            => MapToTemp(mv1, mv2).ToMultivector();

        public abstract IGaSymMultivectorTemp MapToTemp(ulong id1, ulong id2);

        public abstract IGaSymMultivectorTemp MapToTemp(GaSymMultivector mv1, GaSymMultivector mv2);

        public abstract IEnumerable<Tuple<ulong, ulong, IGaSymMultivector>> BasisBladesMaps();

        public virtual IEnumerable<Tuple<ulong, ulong, IGaSymMultivector>> BasisVectorsMaps()
        {
            for (var index1 = 0; index1 < DomainVSpaceDimension; index1++)
            for (var index2 = 0; index2 < DomainVSpaceDimension; index2++)
            {
                var id1 = GaFrameUtils.BasisBladeId(1, (ulong)index1);
                var id2 = GaFrameUtils.BasisBladeId(1, (ulong)index2);
                var mv = this[id1, id2];

                if (!mv.IsNullOrZero())
                    yield return new Tuple<ulong, ulong, IGaSymMultivector>((ulong)index1, (ulong)index2, mv);
            }
        }
    }
}
