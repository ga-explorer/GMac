using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMac.GMacCompiler.Semantic.AST;
using GMac.GMacCompiler.Semantic.AST.Extensions;
using GMac.GMacCompiler.Semantic.ASTConstants;
using GMac.GMacCompiler.Syntax;
using Irony.Parsing;
using IronyGrammars.Semantic.Expression.ValueAccess;
using IronyGrammars.Semantic.Scope;
using IronyGrammars.Semantic.Symbol;
using IronyGrammars.Semantic.Type;
using IronyGrammars.SourceCode;
using UtilLib.DataStructures;

//using UtilLib.Pools;

namespace GMac.GMacCompiler.Semantic.ASTGenerator
{
    internal sealed class GMacValueAccessGenerator : GMacAstSymbolGenerator
    {
        public static LanguageValueAccess Translate(GMacSymbolTranslatorContext context, ParseTreeNode node, bool isLvalue)
        {
            context.PushState(node);

            var translator = new GMacValueAccessGenerator();//new GMacValueAccessGenerator(context, isLvalue, true);

            translator.SetContext(context, isLvalue, true);
            translator.Translate();

            context.PopState();

            var result = translator._generatedValueAccess;

            //MasterPool.Release(translator);

            return result;
        }

        //public static bool TryTranslate_LValue(GMacSymbolTranslatorContext context, ParseTreeNode node, out LanguageValueAccess varAccess)
        //{
        //    var qualList = GenUtils.Translate_Qualified_Identifier(node);

        //    LanguageSymbol symbol;

        //    var flag =
        //        context
        //        .OpenedDistinctScopes()
        //        .LookupSymbol(qualList.FirstItem, out symbol);

        //    if (flag == false || (symbol is SymbolLValue) == false)
        //    {
        //        varAccess = null;
        //        return false;
        //    }

        //    varAccess = LanguageValueAccess.Create(symbol);

        //    return true;
        //}

        public static LanguageValueAccess Translate(GMacSymbolTranslatorContext context, ParseTreeNode node, bool isLvalue, TrimmedList<string> qualList)
        {
            context.PushState(node);

            var translator = new GMacValueAccessGenerator();//new GMacValueAccessGenerator(qualList, context, isLvalue, true);

            translator.SetContext(qualList, context, isLvalue, true);
            translator.Translate();

            context.PopState();

            var result = translator._generatedValueAccess;

            //MasterPool.Release(translator);

            return result;
        }

        public static LanguageSymbol Translate_Direct(GMacSymbolTranslatorContext context, ParseTreeNode node)
        {
            var valAccess = Translate(context, node, false);

            return 
                valAccess.IsFullAccess 
                ? valAccess.RootSymbol 
                : context.CompilationLog.RaiseGeneratorError<LanguageSymbol>("Expecting a direct r-value", node);
        }

        public static LanguageSymbol Translate_Direct(GMacSymbolTranslatorContext context, ParseTreeNode node, TrimmedList<string> qualList)
        {
            var valAccess = Translate(context, node, false, qualList);

            return 
                valAccess.IsFullAccess 
                ? valAccess.RootSymbol 
                : context.CompilationLog.RaiseGeneratorError<LanguageSymbol>("Expecting a direct r-value", node);
        }

        public static LanguageSymbol Translate_Direct(GMacSymbolTranslatorContext context, ParseTreeNode node, IEnumerable<string> acceptedRoleNames)
        {
            var acceptedRoleNamesArray = acceptedRoleNames.ToArray();

            var valAccess = Translate(context, node, false);

            if (valAccess.IsFullAccess && acceptedRoleNamesArray.Contains(valAccess.RootSymbol.SymbolRoleName))
                return valAccess.RootSymbol;

            var acceptedText =
                acceptedRoleNamesArray.Aggregate(
                    new StringBuilder(), 
                    (s, roleName) => s.Append(roleName).Append(", "),
                    s => { s.Length -= 2; return s.ToString(); }
                    );

            return context.CompilationLog.RaiseGeneratorError<SymbolWithScope>("Expecting a direct symbol with role: " + acceptedText, node);
        }

        public static LanguageSymbol Translate_Direct(GMacSymbolTranslatorContext context, ParseTreeNode node, string acceptedRoleName)
        {
            var valAccess = Translate(context, node, false);

            if (valAccess.IsFullAccess && acceptedRoleName == valAccess.RootSymbol.SymbolRoleName)
                return valAccess.RootSymbol;

            return context.CompilationLog.RaiseGeneratorError<SymbolWithScope>("Expecting a direct symbol with role: " + acceptedRoleName, node);
        }

