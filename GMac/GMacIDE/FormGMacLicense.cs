using System;
using System.Windows.Forms;
using GMac.GMacUtils;

namespace GMac.GMacIDE
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
            textBoxLicense.Text = GMacSystemUtils.License;
        }
    }
}
