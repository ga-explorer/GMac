using System.Collections.Generic;
using TextComposerLib.Text.Parametric;

namespace TextComposerLib.Text.Linear
{
    public static class LinearUtils
    {
        public static LinearComposer Append(this LinearComposer textBuilder, ParametricComposer template, params object[] parametersValues)
        {
            var text = template.GenerateText(parametersValues);

            return textBuilder.Append(text);
        }

        public static LinearComposer Append(this LinearComposer textBuilder, ParametricComposer template, params string[] parametersValues)
        {
            var text = template.GenerateText(parametersValues);

            return textBuilder.Append(text);
        }

        public static LinearComposer Append(this LinearComposer textBuilder, ParametricComposer template, IDictionary<string, string> parametersValues)
        {
            var text = template.GenerateText(parametersValues);

            return textBuilder.Append(text);
        }

        public static LinearComposer Append(this LinearComposer textBuilder, ParametricComposer template, IParametericComposerValueSource parametersValues)
        {
            var text = template.GenerateText(parametersValues);

            return textBuilder.Append(text);
        }

        public static LinearComposer AppendLine(this LinearComposer textBuilder, ParametricComposer template, params object[] parametersValues)
        {
            var text = template.GenerateText(parametersValues);

            return textBuilder.AppendLine(text);
        }

        public static LinearComposer AppendLine(this LinearComposer textBuilder, ParametricComposer template, params string[] parametersValues)
        {
            var text = template.GenerateText(parametersValues);

            return textBuilder.AppendLine(text);
        }

        public static LinearComposer AppendLine(this LinearComposer textBuilder, ParametricComposer template, IDictionary<string, string> parametersValues)
        {
            var text = template.GenerateText(parametersValues);

            return textBuilder.AppendLine(text);
        }

        public static LinearComposer AppendLine(this LinearComposer textBuilder, ParametricComposer template, IParametericComposerValueSource parametersValues)
        {
            var text = template.GenerateText(parametersValues);

            return textBuilder.AppendLine(text);
        }

        public static LinearComposer AppendNewLine(this LinearComposer textBuilder, ParametricComposer template, params object[] parametersValues)
        {
            var text = template.GenerateText(parametersValues);

            return textBuilder.AppendNewLine(text);
        }

        public static LinearComposer AppendNewLine(this LinearComposer textBuilder, ParametricComposer template, params string[] parametersValues)
        {
            var text = template.GenerateText(parametersValues);

            return textBuilder.AppendNewLine(text);
        }

        public static LinearComposer AppendNewLine(this LinearComposer textBuilder, ParametricComposer template, IDictionary<string, string> parametersValues)
        {
            var text = template.GenerateText(parametersValues);

            return textBuilder.AppendNewLine(text);
        }

        public static LinearComposer AppendNewLine(this LinearComposer textBuilder, ParametricComposer template, IParametericComposerValueSource parametersValues)
        {
            var text = template.GenerateText(parametersValues);

            return textBuilder.AppendNewLine(text);
        }

        public static LinearComposer AppendAtNewLine(this LinearComposer textBuilder, ParametricComposer template, params object[] parametersValues)
        {
            var text = template.GenerateText(parametersValues);

            return textBuilder.AppendAtNewLine(text);
        }

        public static LinearComposer AppendAtNewLine(this LinearComposer textBuilder, ParametricComposer template, params string[] parametersValues)
        {
            var text = template.GenerateText(parametersValues);

            return textBuilder.AppendAtNewLine(text);
        }

        public static LinearComposer AppendAtNewLine(this LinearComposer textBuilder, ParametricComposer template, IDictionary<string, string> parametersValues)
        {
            var text = template.GenerateText(parametersValues);

            return textBuilder.AppendAtNewLine(text);
        }

        public static LinearComposer AppendAtNewLine(this LinearComposer textBuilder, ParametricComposer template, IParametericComposerValueSource parametersValues)
        {
            var text = template.GenerateText(parametersValues);

            return textBuilder.AppendAtNewLine(text);
        }

        public static LinearComposer AppendLineAtNewLine(this LinearComposer textBuilder, ParametricComposer template, params object[] parametersValues)
        {
            var text = template.GenerateText(parametersValues);

            return textBuilder.AppendLineAtNewLine(text);
        }

        public static LinearComposer AppendLineAtNewLine(this LinearComposer textBuilder, ParametricComposer template, params string[] parametersValues)
        {
            var text = template.GenerateText(parametersValues);

            return textBuilder.AppendLineAtNewLine(text);
        }

        public static LinearComposer AppendLineAtNewLine(this LinearComposer textBuilder, ParametricComposer template, IDictionary<string, string> parametersValues)
        {
            var text = template.GenerateText(parametersValues);

            return textBuilder.AppendLineAtNewLine(text);
        }

        public static LinearComposer AppendLineAtNewLine(this LinearComposer textBuilder, ParametricComposer template, IParametericComposerValueSource parametersValues)
        {
            var text = template.GenerateText(parametersValues);

            return textBuilder.AppendLineAtNewLine(text);
        }
    }
}
