using System.Collections.Generic;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;
using Wolfram.NETLink;

namespace GeometricAlgebraSymbolicsLib.Multivectors
{
    public interface IGaSymMultivector : IEnumerable<KeyValuePair<int, Expr>>
    {
        int VSpaceDimension { get; }

        int GaSpaceDimension { get; }

        Expr this[int id] { get; }

        Expr this[int grade, int index] { get; }

        IEnumerable<int> BasisBladeIds { get; }

        IEnumerable<int> NonZeroBasisBladeIds { get; }

        IEnumerable<MathematicaScalar> BasisBladeScalars { get; }

        IEnumerable<Expr> BasisBladeExprScalars { get; }

        IEnumerable<MathematicaScalar> NonZeroBasisBladeScalars { get; }

        IEnumerable<Expr> NonZeroBasisBladeExprScalars { get; }

        IEnumerable<KeyValuePair<int, MathematicaScalar>> Terms { get; }

        IEnumerable<KeyValuePair<int, Expr>> ExprTerms { get; }

        IEnumerable<KeyValuePair<int, MathematicaScalar>> NonZeroTerms { get; }

        IEnumerable<KeyValuePair<int, Expr>> NonZeroExprTerms { get; }

        bool ContainsBasisBlade(int id);

        bool IsTemp { get; }

        int TermsCount { get; }

        bool IsTerm();

        bool IsScalar();

        bool IsZero();

        bool IsEqualZero();

        void Simplify();

        MathematicaScalar[] TermsToArray();

        Expr[] TermsToExprArray();

        GaSymMultivector ToMultivector();

        GaSymMultivector GetVectorPart();
    }
}