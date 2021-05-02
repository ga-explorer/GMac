using System;
using System.Collections.Generic;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraStructuresLib.Maps;

namespace GeometricAlgebraNumericsLib.Maps.Bilinear
{
    public interface IGaNumMapBilinear : IGaMap
    {
        int DomainVSpaceDimension { get; }

        ulong DomainGaSpaceDimension { get; }

        GaNumMapBilinearAssociativity Associativity { get; }

        IGaNumMultivector this[ulong id1, ulong id2] { get; }

        GaNumSarMultivector this[GaNumSarMultivector mv1, GaNumSarMultivector mv2] { get; }

        GaNumDgrMultivector this[GaNumDgrMultivector mv1, GaNumDgrMultivector mv2] { get; }

        GaNumSarMultivector this[params GaNumSarMultivector[] mvList] { get; }

        GaNumDgrMultivector this[params GaNumDgrMultivector[] mvList] { get; }

        IEnumerable<Tuple<ulong, ulong, IGaNumMultivector>> BasisBladesMaps();

        IEnumerable<Tuple<ulong, ulong, IGaNumMultivector>> BasisVectorsMaps();
    }
}
