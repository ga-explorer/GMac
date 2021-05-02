using CodeComposerLib.Irony.Semantic.Expression;
using CodeComposerLib.Irony.Semantic.Expression.Basic;
using CodeComposerLib.Irony.Semantic.Expression.ValueAccess;
using CodeComposerLib.Irony.Semantic.Type;
using CodeComposerLib.Irony.SourceCode;
using GMac.GMacCompiler.Semantic.AST;
using GMac.GMacCompiler.Semantic.ASTConstants;
using GMac.GMacCompiler.Syntax;
using Irony.Parsing;

namespace GMac.GMacCompiler.Semantic.ASTGenerator
{
    internal sealed class GMacExpressionAtomicFunctionGenerator : GMacAstSymbolGenerator
    {
        public static ILanguageExpression Translate(GMacExpressionBasicGenerator langExprGen, ParseTreeNode node)
        {
            var context = langExprGen.Context;

            context.PushState(node);

            var translator = new GMacExpressionAtomicFunctionGenerator();// new GMacExpressionAtomicFunctionGenerator(langExprGen);

            translator.SetContext(langExprGen);
            translator.Translate();

            context.PopState();

            var result = translator._generatedExpression;

            //MasterPool.Release(translator);

            return result;
        }


        private ILanguageExpression _generatedExpression;

        public GMacExpressionBasicGenerator BasicExpressionGenerator { get; private set; }

        
        //public override void ResetOnAcquire()
        //{
        //    base.ResetOnAcquire();

        //    _generatedExpression = null;
        //    BasicExpressionGenerator = null;
        //}


        private void SetContext(GMacExpressionBasicGenerator basicExprGen)
        {
            SetContext(basicExprGen.Context);
            BasicExpressionGenerator = basicExprGen;
        }


        private int translate_BasisBladeCoefficient(GMacFrameMultivector mvType, ParseTreeNode node)
        {
            var basisBladesIds = GMacFrameSubspacePatternGenerator.Translate(Context, node, mvType.ParentFrame);

            return basisBladesIds.FirstTrueIndex;
        }

        private ILanguageExpression translate_Expression_Function_Macro(GMacMacro macro, ParseTreeNode node)
        {
            var operands = OperandsByValueAccess.Create();

            if (node.ChildNodes.Count == 0) 
                return BasicPolyadic.Create(macro.OutputParameterType, macro, operands);

            var expressionFunctionInputsNode = node.ChildNodes[0];

            var subNode = expressionFunctionInputsNode.ChildNodes[0];

            switch (subNode.Term.ToString())
            {
                case GMacParseNodeNames.ExpressionFunctionInputsAssignments:
                    var expressionFunctionInputsAssignmentsNode = subNode;

                    foreach (var nodeExpressionFunctionInputsAssignmentsItem in expressionFunctionInputsAssignmentsNode.ChildNodes)
                    {
                        var lhsNode = nodeExpressionFunctionInputsAssignmentsItem.ChildNodes[0].ChildNodes[0];
                        var rhsNode = nodeExpressionFunctionInputsAssignmentsItem.ChildNodes[1];

                        var lhsValAccess = GMacValueAccessGenerator.Translate_LValue_MacroParameter(Context, lhsNode, macro);

                        var rhsExpr =
                            BasicExpressionGenerator.Generate_PolyadicOperand(
                                lhsValAccess.ExpressionType,
                                GMacExpressionGenerator.Translate(BasicExpressionGenerator, rhsNode)
                                );

                        operands.AddOperand(lhsValAccess, rhsExpr);
                    }

                    break;

                case GMacParseNodeNames.ExpressionFunctionInputsExpressions:
                    var nodeExpressionFunctionInputsExpressions = subNode;

                    var i = -1;
                    foreach (var parameter in macro.Parameters)
                    {
                        if (i >= nodeExpressionFunctionInputsExpressions.ChildNodes.Count)
                            break;

                        //The first parameter of any macro is the 'result' output parameter; ignore it
                        if (i >= 0)
                        {
                            var rhsNode = nodeExpressionFunctionInputsExpressions.ChildNodes[i];

                            var lhsValAccess = LanguageValueAccess.Create(parameter);

                            var rhsExpr =
                                BasicExpressionGenerator.Generate_PolyadicOperand(
                                    lhsValAccess.ExpressionType,
                                    GMacExpressionGenerator.Translate(BasicExpressionGenerator, rhsNode)
                                    );

                            operands.AddOperand(lhsValAccess, rhsExpr);
                        }

                        i = i + 1;
                    }

                    if (nodeExpressionFunctionInputsExpressions.ChildNodes.Count > i)
                        return CompilationLog.RaiseGeneratorError<ILanguageExpression>("Expecting a list of at most " + i + " expression as input to the macro call", node);

                    break;

                default:
                    return CompilationLog.RaiseGeneratorError<ILanguageExpression>("Expecting a list of expressions or list of parameter assignments as input to the macro call", node);
            }

            return BasicPolyadic.Create(macro.OutputParameterType, macro, operands);
        }

