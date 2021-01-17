using System;
using System.Collections.Generic;
using GeometricAlgebraNumericsLib.Maps;
using GeometricAlgebraStructuresLib.Multivectors;

namespace GeometricAlgebraStructuresLib.Maps
{
    public interface IGaMapBilinear<T> : IGaMap
    {
        int DomainVSpaceDimension { get; }

        int DomainGaSpaceDimension { get; }

        GaMapBilinearAssociativity Associativity { get; }

        IGaMultivector<T> this[int id1, int id2] { get; }

        IGaMultivector<T> this[IGaMultivector<T> mv1, IGaMultivector<T> mv2] { get; }

        IGaMultivector<T> this[params IGaMultivector<T>[] mvList] { get; }

        IEnumerable<Tuple<int, int, IGaMultivector<T>>> BasisBladesMaps();

        IEnumerable<Tuple<int, int, IGaMultivector<T>>> BasisVectorsMaps();
    }
}
