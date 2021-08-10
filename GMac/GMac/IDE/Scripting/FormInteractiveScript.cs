using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CodeComposerLib.Irony.Semantic.Symbol;
using FastColoredTextBoxNS;
using GMac.Engine;
using GMac.Engine.AST;
using GMac.Engine.Compiler.Semantic.AST;
using GMac.Engine.Compiler.Semantic.ASTConstants;
using GMac.Engine.Scripting;
using TextComposerLib;
using TextComposerLib.Loggers.Progress;
using TextComposerLib.WinForms.UserInterface.UI;

namespace GMac.IDE.Scripting
{
    public partial class FormInteractiveScript : Form
    {
        private string _scriptFilePath;

        private readonly GMacAst _gmacAst;

        private GMacScriptManager _gmacScript;

        private readonly List<string> _usedNamespaces = new List<string>(GMacScriptManager.DefaultNamespacesList);

        private bool _scriptChanged;

        internal bool ScriptChanged
        {
            get { return _scriptChanged; }
            private set
            {
                if (_scriptChanged == value) return;

                _scriptChanged = value;

                Text = @"Interactive Script" + (_scriptChanged ? @"(*)" : "");
            }
        }


        internal FormInteractiveScript(GMacAst ast)
        {
            InitializeComponent();

            _gmacAst = ast;

            ResetScriptManager();
        }


        private void ResetScriptManager()
        {
            _scriptFilePath = "";

            _gmacScript = new GMacScriptManager(new AstRoot(_gmacAst));

            textBoxScript.Text = string.Empty;
            textBoxMembers.Text = string.Empty;

            listBoxAllNamespaces.Items.Clear();
            listBoxAllNamespaces.Items.AddRange(
                NamespacesUtils.Namespaces.Cast<object>().ToArray()
                );

            listBoxUsedNamespaces.Items.Clear();
            listBoxUsedNamespaces.Items.AddRange(
                _usedNamespaces.Cast<object>().ToArray()
                );

            ShowMainSymbols();
        }

        private static string ConvertSymbolRoleName(string roleName)
        {
            switch (roleName)
            {
                case RoleNames.Namespace:
                    return "Namespaces";

                case RoleNames.Frame:
                    return "Frames";

                case RoleNames.FrameMultivector:
                    return "Frame Multivectors";

                case RoleNames.FrameBasisVector:
                    return "Frame Basis Vectors";

                case RoleNames.FrameSubspace:
                    return "Frame Subspaces";

                case RoleNames.Constant:
                    return "Constants";

                case RoleNames.Structure:
                    return "Structures";

                case RoleNames.Macro:
                    return "Macros";
            }

            return roleName;
        }

        private void ShowMainSymbols()
        {
            treeViewComponents.Nodes.Clear();

            var symbolGroups =
                _gmacScript.Root
                .AssociatedAst
                .MainSymbols()
                .GroupBy(item => item.SymbolRoleName);

            foreach (var symbolGroup in symbolGroups)
            {
                var node = new TreeNode(ConvertSymbolRoleName(symbolGroup.Key));

                foreach (var symbol in symbolGroup)
                    node.Nodes.Add(
                        new TreeNode(symbol.SymbolAccessName) { Tag = symbol }
                        );

                treeViewComponents.Nodes.Add(node);
            }
        }

        private void ShowInterpreterRequests()
        {
            var s = new StringBuilder();

            var iprCommands = 
                _gmacScript
                .Progress
                .History
                .ReadHistory()
                .Where(p => p.SourceId == "GMac Script Interpreter");

            foreach (var iprCommand in iprCommands)
            {
                var details = iprCommand.Details.Trim();

                s.Append("<")
                    .Append(iprCommand.ResultText)
                    .Append("> ")
                    .Append(iprCommand.Title);

                if (details.IsEmptyOrSingleLine())
                    s.Append(' ').AppendLine(details);
                else
                    s.AppendLine().Append(iprCommand.Details);

                s.AppendLine();
            }

            textBoxCommands.Text = s.ToString();
        }

        private void ShowOutput()
        {
            textBoxOutput.Text = _gmacScript.Ipr.Output.Log.ToString();
        }

        private void SetScriptFromInterface()
        {
            GMacEngineUtils.ResetProgress();

            textBoxOutput.Text = string.Empty;
            textBoxCSharpCode.Text = string.Empty;
            listViewErrors.Items.Clear();
            textBoxErrorDetails.Text = string.Empty;

            _gmacScript.SetScript(
                textBoxScript.Text,
                textBoxMembers.Text,
                _usedNamespaces,
                Enumerable.Empty<string>()
                );
        }

