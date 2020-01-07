using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeComposerLib.Irony.Semantic.Type;
using DataStructuresLib;
using GeometricAlgebraSymbolicsLib;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;
using GMac.GMacAPI.CodeBlock;
using GMac.GMacAST;
using GMac.GMacAST.Expressions;
using GMac.GMacAST.Symbols;
using GMac.GMacCompiler.Semantic.AST;
using GMac.GMacCompiler.Semantic.AST.Extensions;
using GMac.GMacCompiler.Semantic.ASTDebug;
using GMac.GMacCompiler.Semantic.ASTInterpreter.LowLevel.Generator;
using GMac.GMacCompiler.Semantic.ASTInterpreter.LowLevel.Optimizer;
using TextComposerLib;

using TextComposerLib.Logs.Progress;
using TextComposerLib.Text;
using TextComposerLib.Text.Linear;
using Wolfram.NETLink;

namespace GMac.GMacAPI.Binding
{
    /// <summary>
    /// This class holds a list of low-level macro parameter data-store value access bindings to scalar binding 
    /// ptterns
    /// </summary>
    public sealed class GMacMacroBinding : IProgressReportSource
    {
        /// <summary>
        /// Create an empty macro parameters list binding pattern
        /// </summary>
        /// <param name="baseMacro"></param>
        /// <param name="outputToConstAction"></param>
        /// <returns></returns>
        public static GMacMacroBinding Create(AstMacro baseMacro, GMacBindOutputToConstantBehavior outputToConstAction = GMacBindOutputToConstantBehavior.BindToVariable)
        {
            return new GMacMacroBinding(baseMacro) { BindOutputToConstantBehavior = outputToConstAction };
        }

        /// <summary>
        /// Create a macro parameters list binding pattern from a tree pattern
        /// </summary>
        /// <param name="macroTreeBinding"></param>
        /// <param name="outputToConstAction"></param>
        /// <returns></returns>
        public static GMacMacroBinding Create(GMacMacroTreeBinding macroTreeBinding, GMacBindOutputToConstantBehavior outputToConstAction)
        {
            var listPattern = new GMacMacroBinding(macroTreeBinding.BaseMacro)
            {
                BindOutputToConstantBehavior = outputToConstAction
            };

            return listPattern.BindUsing(macroTreeBinding);
        }

        /// <summary>
        /// Create a macro parameters list binding pattern from a tree pattern
        /// </summary>
        /// <param name="macroTreeBinding"></param>
        /// <returns></returns>
        public static GMacMacroBinding Create(GMacMacroTreeBinding macroTreeBinding)
        {
            var listPattern = new GMacMacroBinding(macroTreeBinding.BaseMacro)
            {
                BindOutputToConstantBehavior = macroTreeBinding.BindOutputToConstantAction
            };

            return listPattern.BindUsing(macroTreeBinding);
        }



        private readonly Dictionary<string, GMacMacroParameterBinding> _patternDictionary =
            new Dictionary<string, GMacMacroParameterBinding>();

        /// <summary>
        /// The base macro for this binding pattern
        /// </summary>
        internal GMacMacro AssociatedMacro => BaseMacro.AssociatedMacro;

        internal TypePrimitive ScalarType => BaseMacro.AssociatedMacro.GMacRootAst.ScalarType;


        public string ProgressSourceId => "GMac Macro Binding";

        public ProgressComposer Progress => GMacSystemUtils.Progress;


        /// <summary>
        /// Determines the action that should be taken when trying to bind a macro output parameter to a constant
        /// </summary>
        public GMacBindOutputToConstantBehavior BindOutputToConstantBehavior { get; set; }

        /// <summary>
        /// The base macro for this macro binding
        /// </summary>
        public AstMacro BaseMacro { get; }

        /// <summary>
        /// If true, no optimization by re-ordering of output variables computation is performed
        /// </summary>
        public bool FixOutputComputationsOrder { get; set; }


        /// <summary>
        /// A list of parameters value access bindings defined in this macro binding
        /// </summary>
        public IEnumerable<GMacMacroParameterBinding> Bindings
        {
            get { return _patternDictionary.Select(pair => pair.Value); }
        }

        /// <summary>
        /// The primitive bindings of this macro binding
        /// </summary>
        public IEnumerable<GMacMacroParameterBinding> ConstantBindings
        {
            get
            {
                return
                    _patternDictionary
                    .Where(p => p.Value.IsConstant)
                    .Select(pair => pair.Value);
            }
        }

        /// <summary>
        /// The primitive bindings of this macro binding
        /// </summary>
        public IEnumerable<GMacMacroParameterBinding> VariableBindings
        {
            get
            {
                return
                    _patternDictionary
                    .Where(p => p.Value.IsVariable)
                    .Select(pair => pair.Value);
            }
        }

        /// <summary>
        /// The primitive bindings of this macro binding
        /// </summary>
        public IEnumerable<GMacMacroParameterBinding> InputBindings
        {
            get
            {
                return
                    _patternDictionary
                    .Where(p => p.Value.IsInput)
                    .Select(pair => pair.Value);
            }
        }

        /// <summary>
        /// The primitive bindings of this macro binding
        /// </summary>
        public IEnumerable<GMacMacroParameterBinding> OutputBindings
        {
            get
            {
                return
                    _patternDictionary
                    .Where(p => p.Value.IsOutput)
                    .Select(pair => pair.Value);
            }
        }

