using System.ComponentModel;
using System.Windows.Forms;

namespace GMac.GMacIDE
{
    partial class FormGMacAbout
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelCopyright = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.linkLabelName = new System.Windows.Forms.LinkLabel();
            this.buttonViewLicense = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelCopyright
            // 
            this.labelCopyright.AutoSize = true;
            this.labelCopyright.BackColor = System.Drawing.Color.Transparent;
            this.labelCopyright.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCopyright.ForeColor = System.Drawing.Color.PeachPuff;
            this.labelCopyright.Location = new System.Drawing.Point(18, 112);
            this.labelCopyright.Name = "labelCopyright";
            this.labelCopyright.Size = new System.Drawing.Size(314, 22);
            this.labelCopyright.TabIndex = 9;
            this.labelCopyright.Text = "Copyright (c) 2016 Ahmad Hosny Eid";
            this.labelCopyright.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.BackColor = System.Drawing.Color.Transparent;
            this.labelVersion.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVersion.ForeColor = System.Drawing.Color.PeachPuff;
            this.labelVersion.Location = new System.Drawing.Point(16, 72);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(172, 32);
            this.labelVersion.TabIndex = 7;
            this.labelVersion.Text = "Version 1.0.0";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.linkLabelName);
            this.panel1.Controls.Add(this.labelCopyright);
            this.panel1.Controls.Add(this.labelVersion);
            this.panel1.Location = new System.Drawing.Point(296, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(404, 165);
            this.panel1.TabIndex = 10;
            // 
            // linkLabelName
            // 
            this.linkLabelName.ActiveLinkColor = System.Drawing.Color.PeachPuff;
            this.linkLabelName.AutoSize = true;
            this.linkLabelName.Font = new System.Drawing.Font("Times New Roman", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelName.LinkArea = new System.Windows.Forms.LinkArea(0, 4);
            this.linkLabelName.LinkColor = System.Drawing.Color.PeachPuff;
            this.linkLabelName.Location = new System.Drawing.Point(12, 10);
            this.linkLabelName.Name = "linkLabelName";
            this.linkLabelName.Size = new System.Drawing.Size(112, 40);
            this.linkLabelName.TabIndex = 10;
            this.linkLabelName.TabStop = true;
            this.linkLabelName.Text = "GMac";
            this.linkLabelName.VisitedLinkColor = System.Drawing.Color.PeachPuff;
            this.linkLabelName.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // buttonViewLicense
            // 
            this.buttonViewLicense.BackColor = System.Drawing.Color.PeachPuff;
            this.buttonViewLicense.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonViewLicense.ForeColor = System.Drawing.Color.Black;
            this.buttonViewLicense.Location = new System.Drawing.Point(476, 183);
            this.buttonViewLicense.Name = "buttonViewLicense";
            this.buttonViewLicense.Size = new System.Drawing.Size(224, 41);
            this.buttonViewLicense.TabIndex = 11;
            this.buttonViewLicense.Text = "View License Agreement";
            this.buttonViewLicense.UseVisualStyleBackColor = false;
            this.buttonViewLicense.Click += new System.EventHandler(this.buttonViewLicense_Click);
            // 
            // FormGMacAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::GMac.Properties.Resources.GMac_Logo;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(712, 712);
            this.ControlBox = false;
            this.Controls.Add(this.buttonViewLicense);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormGMacAbout";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.FormGMacAbout_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormGMacAbout_KeyDown);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FormGMacAbout_MouseClick);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Label labelCopyright;
        private Label labelVersion;
        private Panel panel1;
        private LinkLabel linkLabelName;
        private Button buttonViewLicense;
    }
}