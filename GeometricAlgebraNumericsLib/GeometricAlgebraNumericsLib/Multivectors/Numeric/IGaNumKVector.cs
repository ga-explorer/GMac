using System.Collections.Generic;

namespace GeometricAlgebraNumericsLib.Multivectors.Numeric
{
    public interface IGaNumKVector : IGaNumGradedMultivector
    {
        int Grade { get; }

        int KvSpaceDimension { get; }

        IReadOnlyList<double> ScalarValuesArray { get; }

        int GetBasisBladeId(int index);

        int GetBasisBladeIndex(int id);

        GaNumDarKVector ToDarKVector();

        GaNumSarKVector ToSarKVector();
    }
}