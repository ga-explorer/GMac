using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeComposerLib.Irony.Semantic.Expression.Value;
using CodeComposerLib.Irony.Semantic.Expression.ValueAccess;
using CodeComposerLib.Irony.Semantic.Symbol;
using CodeComposerLib.Irony.Semantic.Type;
using GMac.GMacAPI.Binding;

namespace GMac.GMacCompiler.Semantic.AST.Extensions
{
    internal static class LanguageValueAccessExtensions
    {
        public static bool IsMultivector(this LanguageValueAccess valueAccess)
        {
            return valueAccess.ExpressionType is GMacFrameMultivector; 
        }

        public static bool IsFullMultivector(this LanguageValueAccess valueAccess) 
        {
            return 
                valueAccess.ExpressionType is GMacFrameMultivector && 
                !(valueAccess.LastAccessStep is ValueAccessStepByKeyList<int>);
        }

        public static bool HasBaseMultivector(this LanguageValueAccess valueAccess)
        {
            return
                valueAccess.IsPartialAccess &&
                valueAccess.NextToLastAccessStep.AccessStepType is GMacFrameMultivector &&
                (
                    valueAccess.LastAccessStep is ValueAccessStepByKeyList<int> ||
                    valueAccess.LastAccessStep is ValueAccessStepByKey<int>
                );
        }

        public static bool IsPartialMultivector(this LanguageValueAccess valueAccess) 
        {
            return
                valueAccess.IsPartialAccess &&
                valueAccess.NextToLastAccessStep.AccessStepType is GMacFrameMultivector &&
                (valueAccess.LastAccessStep is ValueAccessStepByKeyList<int>);
        }

        public static bool IsMultivectorCoefficient(this LanguageValueAccess valueAccess)
        {
            return
                valueAccess.IsPartialAccess &&
                valueAccess.NextToLastAccessStep.AccessStepType is GMacFrameMultivector &&
                (valueAccess.LastAccessStep is ValueAccessStepByKey<int>);
        }

        public static bool IsStructure(this LanguageValueAccess valueAccess) 
        {
            return valueAccess.ExpressionType is GMacStructure; 
        }

        public static string GetName(this LanguageValueAccess valueAccess)
        {
            var s = new StringBuilder();

            ValueAccessStep parentStep = null;

            foreach (var accessStep in valueAccess.AccessSteps)
            {
                if (!accessStep.IsFirstComponent)
                    s.Append(".");

                s.Append(GetName(accessStep, parentStep));

                parentStep = accessStep;
            }

            return s.ToString();
        }

        public static string GetName(this ValueAccessStep accessStep, ValueAccessStep parentStep)
        {
            var stepAsRoot = accessStep as ValueAccessStepAsRootSymbol;

            if (stepAsRoot != null)
                return stepAsRoot.AccessSymbol.SymbolAccessName;

            //if (access_step is ValueAccessStepBySymbol)
            //{
            //    return ((ValueAccessStepBySymbol)access_step).ComponentSymbol.SymbolAccessName;
            //}

            var stepByKey = accessStep as ValueAccessStepByKey<int>;

            if (stepByKey != null)
            {
                var c = stepByKey;

                var frame = ((GMacFrameMultivector)parentStep.AccessStepType).ParentFrame;

                return "#" + frame.BasisBladeName(c.AccessKey) + "#";
            }

            var stepByKeyList = accessStep as ValueAccessStepByKeyList<int>;

            if (stepByKeyList != null)
            {
                var c = stepByKeyList;

                var frame = ((GMacFrameMultivector)parentStep.AccessStepType).ParentFrame;

                var s = new StringBuilder();

                foreach (var id in c.AccessKeyList)
                    s.Append(frame.BasisBladeName(id)).Append(", ");

                s.Length = s.Length - 2;

                return "@" + s + "@";
            }

            var accessStepByKey = accessStep as ValueAccessStepByKey<string>;

            return 
                accessStepByKey != null 
                ? accessStepByKey.AccessKey 
                : "<unknown component>";
        }

