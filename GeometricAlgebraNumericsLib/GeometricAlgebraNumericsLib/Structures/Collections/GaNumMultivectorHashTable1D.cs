﻿using GeometricAlgebraNumericsLib.Multivectors.Numeric;

namespace GeometricAlgebraNumericsLib.Structures.Collections
{
    public class GaNumMultivectorHashTable1D 
        : GaSparseTable1D<ulong, IGaNumMultivector>
    {
        public override bool IsDefaultValue(IGaNumMultivector value)
        {
            return value.IsNullOrZero();
        }
    }
}
