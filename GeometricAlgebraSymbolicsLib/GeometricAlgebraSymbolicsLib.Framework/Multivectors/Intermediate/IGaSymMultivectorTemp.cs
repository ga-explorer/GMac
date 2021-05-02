using GeometricAlgebraSymbolicsLib.Cas.Mathematica.NETLink;

namespace GeometricAlgebraSymbolicsLib.Multivectors.Intermediate
{
    public interface IGaSymMultivectorTemp : IGaSymMultivector
    {
        IGaSymMultivectorTemp AddFactor(ulong id, Expr coef);

        IGaSymMultivectorTemp AddFactor(ulong id, bool isNegative, Expr coef);

        IGaSymMultivectorTemp SetTermCoef(ulong id, Expr coef);

        IGaSymMultivectorTemp SetTermCoef(ulong id, bool isNegative, Expr coef);
    }
}