        private ILanguageExpression translate_Expression_Function_Structure(GMacStructure structure, ILanguageExpressionAtomic defaultValueSource, ParseTreeNode node)
        {
            var operands = OperandsByValueAccess.Create();

            if (node.ChildNodes.Count == 0)
                return
                    defaultValueSource == null
                    ? structure.CreateConstructorExpression(operands)
                    : structure.CreateConstructorExpression(defaultValueSource, operands);

            var expressionFunctionInputsNode = node.ChildNodes[0];

            var subNode = expressionFunctionInputsNode.ChildNodes[0];

            switch (subNode.Term.ToString())
            {
                case GMacParseNodeNames.ExpressionFunctionInputsAssignments:
                    var expressionFunctionInputsAssignmentsNode = subNode;

                    foreach (var nodeExpressionFunctionInputsAssignmentsItem in expressionFunctionInputsAssignmentsNode.ChildNodes)
                    {
                        var lhsNode = nodeExpressionFunctionInputsAssignmentsItem.ChildNodes[0].ChildNodes[0];
                        var rhsNode = nodeExpressionFunctionInputsAssignmentsItem.ChildNodes[1];

                        var lhsValAccess = GMacValueAccessGenerator.Translate_LValue_StructureMember(Context, lhsNode, structure);

                        var rhsExpr =
                            BasicExpressionGenerator.Generate_PolyadicOperand(
                                lhsValAccess.ExpressionType,
                                GMacExpressionGenerator.Translate(BasicExpressionGenerator, rhsNode)
                                );

                        operands.AddOperand(lhsValAccess, rhsExpr);
                    }

                    break;

                case GMacParseNodeNames.ExpressionFunctionInputsExpressions:
                    var expressionFunctionInputsExpressionsNode = subNode;

                    var i = 0;

                    foreach (var dataMember in structure.DataMembers)
                    {
                        if (i >= expressionFunctionInputsExpressionsNode.ChildNodes.Count)
                            break;

                        var rhsNode = expressionFunctionInputsExpressionsNode.ChildNodes[i];

                        var lhsValAccess = LanguageValueAccess.Create(dataMember);

                        var rhsExpr = 
                            BasicExpressionGenerator.Generate_PolyadicOperand(
                                lhsValAccess.ExpressionType,
                                GMacExpressionGenerator.Translate(BasicExpressionGenerator, rhsNode)
                                );

                        operands.AddOperand(lhsValAccess, rhsExpr);

                        i = i + 1;
                    }

                    if (expressionFunctionInputsExpressionsNode.ChildNodes.Count > i)
                        return CompilationLog.RaiseGeneratorError<ILanguageExpression>("Expecting a list of at most " + i + " expression as input to the structure construction", node);

                    break;

                default:
                    return CompilationLog.RaiseGeneratorError<ILanguageExpression>("Expecting a list of expressions or a list of member assignments as input to the structure construction", node);
            }

            return 
                defaultValueSource == null 
                ? structure.CreateConstructorExpression(operands) 
                : structure.CreateConstructorExpression(defaultValueSource, operands);
        }

