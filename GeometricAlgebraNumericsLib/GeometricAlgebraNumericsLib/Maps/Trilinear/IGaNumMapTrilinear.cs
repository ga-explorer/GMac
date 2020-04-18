using System;
using System.Collections.Generic;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;

namespace GeometricAlgebraNumericsLib.Maps.Trilinear
{
    public interface IGaNumMapTrilinear
    {
        int DomainVSpaceDimension { get; }

        int DomainGaSpaceDimension { get; }

        IGaNumMultivector this[int id1, int id2, int id3] { get; }

        GaNumSarMultivector this[GaNumSarMultivector mv1, GaNumSarMultivector mv2, GaNumSarMultivector mv3] { get; }

        IEnumerable<Tuple<int, int, int, IGaNumMultivector>> BasisBladesMaps();

        IEnumerable<Tuple<int, int, int, IGaNumMultivector>> BasisVectorsMaps();
    }
}