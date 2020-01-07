using System;
using System.Collections.Generic;
using GeometricAlgebraNumericsLib.Multivectors;

namespace GeometricAlgebraNumericsLib.Maps.Unilinear
{
    public interface IGaNumMapUnilinear : IGaNumMap
    {
        int DomainVSpaceDimension { get; }

        int DomainGaSpaceDimension { get; }

        IGaNumMultivector DomainPseudoScalarMap { get; }

        IGaNumMultivector this[int id1] { get; }

        GaNumMultivector this[GaNumMultivector mv1] { get; }

        IEnumerable<Tuple<int, IGaNumMultivector>> BasisBladeMaps();

        IEnumerable<Tuple<int, IGaNumMultivector>> BasisVectorMaps();
    }
}