        /// <summary>
        /// The primitive bindings of this macro binding
        /// </summary>
        public IEnumerable<GMacMacroParameterBinding> VariableInputBindings
        {
            get
            {
                return
                    _patternDictionary
                    .Where(p => p.Value.IsVariable && p.Value.IsInput)
                    .Select(pair => pair.Value);
            }
        }

        /// <summary>
        /// The primitive bindings of this macro binding
        /// </summary>
        public IEnumerable<GMacMacroParameterBinding> VariableOutputBindings
        {
            get
            {
                return
                    _patternDictionary
                    .Where(p => p.Value.IsVariable && p.Value.IsOutput)
                    .Select(pair => pair.Value);
            }
        }



        /// <summary>
        /// The primitive parameter value access components used in this macro binding
        /// </summary>
        public IEnumerable<AstDatastoreValueAccess> UsedParameters
        {
            get { return _patternDictionary.Select(p => p.Value.ValueAccess); }
        }

        /// <summary>
        /// The primitive parameter value access components names used in this macro binding
        /// </summary>
        public IEnumerable<string> UsedParametersNames
        {
            get { return _patternDictionary.Select(p => p.Value.ValueAccessName); }
        }

        /// <summary>
        /// The constant parameters used in this binding
        /// </summary>
        public IEnumerable<AstDatastoreValueAccess> UsedConstantParameters
        {
            get
            {
                return
                    _patternDictionary
                    .Where(p => p.Value.IsConstant)
                    .Select(p => p.Value.ValueAccess);
            }
        }

        /// <summary>
        /// The constant parameters names used in this binding
        /// </summary>
        public IEnumerable<string> UsedConstantParameterNames
        {
            get
            {
                return
                    _patternDictionary
                    .Where(p => p.Value.IsConstant)
                    .Select(p => p.Value.ValueAccessName);
            }
        }

        /// <summary>
        /// The constant parameters used in this binding
        /// </summary>
        public IEnumerable<AstDatastoreValueAccess> UsedVariableParameters
        {
            get
            {
                return
                    _patternDictionary
                    .Where(p => p.Value.IsVariable)
                    .Select(p => p.Value.ValueAccess);
            }
        }

        /// <summary>
        /// The constant parameters names used in this binding
        /// </summary>
        public IEnumerable<string> UsedVariableParameterNames
        {
            get
            {
                return
                    _patternDictionary
                    .Where(p => p.Value.IsVariable)
                    .Select(p => p.Value.ValueAccessName);
            }
        }

        /// <summary>
        /// The input parameters used in this binding
        /// </summary>
        public IEnumerable<AstDatastoreValueAccess> UsedInputs
        {
            get
            {
                return 
                    _patternDictionary
                    .Where(p => p.Value.IsInput)
                    .Select(p => p.Value.ValueAccess);
            }
        }

        /// <summary>
        /// The input parameters names used in this binding
        /// </summary>
        public IEnumerable<string> UsedInputNames
        {
            get
            {
                return 
                    _patternDictionary
                    .Where(p => p.Value.IsInput)
                    .Select(p => p.Value.ValueAccessName);
            }
        }

        /// <summary>
        /// The input parameters used in this binding
        /// </summary>
        public IEnumerable<AstDatastoreValueAccess> UsedOutputs
        {
            get
            {
                return
                    _patternDictionary
                    .Where(p => p.Value.IsOutput)
                    .Select(p => p.Value.ValueAccess);
            }
        }

        /// <summary>
        /// The input parameters names used in this binding
        /// </summary>
        public IEnumerable<string> UsedOutputNames
        {
            get
            {
                return
                    _patternDictionary
                    .Where(p => p.Value.IsOutput)
                    .Select(p => p.Value.ValueAccessName);
            }
        }

        /// <summary>
        /// The variable input parameters used in this binding
        /// </summary>
        public IEnumerable<AstDatastoreValueAccess> UsedVariableInputs
        {
            get
            {
                return
                    _patternDictionary
                    .Where(p => p.Value.IsVariable && p.Value.IsInput)
                    .Select(p => p.Value.ValueAccess);
            }
        }

        /// <summary>
        /// The input parameters names used in this binding
        /// </summary>
        public IEnumerable<string> UsedVariableInputNames
        {
            get
            {
                return
                    _patternDictionary
                    .Where(p => p.Value.IsVariable && p.Value.IsInput)
                    .Select(p => p.Value.ValueAccessName);
            }
        }

        /// <summary>
        /// The variable output parameters used in this binding
        /// </summary>
        public IEnumerable<AstDatastoreValueAccess> UsedVariableOutputs
        {
            get
            {
                return
                    _patternDictionary
                    .Where(p => p.Value.IsVariable && p.Value.IsOutput)
                    .Select(p => p.Value.ValueAccess);
            }
        }

        /// <summary>
        /// The variable output parameters names used in this binding
        /// </summary>
        public IEnumerable<string> UsedVariableOutputNames
        {
            get
            {
                return
                    _patternDictionary
                    .Where(p => p.Value.IsVariable && p.Value.IsOutput)
                    .Select(p => p.Value.ValueAccessName);
            }
        }

        /// <summary>
        /// Gets the binding associated with the given primitive parameter value access component
        /// </summary>
        /// <param name="valueAccess"></param>
        /// <returns></returns>
        public GMacMacroParameterBinding this[AstDatastoreValueAccess valueAccess] 
            => _patternDictionary[valueAccess.ValueAccessName];

        /// <summary>
        /// Gets the binding associated with the given primitive parameter value access component
        /// </summary>
        /// <param name="valueAccessName"></param>
        /// <returns></returns>
        public GMacMacroParameterBinding this[string valueAccessName]
            => _patternDictionary[valueAccessName];