        private ILanguageExpression translate_Expression_Function_Subspace(GMacFrameSubspace subspace, ILanguageExpressionAtomic defaultValueSource, ParseTreeNode node)
        {
            var operands = OperandsByIndex.Create();

            if (node.ChildNodes.Count <= 0)
                return
                    ReferenceEquals(defaultValueSource, null)
                    ? subspace.ParentFrame.MultivectorType.CreateConstructorExpression(operands)
                    : subspace.ParentFrame.MultivectorType.CreateConstructorExpression(defaultValueSource, operands);

            var expressionFunctionInputsNode = node.ChildNodes[0];

            var subNode = expressionFunctionInputsNode.ChildNodes[0];

            switch (subNode.Term.ToString())
            {
                //case GMacParseNodeNames.ExpressionFunctionInputsAssignments:
                //    var expressionFunctionInputsAssignmentsNode = subNode;

                //    foreach (var nodeExpressionFunctionInputsAssignmentsItem in expressionFunctionInputsAssignmentsNode.ChildNodes)
                //    {
                //        var lhsNode = nodeExpressionFunctionInputsAssignmentsItem.ChildNodes[0].ChildNodes[0];
                //        var rhsNode = nodeExpressionFunctionInputsAssignmentsItem.ChildNodes[1];

                //        if (lhsNode.Term.Name == GMacParseNodeNames.BasisBladeCoefficient)
                //        {
                //            var basisBladeId = translate_BasisBladeCoefficient(subspace.ParentFrame.MultivectorType, lhsNode);

                //            var rhsExpr =
                //                BasicExpressionGenerator.Generate_PolyadicOperand(
                //                    GMacRootAst.ScalarType,
                //                    GMacExpressionGenerator.Translate(BasicExpressionGenerator, rhsNode)
                //                    );

                //            operands.AddOperand(basisBladeId, rhsExpr);
                //        }
                //        else
                //            return CompilationLog.RaiseGeneratorError<ILanguageExpression>("Expecting a list of basis blade assignments as input to the multivector construction", node);
                //    }

                //    break;

                case GMacParseNodeNames.ExpressionFunctionInputsExpressions:
                    var expressionFunctionInputsExpressionsNode = subNode;

                    var i = 0;

                    foreach (var basisBladeId in subspace.SubspaceSignaturePattern.TrueIndexes)
                    {
                        if (i >= expressionFunctionInputsExpressionsNode.ChildNodes.Count)
                            break;

                        var rhsNode = expressionFunctionInputsExpressionsNode.ChildNodes[i];

                        var rhsExpr =
                            BasicExpressionGenerator.Generate_PolyadicOperand(
                                GMacRootAst.ScalarType,
                                GMacExpressionGenerator.Translate(BasicExpressionGenerator, rhsNode)
                                );

                        operands.AddOperand((ulong)basisBladeId, rhsExpr);

                        i = i + 1;
                    }

                    if (expressionFunctionInputsExpressionsNode.ChildNodes.Count > i)
                        return CompilationLog.RaiseGeneratorError<ILanguageExpression>("Expecting a list of at most " + i + " expression as input to the multivector subspace construction", node);

                    break;

                default:
                    return CompilationLog.RaiseGeneratorError<ILanguageExpression>("Expecting a list of expressions as input to the multivector subspace construction", node);
            }

            return
                ReferenceEquals(defaultValueSource, null)
                ? subspace.ParentFrame.MultivectorType.CreateConstructorExpression(operands)
                : subspace.ParentFrame.MultivectorType.CreateConstructorExpression(defaultValueSource, operands);
        }

