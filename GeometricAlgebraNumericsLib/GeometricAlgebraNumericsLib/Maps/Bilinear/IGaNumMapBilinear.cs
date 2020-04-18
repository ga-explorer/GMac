using System;
using System.Collections.Generic;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;

namespace GeometricAlgebraNumericsLib.Maps.Bilinear
{
    public interface IGaNumMapBilinear : IGaNumMap
    {
        int DomainVSpaceDimension { get; }

        int DomainGaSpaceDimension { get; }

        GaNumMapBilinearAssociativity Associativity { get; }

        IGaNumMultivector this[int id1, int id2] { get; }

        GaNumSarMultivector this[GaNumSarMultivector mv1, GaNumSarMultivector mv2] { get; }

        GaNumDgrMultivector this[GaNumDgrMultivector mv1, GaNumDgrMultivector mv2] { get; }

        GaNumSarMultivector this[params GaNumSarMultivector[] mvList] { get; }

        GaNumDgrMultivector this[params GaNumDgrMultivector[] mvList] { get; }

        IEnumerable<Tuple<int, int, IGaNumMultivector>> BasisBladesMaps();

        IEnumerable<Tuple<int, int, IGaNumMultivector>> BasisVectorsMaps();
    }
}
