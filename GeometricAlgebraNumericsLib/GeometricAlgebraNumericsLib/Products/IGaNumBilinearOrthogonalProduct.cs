﻿using GeometricAlgebraNumericsLib.Maps.Bilinear;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;

namespace GeometricAlgebraNumericsLib.Products
{
    public interface IGaNumBilinearOrthogonalProduct : IGaNumMapBilinear
    {
        GaNumTerm MapToTerm(ulong id1, ulong id2);
    }
}