        private void SetInterfaceFromScript()
        {
            GMacEngineUtils.ResetProgress();

            textBoxOutput.Text = string.Empty;
            textBoxCSharpCode.Text = string.Empty;
            listViewErrors.Items.Clear();
            textBoxErrorDetails.Text = string.Empty;

            textBoxScript.Text = _gmacScript.ScriptText;
            textBoxMembers.Text = _gmacScript.ScriptClassMembersText;

            _usedNamespaces.Clear();
            _usedNamespaces.AddRange(GMacScriptManager.DefaultNamespacesList);

            if (_gmacScript.OpenedNamespaces != null)
                _usedNamespaces.AddRange(
                    _gmacScript
                    .OpenedNamespaces
                    .Where(ns => _usedNamespaces.Contains(ns) == false)
                    );
        }

        private void ShowErrors()
        {
            listViewErrors.Items.Clear();

            var progressLog = 
                _gmacScript
                .Progress
                .History
                .ReadHistory()
                .Where(p => p.Kind == ProgressEventArgsKind.Error || p.Result == ProgressEventArgsResult.Failure);

            foreach (var progressItem in progressLog)
            {
                var item = listViewErrors.Items.Add(progressItem.Source.ProgressSourceId);

                item.Tag = progressItem;

                item.SubItems.Add(progressItem.Title);
            }
        }

        private void GenerateScript()
        {
            SetScriptFromInterface();

            _gmacScript.GenerateScript();

            textBoxCSharpCode.Text = _gmacScript.ScriptCode;

            if (_gmacScript.Progress.History.HasErrorsOrFailures)
            {
                ShowErrors();
                tabControlLower.SelectedTab = tabPageErrors;
                listViewErrors.Focus();
            }

            tabControlUpper.SelectedTab = tabPageCSharpCode;
            textBoxCSharpCode.Focus();
        }

        private void CompileScript()
        {
            SetScriptFromInterface();

            _gmacScript.CompileScript();

            textBoxCSharpCode.Text = _gmacScript.ScriptCode;

            if (_gmacScript.Progress.History.HasErrorsOrFailures)
            {
                ShowErrors();
                tabControlLower.SelectedTab = tabPageErrors;
                listViewErrors.Focus();
            }

            tabControlUpper.SelectedTab = tabPageCSharpCode;
            textBoxCSharpCode.Focus();
        }

        private void ExecuteScript()
        {
            SetScriptFromInterface();

            _gmacScript.ExecuteScript();

            textBoxCSharpCode.Text = _gmacScript.ScriptCode;

            if (_gmacScript.Progress.History.HasErrorsOrFailures == false)
            {
                //Script generation, compilation, and execution are all Ok
                ShowInterpreterRequests();

                ShowOutput();

                tabControlLower.SelectedTab = tabPageOutput;
                textBoxOutput.Focus();
            }
            else if (_gmacScript.ScriptCompilationComplete)
            {
                //Script generation and compilation are Ok, error during script execution
                ShowInterpreterRequests();

                ShowErrors();
                tabControlLower.SelectedTab = tabPageErrors;
                listViewErrors.Focus();
            }
            else
            {
                //Error during script generation or compilation
                tabControlUpper.SelectedTab = tabPageCSharpCode;
                textBoxCSharpCode.Focus();

                ShowErrors();
                tabControlLower.SelectedTab = tabPageErrors;
                listViewErrors.Focus();
            }
        }


        private void treeViewComponents_DoubleClick(object sender, EventArgs e)
        {
            var symbol = treeViewComponents.SelectedNode.Tag as LanguageSymbol;

            if (ReferenceEquals(symbol, null))
                return;

            //TODO: Select appropriate symbol ref command here
            textBoxScript.InsertText("[@ " + symbol.SymbolAccessName + " |> frame @]");
        }

        private void buttonAddSelected_Click(object sender, EventArgs e)
        {
            //Add all selected namespaces not already in the used namespaces list
            var selectedNamespaces = 
                listBoxAllNamespaces
                .SelectedItems
                .Cast<string>()
                .Where(nameSpace => _usedNamespaces.Contains(nameSpace) == false);

            _usedNamespaces.AddRange(selectedNamespaces);

            listBoxUsedNamespaces.BeginUpdate();
            listBoxUsedNamespaces.Items.Clear();
            listBoxUsedNamespaces.Items.AddRange(_usedNamespaces.Cast<object>().ToArray());
            listBoxUsedNamespaces.EndUpdate();

            ScriptChanged = true;
        }

