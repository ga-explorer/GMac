using Wolfram.NETLink;

namespace GMac.GMacMath.Symbolic.Multivectors.Intermediate
{
    public interface IGaSymMultivectorTemp : IGaSymMultivector
    {
        IGaSymMultivectorTemp AddFactor(int id, Expr coef);

        IGaSymMultivectorTemp AddFactor(int id, bool isNegative, Expr coef);

        IGaSymMultivectorTemp SetTermCoef(int id, Expr coef);

        IGaSymMultivectorTemp SetTermCoef(int id, bool isNegative, Expr coef);
    }
}