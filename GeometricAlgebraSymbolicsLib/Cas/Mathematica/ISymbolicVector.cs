using System.Collections.Generic;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;

namespace GeometricAlgebraSymbolicsLib.Cas.Mathematica
{
    public interface ISymbolicVector : ISymbolicObject, IEnumerable<MathematicaScalar>
    {
        int Size { get; }

        MathematicaScalar this[int index] { get; }


        bool IsFullVector();

        bool IsSparseVector();

        
        ISymbolicVector Times(ISymbolicMatrix m);


        MathematicaVector ToMathematicaVector();

        MathematicaVector ToMathematicaFullVector();

        MathematicaVector ToMathematicaSparseVector();
    }
}
