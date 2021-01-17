using System;
using System.Collections.Generic;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;

namespace GeometricAlgebraNumericsLib.Maps.Unilinear
{
    public interface IGaNumMapUnilinear : IGaMap
    {
        int DomainVSpaceDimension { get; }

        int DomainGaSpaceDimension { get; }

        IGaNumMultivector DomainPseudoScalarMap { get; }

        IGaNumMultivector this[int id1] { get; }

        GaNumSarMultivector this[GaNumSarMultivector mv1] { get; }

        GaNumDgrMultivector this[GaNumDgrMultivector mv1] { get; }

        IEnumerable<Tuple<int, IGaNumMultivector>> BasisBladeMaps();

        IEnumerable<Tuple<int, IGaNumMultivector>> BasisVectorMaps();
    }
}