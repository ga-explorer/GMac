using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CSScriptLib;
using GMac.Engine.AST;
using TextComposerLib;
using TextComposerLib.Logs.Progress;
using TextComposerLib.Text;
using TextComposerLib.Text.Linear;

namespace GMac.Engine.Scripting
{
    public sealed class GMacScriptManager : IProgressReportSource
    {
        public static readonly string[] DefaultNamespacesList = new[]
        {
            "System",
            "System.Collections.Generic",
            "System.Drawing",
            "System.Linq",
            "GMac",
            "GMac.GMacAST",
            "GMac.GMacAST.Symbols",
            "GMac.GMacAST.Expressions",
            "GMac.GMacAST.Commands",
            "GMac.GMacAPI.Binding",
            "GMac.GMacScripting",
            "TextComposerLib",
            "TextComposerLib.Text.Linear",
            "Wolfram.NETLink"
        };


        private IGMacScript _gmacScriptInstance;

        private readonly GMacScriptGenerator _scriptGenerator;


        public AstRoot Root { get; }

        public string ProgressSourceId => "GMac Script Manager";

        public ProgressComposer Progress => GMacEngineUtils.Progress;

        public GMacScriptInterpreter Ipr { get; }

        public string ScriptText { get; private set; }

        public string ScriptClassMembersText { get; private set; }

        public List<string> OpenedNamespaces { get; private set; }

        public string ScriptCode { get; private set; }

        public Exception GenerationError { get; private set; }

        public Exception CompilationError { get; private set; }

        public Exception ExecutionError { get; private set; }

        public Exception ScriptError
        {
            get
            {
                if (HasGenerationError) return GenerationError;

                if (HasCompilationError) return CompilationError;

                return ExecutionError;
            }
        }

        public bool ScriptGenerationComplete { get; private set; }

        public bool ScriptCompilationComplete { get; private set; }

        public bool ScriptExecutionComplete { get; private set; }


        public bool HasGenerationError => GenerationError != null;

        public bool HasCompilationError => CompilationError != null;

        public bool HasExecutionError => ExecutionError != null;

        public bool HasError => GenerationError != null ||
                                CompilationError != null ||
                                ExecutionError != null;


        public GMacScriptManager(AstRoot root)
        {
            Root = root;

            Ipr = new GMacScriptInterpreter(Root);

            _scriptGenerator = new GMacScriptGenerator() {Shortcuts = Ipr.Shortcuts};
        }


        private bool GenerateCSharpCode()
        {
            var progressId = this.ReportStart("Script Generation", ScriptCode);

            try
            {
                _scriptGenerator.Generate(OpenedNamespaces, ScriptClassMembersText, ScriptText);

                ScriptCode = _scriptGenerator.GeneratedText;

                this.ReportFinish(progressId, "", ProgressEventArgsResult.Success);

                return true;
            }
            catch (Exception e)
            {
                GenerationError = e;

                this.ReportError(e);

                this.ReportFinish(progressId, "", ProgressEventArgsResult.Failure);

                return false;
            }
        }

        private bool CompileCSharpCode()
        {
            if (ScriptGenerationComplete == false) return false;

            var progressId = this.ReportStart("Script Compilation", ScriptCode);

            try
            {
                //CSScript.ShareHostRefAssemblies = true;

                //CSScript.AssemblyResolvingEnabled = true;

                //CSScript.Evaluator.LoadCode(ScriptCode);

                ////var helper = new AsmHelper(CSScript.LoadCode(code, null, true));
                //var helper = new AsmHelper(CSScript.LoadCode(ScriptCode));
                
                ////the only reflection based call 
                //_gmacScriptInstance = (IGMacScript) helper.CreateObject("InteractiveScript");

                _gmacScriptInstance = (IGMacScript) CSScript.Evaluator.LoadCode(ScriptCode);

                _gmacScriptInstance.Ipr = Ipr;

                this.ReportFinish(progressId, "", ProgressEventArgsResult.Success);

                return true;
            }
            catch (Exception e)
            {
                CompilationError = e;

                this.ReportError(e);

                this.ReportFinish(progressId, "", ProgressEventArgsResult.Failure);

                return false;
            }
        }

