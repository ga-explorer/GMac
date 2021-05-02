using System;
using GeometricAlgebraStructuresLib.Frames;
using GMac.GMacCompiler.Semantic.ASTConstants;
using TextComposerLib.Text.Linear;
using TextComposerLib.Text.Structured;

namespace GradedMultivectorsLibraryComposer.Composers.CSharp.KVectorClass
{
    internal sealed class FactorMainMethodsFileGenerator : CodeLibraryCodeFileGenerator 
    {
        internal FactorMainMethodsFileGenerator(CodeLibraryComposer libGen)
            : base(libGen)
        {
        }


        private void GenerateMaxCoefIdFunction(int grade)
        {
            var casesText = new ListTextComposer(Environment.NewLine);

            var maxIndex = CurrentFrame.KvSpaceDimension(grade) - 1;

            for (var index = 1UL; index < maxIndex; index++)
                casesText.Add(
                    Templates["maxcoefid_case"],
                    "index", index,
                    "id", CurrentFrame.BasisBladeId(grade, index)
                    );

            TextComposer.Append(
                Templates["maxcoefid"],
                "grade", grade,
                "double", GMacLanguage.ScalarTypeName,
                "initid", CurrentFrame.BasisBladeId(grade, 0),
                "maxindex", maxIndex,
                "maxid", CurrentFrame.BasisBladeId(grade, maxIndex),
                "maxcoefid_case", casesText
                );
        }

        private void GenerateFactorGradeFunction(int grade)
        {
            var casesText = new ListTextComposer(Environment.NewLine);

            for (var index = 1UL; index < CurrentFrame.KvSpaceDimension(grade); index++)
                casesText.Add(
                    Templates["factorgrade_case"].GenerateUsing(
                        CurrentFrame.BasisBladeId(grade, index)
                        )
                    );

            TextComposer.Append(
                Templates["factorgrade"],
                "frame", CurrentFrameName,
                "grade", grade,
                "double", GMacLanguage.ScalarTypeName,
                "factorgrade_case", casesText
                );
        }

        private void GenerateFactorMainFunction()
        {
            var casesText = new ListTextComposer(Environment.NewLine);

            for (var grade = 2; grade < CurrentFrame.VSpaceDimension; grade++)
                casesText.Add(
                    Templates["factor_main_case"],
                    "name", DefaultMacro.EuclideanUnary.Magnitude,
                    "grade", grade,
                    "frame", CurrentFrameName
                    );

            TextComposer.Append(
                Templates["factor_main"],
                "frame", CurrentFrameName,
                "maxgrade", CurrentFrame.VSpaceDimension,
                "maxid", CurrentFrame.MaxBasisBladeId,
                "factor_main_case", casesText
                );
        }

        public override void Generate()
        {
            GenerateKVectorFileStartCode();

            for (var grade = 2; grade < CurrentFrame.VSpaceDimension; grade++)
            {
                GenerateMaxCoefIdFunction(grade);

                GenerateFactorGradeFunction(grade);
            }

            GenerateFactorMainFunction();

            GenerateKVectorFileFinishCode();

            FileComposer.FinalizeText();
        }
    }
}
