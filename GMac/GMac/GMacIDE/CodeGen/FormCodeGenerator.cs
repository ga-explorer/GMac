using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using GMac.GMacAPI.CodeGen;
using GMac.GMacAST.Symbols;
using TextComposerLib.UI.WinForms.UserInterface.UI;

namespace GMac.GMacIDE.CodeGen
{
    public partial class FormCodeGenerator : Form
    {
        //private readonly AstRoot _astRoot;

        private AstSymbol[] _astSymbols;

        private Dictionary<string, AstSymbol> _selectedAstSymbols;


        internal FormCodeGenerator(IEnumerable<GMacCodeLibraryComposer> generatorsList)
        {
            //_astRoot = astRoot;

            InitializeComponent();

            FillGeneratorsList(generatorsList);
        }


        private void SetSymbols(GMacCodeLibraryComposer libGen)
        {
            _astSymbols =
                libGen.GetBaseSymbolsList()
                .OrderBy(item => item.AccessName)
                .ToArray();

            _selectedAstSymbols =
                _astSymbols
                .ToDictionary(item => item.AccessName, item => item);
        }

        private void FillAstItemsList()
        {
            var roleFilter = textBoxRuleFilter.Text.Trim();
            var nameFilter = textBoxNameFilter.Text.Trim();

            var symbolsList = 
                _astSymbols
                .Where(
                    item =>
                        (string.IsNullOrEmpty(roleFilter) || item.RoleName.IndexOf(roleFilter, StringComparison.OrdinalIgnoreCase) >= 0) &&
                        (string.IsNullOrEmpty(nameFilter) || item.AccessName.IndexOf(nameFilter, StringComparison.OrdinalIgnoreCase) >= 0)
                    )
                .Select(
                    symbol => 
                        new ListViewItem(new[] {symbol.RoleName, symbol.AccessName })
                        {
                            Tag = symbol,

                            Checked = _selectedAstSymbols.ContainsKey(symbol.AssociatedSymbol.SymbolAccessName)
                        }
                    )
                .ToArray();

            listViewAstItems.Items.Clear();

            listViewAstItems.Items.AddRange(symbolsList);
        }

        private void FillGeneratorsList(IEnumerable<GMacCodeLibraryComposer> generatorsList)
        {
            foreach (var gen in generatorsList)
                comboBoxGenerators.Items.Add(gen);

            comboBoxGenerators.SelectedIndex = 0;

            var activeGenerator = comboBoxGenerators.SelectedItem as GMacCodeLibraryComposer;

            if (activeGenerator == null) return;

            textBoxDescription.Text = activeGenerator.Description;

            SetSymbols(activeGenerator);

            FillAstItemsList();
        }

        private void BeginCodeGeneration(GMacCodeLibraryComposer libGen)
        {
            GMacSystemUtils.ResetProgress();

            var formProgress = new FormProgress(libGen.Progress, libGen.Generate, null);
            formProgress.ShowDialog(this);

            var formFiles = new FormFilesComposer(libGen.CodeFilesComposer);
            formFiles.ShowDialog(this);
        }


        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            var activeGenerator = comboBoxGenerators.SelectedItem as GMacCodeLibraryComposer;

            if (activeGenerator == null)
                return;

            //Make an empty copy of the selected generator to sure previous generation session data are removed
            activeGenerator = activeGenerator.CreateEmptyGenerator();

            activeGenerator.MacroGenDefaults.AllowGenerateMacroCode = checkBoxGenerateMacroCode.Checked;

            activeGenerator.SelectedSymbols.SetSymbols(_selectedAstSymbols.Values);

            BeginCodeGeneration(activeGenerator);
        }

        private void comboBoxGenerators_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxRuleFilter.Text = string.Empty;
            textBoxNameFilter.Text = string.Empty;

            var activeGenerator = comboBoxGenerators.SelectedItem as GMacCodeLibraryComposer;

            if (activeGenerator == null) return;

            textBoxDescription.Text = activeGenerator.Description;

            SetSymbols(activeGenerator);

            FillAstItemsList();
        }

        private void buttonSelectAll_Click(object sender, EventArgs e)
        {
            foreach (var item in listViewAstItems.Items.Cast<ListViewItem>())
                item.Checked = true;
        }

        private void buttonSelectNone_Click(object sender, EventArgs e)
        {
            foreach (var item in listViewAstItems.Items.Cast<ListViewItem>())
                item.Checked = false;
        }

        private void buttonInvertSelection_Click(object sender, EventArgs e)
        {
            foreach (var item in listViewAstItems.Items.Cast<ListViewItem>())
                item.Checked = !item.Checked;
        }

        private void buttonFilterView_Click(object sender, EventArgs e)
        {
            FillAstItemsList();
        }

        private void buttonShowAll_Click(object sender, EventArgs e)
        {
            textBoxRuleFilter.Text = string.Empty;
            textBoxNameFilter.Text = string.Empty;

            FillAstItemsList();
        }

        private void listViewAstItems_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            var symbol = (AstSymbol)e.Item.Tag;

            if (e.Item.Checked && _selectedAstSymbols.ContainsKey(symbol.AccessName) == false)
                _selectedAstSymbols.Add(symbol.AccessName, symbol);

            else
                _selectedAstSymbols.Remove(symbol.AccessName);
        }
    }
}
