using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMac.GMacAST;
using GMac.GMacAST.Expressions;
using GMac.GMacAST.Symbols;
using GMac.GMacCompiler.Semantic.AST;
using GMac.GMacCompiler.Symbolic;
using GMac.GMacUtils;
using SymbolicInterface.Mathematica;
using SymbolicInterface.Mathematica.Expression;
using SymbolicInterface.Mathematica.ExprFactory;
using TextComposerLib.Text.Parametric;
using Wolfram.NETLink;
using FrameUtils = GMac.GMacUtils.EuclideanUtils;

namespace GMac.GMacAPI.Binding
{
    /// <summary>
    /// This class represents an abstract binding pattern of a composite sub-component of some 
    /// GMac data-store to a multivector of the same multivector type
    /// </summary>
    public sealed class GMacMultivectorBinding : IGMacCompositeBinding, IGMacTypedBinding, IEnumerable<KeyValuePair<int, GMacScalarBinding>>
    {
        /// <summary>
        /// Create an empty multivector pattern
        /// </summary>
        /// <param name="patternType"></param>
        /// <returns></returns>
        public static GMacMultivectorBinding Create(AstFrameMultivector patternType)
        {
            return new GMacMultivectorBinding(patternType);
        }

        /// <summary>
        /// Create a multivector pattern from the given multivector value using only constant components
        /// from the multivector
        /// </summary>
        /// <param name="mv"></param>
        /// <param name="ignoreZeroCoefs"></param>
        /// <returns></returns>
        public static GMacMultivectorBinding CreateFromConstants(AstValueMultivector mv, bool ignoreZeroCoefs = true)
        {
            var mvPattern = new GMacMultivectorBinding(mv.FrameMultivector);

            foreach (var term in mv.AssociatedMultivectorValue.MultivectorCoefficients)
            {
                if (ignoreZeroCoefs && term.Value.IsZero())
                    continue;

                if (term.Value.IsConstant())
                    mvPattern.BindCoefToConstant(term.Key, term.Value.MathExpr);
            }

            return mvPattern;
        }

        /// <summary>
        /// Create a multivector pattern from the given multivector value using only non-constant components
        /// from the multivector
        /// </summary>
        /// <param name="mv"></param>
        /// <returns></returns>
        public static GMacMultivectorBinding CreateFromNonConstants(AstValueMultivector mv)
        {
            var mvPattern = new GMacMultivectorBinding(mv.FrameMultivector);

            foreach (var term in mv.AssociatedMultivectorValue.MultivectorCoefficients)
                if (term.Value.IsConstant() == false)
                    mvPattern.BindCoefToVariable(term.Key);

            return mvPattern;
        }

        /// <summary>
        /// Create a multivector pattern from the given multivector value using only constant components
        /// from the multivector. All the created bindings are variable bindings
        /// </summary>
        /// <param name="mv"></param>
        /// <param name="ignoreZeroCoefs"></param>
        /// <returns></returns>
        public static GMacMultivectorBinding CreateConstantsFreeFromConstants(AstValueMultivector mv, bool ignoreZeroCoefs = true)
        {
            var mvPattern = new GMacMultivectorBinding(mv.FrameMultivector);

            foreach (var term in mv.AssociatedMultivectorValue.MultivectorCoefficients)
            {
                if (ignoreZeroCoefs && term.Value.IsZero())
                    continue;

                if (term.Value.IsConstant())
                    mvPattern.BindCoefToVariable(term.Key);
            }

            return mvPattern;
        }

        /// <summary>
        /// Create a multivector pattern having no constant components from the given multivector value
        /// </summary>
        /// <param name="mv"></param>
        /// <param name="ignoreZeroCoefs"></param>
        /// <returns></returns>
        public static GMacMultivectorBinding CreateConstantsFree(AstValueMultivector mv, bool ignoreZeroCoefs = true)
        {
            var mvPattern = new GMacMultivectorBinding(mv.FrameMultivector);

            foreach (var term in mv.AssociatedMultivectorValue.MultivectorCoefficients)
            {
                if (ignoreZeroCoefs && term.Value.IsZero())
                    continue;

                mvPattern.BindCoefToVariable(term.Key);
            }

            return mvPattern;
        }

