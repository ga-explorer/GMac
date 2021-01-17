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

        public int DomainGaSpaceDimension
            => DomainVSpaceDimension.ToGaSpaceDimension();

        public abstract IGaNumMultivector this[int id1, int id2, int id3] { get; }

        public abstract GaNumSarMultivector this[GaNumSarMultivector mv1, GaNumSarMultivector mv2, GaNumSarMultivector mv3] { get; }

        public abstract IEnumerable<Tuple<int, int, int, IGaNumMultivector>> BasisBladesMaps();

        public virtual IEnumerable<Tuple<int, int, int, IGaNumMultivector>> BasisVectorsMaps()
        {
            for (var index1 = 0; index1 < DomainVSpaceDimension; index1++)
            for (var index2 = 0; index2 < DomainVSpaceDimension; index2++)
            for (var index3 = 0; index3 < DomainVSpaceDimension; index3++)
            {
                var id1 = GaFrameUtils.BasisBladeId(1, index1);
                var id2 = GaFrameUtils.BasisBladeId(1, index2);
                var id3 = GaFrameUtils.BasisBladeId(1, index3);
                var mv = this[id1, id2, id3];

                if (!mv.IsNullOrEmpty())
                    yield return new Tuple<int, int, int, IGaNumMultivector>(index1, index2, index3, mv);
            }
        }
    }
}
