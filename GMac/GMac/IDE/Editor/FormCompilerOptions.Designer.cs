using System.ComponentModel;
using System.Windows.Forms;

namespace GMac.IDE.Editor
{
    partial class FormCompilerOptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCompilerOptions));
            this.buttonDone = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.groupBoxLowLevelPropagationMethod = new System.Windows.Forms.GroupBox();
            this.radioButtonPropagateSingleVariableDependent = new System.Windows.Forms.RadioButton();
            this.radioButtonPropagateSingleVariable = new System.Windows.Forms.RadioButton();
            this.radioButtonPropagateConstant = new System.Windows.Forms.RadioButton();
            this.checkBoxSimplifyLowLevelRhsValues = new System.Windows.Forms.CheckBox();
            this.checkBoxReduceLowLevelRhsSubExpressions = new System.Windows.Forms.CheckBox();
            this.checkBoxForceOrthogonalMetricProducts = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.groupBoxLowLevelPropagationMethod.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonDone
            // 
            this.buttonDone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDone.Location = new System.Drawing.Point(537, 405);
            this.buttonDone.Name = "buttonDone";
            this.buttonDone.Size = new System.Drawing.Size(75, 28);
            this.buttonDone.TabIndex = 1;
            this.buttonDone.Text = "&Done";
            this.buttonDone.UseVisualStyleBackColor = true;
            this.buttonDone.Click += new System.EventHandler(this.buttonDone_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.textBoxDescription);
            this.panel1.Controls.Add(this.groupBoxLowLevelPropagationMethod);
            this.panel1.Controls.Add(this.checkBoxSimplifyLowLevelRhsValues);
            this.panel1.Controls.Add(this.checkBoxReduceLowLevelRhsSubExpressions);
            this.panel1.Controls.Add(this.checkBoxForceOrthogonalMetricProducts);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(621, 399);
            this.panel1.TabIndex = 0;
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxDescription.Location = new System.Drawing.Point(12, 219);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.ReadOnly = true;
            this.textBoxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxDescription.Size = new System.Drawing.Size(599, 169);
            this.textBoxDescription.TabIndex = 4;
            // 
            // groupBoxLowLevelPropagationMethod
            // 
            this.groupBoxLowLevelPropagationMethod.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxLowLevelPropagationMethod.Controls.Add(this.radioButtonPropagateSingleVariableDependent);
            this.groupBoxLowLevelPropagationMethod.Controls.Add(this.radioButtonPropagateSingleVariable);
            this.groupBoxLowLevelPropagationMethod.Controls.Add(this.radioButtonPropagateConstant);
            this.groupBoxLowLevelPropagationMethod.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxLowLevelPropagationMethod.Location = new System.Drawing.Point(12, 103);
            this.groupBoxLowLevelPropagationMethod.Name = "groupBoxLowLevelPropagationMethod";
            this.groupBoxLowLevelPropagationMethod.Size = new System.Drawing.Size(599, 110);
            this.groupBoxLowLevelPropagationMethod.TabIndex = 3;
            this.groupBoxLowLevelPropagationMethod.TabStop = false;
            this.groupBoxLowLevelPropagationMethod.Text = "Low Level Propagation Method";
            this.groupBoxLowLevelPropagationMethod.Enter += new System.EventHandler(this.groupBoxLowLevelPropagationMethod_Enter);
            // 
            // radioButtonPropagateSingleVariableDependent
            // 
            this.radioButtonPropagateSingleVariableDependent.AutoSize = true;
            this.radioButtonPropagateSingleVariableDependent.Location = new System.Drawing.Point(6, 84);
            this.radioButtonPropagateSingleVariableDependent.Name = "radioButtonPropagateSingleVariableDependent";
            this.radioButtonPropagateSingleVariableDependent.Size = new System.Drawing.Size(483, 20);
            this.radioButtonPropagateSingleVariableDependent.TabIndex = 2;
            this.radioButtonPropagateSingleVariableDependent.TabStop = true;
            this.radioButtonPropagateSingleVariableDependent.Text = "Propagate Constants and Expressions Depending on a Single Variable";
            this.radioButtonPropagateSingleVariableDependent.UseVisualStyleBackColor = true;
            // 
            // radioButtonPropagateSingleVariable
            // 
            this.radioButtonPropagateSingleVariable.AutoSize = true;
            this.radioButtonPropagateSingleVariable.Location = new System.Drawing.Point(6, 58);
            this.radioButtonPropagateSingleVariable.Name = "radioButtonPropagateSingleVariable";
            this.radioButtonPropagateSingleVariable.Size = new System.Drawing.Size(302, 20);
            this.radioButtonPropagateSingleVariable.TabIndex = 1;
            this.radioButtonPropagateSingleVariable.TabStop = true;
            this.radioButtonPropagateSingleVariable.Text = "Propagate Constants and Single Variables";
            this.radioButtonPropagateSingleVariable.UseVisualStyleBackColor = true;
            // 
            // radioButtonPropagateConstant
            // 
            this.radioButtonPropagateConstant.AutoSize = true;
            this.radioButtonPropagateConstant.Location = new System.Drawing.Point(6, 32);
            this.radioButtonPropagateConstant.Name = "radioButtonPropagateConstant";
            this.radioButtonPropagateConstant.Size = new System.Drawing.Size(199, 20);
            this.radioButtonPropagateConstant.TabIndex = 0;
            this.radioButtonPropagateConstant.TabStop = true;
            this.radioButtonPropagateConstant.Text = "Propagate Constants Only";
            this.radioButtonPropagateConstant.UseVisualStyleBackColor = true;
            // 
            // checkBoxSimplifyLowLevelRhsValues
            // 
            this.checkBoxSimplifyLowLevelRhsValues.AutoSize = true;
            this.checkBoxSimplifyLowLevelRhsValues.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxSimplifyLowLevelRhsValues.Location = new System.Drawing.Point(12, 64);
            this.checkBoxSimplifyLowLevelRhsValues.Name = "checkBoxSimplifyLowLevelRhsValues";
            this.checkBoxSimplifyLowLevelRhsValues.Size = new System.Drawing.Size(226, 20);
            this.checkBoxSimplifyLowLevelRhsValues.TabIndex = 2;
            this.checkBoxSimplifyLowLevelRhsValues.Text = "Simplify Low Level RHS Values";
            this.checkBoxSimplifyLowLevelRhsValues.UseVisualStyleBackColor = true;
            this.checkBoxSimplifyLowLevelRhsValues.Enter += new System.EventHandler(this.checkBoxSimplifyLowLevelRhsValues_Enter);
            // 
            // checkBoxReduceLowLevelRhsSubExpressions
            // 
            this.checkBoxReduceLowLevelRhsSubExpressions.AutoSize = true;
            this.checkBoxReduceLowLevelRhsSubExpressions.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxReduceLowLevelRhsSubExpressions.Location = new System.Drawing.Point(12, 38);
            this.checkBoxReduceLowLevelRhsSubExpressions.Name = "checkBoxReduceLowLevelRhsSubExpressions";
            this.checkBoxReduceLowLevelRhsSubExpressions.Size = new System.Drawing.Size(289, 20);
            this.checkBoxReduceLowLevelRhsSubExpressions.TabIndex = 1;
            this.checkBoxReduceLowLevelRhsSubExpressions.Text = "Reduce Low Level RHS Sub-Expressions";
            this.checkBoxReduceLowLevelRhsSubExpressions.UseVisualStyleBackColor = true;
            this.checkBoxReduceLowLevelRhsSubExpressions.Enter += new System.EventHandler(this.checkBoxReduceLowLevelRhsSubExpressions_Enter);
            // 
            // checkBoxForceOrthogonalMetricProducts
            // 
            this.checkBoxForceOrthogonalMetricProducts.AutoSize = true;
            this.checkBoxForceOrthogonalMetricProducts.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxForceOrthogonalMetricProducts.Location = new System.Drawing.Point(12, 12);
            this.checkBoxForceOrthogonalMetricProducts.Name = "checkBoxForceOrthogonalMetricProducts";
            this.checkBoxForceOrthogonalMetricProducts.Size = new System.Drawing.Size(250, 20);
            this.checkBoxForceOrthogonalMetricProducts.TabIndex = 0;
            this.checkBoxForceOrthogonalMetricProducts.Text = "Force Orthogonal Metric Products";
            this.checkBoxForceOrthogonalMetricProducts.UseVisualStyleBackColor = true;
            this.checkBoxForceOrthogonalMetricProducts.Enter += new System.EventHandler(this.checkBoxForceOrthogonalMetricProducts_Enter);
            // 
            // FormCompilerOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 442);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonDone);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "FormCompilerOptions";
            this.Text = "FormCompilerOptions";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBoxLowLevelPropagationMethod.ResumeLayout(false);
            this.groupBoxLowLevelPropagationMethod.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Button buttonDone;
        private Panel panel1;
        private CheckBox checkBoxForceOrthogonalMetricProducts;
        private CheckBox checkBoxSimplifyLowLevelRhsValues;
        private CheckBox checkBoxReduceLowLevelRhsSubExpressions;
        private GroupBox groupBoxLowLevelPropagationMethod;
        private RadioButton radioButtonPropagateSingleVariableDependent;
        private RadioButton radioButtonPropagateSingleVariable;
        private RadioButton radioButtonPropagateConstant;
        private TextBox textBoxDescription;

    }
}