        /// <summary>
        /// Create a multivector pattern binding all IDs in the given subspace to variables
        /// </summary>
        /// <param name="subspace"></param>
        /// <returns></returns>
        public static GMacMultivectorBinding Create(AstFrameSubspace subspace)
        {
            var mvPattern = new GMacMultivectorBinding(subspace.FrameMultivector);

            foreach (var id in subspace.AssociatedSubspace.SubspaceSignaturePattern.TrueIndexes)
                mvPattern.BindCoefToVariable(id);

            return mvPattern;
        }

        /// <summary>
        /// Create a multivector pattern from the given multivector value
        /// </summary>
        /// <param name="mv"></param>
        /// <param name="ignoreZeroCoefs"></param>
        /// <returns></returns>
        public static GMacMultivectorBinding Create(AstValueMultivector mv, bool ignoreZeroCoefs = true)
        {
            var mvPattern = new GMacMultivectorBinding(mv.FrameMultivector);

            foreach (var term in mv.AssociatedMultivectorValue.MultivectorCoefficients)
            {
                if (ignoreZeroCoefs && term.Value.IsZero())
                    continue;

                if (term.Value.IsConstant())
                    mvPattern.BindCoefToConstant(term.Key, term.Value.MathExpr);

                else
                    mvPattern.BindCoefToVariable(term.Key);
            }

            return mvPattern;
        }


        private readonly Dictionary<int, GMacScalarBinding> _patternDictionary =
            new Dictionary<int, GMacScalarBinding>();


        /// <summary>
        /// The GMac type of this pattern
        /// </summary>
        public AstType GMacType => BaseFrameMultivector.GMacType;

        /// <summary>
        /// The GMac multivector type of this pattern
        /// </summary>
        public AstFrameMultivector BaseFrameMultivector { get; }

        /// <summary>
        /// The frame of the multivector type of this pattern 
        /// </summary>
        public AstFrame BaseFrame => BaseFrameMultivector.ParentFrame;

        /// <summary>
        /// The GMac scalar type of the components of this multivector type
        /// </summary>
        public AstType ScalarType => new AstType(
            BaseFrameMultivector.AssociatedFrameMultivector.GMacRootAst.ScalarType
            );

        /// <summary>
        /// The basis blade IDs used in this binding pattern
        /// </summary>
        public IEnumerable<int> BoundIDs
        {
            get { return _patternDictionary.Select(pair => pair.Key); }
        }

        /// <summary>
        /// The basis blade IDs used in constant scalar bindings in this binding pattern
        /// </summary>
        public IEnumerable<int> BoundConstantIDs
        {
            get
            {
                return 
                    _patternDictionary
                    .Where(pair => pair.Value.IsConstant)
                    .Select(pair => pair.Key);
            }
        }

        /// <summary>
        /// The basis blade IDs used in variable and non-zero constant scalar bindings in this binding pattern
        /// </summary>
        public IEnumerable<int> BoundNonZeroIDs
        {
            get
            {
                return
                    _patternDictionary
                    .Where(pair => pair.Value.IsNonZero)
                    .Select(pair => pair.Key);
            }
        }

        /// <summary>
        /// The basis blade IDs used in zero constant scalar bindings in this binding pattern
        /// </summary>
        public IEnumerable<int> BoundZeroConstantIDs
        {
            get
            {
                return
                    _patternDictionary
                    .Where(pair => pair.Value.IsZeroConstant)
                    .Select(pair => pair.Key);
            }
        }

        /// <summary>
        /// The basis blade IDs used in non-zero constant scalar bindings in this binding pattern
        /// </summary>
        public IEnumerable<int> BoundNonZeroConstantIDs
        {
            get
            {
                return
                    _patternDictionary
                    .Where(pair => pair.Value.IsNonZeroConstant)
                    .Select(pair => pair.Key);
            }
        }

