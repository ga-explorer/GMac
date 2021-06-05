using System;
using System.Collections.Generic;
using GeometricAlgebraSymbolicsLib.Multivectors;
using GeometricAlgebraSymbolicsLib.Multivectors.Intermediate;

namespace GeometricAlgebraSymbolicsLib.Maps.Bilinear
{
    public interface IGaSymMapBilinear : IGaSymMap
    {
        int DomainVSpaceDimension { get; }

        ulong DomainGaSpaceDimension { get; }

        IGaSymMultivector this[ulong id1, ulong id2] { get; }

        GaSymMultivector this[GaSymMultivector mv1, GaSymMultivector mv2] { get; }

        IGaSymMultivectorTemp MapToTemp(ulong id1, ulong id2);

        IGaSymMultivectorTemp MapToTemp(GaSymMultivector mv1, GaSymMultivector mv2);

        IEnumerable<Tuple<ulong, ulong, IGaSymMultivector>> BasisBladesMaps();

        IEnumerable<Tuple<ulong, ulong, IGaSymMultivector>> BasisVectorsMaps();
    }
}