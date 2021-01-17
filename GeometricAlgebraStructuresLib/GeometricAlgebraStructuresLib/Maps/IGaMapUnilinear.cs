using System;
using System.Collections.Generic;
using GeometricAlgebraNumericsLib.Maps;
using GeometricAlgebraStructuresLib.Multivectors;

namespace GeometricAlgebraStructuresLib.Maps
{
    public interface IGaMapUnilinear<T> : IGaMap
    {
        int DomainVSpaceDimension { get; }

        int DomainGaSpaceDimension { get; }

        IGaMultivector<T> DomainPseudoScalarMap { get; }

        IGaMultivector<T> this[int id1] { get; }

        IGaMultivector<T> this[IGaMultivector<T> mv1] { get; }

        IEnumerable<Tuple<int, IGaMultivector<T>>> BasisBladeMaps();

        IEnumerable<Tuple<int, IGaMultivector<T>>> BasisVectorMaps();
    }
}