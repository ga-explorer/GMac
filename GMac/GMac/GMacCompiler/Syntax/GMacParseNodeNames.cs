namespace GMac.GMacCompiler.Syntax
{
    /// <summary>
    /// An enumeration of all relevant GMac parse node names for use during grammar creation and symbol translation
    /// </summary>
    internal static class GMacParseNodeNames
    {
        public const string MultiLineComment = "MultiLineComment";

        public const string SingleLineComment = "SingleLineComment";

        public const string DqString = "DQString";

        public const string SqString = "SQString";

        public const string Identifier = "Identifier";

        public const string ConstantNumber = "ConstantNumber";

        public const string GMacDslRoot = "GMacDSLRoot";

        public const string GMacDslItemsList = "GMacDSL_Items_List";

        public const string GMacDslItem = "GMacDSL_Item";

        public const string Breakpoint = "Breakpoint";

        public const string Namespace = "Namespace";

        public const string OpenNamespace = "OpenNamespace";

        public const string Frame = "Frame";

        public const string FrameVectors = "Frame_Vectors";

        public const string FrameSignature = "Frame_Signature";

        public const string FrameSignatureEuclidean = "Frame_Signature_Euclidean";

        public const string FrameSignatureIpm = "Frame_Signature_IPM";

        public const string FrameSignatureCbm = "Frame_Signature_CBM";

        public const string FrameSignatureOrthonormal = "Frame_Signature_Orthonormal";

        public const string FrameSignatureOrthogonal = "Frame_Signature_Orthogonal";

        public const string FrameSignatureReciprocal = "Frame_Signature_Reciprocal";

        public const string FrameSubspaceList = "Frame_Subspace_List";

        public const string FrameSubspace = "Frame_Subspace";

        public const string StringLiteral = "StringLiteral";

        public const string OuterproductList = "Outerproduct_List";

        public const string IdentifierList = "Identifier_List";

        public const string QualifiedIdentifier = "Qualified_Identifier";

        public const string QualifiedIdentifierList = "Qualified_Identifier_List";

        public const string BasisBladesSet = "BasisBladesSet";

        public const string BasisBladesSetList = "BasisBladesSet_List";

        public const string BasisBladesSetListItem = "BasisBladesSet_List_Item";

        public const string BasisBladesSetListItemGaSpan = "BasisBladesSet_List_Item_GASpan";

        public const string QualifiedBasisBladesSet = "Qualified_BasisBladesSet";

        public const string DataMembersSet = "DataMembersSet";

        public const string QualifiedDataMembersSet = "Qualified_DataMembersSet";

        public const string BasisBladeCoefficient = "BasisBladeCoefficient";

        public const string QualifiedBasisBladeCoefficient = "Qualified_BasisBladeCoefficient";

        public const string QualifiedItem = "Qualified_Item";

        public const string Macro = "Macro";

        public const string MacroTemplate = "MacroTemplate";

        public const string MacroInputs = "Macro_Inputs";

        public const string IdentifierDeclaration = "Identifier_Declaration";

        public const string TemplatesImplementation = "TemplatesImplementation";

        public const string ExpressionOpt = "Expression_opt";

        public const string Expression = "Expression";

        public const string ExpressionSum = "Expression_Sum";

        public const string ExpressionProduct = "Expression_Product";

        public const string ExpressionAtomic = "Expression_Atomic";

        public const string ExpressionBracketed = "Expression_Bracketed";

        public const string ExpressionScoped = "Expression_Scoped";

        public const string ExpressionFunction = "Expression_Function";

        public const string ExpressionFunctionDefaultValueOpt = "Expression_Function_Default_Value_opt";

        public const string ExpressionFunctionDefaultValue = "Expression_Function_Default_Value";

        public const string ExpressionFunctionInputsOpt = "Expression_Function_Inputs_opt";

        public const string ExpressionFunctionInputs = "Expression_Function_Inputs";

        public const string ExpressionFunctionInputsExpressions = "Expression_Function_Inputs_Expressions";

        public const string ExpressionFunctionInputsAssignments = "Expression_Function_Inputs_Assignments";

        public const string ExpressionFunctionInputsAssignmentsItem = "Expression_Function_Inputs_Assignments_Item";

        public const string ExpressionFunctionInputsAssignmentsItemLhs = "Expression_Function_Inputs_Assignments_Item_LHS";

        public const string ExpressionComposite = "Expression_Composite";

        public const string Command = "Command";

        public const string CommandLet = "Command_Let";

        public const string CommandLetLhs = "Command_Let_LHS";

        public const string CommandDeclare = "Command_Declare";

        public const string CommandReturn = "Command_Return";

        public const string CommandBlock = "Command_Block";

        public const string CommandBlockCommandsList = "Command_Block_Commands_List";

        public const string GaBinarySum = "GA_Binary_Sum";

        public const string GaUnaryOperation = "GA_Unary_Operation";

        public const string GaBinaryProduct = "GA_Binary_Product";

        public const string Constant = "Constant";

        public const string Structure = "Structure";

        public const string StructureMembers = "Structure_Members";

        public const string Transform = "Transform";

        //public const string AccessScheme = "AccessScheme";

        //public const string AccessSchemeCommands = "AccessSchemeCommands";

        //public const string Command_Access = "Command_Access";

        //public const string Binding = "Binding";

        //public const string BindingCommands = "BindingCommands";

        //public const string Command_Bind = "Command_Bind";

        //public const string Command_Bind_Target = "Command_Bind_Target";

        //public const string Command_Bind_Target_Constant = "Command_Bind_Target_Constant";

        //public const string Command_Bind_Target_Access_Scheme = "Command_Bind_Target_Access_Scheme";

        //public const string Command_Bind_Target_Access_Template = "Command_Bind_Target_Access_Template";
    }
}
