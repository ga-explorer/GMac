using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using GMac.Engine.AST.Expressions;

namespace GMac.IDE.Scripting
{
    public partial class FormDisplayValues : Form
    {
        private readonly Dictionary<string, AstValue> _valuesList = new Dictionary<string, AstValue>();

        public FormDisplayValues()
        {
            InitializeComponent();
        }

        public void UpdateValues(IDictionary<string, AstValue> valuesList)
        {
            _valuesList.Clear();

            foreach (var pair in valuesList)
                _valuesList.Add(pair.Key, pair.Value);

            listBoxValueDescriptions.Items.Clear();
            listBoxValueDescriptions.Items.AddRange(_valuesList.Keys.Cast<object>().ToArray());

            listViewValueDetails.Clear();
        }


        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
