using System;
using System.Collections.Generic;
using GeometricAlgebraNumericsLib.Multivectors;

namespace GeometricAlgebraNumericsLib.Maps.Bilinear
{
    public interface IGaNumMapBilinear : IGaNumMap
    {
        int DomainVSpaceDimension { get; }

        int DomainGaSpaceDimension { get; }

        GaNumMapBilinearAssociativity Associativity { get; }

        IGaNumMultivector this[int id1, int id2] { get; }

        GaNumMultivector this[GaNumMultivector mv1, GaNumMultivector mv2] { get; }

        GaNumMultivector this[params GaNumMultivector[] mvList] { get; }

        IEnumerable<Tuple<int, int, IGaNumMultivector>> BasisBladesMaps();

        IEnumerable<Tuple<int, int, IGaNumMultivector>> BasisVectorsMaps();
    }
}
