using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using GMac.GMacAPI.CodeGen;
using GMac.GMacAPI.CodeGen.BuiltIn.CSharp.Direct;
using GMac.GMacAPI.CodeGen.BuiltIn.GMac.GMacFrame;
using GMac.GMacAST;
using GMac.GMacCompiler;
using GMac.GMacCompiler.Semantic.AST;
using GMac.GMacIDE.AstExplorer;
using GMac.GMacIDE.CodeGen;
using GMac.GMacIDE.Scripting;
using GMac.GMacIDE.Tools;
using IronyGrammars.Semantic.Symbol;
using IronyGrammars.SourceCode;
using TextComposerLib;

namespace GMac.GMacIDE.Editor
{
    public partial class FormGMacDslEditor : Form
    {
        #region Interface Members

        /// <summary>
        /// The project data (the main input of the GMac compiler)
        /// </summary>
        private GMacProject _dslProject;

        /// <summary>
        /// The GMacDSL compiler
        /// </summary>
        private GMacProjectCompiler _dslCompiler = new GMacProjectCompiler();

        /// <summary>
        /// The root AST node for the compiled GMacDSL code
        /// </summary>
        private GMacAst _astRoot;

        /// <summary>
        /// The source code file that is being edited
        /// </summary>
        private ISourceCodeUnit _openedDslFile;

        /// <summary>
        /// True when the text changes in the source code text editor
        /// </summary>
        private bool _editorTextChangedStatus;

