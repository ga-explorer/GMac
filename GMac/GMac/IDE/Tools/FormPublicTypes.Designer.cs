using System.ComponentModel;
using System.Windows.Forms;

namespace GMac.IDE.Tools
{
    partial class FormPublicTypes
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
            this.listBoxTypes = new System.Windows.Forms.ListBox();
            this.textBoxMembers = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listBoxNamespaces = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxTypes
            // 
            this.listBoxTypes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxTypes.FormattingEnabled = true;
            this.listBoxTypes.IntegralHeight = false;
            this.listBoxTypes.ItemHeight = 16;
            this.listBoxTypes.Location = new System.Drawing.Point(0, 0);
            this.listBoxTypes.Name = "listBoxTypes";
            this.listBoxTypes.Size = new System.Drawing.Size(219, 208);
            this.listBoxTypes.TabIndex = 0;
            this.listBoxTypes.SelectedIndexChanged += new System.EventHandler(this.listBoxTypes_SelectedIndexChanged);
            // 
            // textBoxMembers
            // 
            this.textBoxMembers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMembers.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxMembers.Location = new System.Drawing.Point(12, 226);
            this.textBoxMembers.MaxLength = 0;
            this.textBoxMembers.Multiline = true;
            this.textBoxMembers.Name = "textBoxMembers";
            this.textBoxMembers.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxMembers.Size = new System.Drawing.Size(600, 208);
            this.textBoxMembers.TabIndex = 1;
            this.textBoxMembers.WordWrap = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 12);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listBoxNamespaces);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listBoxTypes);
            this.splitContainer1.Size = new System.Drawing.Size(600, 208);
            this.splitContainer1.SplitterDistance = 377;
            this.splitContainer1.TabIndex = 2;
            // 
            // listBoxNamespaces
            // 
            this.listBoxNamespaces.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxNamespaces.FormattingEnabled = true;
            this.listBoxNamespaces.IntegralHeight = false;
            this.listBoxNamespaces.ItemHeight = 16;
            this.listBoxNamespaces.Location = new System.Drawing.Point(0, 0);
            this.listBoxNamespaces.Name = "listBoxNamespaces";
            this.listBoxNamespaces.Size = new System.Drawing.Size(377, 208);
            this.listBoxNamespaces.TabIndex = 0;
            this.listBoxNamespaces.SelectedIndexChanged += new System.EventHandler(this.listBoxNamespaces_SelectedIndexChanged);
            // 
            // FormPublicTypes
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(624, 446);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.textBoxMembers);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "FormPublicTypes";
            this.Text = "FormPublicTypes";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ListBox listBoxTypes;
        private TextBox textBoxMembers;
        private SplitContainer splitContainer1;
        private ListBox listBoxNamespaces;
    }
}