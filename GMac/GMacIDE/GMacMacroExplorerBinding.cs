using System;
using System.Text;

namespace GMac.GMacIDE
{
    internal sealed class GMacMacroExplorerBinding
    {
        public static GMacMacroExplorerBinding Parse(string bindingLineText)
        {
            bindingLineText = bindingLineText?.Trim();

            if (string.IsNullOrEmpty(bindingLineText))
                return null;

            var eqIndex = bindingLineText.IndexOf('=');

            //It's a variable binding
            if (eqIndex < 0)
                return new GMacMacroExplorerBinding(
                    bindingLineText.Trim(),
                    false
                    );

            //Found the equal sign, more tests to find kind of binding
            var valueAccessName = bindingLineText.Substring(0, eqIndex).Trim();
            bindingLineText = bindingLineText.Substring(eqIndex + 1).Trim();

            var constantIndex = bindingLineText.IndexOf("constant:", StringComparison.Ordinal);
            var variableIndex = bindingLineText.IndexOf("variable:", StringComparison.Ordinal);
            var testIndex = bindingLineText.IndexOf("test:", StringComparison.Ordinal);

            //It's a constant binding
            if (constantIndex < 0 && variableIndex < 0 && testIndex < 0)
                return new GMacMacroExplorerBinding(
                    valueAccessName,
                    true,
                    bindingLineText
                    );

            //It's a constant binding
            if (constantIndex == 0)
                return new GMacMacroExplorerBinding(
                    valueAccessName,
                    true,
                    bindingLineText.Substring("constant:".Length)
                );

            //It's a variable binding
            string targetVariableName = "";
            string testValueText = "";

            if (variableIndex >= 0)
            {
                var n1 = variableIndex + "variable:".Length;
                var n2 = (testIndex > variableIndex ? testIndex : bindingLineText.Length) - n1;
                targetVariableName = bindingLineText.Substring(n1, n2).Trim();
            }

            if (testIndex >= 0)
            {
                var n1 = testIndex + "test:".Length;
                var n2 = (variableIndex > testIndex ? variableIndex : bindingLineText.Length) - n1;
                testValueText = bindingLineText.Substring(n1, n2).Trim();
            }

            return new GMacMacroExplorerBinding(
                valueAccessName,
                false,
                "",
                targetVariableName,
                testValueText
                );
        }


        public string ValueAccessName { get; }

        public bool IsConstantBinding { get; }

        public bool IsVariableBinding => !IsConstantBinding;

        public bool HasTargetVariableName => !string.IsNullOrEmpty(TargetVariableName);

        public bool HasTestValue => !string.IsNullOrEmpty(TestValueText);

        public string ConstantValueText { get; }

        public string TargetVariableName { get; }

        public string TestValueText { get; }




        private GMacMacroExplorerBinding(string valueAccessName, bool isConstantBinding, string constantValueText = "", string targetVariableNameText = "", string testValueText = "")
        {
            ValueAccessName = valueAccessName;
            IsConstantBinding = isConstantBinding;
            ConstantValueText = constantValueText;
            TargetVariableName = targetVariableNameText;
            TestValueText = testValueText;
        }


        public override string ToString()
        {
            var s = new StringBuilder();

            s.Append(ValueAccessName);

            if (IsConstantBinding)
            {
                s.Append(" = constant: ").Append(ConstantValueText);
            }
            else
            {
                s.Append(" = variable: ").Append(TargetVariableName);
                s.Append(" test: ").Append(TestValueText);
            }

            return s.ToString();
        }
    }
}
