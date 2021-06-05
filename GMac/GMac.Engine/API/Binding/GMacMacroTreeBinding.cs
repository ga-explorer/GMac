using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMac.Engine.AST.Symbols;
using GMac.Engine.Compiler.Semantic.AST;

namespace GMac.Engine.API.Binding
{
    public sealed class GMacMacroTreeBinding : IGMacCompositeBinding, IEnumerable<KeyValuePair<string, IGMacTypedBinding>>
    {
        public static GMacMacroTreeBinding Create(AstMacro baseMacroInfo, GMacBindOutputToConstantBehavior outputToConstAction = GMacBindOutputToConstantBehavior.Prevent)
        {
            return new GMacMacroTreeBinding(baseMacroInfo) { BindOutputToConstantAction = outputToConstAction };
        }


        private readonly Dictionary<string, IGMacTypedBinding> _patternDictionary =
            new Dictionary<string, IGMacTypedBinding>();


        public AstMacro BaseMacro { get; }

        internal GMacMacro BaseGMacMacro => BaseMacro.AssociatedMacro;

        /// <summary>
        /// Determines the action that should be taken when trying to bind a macro output parameter to a constant
        /// </summary>
        public GMacBindOutputToConstantBehavior BindOutputToConstantAction { get; set; }


        public IEnumerable<AstMacroParameter> BoundParameters
        {
            get { return _patternDictionary.Select(pair => BaseGMacMacro.GetParameter(pair.Key).ToAstMacroParameter()); }
        }

        public IEnumerable<string> BoundParametersNames
        {
            get { return _patternDictionary.Select(pair => pair.Key); }
        }

        public IEnumerable<AstMacroParameter> NotBoundParameters
        {
            get
            {
                return
                    BaseGMacMacro
                    .Parameters
                    .Where(macroParameter => _patternDictionary.ContainsKey(macroParameter.ObjectName) == false)
                    .Select(p => p.ToAstMacroParameter());
            }
        }

        public IEnumerable<string> NotBoundParametersNames
        {
            get
            {
                return
                    BaseGMacMacro
                    .Parameters
                    .Select(macroParameter => macroParameter.ObjectName)
                    .Where(macroParameterName => _patternDictionary.ContainsKey(macroParameterName) == false);
            }
        }

        public bool HasConstantComponent
        {
            get
            {
                return _patternDictionary.Any(pair => pair.Value.HasConstantComponent);
            }
        }

        public bool HasVariableComponent
        {
            get
            {
                return _patternDictionary.Any(pair => pair.Value.HasVariableComponent);
            }
        }


        private GMacMacroTreeBinding(AstMacro baseMacroInfo)
        {
            BaseMacro = baseMacroInfo;
        }


        public void Clear()
        {
            _patternDictionary.Clear();
        }

        public bool HasBoundParameter(string parameterName)
        {
            return _patternDictionary.ContainsKey(parameterName);
        }


        public GMacMacroTreeBinding BindParameterToPattern(string parameterName, IGMacTypedBinding pattern)
        {
            if (BaseGMacMacro.LookupParameter(parameterName, out var macroParameter) == false)
                throw new KeyNotFoundException("Macro parameter " + parameterName + " not found");

            if (macroParameter.HasSameType(pattern.GMacType.AssociatedType) == false)
                throw new InvalidOperationException(
                    $"Can't bind macro parameter {macroParameter.SymbolAccessName} of type {macroParameter.SymbolTypeSignature} to pattern of type {pattern.GMacType.GMacTypeSignature}"
                    );

            if (macroParameter.DirectionOut && pattern.HasConstantComponent)
                switch (BindOutputToConstantAction)
                {
                    case GMacBindOutputToConstantBehavior.Prevent:
                        throw new InvalidOperationException(
                            $"Unable to bind the output macro parameter {parameterName} to a pattern with constant components"
                            );

                    case GMacBindOutputToConstantBehavior.Ignore:
                        return this;

                    case GMacBindOutputToConstantBehavior.BindToVariable:
                        pattern = pattern.ToConstantsFreePattern() as IGMacTypedBinding;
                        break;
                }

            if (_patternDictionary.ContainsKey(parameterName))
                _patternDictionary[parameterName] = pattern;

            else
                _patternDictionary.Add(parameterName, pattern);

            return this;
        }


