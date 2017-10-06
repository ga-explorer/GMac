using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMac.GMacAST;
using GMac.GMacAST.Symbols;
using IronyGrammars.Semantic.Expression.Value;
using TextComposerLib.Text.Parametric;

namespace GMac.GMacAPI.Binding
{
    /// <summary>
    /// This class represents an abstract binding pattern of a composite sub-component of some 
    /// GMac data-store to a structure of the same structure type
    /// </summary>
    public sealed class GMacStructureBinding : IGMacCompositeBinding, IGMacTypedBinding, IEnumerable<KeyValuePair<string, IGMacTypedBinding>>
    {
        public static GMacStructureBinding Create(AstStructure patternType)
        {
            return new GMacStructureBinding(patternType);
        }


        private readonly Dictionary<string, IGMacTypedBinding> _patternDictionary =
            new Dictionary<string, IGMacTypedBinding>();


        /// <summary>
        /// The GMac type of this structure pattern
        /// </summary>
        public AstType GMacType => BaseStructure.GMacType;

        /// <summary>
        /// The GMac structure type of this structure pattern
        /// </summary>
        public AstStructure BaseStructure { get; }


        /// <summary>
        /// The structure members used in this binding pattern
        /// </summary>
        public IEnumerable<AstStructureDataMember> BoundMembers
        {
            get { return _patternDictionary.Select(pair => BaseStructure.DataMember(pair.Key)); }
        }

        /// <summary>
        /// The structure members names used in this binding pattern
        /// </summary>
        public IEnumerable<string> BoundMembersNames
        {
            get { return _patternDictionary.Select(pair => pair.Key); }
        }

        /// <summary>
        /// The structure members not used in this binding pattern
        /// </summary>
        public IEnumerable<AstStructureDataMember> NotBoundMembers
        {
            get
            {
                return
                    BaseStructure
                    .DataMembers
                    .Where(dataMember => _patternDictionary.ContainsKey(dataMember.Name) == false);
            }
        }

        /// <summary>
        /// The structure members names not used in this binding pattern
        /// </summary>
        public IEnumerable<string> NotBoundMembersNames
        {
            get
            {
                return
                    BaseStructure
                    .DataMembers
                    .Select(dataMember => dataMember.Name)
                    .Where(dataMemberName => _patternDictionary.ContainsKey(dataMemberName) == false);
            }
        }

        /// <summary>
        /// True if any primitive sub-component of this pattern in any level has a constant scalar binding pattern
        /// </summary>
        public bool HasConstantComponent
        {
            get
            {
                return _patternDictionary.Any(pair => pair.Value.HasConstantComponent);
            }
        }

        /// <summary>
        /// True if any primitive sub-component of this pattern in any level has a variable scalar binding pattern
        /// </summary>
        public bool HasVariableComponent
        {
            get
            {
                return _patternDictionary.Any(pair => pair.Value.HasVariableComponent);
            }
        }


        private GMacStructureBinding(AstStructure patternType)
        {
            BaseStructure = patternType;
        }


        /// <summary>
        /// Clear all components of this pattern
        /// </summary>
        public void Clear()
        {
            _patternDictionary.Clear();
        }

        /// <summary>
        /// True if this pattern has a binding of the given structure data member
        /// </summary>
        /// <param name="memberName"></param>
        /// <returns></returns>
        public bool HasBoundMember(string memberName)
        {
            return _patternDictionary.ContainsKey(memberName);
        }


        public GMacStructureBinding BindMemberToPattern(string memberName, IGMacTypedBinding pattern)
        {
            var dataMember = BaseStructure.DataMember(memberName);

            if (dataMember.IsNullOrInvalid())
                throw new KeyNotFoundException("Structure data member " + memberName + " not found");

            if (dataMember.AssociatedDataMember.HasSameType(pattern.GMacType.AssociatedType) == false)
                throw new InvalidOperationException(
                    $"Can't bind structure data member {dataMember.AccessName} of type {dataMember.GMacTypeSignature} to pattern of type {pattern.GMacType.GMacTypeSignature}"
                    );

            if (_patternDictionary.ContainsKey(memberName))
                _patternDictionary[memberName] = pattern;

            else
                _patternDictionary.Add(memberName, pattern);

            return this;
        }