        /// <summary>
        /// If this value access is a multivector coefficient partial access this returns the basis
        /// blade id of the coefficient else it returns -1
        /// </summary>
        /// <param name="valueAccess"></param>
        /// <returns></returns>
        public static int GetBasisBladeId(this LanguageValueAccess valueAccess)
        {
            return 
                IsMultivectorCoefficient(valueAccess)
                ? ((ValueAccessStepByKey<int>) valueAccess.LastAccessStep).AccessKey
                : -1;
        }

        /// <summary>
        /// If this value access is a partial mulltivector this returns the list of basis blade id's
        /// else it returns null
        /// </summary>
        /// <param name="valueAccess"></param>
        /// <returns></returns>
        public static List<int> GetBasisBladeIdsList(this LanguageValueAccess valueAccess)
        {
            return 
                IsPartialMultivector(valueAccess)
                ? ((ValueAccessStepByKeyList<int>)valueAccess.LastAccessStep).AccessKeyList : 
                null;
        }

        /// <summary>
        /// If this value access is a multivector coefficient or a partial mulltivector this returns the
        /// base multivector value access else returns null
        /// </summary>
        /// <param name="valueAccess"></param>
        /// <returns></returns>
        public static LanguageValueAccess GetBaseMultivector(this LanguageValueAccess valueAccess)
        {
            return
                HasBaseMultivector(valueAccess)
                ? valueAccess.DuplicateExceptLast()
                : null;
        }


        /// <summary>
        /// Iterate through all access steps until
        /// </summary>
        /// <param name="valueAccess"></param>
        /// <returns></returns>
        public static LanguageValueAccess ReduceMultivectorAccess(this LanguageValueAccess valueAccess)
        {
            //TODO: Complete this

            //Iterate through all access steps until a full multivector is found
            //Any subsequent access steps by keys list are intersected together into a single 
            //access by keys list step followed by any other access steps if any.

            return valueAccess;
        }

        /// <summary>
        /// Convert a value access with a root SymbolDataStore into a list of value access objects having 
        /// primitive types
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public static IEnumerable<LanguageValueAccess> ExpandAll(this SymbolDataStore symbol)
        {
            return ExpandAll(LanguageValueAccess.Create(symbol));
        }

        /// <summary>
        /// Convert a value access with a root SymbolDataStore into a list of value access objects having 
        /// primitive types
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<LanguageValueAccess> ExpandAll(this LanguageValueAccess valueAccess)
        {
            valueAccess = ReduceMultivectorAccess(valueAccess);

            var stack = new Stack<LanguageValueAccess>();
            stack.Push(valueAccess);

            while (stack.Count > 0)
            {
                var rootValueAccess = stack.Pop();

                if (rootValueAccess.ExpressionType is TypePrimitive)
                {
                    yield return rootValueAccess;
                    continue;
                }

                var mvType = rootValueAccess.ExpressionType as GMacFrameMultivector;

                if (mvType != null)
                {
                    var scalarType = ((GMacAst)valueAccess.RootAst).ScalarType;

                    var partialMvFlag = (rootValueAccess.LastAccessStep is ValueAccessStepByKeyList<int>);

                    var idList =
                        partialMvFlag
                            ? ((ValueAccessStepByKeyList<int>)rootValueAccess.LastAccessStep).AccessKeyList
                            : Enumerable.Range(0, mvType.ParentFrame.GaSpaceDimension);

                    var childValueAccessList =
                        idList
                            .Select(
                                id =>
                                    partialMvFlag
                                        ? rootValueAccess.DuplicateExceptLast().Append(id, scalarType)
                                        : rootValueAccess.Duplicate().Append(id, scalarType)
                            );

                    foreach (var childValueAccess in childValueAccessList)
                        yield return childValueAccess;

                    continue;
                }

                var structure = rootValueAccess.ExpressionType as GMacStructure;

                if (structure == null)
                    continue;

                foreach (var dataMember in structure.DataMembers)
                {
                    var childValueAccess =
                        rootValueAccess
                        .Duplicate()
                        .Append(dataMember.ObjectName, dataMember.SymbolType);

                    if (dataMember.SymbolType is TypePrimitive)
                        yield return childValueAccess;

                    else
                        stack.Push(childValueAccess);
                }
            }
        }