        private ILanguageExpression translate_Expression_Function_MultivectorType(GMacFrameMultivector mvType, ILanguageExpressionAtomic defaultValueSource, ParseTreeNode node)
        {
            var operands = OperandsByIndex.Create();

            if (node.ChildNodes.Count <= 0)
                return
                    ReferenceEquals(defaultValueSource, null)
                    ? mvType.CreateConstructorExpression(operands)
                    : mvType.CreateConstructorExpression(defaultValueSource, operands);
            
            var expressionFunctionInputsNode = node.ChildNodes[0];

            var subNode = expressionFunctionInputsNode.ChildNodes[0];

            switch (subNode.Term.ToString())
            {
                case GMacParseNodeNames.ExpressionFunctionInputsAssignments:
                    var expressionFunctionInputsAssignmentsNode = subNode;

                    foreach (var nodeExpressionFunctionInputsAssignmentsItem in expressionFunctionInputsAssignmentsNode.ChildNodes)
                    {
                        var lhsNode = nodeExpressionFunctionInputsAssignmentsItem.ChildNodes[0].ChildNodes[0];
                        var rhsNode = nodeExpressionFunctionInputsAssignmentsItem.ChildNodes[1];

                        if (lhsNode.Term.Name == GMacParseNodeNames.BasisBladeCoefficient)
                        {
                            var basisBladeId = translate_BasisBladeCoefficient(mvType, lhsNode);

                            var rhsExpr =
                                BasicExpressionGenerator.Generate_PolyadicOperand(
                                    GMacRootAst.ScalarType,
                                    GMacExpressionGenerator.Translate(BasicExpressionGenerator, rhsNode)
                                    );

                            operands.AddOperand((ulong)basisBladeId, rhsExpr);
                        }
                        else
                            return CompilationLog.RaiseGeneratorError<ILanguageExpression>("Expecting a list of basis blade assignments as input to the multivector construction", node);
                    }

                    break;

                case GMacParseNodeNames.ExpressionFunctionInputsExpressions:
                    return 
                        ReferenceEquals(defaultValueSource, null) 
                            ? translate_Expression_Function_Cast(mvType, node) 
                            : CompilationLog.RaiseGeneratorError<ILanguageExpression>("A default value cannot be used for a type cast operation", node);

                default:
                    return CompilationLog.RaiseGeneratorError<ILanguageExpression>("Expecting a list of basis blade assignments as input to the multivector construction", node);
            }

            return 
                ReferenceEquals(defaultValueSource, null) 
                ? mvType.CreateConstructorExpression(operands) 
                : mvType.CreateConstructorExpression(defaultValueSource, operands);
        }

        ///node is 'Expression_Function_Inputs'
        private ILanguageExpression translate_Expression_Function_Inputs_1(ParseTreeNode node)
        {
            node.Assert(GMacParseNodeNames.ExpressionFunctionInputs);

            var subNode = node.ChildNodes[0];

            if (subNode.Term.ToString() != GMacParseNodeNames.ExpressionFunctionInputsExpressions)
                return CompilationLog.RaiseGeneratorError<ILanguageExpression>("Expecting a single expression as input", node);

            var expressionFunctionInputsExpressionsNode = subNode;

            return 
                expressionFunctionInputsExpressionsNode.ChildNodes.Count == 1 
                ? GMacExpressionGenerator.Translate(BasicExpressionGenerator, subNode.ChildNodes[0]) 
                : CompilationLog.RaiseGeneratorError<ILanguageExpression>("Expecting a single expression as input", node);
        }

        ///node is 'Expression_Function_Inputs'
        private void translate_Expression_Function_Inputs_2(ParseTreeNode node, out ILanguageExpression expr1, out ILanguageExpression expr2)
        {
            node.Assert(GMacParseNodeNames.ExpressionFunctionInputs);

            expr1 = null;
            expr2 = null;

            var subNode = node.ChildNodes[0];

            if (subNode.Term.ToString() == GMacParseNodeNames.ExpressionFunctionInputsExpressions)
            {
                var expressionFunctionInputsExpressionsNode = subNode;

                if (expressionFunctionInputsExpressionsNode.ChildNodes.Count == 2)
                {
                    expr1 = 
                        GMacExpressionGenerator.Translate(BasicExpressionGenerator, subNode.ChildNodes[0]);

                    expr2 =
                        GMacExpressionGenerator.Translate(BasicExpressionGenerator, subNode.ChildNodes[1]);

                    return;
                }

                CompilationLog.RaiseGeneratorError<ILanguageExpression>("Expecting a single expression as input", node);
            }

            CompilationLog.RaiseGeneratorError<ILanguageExpression>("Expecting a single expression as input", node);
        }