        //styles
        private readonly TextStyle _blueStyle = new TextStyle(Brushes.Blue, null, FontStyle.Regular);
        private readonly TextStyle _boldStyle = new TextStyle(null, null, FontStyle.Bold);// | FontStyle.Underline);
        private readonly TextStyle _grayStyle = new TextStyle(Brushes.Gray, null, FontStyle.Regular);
        private readonly TextStyle _magentaStyle = new TextStyle(Brushes.Magenta, null, FontStyle.Regular);
        private readonly TextStyle _greenStyle = new TextStyle(Brushes.Green, null, FontStyle.Italic);
        private readonly TextStyle _brownStyle = new TextStyle(Brushes.Brown, null, FontStyle.Italic);
        //private TextStyle _maroonStyle = new TextStyle(Brushes.Maroon, null, FontStyle.Regular);
        private readonly MarkerStyle _sameWordsStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(40, Color.Gray)));

        #endregion


        public FormGMacDslEditor()
        {
            InitializeComponent();

            InitializeTextEditorStyles();

            UpdateInterface_EnableControls();
        }


        #region Interface Control Methods

        private void InitializeTextEditorStyles()
        {
            SourceCodeTextEditor.ClearStylesBuffer();

            ////styles
            //private TextStyle BlueStyle = new TextStyle(Brushes.Blue, null, FontStyle.Regular);
            //private TextStyle BoldStyle = new TextStyle(null, null, FontStyle.Bold);// | FontStyle.Underline);
            //private TextStyle GrayStyle = new TextStyle(Brushes.Gray, null, FontStyle.Regular);
            //private TextStyle MagentaStyle = new TextStyle(Brushes.Magenta, null, FontStyle.Regular);
            //private TextStyle GreenStyle = new TextStyle(Brushes.Green, null, FontStyle.Italic);
            //private TextStyle BrownStyle = new TextStyle(Brushes.Brown, null, FontStyle.Italic);
            //private TextStyle MaroonStyle = new TextStyle(Brushes.Maroon, null, FontStyle.Regular);

            //add this style explicitly for drawing under other styles
            SourceCodeTextEditor.AddStyle(_sameWordsStyle);
        }

        private void InitializeProject(GMacProject gmacDslProject)
        {
            _dslProject = gmacDslProject;
            _dslCompiler = new GMacProjectCompiler();
            _astRoot = null;

            ResetCompilationInterface();

            ChangeOpenedSourceFile(null);
        }

        private void ResetCompilationInterface()
        {
            listViewErrors.Items.Clear();

            listViewWarnings.Items.Clear();

            tabControl2.SelectedTab = tabPageOutput;

            textBoxOutputLog.Text = string.Empty;

            treeViewComponents.Nodes.Clear();

            tabControl1.SelectedTab = tabPageSourceFiles;

            Application.DoEvents();
        }

        /// <summary>
        /// Update the interface of the form
        /// </summary>
        private void UpdateInterface_EnableControls()
        {
            panel1.Enabled = !ReferenceEquals(_dslProject, null);
            panel1.Visible = !ReferenceEquals(_dslProject, null);

            SourceCodeTextEditor.Enabled = !ReferenceEquals(_openedDslFile, null);
            SourceCodeTextEditor.Visible = !ReferenceEquals(_openedDslFile, null);
            SourceCodeTextEditor.ReadOnly = !ReferenceEquals(_openedDslFile, null) &&
                                            _openedDslFile.IsText;

            menuItemFile_ManageSourceFiles.Enabled = !ReferenceEquals(_dslProject, null);

            menuItemFile_EditSourceFile.Enabled = 
                !ReferenceEquals(_dslProject, null) && 
                listBoxSourceFiles.SelectedItem != null;

            menuItemFile_SaveSourceFile.Enabled =
                !ReferenceEquals(_openedDslFile, null) &&
                SourceCodeTextEditor.IsChanged;
            
            menuItemFile_CloseSourceFile.Enabled = !ReferenceEquals(_openedDslFile, null);

            menuItemCompile_CompileDSLCode.Enabled =
                !ReferenceEquals(_dslProject, null) &&
                _dslProject.ContainsSourceFiles;

            menuItemTools_ExploreAST.Enabled =
                !ReferenceEquals(_dslProject, null) &&
                _dslProject.ContainsSourceFiles;

            menuItemTools_CodeGenerator.Enabled =
                !ReferenceEquals(_dslProject, null) &&
                _dslProject.ContainsSourceFiles;

            menuItemTools_ExploreMacro.Enabled =
                !ReferenceEquals(_dslProject, null) &&
                _dslProject.ContainsSourceFiles;

            menuItemTools_InteractiveScript.Enabled =
                !ReferenceEquals(_dslProject, null) &&
                _dslProject.ContainsSourceFiles;

            Text = @"GMac Compiler IDE" + (SourceCodeTextEditor.IsChanged ? @" (*)" : String.Empty);
        }

        private void UpdateInterface_SourceFilesList()
        {
            var selectedFile = (ISourceCodeUnit)listBoxSourceFiles.SelectedItem;

            listBoxSourceFiles.Items.Clear();

            listBoxSourceFiles.Items.AddRange(
                _dslProject.SourceFiles.Cast<object>().ToArray()
                );

            listBoxSourceFiles.Items.AddRange(
                _dslProject.GeneratedCode.Cast<object>().ToArray()
                );

            if (ReferenceEquals(selectedFile, null) == false)
            {
                for (var i = 0; i < listBoxSourceFiles.Items.Count; i++)
                {
                    var file = (ISourceCodeUnit)listBoxSourceFiles.Items[i];

                    if (file != selectedFile) 
                        continue;

                    listBoxSourceFiles.SelectedIndex = i;
                    return;
                }
            }

            if (listBoxSourceFiles.Items.Count > 0)
                listBoxSourceFiles.SelectedIndex = 0;
        }


        private void SetEditorTextChangedStatus(bool flag)
        {
            if (flag == _editorTextChangedStatus) 
                return;

            _editorTextChangedStatus = flag;
                
            if (SourceCodeTextEditor.IsChanged != flag) 
                SourceCodeTextEditor.IsChanged = flag;

            if (!flag)
                _astRoot = null;

            UpdateInterface_EnableControls();
        }

        /// <summary>
        /// If a file is selected in the source files list this method loads it into the text editor and update the interface
        /// </summary>
        private bool OpenSelectedSourceFile()
        {
            if (listBoxSourceFiles.SelectedItem == null)
                return false;

            var codeFile = listBoxSourceFiles.SelectedItem as LanguageCodeFile;

            if (codeFile != null)
                return ChangeOpenedSourceFile(codeFile);

            var codeTextUnit = listBoxSourceFiles.SelectedItem as LanguageCodeText;

            if (codeTextUnit != null)
                return ChangeOpenedSourceFile(codeTextUnit);

            return false;
        }

        private bool ChangeOpenedSourceFile(ISourceCodeUnit sourceCodeUnit)
        {
            //A project must be active
            if (_dslProject == null)
                return false;

            //If the opened file is the same as the new file do nothing
            if (ReferenceEquals(sourceCodeUnit, _openedDslFile))
                return true;

            //If the opened file nedds saving ask for it
            if (AskSaveSourceCode() == false)
                return false;

            //Set the opened file to the new file
            _openedDslFile = sourceCodeUnit;

            if (!ReferenceEquals(_openedDslFile, null))
            {
                //Load the code into the text editor
                if (ShowSourceCode() == false)
                    return false;

                //Update the selected item in the source files list if necessary
                if (((ISourceCodeUnit)listBoxSourceFiles.SelectedItem) != sourceCodeUnit)
                    for (var i = 0; i < listBoxSourceFiles.Items.Count; i++)
                        if ((ISourceCodeUnit)listBoxSourceFiles.Items[i] == _openedDslFile)
                        {
                            listBoxSourceFiles.SelectedIndex = i;
                            break;
                        }
            }
            else
            {
                SourceCodeTextEditor.Text = "";

                SetEditorTextChangedStatus(false);
            }

            UpdateInterface_EnableControls();

            return true;
        }

        /// <summary>
        /// Load souce code from file into the text editor
        /// </summary>
        private bool ShowSourceCode()
        {
            try
            {
                SourceCodeTextEditor.Text = 
                    _openedDslFile.IsFile
                    ? File.ReadAllText(_openedDslFile.FilePath) 
                    : _openedDslFile.CodeText;

                ShowCodeLocation(0, 0);

                //move caret to start text
                SourceCodeTextEditor.Selection.Start = Place.Empty;
                SourceCodeTextEditor.DoCaretVisible();
                SourceCodeTextEditor.ClearUndo();

                SetEditorTextChangedStatus(false);

                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

                return false;
            }
        }

        /// <summary>
        /// Save the code in the text editor into the file
        /// </summary>
        /// <returns></returns>
        private bool SaveSourceCode()
        {
            try
            {
                File.WriteAllText(_openedDslFile.FilePath, SourceCodeTextEditor.Text, _openedDslFile.TextEncoding);

                SetEditorTextChangedStatus(false);

                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }

        private bool SourceCodeModified => _openedDslFile != null && _editorTextChangedStatus;

        /// <summary>
        /// Ask user to save text editor code into file
        /// </summary>
        /// <returns></returns>
        private bool AskSaveSourceCode()
        {
            if (!SourceCodeModified) 
                return true;

            var result = MessageBox.Show(@"Save code to file first?", @"Source Code", MessageBoxButtons.YesNoCancel);

            if (result == DialogResult.Yes && SaveSourceCode())
            {
                UpdateInterface_EnableControls();

                SourceCodeTextEditor.Focus();
            }
            else if (result == DialogResult.Cancel)
            {
                SourceCodeTextEditor.Focus();

                return false;
            }

            return true;
        }

        private void FillErrorList(LanguageCompilationLog compilationLog)
        {
            tabPageErrors.Text = compilationLog.ErrorsCount + @" Errors";

            listViewErrors.Items.Clear();

            for (var i = 0; i < compilationLog.ErrorsCount; i++)
            {
                var errorMessage = compilationLog.ErrorMessages[i];

                var item = listViewErrors.Items.Add((i + 1).ToString());

                item.SubItems.Add(errorMessage.CodeLocation.CodeUnit.FilePath);
                item.SubItems.Add(errorMessage.CodeLocation.LineColumnDescription());
                item.SubItems.Add(errorMessage.Description);

                item.Tag = errorMessage;
            }

            listViewErrors.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void FillWarningList(LanguageCompilationLog compilationLog)
        {
            tabPageWarnings.Text = compilationLog.WarningsCount + @" Warnings";

            listViewWarnings.Items.Clear();

            for (var i = 0; i < compilationLog.WarningsCount; i++)
            {
                var warningMessage = compilationLog.WarningMessages[i];

                var item = listViewWarnings.Items.Add((i + 1).ToString());

                item.SubItems.Add(warningMessage.CodeLocation.CodeUnit.FilePath);
                item.SubItems.Add(warningMessage.CodeLocation.LineColumnDescription());
                item.SubItems.Add(warningMessage.Description);

                item.Tag = warningMessage;
            }

            listViewWarnings.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void MarkSelectedError()
        {
            if (listViewErrors.SelectedItems.Count <= 0) 
                return;

            var item = listViewErrors.SelectedItems[0];

            var errorMessage = item.Tag as LanguageCompilationMessage;

            if (errorMessage != null)
                ShowCodeLocation(errorMessage.CodeLocation, errorMessage.CodeSpanLength);
        }

        private void FillComponentsTree()
        {
            treeViewComponents.Nodes.Clear();

            if (ReferenceEquals(_astRoot, null))
                return;

            treeViewComponents.Nodes.Clear();

            treeViewComponents.Nodes.Add(GMacAstToTreeViewNodesByRole.Convert(_astRoot));
        }


        private void ShowNotificationMessage(string message)
        {
            var form = new FormNotificationMessage(message);

            form.ShowDialog(this);
        }

        private void ShowCodeLocation(int absoluteLocation, int length)
        {
            SourceCodeTextEditor.SelectionStart = absoluteLocation;
            SourceCodeTextEditor.SelectionLength = length;
            SourceCodeTextEditor.DoSelectionVisible();
        }

        private void ShowCodeLocation(LanguageCodeLocation location, int length)
        {
            ChangeOpenedSourceFile(location.CodeUnit);

            SourceCodeTextEditor.SelectionStart = location.CharacterNumber;
            SourceCodeTextEditor.SelectionLength = length;
            SourceCodeTextEditor.DoSelectionVisible();
        }

        private bool CompileSourceFiles(bool forceCompilation = false)
        {
            if (AskSaveSourceCode() == false)
                return false;

            ResetCompilationInterface();

            var startCompilationTime = DateTime.Now;

            textBoxOutputLog.Text =
                startCompilationTime.ToLongTimeString() +
                @" Start Compilation" +
                Environment.NewLine;

            Application.DoEvents();

            GMacSystemUtils.ResetProgress();

            _dslCompiler.Compile(_dslProject, forceCompilation);

            var endCompilationTime = DateTime.Now;

            textBoxOutputLog.Text += _dslCompiler.CompilationLog.Progress.History.ToString();

            //textBoxOutputLog.Text += _dslCompiler.CompilationLog.TimeCounter.RootEventsSpanToString();

            textBoxOutputLog.Text +=
                endCompilationTime.ToLongTimeString() +
                @" End Compilation " +
                (_dslCompiler.CompilationLog.HasErrors ? @"with errors " : String.Empty) +
                @"after " +
                (endCompilationTime - startCompilationTime) +
                Environment.NewLine;

            FillErrorList(_dslCompiler.CompilationLog);

            FillWarningList(_dslCompiler.CompilationLog);

            UpdateInterface_SourceFilesList();

            if (_dslCompiler.CompilationLog.HasErrors)
            {
                listViewErrors.SelectedIndices.Add(0);

                tabControl2.SelectedTab = tabPageErrors;

                MarkSelectedError();
            }
            else
            {
                _astRoot = _dslCompiler.RootGMacAst;

                FillComponentsTree();

                tabControl1.SelectedTab = tabPageComponents;
            }

            return true;
        }

        #endregion


        #region Interface Events


        private void formDSLEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (AskSaveSourceCode() == false)
                e.Cancel = true;
        }


        private void SourceCodeTextEditor_TextChanged(object sender, TextChangedEventArgs e)
        {
            SetEditorTextChangedStatus(true);

            SourceCodeTextEditor.LeftBracket = '(';
            SourceCodeTextEditor.RightBracket = ')';
            SourceCodeTextEditor.LeftBracket2 = '\x0';
            SourceCodeTextEditor.RightBracket2 = '\x0';


            //clear style of changed range
            e.ChangedRange.ClearStyle(_blueStyle, _boldStyle, _grayStyle, _magentaStyle, _greenStyle, _brownStyle);

            //comment highlighting
            e.ChangedRange.SetStyle(_greenStyle, @"//.*$", RegexOptions.Multiline);

            //string highlighting
            e.ChangedRange.SetStyle(_brownStyle, @"""""|@""""|''|@"".*?""|(?<!@)(?<range>"".*?[^\\]"")|'.*?[^\\]'");

            //comment highlighting
            e.ChangedRange.SetStyle(_greenStyle, @"(/\*.*?\*/)|(/\*.*)", RegexOptions.Singleline);
            e.ChangedRange.SetStyle(_greenStyle, @"(/\*.*?\*/)|(.*\*/)", RegexOptions.Singleline | RegexOptions.RightToLeft);

            //number highlighting
            e.ChangedRange.SetStyle(_magentaStyle, @"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfF]?\b|\b0x[a-fA-F\d]+\b");

            //attribute highlighting
            e.ChangedRange.SetStyle(_grayStyle, @"^\s*(?<range>\[.+?\])\s*$", RegexOptions.Multiline);

            //class name highlighting
            //e.ChangedRange.SetStyle(BoldStyle, @"\b(namespace|constant|macro|frame|structure|class|member)\s+(?<range>\w+?)\b");

            //keyword highlighting
            e.ChangedRange.SetStyle(_blueStyle, @"\b(breakpoint|using|begin|end|frame|macro|transform|euclidean|IPM|CBM|orthogonal|orthonormal|reciprocal|class|ga|constants|declare|let|return|namespace|constant|structure|from|to|open|template|implement|subspace|access|binding|on|bind|to|with|result)\b");

            //clear folding markers
            e.ChangedRange.ClearFoldingMarkers();
            //set folding markers
            e.ChangedRange.SetFoldingMarkers("{", "}");//allow to collapse brackets block
            e.ChangedRange.SetFoldingMarkers(@"begin\b", @"end\b");//allow to collapse begin\end blocks
            //e.ChangedRange.SetFoldingMarkers(@"#region\b", @"#endregion\b");//allow to collapse #region blocks
            e.ChangedRange.SetFoldingMarkers(@"/\*", @"\*/");//allow to collapse comment block
        }

        private void SourceCodeTextEditor_SelectionChanged(object sender, EventArgs e)
        {
            var place = SourceCodeTextEditor.Selection.Start;
            var line = place.iLine + 1;
            var column = place.iChar + 1;
            var length = SourceCodeTextEditor.SelectionLength;

            if (length == 0)
                toolStripStatusLabelCodeLocation.Text =
                    @"Line: " + line + @", " +
                    @"Character: " + column;

            else
                toolStripStatusLabelCodeLocation.Text =
                    @"Line: " + line + @", " +
                    @"Character: " + column + @", " +
                    @"Length: " + length;
        }



        private void listBoxSourceFiles_DoubleClick(object sender, EventArgs e)
        {
            OpenSelectedSourceFile();
        }


        private void listViewErrors_SelectedIndexChanged(object sender, EventArgs e)
        {
            MarkSelectedError();
        }

        private void listViewErrors_DoubleClick(object sender, EventArgs e)
        {
            if (listViewErrors.SelectedItems.Count <= 0) 
                return;

            var item = listViewErrors.SelectedItems[0];

            var errorMessage = item.Tag as LanguageCompilationMessage;

            if (errorMessage == null) 
                return;

            var form = new FormNotificationMessage(errorMessage.Description);

            form.ShowDialog(this);
        }


        private void listViewWarnings_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewErrors.SelectedItems.Count <= 0) 
                return;

            var item = listViewWarnings.SelectedItems[0];

            var errorMessage = item.Tag as LanguageCompilationMessage;

            if (errorMessage != null)
                ShowCodeLocation(errorMessage.CodeLocation, errorMessage.CodeSpanLength);
        }

        private void listViewWarnings_DoubleClick(object sender, EventArgs e)
        {
            if (listViewWarnings.SelectedItems.Count <= 0) 
                return;

            var item = listViewWarnings.SelectedItems[0];

            var errorMessage = item.Tag as LanguageCompilationMessage;

            if (errorMessage == null) 
                return;

            var form = new FormNotificationMessage(errorMessage.Description);

            form.ShowDialog(this);
        }


        private void menuItem_File_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void menuItem_File_ManageSourceFiles_Click(object sender, EventArgs e)
        {
            if (AskSaveSourceCode() == false)
                return;

            if (_dslProject == null) return;

            var dialog = new FormGMacDslSourceFilesEditor(_dslProject.SourceFilesPaths);


            if (dialog.ShowDialog(this) != DialogResult.OK || dialog.FilesCount == 0) return;
           
            _dslProject.SetSourceFilesList(dialog.FilePaths);

            _dslProject.SaveProjectToXmlFile();

            UpdateInterface_SourceFilesList();

            UpdateInterface_EnableControls();


            //TODO: Any opened files not in the list should be closed now
        }

        private void menuItem_File_OpenProject_Click(object sender, EventArgs e)
        {
            if (AskSaveSourceCode() == false)
                return;

            openFileDialog1.CheckFileExists = true;
            openFileDialog1.DefaultExt = "gmacproj";
            openFileDialog1.Filter = @"GMac Project Files (*.gmacproj)|*.gmacproj";
            openFileDialog1.Multiselect = false;
            openFileDialog1.SupportMultiDottedExtensions = true;
            openFileDialog1.Title = @"Open GMac Project File";

            if (openFileDialog1.ShowDialog(this) != DialogResult.OK) 
                return;

            InitializeProject(
                GMacProject.CreateFromXmlFile(openFileDialog1.FileName)
                );

            UpdateInterface_SourceFilesList();

            UpdateInterface_EnableControls();
        }

        private void menuItem_File_NewProject_Click(object sender, EventArgs e)
        {
            if (AskSaveSourceCode() == false)
                return;

            saveFileDialog1.DefaultExt = "gmacproj";
            saveFileDialog1.Filter = @"GMac Projects (*.gmacproj)|*.gmacproj";
            saveFileDialog1.OverwritePrompt = true;
            saveFileDialog1.SupportMultiDottedExtensions = true;
            saveFileDialog1.Title = @"New GMac Project File";

            if (saveFileDialog1.ShowDialog(this) != DialogResult.OK) 
                return;

            InitializeProject(
                GMacProject.CreateNew(saveFileDialog1.FileName)
                );

            UpdateInterface_SourceFilesList();

            UpdateInterface_EnableControls();
        }

        private void menuItem_File_EditSourceFile_Click(object sender, EventArgs e)
        {
            OpenSelectedSourceFile();
        }

        private void menuItem_File_SaveSourceFile_Click(object sender, EventArgs e)
        {
            SaveSourceCode();

            UpdateInterface_EnableControls();
        }

        private void menuItem_File_CloseSourceFile_Click(object sender, EventArgs e)
        {
            ChangeOpenedSourceFile(null);
        }

        private void menuItemCompile_CompileDSLCode_Click(object sender, EventArgs e)
        {
            CompileSourceFiles(true);
        }

        private void menuItemCompile_CompilerOptions_Click(object sender, EventArgs e)
        {
            var optionsForm = new FormCompilerOptions();

            optionsForm.ShowDialog(this);
        }

        private void menuItemTools_ExploreAST_Click(object sender, EventArgs e)
        {
            if (CompileSourceFiles() == false)
                return;

            if (ReferenceEquals(_astRoot, null))
                return;

            var form = new FormAstExplorer(_astRoot);

            form.ShowDialog(this);
        }

        private void menuItemTools_ExploreMacro_Click(object sender, EventArgs e)
        {
            if (ReferenceEquals(treeViewComponents.SelectedNode, null))
            {
                MessageBox.Show(@"Please select a macro to explore after compiling the DSL source code");

                return;
            }

            var macro = treeViewComponents.SelectedNode.Tag as GMacMacro;

            if (ReferenceEquals(macro, null))
            {
                MessageBox.Show(@"Please select a macro to explore");

                return;
            }

            
            if (CompileSourceFiles() == false)
                return;

            if (_astRoot == null)
                return;

            var form = new FormGMacMacroExplorer(macro);

            form.ShowDialog(this);
        }

        private void menuItemTools_CodeGenerator_Click(object sender, EventArgs e)
        {
            if (CompileSourceFiles() == false)
                return;

            if (ReferenceEquals(_astRoot, null))
                return;

            var astRoot = _astRoot.ToAstRoot();

            var generatorsList = new GMacCodeLibraryComposer[]
            {
                new DirectLibrary(astRoot),
                new FrameLibrary(astRoot)
            };

            var form = new FormCodeGenerator(generatorsList);

            form.ShowDialog(this);
        }

        private void menuItemTools_InteractiveScript_Click(object sender, EventArgs e)
        {
            if (CompileSourceFiles() == false)
                return;

            if (ReferenceEquals(_astRoot, null))
                return;

            var form = new FormInteractiveScript(_astRoot);

            form.ShowDialog(this);
        }

        private void menuItemTools_ExploreClasses_Click(object sender, EventArgs e)
        {
            var form = new FormPublicTypes();
            form.ShowDialog(this);
        }

        private void menuItemHelp_AboutGMac_Click(object sender, EventArgs e)
        {
            var form = new FormGMacAbout();
            form.ShowDialog(this);
        }


        private void treeViewComponents_DoubleClick(object sender, EventArgs e)
        {
            var node = treeViewComponents.SelectedNode;

            if (!(node.Tag is LanguageSymbol))
                return;

            var symbol = (LanguageSymbol)node.Tag;

            if (ReferenceEquals(symbol.CodeLocation, null))
            {
                MessageBox.Show(@"Symbol has no parse information attached.");

                return;
            }

            var location = symbol.CodeLocation.CodeLocation;

            ShowCodeLocation(location, symbol.CodeLocation.CodeSpanLength);

            //ShowCodeLocation(location, symbol.ParseNode.FindTokenAndGetText().Length);
        }

        #endregion
    }
}
