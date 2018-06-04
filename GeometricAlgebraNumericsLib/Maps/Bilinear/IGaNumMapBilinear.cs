using System;
using System.Collections.Generic;
using GeometricAlgebraNumericsLib.Multivectors;
using GeometricAlgebraNumericsLib.Multivectors.Intermediate;

namespace GeometricAlgebraNumericsLib.Maps.Bilinear
{
    public interface IGaNumMapBilinear : IGaNumMap
    {
        int DomainVSpaceDimension { get; }

        int DomainGaSpaceDimension { get; }

        IGaNumMultivector this[int id1, int id2] { get; }

        GaNumMultivector this[GaNumMultivector mv1, GaNumMultivector mv2] { get; }

        IGaNumMultivectorTemp MapToTemp(GaNumMultivector mv1, GaNumMultivector mv2);

        IEnumerable<Tuple<int, int, IGaNumMultivector>> BasisBladesMaps();

        IEnumerable<Tuple<int, int, IGaNumMultivector>> BasisVectorsMaps();
    }
}
