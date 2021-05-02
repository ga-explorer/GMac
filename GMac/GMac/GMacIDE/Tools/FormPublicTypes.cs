using System;
using System.Linq;
using System.Windows.Forms;
using GMac.GMacIDE.Scripting;
using TextComposerLib.Text;

namespace GMac.GMacIDE.Tools
{
    public partial class FormPublicTypes : Form
    {
        public FormPublicTypes()
        {
            InitializeComponent();

            UpdateInterface();
        }

        private void UpdateInterface()
        {
            foreach (var nameSpace in NamespacesUtils.Namespaces)
                listBoxNamespaces.Items.Add(nameSpace);
        }

        private void listBoxNamespaces_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBoxTypes.Items.Clear();

            var nameSpace = listBoxNamespaces.SelectedItem as string;

            if (string.IsNullOrEmpty(nameSpace)) return;

            foreach (var pair in NamespacesUtils.PublicClasses(nameSpace))
                listBoxTypes.Items.Add(pair.Value.Name);
        }

        private void listBoxTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxMembers.Text = string.Empty;

            var nameSpace = listBoxNamespaces.SelectedItem as string;

            var typeName = listBoxTypes.SelectedItem as string;

            if (string.IsNullOrEmpty(nameSpace) || string.IsNullOrEmpty(typeName)) return;

            textBoxMembers.Text = 
                NamespacesUtils
                .ExportedTypes[nameSpace + "." + typeName]
                .MemberSignatures()
                .Select(s => s.Replace("get_", "").Replace("set_", "").Replace("public ", ""))
                .Concatenate(Environment.NewLine);
        }
    }
}
