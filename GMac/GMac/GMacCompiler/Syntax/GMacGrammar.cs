using Irony.Parsing;

namespace GMac.GMacCompiler.Syntax
{
    internal enum GMacGrammarRootType 
    { 
        /// <summary>
        /// For normal parsing of full GMacDSL source code
        /// </summary>
        Normal = 0, 

        /// <summary>
        /// For parsing GMacDSL expressions only
        /// </summary>
        Expression = 1,
 
        /// <summary>
        /// For parsing GMacDSL qualified items only (for producing LanguageValueAccess objects)
        /// </summary>
        QualifiedItem = 2, 

        /// <summary>
        /// For parsing GMacDSL macro definition
        /// </summary>
        Macro = 3,

        /// <summary>
        /// For parsing GMacDSL structure definition
        /// </summary>
        Structure = 4,

        /// <summary>
        /// For parsing GMacDSL commands
        /// </summary>
        Commands = 5
    }

    internal sealed class GMacGrammar : Grammar
    {
        public GMacGrammar() : this(GMacGrammarRootType.Normal) { }

        public GMacGrammar(GMacGrammarRootType rootType)
        {
            #region Terminals

            var multiLineComment = new CommentTerminal(GMacParseNodeNames.MultiLineComment, "/*", "*/");
            NonGrammarTerminals.Add(multiLineComment);

            var singleLineComment = new CommentTerminal(GMacParseNodeNames.SingleLineComment, "//", "\n", "\r\n");
            NonGrammarTerminals.Add(singleLineComment);

            //Double quoted string
            var dqString = new StringLiteral(GMacParseNodeNames. DqString, "\"", StringOptions.AllowsAllEscapes);
            dqString.AddPrefix("@", StringOptions.NoEscapes | StringOptions.AllowsLineBreak | StringOptions.AllowsDoubledQuote);

            //Single quoted string
            var sqString = new StringLiteral(GMacParseNodeNames.SqString, "'", StringOptions.AllowsAllEscapes);
            sqString.AddPrefix("@", StringOptions.NoEscapes | StringOptions.AllowsLineBreak | StringOptions.AllowsDoubledQuote);

            var identifier = new IdentifierTerminal(GMacParseNodeNames.Identifier);

            var constantNumber = new NumberLiteral(GMacParseNodeNames.ConstantNumber, NumberOptions.AllowSign);

            var punctColon = ToTerm(":");
            var punctDot = ToTerm(".");
            var punctComma = ToTerm(",");
            var punctEqual = ToTerm("=");
            var punctHash = ToTerm("#");
            var punctAt = ToTerm("@");
            var punctAmp = ToTerm("&");
            var punctLcb = ToTerm("{");
            var punctRcb = ToTerm("}");
            var punctLrb = ToTerm("(");
            var punctRrb = ToTerm(")");
            var punctLsb = ToTerm("[");
            var punctRsb = ToTerm("]");
            var punctLab = ToTerm("<");
            var punctRab = ToTerm(">");

            var keywordBreakpoint = ToTerm("breakpoint");

            //TODO: Create an enum for GMacDSL keywords. This is useful for coloring in the source code editor
            var keywordUsing = ToTerm("using");
            var keywordBegin = ToTerm("begin");
            var keywordEnd = ToTerm("end");
            var keywordFrame = ToTerm("frame");
            var keywordMacro = ToTerm("macro");
            var keywordTransform = ToTerm("transform");
            var keywordEuclidean = ToTerm("euclidean");
            var keywordIpm = ToTerm("IPM");
            var keywordCbm = ToTerm("CBM");
            var keywordOrthogonal = ToTerm("orthogonal");
            var keywordOrthonormal = ToTerm("orthonormal");
            var keywordReciprocal = ToTerm("reciprocal");
            var keywordClass = ToTerm("class");
            var keywordGa = ToTerm("ga");
            var keywordDeclare = ToTerm("declare");
            var keywordLet = ToTerm("let");
            var keywordOutput = ToTerm("output");
            var keywordReturn = ToTerm("return");
            var keywordNamespace = ToTerm("namespace");
            var keywordConstant = ToTerm("constant");
            var keywordStructure = ToTerm("structure");
            var keywordFrom = ToTerm("from");
            var keywordTo = ToTerm("to");
            var keywordOpen = ToTerm("open");
            var keywordTemplate = ToTerm("template");
            var keywordImplement = ToTerm("implement");
            var keywordSubspace = ToTerm("subspace");
            var keywordOn = ToTerm("on");
            var keywordAccess = ToTerm("access");
            var keywordBind = ToTerm("bind");
            var keywordWith = ToTerm("with");
            var keywordBinding = ToTerm("binding");

            var gaOpCaret = ToTerm("^");
            var gaOpTimes = ToTerm("*");
            var gaOpDivide = ToTerm("/");
            var gaOpPlus = ToTerm("+");
            var gaOpMinus = ToTerm("-");

            var gaOpOuterProduct = ToTerm("op");
            
            var gaOpGeometricProduct = ToTerm("gp");
            var gaOpScalarProduct = ToTerm("sp");
            var gaOpLeftContractionProduct = ToTerm("lcp");
            var gaOpRightContractionProduct = ToTerm("rcp");
            var gaOpFatDotProduct = ToTerm("fdp");
            var gaOpHestenesInnrProduct = ToTerm("hip");
            var gaOpCommutatorProduct = ToTerm("cp");
            var gaOpAntiCommutatorProduct = ToTerm("acp");

            var gaOpEuclideanGeometricProduct = ToTerm("egp");
            var gaOpEuclideanScalarProduct = ToTerm("esp");
            var gaOpEuclideanLeftContractionProduct = ToTerm("elcp");
            var gaOpEuclideanRightContractionProduct = ToTerm("ercp");
            var gaOpEuclideanFatDotProduct = ToTerm("efdp");
            var gaOpEuclideanHestenesInnrProduct = ToTerm("ehip");
            var gaOpEuclideanCommutatorProduct = ToTerm("ecp");
            var gaOpEuclideanAntiCommutatorProduct = ToTerm("eacp");

            #endregion



            #region Non-Terminals

            var gmacDslRoot = new NonTerminal(GMacParseNodeNames.GMacDslRoot);
            var gmacDslItemsList = new NonTerminal(GMacParseNodeNames.GMacDslItemsList);
            var gmacDslItem = new NonTerminal(GMacParseNodeNames.GMacDslItem);

            var breakpoint = new NonTerminal(GMacParseNodeNames.Breakpoint);

            var Namespace = new NonTerminal(GMacParseNodeNames.Namespace);
            var openNamespace = new NonTerminal(GMacParseNodeNames.OpenNamespace);

            var frame = new NonTerminal(GMacParseNodeNames.Frame);
            var frameVectors = new NonTerminal(GMacParseNodeNames.FrameVectors);
            var frameSignature = new NonTerminal(GMacParseNodeNames.FrameSignature);
            var frameSignatureEuclidean = new NonTerminal(GMacParseNodeNames.FrameSignatureEuclidean);
            var frameSignatureIpm = new NonTerminal(GMacParseNodeNames.FrameSignatureIpm);
            var frameSignatureCbm = new NonTerminal(GMacParseNodeNames.FrameSignatureCbm);
            var frameSignatureOrthonormal = new NonTerminal(GMacParseNodeNames.FrameSignatureOrthonormal);
            var frameSignatureOrthogonal = new NonTerminal(GMacParseNodeNames.FrameSignatureOrthogonal);
            var frameSignatureReciprocal = new NonTerminal(GMacParseNodeNames.FrameSignatureReciprocal);

            var frameSubspaceList = new NonTerminal(GMacParseNodeNames.FrameSubspaceList);
            var frameSubspace = new NonTerminal(GMacParseNodeNames.FrameSubspace);

            var stringLiteral = new NonTerminal(GMacParseNodeNames.StringLiteral);
            var outerproductList = new NonTerminal(GMacParseNodeNames.OuterproductList);
            var identifierList = new NonTerminal(GMacParseNodeNames.IdentifierList);
            var qualifiedIdentifier = new NonTerminal(GMacParseNodeNames.QualifiedIdentifier);
            var qualifiedIdentifierList = new NonTerminal(GMacParseNodeNames.QualifiedIdentifierList);

            var basisBladesSet = new NonTerminal(GMacParseNodeNames.BasisBladesSet);
            var basisBladesSetList = new NonTerminal(GMacParseNodeNames.BasisBladesSetList);
            var basisBladesSetListItem = new NonTerminal(GMacParseNodeNames.BasisBladesSetListItem);
            var basisBladesSetListItemGaSpan = new NonTerminal(GMacParseNodeNames.BasisBladesSetListItemGaSpan);
            var qualifiedBasisBladesSet = new NonTerminal(GMacParseNodeNames.QualifiedBasisBladesSet);

            var dataMembersSet = new NonTerminal(GMacParseNodeNames.DataMembersSet);
            var qualifiedDataMembersSet = new NonTerminal(GMacParseNodeNames.QualifiedDataMembersSet);

            var basisBladeCoefficient = new NonTerminal(GMacParseNodeNames.BasisBladeCoefficient);
            var qualifiedBasisBladeCoefficient = new NonTerminal(GMacParseNodeNames.QualifiedBasisBladeCoefficient);

            var qualifiedItem = new NonTerminal(GMacParseNodeNames.QualifiedItem);

            var macro = new NonTerminal(GMacParseNodeNames.Macro);
            var macroTemplate = new NonTerminal(GMacParseNodeNames.MacroTemplate);
            var macroInputs = new NonTerminal(GMacParseNodeNames.MacroInputs);
            var identifierDeclaration = new NonTerminal(GMacParseNodeNames.IdentifierDeclaration);

            var templatesImplementation = new NonTerminal(GMacParseNodeNames.TemplatesImplementation);

            var expressionOpt = new NonTerminal(GMacParseNodeNames.ExpressionOpt);
            var expression = new NonTerminal(GMacParseNodeNames.Expression);
            var expressionSum = new NonTerminal(GMacParseNodeNames.ExpressionSum);
            var expressionProduct = new NonTerminal(GMacParseNodeNames.ExpressionProduct);
            var expressionAtomic = new NonTerminal(GMacParseNodeNames.ExpressionAtomic);
            var expressionBracketed = new NonTerminal(GMacParseNodeNames.ExpressionBracketed);
            var expressionScoped = new NonTerminal(GMacParseNodeNames.ExpressionScoped);

            var expressionFunction = new NonTerminal(GMacParseNodeNames.ExpressionFunction);
            var expressionFunctionDefaultValueOpt = new NonTerminal(GMacParseNodeNames.ExpressionFunctionDefaultValueOpt);
            var expressionFunctionDefaultValue = new NonTerminal(GMacParseNodeNames.ExpressionFunctionDefaultValue);
            var expressionFunctionInputsOpt = new NonTerminal(GMacParseNodeNames.ExpressionFunctionInputsOpt);
            var expressionFunctionInputs = new NonTerminal(GMacParseNodeNames.ExpressionFunctionInputs);
            var expressionFunctionInputsExpressions = new NonTerminal(GMacParseNodeNames.ExpressionFunctionInputsExpressions);
            var expressionFunctionInputsAssignments = new NonTerminal(GMacParseNodeNames.ExpressionFunctionInputsAssignments);
            var expressionFunctionInputsAssignmentsItem = new NonTerminal(GMacParseNodeNames.ExpressionFunctionInputsAssignmentsItem);
            var expressionFunctionInputsAssignmentsItemLhs = new NonTerminal(GMacParseNodeNames.ExpressionFunctionInputsAssignmentsItemLhs);

            var expressionComposite = new NonTerminal(GMacParseNodeNames.ExpressionComposite);

            var command = new NonTerminal(GMacParseNodeNames.Command);
            var commandLet = new NonTerminal(GMacParseNodeNames.CommandLet);
            var commandLetLhs = new NonTerminal(GMacParseNodeNames.CommandLetLhs);
            var commandDeclare = new NonTerminal(GMacParseNodeNames.CommandDeclare);
            var commandReturn = new NonTerminal(GMacParseNodeNames.CommandReturn);
            //var Command_Output = new NonTerminal(GMacParseNodeNames.Command_Output);
            var commandBlock = new NonTerminal(GMacParseNodeNames.CommandBlock);
            var commandBlockCommandsList = new NonTerminal(GMacParseNodeNames.CommandBlockCommandsList);
            
            var gaBinarySum = new NonTerminal(GMacParseNodeNames.GaBinarySum);
            //var gaUnaryOperation = new NonTerminal(GMacParseNodeNames.GaUnaryOperation);
            var gaBinaryProduct = new NonTerminal(GMacParseNodeNames.GaBinaryProduct);


            var constant = new NonTerminal(GMacParseNodeNames.Constant);

            var structure = new NonTerminal(GMacParseNodeNames.Structure);
            var structureMembers = new NonTerminal(GMacParseNodeNames.StructureMembers);

            var transform = new NonTerminal(GMacParseNodeNames.Transform);

#endregion



#region Rules

            switch (rootType)
            {
                case GMacGrammarRootType.Normal:
                    Root = gmacDslRoot;
                    break;

                case GMacGrammarRootType.Expression:
                    Root = expression;
                    break;

                case GMacGrammarRootType.QualifiedItem:
                    Root = qualifiedItem;
                    break;

                case GMacGrammarRootType.Macro:
                    Root = macro;
                    break;

                case GMacGrammarRootType.Structure:
                    Root = structure;
                    break;

                case GMacGrammarRootType.Commands:
                    Root = commandBlockCommandsList;
                    break;

                default:
                    Root = gmacDslRoot;
                    break;
            }

            gmacDslRoot.Rule =
                gmacDslItemsList;

            gmacDslItemsList.Rule =
                MakeStarRule(gmacDslItemsList, gmacDslItem);

            gmacDslItem.Rule =
                breakpoint
                | Namespace
                | openNamespace
                | frame
                | structure
                | constant
                | macro
                | transform
                | macroTemplate
                | templatesImplementation;


            breakpoint.Rule =
                keywordBreakpoint;


            Namespace.Rule =
                keywordNamespace + qualifiedIdentifier;

            openNamespace.Rule =
                keywordOpen + qualifiedIdentifier;


            frame.Rule =
                keywordFrame + identifier + punctLrb + frameVectors + punctRrb + frameSignature + frameSubspaceList;

            frameVectors.Rule =
                MakePlusRule(frameVectors, punctComma, identifier);

            frameSignature.Rule =
                frameSignatureEuclidean 
                | frameSignatureIpm 
                | frameSignatureCbm
                | frameSignatureOrthonormal
                | frameSignatureOrthogonal 
                | frameSignatureReciprocal;

            frameSignatureEuclidean.Rule =
                keywordEuclidean;

            frameSignatureIpm.Rule =
                keywordIpm + stringLiteral;

            frameSignatureCbm.Rule =
                keywordCbm + qualifiedIdentifier + stringLiteral;

            frameSignatureOrthonormal.Rule =
                keywordOrthonormal + stringLiteral;

            frameSignatureOrthogonal.Rule =
                keywordOrthogonal + stringLiteral;

            frameSignatureReciprocal.Rule =
                keywordReciprocal + qualifiedIdentifier;

            frameSubspaceList.Rule =
                MakeStarRule(frameSubspaceList, frameSubspace);

            frameSubspace.Rule =
                keywordSubspace + identifier + punctEqual + basisBladesSet;


            stringLiteral.Rule =
                dqString | sqString;

            outerproductList.Rule =
                MakePlusRule(outerproductList, gaOpCaret, identifier);

            identifierList.Rule =
                MakePlusRule(identifierList, punctComma, identifier);

            qualifiedIdentifier.Rule =
                MakePlusRule(qualifiedIdentifier, punctDot, identifier);

            qualifiedIdentifierList.Rule =
                MakePlusRule(qualifiedIdentifierList, punctComma, qualifiedIdentifier);


            basisBladesSet.Rule =
                punctAt + basisBladesSetList + punctAt;

            basisBladesSetList.Rule =
                MakePlusRule(basisBladesSetList, punctComma, basisBladesSetListItem);

            basisBladesSetListItem.Rule =
                outerproductList | basisBladesSetListItemGaSpan;

            basisBladesSetListItemGaSpan.Rule =
                keywordGa + punctLcb + identifierList + punctRcb;

            qualifiedBasisBladesSet.Rule =
                qualifiedIdentifier + punctDot + basisBladesSet;


            dataMembersSet.Rule =
                punctAt + identifierList + punctAt;

            qualifiedDataMembersSet.Rule =
                qualifiedIdentifier + punctDot + dataMembersSet;


            basisBladeCoefficient.Rule =
                punctHash + outerproductList + punctHash;

            qualifiedBasisBladeCoefficient.Rule =
                qualifiedIdentifier + punctDot + basisBladeCoefficient;


            qualifiedItem.Rule =
                qualifiedIdentifier
                | qualifiedBasisBladeCoefficient
                | qualifiedBasisBladesSet
                | qualifiedDataMembersSet;//TODO: Implement Qualified_DataMembersSet in the AST generators


            macroTemplate.Rule =
                keywordTemplate + macro;

            macro.Rule =
                keywordMacro + qualifiedIdentifier + punctLrb + macroInputs + punctRrb + punctColon + qualifiedIdentifier + commandBlock;

            macroInputs.Rule =
                MakeStarRule(macroInputs, punctComma, identifierDeclaration);

            identifierDeclaration.Rule =
                identifier + punctColon + qualifiedIdentifier;


            templatesImplementation.Rule =
                keywordImplement + qualifiedIdentifierList + keywordUsing + qualifiedIdentifierList;


            expressionOpt.Rule =
                Empty | expression;

            //TODO: This gaUnaryOperation + expressionSum is wrong and needs correction
            expression.Rule =
                expressionSum;// | gaUnaryOperation + expressionSum;

            expressionSum.Rule =
                expressionProduct | expressionSum + gaBinarySum + expressionProduct;

            expressionProduct.Rule =
                expressionAtomic | expressionProduct + gaBinaryProduct + expressionAtomic;

            expressionAtomic.Rule =
                constantNumber
                | qualifiedItem
                | expressionBracketed
                | expressionScoped
                | stringLiteral
                | expressionComposite
                | expressionFunction;

            expressionBracketed.Rule =
                punctLrb + expression + punctRrb;

            expressionScoped.Rule =
                qualifiedIdentifier + punctDot + punctAmp + expression + punctAmp;


            expressionFunction.Rule =
                qualifiedIdentifier + expressionFunctionDefaultValueOpt + punctLrb + expressionFunctionInputsOpt + punctRrb;

            expressionFunctionDefaultValueOpt.Rule =
                expressionFunctionDefaultValue | Empty;

            expressionFunctionDefaultValue.Rule =
                punctLcb + expression + punctRcb;

            expressionFunctionInputsOpt.Rule =
                Empty | expressionFunctionInputs;

            expressionFunctionInputs.Rule =
                expressionFunctionInputsExpressions | expressionFunctionInputsAssignments;

            expressionFunctionInputsExpressions.Rule =
                MakePlusRule(expressionFunctionInputsExpressions, punctComma, expression);

            expressionFunctionInputsAssignments.Rule =
                MakePlusRule(expressionFunctionInputsAssignments, punctComma, expressionFunctionInputsAssignmentsItem);

            expressionFunctionInputsAssignmentsItem.Rule =
                expressionFunctionInputsAssignmentsItemLhs + punctEqual + expression;

            expressionFunctionInputsAssignmentsItemLhs.Rule =
                qualifiedIdentifier | qualifiedBasisBladeCoefficient | basisBladeCoefficient;


            expressionComposite.Rule =
                punctLcb + keywordOutput + identifierDeclaration + commandBlockCommandsList + punctRcb;


            commandBlock.Rule =
                keywordBegin + commandBlockCommandsList + keywordEnd;

            commandBlockCommandsList.Rule =
                MakePlusRule(commandBlockCommandsList, command);

            command.Rule =
                commandDeclare
                | commandLet
                | commandReturn
                | commandBlock;
                //| Command_Output
                

            commandLet.Rule =
                keywordLet + commandLetLhs + punctEqual + expression;

            commandLetLhs.Rule =
                identifierDeclaration
                | qualifiedItem;

            commandDeclare.Rule =
                keywordDeclare + identifierDeclaration;

            commandReturn.Rule =
                keywordReturn + expression;


            //gaUnaryOperation.Rule =
            //    gaOpPlus
            //    | gaOpMinus;

            gaBinarySum.Rule =
                gaOpPlus 
                | gaOpMinus;

            gaBinaryProduct.Rule =
                gaOpTimes 
                | gaOpDivide 
                | gaOpCaret

                | gaOpOuterProduct
                
                | gaOpGeometricProduct
                | gaOpScalarProduct
                | gaOpLeftContractionProduct
                | gaOpRightContractionProduct
                | gaOpFatDotProduct
                | gaOpHestenesInnrProduct
                | gaOpCommutatorProduct
                | gaOpAntiCommutatorProduct
                
                | gaOpEuclideanGeometricProduct
                | gaOpEuclideanScalarProduct
                | gaOpEuclideanLeftContractionProduct
                | gaOpEuclideanRightContractionProduct
                | gaOpEuclideanFatDotProduct
                | gaOpEuclideanHestenesInnrProduct
                | gaOpEuclideanCommutatorProduct
                | gaOpEuclideanAntiCommutatorProduct;



            constant.Rule =
                keywordConstant + qualifiedIdentifier + punctEqual + expression;



            structure.Rule =
                keywordStructure + qualifiedIdentifier + punctLrb + structureMembers + punctRrb;

            structureMembers.Rule =
                MakePlusRule(structureMembers, punctComma, identifierDeclaration);



            transform.Rule =
                keywordTransform + identifier + keywordFrom + qualifiedIdentifier + keywordTo + qualifiedIdentifier + punctEqual;



#endregion

#region Configuration

            RegisterBracePair("[", "]");
            RegisterBracePair("(", ")");
            RegisterBracePair("{", "}");

            RegisterOperators(1, Associativity.Left,
                gaOpPlus, 
                gaOpMinus
                );

            RegisterOperators(2, Associativity.Left,
                gaOpTimes, 
                gaOpDivide, 
                gaOpCaret,
                gaOpGeometricProduct,
                gaOpLeftContractionProduct,
                gaOpRightContractionProduct,
                gaOpFatDotProduct,
                gaOpHestenesInnrProduct,
                gaOpCommutatorProduct,
                gaOpAntiCommutatorProduct,
                gaOpEuclideanGeometricProduct,
                gaOpEuclideanScalarProduct,
                gaOpEuclideanLeftContractionProduct,
                gaOpEuclideanRightContractionProduct,
                gaOpEuclideanFatDotProduct,
                gaOpEuclideanHestenesInnrProduct,
                gaOpEuclideanCommutatorProduct,
                gaOpEuclideanAntiCommutatorProduct
                );

            MarkPunctuation(
                keywordBreakpoint,
                keywordUsing,
                keywordBegin,
                keywordEnd,
                keywordDeclare,
                keywordCbm,
                keywordClass,
                keywordConstant,
                keywordEuclidean,
                keywordFrame,
                keywordGa,
                keywordIpm,
                keywordMacro,
                keywordNamespace,
                keywordOrthonormal,
                keywordOrthogonal,
                keywordReciprocal,
                keywordTransform,
                keywordLet,
                keywordOutput,
                keywordReturn,
                keywordStructure,
                keywordFrom,
                keywordTo,
                keywordOpen,
                keywordTemplate,
                keywordImplement,
                keywordSubspace,
                keywordOn,
                keywordAccess,
                keywordBind,
                keywordWith,
                keywordBinding,
                punctColon,
                punctDot,
                punctComma, 
                punctEqual, 
                punctHash, 
                punctAt,
                punctAmp,
                punctLcb, 
                punctRcb,
                punctLrb, 
                punctRrb,
                punctLsb, 
                punctRsb,
                punctLab,
                punctRab
                );

            //this.MarkTransient(
            //    DSL_Definition_Item,
            //    Frame_Classes_List_opt,
            //    Frame_Class_Constants_opt,
            //    Macro_Inputs_opt,
            //    Macro_Input_Type_opt,
            //    Multivector_Expression_Atomic
            //    );

#endregion
        }
    }
}