        public static ILanguageType Translate_Direct_LanguageType(GMacSymbolTranslatorContext context, ParseTreeNode node)
        {
            var valAccess = Translate(context, node, false);

            if (valAccess.IsFullAccess && valAccess.RootSymbol is ILanguageType)
                return (ILanguageType)valAccess.RootSymbol;

            return context.CompilationLog.RaiseGeneratorError<ILanguageType>("Expecting a language type", node);
        }

        public static LanguageValueAccess Translate_LValue_StructureMember(GMacSymbolTranslatorContext context, ParseTreeNode node, GMacStructure structure)
        {
            context.PushState(structure.ChildSymbolScope, node);

            var translator = new GMacValueAccessGenerator();//new GMacValueAccessGenerator(context, true, false);

            translator.SetContext(context, true, false);
            translator.Translate();

            context.PopState();

            if (translator._generatedValueAccess.RootSymbol is SymbolStructureDataMember)
            {
                var result = translator._generatedValueAccess;

                //MasterPool.Release(translator);

                return result;
            }

            return context.CompilationLog.RaiseGeneratorError<LanguageValueAccess>("Expecting a structure member", node);
        }

        public static LanguageValueAccess Translate_LValue_MacroParameter(GMacSymbolTranslatorContext context, ParseTreeNode node, GMacMacro macro)
        {
            context.PushState(macro.ChildSymbolScope, node);

            var translator = new GMacValueAccessGenerator();//new GMacValueAccessGenerator(context, true, false);

            translator.SetContext(context, true, false);
            translator.Translate();

            context.PopState();

            if (translator._generatedValueAccess.RootSymbol is SymbolProcedureParameter)
            {
                var result = translator._generatedValueAccess;

                //MasterPool.Release(translator);

                return result;
            }

            return context.CompilationLog.RaiseGeneratorError<LanguageValueAccess>("Expecting a macro parameter", node);
        }


        ///This holds the list of dot-separated identidiers to be translated. 
        private TrimmedList<string> _qualList;

        ///This flag is true if the translated LanguageValueAccess is to be used as an L-Value
        private bool _isLValue;

        ///If false, only the parent scope is searched for the first item in the list of dot-separated identidiers to be translated. 
        private bool _followScopeChain;

        ///The generated LanguageValueAccess AST object
        private LanguageValueAccess _generatedValueAccess;


        //public override void ResetOnAcquire()
        //{
        //    base.ResetOnAcquire();

        //    _qualList = null;
        //    _isLValue = false;
        //    _followScopeChain = false;
        //    _generatedValueAccess = null;
        //}


        private void SetContext(GMacSymbolTranslatorContext context, bool isLvalue, bool followScopeChain)
        {
            SetContext(context);

            _isLValue = isLvalue;
            _followScopeChain = followScopeChain;
            _qualList = null;

            _generatedValueAccess = null;
        }

        private void SetContext(TrimmedList<string> qualList, GMacSymbolTranslatorContext context, bool isLvalue, bool followScopeChain)
        {
            SetContext(context);

            _isLValue = isLvalue;
            _followScopeChain = followScopeChain;
            _qualList = new TrimmedList<string>(qualList);

            _generatedValueAccess = null;
        }


        private LanguageValueAccess translate_FollowComponentsSequence(GMacStructure structure, LanguageValueAccess valAccess)
        {
            while (true)
            {
                //TODO: Search for a structure macro here

                SymbolStructureDataMember dataMember;

                if (structure.LookupDataMember(_qualList.FirstItem, out dataMember) == false)
                    CompilationLog.RaiseGeneratorError<LanguageValueAccess>("Structure member '" + _qualList.FirstItem + "' not recognized", RootParseNode);

                valAccess.Append(dataMember.ObjectName, dataMember.SymbolType);

                _qualList.IncreaseActiveStartOffset();

                if (_qualList.ActiveLength <= 0)
                    return valAccess;

                if (!dataMember.SymbolType.IsStructure())
                    return CompilationLog.RaiseGeneratorError<LanguageValueAccess>("Data components not recognized", RootParseNode);

                structure = (GMacStructure)dataMember.SymbolType;
            }
        }

