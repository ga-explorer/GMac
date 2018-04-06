using System;
using System.Collections.Generic;
using GMac.GMacMath.Numeric.Multivectors;
using GMac.GMacMath.Numeric.Multivectors.Intermediate;

namespace GMac.GMacMath.Numeric.Maps.Unilinear
{
    public interface IGaNumMapUnilinear : IGaNumMap
    {
        int DomainVSpaceDimension { get; }

        int DomainGaSpaceDimension { get; }

        IGaNumMultivector this[int id1] { get; }

        GaNumMultivector this[GaNumMultivector mv1] { get; }

        IGaNumMultivectorTemp MapToTemp(GaNumMultivector mv1);

        IEnumerable<Tuple<int, IGaNumMultivector>> BasisBladeMaps();

        IEnumerable<Tuple<int, IGaNumMultivector>> BasisVectorMaps();
    }
}