        ///node is 'Expression_Function_Inputs_opt'
        private ILanguageExpression translate_Expression_Function_Transform(GMacMultivectorTransform transform, ParseTreeNode node)
        {
            if (node.ChildNodes.Count != 1)
                return
                    CompilationLog.RaiseGeneratorError<ILanguageExpression>(
                        "Expecting a single expression as input to the transform", node);
            
            var expr = translate_Expression_Function_Inputs_1(node.ChildNodes[0]);

            return BasicExpressionGenerator.Generate_TransformApplication(transform, expr);
        }

        private ILanguageExpression translate_Expression_Function_Cast(GMacFrameMultivector targetType, ParseTreeNode node)
        {
            if (node.ChildNodes.Count != 1)
                return
                    CompilationLog.RaiseGeneratorError<ILanguageExpression>(
                        "Expecting a single expression as input to the type cast operation", node);
            
            var expr = translate_Expression_Function_Inputs_1(node.ChildNodes[0]);

            return BasicExpressionGenerator.Generate_TypeCast(targetType, targetType, expr);
        }

        private ILanguageExpression translate_Expression_Function_Cast(TypePrimitive targetType, ParseTreeNode node)
        {
            if (node.ChildNodes.Count != 1)
                return
                    CompilationLog.RaiseGeneratorError<ILanguageExpression>(
                        "Expecting a single expression as input to the type cast operation", node);
            
            var expr = translate_Expression_Function_Inputs_1(node.ChildNodes[0]);

            return BasicExpressionGenerator.Generate_TypeCast(targetType, targetType, expr);
        }

        private ILanguageExpression translate_BuiltinMacro_norm(GMacOpInfo opInfo, ParseTreeNode node)
        {
            if (node.ChildNodes.Count != 1)
                return
                    CompilationLog.RaiseGeneratorError<ILanguageExpression>(
                        "Expecting a single expression as input to the builtin macro", node);
            
            var expr = translate_Expression_Function_Inputs_1(node.ChildNodes[0]);

            return BasicExpressionGenerator.Generate_UnaryNorm(opInfo, expr);
        }

        private ILanguageExpression translate_BuiltinMacro_reverse(ParseTreeNode node)
        {
            if (node.ChildNodes.Count != 1)
                return
                    CompilationLog.RaiseGeneratorError<ILanguageExpression>(
                        "Expecting a single expression as input to the builtin macro", node);
            
            var expr = translate_Expression_Function_Inputs_1(node.ChildNodes[0]);

            return BasicExpressionGenerator.Generate_UnaryReverse(expr);
        }

        private ILanguageExpression translate_BuiltinMacro_clif_conj(ParseTreeNode node)
        {
            if (node.ChildNodes.Count != 1)
                return
                    CompilationLog.RaiseGeneratorError<ILanguageExpression>(
                        "Expecting a single expression as input to the builtin macro", node);
            
            var expr = translate_Expression_Function_Inputs_1(node.ChildNodes[0]);

            return BasicExpressionGenerator.Generate_UnaryCliffordConjugate(expr);
        }

        private ILanguageExpression translate_BuiltinMacro_grade_inv(ParseTreeNode node)
        {
            if (node.ChildNodes.Count != 1)
                return
                    CompilationLog.RaiseGeneratorError<ILanguageExpression>(
                        "Expecting a single expression as input to the builtin macro", node);
            
            var expr = translate_Expression_Function_Inputs_1(node.ChildNodes[0]);

            return BasicExpressionGenerator.Generate_UnaryGradeInversion(expr);
        }
        private ILanguageExpression translate_BuiltinMacro_diff(ParseTreeNode node)
        {
            if (node.ChildNodes.Count != 1)
                return
                    CompilationLog.RaiseGeneratorError<ILanguageExpression>(
                        "Expecting two expressions as input to the builtin macro", node);

            translate_Expression_Function_Inputs_2(node.ChildNodes[0], out var expr1, out var expr2);

            return BasicExpressionGenerator.Generate_Diff(expr1, expr2);
        }

