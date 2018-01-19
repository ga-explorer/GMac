using System.Collections.Generic;

namespace GMac.GMacIDE
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
                GMacMacroExplorerBinding value;

                return _dictionary.TryGetValue(valueAccessName, out value) ? value : null;
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
    }
}