        /// <summary>
        /// Return a list of parameters with variable binding and having target
        /// variable names. This is only for use within the FormGMacMacroExplorer
        /// </summary>
        internal IEnumerable<KeyValuePair<string, string>> TargetVariablesNames
            => _patternDictionary
                .Where(p =>
                    p.Value.IsVariable && 
                    !string.IsNullOrEmpty(p.Value.TargetVariableName)
                ).Select(p => 
                    new KeyValuePair<string, string>(
                        p.Key, 
                        p.Value.TargetVariableName
                    )
                );

        /// <summary>
        /// If any output parameter is bound the macro binding is considered ready
        /// </summary>
        public bool IsMacroBindingReady
        {
            get
            {
                return _patternDictionary.Any(pair => pair.Value.ValueAccess.IsOutputParameter);
            }
        }


        private GMacMacroBinding(AstMacro baseMacro)
        {
            BaseMacro = baseMacro;
        }


        /// <summary>
        /// Compile a string into a LanguageValueAccess object with macro parameter as its root
        /// </summary>
        /// <param name="valueAccessName"></param>
        /// <returns></returns>
        internal AstDatastoreValueAccess ToValueAccess(string valueAccessName)
        {
            return BaseMacro.Parameter(valueAccessName);
                //_expressionCompiler
                //.MacroParameterValueAccess(
                //.AssociatedValueAccess;
        }

        internal AstDatastoreValueAccess ToValueAccess(string valueAccessName, ILanguageType requiredType)
        {
            var valueAccess = ToValueAccess(valueAccessName);

            if (valueAccess.GMacType.AssociatedType.IsSameType(requiredType))
                return valueAccess;

            throw new InvalidOperationException(
                $"Specified macro parameter {valueAccessName} is not of required type {requiredType.TypeSignature}"
                );
        }


        /// <summary>
        /// Clear all parameters bindings
        /// </summary>
        public void Clear()
        {
            _patternDictionary.Clear();
        }

        /// <summary>
        /// A list of parameters value access bindings defined in this macro binding that are sub-components
        /// of the given macro parameter value access
        /// </summary>
        /// <param name="paramValueAccess"></param>
        /// <returns></returns>
        public IEnumerable<GMacMacroParameterBinding> GetBindings(AstDatastoreValueAccess paramValueAccess)
        {
            var result = new List<GMacMacroParameterBinding>();

            if (paramValueAccess.IsNullOrInvalid()) return result;

            var name = paramValueAccess.ValueAccessName;

            //A single parameter is to be found
            if (paramValueAccess.IsPrimitive)
            {
                var item = Bindings.FirstOrDefault(
                    p => p.ValueAccessName == name
                    );

                if (ReferenceEquals(item, null) == false)
                    result.Add(item);

                return result;
            }

            //Partial multivectors require special treatment
            if (paramValueAccess.IsPartialMultivector)
            {
                var primitiveValueAccessList = paramValueAccess.ExpandAll();

                foreach (var primitiveValueAccess in primitiveValueAccessList)
                {
                    if (_patternDictionary.TryGetValue(primitiveValueAccess.ValueAccessName, out var paramVar))
                        result.Add(paramVar);
                }

                return result;
            }

            //All other cases can be searched by name
            name = name + ".";

            result.AddRange(
                Bindings.Where(
                    p => p.ValueAccessName.IndexOf(name, StringComparison.Ordinal) == 0
                    )
                );

            return result;
        }

        /// <summary>
        /// A list of parameters value access objects used in this macro binding that are sub-components
        /// of the given macro parameter value access
        /// </summary>
        /// <param name="paramValueAccess"></param>
        /// <returns></returns>
        public IEnumerable<AstDatastoreValueAccess> GetUsedParameters(AstDatastoreValueAccess paramValueAccess)
        {
            return GetBindings(paramValueAccess).Select(b => b.ValueAccess);
        }

        //TODO: Implement more methods to group macro parameters into composite patterns

        /// <summary>
        /// Try to find the scalar pattern associated with the given primitive parameter value access component
        /// </summary>
        /// <param name="paramValueAccess"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public bool TryGetScalarBinding(AstDatastoreValueAccess paramValueAccess, out GMacScalarBinding pattern)
        {
            if (_patternDictionary.TryGetValue(paramValueAccess.ValueAccessName, out var paramBinding))
            {
                pattern = paramBinding.ToScalarBinding;
                return true;
            }

            pattern = null;
            return false;
        }

        /// <summary>
        /// Try to find the scalar macro parameter binding pattern associated with the given primitive parameter 
        /// value access component
        /// </summary>
        /// <param name="paramValueAccess"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public bool TryGetBinding(AstDatastoreValueAccess paramValueAccess, out GMacMacroParameterBinding pattern)
        {
            return _patternDictionary.TryGetValue(paramValueAccess.ValueAccessName, out pattern);
        }


        /// <summary>
        /// Remove the given parameters from the binding pattern
        /// </summary>
        /// <param name="valueAccessName"></param>
        /// <returns></returns>
        public GMacMacroBinding UnBind(string valueAccessName)
        {
            UnBind(ToValueAccess(valueAccessName));

            return this;
        }

