using System;
using System.Collections.Generic;
using GeometricAlgebraNumericsLib.Multivectors;
using GeometricAlgebraNumericsLib.Multivectors.Intermediate;

namespace GeometricAlgebraNumericsLib.Maps.Trilinear
{
    public interface IGaNumMapTrilinear
    {
        int DomainVSpaceDimension { get; }

        int DomainGaSpaceDimension { get; }

        IGaNumMultivector this[int id1, int id2, int id3] { get; }

        GaNumMultivector this[GaNumMultivector mv1, GaNumMultivector mv2, GaNumMultivector mv3] { get; }

        IGaNumMultivectorTemp MapToTemp(GaNumMultivector mv1, GaNumMultivector mv2, GaNumMultivector mv3);

        IEnumerable<Tuple<int, int, int, IGaNumMultivector>> BasisBladesMaps();

        IEnumerable<Tuple<int, int, int, IGaNumMultivector>> BasisVectorsMaps();
    }
}