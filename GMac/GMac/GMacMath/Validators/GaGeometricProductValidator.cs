using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Products;

namespace GMac.GMacMath.Validators
{
    public sealed class GaGeometricProductValidator : GMacMathValidator
    {
        public GaNumFrame NumericFrame { get; set; }


        private void ValidateNumericFrame()
        {
            if (NumericFrame == null)
                return;

            ReportComposer.AppendHeader("Numeric Geometric Product of 3 Basis Blades Validations");

            var gp = (IGaNumOrthogonalGeometricProduct) NumericFrame.Gp;

            for (var id1 = 0; id1 < NumericFrame.GaSpaceDimension; id1++)
            {
                for (var id2 = 0; id2 < NumericFrame.GaSpaceDimension; id2++)
                {
                    var gp12 = gp.MapToTerm(id1, id2);

                    for (var id3 = 0; id3 < NumericFrame.GaSpaceDimension; id3++)
                    {
                        var gp123la = gp.MapToTermLa(id1, id2, id3);
                        var gp123ra = gp.MapToTermRa(id1, id2, id3);

                        var gp23 = gp.MapToTerm(id2, id3);

                        var gp12_3 = gp.MapToTerm(gp12.BasisBladeId, id3);
                        gp12_3.ScalarValue *= gp12.ScalarValue;

                        var gp1_23 = gp.MapToTerm(id1, gp23.BasisBladeId);
                        gp1_23.ScalarValue *= gp23.ScalarValue;

                        ValidateEqual($"Gp(Gp({id1}, {id2}), {id3}) == GpLA({id1}, {id2}, {id3})", gp12_3, gp123la);
                        ValidateEqual($"Gp({id1}, Gp({id2}, {id3})) == GpRA({id1}, {id2}, {id3})", gp1_23, gp123ra);
                        ValidateEqual($"GpLA({id1}, {id2}, {id3}) == GpRA({id1}, {id2}, {id3})", gp123la, gp123ra);
                    }
                }
            }

            ReportComposer.AppendLineAtNewLine();
        }

        public override string Validate()
        {
            ValidateNumericFrame();

            return ReportComposer.ToString();
        }
    }
}