using System;
using System.Collections.Generic;
using GMac.GMacMath.Symbolic.Multivectors;
using GMac.GMacMath.Symbolic.Multivectors.Intermediate;

namespace GMac.GMacMath.Symbolic.Maps.Trilinear
{
    public interface IGaSymMapTrilinear : IGaSymMap
    {
        int DomainVSpaceDimension { get; }

        int DomainGaSpaceDimension { get; }

        IGaSymMultivector this[int id1, int id2, int id3] { get; }

        GaSymMultivector this[GaSymMultivector mv1, GaSymMultivector mv2, GaSymMultivector mv3] { get; }

        IGaSymMultivectorTemp MapToTemp(int id1, int id2, int id3);

        IGaSymMultivectorTemp MapToTemp(GaSymMultivector mv1, GaSymMultivector mv2, GaSymMultivector mv3);

        IEnumerable<Tuple<int, int, int, IGaSymMultivector>> BasisBladesMaps();

        IEnumerable<Tuple<int, int, int, IGaSymMultivector>> BasisVectorsMaps();
    }
}