        /// <summary>
        /// Remove the given parameters from the binding pattern
        /// </summary>
        /// <param name="valueAccess"></param>
        /// <returns></returns>
        public GMacMacroBinding UnBind(AstDatastoreValueAccess valueAccess)
        {
            if (valueAccess.IsNullOrInvalid()) return this;

            if (valueAccess.IsScalar)
            {
                _patternDictionary.Remove(valueAccess.ValueAccessName);
                return this;
            }

            var valueAccessNamesList = 
                valueAccess
                .AssociatedValueAccess
                .ExpandAll()
                .Select(v => v.GetName());

            foreach (var valueAccessName in valueAccessNamesList)
                _patternDictionary.Remove(valueAccessName);

            return this;
        }
        

        /// <summary>
        /// Bind a macro parameter of scalar type to a given scalar pattern
        /// </summary>
        /// <param name="valueAccessName"></param>
        /// <param name="scalarPattern"></param>
        /// <returns></returns>
        public GMacMacroBinding BindScalarToPattern(string valueAccessName, GMacScalarBinding scalarPattern)
        {
            return BindScalarToPattern(
                ToValueAccess(valueAccessName, ScalarType),
                scalarPattern
                );
        }

        /// <summary>
        /// All binding methods eventually call this one so all checks are performed here
        /// If the given scalar binding is null the scalar macro parameter is un-bound
        /// </summary>
        /// <param name="valueAccess"></param>
        /// <param name="scalarBinding"></param>
        /// <param name="testValueExpr"></param>
        /// <returns></returns>
        public GMacMacroBinding BindScalarToPattern(AstDatastoreValueAccess valueAccess, GMacScalarBinding scalarBinding, Expr testValueExpr = null)
        {
            if (valueAccess.IsNullOrInvalid())
                throw new ArgumentNullException(nameof(valueAccess));

            var valueAccessName = valueAccess.ValueAccessName;

            if (valueAccess.IsMacroParameter == false || valueAccess.IsScalar == false)
                throw new InvalidOperationException(
                    $"Unable to bind the value access {valueAccessName} as it's either non-scalar or not a macro parameter"
                    );

            if (scalarBinding == null)
                return UnBind(valueAccess);

            if (valueAccess.AssociatedValueAccess.RootSymbol.ParentLanguageSymbol.ObjectId != AssociatedMacro.ObjectId)
                throw new InvalidOperationException(
                    $"Unable to bind the value access {valueAccessName} as it's not a peremeter of the macro {AssociatedMacro.SymbolAccessName}"
                    );

            GMacMacroParameterBinding paramBinding = null;

            if (valueAccess.IsOutputParameter && scalarBinding.IsConstant)
            {
                switch (BindOutputToConstantBehavior)
                {
                    case GMacBindOutputToConstantBehavior.Prevent:
                        throw new InvalidOperationException(
                            $"Unable to bind the output macro parameter {valueAccessName} to a constant"
                            );

                    case GMacBindOutputToConstantBehavior.Ignore:
                        return this;

                    case GMacBindOutputToConstantBehavior.BindToVariable:
                        paramBinding = GMacMacroParameterBinding.CreateVariable(valueAccess);
                        break;
                }
            }
            else
            {
                paramBinding = GMacMacroParameterBinding.Create(valueAccess, scalarBinding, testValueExpr);
            }

            //If the value access already exists remove it from the dictionary to preserve
            //order of parameters bindings
            if (_patternDictionary.ContainsKey(valueAccessName))
                _patternDictionary.Remove(valueAccessName);

            _patternDictionary.Add(valueAccessName, paramBinding);

            return this;
        }


        /// <summary>
        /// Bind the given scalar parameter to a constant
        /// </summary>
        /// <param name="valueAccessName"></param>
        /// <param name="constExpr"></param>
        /// <returns></returns>
        public GMacMacroBinding BindScalarToConstant(string valueAccessName, Expr constExpr)
        {
            return BindScalarToPattern(
                ToValueAccess(valueAccessName, ScalarType),
                GMacScalarBinding.CreateConstant(BaseMacro.Root, constExpr)
                );
        }

        /// <summary>
        /// Bind the given scalar parameter to a constant
        /// </summary>
        /// <param name="valueAccess"></param>
        /// <param name="constExpr"></param>
        /// <returns></returns>
        public GMacMacroBinding BindScalarToConstant(AstDatastoreValueAccess valueAccess, Expr constExpr)
        {
            return BindScalarToPattern(
                valueAccess, 
                GMacScalarBinding.CreateConstant(BaseMacro.Root, constExpr)
                );
        }

        /// <summary>
        /// Bind a macro parameter of scalar type to a constant
        /// </summary>
        /// <param name="valueAccessName"></param>
        /// <param name="value"></param>
        public GMacMacroBinding BindScalarToConstant(string valueAccessName, int value)
        {
            return BindScalarToPattern(
                ToValueAccess(valueAccessName, ScalarType),
                GMacScalarBinding.CreateConstant(BaseMacro.Root, value.ToExpr())
                );
        }

        /// <summary>
        /// Bind a macro parameter of scalar type to a constant
        /// </summary>
        /// <param name="valueAccess"></param>
        /// <param name="value"></param>
        public GMacMacroBinding BindScalarToConstant(AstDatastoreValueAccess valueAccess, int value)
        {
            return BindScalarToPattern(
                valueAccess,
                GMacScalarBinding.CreateConstant(BaseMacro.Root, value.ToExpr())
                );
        }

        /// <summary>
        /// Bind a macro parameter of scalar type to a constant
        /// </summary>
        /// <param name="valueAccessName"></param>
        /// <param name="value"></param>
        public GMacMacroBinding BindScalarToConstant(string valueAccessName, double value)
        {
            return BindScalarToPattern(
                ToValueAccess(valueAccessName, ScalarType),
                GMacScalarBinding.CreateConstant(BaseMacro.Root, value.ToExpr())
                );
        }