        /// <summary>
        /// Convert a value access with a root SymbolDataStore into a list of value access objects having 
        /// types other than structures (i.e. primitives and multivectors only)
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public static IEnumerable<LanguageValueAccess> ExpandStructures(this SymbolDataStore symbol)
        {
            return ExpandStructures(LanguageValueAccess.Create(symbol));
        }

        /// <summary>
        /// Convert a value access with a root SymbolDataStore into a list of value access objects having 
        /// types other than structures (i.e. primitives and multivectors only)
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<LanguageValueAccess> ExpandStructures(this LanguageValueAccess valueAccess)
        {
            valueAccess = ReduceMultivectorAccess(valueAccess);

            var stack = new Stack<LanguageValueAccess>();
            stack.Push(valueAccess);

            while (stack.Count > 0)
            {
                var rootValueAccess = stack.Pop();

                if (rootValueAccess.ExpressionType is TypePrimitive || rootValueAccess.ExpressionType is GMacFrameMultivector)
                {
                    yield return rootValueAccess;
                    continue;
                }

                var structure = rootValueAccess.ExpressionType as GMacStructure;

                if (structure == null)
                    continue;

                foreach (var dataMember in structure.DataMembers)
                {
                    var childValueAccess =
                        rootValueAccess
                        .Duplicate()
                        .Append(dataMember.ObjectName, dataMember.SymbolType);

                    if (dataMember.SymbolType is TypePrimitive)
                        yield return childValueAccess;

                    else
                        stack.Push(childValueAccess);
                }
            }
        }

        /// <summary>
        /// Convert a value access with a root SymbolLValue into a list of value access objects having 
        /// primitive types and construct assignments to primitive components of the given value provided
        /// that the given value is of the same type as the given value access
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IEnumerable<Tuple<LanguageValueAccess, ILanguageValuePrimitive>>
            ExpandAndAssignAll(this SymbolLValue symbol, ILanguageValue value)
        {
            return ExpandAndAssignAll(LanguageValueAccess.Create(symbol), value);
        }

        /// <summary>
        /// Convert a value access with a root SymbolLValue into a list of value access objects having 
        /// primitive types and construct assignments to primitive components of the given value provided
        /// that the given value is of the same type as the given value access
        /// </summary>
        /// <param name="valueAccess"></param>
        /// <param name="value"></param>
        /// <returns>A list of Tuple objects holding the expanded assignments</returns>
        public static IEnumerable<Tuple<LanguageValueAccess, ILanguageValuePrimitive>>
            ExpandAndAssignAll(this LanguageValueAccess valueAccess, ILanguageValue value)
        {
            valueAccess = ReduceMultivectorAccess(valueAccess);

            var accessStack = new Stack<LanguageValueAccess>();
            accessStack.Push(valueAccess);

            var valuesStack = new Stack<ILanguageValue>();
            valuesStack.Push(value);

            while (valuesStack.Count > 0)
            {
                var rootValueAccess = accessStack.Pop();
                var rootValue = valuesStack.Pop();

                var valuePrimitive = rootValue as ILanguageValuePrimitive;

                if (valuePrimitive != null)
                {
                    yield return
                        new Tuple<LanguageValueAccess, ILanguageValuePrimitive>(
                            rootValueAccess,
                            valuePrimitive
                            );

                    continue;
                }

                var valueMultivector = rootValue as GMacValueMultivector;

                if (valueMultivector != null)
                {
                    var mvValue = valueMultivector;
                    var scalarType = mvValue.CoefficientType;

                    var s = rootValueAccess.LastAccessStep as ValueAccessStepByKeyList<int>;

                    var partialMvFlag = !ReferenceEquals(s, null);

                    var idsList =
                        partialMvFlag
                            ? s.AccessKeyList
                            : null;

                    foreach (var id in mvValue.SymbolicMultivector.NonZeroBasisBladeIds)
                    {
                        if (!ReferenceEquals(idsList, null) && !idsList.Contains(id))
                            continue;

                        var childValueAccess =
                            partialMvFlag
                                ? rootValueAccess.DuplicateExceptLast().Append(id, scalarType)
                                : rootValueAccess.Duplicate().Append(id, scalarType);

                        yield return
                            new Tuple<LanguageValueAccess, ILanguageValuePrimitive>(
                                childValueAccess,
                                mvValue[id]
                                );
                    }

                    continue;
                }

                var valueStructureSparse = rootValue as ValueStructureSparse;

                if (valueStructureSparse == null)
                    continue;

                var structValue = valueStructureSparse;

                foreach (var pair in structValue)
                {
                    var dataMemberName = pair.Key;

                    var dataMember = structValue.ValueStructureType.GetDataMember(dataMemberName);

                    var childValueAccess =
                        rootValueAccess
                            .Duplicate()
                            .Append(dataMember.ObjectName, dataMember.SymbolType);

                    var languageValuePrimitive = pair.Value as ILanguageValuePrimitive;

                    if (languageValuePrimitive != null)
                        yield return
                            new Tuple<LanguageValueAccess, ILanguageValuePrimitive>(
                                childValueAccess,
                                languageValuePrimitive
                                );

                    else
                    {
                        accessStack.Push(childValueAccess);
                        valuesStack.Push(pair.Value);
                    }
                }
            }
        }

