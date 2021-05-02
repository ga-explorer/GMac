using System.Collections.Generic;

namespace GeometricAlgebraNumericsLib.Multivectors.Numeric
{
    public interface IGaNumKVector : IGaNumGradedMultivector
    {
        int Grade { get; }

        ulong KvSpaceDimension { get; }

        IReadOnlyList<double> ScalarValuesArray { get; }

        ulong GetBasisBladeId(ulong index);

        ulong GetBasisBladeIndex(ulong id);

        GaNumDarKVector ToDarKVector();

        GaNumSarKVector ToSarKVector();
    }
}