        /// <summary>
        /// Bind a macro parameter of scalar type to a constant
        /// </summary>
        /// <param name="valueAccess"></param>
        /// <param name="value"></param>
        public GMacMacroBinding BindScalarToConstant(AstDatastoreValueAccess valueAccess, double value)
        {
            return BindScalarToPattern(
                valueAccess,
                GMacScalarBinding.CreateConstant(BaseMacro.Root, value.ToExpr())
                );
        }

        /// <summary>
        /// Bind a macro parameter of scalar type to a constant
        /// </summary>
        /// <param name="valueAccessName"></param>
        /// <param name="value"></param>
        public GMacMacroBinding BindScalarToConstant(string valueAccessName, string value)
        {
            return BindScalarToPattern(
                ToValueAccess(valueAccessName, ScalarType),
                GMacScalarBinding.CreateConstant(BaseMacro.Root, value.ToExpr(GaSymbolicsUtils.Cas))
                );
        }

        /// <summary>
        /// Bind a macro parameter of scalar type to a constant
        /// </summary>
        /// <param name="valueAccess"></param>
        /// <param name="value"></param>
        public GMacMacroBinding BindScalarToConstant(AstDatastoreValueAccess valueAccess, string value)
        {
            return BindScalarToPattern(
                valueAccess,
                GMacScalarBinding.CreateConstant(BaseMacro.Root, value.ToExpr(GaSymbolicsUtils.Cas))
                );
        }

        /// <summary>
        /// Bind a macro parameter of scalar type to a constant
        /// </summary>
        /// <param name="valueAccessName"></param>
        /// <param name="value"></param>
        public GMacMacroBinding BindScalarToConstant(string valueAccessName, MathematicaScalar value)
        {
            return BindScalarToPattern(
                ToValueAccess(valueAccessName, ScalarType),
                GMacScalarBinding.CreateConstant(BaseMacro.Root, value.Expression)
                );
        }

        /// <summary>
        /// Bind a macro parameter of scalar type to a constant
        /// </summary>
        /// <param name="valueAccess"></param>
        /// <param name="value"></param>
        public GMacMacroBinding BindScalarToConstant(AstDatastoreValueAccess valueAccess, MathematicaScalar value)
        {
            return BindScalarToPattern(
                valueAccess,
                GMacScalarBinding.CreateConstant(BaseMacro.Root, value.Expression)
                );
        }

        /// <summary>
        /// Bind a macro parameter of scalar type to a constant
        /// </summary>
        /// <param name="valueAccessName"></param>
        /// <param name="value"></param>
        public GMacMacroBinding BindScalarToConstant(string valueAccessName, AstValueScalar value)
        {
            return BindScalarToPattern(
                ToValueAccess(valueAccessName, ScalarType),
                GMacScalarBinding.CreateConstant(BaseMacro.Root, value.AssociatedScalarValue.Value.Expression)
                );
        }


        /// <summary>
        /// Bind a macro parameter of multivector type to a set of target language variables or constant values
        /// using a generating function acting on each basis blade of the multivector
        /// </summary>
        /// <param name="valueAccessName"></param>
        /// <param name="bindingFunction"></param>
        /// <param name="ignoreNullPatterns"></param>
        public GMacMacroBinding BindMultivectorUsing(string valueAccessName, Func<int, GMacScalarBinding> bindingFunction, bool ignoreNullPatterns = true)
        {
            return BindMultivectorUsing(ToValueAccess(valueAccessName), bindingFunction, ignoreNullPatterns);
        }

        /// <summary>
        /// Bind a macro parameter of multivector type to a set of target language variables or constant values
        /// using a generating function acting on each basis blade of the multivector
        /// </summary>
        /// <param name="valueAccess"></param>
        /// <param name="bindingFunction"></param>
        /// <param name="ignoreNullPatterns"></param>
        /// <returns></returns>
        public GMacMacroBinding BindMultivectorUsing(AstDatastoreValueAccess valueAccess, Func<int, GMacScalarBinding> bindingFunction, bool ignoreNullPatterns = true)
        {
            if (valueAccess.IsNullOrInvalid()) 
                throw new ArgumentNullException(nameof(valueAccess));

            if (valueAccess.AssociatedValueAccess.ExpressionType.IsFrameMultivector() == false)
                throw new InvalidOperationException("Specified macro parameter is not of multivector type");

            var primitiveValueAccessList =
                valueAccess.ExpandAll();

            foreach (var primitiveValueAccess in primitiveValueAccessList)
            {
                var id = primitiveValueAccess.GetBasisBladeId();

                var scalarPattern = bindingFunction(id);

                if (ignoreNullPatterns == false || scalarPattern != null)
                    BindScalarToPattern(primitiveValueAccess, scalarPattern);
            }

            return this;
        }

        /// <summary>
        /// Bind a macro parameter of multivector type to a set of target language variables or constant values
        /// using a generating function acting on each basis blade of the multivector
        /// </summary>
        /// <param name="valueAccessName"></param>
        /// <param name="bindingFunction"></param>
        /// <param name="ignoreNullPatterns"></param>
        public GMacMacroBinding BindMultivectorUsing(string valueAccessName, Func<AstFrame, int, GMacScalarBinding> bindingFunction, bool ignoreNullPatterns = true)
        {
            return BindMultivectorUsing(ToValueAccess(valueAccessName), bindingFunction, ignoreNullPatterns);
        }