        public static IEnumerable<Tuple<LanguageValueAccess, GMacScalarBinding>>
            ExpandAndAssignAll(this SymbolLValue symbol, IGMacBinding pattern)
        {
            return ExpandAndAssignAll(LanguageValueAccess.Create(symbol), pattern);
        }

        public static IEnumerable<Tuple<LanguageValueAccess, GMacScalarBinding>>
            ExpandAndAssignAll(this LanguageValueAccess valueAccess, IGMacBinding pattern)
        {
            valueAccess = ReduceMultivectorAccess(valueAccess);

            var accessStack = new Stack<LanguageValueAccess>();
            accessStack.Push(valueAccess);

            var valuesStack = new Stack<IGMacBinding>();
            valuesStack.Push(pattern);

            while (valuesStack.Count > 0)
            {
                var rootValueAccess = accessStack.Pop();
                var rootValue = valuesStack.Pop();

                var scalarPattern = rootValue as GMacScalarBinding;

                if (scalarPattern != null)
                {
                    yield return
                        new Tuple<LanguageValueAccess, GMacScalarBinding>(
                            rootValueAccess,
                            scalarPattern
                            );

                    continue;
                }

                var mvPattern = rootValue as GMacMultivectorBinding;

                if (mvPattern != null)
                {
                    var scalarType = mvPattern.ScalarType;

                    var s = rootValueAccess.LastAccessStep as ValueAccessStepByKeyList<int>;

                    var partialMvFlag = !ReferenceEquals(s, null);

                    var idsList =
                        partialMvFlag
                            ? s.AccessKeyList
                            : null;

                    foreach (var pair in mvPattern)
                    {
                        var id = pair.Key;

                        if (!ReferenceEquals(idsList, null) && !idsList.Contains(id))
                            continue;

                        var childValueAccess =
                            partialMvFlag
                                ? rootValueAccess.DuplicateExceptLast().Append(id, scalarType.AssociatedPrimitiveType)
                                : rootValueAccess.Duplicate().Append(id, scalarType.AssociatedPrimitiveType);

                        yield return
                            new Tuple<LanguageValueAccess, GMacScalarBinding>(
                                childValueAccess,
                                mvPattern[id]
                                );
                    }

                    continue;
                }

                var structPattern = rootValue as GMacStructureBinding;

                if (structPattern == null)
                    continue;

                foreach (var pair in structPattern)
                {
                    var dataMemberName = pair.Key;

                    var dataMember = structPattern.BaseStructure.AssociatedStructure.GetDataMember(dataMemberName);

                    var childValueAccess =
                        rootValueAccess
                            .Duplicate()
                            .Append(dataMember.ObjectName, dataMember.SymbolType);

                    var structMemberScalarPattern = pair.Value as GMacScalarBinding;

                    if (structMemberScalarPattern != null)
                        yield return
                            new Tuple<LanguageValueAccess, GMacScalarBinding>(
                                childValueAccess,
                                structMemberScalarPattern
                                );

                    else
                    {
                        accessStack.Push(childValueAccess);
                        valuesStack.Push(pair.Value);
                    }
                }
            }
        }
    }
}
