using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace GMac.GMacIDE.Editor
{
    public partial class FormTargetSourceFilesEditor : Form
    {
        public List<string> FileNames = new List<string>();

        public FormTargetSourceFilesEditor()
        {
            InitializeComponent();
        }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            FileNames.Clear();

            foreach (ListViewItem item in listViewFiles.Items)
                FileNames.Add(item.Tag as string);
            
            DialogResult = DialogResult.OK;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private static bool FileContainsGMacBindingPoint(string fileName)
        {
            var code = File.ReadAllText(fileName);
            var regex = new Regex(@"\bGMac\s*<\s*\w+(\.\w+)*\s*>\s*begin", RegexOptions.Singleline);
            return regex.IsMatch(code);
        }

        private bool CanAddFile(string fileName)
        {
            //Test if file is not already present in the list
            var flag = 
                listViewFiles
                .Items
                .Cast<ListViewItem>()
                .All(item => (item.Tag as string) != fileName);

            //Test if file satisfies size condition
            int maxSize;
            int.TryParse(textBoxMaxFileSize.Text, out maxSize);

            if (flag && maxSize > 0)
            {
                maxSize *= 1024;

                var a = new FileInfo(fileName);
                if (a.Length > maxSize)
                    flag = false;
            }

            //Test if file satisfies GMac binding point condition
            if (flag && checkBoxGMacOny.Checked)
            {
                flag = FileContainsGMacBindingPoint(fileName);
            }

            return flag;
        }

        private void TryAddFiles(IEnumerable<string> fileNames)
        {
            foreach (var fileName in fileNames)
                if (CanAddFile(fileName))
                {
                    var fileNumber = listViewFiles.Items.Count + 1;
                    var item = listViewFiles.Items.Add(fileNumber.ToString());//.PadLeft(5));

                    item.SubItems.Add(Path.GetFileName(fileName));
                    item.SubItems.Add(Path.GetDirectoryName(fileName));

                    item.Tag = fileName;
                }
        }

        private void buttonAddFiles_Click(object sender, EventArgs e)
        {
            var fileFilter = textBoxFileSearchPattern.Text.Trim();

            fileFilter = (fileFilter == "") ? "*.*" : fileFilter;

            openFileDialog1.Reset();
            openFileDialog1.DefaultExt = "";
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.Filter = @"Target Source Files (" + fileFilter + @")|" + fileFilter;
            openFileDialog1.Multiselect = true;
            openFileDialog1.SupportMultiDottedExtensions = true;
            openFileDialog1.ValidateNames = true;
            openFileDialog1.Title = @"Insert Target Source Files...";

            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                TryAddFiles(openFileDialog1.FileNames);

                listViewFiles.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                listViewFiles.Focus();
            }

            DialogResult = DialogResult.None;
        }

        private void buttonAddFilesInFolder_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.Description = @"Select Folder";
            folderBrowserDialog1.RootFolder = Environment.SpecialFolder.Desktop;
            folderBrowserDialog1.ShowNewFolderButton = false;

            if (folderBrowserDialog1.ShowDialog(this) == DialogResult.OK)
            {
                var op = checkBoxSearchSubfolders.Checked ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

                var fileNames = Directory.GetFiles(folderBrowserDialog1.SelectedPath, textBoxFileSearchPattern.Text, op);

                TryAddFiles(fileNames);

                listViewFiles.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                listViewFiles.Focus();
            }

            DialogResult = DialogResult.None;
        }

        private void buttonRemoveFiles_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewFiles.SelectedItems)
                listViewFiles.Items.Remove(item);

            var i = 1;
            foreach (ListViewItem item in listViewFiles.Items)
            {
                item.SubItems[0].Text = i.ToString();//.PadLeft(5);
                i++;
            }

            listViewFiles.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listViewFiles.Focus();

            DialogResult = DialogResult.None;
        }
    }
}
