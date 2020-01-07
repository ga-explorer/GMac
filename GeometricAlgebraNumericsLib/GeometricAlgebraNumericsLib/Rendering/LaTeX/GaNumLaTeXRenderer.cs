using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CodeComposerLib.LaTeX;
using DataStructuresLib;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Multivectors;
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

        public int GaSpaceDimension { get; }

        public int MaxBasisBladeId 
            => GaSpaceDimension - 1;

        public int GradesCount 
            => VSpaceDimension + 1;


        public GaNumLaTeXRenderer(params string[] basisNames)
        {
            BasisBladeForm = GaLaTeXBasisBladeForm.BasisVectorsOuterProduct;
            _basisNames = basisNames;

            if (!_basisNames.Length.IsValidVaSpaceDimension())
                throw new InvalidOperationException("Invalid number of basis vectors");

            VSpaceDimension = _basisNames.Length;
            GaSpaceDimension = VSpaceDimension.ToGaSpaceDimension();
        }

        public GaNumLaTeXRenderer(GaLaTeXBasisBladeForm basisBladeForm, params string[] basisNames)
        {
            BasisBladeForm = basisBladeForm;
            _basisNames = basisNames;

            if (BasisBladeForm == GaLaTeXBasisBladeForm.BasisBlade)
            {
                if (!_basisNames.Length.IsValidGaSpaceDimension())
                    throw new InvalidOperationException("Invalid number of basis blades");

                GaSpaceDimension = _basisNames.Length;
                VSpaceDimension = GaSpaceDimension.ToVSpaceDimension();
            }
            else
            {
                if (!_basisNames.Length.IsValidVaSpaceDimension())
                    throw new InvalidOperationException("Invalid number of basis vectors");

                VSpaceDimension = _basisNames.Length;
                GaSpaceDimension = VSpaceDimension.ToGaSpaceDimension();
            }
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
            if (!this.IsValidBasisBladeId(basisBladeId))
                return "Invalid basis blade id " + basisBladeId;

            if (BasisBladeForm == GaLaTeXBasisBladeForm.BasisBlade)
                return _basisNames[basisBladeId];

            if (basisBladeId == 0)
                return @"\mathfrak{1}";

            var basisVectors =
                _basisNames.PickUsingPattern(basisBladeId).ToArray();

            if (BasisBladeForm == GaLaTeXBasisBladeForm.BasisVectorsGeometricProduct)
                return basisVectors.Concatenate();

            if (BasisBladeForm == GaLaTeXBasisBladeForm.BasisVectorsOuterProduct)
                return basisVectors.Concatenate(@" \wedge ");

            return BasisVectorPrefix.Replace(
                "{}", 
                basisVectors.Concatenate("", "{", "}")
            );
        }

        public string FormatMultivector(GaNumMultivector mv)
        {
            var composer = new List<string>();

            foreach (var term in mv.NonZeroTerms)
            {
                var basisBlade = FormatBasisBlade(term.Key);
                var scalar = term.Value.ToString("G");

                composer.Add(@" \left( " + scalar + @" \right) " + basisBlade);
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

        public Image RenderMultivector(GaNumMultivector mv)
        {
            return RenderMathToImage(FormatMultivector(mv));
        } 
    }
}