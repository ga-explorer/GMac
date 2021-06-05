using System;
using System.IO;
using System.Windows.Forms;
using TextComposerLib.WinForms.UserInterface.UI;

namespace GMac.CodeComposers.GradedMultivectorsLibraryComposer
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();

            InitializeInterface();

            var outputsFolder =
                Path.Combine(Directory.GetCurrentDirectory(), "Outputs");

            textBoxOutputFolder.Text = outputsFolder;
        }


        private void InitializeInterface()
        {
            for (var i = 0; i < LibraryComposerFactory.GMacDslCodeList.Count; i++)
                comboBoxGMacDslCode.Items.Add($"Code Sample {i + 1}");

            if (comboBoxGMacDslCode.Items.Count > 0)
                comboBoxGMacDslCode.SelectedIndex = 0;

            comboBoxTargetLanguage.Items.Add("C#");
            comboBoxTargetLanguage.SelectedIndex = 0;
        }

        private void buttonComposeCode_Click(object sender, EventArgs e)
        {
            //if (Directory.Exists(textBoxOutputFolder.Text))
            //{
            //    var result = MessageBox.Show(
            //        @"Output folder exists, all files and subfolders will be deleted", 
            //        @"Delete Confirmation", 
            //        MessageBoxButtons.OKCancel, 
            //        MessageBoxIcon.Question
            //    );

            //    if (result == DialogResult.Cancel)
            //        return;

            //    try
            //    {
            //        Directory.Delete(textBoxOutputFolder.Text, true);
            //    }
            //    catch (Exception e1)
            //    {
            //        MessageBox.Show(
            //            @"Error deleting output folder contents: " + 
            //                Environment.NewLine + e1.Message, 
            //            @"Error", 
            //            MessageBoxButtons.OK, 
            //            MessageBoxIcon.Error
            //        );

            //        return;
            //    }
            //}
            //else
            //{
            //    try
            //    {
            //        Directory.CreateDirectory(textBoxOutputFolder.Text);
            //    }
            //    catch (Exception e2)
            //    {
            //        MessageBox.Show(
            //            @"Error creating output folder: " + 
            //            Environment.NewLine + e2.Message, 
            //            @"Error", 
            //            MessageBoxButtons.OK, 
            //            MessageBoxIcon.Error
            //        );

            //        return;
            //    }
            //}

            toolStripStatusLabel1.Text = @"Composing Code...";

            //Select the GMacDSL source code to compose the library from
            var dslCode = LibraryComposerFactory.GMacDslCodeList[
                comboBoxGMacDslCode.SelectedIndex
            ];

            //Begin composing the library and get a FilesComposer object containing the generated 
            //source code
            var filesComposer = 
                LibraryComposerFactory.ComposeLibrary(
                    dslCode,
                    textBoxOutputFolder.Text,
                    checkBoxGenerateMacroCode.Checked,
                    comboBoxTargetLanguage.Text
                    );

            toolStripStatusLabel1.Text = @"Ready";

            if (filesComposer == null) return;

            //MessageBox.Show(@"Target code files saved successfully", @"Files Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //Show final generated files and read their contents from disk
            var formFiles = new FormFilesComposer(filesComposer);
            formFiles.ShowDialog(this);
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog(this) == DialogResult.Cancel)
                return;

            textBoxOutputFolder.Text = folderBrowserDialog1.SelectedPath;
        }

        private void comboBoxGMacDslCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxGMacDslCode.SelectedIndex >= 0)
                textBoxGMacDslCode.Text = 
                    LibraryComposerFactory.GMacDslCodeList[comboBoxGMacDslCode.SelectedIndex];
        }
    }
}