        private ILanguageExpression translate_TryBuiltinMacro(ParseTreeNode node)
        {
            var nodeQualifiedIdentifier = node.ChildNodes[0];

            if (nodeQualifiedIdentifier.ChildNodes.Count != 1)
                return null;

            var nodeDefaultValueOpt = node.ChildNodes[1];

            var nodeInputsOpt = node.ChildNodes[2];

            switch (nodeQualifiedIdentifier.FindTokenAndGetText())
            {
                case "norm2":
                    return 
                        nodeDefaultValueOpt.ChildNodes.Count == 0 
                        ? translate_BuiltinMacro_norm(GMacOpInfo.UnaryNorm2, nodeInputsOpt) 
                        : CompilationLog.RaiseGeneratorError<ILanguageExpression>("A default value cannot be used for a macro call operation", nodeDefaultValueOpt);

                case "mag":
                    return
                        nodeDefaultValueOpt.ChildNodes.Count == 0
                        ? translate_BuiltinMacro_norm(GMacOpInfo.UnaryMagnitude, nodeInputsOpt)
                        : CompilationLog.RaiseGeneratorError<ILanguageExpression>("A default value cannot be used for a macro call operation", nodeDefaultValueOpt);

                case "mag2":
                    return 
                        nodeDefaultValueOpt.ChildNodes.Count == 0
                        ? translate_BuiltinMacro_norm(GMacOpInfo.UnaryMagnitude2, nodeInputsOpt) 
                        : CompilationLog.RaiseGeneratorError<ILanguageExpression>("A default value cannot be used for a macro call operation", nodeDefaultValueOpt);

                case "emag":
                    return
                        nodeDefaultValueOpt.ChildNodes.Count == 0
                        ? translate_BuiltinMacro_norm(GMacOpInfo.UnaryEuclideanMagnitude, nodeInputsOpt)
                        : CompilationLog.RaiseGeneratorError<ILanguageExpression>("A default value cannot be used for a macro call operation", nodeDefaultValueOpt);

                case "emag2":
                    return
                        nodeDefaultValueOpt.ChildNodes.Count == 0
                        ? translate_BuiltinMacro_norm(GMacOpInfo.UnaryEuclideanMagnitude2, nodeInputsOpt)
                        : CompilationLog.RaiseGeneratorError<ILanguageExpression>("A default value cannot be used for a macro call operation", nodeDefaultValueOpt);

                case "grade_inv":
                    return 
                        nodeDefaultValueOpt.ChildNodes.Count == 0 
                        ? translate_BuiltinMacro_grade_inv(nodeInputsOpt) 
                        : CompilationLog.RaiseGeneratorError<ILanguageExpression>("A default value cannot be used for a macro call operation", nodeDefaultValueOpt);

                case "cliff_conj":
                    return 
                        nodeDefaultValueOpt.ChildNodes.Count == 0 
                        ? translate_BuiltinMacro_clif_conj(nodeInputsOpt) 
                        : CompilationLog.RaiseGeneratorError<ILanguageExpression>("A default value cannot be used for a macro call operation", nodeDefaultValueOpt);

                case "reverse":
                    return 
                        nodeDefaultValueOpt.ChildNodes.Count == 0 
                        ? translate_BuiltinMacro_reverse(nodeInputsOpt) 
                        : CompilationLog.RaiseGeneratorError<ILanguageExpression>("A default value cannot be used for a macro call operation", nodeDefaultValueOpt);

                case "diff":
                    return 
                        nodeDefaultValueOpt.ChildNodes.Count == 0 
                        ? translate_BuiltinMacro_diff(nodeInputsOpt) 
                        : CompilationLog.RaiseGeneratorError<ILanguageExpression>("A default value cannot be used for a macro call operation", nodeDefaultValueOpt);

                default:
                    return null;
            }
        }

