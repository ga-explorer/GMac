using System;
using System.Collections.Generic;
using GeometricAlgebraSymbolicsLib.Multivectors;
using GeometricAlgebraSymbolicsLib.Multivectors.Intermediate;

namespace GeometricAlgebraSymbolicsLib.Maps.Unilinear
{
    public interface IGaSymMapUnilinear : IGaSymMap
    {
        int DomainVSpaceDimension { get; }

        int DomainGaSpaceDimension { get; }

        IGaSymMultivector this[int grade1, int index1] { get; }

        IGaSymMultivector this[int id1] { get; }

        GaSymMultivector this[GaSymMultivector mv1] { get; }

        IGaSymMultivectorTemp MapToTemp(int id1);

        IGaSymMultivectorTemp MapToTemp(GaSymMultivector mv1);

        IEnumerable<Tuple<int, IGaSymMultivector>> BasisBladeMaps();

        IEnumerable<Tuple<int, IGaSymMultivector>> BasisVectorMaps();
    }
}