        private LanguageValueAccess translate_StartAt_DataSymbol(SymbolDataStore startSymbol)
        {
            //Macro output parameters can only be written to (i.e. in LHS of assignment) but never read from (i.e. in RHS expression of assignment).
            if (_isLValue == false && startSymbol is SymbolProcedureParameter && ((SymbolProcedureParameter)startSymbol).DirectionOut)
                return CompilationLog.RaiseGeneratorError<LanguageValueAccess>("Cannot use output parameter" + startSymbol.ObjectName + " in RHS of an expression", RootParseNode);

            if (_qualList.ActiveLength == 0)
                return LanguageValueAccess.Create(startSymbol);

            if (!startSymbol.SymbolType.IsStructure())
                return CompilationLog.RaiseGeneratorError<LanguageValueAccess>("Data components not recognized", RootParseNode);

            var valAccess = LanguageValueAccess.Create(startSymbol);
            var structure = (GMacStructure)startSymbol.SymbolType;

            return translate_FollowComponentsSequence(structure, valAccess);
        }

        private LanguageValueAccess translate_StartAt_DirectSymbol(LanguageSymbol startSymbol)
        {
            return 
                _qualList.ActiveLength == 0 
                ? LanguageValueAccess.Create(startSymbol) 
                : CompilationLog.RaiseGeneratorError<LanguageValueAccess>("Components not recognized", RootParseNode);
        }

        private LanguageValueAccess translate_StartAt_FrameDefinition(GMacFrame frame)
        {
            if (_qualList.ActiveLength == 0)
                return LanguageValueAccess.Create(frame);

            LanguageSymbol symbol;
            var flag = frame.ChildSymbolScope.LookupSymbol(_qualList.FirstItem, out symbol);

            _qualList.IncreaseActiveStartOffset(1);

            if (flag == false)
                return CompilationLog.RaiseGeneratorError<LanguageValueAccess>("Frame component not recognized", RootParseNode);

            switch (symbol.SymbolRoleName)
            {
                case RoleNames.Constant:
                    return translate_StartAt_DataSymbol((SymbolDataStore)symbol);

                case RoleNames.FrameBasisVector:
                case RoleNames.FrameMultivector:
                case RoleNames.Macro:
                    return translate_StartAt_DirectSymbol(symbol);

                default:
                    return CompilationLog.RaiseGeneratorError<LanguageValueAccess>("Frame component name not recognized", RootParseNode);
            }
        }

        private LanguageValueAccess translate_StartAt_NamespaceDefinition(GMacNamespace nameSpace)
        {
            while (true)
            {
                if (_qualList.ActiveLength == 0)
                    return LanguageValueAccess.Create(nameSpace);

                LanguageSymbol symbol;

                if (nameSpace.ChildSymbolScope.LookupSymbol(_qualList.FirstItem, out symbol) == false)
                    CompilationLog.RaiseGeneratorError<LanguageValueAccess>("Symbol name not recognized", RootParseNode);

                _qualList.IncreaseActiveStartOffset(1);

                if (_isLValue)
                    CompilationLog.RaiseGeneratorError<int>("LValue symbol name not recognized", RootParseNode);

                switch (symbol.SymbolRoleName)
                {
                    case RoleNames.Constant:
                        return translate_StartAt_DataSymbol((SymbolDataStore) symbol);

                    case RoleNames.Structure:
                    case RoleNames.Macro:
                    case RoleNames.Transform:
                        return translate_StartAt_DirectSymbol(symbol);

                    case RoleNames.Frame:
                        return translate_StartAt_FrameDefinition((GMacFrame) symbol);

                    case RoleNames.Namespace:
                        nameSpace = (GMacNamespace) symbol;
                        continue;

                    default:
                        return CompilationLog.RaiseGeneratorError<LanguageValueAccess>("RValue symbol name not recognized", RootParseNode);
                }
            }
        }

