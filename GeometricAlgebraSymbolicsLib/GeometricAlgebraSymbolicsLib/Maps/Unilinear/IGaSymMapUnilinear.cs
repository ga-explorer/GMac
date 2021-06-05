using System;
using System.Collections.Generic;
using GeometricAlgebraSymbolicsLib.Multivectors;
using GeometricAlgebraSymbolicsLib.Multivectors.Intermediate;

namespace GeometricAlgebraSymbolicsLib.Maps.Unilinear
{
    public interface IGaSymMapUnilinear : IGaSymMap
    {
        int DomainVSpaceDimension { get; }

        ulong DomainGaSpaceDimension { get; }

        IGaSymMultivector this[int grade1, ulong index1] { get; }

        IGaSymMultivector this[ulong id1] { get; }

        GaSymMultivector this[GaSymMultivector mv1] { get; }

        IGaSymMultivectorTemp MapToTemp(ulong id1);

        IGaSymMultivectorTemp MapToTemp(GaSymMultivector mv1);

        IEnumerable<Tuple<ulong, IGaSymMultivector>> BasisBladeMaps();

        IEnumerable<Tuple<ulong, IGaSymMultivector>> BasisVectorMaps();
    }
}