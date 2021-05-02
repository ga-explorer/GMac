using System.Collections.Generic;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.NETLink;

namespace GeometricAlgebraSymbolicsLib.Multivectors
{
    public interface IGaSymMultivector 
        : IEnumerable<KeyValuePair<ulong, Expr>>
    {
        int VSpaceDimension { get; }

        ulong GaSpaceDimension { get; }

        Expr this[ulong id] { get; }

        Expr this[int grade, ulong index] { get; }

        IEnumerable<ulong> BasisBladeIds { get; }

        IEnumerable<ulong> NonZeroBasisBladeIds { get; }

        IEnumerable<MathematicaScalar> BasisBladeScalars { get; }

        IEnumerable<Expr> BasisBladeExprScalars { get; }

        IEnumerable<MathematicaScalar> NonZeroBasisBladeScalars { get; }

        IEnumerable<Expr> NonZeroBasisBladeExprScalars { get; }

        IEnumerable<KeyValuePair<ulong, MathematicaScalar>> Terms { get; }

        IEnumerable<KeyValuePair<ulong, Expr>> ExprTerms { get; }

        IEnumerable<KeyValuePair<ulong, MathematicaScalar>> NonZeroTerms { get; }

        IEnumerable<KeyValuePair<ulong, Expr>> NonZeroExprTerms { get; }

        bool ContainsBasisBlade(ulong id);

        bool IsTemp { get; }

        ulong TermsCount { get; }

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