        /// <summary>
        /// Bind a macro parameter of multivector type to a set of target language variables or constant values
        /// using a generating function acting on each basis blade of the multivector
        /// </summary>
        /// <param name="valueAccess"></param>
        /// <param name="bindingFunction"></param>
        /// <param name="ignoreNullPatterns"></param>
        /// <returns></returns>
        public GMacMacroBinding BindMultivectorUsing(AstDatastoreValueAccess valueAccess, Func<AstFrame, int, GMacScalarBinding> bindingFunction, bool ignoreNullPatterns = true)
        {
            if (valueAccess.IsNullOrInvalid())
                throw new ArgumentNullException(nameof(valueAccess));

            if (valueAccess.AssociatedValueAccess.ExpressionType.IsFrameMultivector() == false)
                throw new InvalidOperationException("Specified macro parameter is not of multivector type");

            var frameInfo =
                new AstFrame(
                    ((GMacFrameMultivector)valueAccess.AssociatedValueAccess.ExpressionType).ParentFrame
                    );

            var primitiveValueAccessList =
                valueAccess.ExpandAll();

            foreach (var primitiveValueAccess in primitiveValueAccessList)
            {
                var id = primitiveValueAccess.GetBasisBladeId();

                var scalarPattern = bindingFunction(frameInfo, id);

                if (ignoreNullPatterns == false || scalarPattern != null)
                    BindScalarToPattern(primitiveValueAccess, scalarPattern);
            }

            return this;
        }


        /// <summary>
        /// If the given value access is a multivector this binds a given subspace to variables
        /// </summary>
        /// <param name="valueAccess"></param>
        /// <param name="subspace"></param>
        /// <returns></returns>
        public GMacMacroBinding BindMultivectorPartToVariables(AstDatastoreValueAccess valueAccess, AstFrameSubspace subspace)
        {
            if (valueAccess.IsNullOrInvalid())
                throw new ArgumentNullException(nameof(valueAccess));

            if (subspace.IsNullOrInvalid())
                throw new ArgumentNullException(nameof(subspace));

            return BindToVariables(valueAccess.SelectMultivectorComponents(subspace));
        }

        /// <summary>
        /// If the given value access is a multivector this binds a given subspace to variables
        /// </summary>
        /// <param name="valueAccessName"></param>
        /// <param name="subspace"></param>
        /// <returns></returns>
        public GMacMacroBinding BindMultivectorPartToVariables(string valueAccessName, AstFrameSubspace subspace)
        {
            return BindMultivectorPartToVariables(ToValueAccess(valueAccessName), subspace);
        }

        /// <summary>
        /// If the given value access is a multivector this binds a given subspace to variables
        /// </summary>
        /// <param name="valueAccess"></param>
        /// <param name="idsList"></param>
        /// <returns></returns>
        public GMacMacroBinding BindMultivectorPartToVariables(AstDatastoreValueAccess valueAccess, IEnumerable<int> idsList)
        {
            if (valueAccess.IsNullOrInvalid())
                throw new ArgumentNullException(nameof(valueAccess));

            return BindToVariables(valueAccess.SelectMultivectorComponents(idsList));
        }

        /// <summary>
        /// If the given value access is a multivector this binds a given subspace to variables
        /// </summary>
        /// <param name="valueAccessName"></param>
        /// <param name="idsList"></param>
        /// <returns></returns>
        public GMacMacroBinding BindMultivectorPartToVariables(string valueAccessName, IEnumerable<int> idsList)
        {
            return BindMultivectorPartToVariables(ToValueAccess(valueAccessName), idsList);
        }

        /// <summary>
        /// If the given value access is a multivector this binds a given subspace to variables
        /// </summary>
        /// <param name="valueAccess"></param>
        /// <param name="grade"></param>
        /// <returns></returns>
        public GMacMacroBinding BindMultivectorPartToVariables(AstDatastoreValueAccess valueAccess, int grade)
        {
            if (valueAccess.IsNullOrInvalid())
                throw new ArgumentNullException(nameof(valueAccess));

            return BindToVariables(valueAccess.SelectMultivectorComponents(grade));
        }

        /// <summary>
        /// If the given value access is a multivector this binds a given subspace to variables
        /// </summary>
        /// <param name="valueAccessName"></param>
        /// <param name="grade"></param>
        /// <returns></returns>
        public GMacMacroBinding BindMultivectorPartToVariables(string valueAccessName, int grade)
        {
            return BindMultivectorPartToVariables(ToValueAccess(valueAccessName), grade);
        }

        /// <summary>
        /// If the given value access is a multivector this binds a given subspace to variables
        /// </summary>
        /// <param name="valueAccess"></param>
        /// <param name="grade"></param>
        /// <param name="indexList"></param>
        /// <returns></returns>
        public GMacMacroBinding BindMultivectorPartToVariables(AstDatastoreValueAccess valueAccess, int grade, IEnumerable<int> indexList)
        {
            if (valueAccess.IsNullOrInvalid())
                throw new ArgumentNullException(nameof(valueAccess));

            return BindToVariables(valueAccess.SelectMultivectorComponents(grade, indexList));
        }

        /// <summary>
        /// If the given value access is a multivector this binds a given subspace to variables
        /// </summary>
        /// <param name="valueAccessName"></param>
        /// <param name="grade"></param>
        /// <param name="indexList"></param>
        /// <returns></returns>
        public GMacMacroBinding BindMultivectorPartToVariables(string valueAccessName, int grade, IEnumerable<int> indexList)
        {
            return BindMultivectorPartToVariables(ToValueAccess(valueAccessName), grade, indexList);
        }


