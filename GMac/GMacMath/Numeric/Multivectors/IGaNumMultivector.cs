using System.Collections.Generic;

namespace GMac.GMacMath.Numeric.Multivectors
{
    public interface IGaNumMultivector : IEnumerable<KeyValuePair<int, double>>
    {
        int VSpaceDimension { get; }

        int GaSpaceDimension { get; }

        double this[int id] { get; }

        double this[int grade, int index] { get; }

        IEnumerable<int> BasisBladeIds { get; }

        IEnumerable<int> NonZeroBasisBladeIds { get; }

        IEnumerable<double> BasisBladeScalars { get; }

        IEnumerable<double> NonZeroBasisBladeScalars { get; }

        IEnumerable<KeyValuePair<int, double>> Terms { get; }

        IEnumerable<KeyValuePair<int, double>> NonZeroTerms { get; }

        bool IsTemp { get; }

        int TermsCount { get; }

        bool IsTerm();

        bool IsScalar();

        bool IsZero();

        bool IsNearZero(double epsilon);

        bool ContainsBasisBlade(int id);

        void Simplify();

        double[] TermsToArray();

        GaNumMultivector ToMultivector();

        GaNumMultivector GetVectorPart();
    }
}