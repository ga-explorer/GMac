using System;
using System.Windows.Forms;
using GMac.GMacAPI.CodeGen;
using GMac.GMacAST;
using GMac.GMacCompiler;
using GMac.GMacUtils;
using GMacSamples.CodeGen.Multivectors;
using GMacSamples.DSLCode;
using TextComposerLib.Files.UI;
using TextComposerLib.Logs.Progress.UI;

namespace GMacSamples.CodeGen
{
    public partial class CodeGenSamples : Form
    {
        public CodeGenSamples()
        {
            InitializeComponent();

            InitializeTasks();
        }

        private AstRoot BeginCompilation(string dslCode)
        {
            //GMacSystemUtils.InitializeGMac();

            //Compile GMacDSL code into GMacAST
            var compiler = GMacProjectCompiler.CompileDslCode(dslCode, Application.LocalUserAppDataPath, "tempTask");

            compiler.Progress.DisableAfterNextReport = true;

            if (compiler.Progress.History.HasErrorsOrFailures)
            {
                var formProgress = new FormProgress(compiler.Progress, null, null);
                formProgress.ShowDialog(this);

                return null;
            }

            return compiler.Root;
        }

        private void BeginCodeGeneration(GMacCodeLibraryComposer libGen)
        {
            var formProgress = new FormProgress(libGen.Progress, libGen.Generate, null);
            formProgress.ShowDialog(this);

            var formFiles = new FormFilesComposer(libGen.CodeFilesComposer);
            formFiles.ShowDialog(this);
        }

        #region Tasks
        private void InitializeTasks()
        {
            listBoxTasks.Items.Add(
                new CodeGenSampleTask(
                    @"Multivector Library 1",
                    @"Generate a library for general operations on multivectors. Multivectors are represented sparsely by grades",
                    Task2
                    )
                );
        }

        private string Task2()
        {
            var ast = BeginCompilation(DslCodeSamples.Sample1);

            if (ReferenceEquals(ast, null)) return "";

            //Create and initialize code generator
            var activeGenerator = new MvLibrary(ast)
            {
                MacroGenDefaults = {AllowGenerateMacroCode = checkBoxGenerateMacroCode.Checked}
            };


            activeGenerator.SelectedSymbols.SetSymbols(ast.Frames);

            BeginCodeGeneration(activeGenerator);

            return "";
        }
        #endregion


        private void buttonExecTask_Click(object sender, EventArgs e)
        {
            textBoxResults.Text = String.Empty;

            var task = listBoxTasks.SelectedItem as CodeGenSampleTask;

            if (ReferenceEquals(task, null))
            {
                MessageBox.Show(
                    @"Please select a task to execute",
                    @"Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation
                    );

                return;
            }

            GMacSystemUtils.ResetProgress();

            textBoxResults.Text = task.TaskAction();
        }

        private void listBoxTasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxResults.Text = String.Empty;
            textBoxTaskDescription.Text = String.Empty;

            var task = listBoxTasks.SelectedItem as CodeGenSampleTask;

            if (ReferenceEquals(task, null))
                return;

            textBoxTaskDescription.Text = task.TaskDescription;
        }
    }
}
