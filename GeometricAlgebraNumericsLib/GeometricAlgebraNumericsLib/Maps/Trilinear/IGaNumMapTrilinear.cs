using System;
using System.Collections.Generic;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;

namespace GeometricAlgebraNumericsLib.Maps.Trilinear
{
    public interface IGaNumMapTrilinear
    {
        int DomainVSpaceDimension { get; }

        ulong DomainGaSpaceDimension { get; }

        IGaNumMultivector this[ulong id1, ulong id2, ulong id3] { get; }

        GaNumSarMultivector this[GaNumSarMultivector mv1, GaNumSarMultivector mv2, GaNumSarMultivector mv3] { get; }

        IEnumerable<Tuple<ulong, ulong, ulong, IGaNumMultivector>> BasisBladesMaps();

        IEnumerable<Tuple<ulong, ulong, ulong, IGaNumMultivector>> BasisVectorsMaps();
    }
}