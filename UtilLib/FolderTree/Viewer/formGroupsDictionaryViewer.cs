using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UtilLib.FolderTree.Viewer
{
    public partial class FormGroupsDictionaryViewer : Form
    {
        private readonly Dictionary<string, FolderTreeItemsGrouping<string>> _sourceDictionary;


        public FormGroupsDictionaryViewer(IEnumerable<IGrouping<string, FolderTreeItem>> sourceDict)
        {
            InitializeComponent();

            _sourceDictionary = FolderTreeItemsGrouping<string>.CreateDictionary(sourceDict);

            if (_sourceDictionary.Count == 0)
            {
                MessageBox.Show("No groups to display!");
                Close();
                return;
            }

            FillGroupsList();
        }

        public FormGroupsDictionaryViewer(IEnumerable<IGrouping<string, FolderItem>> sourceDict)
        {
            InitializeComponent();

            _sourceDictionary = FolderTreeItemsGrouping<string>.CreateDictionary(sourceDict);

            if (_sourceDictionary.Count == 0)
            {
                MessageBox.Show("No groups to display!");
                Close();
                return;
            }

            FillGroupsList();
        }

        public FormGroupsDictionaryViewer(IEnumerable<IGrouping<string, FileItem>> sourceDict)
        {
            InitializeComponent();

            _sourceDictionary = FolderTreeItemsGrouping<string>.CreateDictionary(sourceDict);

            if (_sourceDictionary.Count == 0)
            {
                MessageBox.Show("No groups to display!");
                Close();
                return;
            }

            FillGroupsList();
        }


        private void SetStatus(string statusText)
        {
            toolStripStatusLabel1.Text = statusText;
            Application.DoEvents();
        }

        private void SetCountersStatus(int groupsCount, int filesCount, int foldersCount, int parentFoldersCount)
        {
            var s = new StringBuilder();

            s.Append(groupsCount.ToString("###,###,##0"))
                .Append(" Group(s), ")
                .Append(filesCount.ToString("###,###,##0"))
                .Append(" File(s), ")
                .Append(foldersCount.ToString("###,###,##0"))
                .Append(" Folder(s), ")
                .Append(parentFoldersCount.ToString("###,###,##0"))
                .Append(" Parent Folder(s).");

            toolStripStatusLabel2.Text = s.ToString();
            Application.DoEvents();
        }

        private void FillGroupsList()
        {
            SetStatus("Reading group names...");

            listBoxItems.Items.Clear();

            if (textBoxFilter.Text.Trim() == "")
            {
                foreach (var group in _sourceDictionary)
                    listBoxItems.Items.Add("<" + group.Key + ">");

                SetStatus("Ready");

                if (listBoxItems.SelectedIndex < 0 && listBoxItems.Items.Count > 0)
                    listBoxItems.SelectedIndex = 0;
                else
                    textBoxItems.Text = "";

                return;
            }

            var partText = textBoxFilter.Text.Trim();
            var displayDict = _sourceDictionary.Where(group => group.Key.Contains(partText));

            foreach (var group in displayDict)
                listBoxItems.Items.Add("<" + group.Key + ">");

            SetStatus("Ready");

            if (listBoxItems.SelectedIndex < 0 && listBoxItems.Items.Count > 0)
                listBoxItems.SelectedIndex = 0;
            else
                textBoxItems.Text = "";
        }

        private FolderTreeItemsGrouping<string> SelectedItemIndexToGroup(int index)
        {
            var groupId = (string)(listBoxItems.SelectedItems[index]);
            groupId = groupId.Substring(1, groupId.Length - 2);

            return _sourceDictionary[groupId];
        }

        private void UpdateItemsText()
        {
            textBoxItems.Text = "";

            if (listBoxItems.SelectedItems.Count < 1)
            {
                SetCountersStatus(0, 0, 0, 0);
                return;
            }

            SetStatus("Reading group items...");

            var pathsList = new List<string>();
            var s = new StringBuilder();
            FolderTreeItemsGrouping<string> itemsGroup;

            //Test if only one group is selected
            if (listBoxItems.SelectedItems.Count == 1)
                itemsGroup = SelectedItemIndexToGroup(0);

            else
            {
                //Multiple groups selected
                //Create a copy of the first selected group
                itemsGroup = FolderTreeItemsGrouping<string>.CreateCopy(SelectedItemIndexToGroup(0));

                //Merge the data of the remaining groups into the copy of the first selected group
                for (var i = 1; i < listBoxItems.SelectedItems.Count; i++)
                    itemsGroup = itemsGroup.MergeItems(SelectedItemIndexToGroup(i));
            }

            var filesCount = itemsGroup.FilesCount;
            var foldersCount = itemsGroup.FoldersCount;
            var parentFoldersCount = itemsGroup.ParentFoldersCount;

            if (checkBoxShowParentFolders.Checked)
                pathsList.AddRange(itemsGroup.ParentFolders().Select(item => item.ItemPath));

            if (checkBoxShowFolders.Checked)
                pathsList.AddRange(itemsGroup.Folders().Select(item => item.ItemPath));

            if (checkBoxShowFiles.Checked)
                pathsList.AddRange(itemsGroup.Files().Select(item => item.ItemPath));

            pathsList.Sort();

            //Write the data of the group to the string builder
            s.Append("group <");
            s.Append(itemsGroup.Key);
            s.AppendLine(">");
            s.AppendLine("{");

            if (pathsList.Count > 0)
                foreach (var path in pathsList)
                    s.Append("   ").AppendLine(path);

            s.AppendLine("}");
            s.AppendLine();

            textBoxItems.Text = s.ToString();

            SetCountersStatus(listBoxItems.SelectedItems.Count, filesCount, foldersCount, parentFoldersCount);

            SetStatus("Ready");
        }


        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonCopyItems_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBoxItems.Text);
        }

        private void buttonRefreshList_Click(object sender, EventArgs e)
        {
            FillGroupsList();
        }

        private void listBoxItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateItemsText();
        }

        private void checkBoxShowFiles_CheckedChanged(object sender, EventArgs e)
        {
            UpdateItemsText();
        }

        private void checkBoxShowFolders_CheckedChanged(object sender, EventArgs e)
        {
            UpdateItemsText();
        }

        private void checkBoxShowParentFolders_CheckedChanged(object sender, EventArgs e)
        {
            UpdateItemsText();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            saveFileDialog1.OverwritePrompt = true;
            saveFileDialog1.SupportMultiDottedExtensions = true;
            saveFileDialog1.Filter = "All Files|*.*";

            if (saveFileDialog1.ShowDialog(this) != DialogResult.OK) 
                return;

            try
            {
                File.WriteAllText(saveFileDialog1.FileName, textBoxItems.Text);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
    }
}