        /// <summary>
        /// Bind a macro parameter of any type to a constant of the same type
        /// </summary>
        /// <param name="valueAccessName"></param>
        /// <param name="value"></param>
        public GMacMacroBinding BindToConstants(string valueAccessName, AstValue value)
        {
            var valueAccess = ToValueAccess(valueAccessName, value.AssociatedValue.ExpressionType);

            return BindToConstants(valueAccess, value);
        }

        /// <summary>
        /// Bind a macro parameter of any type to a constant of the same type
        /// </summary>
        /// <param name="valueAccess"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public GMacMacroBinding BindToConstants(AstDatastoreValueAccess valueAccess, AstValue value)
        {
            if (valueAccess.IsNullOrInvalid())
                throw new ArgumentNullException(nameof(valueAccess));

            //This is checked inside each primitive binding to select appropriate action according to the
            // BindOutputToConstantBehavior mamber
            //if (valueAccess.IsInputParameter == false)
            //    throw new InvalidOperationException(
            //        String.Format(
            //            "Specified value access {0} is not a macro input parameter",
            //            valueAccess.ValueAccessName
            //            )
            //        );

            if (valueAccess.AssociatedValueAccess.ExpressionType.IsSameType(value.AssociatedValue.ExpressionType) == false)
                throw new InvalidOperationException(
                    $"Specified macro parameter {valueAccess.ValueAccessName} of type {valueAccess.GMacTypeSignature} is not of same type {value.GMacTypeSignature} as given value"
                    );

            var assignmentsList = valueAccess.AssociatedValueAccess.ExpandAndAssignAll(value.AssociatedValue);

            foreach (var assignment in assignmentsList)
                BindScalarToConstant(
                    assignment.Item1.ToAstDatastoreValueAccess(), 
                    assignment.Item2.ToExpr()
                    );

            return this;
        }


        /// <summary>
        /// Bind a macro parameter of any type to a set of variables
        /// </summary>
        /// <param name="valueAccessName"></param>
        /// <param name="testValueExpr"></param>
        /// <returns></returns>
        public GMacMacroBinding BindToVariables(string valueAccessName, Expr testValueExpr = null)
        {
            return BindToVariables(ToValueAccess(valueAccessName), testValueExpr);
        }

        /// <summary>
        /// Bind a macro parameter of any type to a set of variables
        /// </summary>
        /// <param name="valueAccess"></param>
        /// <param name="testValueExpr"></param>
        /// <returns></returns>
        public GMacMacroBinding BindToVariables(AstDatastoreValueAccess valueAccess, Expr testValueExpr = null)
        {
            if (valueAccess.IsNullOrInvalid())
                throw new ArgumentNullException(nameof(valueAccess));

            if (valueAccess.IsScalar)
            {
                BindScalarToPattern(
                    valueAccess, 
                    GMacScalarBinding.CreateVariable(BaseMacro.Root), 
                    testValueExpr
                    );

                return this;
            }

            var primitiveValueAccessList = valueAccess.ExpandAll();

            foreach (var primitiveValueAccess in primitiveValueAccessList)
                BindScalarToPattern(
                    primitiveValueAccess, 
                    GMacScalarBinding.CreateVariable(BaseMacro.Root),
                    testValueExpr
                    );

            return this;
        }


        /// <summary>
        /// Bind a macro parameter of any type to a pattern of the same type
        /// </summary>
        /// <param name="valueAccessName"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public GMacMacroBinding BindToTreePattern(string valueAccessName, IGMacTypedBinding pattern)
        {
            return BindToTreePattern(ToValueAccess(valueAccessName), pattern);
        }

        /// <summary>
        /// Bind a macro parameter of any type to a pattern of the same type
        /// </summary>
        /// <param name="valueAccess"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public GMacMacroBinding BindToTreePattern(AstDatastoreValueAccess valueAccess, IGMacTypedBinding pattern)
        {
            if (valueAccess.IsNullOrInvalid())
                throw new ArgumentNullException(nameof(valueAccess));

            if (valueAccess.AssociatedValueAccess.ExpressionType.IsSameType(pattern.GMacType.AssociatedType) == false)
                throw new InvalidOperationException(
                    $"Specified macro parameter {valueAccess.ValueAccessName} of type {valueAccess.GMacTypeSignature} is not of same type {pattern.GMacType.GMacTypeSignature} as given pattern"
                    );

            var assignmentsList = valueAccess.AssociatedValueAccess.ExpandAndAssignAll(pattern);

            foreach (var assignment in assignmentsList)
                BindScalarToPattern(
                    assignment.Item1.ToAstDatastoreValueAccess(),
                    assignment.Item2
                    );

            return this;
        }


        ///// <summary>
        ///// Bind a macro parameter of any type using the corresponding expressions inside the
        ///// given value  of the same type
        ///// </summary>
        ///// <param name="valueAccessName"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public GMacMacroBinding BindUsing(string valueAccessName, AstValue value)
        //{
        //    var valueAccess = ToValueAccess(valueAccessName, value.AssociatedValue.ExpressionType);

        //    return BindUsing(valueAccess, value);
        //}

        ///// <summary>
        ///// Bind a macro parameter of any type using the corresponding expressions inside the
        ///// given value  of the same type
        ///// </summary>
        ///// <param name="valueAccess"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public GMacMacroBinding BindUsing(AstDatastoreValueAccess valueAccess, AstValue value)
        //{
        //    if (valueAccess.IsNullOrInvalid())
        //        throw new ArgumentNullException("valueAccess");

