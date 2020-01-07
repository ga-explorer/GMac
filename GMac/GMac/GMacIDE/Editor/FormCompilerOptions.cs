using System;
using System.Windows.Forms;
using GMac.GMacCompiler;

namespace GMac.GMacIDE.Editor
{
    public partial class FormCompilerOptions : Form
    {
        public FormCompilerOptions()
        {
            InitializeComponent();

            FillInterface();
        }

        private void FillInterface()
        {
            checkBoxForceOrthogonalMetricProducts.Checked = 
                GMacCompilerOptions.ForceOrthogonalMetricProducts;

            checkBoxReduceLowLevelRhsSubExpressions.Checked =
                GMacCompilerOptions.ReduceLowLevelRhsSubExpressions;

            checkBoxSimplifyLowLevelRhsValues.Checked =
                GMacCompilerOptions.SimplifyLowLevelRhsValues;

            switch (GMacCompilerOptions.LowLevelPropagationMethod)
            {
                case GMacCompilerOptions.LowLevelPropagation.PropagateConstant:
                    radioButtonPropagateConstant.Checked = true;
                    break;

                case GMacCompilerOptions.LowLevelPropagation.PropagateSingleVariable:
                    radioButtonPropagateSingleVariable.Checked = true;
                    break;

                case GMacCompilerOptions.LowLevelPropagation.PropagateSingleVariableDependent:
                    radioButtonPropagateSingleVariableDependent.Checked = true;
                    break;
            }
        }

        private void FillOptions()
        {
            GMacCompilerOptions.ForceOrthogonalMetricProducts =
                checkBoxForceOrthogonalMetricProducts.Checked;

            GMacCompilerOptions.ReduceLowLevelRhsSubExpressions =
                checkBoxReduceLowLevelRhsSubExpressions.Checked;

            GMacCompilerOptions.SimplifyLowLevelRhsValues =
                checkBoxSimplifyLowLevelRhsValues.Checked;

            if (radioButtonPropagateConstant.Checked)
                GMacCompilerOptions.LowLevelPropagationMethod =
                    GMacCompilerOptions.LowLevelPropagation.PropagateConstant;

            else if (radioButtonPropagateSingleVariable.Checked)
                GMacCompilerOptions.LowLevelPropagationMethod =
                    GMacCompilerOptions.LowLevelPropagation.PropagateSingleVariable;

            else if (radioButtonPropagateSingleVariableDependent.Checked)
                GMacCompilerOptions.LowLevelPropagationMethod =
                    GMacCompilerOptions.LowLevelPropagation.PropagateSingleVariableDependent;
        }

        private void checkBoxForceOrthogonalMetricProducts_Enter(object sender, EventArgs e)
        {
            textBoxDescription.Text =
@"Force Orthogonal Metric Products:

If true, any metric products on non-orthogonal derived frames' multivectors are replaced by equivalent orthogonal operations using derived-to-base and base-to-derived outermorphisms on the derived frames When this flag is used better compilation time is achieved but more computations are generated.

This flag is used during AST basic expressions generation.";
        }

        private void checkBoxReduceLowLevelRhsSubExpressions_Enter(object sender, EventArgs e)
        {
            textBoxDescription.Text =
@"Reduce Low Level RHS Sub-Expressions:

When True the low-level optimizer attempts to extract all common sub-expressions in all rhs values and refactor the sub-expressions as temporary variables. This may take longer time during low level optimization.

This flag is used during low-level optimization of a macro's code.";
        }

        private void checkBoxSimplifyLowLevelRhsValues_Enter(object sender, EventArgs e)
        {
            textBoxDescription.Text =
@"Simplify Low Level RHS Values:

When True the low-level generator uses Mathematica's Simplify[] function on all rhs values before assigning them to lhs temp or output variables.

This flag us used during low-level generation of a macro's code.";
        }

        private void groupBoxLowLevelPropagationMethod_Enter(object sender, EventArgs e)
        {
            textBoxDescription.Text =
@"Low Level Propagation Method:

During low-level intermediate code generation this option selects a method for using the item in the RHS of any following items. If item A depende on item B in its RHS value (for example 'A = 3 * B') there are 3 cases:

    1- Propagate constants only: If B is assigned a constant value (for example 'B = 5') then propagate the value of B not the symbol B (A = 15) hence B is not required anymore. Else propagate the symbol B ('A = 3 * B' as is).

    2- Propagate constants and single variables: If B is a constant or is assigned a single variable (for example 'B = C') propagate the RHS assigned to B (i.e. A = 3 * C) hence B is not required anymore. Else propagate the symbol B ('A = 3 * B' as is).

    3- Propagate constants and expressions depending on a single variable: If B is a constant or is assigned an expression depending on a single variable (for example 'B = C + Power[C, 2]') propagate the RHS assigned to B (i.e. A = 3 * C + 3 * Power[C, 2]) hence B is not required anymore. Else propagate the symbol B ('A = 3 * B' as is).";
        }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            FillOptions();
            
            DialogResult = DialogResult.OK;
        }
    }
}
