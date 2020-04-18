using System;
using System.Collections.Generic;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraSymbolicsLib.Multivectors;
using GeometricAlgebraSymbolicsLib.Multivectors.Intermediate;

namespace GeometricAlgebraSymbolicsLib.Maps.Trilinear
{
    public abstract class GaSymMapTrilinear : GaSymMap, IGaSymMapTrilinear
    {
        public abstract int DomainVSpaceDimension { get; }

        public int DomainGaSpaceDimension
            => DomainVSpaceDimension.ToGaSpaceDimension();

        public abstract IGaSymMultivector this[int id1, int id2, int id3] { get; }

        public GaSymMultivector this[GaSymMultivector mv1, GaSymMultivector mv2, GaSymMultivector mv3]
            => MapToTemp(mv1, mv2, mv3).ToMultivector();

        public abstract IGaSymMultivectorTemp MapToTemp(int id1, int id2, int id3);

        public abstract IGaSymMultivectorTemp MapToTemp(GaSymMultivector mv1, GaSymMultivector mv2, GaSymMultivector mv3);

        public abstract IEnumerable<Tuple<int, int, int, IGaSymMultivector>> BasisBladesMaps();

        public virtual IEnumerable<Tuple<int, int, int, IGaSymMultivector>> BasisVectorsMaps()
        {
            for (var index1 = 0; index1 < DomainVSpaceDimension; index1++)
            for (var index2 = 0; index2 < DomainVSpaceDimension; index2++)
            for (var index3 = 0; index3 < DomainVSpaceDimension; index3++)
            {
                var id1 = GaNumFrameUtils.BasisBladeId(1, index1);
                var id2 = GaNumFrameUtils.BasisBladeId(1, index2);
                var id3 = GaNumFrameUtils.BasisBladeId(1, index3);
                var mv = this[id1, id2, id3];

                if (!mv.IsNullOrZero())
                    yield return new Tuple<int, int, int, IGaSymMultivector>(index1, index2, index3, mv);
            }
        }
    }
}