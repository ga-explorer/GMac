using System.ComponentModel;
using System.Windows.Forms;

namespace GMacSamples.CodeGen
{
    partial class CodeGenSamples
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
            this.textBoxResults = new System.Windows.Forms.TextBox();
            this.listBoxTasks = new System.Windows.Forms.ListBox();
            this.textBoxTaskDescription = new System.Windows.Forms.TextBox();
            this.buttonExecTask = new System.Windows.Forms.Button();
            this.checkBoxGenerateMacroCode = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // textBoxResults
            // 
            this.textBoxResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxResults.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxResults.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxResults.Location = new System.Drawing.Point(309, 140);
            this.textBoxResults.Multiline = true;
            this.textBoxResults.Name = "textBoxResults";
            this.textBoxResults.ReadOnly = true;
            this.textBoxResults.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxResults.Size = new System.Drawing.Size(303, 256);
            this.textBoxResults.TabIndex = 0;
            this.textBoxResults.WordWrap = false;
            // 
            // listBoxTasks
            // 
            this.listBoxTasks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxTasks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBoxTasks.FormattingEnabled = true;
            this.listBoxTasks.IntegralHeight = false;
            this.listBoxTasks.ItemHeight = 19;
            this.listBoxTasks.Location = new System.Drawing.Point(12, 12);
            this.listBoxTasks.Name = "listBoxTasks";
            this.listBoxTasks.ScrollAlwaysVisible = true;
            this.listBoxTasks.Size = new System.Drawing.Size(291, 384);
            this.listBoxTasks.Sorted = true;
            this.listBoxTasks.TabIndex = 9;
            this.listBoxTasks.SelectedIndexChanged += new System.EventHandler(this.listBoxTasks_SelectedIndexChanged);
            // 
            // textBoxTaskDescription
            // 
            this.textBoxTaskDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTaskDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxTaskDescription.Location = new System.Drawing.Point(309, 12);
            this.textBoxTaskDescription.MaxLength = 0;
            this.textBoxTaskDescription.Multiline = true;
            this.textBoxTaskDescription.Name = "textBoxTaskDescription";
            this.textBoxTaskDescription.ReadOnly = true;
            this.textBoxTaskDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxTaskDescription.Size = new System.Drawing.Size(303, 122);
            this.textBoxTaskDescription.TabIndex = 10;
            // 
            // buttonExecTask
            // 
            this.buttonExecTask.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonExecTask.Location = new System.Drawing.Point(12, 402);
            this.buttonExecTask.Name = "buttonExecTask";
            this.buttonExecTask.Size = new System.Drawing.Size(132, 28);
            this.buttonExecTask.TabIndex = 11;
            this.buttonExecTask.Text = "&Execute Task";
            this.buttonExecTask.UseVisualStyleBackColor = true;
            this.buttonExecTask.Click += new System.EventHandler(this.buttonExecTask_Click);
            // 
            // checkBoxGenerateMacroCode
            // 
            this.checkBoxGenerateMacroCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxGenerateMacroCode.AutoSize = true;
            this.checkBoxGenerateMacroCode.Location = new System.Drawing.Point(150, 406);
            this.checkBoxGenerateMacroCode.Name = "checkBoxGenerateMacroCode";
            this.checkBoxGenerateMacroCode.Size = new System.Drawing.Size(170, 23);
            this.checkBoxGenerateMacroCode.TabIndex = 12;
            this.checkBoxGenerateMacroCode.Text = "Generate Macro Code";
            this.checkBoxGenerateMacroCode.UseVisualStyleBackColor = true;
            // 
            // CodeGenSamples
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 442);
            this.Controls.Add(this.checkBoxGenerateMacroCode);
            this.Controls.Add(this.buttonExecTask);
            this.Controls.Add(this.textBoxTaskDescription);
            this.Controls.Add(this.listBoxTasks);
            this.Controls.Add(this.textBoxResults);
            this.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "CodeGenSamples";
            this.Text = "FormGeneralTests";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox textBoxResults;
        private ListBox listBoxTasks;
        private TextBox textBoxTaskDescription;
        private Button buttonExecTask;
        private CheckBox checkBoxGenerateMacroCode;
    }
}