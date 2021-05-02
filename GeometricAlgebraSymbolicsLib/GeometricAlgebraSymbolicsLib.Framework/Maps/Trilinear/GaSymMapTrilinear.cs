﻿using System;
using System.Collections.Generic;
using GeometricAlgebraStructuresLib.Frames;
using GeometricAlgebraSymbolicsLib.Multivectors;
using GeometricAlgebraSymbolicsLib.Multivectors.Intermediate;

namespace GeometricAlgebraSymbolicsLib.Maps.Trilinear
{
    public abstract class GaSymMapTrilinear : GaSymMap, IGaSymMapTrilinear
    {
        public abstract int DomainVSpaceDimension { get; }

        public ulong DomainGaSpaceDimension
            => DomainVSpaceDimension.ToGaSpaceDimension();

        public abstract IGaSymMultivector this[ulong id1, ulong id2, ulong id3] { get; }

        public GaSymMultivector this[GaSymMultivector mv1, GaSymMultivector mv2, GaSymMultivector mv3]
            => MapToTemp(mv1, mv2, mv3).ToMultivector();

        public abstract IGaSymMultivectorTemp MapToTemp(ulong id1, ulong id2, ulong id3);

        public abstract IGaSymMultivectorTemp MapToTemp(GaSymMultivector mv1, GaSymMultivector mv2, GaSymMultivector mv3);

        public abstract IEnumerable<Tuple<ulong, ulong, ulong, IGaSymMultivector>> BasisBladesMaps();

        public virtual IEnumerable<Tuple<ulong, ulong, ulong, IGaSymMultivector>> BasisVectorsMaps()
        {
            for (var index1 = 0; index1 < DomainVSpaceDimension; index1++)
            for (var index2 = 0; index2 < DomainVSpaceDimension; index2++)
            for (var index3 = 0; index3 < DomainVSpaceDimension; index3++)
            {
                var id1 = GaFrameUtils.BasisBladeId(1, (ulong)index1);
                var id2 = GaFrameUtils.BasisBladeId(1, (ulong)index2);
                var id3 = GaFrameUtils.BasisBladeId(1, (ulong)index3);
                var mv = this[id1, id2, id3];

                if (!mv.IsNullOrZero())
                    yield return new Tuple<ulong, ulong, ulong, IGaSymMultivector>((ulong)index1, (ulong)index2, (ulong)index3, mv);
            }
        }
    }
}