using System.Collections.Generic;
using GeometricAlgebraSymbolicsLib;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica;
using GMac.Engine.API.Binding;
using GMac.Engine.AST.Symbols;

namespace GMac.IDE
{
    internal sealed class GMacMacroExplorerBindingData
    {
        private readonly Dictionary<string, GMacMacroExplorerBinding> _dictionary
            = new Dictionary<string, GMacMacroExplorerBinding>();


        public IEnumerable<GMacMacroExplorerBinding> Bindings =>
            _dictionary.Values;

        public GMacMacroExplorerBinding this[string valueAccessName]
        {
            get
            {
                return _dictionary.TryGetValue(valueAccessName, out var value) ? value : null;
            }
            set
            {
                if (_dictionary.ContainsKey(valueAccessName))
                    _dictionary[valueAccessName] = value;

                else
                    _dictionary.Add(valueAccessName, value);
            }
        }


        public GMacMacroExplorerBinding Add(GMacMacroExplorerBinding binding)
        {
            if (binding == null) return null;

            this[binding.ValueAccessName] = binding;

            return binding;
        }

        public GMacMacroExplorerBinding AddFromText(string bindingText)
        {
            var binding = GMacMacroExplorerBinding.Parse(bindingText);

            if (binding == null) return null;

            this[binding.ValueAccessName] = binding;

            return binding;
        }

        public GMacMacroExplorerBindingData Clear()
        {
            _dictionary.Clear();

            return this;
        }

        public GMacMacroBinding ToMacroBinding(AstMacro baseMacro)
        {
            var macroBinding = GMacMacroBinding.Create(baseMacro);

            foreach (var bindingData in Bindings)
            {
                if (bindingData.IsConstantBinding)
                {
                    macroBinding.BindScalarToConstant(
                        bindingData.ValueAccessName,
                        bindingData.ConstantValueText
                    );

                    continue;
                }

                var testValueExpr =
                    bindingData.HasTestValue
                        ? bindingData.TestValueText.ToExpr(GaSymbolicsUtils.Cas)
                        : null;

                macroBinding.BindToVariables(
                    bindingData.ValueAccessName,
                    testValueExpr
                );

                if (bindingData.HasTargetVariableName)
                    macroBinding[bindingData.ValueAccessName].TargetVariableName =
                        bindingData.TargetVariableName;
            }

            return macroBinding;
        }
    }
}
