using System.Linq;
using GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories;
using TextComposerLib.Text;

namespace GeometricAlgebraNumericsLib.Validators
{
    public sealed class GaNumMultivectorValidator : GaNumValidator
    {
        public override string Validate()
        {
            ReportComposer.AppendHeader("Numeric Multivector Validations");

            var vSpaceDim = 3;
            var gaSpaceDim = 1 << vSpaceDim;

            var randGen = new GaRandomGenerator(10);

            //Initialize multivectors with random coefficients
            var mv1 = RandomGenerator.GetNumFullMultivectorTerms(vSpaceDim).CreateSarMultivector(vSpaceDim);
            var mv2 = mv1.GetDgrMultivector();
            var mvTree = mv1.BtrRootNode;

            //ValidateEqual("Input Multivectors", mv1, mv2.ToMultivector());

            var mvTerms1 = mv1
                .GetStoredTerms()
                .OrderBy(t => t.BasisBladeId)
                .Select(t => $"{t.BasisBladeId}: {t.ScalarValue}")
                .Concatenate(", ");

            var mvTerms2 = mv2
                .GetStoredTerms()
                .OrderBy(t => t.BasisBladeId)
                .Select(t => $"{t.BasisBladeId}: {t.ScalarValue}")
                .Concatenate(", ");

            //var mvTerms3 = mvTree
            //    .GetNodeInfo(vSpaceDim, 0)
            //    .GetTreeLeafNodesInfo()
            //    .Select(n => new KeyValuePair<int, double>((int) n.Id, n.Value))
            //    .OrderBy(t => t.Key)
            //    .Select(t => $"{t.Key}: {t.Value}")
            //    .Concatenate(", ");

            ReportComposer
                .AppendLine(mvTerms1)
                .AppendLine(mvTerms2);
                //.AppendLine(mvTerms3);

            ReportComposer.AppendLine(mvTree.ToString());

            return ReportComposer.ToString();
        }
    }
}