        private ILanguageExpression translate_Expression_Function(ParseTreeNode node)
        {
            var expr = translate_TryBuiltinMacro(node);

            if (expr != null)
                return expr;

            var nodeQualifiedIdentifier = node.ChildNodes[0];

            var nodeDefaultValueOpt = node.ChildNodes[1];

            var nodeInputsOpt = node.ChildNodes[2];

            ILanguageExpressionAtomic defaultValueSource;

            var symbol = GMacValueAccessGenerator.Translate_Direct(Context, nodeQualifiedIdentifier);

            var macro = symbol as GMacMacro;

            if (macro != null)
            {
                return 
                    nodeDefaultValueOpt.ChildNodes.Count == 0 
                    ? translate_Expression_Function_Macro(macro, nodeInputsOpt) 
                    : CompilationLog.RaiseGeneratorError<ILanguageExpression>("A default value cannot be used for a macro call operation", nodeDefaultValueOpt);
            }

            var structure = symbol as GMacStructure;

            if (structure != null)
            {
                if (nodeDefaultValueOpt.ChildNodes.Count <= 0)
                    return translate_Expression_Function_Structure(structure, null,
                        nodeInputsOpt);
                
                var nodeDefaultValue = nodeDefaultValueOpt.ChildNodes[0];

                defaultValueSource =
                    BasicExpressionGenerator.Generate_PolyadicOperand(
                        structure,
                        GMacExpressionGenerator.Translate(BasicExpressionGenerator, nodeDefaultValue.ChildNodes[0])
                        );

                return translate_Expression_Function_Structure(structure, defaultValueSource, nodeInputsOpt);
            }

            var subspace = symbol as GMacFrameSubspace;

            if (subspace != null)
            {
                if (nodeDefaultValueOpt.ChildNodes.Count <= 0)
                    return translate_Expression_Function_Subspace(subspace, null, nodeInputsOpt);

                var nodeDefaultValue = nodeDefaultValueOpt.ChildNodes[0];

                defaultValueSource =
                    BasicExpressionGenerator.Generate_PolyadicOperand(
                        subspace.ParentFrame.MultivectorType,
                        GMacExpressionGenerator.Translate(BasicExpressionGenerator, nodeDefaultValue.ChildNodes[0])
                        );

                return translate_Expression_Function_Subspace(subspace, defaultValueSource, nodeInputsOpt);
            }

            var multivector = symbol as GMacFrameMultivector;

            if (multivector != null)
            {
                if (nodeDefaultValueOpt.ChildNodes.Count <= 0)
                    return translate_Expression_Function_MultivectorType(multivector,
                        null, nodeInputsOpt);

                var nodeDefaultValue = nodeDefaultValueOpt.ChildNodes[0];

                defaultValueSource =
                    BasicExpressionGenerator.Generate_PolyadicOperand(
                        multivector,
                        GMacExpressionGenerator.Translate(BasicExpressionGenerator, nodeDefaultValue.ChildNodes[0])
                        );

                return translate_Expression_Function_MultivectorType(multivector, defaultValueSource, nodeInputsOpt);
            }

            var typePrimitive = symbol as TypePrimitive;

            if (typePrimitive != null)
            {
                return 
                    nodeDefaultValueOpt.ChildNodes.Count == 0 
                    ? translate_Expression_Function_Cast(typePrimitive, nodeInputsOpt) 
                    : CompilationLog.RaiseGeneratorError<ILanguageExpression>("A default value cannot be used for a type cast operation", nodeDefaultValueOpt);
            }

            var transform = symbol as GMacMultivectorTransform;

            if (transform != null)
            {
                return 
                    nodeDefaultValueOpt.ChildNodes.Count == 0 
                    ? translate_Expression_Function_Transform(transform, nodeInputsOpt) 
                    : CompilationLog.RaiseGeneratorError<ILanguageExpression>("A default value cannot be used for a multivector transform operation", nodeDefaultValueOpt);
            }

            return CompilationLog.RaiseGeneratorError<ILanguageExpression>("Expecting a macro, structure, transform, or multivector class", node);
        }


        protected override void Translate()
        {
            _generatedExpression = translate_Expression_Function(Context.ActiveParseNode);
        }
    }
}
