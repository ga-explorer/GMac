using System.ComponentModel;
using System.Windows.Forms;

namespace GMac.GMacIDE
{
    partial class FormGMacLicense
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
            this.buttonDone = new System.Windows.Forms.Button();
            this.textBoxLicense = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonDone
            // 
            this.buttonDone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDone.Location = new System.Drawing.Point(514, 402);
            this.buttonDone.Name = "buttonDone";
            this.buttonDone.Size = new System.Drawing.Size(98, 28);
            this.buttonDone.TabIndex = 1;
            this.buttonDone.Text = "Done";
            this.buttonDone.UseVisualStyleBackColor = true;
            this.buttonDone.Click += new System.EventHandler(this.buttonDone_Click);
            // 
            // textBoxLicense
            // 
            this.textBoxLicense.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxLicense.BackColor = System.Drawing.Color.White;
            this.textBoxLicense.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxLicense.Location = new System.Drawing.Point(12, 12);
            this.textBoxLicense.MaxLength = 0;
            this.textBoxLicense.Multiline = true;
            this.textBoxLicense.Name = "textBoxLicense";
            this.textBoxLicense.ReadOnly = true;
            this.textBoxLicense.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxLicense.Size = new System.Drawing.Size(600, 384);
            this.textBoxLicense.TabIndex = 2;
            // 
            // FormGMacLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 442);
            this.Controls.Add(this.textBoxLicense);
            this.Controls.Add(this.buttonDone);
            this.Name = "FormGMacLicense";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "End User License Agreement";
            this.Load += new System.EventHandler(this.FormGMacLicense_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Button buttonDone;
        private TextBox textBoxLicense;
    }
}