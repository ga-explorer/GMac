using System;
using System.Windows.Forms;
using GMac.Engine;

namespace GMac.IDE
{
    public partial class FormGMacLicense : Form
    {
        public FormGMacLicense()
        {
            InitializeComponent();
        }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormGMacLicense_Load(object sender, EventArgs e)
        {
            textBoxLicense.Text = GMacEngineUtils.License;
        }
    }
}
