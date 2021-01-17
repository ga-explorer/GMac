namespace GeometricAlgebraNumericsLib.Validators
{
    //public sealed class GaNumVersorValidator : GaNumValidator
    //{
    //    private readonly GaRandomGenerator _randomGenerator 
    //        = new GaRandomGenerator(10);

    //    private IGaNumVector CreateVector(int vSpaceDim)
    //    {
    //        return _randomGenerator
    //            .GetNumFullVectorTerms(vSpaceDim)
    //            .CreateDgrMultivector(vSpaceDim)
    //            .GetVectorPart();
    //    }


    //    public override string Validate()
    //    {
    //        ReportComposer.AppendHeader("Numeric Versor Validations");

    //        //var v1 = CreateVector(vSpaceDim).GetSarMultivector();
    //        //var v2 = CreateVector(vSpaceDim).GetSarMultivector();
    //        //var v3 = CreateVector(vSpaceDim).GetSarMultivector();

    //        var v1 = "(1.2<1>, -4.6<2>, 2.6<3>, -0.6<4>, 2.5<5>)<>; (1.3<>)<a>; (-2.2<>)<b>; (-2.7<>)<c>"
    //            .ParseGaPoTMultivector(5, 3);

    //        var v2 = "(-2.6<2>, -1.6<3>, -3.6<5>)<>; (-1.2<>)<a>"
    //            .ParseGaPoTMultivector(5, 3);

    //        var v3 = "(-2.2<1>, -2.6<4>)<>; (1.2<>)<b>; (-1.9<>)<c>"
    //            .ParseGaPoTMultivector(5, 3);

    //        var versor = v1.EGp(v2.EGp(v3));

    //        var versorInv = versor.EInverse();

    //        var result = versorInv.EGp(versor);

    //        ReportComposer.AppendLine($"versor: {versor.GetGaNumMultivectorText()}");
    //        ReportComposer.AppendLine($"inv(versor): {versorInv.GetGaNumMultivectorText()}");
    //        ReportComposer.AppendLine($"inv(versor) gp versor: {result.GetGaNumMultivectorText()}");

    //        return ReportComposer.ToString();
    //    }
    //}
}