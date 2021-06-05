using GeometricAlgebraStructuresLib.Frames;
using GMac.Engine.API.Binding;
using GMac.Engine.API.CodeGen;
using GMac.Engine.API.Target;
using GMac.Engine.Compiler.Semantic.ASTConstants;
using TextComposerLib.Text.Linear;

namespace GMac.CodeComposers.GradedMultivectorsLibraryComposer.Composers.CSharp.OutermorphismClass
{
    internal class MapMethodFileGenerator : CodeLibraryMacroCodeFileGenerator
    {
        internal int InputGrade { get; }


        internal MapMethodFileGenerator(CodeLibraryComposer libGen, int inGrade)
            : base(libGen, DefaultMacro.Outermorphism.Apply)
        {
            InputGrade = inGrade;
        }


        protected override void InitializeGenerator(GMacMacroCodeComposer macroCodeGen)
        {

        }

        protected override void SetMacroParametersBindings(GMacMacroBinding macroBinding)
        {
            macroBinding.BindMultivectorPartToVariables("result", InputGrade);
            macroBinding.BindMultivectorPartToVariables("mv", InputGrade);

            for (var i = 0; i < CurrentFrame.VSpaceDimension; i++)
            {
                var id = CurrentFrame.BasisVectorId(i);

                for (var j = 0; j < CurrentFrame.VSpaceDimension; j++)
                {
                    var valueAccessName = "om.ImageV" + (j + 1) + ".#E" + id + "#";

                    macroBinding.BindToVariables(valueAccessName);
                }
            }
        }

        protected override void SetTargetVariablesNames(GMacTargetVariablesNaming targetNaming)
        {
            BladesLibraryGenerator.SetBasisBladeToArrayNaming(targetNaming, "result", InputGrade, "mappedKVectorScalars");
            BladesLibraryGenerator.SetBasisBladeToArrayNaming(targetNaming, "mv", InputGrade, "kVectorScalars");

            for (var i = 0; i < CurrentFrame.VSpaceDimension; i++)
            {
                var id = CurrentFrame.BasisVectorId(i);

                for (var j = 0; j < CurrentFrame.VSpaceDimension; j++)
                {
                    var varName = "omScalars".ScalarItem(i, j);

                    var valueAccessName = "om.ImageV" + (j + 1) + ".#E" + id + "#";

                    targetNaming.SetScalarParameter(valueAccessName, varName);
                }
            }

            BladesLibraryGenerator.SetTargetTempVariablesNames(targetNaming);
        }

        public override void Generate()
        {
            GenerateOutermorphismFileStartCode();

            var computationsText = GenerateComputationsCode();

            TextComposer.Append(
                Templates["om_apply"],
                "double", GMacLanguage.ScalarTypeName,
                "grade", InputGrade,
                "num", CurrentFrame.KvSpaceDimension(InputGrade),
                "computations", computationsText
                );

            GenerateOutermorphismFileFinishCode();

            FileComposer.FinalizeText();
        }
    }
}
