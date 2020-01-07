using System.ComponentModel;
using System.Windows.Forms;

namespace GMac.GMacIDE.Scripting
{
    partial class FormDisplayValues
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDisplayValues));
            this.listBoxValueDescriptions = new System.Windows.Forms.ListBox();
            this.buttonClose = new System.Windows.Forms.Button();
            this.listViewValueDetails = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // listBoxValueDescriptions
            // 
            this.listBoxValueDescriptions.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxValueDescriptions.FormattingEnabled = true;
            this.listBoxValueDescriptions.ItemHeight = 19;
            this.listBoxValueDescriptions.Location = new System.Drawing.Point(2, 2);
            this.listBoxValueDescriptions.Name = "listBoxValueDescriptions";
            this.listBoxValueDescriptions.Size = new System.Drawing.Size(619, 175);
            this.listBoxValueDescriptions.TabIndex = 0;
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(546, 412);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 30);
            this.buttonClose.TabIndex = 1;
            this.buttonClose.Text = "&Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // listViewValueDetails
            // 
            this.listViewValueDetails.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewValueDetails.Location = new System.Drawing.Point(188, 183);
            this.listViewValueDetails.Name = "listViewValueDetails";
            this.listViewValueDetails.Size = new System.Drawing.Size(433, 223);
            this.listViewValueDetails.TabIndex = 2;
            this.listViewValueDetails.UseCompatibleStateImageBehavior = false;
            // 
            // FormDisplayValues
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(624, 446);
            this.Controls.Add(this.listViewValueDetails);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.listBoxValueDescriptions);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "FormDisplayValues";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormDisplayValues";
            this.ResumeLayout(false);

        }

        #endregion

        private ListBox listBoxValueDescriptions;
        private Button buttonClose;
        private ListView listViewValueDetails;
    }
}