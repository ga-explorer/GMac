using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TextComposerLib.Text;

namespace GMac.GMacIDE.Editor
{
    //TODO: Modify this form to be more practical:
    // Add "Project Folder" text box
    // Make project files from same folder inside a list, not a text box
    // Add buttons to manipulate order of project files, add and remove files, etc.
    public partial class FormGMacDslSourceFilesEditor : Form
    {
        private readonly List<string> _sourceFilesList;

        
        internal string DefaultExt { get; set; }

        internal string Filter { get; set; }


        public IEnumerable<string> FilePaths => _sourceFilesList;

        public int FilesCount => _sourceFilesList.Count;


        public FormGMacDslSourceFilesEditor(IEnumerable<string> filePathList)
        {
            InitializeComponent();

            _sourceFilesList = new List<string>(filePathList);

            DefaultExt = "*.gmac";

            Filter = "GMacDSL Code Files|*.gmac|All Files|*.*";
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            var s = new StringBuilder();
            var fileNames = new List<string>();

            var fileNameList =
                textBoxFilesList
                .Lines
                .Where(fileName => string.IsNullOrEmpty(fileName.Trim()) == false);

            foreach (var fileName in fileNameList)
                if (File.Exists(fileName))
                    fileNames.Add(fileName);
                else
                    s.AppendLine("File <" + fileName + "> does not exist!");

            if (s.Length == 0)
            {
                _sourceFilesList.Clear();
                _sourceFilesList.AddRange(fileNames.Distinct());
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show(s.ToString());
            }
        }

        private void formEditDSLSourceFiles_Load(object sender, EventArgs e)
        {
            if (_sourceFilesList.Count > 0)
                textBoxFilesList.Text =
                    _sourceFilesList.Concatenate(Environment.NewLine, "", Environment.NewLine);

            textBoxFilesList.Focus();
        }

        private void buttonInsertFiles_Click(object sender, EventArgs e)
        {
            openFileDialog1.Reset();
            openFileDialog1.DefaultExt = DefaultExt;
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.Filter = Filter;
            openFileDialog1.Multiselect = true;
            openFileDialog1.ValidateNames = true;
            openFileDialog1.Title = @"Insert Existing Source Files...";

            if (openFileDialog1.ShowDialog(this) != DialogResult.OK) 
                return;

            var s = new StringBuilder();

            foreach (var fileName in openFileDialog1.FileNames)
                s.AppendLine(fileName);

            textBoxFilesList.SelectionStart = textBoxFilesList.GetFirstCharIndexOfCurrentLine();

            textBoxFilesList.Paste(s.ToString());

            textBoxFilesList.Focus();
        }

        private void buttonInsertNewFile_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Reset();
            saveFileDialog1.DefaultExt = DefaultExt;
            saveFileDialog1.OverwritePrompt = true;
            saveFileDialog1.Filter = Filter;
            saveFileDialog1.SupportMultiDottedExtensions = true;
            saveFileDialog1.ValidateNames = true;
            saveFileDialog1.Title = @"Create New Source File...";

            if (saveFileDialog1.ShowDialog(this) != DialogResult.OK) 
                return;

            try
            {
                File.WriteAllText(saveFileDialog1.FileName, @"");

                textBoxFilesList.SelectionStart = textBoxFilesList.GetFirstCharIndexOfCurrentLine();

                textBoxFilesList.Paste(saveFileDialog1.FileName);

                textBoxFilesList.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