        //    if (valueAccess.AssociatedValueAccess.ExpressionType.IsSameType(value.AssociatedValue.ExpressionType) == false)
        //        throw new InvalidOperationException(
        //            String.Format(
        //                "Specified macro parameter {0} of type {1} is not of same type {2} as given value",
        //                valueAccess.ValueAccessName,
        //                valueAccess.GMacTypeSignature,
        //                value.GMacTypeSignature
        //                )
        //            );

        //    var assignmentsList = valueAccess.AssociatedValueAccess.ExpandAndAssignAll(value.AssociatedValue);

        //    foreach (var assignment in assignmentsList)
        //        BindScalarToConstant(
        //            assignment.Item1.ToAstDatastoreValueAccess(), 
        //            assignment.Item2.ToExpr()
        //            );

        //    return this;
        //}

        /// <summary>
        /// Add bindings to this list using the bindings in a tree macro binding pattern
        /// </summary>
        /// <param name="treePattern"></param>
        /// <returns></returns>
        public GMacMacroBinding BindUsing(GMacMacroTreeBinding treePattern)
        {
            if (treePattern.BaseGMacMacro.ObjectId != AssociatedMacro.ObjectId)
                throw new InvalidOperationException("Base macro of tree macro pattern not the same as base macro for list pattern");

            var assignmentsList =
                treePattern
                .Select(
                    pair =>
                        treePattern
                        .BaseGMacMacro
                        .GetParameter(pair.Key)
                        .ExpandAndAssignAll(pair.Value)
                        .ToArray()
                    )
                .SelectMany(assignList => assignList);

            foreach (var assignment in assignmentsList)
                BindScalarToPattern(
                    assignment.Item1.ToAstDatastoreValueAccess(), 
                    assignment.Item2
                    );

            return this;
        }


        /// <summary>
        /// Use the low-level macro code generator and optimizer to generate low-level assignments
        /// </summary>
        /// <returns>The optimized low-level assignments output</returns>
        public GMacCodeBlock CreateOptimizedCodeBlock()
        {
            //Create the low-level generator object
            var gen = new LlGenerator(BaseMacro.AssociatedMacro);

            //Define bindings for low-level primitive macro input and output parameters
            foreach (var binding in Bindings)
                if (binding.IsConstant)
                    gen.DefineParameter(
                        binding.ValueAccess.AssociatedValueAccess,
                        binding.ConstantValue.AssociatedScalarValue
                        );

                else
                    gen.DefineParameter(
                        binding.ValueAccess.AssociatedValueAccess//, binding.TestValueExpr
                        );

            //Generate un-optimized low-level macro computations from the base macro and its parameters bindings
            gen.GenerateLowLevelItems();

            var inputsWithTestValues = 
                Bindings
                .Where(p => p.IsVariable && p.IsInput && p.TestValueExpr != null)
                .ToDictionary(
                    binding => binding.ValueAccessName,
                    binding => binding
                    );

            //Optimize low-level macro computations
            return TcbOptimizer.Process(gen, inputsWithTestValues, FixOutputComputationsOrder, Progress);
        }

        /// <summary>
        /// Compose GMacDSL code that calls the base macro of this binding using its parameters 
        /// binding data
        /// </summary>
        /// <returns></returns>
        public string GenerateMacroBodyCallCode(AstMacroBodyKind macroBodyKind = AstMacroBodyKind.RawBody, bool useTestValues = false)
        {
            var codeComposer = new LinearTextComposer();

            //Declare base macro input parameters
            foreach (var inputParameter in BaseMacro.InputParameters)
                codeComposer
                    .Append("declare ")
                    .Append(inputParameter.Name)
                    .Append(" : ")
                    .AppendLine(inputParameter.GMacTypeSignature);

            //Assign binding values to parameter parts
            var varCounter = 0;
            foreach (var parameterBinding in InputBindings)
            {
                if (parameterBinding.IsConstant)
                {
                    codeComposer
                        .Append("let ")
                        .Append(parameterBinding.ValueAccessName)
                        .Append(" = ")
                        .AppendLine(parameterBinding.ConstantExpr.ToString().DoubleQuote());

                    continue;
                }

                if (useTestValues && parameterBinding.TestValueExpr != null)
                {
                    codeComposer
                        .Append("let ")
                        .Append(parameterBinding.ValueAccessName)
                        .Append(" = ")
                        .AppendLine(parameterBinding.TestValueExpr.ToString().DoubleQuote());

                    continue;
                }

                codeComposer
                    .Append("let ")
                    .Append(parameterBinding.ValueAccessName)
                    .Append(" = 'var")
                    .Append(varCounter.ToString())
                    .AppendLine("'");

                varCounter++;
            }

            //Add macro body code
            codeComposer
                .Append("let finalResult = ");

            if (macroBodyKind == AstMacroBodyKind.RawBody)
            {
                codeComposer.Append(
                    BaseMacro
                    .InputParameters
                    .Select(p => p.Name)
                    .Concatenate(", ", BaseMacro.AccessName + "(", ")")
                    );

                return codeComposer.ToString();
            }

            var astDescription = new GMacAstDescription();

            BaseMacro
                .GetBodyCommandBlock(macroBodyKind)
                .AssociatedCommandBlock
                .AcceptVisitor(astDescription);

            codeComposer.AppendAtNewLine(
                astDescription.Log.ToString()
            );

            return codeComposer.ToString();
        }

        public override string ToString()
        {
            var s = new StringBuilder();

            foreach (var paramBinding in _patternDictionary.Select(pair => pair.Value))
                s.AppendLine(paramBinding.ToString());

            return s.ToString();
        }
    }
}
