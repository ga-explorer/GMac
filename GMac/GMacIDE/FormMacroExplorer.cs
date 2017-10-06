using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GMac.GMacAPI.Binding;
using GMac.GMacAPI.CodeGen.BuiltIn.CSharp.Direct;
using GMac.GMacAST;
using GMac.GMacAST.Symbols;
using GMac.GMacCompiler.Semantic.AST;
using GMac.GMacCompiler.Semantic.ASTDebug;
using GMac.GMacUtils;
using TextComposerLib;
using TextComposerLib.Diagrams.GraphViz.Dot;
using TextComposerLib.Diagrams.GraphViz.UI;
using TextComposerLib.Logs.Progress;

namespace GMac.GMacIDE
{
    //TODO: Add possiblity to use interface to select names of variables bound to macro parameters
    public partial class FormMacroExplorer : Form
    {
        private GMacAstDescription AstDescription { get; }

        private AstMacro SelectedMacro { get; }

        private GMacMacroBinding MacroBinding { get; set; }

        private DotGraph Graph { get; set; }

        private SingleMacroGen MacroCodeGenerator { get; set; }

        private GMacMacro SelectedGMacMacro => SelectedMacro.AssociatedMacro;

        private Dictionary<int, ProgressEventArgs> ProgressHistory { get; set; }


        internal FormMacroExplorer(GMacMacro macro)
        {
            InitializeComponent();

            SelectedMacro = new AstMacro(macro);

            AstDescription = new GMacAstDescription();

            textBoxMacroName.Text = macro.SymbolAccessName;

            ResetMacroParameters();
        }


        private void ResetMacroParameters()
        {
            var composer = new StringBuilder();

            var valueAccessList = 
                SelectedMacro
                .Parameters
                .SelectMany(p => p.DatastoreValueAccess.ExpandAll());

            foreach (var valueAccess in valueAccessList)
                composer.AppendLine(valueAccess.ValueAccessName);

            textBoxParameters.Text = composer.ToString();
        }

        private void AddMacroParameterBinding(string line)
        {
            var eqIndex = line.IndexOf('=');

            if (eqIndex < 0)
            {
                MacroBinding.BindToVariables(line);
                
                return;
            }

            var lhs = line.Substring(0, eqIndex - 1).Trim();
            var rhs = line.Substring(eqIndex + 1).Trim();

            MacroBinding.BindScalarToConstant(lhs, rhs);
        }

        private bool UpdateMacroBinding()
        {
            var result = true;

            textBoxDisplay.Text = String.Empty;

            MacroBinding = 
                GMacMacroBinding.Create(
                    SelectedMacro
                    );

            var paramText = 
                textBoxParameters
                .Text
                .SplitLines()
                .Select(line => line.Trim())
                .Where(line => String.IsNullOrEmpty(line) == false);

            foreach (var line in paramText)
                try
                {
                    AddMacroParameterBinding(line);
                }
                catch (Exception e)
                {
                    textBoxDisplay.Text += 
                        new StringBuilder()
                        .AppendLine("Error parsing macro parameter binding at:")
                        .Append("    ").AppendLine(line)
                        .AppendLine("Error Message: ")
                        .Append("    ").AppendLine(e.Message)
                        .AppendLine()
                        .ToString();

                    result = false;
                }

            return result;
        }

        private void GenerateMacro_DslCode()
        {
            var itemText = SelectedGMacMacro.CodeLocation.CodeText;

            MacroCodeGenerator.ReportNormal("DSL Source Code", itemText);
        }

        private void GenerateMacro_ParsedBody()
        {
            AstDescription.Log.Clear();

            AstDescription.Generate_Macro_UsingRawBody(SelectedGMacMacro);

            var itemText = AstDescription.Log.ToString();

            MacroCodeGenerator.ReportNormal("Parsed Body", itemText);
        }

        private void GenerateMacro_CompiledBody()
        {
            AstDescription.Log.Clear();

            AstDescription.Generate_Macro_UsingRawCompiledBody(SelectedGMacMacro);

            var itemText = AstDescription.Log.ToString();

            MacroCodeGenerator.ReportNormal("Compiled Body", itemText);
        }

        private void GenerateMacro_OptimizedBody()
        {
            AstDescription.Log.Clear();

            AstDescription.Generate_Macro_UsingOptimizedCompiledBody(SelectedGMacMacro);

            var itemText = AstDescription.Log.ToString();

            MacroCodeGenerator.ReportNormal("Optimized Body", itemText);
        }

        private void GenerateMacro_SampleTargetCode()
        {
            MacroCodeGenerator.Generate();

            var itemText = MacroCodeGenerator.CodeFilesComposer.FirstFileFinalText;

            MacroCodeGenerator.ReportNormal("Sample Target Code", itemText);
        }

        private void GenerateMacro()
        {
            listBoxGenerationStage.Items.Clear();
            
            textBoxDisplay.Text = String.Empty;

            Graph = null;

            if (UpdateMacroBinding() == false)
                return;

            GMacSystemUtils.ResetProgress();

            MacroCodeGenerator = new SingleMacroGen(MacroBinding)
            {
                MacroGenDefaults = {AllowGenerateMacroCode = true}
            };

            GenerateMacro_DslCode();

            GenerateMacro_ParsedBody();

            GenerateMacro_CompiledBody();

            GenerateMacro_OptimizedBody();

            GenerateMacro_SampleTargetCode();

            ProgressHistory =
                MacroCodeGenerator
                .Progress
                .History
                .ReadHistory()
                .ToDictionary(item => item.ProgressId);

            foreach (var item in ProgressHistory)
                listBoxGenerationStage.Items.Add(item.Value.FullTitle);

            Graph = MacroCodeGenerator.Graph;
        }


        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            if (SelectedMacro.IsNullOrInvalid()) return;

            GenerateMacro();
        }

        private void listBoxGenerationStage_SelectedIndexChanged(object sender, EventArgs e)
        {
            var s = new StringBuilder();

            foreach (var itemIndex in listBoxGenerationStage.SelectedIndices.Cast<int>())
                s.AppendLine(ProgressHistory[itemIndex].ToString());

            textBoxDisplay.Text = s.ToString();
        }

        private void buttonResetParameters_Click(object sender, EventArgs e)
        {
            ResetMacroParameters();
        }

        private void buttonGraphViz_Click(object sender, EventArgs e)
        {
            if (ReferenceEquals(Graph, null))
                return;

            var formGraphViz = new FormGraphVizEditor(Graph);

            formGraphViz.Show(this);
        }
    }
}
