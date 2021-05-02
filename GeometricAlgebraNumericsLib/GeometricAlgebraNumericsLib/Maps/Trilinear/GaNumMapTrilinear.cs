using System;
using System.Collections.Generic;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraStructuresLib.Frames;
using GeometricAlgebraStructuresLib.Maps;

namespace GeometricAlgebraNumericsLib.Maps.Trilinear
{
    public abstract class GaNumMapTrilinear : GaMap, IGaNumMapTrilinear
    {
        public abstract int DomainVSpaceDimension { get; }

        public ulong DomainGaSpaceDimension
            => DomainVSpaceDimension.ToGaSpaceDimension();

        public abstract IGaNumMultivector this[ulong id1, ulong id2, ulong id3] { get; }

        public abstract GaNumSarMultivector this[GaNumSarMultivector mv1, GaNumSarMultivector mv2, GaNumSarMultivector mv3] { get; }

        public abstract IEnumerable<Tuple<ulong, ulong, ulong, IGaNumMultivector>> BasisBladesMaps();

        public virtual IEnumerable<Tuple<ulong, ulong, ulong, IGaNumMultivector>> BasisVectorsMaps()
        {
            for (var index1 = 0UL; index1 < (ulong)DomainVSpaceDimension; index1++)
            for (var index2 = 0UL; index2 < (ulong)DomainVSpaceDimension; index2++)
            for (var index3 = 0UL; index3 < (ulong)DomainVSpaceDimension; index3++)
            {
                var id1 = GaFrameUtils.BasisBladeId(1, index1);
                var id2 = GaFrameUtils.BasisBladeId(1, index2);
                var id3 = GaFrameUtils.BasisBladeId(1, index3);
                var mv = this[id1, id2, id3];

                if (!mv.IsNullOrEmpty())
                    yield return new Tuple<ulong, ulong, ulong, IGaNumMultivector>(index1, index2, index3, mv);
            }
        }
    }
}
