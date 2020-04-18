using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CodeComposerLib.LaTeX;
using DataStructuresLib;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Multivectors;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using TextComposerLib.Text;

namespace GeometricAlgebraNumericsLib.Rendering.LaTeX
{
    public class GaNumLaTeXRenderer : LaTeXRenderer, IGaFrame
    {
        private readonly string[] _basisNames;


        public string BasisVectorPrefix { get; set; } 
            = @"\mathbf{e_{}}";

        public GaLaTeXBasisBladeForm BasisBladeForm { get; }

        public int VSpaceDimension { get; }

        public int GaSpaceDimension 
            => VSpaceDimension.ToGaSpaceDimension();

        public int MaxBasisBladeId 
            => GaSpaceDimension - 1;

        public int GradesCount 
            => VSpaceDimension + 1;


        public GaNumLaTeXRenderer(params string[] basisNames)
        {
            BasisBladeForm = GaLaTeXBasisBladeForm.BasisVectorsOuterProduct;
            _basisNames = basisNames;

            if (!_basisNames.Length.IsValidVSpaceDimension())
                throw new InvalidOperationException("Invalid number of basis vectors");

            VSpaceDimension = _basisNames.Length;
        }

        public GaNumLaTeXRenderer(GaLaTeXBasisBladeForm basisBladeForm, params string[] basisNames)
        {
            BasisBladeForm = basisBladeForm;
            _basisNames = basisNames;

            if (BasisBladeForm == GaLaTeXBasisBladeForm.BasisBlade)
            {
                if (!_basisNames.Length.IsValidGaSpaceDimension())
                    throw new InvalidOperationException("Invalid number of basis blades");

                VSpaceDimension = GaSpaceDimension.ToVSpaceDimension();
            }
            else
            {
                if (!_basisNames.Length.IsValidVSpaceDimension())
                    throw new InvalidOperationException("Invalid number of basis vectors");

                VSpaceDimension = _basisNames.Length;
            }
        }


        private string FormatBasisBlade(int basisBladeId, IReadOnlyList<string> basisNamesArray, GaLaTeXBasisBladeForm basisBladeForm)
        {
            if (!this.IsValidBasisBladeId(basisBladeId))
                return "Invalid basis blade id " + basisBladeId;

            if (basisBladeForm == GaLaTeXBasisBladeForm.BasisBlade)
                return basisNamesArray[basisBladeId];

            if (basisBladeId == 0)
                return @"\mathfrak{1}";

            var basisVectors =
                basisNamesArray.PickUsingPattern(basisBladeId).ToArray();

            if (basisBladeForm == GaLaTeXBasisBladeForm.BasisVectorsGeometricProduct)
                return basisVectors.Concatenate();

            if (basisBladeForm == GaLaTeXBasisBladeForm.BasisVectorsOuterProduct)
                return basisVectors.Concatenate(@" \wedge ");

            return BasisVectorPrefix.Replace(
                "{}", 
                basisVectors.Concatenate(",", "{", "}")
            );
        }

        public string FormatBasisVectors()
        {
            return Enumerable.Range(0, VSpaceDimension - 1)
                .Select(i => FormatBasisBlade(1 << i))
                .Concatenate(
                    ",",
                    @"\left\langle ",
                    @" \right\rangle"
                );
        }

        public string FormatBasisBlade(int basisBladeId)
        {
            return FormatBasisBlade(basisBladeId, _basisNames, BasisBladeForm);
        }

        public string FormatNumMultivector(IGaNumMultivector mv)
        {
            var composer = new List<string>();

            var termsList = mv
                .GetNonZeroTerms()
                .OrderBy(t => t.GetHashCode())
                .ThenBy(t => t.BasisBladeIndex);

            foreach (var term in termsList)
            {
                var basisBlade = FormatBasisBlade(term.BasisBladeId);
                var scalar = term.ScalarValue.ToString("G");

                composer.Add(@" \left( " + scalar + @" \right) " + basisBlade);
            }

            return composer.Concatenate(" + ");
        }

        public string FormatNumTerms(IEnumerable<GaTerm<double>> termsList)
        {
            var composer = new List<string>();

            foreach (var term in termsList)
            {
                var basisBlade = FormatBasisBlade(term.BasisBladeId);
                var scalar = term.ScalarValue.ToString("G");

                composer.Add(@" \left( " + scalar + @" \right) " + basisBlade);
            }

            return composer.Concatenate(" + ");
        }

        public string FormatPoTMultivector(IGaNumMultivector mv, int vSpaceDim1, int vSpaceDim2)
        {
            var mask1 = (1 << vSpaceDim1) - 1;
            var mask2 = ~mask1;

            var gaPoTBasisVectorNames = "abcdefghijklmnopqrstuvwxyz"
                .Take(VSpaceDimension - vSpaceDim1)
                .Select(c => c.ToString())
                .ToArray();

            var termsDictionary =
                mv.GetStoredTerms()
                    .GroupBy(t => (t.BasisBladeId & mask2) >> vSpaceDim1)
                    .OrderBy(g => g.Key.BasisBladeGrade())
                    .ThenBy(g => g.Key.BasisBladeIndex())
                    .ToDictionary(
                        g => g.Key, 
                        g => g.ToArray()
                    );

            var composer = new List<string>();

            foreach (var pair in termsDictionary)
            {
                var gaPoTBasisBlade = FormatBasisBlade(
                    pair.Key,
                    gaPoTBasisVectorNames,
                    GaLaTeXBasisBladeForm.BasisVectorsSubscripts
                );

                var gaPoTScalar = FormatNumTerms(
                    pair
                        .Value
                        .Select(t => 
                            new GaTerm<double>(t.BasisBladeId & mask1, t.ScalarValue)
                        )
                        .OrderBy(t => t.BasisBladeGrade)
                        .ThenBy(t => t.BasisBladeIndex)
                );

                composer.Add(@" \left[ " + gaPoTScalar + @" \right] " + gaPoTBasisBlade);
            }

            return composer.Concatenate(" + ");
        }


        public Image RenderBasisVectors()
        {
            return RenderMathToImage(FormatBasisVectors());
        } 

        public Image RenderBasisBlade(int basisBladeId)
        {
            return RenderMathToImage(FormatBasisBlade(basisBladeId));
        } 

        public Image RenderMultivector(GaNumSarMultivector mv)
        {
            return RenderMathToImage(FormatNumMultivector(mv));
        } 
    }
}