        private LanguageValueAccess translate_Qualified_Identifier(ParseTreeNode node)
        {
            //If _QualList is not already filled, translate the current parse node into a list of identifiers
            if (_qualList == null)
                _qualList = GenUtils.Translate_Qualified_Identifier(node);

            //Lookup the first item of the translated list of identifiers within the current context 
            LanguageSymbol symbol;
            
            var flag = 
                _followScopeChain
                //? Context.LookupSymbolInOpenedDistinctScopes(_qualList.FirstItem, out symbol) 
                ? Context.OpenedDistinctScopes().LookupSymbol(_qualList.FirstItem, out symbol) 
                : Context.ActiveParentScope.LookupSymbol(_qualList.FirstItem, out symbol);

            if (flag == false)
                return CompilationLog.RaiseGeneratorError<LanguageValueAccess>("Symbol name not recognized", RootParseNode);

            //Ignore the first item from the list
            _qualList.IncreaseActiveStartOffset(1);

            if (_isLValue)
                //This is an l-value
                switch (symbol.SymbolRoleName)
                {
                    case RoleNames.StructureDataMember:
                    case RoleNames.MacroParameter:
                    case RoleNames.LocalVariable:
                        return translate_StartAt_DataSymbol((SymbolDataStore)symbol);

                    default:
                        return CompilationLog.RaiseGeneratorError<LanguageValueAccess>("LValue symbol name not recognized", RootParseNode);
                }

            //This is an r-value
            switch (symbol.SymbolRoleName)
            {
                case RoleNames.MacroParameter:
                case RoleNames.LocalVariable:
                case RoleNames.Constant:
                    return translate_StartAt_DataSymbol((SymbolDataStore)symbol);

                case RoleNames.FrameBasisVector:
                case RoleNames.BuiltinType:
                case RoleNames.FrameMultivector:
                case RoleNames.Structure:
                case RoleNames.Macro:
                case RoleNames.MacroTemplate:
                case RoleNames.Transform:
                case RoleNames.FrameSubspace:
                //case RoleNames.Binding:
                    return translate_StartAt_DirectSymbol(symbol);

                case RoleNames.Frame:
                    return translate_StartAt_FrameDefinition((GMacFrame)symbol);

                case RoleNames.Namespace:
                    return translate_StartAt_NamespaceDefinition((GMacNamespace)symbol);

                default:
                    return CompilationLog.RaiseGeneratorError<LanguageValueAccess>("RValue symbol name not recognized", RootParseNode);
            }
        }

        private LanguageValueAccess translate_Qualified_BasisBladesSet(ParseTreeNode node)
        {
            var valAccess = translate_Qualified_Identifier(node.ChildNodes[0]);

            if (!valAccess.ExpressionType.IsFrameMultivector())
                return CompilationLog.RaiseGeneratorError<LanguageValueAccess>("cannot find basis blade term", RootParseNode);

            var frame = ((GMacFrameMultivector)valAccess.ExpressionType).ParentFrame;

            var basisBladesIds = GMacFrameSubspacePatternGenerator.Translate(Context, node.ChildNodes[1], frame);

            var mvType = (GMacFrameMultivector)valAccess.ExpressionType;

            return valAccess.Append(basisBladesIds.TrueIndexes, mvType);
        }

        private LanguageValueAccess translate_Qualified_BasisBladeCoefficient(ParseTreeNode node)
        {
            var valAccess = translate_Qualified_Identifier(node.ChildNodes[0]);

            if (!valAccess.ExpressionType.IsFrameMultivector())
                return CompilationLog.RaiseGeneratorError<LanguageValueAccess>("cannot find basis blade coefficient",
                    RootParseNode);
            
            var frame = ((GMacFrameMultivector)valAccess.ExpressionType).ParentFrame;

            var basisBladesIds = GMacFrameSubspacePatternGenerator.Translate(Context, node.ChildNodes[1], frame);

            return valAccess.Append(basisBladesIds.FirstTrueIndex, GMacRootAst.ScalarType);
        }

        protected override void Translate()
        {
            switch (Context.ActiveParseNode.Term.ToString())
            {
                case GMacParseNodeNames.QualifiedIdentifier:
                    _generatedValueAccess = translate_Qualified_Identifier(Context.ActiveParseNode);
                    break;

                case GMacParseNodeNames.QualifiedBasisBladesSet:
                    _generatedValueAccess = translate_Qualified_BasisBladesSet(Context.ActiveParseNode);
                    break;

                case GMacParseNodeNames.QualifiedBasisBladeCoefficient:
                    _generatedValueAccess = translate_Qualified_BasisBladeCoefficient(Context.ActiveParseNode);
                    break;

                default:
                    CompilationLog.RaiseGeneratorError<int>("Symbol name not recognized", RootParseNode);
                    break;
            }
        }
    }
}
