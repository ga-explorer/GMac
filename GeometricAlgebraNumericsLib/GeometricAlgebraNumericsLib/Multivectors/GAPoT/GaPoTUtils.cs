using System.Diagnostics;
using System.Linq;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories;
using GeometricAlgebraNumericsLib.Rendering.LaTeX;

namespace GeometricAlgebraNumericsLib.Multivectors.GAPoT
{
    //public static class GaPoTUtils
    //{
    //    //public static IEnumerable<GaTerm<GaNumSarMultivector>> GetLoopEGpTerms(this GaPoTMultivector mv1, GaPoTMultivector mv2)
    //    //{
    //    //    foreach (var term1 in mv1.GetNonZeroTerms())
    //    //    {
    //    //        foreach (var term2 in mv2.GetNonZeroTerms())
    //    //        {
    //    //            var value =
    //    //                GaNumFrameUtils.IsNegativeEGp(term1.BasisBladeId, term2.BasisBladeId) 
    //    //                    ? -term1.ScalarValue.EGp(term2.ScalarValue) 
    //    //                    : term1.ScalarValue.EGp(term2.ScalarValue);

    //    //            yield return new GaTerm<GaNumSarMultivector>(
    //    //                term1.BasisBladeId ^ term2.BasisBladeId, 
    //    //                value
    //    //            );
    //    //        }
    //    //    }
    //    //}

    //    //public static GaPoTMultivector EGp(this GaPoTMultivector mv1, GaPoTMultivector mv2)
    //    //{
    //    //    var scalarsDictionary = new Dictionary<int, GaNumSarMultivector>();

    //    //    var termsList = mv1.GetLoopEGpTerms(mv2);
    //    //    foreach (var term in termsList)
    //    //    {
    //    //        if (scalarsDictionary.ContainsKey(term.BasisBladeId))
    //    //            scalarsDictionary[term.BasisBladeId] += term.ScalarValue;
    //    //        else
    //    //            scalarsDictionary.Add(term.BasisBladeId, term.ScalarValue);
    //    //    }

    //    //    return new GaPoTMultivector(
    //    //        mv1.VSpaceDimension, 
    //    //        mv1.ScalarVSpaceDimension, 
    //    //        scalarsDictionary
    //    //    );
    //    //}

    //    public static bool IsValidGaPoTBasisBladeId(this int basisBladeId, int vSpaceDim1, int vSpaceDim2)
    //    {
    //        var vSpaceDim = vSpaceDim1 + vSpaceDim2 + 1;
    //        var gaSpaceDim = vSpaceDim.ToGaSpaceDimension();

    //        if (basisBladeId < 0 || basisBladeId >= gaSpaceDim)
    //            return false;

    //        basisBladeId >>= vSpaceDim1;
    //        var lastBasisVectorId = 1 << vSpaceDim2;

    //        var id = basisBladeId & ~lastBasisVectorId;
    //        var isGradeOdd = (id.BasisBladeGrade() & 1) != 0;
    //        var containsLastBasisVector = (basisBladeId & lastBasisVectorId) != 0;

    //        return isGradeOdd == containsLastBasisVector;
    //    }

    //    public static int GetGaPoTBasisBladeId2(this int basisBladeId, int vSpaceDim1, int vSpaceDim2)
    //    {
    //        Debug.Assert(
    //            basisBladeId.IsValidGaPoTBasisBladeId(vSpaceDim1, vSpaceDim2)
    //        );

    //        return (basisBladeId >> vSpaceDim1) & ~(1 << vSpaceDim2);
    //    }


    //    public static string GetGaPoTMultivectorText(this IGaNumMultivector mv, int vSpaceDim1, int vSpaceDim2)
    //    {
    //        return mv.GetNonZeroTerms().GetPoTTermsListText(vSpaceDim1, vSpaceDim2);
    //    }

    //    public static string GetGaPoTMultivectorLaTeX(this IGaNumMultivector mv, int vSpaceDim1, int vSpaceDim2)
    //    {
    //        var basisVectorNames = 
    //            Enumerable
    //                .Range(1, mv.VSpaceDimension)
    //                .Select(i => i.ToString())
    //                .ToArray();

    //        var latexRenderer = 
    //            new GaNumLaTeXRenderer(GaLaTeXBasisBladeForm.BasisVectorsSubscripts, basisVectorNames);

    //        return latexRenderer.FormatPoTMultivector(mv, vSpaceDim1, vSpaceDim2);
    //    }

    //    public static GaNumSarMultivector ParseGaPoTMultivector(this string sourceText, int vSpaceDim1, int vSpaceDim2)
    //    {
    //        return sourceText
    //            .ParseGaPoTTermsList(vSpaceDim1, vSpaceDim2)
    //            .SumAsSarMultivector(vSpaceDim1 + vSpaceDim2 + 1);
    //    }
    //}
}
