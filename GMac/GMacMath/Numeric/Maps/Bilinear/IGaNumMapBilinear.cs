using System;
using System.Collections.Generic;
using GMac.GMacMath.Numeric.Multivectors;
using GMac.GMacMath.Numeric.Multivectors.Intermediate;

namespace GMac.GMacMath.Numeric.Maps.Bilinear
{
    public interface IGaNumMapBilinear : IGaNumMap
    {
        int DomainVSpaceDimension { get; }

        int DomainGaSpaceDimension { get; }

        IGaNumMultivector this[int id1, int id2] { get; }

        GaNumMultivector this[GaNumMultivector mv1, GaNumMultivector mv2] { get; }

        IGaNumMultivectorTemp MapToTemp(GaNumMultivector mv1, GaNumMultivector mv2);

        IEnumerable<Tuple<int, int, IGaNumMultivector>> BasisBladesMaps();

        IEnumerable<Tuple<int, int, IGaNumMultivector>> BasisVectorsMaps();
    }
}
