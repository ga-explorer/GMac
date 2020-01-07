using System;
using System.Windows.Forms;

namespace UtilLib.FolderTree.Viewer
{
    public partial class FormPrdefinedGroupings : Form
    {
        private FolderItem _hierarchy;
        private bool _rootFoldersChanged;
        private bool _readingHierarchy;

        public FormPrdefinedGroupings()
        {
            InitializeComponent();

            //_Hierarchy.RaiseBeginProcessFolderChildrenEvent += _Hierarchy_BeginProcessFolderChildrenEvent;

            FillGroupingsList();

            UpdateInterface();
        }

        private void FillGroupingsList()
        {
            foreach (var grouping in PredefinedGrouping.GroupingsList)
                listBoxGroupings.Items.Add(grouping.DisplayName);
        }

        private void SetStatus(string statusText)
        {
            toolStripStatusLabel1.Text = statusText;
            Application.DoEvents();
        }

        private void UpdateInterface()
        {
            textBoxFilesPattern.Enabled = (_readingHierarchy == false && checkBoxReadFiles.Checked);
            textBoxFoldersPattern.Enabled = (_readingHierarchy == false && checkBoxReadFolders.Checked);
            textBoxRootFolderPath.Enabled = (_readingHierarchy == false);
            listBoxGroupings.Enabled = (_readingHierarchy == false && _rootFoldersChanged == false);

            buttonAddRootFolder.Enabled = (_readingHierarchy == false);
            buttonClose.Enabled = (_readingHierarchy == false);
            buttonCreateGrouping.Enabled = (_readingHierarchy == false && _hierarchy != null);
            buttonReadHierarchy.Enabled = (textBoxRootFolderPath.Text.Trim() != String.Empty);

            buttonReadHierarchy.Text = _readingHierarchy ? "Stop Reading" : "Read Hierarchy";

            Application.DoEvents();
        }

        //void _Hierarchy_BeginProcessFolderChildrenEvent(object sender, FileSystemHierarchy.BeginProcessFolderChildrenEventArgs e)
        //{
        //    this.SetStatus("Reading... " + e.ParentFolder.ItemPath);
        //}

        private void buttonReadHierarchy_Click(object sender, EventArgs e)
        {
            //if (_ReadingHierarchy)
            //{
            //    _Hierarchy.RequestStopReadHierarchyFromDisk();
            //    return;
            //}

            _readingHierarchy = true;
            UpdateInterface();

            _hierarchy = FolderItem.CreateMultiRoot(textBoxRootFolderPath.Lines);
            //TODO: Find a way to search multiple distinct root folders
            //foreach (string root_path in textBoxRootFolderPath.Lines)
            //    if (root_path.Trim() != "" && Directory.Exists(root_path))
            //        _Hierarchy = FolderItem.CreateRoot(root_path);
            //        //_Hierarchy.AddActiveRootFolder(root_path);

            if (checkBoxReadFiles.Checked && checkBoxReadFolders.Checked)
                _hierarchy.AddDescendantItemsFromDisk(textBoxFilesPattern.Text, textBoxFoldersPattern.Text);
            
            else if (checkBoxReadFolders.Checked)
                _hierarchy.AddDescendantFoldersFromDisk(textBoxFoldersPattern.Text);

            else if (checkBoxReadFiles.Checked)
                _hierarchy.AddChildFilesFromDisk(textBoxFoldersPattern.Text);

            SetStatus("Ready");

            _readingHierarchy = false;
            _rootFoldersChanged = false;
            UpdateInterface();
        }

        private void buttonAddRootFolder_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.Description = "Browse for Root Folder";
            folderBrowserDialog1.ShowNewFolderButton = false;

            var result = folderBrowserDialog1.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                textBoxRootFolderPath.Text += folderBrowserDialog1.SelectedPath + Environment.NewLine;
                _rootFoldersChanged = true;
            }

            UpdateInterface();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBoxRootFolderPath_TextChanged(object sender, EventArgs e)
        {
            if (_rootFoldersChanged)
                return;

            _rootFoldersChanged = true;
            UpdateInterface();
        }

        private void buttonCreateGrouping_Click(object sender, EventArgs e)
        {
            if (listBoxGroupings.SelectedIndex < 0)
            {
                MessageBox.Show("No Grouping Selected");
                return;
            }

            SetStatus("Constructing grouping...");

            var grouping = PredefinedGrouping.GroupingsList[listBoxGroupings.SelectedIndex];

            var dict = grouping.GroupingFunction(_hierarchy);

            var fromViewer = new FormGroupsDictionaryViewer(dict);

            SetStatus("Ready");

            fromViewer.ShowDialog(this);
        }

        private void listBoxGroupings_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxGroupingDescription.Text = 
                listBoxGroupings.SelectedIndex < 0 
                ? String.Empty
                : PredefinedGrouping.GroupingsList[listBoxGroupings.SelectedIndex].Description;
        }
    }
}
