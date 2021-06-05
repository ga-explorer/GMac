using System;
using System.Collections.Generic;
using GeometricAlgebraSymbolicsLib.Multivectors;
using GeometricAlgebraSymbolicsLib.Multivectors.Intermediate;

namespace GeometricAlgebraSymbolicsLib.Maps.Trilinear
{
    public interface IGaSymMapTrilinear : IGaSymMap
    {
        int DomainVSpaceDimension { get; }

        ulong DomainGaSpaceDimension { get; }

        IGaSymMultivector this[ulong id1, ulong id2, ulong id3] { get; }

        GaSymMultivector this[GaSymMultivector mv1, GaSymMultivector mv2, GaSymMultivector mv3] { get; }

        IGaSymMultivectorTemp MapToTemp(ulong id1, ulong id2, ulong id3);

        IGaSymMultivectorTemp MapToTemp(GaSymMultivector mv1, GaSymMultivector mv2, GaSymMultivector mv3);

        IEnumerable<Tuple<ulong, ulong, ulong, IGaSymMultivector>> BasisBladesMaps();

        IEnumerable<Tuple<ulong, ulong, ulong, IGaSymMultivector>> BasisVectorsMaps();
    }
}