using System;
using System.Collections.Generic;
using GeometricAlgebraStructuresLib.Multivectors;

namespace GeometricAlgebraStructuresLib.Maps
{
    public interface IGaMapTrilinear<T>
    {
        int DomainVSpaceDimension { get; }

        int DomainGaSpaceDimension { get; }

        IGaMultivector<T> this[int id1, int id2, int id3] { get; }

        IGaMultivector<T> this[IGaMultivector<T> mv1, IGaMultivector<T> mv2, IGaMultivector<T> mv3] { get; }

        IEnumerable<Tuple<int, int, int, IGaMultivector<T>>> BasisBladesMaps();

        IEnumerable<Tuple<int, int, int, IGaMultivector<T>>> BasisVectorsMaps();
    }
}