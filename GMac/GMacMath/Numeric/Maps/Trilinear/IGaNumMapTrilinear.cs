using System;
using System.Collections.Generic;
using GMac.GMacMath.Numeric.Multivectors;
using GMac.GMacMath.Numeric.Multivectors.Intermediate;

namespace GMac.GMacMath.Numeric.Maps.Trilinear
{
    public interface IGaNumMapTrilinear
    {
        int DomainVSpaceDimension { get; }

        int DomainGaSpaceDimension { get; }

        IGaNumMultivector this[int id1, int id2, int id3] { get; }

        GaNumMultivector this[GaNumMultivector mv1, GaNumMultivector mv2, GaNumMultivector mv3] { get; }

        IGaNumMultivectorTemp MapToTemp(GaNumMultivector mv1, GaNumMultivector mv2, GaNumMultivector mv3);

        IEnumerable<Tuple<int, int, int, IGaNumMultivector>> BasisBladesMaps();

        IEnumerable<Tuple<int, int, int, IGaNumMultivector>> BasisVectorsMaps();
    }
}