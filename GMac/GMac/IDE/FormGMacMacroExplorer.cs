using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CodeComposerLib.WinForms.GraphViz.UserInterface;
using GMac.Engine;
using GMac.Engine.API.Binding;
using GMac.Engine.API.CodeBlock;
using GMac.Engine.API.CodeGen.SingleMacro;
using GMac.Engine.AST;
using GMac.Engine.AST.Symbols;
using GMac.Engine.Compiler.Semantic.AST;
using GMac.Engine.Compiler.Semantic.ASTDebug;
using TextComposerLib;
using TextComposerLib.Loggers.Progress;

namespace GMac.IDE
{
    public partial class FormGMacMacroExplorer : Form
    {
        private GMacAstDescription AstDescription { get; }

        private AstMacro SelectedMacro { get; }

        private GMacMacroExplorerBindingData BindingTextData { get; }
            = new GMacMacroExplorerBindingData();

        private GMacMacroBinding MacroBinding { get; set; }

        private GMacCodeBlock CodeBlock { get; set; }

        private GMacSingleMacroCSharpComposer MacroCodeGenerator { get; set; }

        private GMacMacro SelectedGMacMacro => SelectedMacro.AssociatedMacro;

        private Dictionary<int, ProgressEventArgs> ProgressHistory { get; set; }


        internal FormGMacMacroExplorer(GMacMacro macro)
        {
            InitializeComponent();

            SelectedMacro = new AstMacro(macro);

            AstDescription = new GMacAstDescription();

            textBoxMacroName.Text = macro.SymbolAccessName;
            comboBoxTargetLanguage.SelectedIndex = 0;

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
                composer
                    .AppendLine(valueAccess.ValueAccessName);

            textBoxParameters.Text = composer.ToString();
        }

        private bool UpdateMacroBindingTextData()
        {
            var result = true;

            textBoxDisplay.Text = string.Empty;

            BindingTextData.Clear();

            var paramText = 
                textBoxParameters
                .Text
                .SplitLines()
                .Select(line => line.Trim())
                .Where(line => string.IsNullOrEmpty(line) == false);

            foreach (var line in paramText)
                try
                {
                    BindingTextData.AddFromText(line);
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

        private void GenerateMacro_RawBodyCallCode()
        {
            var itemText = MacroBinding.GenerateMacroBodyCallCode(
                AstMacroBodyKind.RawBody, 
                true
                );

            MacroCodeGenerator.ReportNormal("Raw Body Call Code", itemText);
        }

        private void GenerateMacro_ParsedBodyCallCode()
        {
            var itemText = MacroBinding.GenerateMacroBodyCallCode(
                AstMacroBodyKind.ParsedBody,
                true
            );

            MacroCodeGenerator.ReportNormal("Parsed Body Call Code", itemText);
        }

        private void GenerateMacro_CompiledBodyCallCode()
        {
            var itemText = MacroBinding.GenerateMacroBodyCallCode(
                AstMacroBodyKind.CompiledBody,
                true
            );

            MacroCodeGenerator.ReportNormal("Compiled Body Call Code", itemText);
        }

        private void GenerateMacro_OptimizedBodyCallCode()
        {
            var itemText = MacroBinding.GenerateMacroBodyCallCode(
                AstMacroBodyKind.OptimizedCompiledBody,
                true
            );

            MacroCodeGenerator.ReportNormal("Optimized Body Call Code", itemText);
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
            
            textBoxDisplay.Text = string.Empty;

            CodeBlock = null;

            if (UpdateMacroBindingTextData() == false)
                return;

            GMacEngineUtils.ResetProgress();

            MacroBinding = BindingTextData.ToMacroBinding(SelectedMacro);

            MacroCodeGenerator = new GMacSingleMacroCSharpComposer(MacroBinding)
            {
                MacroGenDefaults =
                {
                    AllowGenerateMacroCode = true,
                    FixOutputComputationsOrder = 
                        checkBoxFixOutputComputationsOrder.Checked
                }
            };

            GenerateMacro_DslCode();

            GenerateMacro_ParsedBody();

            GenerateMacro_CompiledBody();

            GenerateMacro_OptimizedBody();

            GenerateMacro_RawBodyCallCode();

            GenerateMacro_ParsedBodyCallCode();

            GenerateMacro_CompiledBodyCallCode();

            GenerateMacro_OptimizedBodyCallCode();

            GenerateMacro_SampleTargetCode();

            ProgressHistory =
                MacroCodeGenerator
                .Progress
                .History
                .ReadHistory()
                .ToDictionary(item => item.ProgressId);

            foreach (var item in ProgressHistory)
                listBoxGenerationStage.Items.Add(item.Value.FullTitle);

            CodeBlock = MacroCodeGenerator.CodeBlock;
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
            if (ReferenceEquals(CodeBlock, null))
            {
                MessageBox.Show(@"No code block is present");
                return;
            }

            var formGraphViz = new FormGraphVizEditor(CodeBlock.ToGraphViz());

            formGraphViz.Show(this);
        }

        private void buttonExcel_Click(object sender, EventArgs e)
        {
            if (ReferenceEquals(CodeBlock, null))
            {
                MessageBox.Show(@"No code block is present");
                return;
            }

            saveFileDialog1.CreatePrompt = false;
            saveFileDialog1.OverwritePrompt = true;
            saveFileDialog1.DefaultExt = "xlsx";
            saveFileDialog1.Filter = @"Excel Files (*.xls; *.xlsx)|*.xls;*.xlsx";
            saveFileDialog1.Title = @"Save Excel File As..";
            saveFileDialog1.FileName = CodeBlock.BaseMacro.Name + "-CodeBlock.xlsx";
            if (saveFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                var package = CodeBlock.ToExcel();

                package.SaveAs(new FileInfo(saveFileDialog1.FileName));
            }
        }
    }
}
