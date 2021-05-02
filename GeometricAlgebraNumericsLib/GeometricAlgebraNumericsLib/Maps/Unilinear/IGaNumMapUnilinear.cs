using System;
using System.Collections.Generic;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraStructuresLib.Maps;

namespace GeometricAlgebraNumericsLib.Maps.Unilinear
{
    public interface IGaNumMapUnilinear : IGaMap
    {
        int DomainVSpaceDimension { get; }

        ulong DomainGaSpaceDimension { get; }

        IGaNumMultivector DomainPseudoScalarMap { get; }

        IGaNumMultivector this[ulong id1] { get; }

        GaNumSarMultivector this[GaNumSarMultivector mv1] { get; }

        GaNumDgrMultivector this[GaNumDgrMultivector mv1] { get; }

        IEnumerable<Tuple<ulong, IGaNumMultivector>> BasisBladeMaps();

        IEnumerable<Tuple<ulong, IGaNumMultivector>> BasisVectorMaps();
    }
}