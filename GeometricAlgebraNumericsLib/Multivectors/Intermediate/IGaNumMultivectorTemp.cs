namespace GeometricAlgebraNumericsLib.Multivectors.Intermediate
{
    public interface IGaNumMultivectorTemp : IGaNumMultivector
    {
        IGaNumMultivectorTemp AddFactor(int id, double coef);

        IGaNumMultivectorTemp AddFactor(int id, bool isNegative, double coef);

        IGaNumMultivectorTemp SetTermCoef(int id, double coef);

        IGaNumMultivectorTemp SetTermCoef(int id, bool isNegative, double coef);
    }
}