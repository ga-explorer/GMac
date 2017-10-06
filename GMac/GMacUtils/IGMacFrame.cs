﻿namespace GMac.GMacUtils
{
    public interface IGMacFrame
    {
        int VSpaceDimension { get; }

        int GaSpaceDimension { get; }

        int MaxBasisBladeId { get; }

        int GradesCount { get; }
    }
}
