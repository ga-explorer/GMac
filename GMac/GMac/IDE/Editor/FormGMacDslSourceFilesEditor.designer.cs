using System.ComponentModel;
using System.Windows.Forms;

namespace GMac.IDE.Editor
{
    partial class FormGMacDslSourceFilesEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGMacDslSourceFilesEditor));
            this.textBoxFilesList = new System.Windows.Forms.TextBox();
            this.buttonInsertFiles = new System.Windows.Forms.Button();
            this.buttonDone = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.buttonInsertNewFile = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // textBoxFilesList
            // 
            this.textBoxFilesList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFilesList.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxFilesList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxFilesList.Location = new System.Drawing.Point(0, 0);
            this.textBoxFilesList.Multiline = true;
            this.textBoxFilesList.Name = "textBoxFilesList";
            this.textBoxFilesList.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxFilesList.Size = new System.Drawing.Size(782, 509);
            this.textBoxFilesList.TabIndex = 0;
            // 
            // buttonInsertFiles
            // 
            this.buttonInsertFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonInsertFiles.Location = new System.Drawing.Point(12, 515);
            this.buttonInsertFiles.Name = "buttonInsertFiles";
            this.buttonInsertFiles.Size = new System.Drawing.Size(156, 35);
            this.buttonInsertFiles.TabIndex = 1;
            this.buttonInsertFiles.Text = "Insert Existing Files";
            this.buttonInsertFiles.UseVisualStyleBackColor = true;
            this.buttonInsertFiles.Click += new System.EventHandler(this.buttonInsertFiles_Click);
            // 
            // buttonDone
            // 
            this.buttonDone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDone.Location = new System.Drawing.Point(516, 515);
            this.buttonDone.Name = "buttonDone";
            this.buttonDone.Size = new System.Drawing.Size(125, 35);
            this.buttonDone.TabIndex = 2;
            this.buttonDone.Text = "Done";
            this.buttonDone.UseVisualStyleBackColor = true;
            this.buttonDone.Click += new System.EventHandler(this.buttonDone_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(647, 515);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(125, 35);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // buttonInsertNewFile
            // 
            this.buttonInsertNewFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonInsertNewFile.Location = new System.Drawing.Point(174, 515);
            this.buttonInsertNewFile.Name = "buttonInsertNewFile";
            this.buttonInsertNewFile.Size = new System.Drawing.Size(156, 35);
            this.buttonInsertNewFile.TabIndex = 4;
            this.buttonInsertNewFile.Text = "Insert New File";
            this.buttonInsertNewFile.UseVisualStyleBackColor = true;
            this.buttonInsertNewFile.Click += new System.EventHandler(this.buttonInsertNewFile_Click);
            // 
            // FormGMacDslSourceFilesEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.ControlBox = false;
            this.Controls.Add(this.buttonInsertNewFile);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonDone);
            this.Controls.Add(this.buttonInsertFiles);
            this.Controls.Add(this.textBoxFilesList);
            this.Font = new System.Drawing.Font("Vrinda", 11F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "FormGMacDslSourceFilesEditor";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit DSL Source Files List";
            this.Load += new System.EventHandler(this.formEditDSLSourceFiles_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox textBoxFilesList;
        private Button buttonInsertFiles;
        private Button buttonDone;
        private Button buttonCancel;
        private OpenFileDialog openFileDialog1;
        private Button buttonInsertNewFile;
        private SaveFileDialog saveFileDialog1;
    }
}