        public override string ToString()
        {
            var s = new StringBuilder();

            s.Append(BaseGMacMacro.SymbolAccessName)
                .Append("(");

            if (_patternDictionary.Count > 0)
            {
                var separator = ", " + Environment.NewLine;

                foreach (var pair in _patternDictionary)
                    s.Append("    ")
                        .Append(pair.Key)
                        .Append(" = ")
                        .Append(pair.Value)
                        .Append(separator);

                s.Length -= separator.Length;

                s.AppendLine();
            }

            s.Append(")");

            return s.ToString();
        }

        public IEnumerator<KeyValuePair<string, IGMacTypedBinding>> GetEnumerator()
        {
            return _patternDictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _patternDictionary.GetEnumerator();
        }

        public IGMacCompositeBinding PickConstantComponents()
        {
            var macroPattern = new GMacMacroTreeBinding(BaseMacro);

            foreach (var pair in _patternDictionary.Where(pair => pair.Value.HasConstantComponent))
            {
                var scalarPattern = pair.Value as GMacScalarBinding;

                if (scalarPattern != null)
                {
                    macroPattern._patternDictionary.Add(pair.Key, scalarPattern);

                    continue;
                }

                var compositePattern = pair.Value as IGMacCompositeBinding;

                if (compositePattern != null)
                    macroPattern._patternDictionary.Add(
                        pair.Key,
                        compositePattern.PickConstantComponents() as IGMacTypedBinding
                        );
            }

            return macroPattern;
        }

        public IGMacCompositeBinding PickVariableComponents()
        {
            var macroPattern = new GMacMacroTreeBinding(BaseMacro);

            foreach (var pair in _patternDictionary.Where(pair => pair.Value.HasVariableComponent))
            {
                var scalarPattern = pair.Value as GMacScalarBinding;

                if (scalarPattern != null)
                {
                    macroPattern._patternDictionary.Add(pair.Key, scalarPattern);

                    continue;
                }

                var compositePattern = pair.Value as IGMacCompositeBinding;

                if (compositePattern != null)
                    macroPattern._patternDictionary.Add(
                        pair.Key,
                        compositePattern.PickVariableComponents() as IGMacTypedBinding
                        );
            }

            return macroPattern;
        }

        public IGMacCompositeBinding PickConstantComponentsAsVariables()
        {
            var macroPattern = new GMacMacroTreeBinding(BaseMacro);

            foreach (var pair in _patternDictionary.Where(pair => pair.Value.HasConstantComponent))
            {
                var scalarPattern = pair.Value as GMacScalarBinding;

                if (scalarPattern != null)
                {
                    macroPattern._patternDictionary.Add(
                        pair.Key,
                        scalarPattern.ToConstantsFreePattern() as IGMacTypedBinding
                        );

                    continue;
                }

                var compositePattern = pair.Value as IGMacCompositeBinding;

                if (compositePattern != null)
                    macroPattern._patternDictionary.Add(
                        pair.Key,
                        compositePattern.PickConstantComponentsAsVariables() as IGMacTypedBinding
                        );
            }

            return macroPattern;
        }

        public IGMacBinding ToConstantsFreePattern()
        {
            var macroPattern = new GMacMacroTreeBinding(BaseMacro);

            foreach (var pair in _patternDictionary)
                macroPattern
                    ._patternDictionary
                    .Add(
                        pair.Key,
                        pair.Value.ToConstantsFreePattern() as IGMacTypedBinding
                        );

            return macroPattern;
        }
    }
}
