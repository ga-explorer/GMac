using System;
using System.Collections.Generic;
using GeometricAlgebraSymbolicsLib.Multivectors;
using GeometricAlgebraSymbolicsLib.Multivectors.Intermediate;

namespace GeometricAlgebraSymbolicsLib.Maps.Bilinear
{
    public interface IGaSymMapBilinear : IGaSymMap
    {
        int DomainVSpaceDimension { get; }

        int DomainGaSpaceDimension { get; }

        IGaSymMultivector this[int id1, int id2] { get; }

        GaSymMultivector this[GaSymMultivector mv1, GaSymMultivector mv2] { get; }

        IGaSymMultivectorTemp MapToTemp(int id1, int id2);

        IGaSymMultivectorTemp MapToTemp(GaSymMultivector mv1, GaSymMultivector mv2);

        IEnumerable<Tuple<int, int, IGaSymMultivector>> BasisBladesMaps();

        IEnumerable<Tuple<int, int, IGaSymMultivector>> BasisVectorsMaps();
    }
}