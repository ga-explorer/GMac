using System;
using GeometricAlgebraStructuresLib.Frames;
using TextComposerLib.Text.Linear;
using TextComposerLib.Text.Parametric;
using TextComposerLib.Text.Structured;

namespace GMac.CodeComposers.GradedMultivectorsLibraryComposer.Composers.CSharp.KVectorClass
{
    /// <summary>
    /// This class generates a static members file for the k-vector class
    /// </summary>
    internal sealed class StaticCodeFileGenerator : CodeLibraryCodeFileGenerator 
    {
        internal StaticCodeFileGenerator(CodeLibraryComposer libGen)
            : base(libGen)
        {
        }


        private string GenerateDeclarations(int grade)
        {
            var kvDim = CurrentFrame.KvSpaceDimension(grade);

            var template = Templates["static_basisblade_declare"];

            var declaresText = new ListTextComposer(Environment.NewLine);

            var coefsText = new ListTextComposer(", ");

            for (var index = 0UL; index < kvDim; index++)
            {
                coefsText.Clear();

                for (var i = 0UL; i < kvDim; i++)
                    coefsText.Add((i == index) ? "1.0D" : "0.0D");

                declaresText.Add(
                    template,
                    "frame", CurrentFrameName,
                    "id", CurrentFrame.BasisBladeId(grade, index),
                    "grade", grade,
                    "scalars", coefsText
                    );
            }

            declaresText.Add("");

            return declaresText.ToString();
        }

        private string GenerateBasisBladesNames(int grade)
        {
            var namesText = new ListTextComposer(", ") { ActiveItemSuffix = "\"", ActiveItemPrefix = "\"" };

            for (var index = 0UL; index < CurrentFrame.KvSpaceDimension(grade); index++)
                namesText.Add(
                    CurrentFrame.BasisBlade(grade, index).IndexedName
                    );

            return Templates["static_basisblade_name"].GenerateText("names", namesText);
        }

        public override void Generate()
        {
            GenerateKVectorFileStartCode();

            var kvdimsText = new ListTextComposer(", ");
            var basisnamesText = new ListTextComposer("," + Environment.NewLine);
            var basisbladesText = new ListTextComposer(Environment.NewLine);

            foreach (var grade in CurrentFrame.Grades())
            {
                kvdimsText.Add(CurrentFrame.KvSpaceDimension(grade));

                basisnamesText.Add(GenerateBasisBladesNames(grade));

                basisbladesText.Add(GenerateDeclarations(grade));
            }

            TextComposer.Append(
                Templates["static"],
                "frame", CurrentFrameName,
                "vspacedim", CurrentFrame.VSpaceDimension,
                "double", GMacLanguage.ScalarTypeName,
                "kvector_sizes_lookup_table", kvdimsText,
                "basisnames", basisnamesText,
                "basisblades", basisbladesText
                );

            GenerateKVectorFileFinishCode();

            FileComposer.FinalizeText();
        }
    }
}
