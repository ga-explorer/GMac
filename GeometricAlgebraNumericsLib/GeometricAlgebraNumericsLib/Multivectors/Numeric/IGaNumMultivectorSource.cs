using GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories;

namespace GeometricAlgebraNumericsLib.Multivectors.Numeric
{
    public interface IGaNumMultivectorSource : IGaMultivectorSource<double>
    {
        GaNumDarMultivector GetDarMultivector();

        GaNumDgrMultivector GetDgrMultivector();

        GaNumSarMultivector GetSarMultivector();

        GaNumSgrMultivector GetSgrMultivector();


        GaNumDarMultivector GetDarMultivectorCopy();

        GaNumDgrMultivector GetDgrMultivectorCopy();

        GaNumSarMultivector GetSarMultivectorCopy();

        GaNumSgrMultivector GetSgrMultivectorCopy();


        GaNumMultivectorFactory CopyToFactory();
    }
}