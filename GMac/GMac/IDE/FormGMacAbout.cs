using System;
using System.Diagnostics;
using System.Windows.Forms;
using GMac.Engine;

namespace GMac.IDE
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
            linkLabelName.Links[0].Length = GMacEngineUtils.AppName.Length;
            linkLabelName.Text = GMacEngineUtils.AppName;
            labelVersion.Text = GMacEngineUtils.Version;
            labelVersion.Text = GMacEngineUtils.Version;
            labelCopyright.Text = GMacEngineUtils.Copyright;
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