        /// <summary>
        /// The basis blade IDs used in variable scalar bindings in this binding pattern
        /// </summary>
        public IEnumerable<int> BoundVariableIDs
        {
            get
            {
                return
                    _patternDictionary
                    .Where(pair => pair.Value.IsVariable)
                    .Select(pair => pair.Key);
            }
        }

        /// <summary>
        /// The basis blade IDs not used in scalar bindings in this binding pattern
        /// </summary>
        public IEnumerable<int> NotBoundIDs
        {
            get
            {
                return
                    Enumerable
                    .Range(0, BaseFrameMultivector.AssociatedFrameMultivector.ParentFrame.MaxBasisBladeId)
                    .Where(id => _patternDictionary.ContainsKey(id) == false);
            }
        }

        /// <summary>
        /// The multivector grades having some scalar binding in this binding pattern
        /// </summary>
        public IEnumerable<int> BoundGrades => BoundIDs.Select(GMacUtils.FrameUtils.BasisBladeGrade).Distinct();

        /// <summary>
        /// The multivector grades not having any scalar bindings in this binding pattern
        /// </summary>
        public IEnumerable<int> NotBoundGrades
        {
            get
            {
                return
                    Enumerable
                    .Range(0, BaseFrameMultivector.AssociatedFrameMultivector.ParentFrame.VSpaceDimension)
                    .Where(grade => HasBoundCoefOfGrade(grade) == false);
            }
        }

        /// <summary>
        /// The number of basis blades with variable bindings in this pattern
        /// </summary>
        public int VariablesCount => BoundVariableIDs.Count();

        /// <summary>
        /// The number of basis blades with constant bindings in this pattern
        /// </summary>
        public int ConstantsCount => BoundConstantIDs.Count();

        /// <summary>
        /// True if this pattern contains any variable bindings
        /// </summary>
        public bool ContainsVariables => BoundVariableIDs.Any();

        /// <summary>
        /// True if this pattern contains any constant bindings
        /// </summary>
        public bool ContainsConstants => BoundConstantIDs.Any();

        /// <summary>
        /// True if this pattern contains any constant bindings
        /// </summary>
        public bool HasConstantComponent => ContainsConstants;

        /// <summary>
        /// True if this pattern contains any variable bindings
        /// </summary>
        public bool HasVariableComponent => ContainsVariables;

        /// <summary>
        /// The constant bindings in this pattern
        /// </summary>
        public Dictionary<int, Expr> Constants
        {
            get
            {
                return 
                    _patternDictionary
                    .Where(pair => pair.Value.IsConstant)
                    .ToDictionary(pair => pair.Key, pair => pair.Value.ConstantExpr);
            }
        }

        /// <summary>
        /// Gets the scalar binding pattern associated with the given basis blade
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public GMacScalarBinding this[int id] => _patternDictionary[id];

        /// <summary>
        /// Gets the scalar binding pattern associated with the given basis blade
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public GMacScalarBinding this[int grade, int index] => _patternDictionary[GMacUtils.FrameUtils.BasisBladeId(grade, index)];


        private GMacMultivectorBinding(AstFrameMultivector patternType)
        {
            BaseFrameMultivector = patternType;
        }


