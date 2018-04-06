using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TextComposerLib;
using TextComposerLib.Logs.Progress;
using TextComposerLib.Text;
using TextComposerLib.Text.Mapped;
using TextComposerLib.Text.Parametric;

namespace GMac.GMacScripting
{
    /// <summary>
    /// Perform some string manipulation on a given script to convert it to C# class code that can be
    /// executed on GMac AST to compute and display values
    /// </summary>
    internal sealed class GMacScriptGenerator : IProgressReportSource
    {
        #region Templates Text
        private const string ScriptCodeTemplateText =
@"
#opened_namespaces#

/// <summary>
/// This class represents a GMac script to be executed using an internal script interpreter object
/// </summary>
public sealed class InteractiveScript : IGMacScript
{
    /// <summary>
    /// The script interpreter object that executes all script commands
    /// </summary>
    public GMacScriptInterpreter Ipr { get; set; }

    /// <summary>
    /// The root GMacAST for this script
    /// </summary>
    public AstRoot Root { get { return Ipr.Root; } }

    #members_code#

    /// <summary>
    /// The processing code to be executed
    /// </summary>
    public void Process()
    {
        #script_code#
    }
}";
        #endregion


        private static string TranslateSquareBracketCommand(string commandText)
        {
            if (String.IsNullOrEmpty(commandText)) return String.Empty;

            var parts = commandText.Split(new[] { "|>" }, StringSplitOptions.None);

            var s = new StringBuilder();

            if (parts.Length > 1)
            {
                for (var i = parts.Length - 1; i >= 1; i--)
                    s.Append('@').Append(parts[i].Trim()).Append('(');

                s.Append("\"|").Append(parts[0]).Append("|\"");

                s.Append("".PadLeft(parts.Length - 1, ')'));
            }
            else
            {
                s.Append("\"|").Append(parts[0]).Append("|\"");
            }

            return s.ToString();
        }

        private static string TranslateAngleBracketCommand(string commandText)
        {
            if (String.IsNullOrEmpty(commandText)) return String.Empty;

            var parts = commandText.Split(new[] { "|>" }, StringSplitOptions.None);

            var s = new StringBuilder();

            if (parts.Length > 1)
            {
                for (var i = parts.Length - 1; i >= 1; i--)
                    s.Append('@').Append(parts[i].Trim()).Append('(');

                s.Append(parts[0].Trim());

                s.Append("".PadLeft(parts.Length - 1, ')'));
            }
            else
            {
                s.Append(parts[0].Trim());
            }

            return s.ToString();
        }

        private static string TranslateSquareBracketCommands(string scriptText)
        {
            var mappingComposer = new MappingComposer();

            mappingComposer
                .SetDelimitedText(scriptText, "[:", ":]")
                .UniqueMarkedSegments
                .TransformUsing(TranslateSquareBracketCommand);

            return mappingComposer.FinalText;
        }

        private static string TranslateAngleBracketCommands(string scriptText)
        {
            var mappingComposer = new MappingComposer();

            mappingComposer
                .SetDelimitedText(scriptText, "<:", ":>")
                .UniqueMarkedSegments
                .TransformUsing(TranslateAngleBracketCommand);

            return mappingComposer.FinalText;
        }

        private static string ToStringSubstituteCode(string text)
        {
            var textBuilder = new MappingComposer();

            textBuilder
                .SetDelimitedText(text, "|{", "}|")
                .UniqueMarkedSegments
                .TransformByMarkedIndexUsing(index => "|{" + index + "}|");

            if (textBuilder.HasMarkedSegments == false)
                return text.ValueToQuotedLiteral(true);

            return
                textBuilder
                .UniqueMarkedSegments
                .Select(s => s.InitialText)
                .Concatenate(
                    ", ",
                    "Ipr.Substitute(" + textBuilder.FinalText.ValueToQuotedLiteral(true) + ", ",
                    ")"
                );
        }

        /// <summary>
        /// Map all strings delimited by "| and |" into String.Format() statements 
        /// (or string literals if possible)
        /// </summary>
        /// <param name="scriptText"></param>
        /// <returns></returns>
        private static string TranslateFormattedStrings(string scriptText)
        {
            var mappingComposer = new MappingComposer();

            mappingComposer
                .SetDelimitedText(scriptText, "\"|", "|\"")
                .UniqueMarkedSegments
                .TransformUsing(ToStringSubstituteCode);

            return mappingComposer.FinalText;
        }

        private static string GenerateOpenNamespacesCode(IEnumerable<string> openedNamespaces)
        {
            return 
                openedNamespaces
                .Distinct()
                .OrderBy(s => s)
                .Concatenate(Environment.NewLine, "", "", "using ", ";");
        }


        private readonly ParametricComposer _scriptCodeTemplate =
            new ParametricComposer("#", "#", ScriptCodeTemplateText);

        public GMacScriptShortcuts Shortcuts { get; internal set; }

        public string GeneratedText { get; private set; }

        public string ProgressSourceId => "Script Class Generator";

        public ProgressComposer Progress => GMacSystemUtils.Progress;


        /// <summary>
        /// Map all strings identified by a leading @ using the shortcuts dictionary
        /// If the shortcut is not found, leave it as it is
        /// </summary>
        /// <param name="scriptText"></param>
        /// <returns></returns>
        private string TranslateShortcuts(string scriptText)
        {
            var mappingComposer = new MappingComposer();

            mappingComposer
                .SetIdentifiedText(scriptText, "@")
                .UniqueMarkedSegments
                .TransformUsing(
                    s =>
                    {
                        string value;
                        return
                            Shortcuts.TryGetMethodName(s.InitialText.ToLower(), out value)
                            ? value
                            : s.OriginalText;
                    }
                    );

            return mappingComposer.FinalText;
        }

        private string GenerateScriptCode(string scriptCode)
        {
            if (String.IsNullOrEmpty(scriptCode.Trim())) return String.Empty;

            var processCode = TranslateSquareBracketCommands(scriptCode);

            this.ReportNormal("Translate bracketed commands done", processCode);

            processCode = TranslateAngleBracketCommands(processCode);

            this.ReportNormal("Translate tagged commands done", processCode);

            processCode = TranslateFormattedStrings(processCode);

            this.ReportNormal("Translate formatted strings done", processCode);

            processCode = TranslateShortcuts(processCode);

            this.ReportNormal("Translate method shortcuts done", processCode);

            return processCode;
        }


        internal string Generate(IEnumerable<string> openedNamespaces, string membersText, string scriptText)
        {
            _scriptCodeTemplate.ClearBindings();

            //Generate the full code of the script class
            GeneratedText = _scriptCodeTemplate.GenerateText(
                "opened_namespaces", GenerateOpenNamespacesCode(openedNamespaces),
                "members_code", GenerateScriptCode(membersText),
                "script_code", GenerateScriptCode(scriptText)
                );

            return GeneratedText;
        }
    }
}
