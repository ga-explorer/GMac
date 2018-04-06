using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace GMac.GMacIDE
{
    public partial class FormGMacAbout : Form
    {
        public FormGMacAbout()
        {
            InitializeComponent();

            //var foreColor = Color.FromArgb(52, 28, 1);

            //labelGMac.ForeColor = foreColor;
            //labelVersion.ForeColor = foreColor;
            //labelCopyright.ForeColor = foreColor;

            linkLabelName.Links[0].LinkData = "http://gacomputing.info/about-gmac/";
            linkLabelName.Links[0].Length = GMacSystemUtils.AppName.Length;
            linkLabelName.Text = GMacSystemUtils.AppName;
            labelVersion.Text = GMacSystemUtils.Version;
            labelVersion.Text = GMacSystemUtils.Version;
            labelCopyright.Text = GMacSystemUtils.Copyright;
        }

        private void FormGMacAbout_MouseClick(object sender, MouseEventArgs e)
        {
            Close();
        }

        private void FormGMacAbout_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData.ToString());
        }

        private void FormGMacAbout_Load(object sender, EventArgs e)
        {

        }

        private void buttonViewLicense_Click(object sender, EventArgs e)
        {
            new FormGMacLicense().ShowDialog(this);
        }
    }
}