        public override string ToString()
        {
            var s = new StringBuilder();

            s.Append(BaseStructure.AccessName)
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
            var structPattern = new GMacStructureBinding(BaseStructure);

            foreach (var pair in _patternDictionary.Where(pair => pair.Value.HasConstantComponent))
            {
                var scalarPattern = pair.Value as GMacScalarBinding;

                if (scalarPattern != null)
                {
                    structPattern._patternDictionary.Add(pair.Key, scalarPattern);

                    continue;
                }

                var compositePattern = pair.Value as IGMacCompositeBinding;

                if (compositePattern != null)
                    structPattern._patternDictionary.Add(
                        pair.Key, 
                        compositePattern.PickConstantComponents() as IGMacTypedBinding
                        );
            }

            return structPattern;
        }

        public IGMacCompositeBinding PickVariableComponents()
        {
            var structPattern = new GMacStructureBinding(BaseStructure);

            foreach (var pair in _patternDictionary.Where(pair => pair.Value.HasVariableComponent))
            {
                var scalarPattern = pair.Value as GMacScalarBinding;

                if (scalarPattern != null)
                {
                    structPattern._patternDictionary.Add(pair.Key, scalarPattern);

                    continue;
                }

                var compositePattern = pair.Value as IGMacCompositeBinding;

                if (compositePattern != null)
                    structPattern._patternDictionary.Add(
                        pair.Key, 
                        compositePattern.PickVariableComponents() as IGMacTypedBinding
                        );
            }

            return structPattern;
        }

        public IGMacCompositeBinding PickConstantComponentsAsVariables()
        {
            var structPattern = new GMacStructureBinding(BaseStructure);

            foreach (var pair in _patternDictionary.Where(pair => pair.Value.HasConstantComponent))
            {
                var scalarPattern = pair.Value as GMacScalarBinding;

                if (scalarPattern != null)
                {
                    structPattern._patternDictionary.Add(
                        pair.Key, 
                        scalarPattern.ToConstantsFreePattern() as IGMacTypedBinding
                        );

                    continue;
                }

                var compositePattern = pair.Value as IGMacCompositeBinding;

                if (compositePattern != null)
                    structPattern._patternDictionary.Add(
                        pair.Key, 
                        compositePattern.PickConstantComponentsAsVariables() as IGMacTypedBinding
                        );
            }

            return structPattern;
        }

        public IGMacBinding ToConstantsFreePattern()
        {
            var structPattern = new GMacStructureBinding(BaseStructure);

            foreach (var pair in _patternDictionary)
                structPattern
                    ._patternDictionary
                    .Add(
                        pair.Key,
                        pair.Value.ToConstantsFreePattern() as IGMacTypedBinding
                        );

            return structPattern;
        }

        public ValueStructureSparse ToValue(StringSequenceTemplate varNameTemplate)
        {
            var structValue = ValueStructureSparse.Create(BaseStructure.AssociatedStructure);

            foreach (var pair in _patternDictionary)
            {
                var scalarPattern = pair.Value as GMacScalarBinding;

                if (scalarPattern != null)
                {
                    structValue[pair.Key] = scalarPattern.ToValue(varNameTemplate).AssociatedValue;

                    continue;
                }

                var mvPattern = pair.Value as GMacMultivectorBinding;

                if (mvPattern != null)
                {
                    structValue[pair.Key] = mvPattern.ToValue(varNameTemplate).AssociatedValue;

                    continue;
                }

                var structPattern = pair.Value as GMacStructureBinding;

                if (structPattern != null)
                    structValue[pair.Key] = structPattern.ToValue(varNameTemplate);
            }
                
            return structValue;
        }
    }
}