        private void buttonRemoveNamespaces_Click(object sender, EventArgs e)
        {
            //Remove all selected namespaces not in the default namespaces list (i.e. the essential namespaces)
            var selectedNamespaces =
                listBoxAllNamespaces
                .SelectedItems
                .Cast<string>()
                .Where(nameSpace => GMacScriptManager.DefaultNamespacesList.Contains(nameSpace) == false);

            foreach (var nameSpace in selectedNamespaces)
                _usedNamespaces.Remove(nameSpace);

            listBoxUsedNamespaces.BeginUpdate();
            listBoxUsedNamespaces.Items.Clear();
            listBoxUsedNamespaces.Items.AddRange(_usedNamespaces.Cast<object>().ToArray());
            listBoxUsedNamespaces.EndUpdate();

            ScriptChanged = true;
        }

        private void menuItemScript_Generate_Click(object sender, EventArgs e)
        {
            GenerateScript();
        }

        private void menuItemScript_Compile_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = @"Compiling Script...";
            Application.DoEvents();

            CompileScript();

            toolStripStatusLabel1.Text = @"Ready";
        }

        private void menuItemScript_Execute_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = @"Executing Script...";
            Application.DoEvents();

            ExecuteScript();

            toolStripStatusLabel1.Text = @"Ready";
        }

        private void listViewErrors_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewErrors.SelectedItems.Count == 0) return;

            var progressItem = listViewErrors.SelectedItems[0].Tag as ProgressEventArgs;

            if (ReferenceEquals(progressItem, null)) return;

            textBoxErrorDetails.Text = progressItem.ToString();
        }

        private void menuItemView_Progress_Click(object sender, EventArgs e)
        {
            var formProgress = new FormProgress(_gmacScript.Progress);
            formProgress.ShowDialog(this);
        }

        private void menuItemFile_Save_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_scriptFilePath))
            {
                if (saveFileDialog1.ShowDialog(this) == DialogResult.Cancel)
                    return;

                _scriptFilePath = saveFileDialog1.FileName;
            }

            try
            {
                SetScriptFromInterface();
                _gmacScript.SaveScript(_scriptFilePath);
                ScriptChanged = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void menuItemFile_SaveAs_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog(this) == DialogResult.Cancel)
                return;

            _scriptFilePath = saveFileDialog1.FileName;

            SaveScript();
        }

        private bool SaveScript()
        {
            if (string.IsNullOrEmpty(_scriptFilePath))
            {
                if (saveFileDialog1.ShowDialog(this) == DialogResult.Cancel)
                    return true;

                _scriptFilePath = saveFileDialog1.FileName;
            }

            try
            {
                SetScriptFromInterface();
                _gmacScript.SaveScript(_scriptFilePath);
                ScriptChanged = false;

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                return false;
            }
        }

        private bool AskToSaveScript()
        {
            if (ScriptChanged == false) return true;

            var result = MessageBox.Show(@"Save script to file first?", @"Script", MessageBoxButtons.YesNoCancel);

            if (result == DialogResult.Cancel)
                return false;

            if (result == DialogResult.Yes)
                SaveScript();

            return true;
        }

        private void menuItemFile_Open_Click(object sender, EventArgs e)
        {
            if (AskToSaveScript() == false)
                return;

            if (openFileDialog1.ShowDialog(this) == DialogResult.Cancel)
                return;

            _scriptFilePath = openFileDialog1.FileName;

            try
            {
                _gmacScript.LoadScript(_scriptFilePath);
                SetInterfaceFromScript();
                ScriptChanged = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void menuItemFile_New_Click(object sender, EventArgs e)
        {
            if (AskToSaveScript() == false)
                return;

            ResetScriptManager();
            SetInterfaceFromScript();
            ScriptChanged = false;
        }

        private void textBoxScript_TextChanged(object sender, TextChangedEventArgs e)
        {
            ScriptChanged = true;
        }

        private void textBoxMembers_TextChanged(object sender, TextChangedEventArgs e)
        {
            ScriptChanged = true;
        }

        private void FormInteractiveScript_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (AskToSaveScript() == false)
                e.Cancel = true;
        }
    }
}