        private bool ExecuteCSharpCode()
        {
            if (ScriptCompilationComplete == false) return false;

            var progressId = this.ReportStart("Script Execution", ScriptCode);

            try
            {
                _gmacScriptInstance.Process();

                this.ReportFinish(progressId, "", ProgressEventArgsResult.Success);

                return true;
            }
            catch (Exception e)
            {
                ExecutionError = e;

                this.ReportError(e);

                this.ReportFinish(progressId, "", ProgressEventArgsResult.Failure);

                return false;
            }
        }


        public void SaveScript(string filePath)
        {
            var composer = new LinearTextComposer();

            composer
                .AppendLine("[shortcuts]")
                .AppendLine(Ipr.Shortcuts)
                .AppendLine("[namespaces]")
                .AppendLine(OpenedNamespaces.Concatenate(Environment.NewLine))
                .AppendLine()
                .AppendLine("[members]")
                .AppendLine(ScriptClassMembersText)
                .AppendLine("[script]")
                .AppendLine(ScriptText);

            File.WriteAllText(filePath, composer.ToString());
        }

        public void LoadScript(string filePath)
        {
            var text = File.ReadAllText(filePath);

            var idx0 = text.IndexOf("[shortcuts]", StringComparison.Ordinal);
            var idx1 = text.IndexOf("[namespaces]", StringComparison.Ordinal);
            var idx2 = text.IndexOf("[members]", StringComparison.Ordinal);
            var idx3 = text.IndexOf("[script]", StringComparison.Ordinal);

            var start = idx0 + "[shortcuts]".Length;
            var end = idx1;
            var shortcuts = text.Substring(start, end - start).Trim().SplitLines();

            start = idx1 + "[namespaces]".Length;
            end = idx2;
            var openedNamespaces = text.Substring(start, end - start).Trim().SplitLines();

            start = idx2 + "[members]".Length;
            end = idx3;
            var membersText = text.Substring(start, end - start).Trim();

            start = idx3 + "[script]".Length;
            end = text.Length;
            var scriptText = text.Substring(start, end - start).Trim();

            SetScript(scriptText, membersText, openedNamespaces, shortcuts);
        }


        public void SetScript(string scriptText)
        {
            SetScript(scriptText, "", DefaultNamespacesList, Enumerable.Empty<string>());
        }

        public void SetScript(string scriptText, string membersText)
        {
            SetScript(scriptText, membersText, DefaultNamespacesList, Enumerable.Empty<string>());
        }

        public void SetScript(string scriptText, IEnumerable<string> openedNamespaces)
        {
            SetScript(scriptText, "", openedNamespaces, Enumerable.Empty<string>());
        }

        public void SetScript(string scriptText, string membersText, IEnumerable<string> openedNamespaces, IEnumerable<string> shortcuts)
        {
            GenerationError = null;
            CompilationError = null;
            ExecutionError = null;

            ScriptGenerationComplete = false;
            ScriptCompilationComplete = false;
            ScriptExecutionComplete = false;

            Ipr.Shortcuts.ResetShortcuts(shortcuts);
            OpenedNamespaces = new List<string>(openedNamespaces);
            ScriptText = scriptText;
            ScriptClassMembersText = membersText;
            ScriptCode = string.Empty;
            _gmacScriptInstance = null;

            this.ResetProgress();
        }


        public bool GenerateScript()
        {
            ScriptGenerationComplete = GenerateCSharpCode();

            return ScriptGenerationComplete;
        }

        public bool CompileScript()
        {
            ScriptGenerationComplete = GenerateCSharpCode();
            ScriptCompilationComplete = CompileCSharpCode();

            return ScriptCompilationComplete;
        }

        public bool ExecuteScript()
        {
            ScriptGenerationComplete = GenerateCSharpCode();
            ScriptCompilationComplete = CompileCSharpCode();
            ScriptExecutionComplete = ExecuteCSharpCode();

            return ScriptExecutionComplete;
        }
    }
}