        /// <summary>
        /// Clear all scalar bindings in this pattern
        /// </summary>
        public void Clear()
        {
            _patternDictionary.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool HasBoundCoef(int id)
        {
            return _patternDictionary.ContainsKey(id);
        }

        public bool HasBoundCoef(int grade, int index)
        {
            return _patternDictionary.ContainsKey(GMacUtils.FrameUtils.BasisBladeId(grade, index));
        }

        public bool HasBoundCoefOfGrade(int grade)
        {
            return BoundIDs.Any(id => id.BasisBladeGrade() == grade);
        }


        public bool ContainsVariable(int id)
        {
            GMacScalarBinding pattern;

            return _patternDictionary.TryGetValue(id, out pattern) && pattern.IsVariable;
        }

        public bool ContainsConstant(int id)
        {
            GMacScalarBinding pattern;

            return _patternDictionary.TryGetValue(id, out pattern) && pattern.IsConstant;
        }

        public bool ContainsConstant(int id, Expr value)
        {
            GMacScalarBinding pattern;

            return 
                _patternDictionary.TryGetValue(id, out pattern) && 
                pattern.IsConstant &&
                SymbolicUtils.Cas.EvalTrueQ(Mfs.Equal[pattern.ConstantExpr, value]);
        }

        public bool ContainsScalarPattern(int id, GMacScalarBinding scalarPattern)
        {
            return 
                scalarPattern.IsVariable 
                ? ContainsVariable(id) 
                : ContainsConstant(id, scalarPattern.ConstantExpr);
        }

        /// <summary>
        /// True if the given multivector pattern matches this pattern in all its variables and constants
        /// </summary>
        /// <param name="mvPattern"></param>
        /// <returns></returns>
        public bool MatchesPattern(GMacMultivectorBinding mvPattern)
        {
            if (_patternDictionary.Count != mvPattern._patternDictionary.Count)
                return false;

            return 
                _patternDictionary
                .All(
                    pair => mvPattern.ContainsScalarPattern(pair.Key, pair.Value)
                    );
        }

        /// <summary>
        /// True if the given RHS multivector pattern can be stored in this pattern without loss of information
        /// </summary>
        /// <param name="rhsMvPattern"></param>
        /// <returns></returns>
        public bool CanStoreRhs(GMacMultivectorBinding rhsMvPattern)
        {
            return 
                rhsMvPattern
                .All(
                    pair => 
                        ContainsVariable(pair.Key) || 
                        (pair.Value.IsConstant && ContainsConstant(pair.Key, pair.Value.ConstantExpr))
                    );
        }

        /// <summary>
        /// True if the given RHS multivector value can be stored in this pattern without loss of information
        /// </summary>
        /// <param name="mv"></param>
        /// <returns></returns>
        public bool CanStoreRhs(AstValueMultivector mv)
        {
            return CanStoreRhs(Create(mv));
        }

        
        public GMacMultivectorBinding UnBindCoef(int id)
        {
            _patternDictionary.Remove(id);

            return this;
        }

        public GMacMultivectorBinding UnBindCoef(int grade, int index)
        {
            _patternDictionary.Remove(GMacUtils.FrameUtils.BasisBladeId(grade, index));

            return this;
        }

        public GMacMultivectorBinding UnBindCoefs(IEnumerable<int> ids)
        {
            foreach (var id in ids)
                _patternDictionary.Remove(id);

            return this;
        }

        public GMacMultivectorBinding UnBindKVector(int grade)
        {
            UnBindCoefs(GMacUtils.FrameUtils.BasisBladeIDsOfGrade(BaseFrame.VSpaceDimension, grade));

            return this;
        }

        public GMacMultivectorBinding UnBindCoefs(int grade, IEnumerable<int> indexes)
        {
            foreach (var index in indexes)
                _patternDictionary.Remove(GMacUtils.FrameUtils.BasisBladeId(grade, index));

            return this;
        }


        /// <summary>
        /// Bind the given scalar pattern with the id. 
        /// If the given pattern is null this function un-binds the given id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public GMacMultivectorBinding BindCoefToPattern(int id, GMacScalarBinding pattern)
        {
            if (BaseFrameMultivector.AssociatedFrameMultivector.ParentFrame.IsValidBasisBladeId(id) == false)
                throw new InvalidOperationException("Basis Blade ID not accepted");

            if (pattern == null)
                _patternDictionary.Remove(id);

            else if (_patternDictionary.ContainsKey(id))
                _patternDictionary[id] = pattern;

            else
                _patternDictionary.Add(id, pattern);

            return this;
        }


        public GMacMultivectorBinding BindCoefToConstant(int id, Expr valueExpr)
        {
            return BindCoefToPattern(
                id, 
                GMacScalarBinding.CreateConstant(BaseFrameMultivector.Root, valueExpr)
                );
        }

        public GMacMultivectorBinding BindCoefToVariable(int id)
        {
            return BindCoefToPattern(
                id,
                GMacScalarBinding.CreateVariable(BaseFrameMultivector.Root)
                );
        }

        public GMacMultivectorBinding BindCoefToConstant(int grade, int index, Expr valueExpr)
        {
            return BindCoefToPattern(
                GMacUtils.FrameUtils.BasisBladeId(grade, index),
                GMacScalarBinding.CreateConstant(BaseFrameMultivector.Root, valueExpr)
                );
        }

        public GMacMultivectorBinding BindCoefToVariable(int grade, int index)
        {
            return BindCoefToPattern(
                GMacUtils.FrameUtils.BasisBladeId(grade, index),
                GMacScalarBinding.CreateVariable(BaseFrameMultivector.Root)
                );
        }

        public GMacMultivectorBinding BindBasisVectorCoefToConstant(int index, Expr valueExpr)
        {
            return BindCoefToPattern(
                GMacUtils.FrameUtils.BasisVectorId(index),
                GMacScalarBinding.CreateConstant(BaseFrameMultivector.Root, valueExpr)
                );
        }

        public GMacMultivectorBinding BindBasisVectorCoefToVariable(int index)
        {
            return BindCoefToPattern(
                GMacUtils.FrameUtils.BasisVectorId(index),
                GMacScalarBinding.CreateVariable(BaseFrameMultivector.Root)
                );
        }


        public GMacMultivectorBinding BindCoefsUsingPattern(GMacMultivectorBinding pattern)
        {
            foreach (var pair in pattern._patternDictionary)
                BindCoefToPattern(pair.Key, pair.Value);

            return this;
        }

        public GMacMultivectorBinding BindCoefsToVariables(IEnumerable<int> ids)
        {
            foreach (var id in ids)
                BindCoefToVariable(id);

            return this;
        }

        public GMacMultivectorBinding BindCoefsToVariables(params int[] ids)
        {
            foreach (var id in ids)
                BindCoefToVariable(id);

            return this;
        }

        public GMacMultivectorBinding BindCoefsToVariables(int grade, IEnumerable<int> indexes)
        {
            foreach (var index in indexes)
                BindCoefToVariable(grade, index);

            return this;
        }


        public GMacMultivectorBinding BindKVectorToVariables(int grade)
        {
            if (BaseFrameMultivector.AssociatedFrameMultivector.ParentFrame.IsValidGrade(grade) == false)
                throw new InvalidOperationException("Grade not accepted");

            var kvSpaceDim = 
                GMacUtils.FrameUtils.KvSpaceDimension(BaseFrameMultivector.AssociatedFrameMultivector.ParentFrame.VSpaceDimension, grade);

            for (var index = 0; index < kvSpaceDim; index++)
                BindCoefToVariable(grade, index);

            return this;
        }


        public GMacMultivectorBinding BindScalarToVariable()
        {
            return BindCoefToVariable(0);
        }

        public GMacMultivectorBinding BindScalarToConstant(Expr valueExpr)
        {
            return BindCoefToConstant(0, valueExpr);
        }

        public GMacMultivectorBinding BindPseudoScalarToVariable()
        {
            return BindCoefToVariable(BaseFrameMultivector.AssociatedFrameMultivector.ParentFrame.MaxBasisBladeId);
        }

        public GMacMultivectorBinding BindPseudoScalarToConstant(Expr valueExpr)
        {
            return BindCoefToConstant(BaseFrameMultivector.AssociatedFrameMultivector.ParentFrame.MaxBasisBladeId, valueExpr);
        }


        public GMacMultivectorBinding BindToVariables()
        {
            BindKVectorsRangeToVariables(0, BaseFrameMultivector.ParentFrame.VSpaceDimension);

            return this;
        }

        public GMacMultivectorBinding BindKVectorsToVariables(IEnumerable<int> grades)
        {
            foreach (var grade in grades)
                BindKVectorToVariables(grade);

            return this;
        }

        public GMacMultivectorBinding BindKVectorsToVariables(params int[] grades)
        {
            foreach (var grade in grades)
                BindKVectorToVariables(grade);

            return this;
        }

        public GMacMultivectorBinding BindKVectorsRangeToVariables(int maxGrade)
        {
            BindKVectorsRangeToVariables(0, maxGrade);

            return this;
        }

        public GMacMultivectorBinding BindKVectorsRangeToVariables(int minGrade, int maxGrade)
        {
            for (var grade = minGrade; grade <= maxGrade; grade++)
                BindKVectorToVariables(grade);

            return this;
        }

        public GMacMultivectorBinding BindEvenKVectorsToVariables()
        {
            BindEvenKVectorsToVariables(0, BaseFrameMultivector.AssociatedFrameMultivector.ParentFrame.VSpaceDimension);

            return this;
        }

        public GMacMultivectorBinding BindEvenKVectorsToVariables(int maxGrade)
        {
            BindEvenKVectorsToVariables(0, maxGrade);

            return this;
        }

        public GMacMultivectorBinding BindEvenKVectorsToVariables(int minGrade, int maxGrade)
        {
            for (var grade = minGrade; grade <= maxGrade; grade++)
                if (grade % 2 == 0)
                    BindKVectorToVariables(grade);

            return this;
        }

        public GMacMultivectorBinding BindOddKVectorsToVariables()
        {
            BindOddKVectorsToVariables(1, BaseFrameMultivector.AssociatedFrameMultivector.ParentFrame.VSpaceDimension);

            return this;
        }

        public GMacMultivectorBinding BindOddKVectorsToVariables(int maxGrade)
        {
            BindOddKVectorsToVariables(1, maxGrade);

            return this;
        }

        public GMacMultivectorBinding BindOddKVectorsToVariables(int minGrade, int maxGrade)
        {
            for (var grade = minGrade; grade <= maxGrade; grade++)
                if (grade % 2 == 1)
                    BindKVectorToVariables(grade);

            return this;
        }


        /// <summary>
        /// This method iterates through the coefficeints of the given value. If the coefficient is
        /// a numeric value it binds the corresponding id with the constant. If the coefficeint is not a
        /// numeric value it binds the id with a variable
        /// </summary>
        /// <param name="mvValue"></param>
        /// <returns></returns>
        public GMacMultivectorBinding BindUsing(AstValueMultivector mvValue)
        {
            foreach (var term in mvValue.AssociatedMultivectorValue.MultivectorCoefficients)
            {
                var expr = term.Value.MathExpr;

                if (SymbolicUtils.Cas[Mfs.NumericQ[expr]].TrueQ())
                    BindCoefToConstant(term.Key, term.Value.MathExpr);

                else
                    BindCoefToVariable(term.Key);
            }

            return this;
        }

        /// <summary>
        /// This function iterates over all possible IDs and calls the binding function to get a scalar
        /// pattern. If the function return null on a pattern and ignoreNullPatterns is true no binding
        /// is done for this ID. If the function return null on a pattern and ignoreNullPatterns is false
        /// the ID is un-bound from this multivector pattern
        /// </summary>
        /// <param name="bindingFunction"></param>
        /// <param name="ignoreNullPatterns"></param>
        /// <returns></returns>
        public GMacMultivectorBinding BindUsing(Func<int, GMacScalarBinding> bindingFunction, bool ignoreNullPatterns = true)
        {
            for (var id = 0; id < BaseFrame.GaSpaceDimension; id++)
            {
                var scalarPattern = bindingFunction(id);

                if (ignoreNullPatterns == false || scalarPattern != null)
                    BindCoefToPattern(id, scalarPattern);
            }

            return this;
        }

        /// <summary>
        /// This function iterates over all possible IDs and calls the binding function to get a scalar
        /// pattern. If the function return null on a pattern and ignoreNullPatterns is true no binding
        /// is done for this ID. If the function return null on a pattern and ignoreNullPatterns is false
        /// the ID is un-bound from this multivector pattern
        /// </summary>
        /// <param name="bindingFunction"></param>
        /// <param name="ignoreNullPatterns"></param>
        /// <returns></returns>
        public GMacMultivectorBinding BindUsing(Func<AstFrame, int, GMacScalarBinding> bindingFunction, bool ignoreNullPatterns = true)
        {
            for (var id = 0; id < BaseFrame.GaSpaceDimension; id++)
            {
                var scalarPattern = bindingFunction(BaseFrame, id);

                if (ignoreNullPatterns == false || scalarPattern != null)
                    BindCoefToPattern(id, scalarPattern);
            }

            return this;
        }

        /// <summary>
        /// This function iterates over all possible IDs and calls the binding function to get a scalar
        /// pattern. If the function return null on a pattern and ignoreNullPatterns is true no binding
        /// is done for this ID. If the function return null on a pattern and ignoreNullPatterns is false
        /// the ID is un-bound from this multivector pattern
        /// </summary>
        /// <param name="bindingFunction"></param>
        /// <param name="ignoreNullPatterns"></param>
        /// <returns></returns>
        public GMacMultivectorBinding BindUsing(Func<AstFrameBasisBlade, GMacScalarBinding> bindingFunction, bool ignoreNullPatterns = true)
        {
            for (var id = 0; id < BaseFrame.GaSpaceDimension; id++)
            {
                var scalarPattern = bindingFunction(BaseFrame.BasisBlade(id));

                if (ignoreNullPatterns == false || scalarPattern != null)
                    BindCoefToPattern(id, scalarPattern);
            }

            return this;
        }


        public override string ToString()
        {
            var s = new StringBuilder();

            s.Append(BaseFrameMultivector.AccessName)
                .Append("(");

            if (_patternDictionary.Count > 0)
            {
                foreach (var pair in _patternDictionary)
                    s.Append("#")
                        .Append(BaseFrame.AssociatedFrame.BasisBladeName(pair.Key))
                        .Append("# = ")
                        .Append(pair.Value)
                        .Append(", ");

                s.Length -= 2;
            }

            s.Append(")");

            return s.ToString();
        }

        public IEnumerator<KeyValuePair<int, GMacScalarBinding>> GetEnumerator()
        {
            return _patternDictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _patternDictionary.GetEnumerator();
        }


        public IGMacBinding ToConstantsFreePattern()
        {
            var mvPattern = new GMacMultivectorBinding(BaseFrameMultivector);

            foreach (var pair in _patternDictionary)
                mvPattern
                    ._patternDictionary
                    .Add(
                        pair.Key, 
                        (GMacScalarBinding)pair.Value.ToConstantsFreePattern()
                        );

            return mvPattern;
        }

        public IGMacCompositeBinding PickConstantComponents()
        {
            var mvPattern = new GMacMultivectorBinding(BaseFrameMultivector);

            foreach (var pair in _patternDictionary.Where(pair => pair.Value.IsConstant))
                mvPattern._patternDictionary.Add(pair.Key, pair.Value);

            return mvPattern;
        }

        public IGMacCompositeBinding PickVariableComponents()
        {
            var mvPattern = new GMacMultivectorBinding(BaseFrameMultivector);

            foreach (var pair in _patternDictionary.Where(pair => pair.Value.IsVariable))
                mvPattern._patternDictionary.Add(pair.Key, pair.Value);

            return mvPattern;
        }

        public IGMacCompositeBinding PickConstantComponentsAsVariables()
        {
            var mvPattern = new GMacMultivectorBinding(BaseFrameMultivector);

            foreach (var pair in _patternDictionary.Where(pair => pair.Value.IsConstant))
                mvPattern._patternDictionary.Add(
                    pair.Key, 
                    GMacScalarBinding.CreateVariable(BaseFrameMultivector.Root)
                    );

            return mvPattern;
        }

        /// <summary>
        /// Convert this binding pattern into a multivector value
        /// </summary>
        /// <param name="varNameTemplate"></param>
        /// <returns></returns>
        public AstValueMultivector ToValue(StringSequenceTemplate varNameTemplate)
        {
            var mv = GaMultivector.CreateZero(BaseFrameMultivector.AssociatedFrameMultivector.ParentFrame.GaSpaceDimension);

            foreach (var pair in _patternDictionary)
                mv[pair.Key] = pair.Value.ToMathematicaScalar(varNameTemplate);

            return new AstValueMultivector(
                GMacValueMultivector.Create(
                    BaseFrameMultivector.AssociatedFrameMultivector,
                    mv
                    )
                );
        }

        /// <summary>
        /// Convert this binding pattern into a multivector value
        /// </summary>
        /// <param name="basisBladeToVarName"></param>
        /// <returns></returns>
        public AstValueMultivector ToValue(Func<int, string> basisBladeToVarName)
        {
            //var frameInfo = new AstFrame(MultivectorType.AssociatedFrameMultivector.ParentFrame);

            var mv = GaMultivector.CreateZero(BaseFrameMultivector.AssociatedFrameMultivector.ParentFrame.GaSpaceDimension);

            foreach (var pair in _patternDictionary)
                mv[pair.Key] =
                    pair.Value.IsConstant
                    ? pair.Value.ConstantSymbolicScalar
                    : MathematicaScalar.Create(
                        SymbolicUtils.Cas, basisBladeToVarName(pair.Key)
                        );

            return
                new AstValueMultivector(
                    GMacValueMultivector.Create(
                        BaseFrameMultivector.AssociatedFrameMultivector,
                        mv
                        )
                    );
        }

        /// <summary>
        /// Convert this binding pattern into a multivector value
        /// </summary>
        /// <param name="basisBladeToVarName"></param>
        /// <returns></returns>
        public AstValueMultivector ToValue(Func<AstFrame, int, string> basisBladeToVarName)
        {
            var frameInfo = new AstFrame(BaseFrameMultivector.AssociatedFrameMultivector.ParentFrame);

            var mv = GaMultivector.CreateZero(BaseFrameMultivector.AssociatedFrameMultivector.ParentFrame.GaSpaceDimension);

            foreach (var pair in _patternDictionary)
                mv[pair.Key] =
                    pair.Value.IsConstant
                    ? pair.Value.ConstantSymbolicScalar
                    : MathematicaScalar.Create(
                        SymbolicUtils.Cas, basisBladeToVarName(frameInfo, pair.Key)
                        );

            return
                new AstValueMultivector(
                    GMacValueMultivector.Create(
                        BaseFrameMultivector.AssociatedFrameMultivector,
                        mv
                        )
                    );
        }

        /// <summary>
        /// Convert this binding pattern into a multivector value
        /// </summary>
        /// <param name="basisBladeToVarName"></param>
        /// <returns></returns>
        public AstValueMultivector ToValue(Func<AstFrameBasisBlade, string> basisBladeToVarName)
        {
            var frameInfo = new AstFrame(BaseFrameMultivector.AssociatedFrameMultivector.ParentFrame);

            var mv = GaMultivector.CreateZero(BaseFrameMultivector.AssociatedFrameMultivector.ParentFrame.GaSpaceDimension);

            foreach (var pair in _patternDictionary)
                mv[pair.Key] = 
                    pair.Value.IsConstant 
                    ? pair.Value.ConstantSymbolicScalar
                    : MathematicaScalar.Create(
                        SymbolicUtils.Cas, basisBladeToVarName(frameInfo.BasisBlade(pair.Key))
                        );

            return 
                new AstValueMultivector(
                    GMacValueMultivector.Create(
                        BaseFrameMultivector.AssociatedFrameMultivector,
                        mv
                        )
                    );
        }
    }
}
