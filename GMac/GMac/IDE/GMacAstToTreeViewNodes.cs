using System.Windows.Forms;
using CodeComposerLib.Irony.Semantic;
using CodeComposerLib.Irony.Semantic.Command;
using CodeComposerLib.Irony.Semantic.Expression;
using CodeComposerLib.Irony.Semantic.Expression.Basic;
using CodeComposerLib.Irony.Semantic.Expression.Value;
using CodeComposerLib.Irony.Semantic.Expression.ValueAccess;
using CodeComposerLib.Irony.Semantic.Symbol;
using DataStructuresLib;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;
using GMac.Engine.Compiler.Semantic.AST;
using GMac.Engine.Compiler.Semantic.AST.Extensions;
using Microsoft.CSharp.RuntimeBinder;

namespace GMac.IDE
{
    public sealed class GMacAstToTreeViewNodes : IAstNodeDynamicVisitor<TreeNode>
    {
        public static TreeNode Convert(IIronyAstObject item)
        {
            var converter = new GMacAstToTreeViewNodes();

            return item.AcceptVisitor(converter);
        }


        public bool ShowFrames = true;

        public bool ShowFrameBasisVectors = true;

        public bool ShowFrameMultivectorType = true;

        public bool ShowFrameSubspaces = true;

        public bool ShowConstants = true;

        public bool ShowStructures = true;

        public bool ShowStructureMembers = true;

        public bool ShowMacros = true;

        public bool ShowMacroParameters = true;

        public bool ShowMacroTemplates = true;

        public bool ShowCommandsAndExpressions = true;

        public bool ShowTransforms = true;


        public bool IgnoreNullElements => false;

        public bool UseExceptions => false;


        public TreeNode Visit(ValuePrimitive<int> value)
        {
            var node = new TreeNode("<INT_VALUE> " + value) {Tag = value};

            return node;
        }

        public TreeNode Visit(ValuePrimitive<double> value)
        {
            var node = new TreeNode("<DOUBLE_VALUE> " + value) {Tag = value};

            return node;
        }

        public TreeNode Visit(ValuePrimitive<bool> value)
        {
            var node = new TreeNode("<BOOL_VALUE> " + value) {Tag = value};

            return node;
        }

        public TreeNode Visit(ValuePrimitive<MathematicaScalar> value)
        {
            var node = new TreeNode("<SCALAR_VALUE> " + value) {Tag = value};

            return node;
        }

        private TreeNode Visit(MathematicaScalar value)
        {
            var node = new TreeNode("<SCALAR_VALUE> " + value) {Tag = value};

            return node;
        }

        private TreeNode Visit(GMacFrameMultivector mvType, int lhsId, MathematicaScalar rhsScalar)
        {
            var lhsName = "#" + mvType.ParentFrame.BasisBladeName(lhsId) + "#";

            var node = new TreeNode("<ASSIGN> " + lhsName) {Tag = lhsId};

            node.Nodes.Add(Visit(rhsScalar));

            return node;
        }

        private TreeNode Visit(string lhsName, ILanguageValue rhsValue)
        {
            var node = new TreeNode("<ASSIGN> " + lhsName) {Tag = lhsName};

            node.Nodes.Add(rhsValue.AcceptVisitor(this));

            return node;
        }

        public TreeNode Visit(GMacValueMultivector value)
        {
            var node = new TreeNode("<MULTIVECTOR_VALUE> " + value.ValueMultivectorType.TypeSignature) {Tag = value};

            foreach (var pair in value.SymbolicMultivector.Terms)
                node.Nodes.Add(Visit(value.ValueMultivectorType, (int)pair.Key, pair.Value));

            return node;
        }

        public TreeNode Visit(ValueStructureSparse value)
        {
            var node = new TreeNode("<STRUCTURE_VALUE> " + value.ValueStructureType.TypeSignature) {Tag = value};

            foreach (var pair in value)
                node.Nodes.Add(Visit(pair.Key, pair.Value));

            return node;
        }

        public TreeNode Visit(LanguageValueAccess valueAccess)
        {
            var node = new TreeNode("<ACCESS> " + valueAccess.GetName()) {Tag = valueAccess};

            return node;
        }

        private TreeNode Visit(GMacFrameMultivector mvType, int lhsId, ILanguageExpression rhsExpr)
        {
            var lhsName = "#" + mvType.ParentFrame.BasisBladeName(lhsId) + "#";

            var node = new TreeNode("<ASSIGN> " + lhsName) {Tag = lhsId};

            node.Nodes.Add(rhsExpr.AcceptVisitor(this));

            return node;
        }

        private TreeNode Visit(string lhsName, ILanguageExpression rhsExpr)
        {
            var node = new TreeNode("<ASSIGN> " + lhsName) {Tag = lhsName};

            node.Nodes.Add(rhsExpr.AcceptVisitor(this));

            return node;
        }

        private TreeNode Visit_SymbolicExpression(BasicPolyadic expr)
        {
            var symbolicExpr = (GMacParametricSymbolicExpression)expr.Operator;
            var operands = expr.Operands.AsByName;

            var node = new TreeNode("<CALL>" + symbolicExpr.OperatorName) {Tag = expr};

            foreach (var pair in operands.OperandsDictionary)
                node.Nodes.Add(Visit(pair.Key, pair.Value));

            return node;
        }

        private TreeNode Visit_MultivectorConstruction(BasicPolyadic expr)
        {
            var mvTypeCons = (GMacFrameMultivectorConstructor)expr.Operator;
            var operands = expr.Operands.AsByIndex;

            var node = new TreeNode("<CONSTRUCT>" + mvTypeCons.MultivectorType.SymbolAccessName) {Tag = expr};

            if (mvTypeCons.HasDefaultValueSource)
            {
                var subNode = node.Nodes.Add("<DEFAULT>");
                subNode.Nodes.Add(mvTypeCons.DefaultValueSource.AcceptVisitor(this));
            }

            foreach (var pair in operands.OperandsDictionary)
                node.Nodes.Add(Visit(mvTypeCons.MultivectorType, (int)pair.Key, pair.Value));

            return node;
        }

        private TreeNode Visit_StructureConstruction(BasicPolyadic expr)
        {
            var structureCons = (GMacStructureConstructor)expr.Operator;
            var operands = expr.Operands.AsByValueAccess;

            var node = new TreeNode("<CONSTRUCT>" + structureCons.Structure.SymbolAccessName) {Tag = expr};

            if (structureCons.HasDefaultValueSource)
            {
                var subNode = node.Nodes.Add("<DEFAULT>");
                subNode.Nodes.Add(structureCons.DefaultValueSource.AcceptVisitor(this));
            }

            foreach (var command in operands.AssignmentsList)
                node.Nodes.Add(command.AcceptVisitor(this));

            return node;
        }

        private TreeNode Visit_MacroCall(BasicPolyadic expr)
        {
            var macro = (GMacMacro)expr.Operator;
            var operands = expr.Operands.AsByValueAccess;

            var node = new TreeNode("<CALL>" + macro.SymbolAccessName) {Tag = expr};

            foreach (var command in operands.AssignmentsList)
                node.Nodes.Add(command.AcceptVisitor(this));

            return node;
        }

        public TreeNode Visit(BasicPolyadic expr)
        {
            if (expr.Operator is GMacMacro)
                return Visit_MacroCall(expr);

            if (expr.Operator is GMacStructureConstructor)
                return Visit_StructureConstruction(expr);

            if (expr.Operator is GMacFrameMultivectorConstructor)
                return Visit_MultivectorConstruction(expr);

            if (expr.Operator is GMacParametricSymbolicExpression)
                return Visit_SymbolicExpression(expr);

            return null;
        }

        public TreeNode Visit(BasicBinary expr)
        {
            var node = new TreeNode("<BINARY>" + expr.Operator.OperatorName) {Tag = expr};

            node.Nodes.Add(expr.Operand1.AcceptVisitor(this));

            node.Nodes.Add(expr.Operand2.AcceptVisitor(this));

            return node;
        }

        public TreeNode Visit(BasicUnary expr)
        {
            var node = new TreeNode("<UNARY>" + expr.Operator.OperatorName) {Tag = expr};

            node.Nodes.Add(expr.Operand.AcceptVisitor(this));

            return node;
        }

        public TreeNode Visit(CompositeExpression expr)
        {
            var node = new TreeNode("<COMPOSITE> Output " + expr.OutputVariable.ObjectName) {Tag = expr};

            foreach (var item in expr.Commands)
                node.Nodes.Add(item.AcceptVisitor(this));

            return node;
        }

        public TreeNode Visit(CommandDeclareVariable command)
        {
            var nodeName = "<DECLARE> " + command.DataStore.ObjectName + " : " + command.DataStore.SymbolTypeSignature;

            var node = new TreeNode(nodeName) { Tag = command };

            return node;
        }

        public TreeNode Visit(OperandsByValueAccessAssignment assignment)
        {
            var node = new TreeNode("<OP_ASSIGN> " + assignment.LhsValueAccess.GetName()) {Tag = assignment};

            node.Nodes.Add(assignment.RhsExpression.AcceptVisitor(this));

            return node;
        }

        public TreeNode Visit(CommandAssign command)
        {
            var node = new TreeNode("<ASSIGN> " + command.LhsValueAccess.GetName()) {Tag = command};

            node.Nodes.Add(command.RhsExpression.AcceptVisitor(this));

            return node;
        }

        public TreeNode Visit(CommandBlock command)
        {
            var node = new TreeNode("<BLOCK>") {Tag = command};

            foreach (var item in command.Commands)
                node.Nodes.Add(item.AcceptVisitor(this));

            return node;
        }

        public TreeNode Visit(SymbolStructureDataMember dataMember)
        {
            var node = new TreeNode("<MEMBER> " + dataMember.ObjectName + " : " + dataMember.SymbolTypeSignature)
            {
                Tag = dataMember
            };

            return node;
        }

        public TreeNode Visit(GMacStructure structure)
        {
            var node = new TreeNode("<STRUCTURE> " + structure.ObjectName) {Tag = structure};

            if (!ShowStructureMembers) 
                return node;

            foreach (var childSymbol in structure.DataMembers)
                node.Nodes.Add(Visit(childSymbol));

            return node;
        }

        public TreeNode Visit(GMacMacroTemplate macroTemplate)
        {
            var node = new TreeNode("<MACRO_TEMPLATE> " + macroTemplate.ObjectName) {Tag = macroTemplate};

            return node;
        }

        public TreeNode Visit(SymbolProcedureParameter param)
        {
            var node = new TreeNode("<PARAMETER> " + param.ObjectName + " : " + param.GetParameterTypeSignature())
            {
                Tag = param
            };

            return node;
        }

        public TreeNode Visit(GMacMultivectorTransform transform)
        {
            var node = new TreeNode("<TRANSFORM> " + transform.ObjectName) {Tag = transform};

            var subNode = new TreeNode("<FROM> " + transform.SourceFrame.SymbolAccessName) { Tag = transform.SourceFrame };
            node.Nodes.Add(subNode);

            subNode = new TreeNode("<TO> " + transform.TargetFrame.SymbolAccessName) { Tag = transform.TargetFrame };
            node.Nodes.Add(subNode);

            return node;
        }

        public TreeNode Visit(GMacMacro macro)
        {
            var node = new TreeNode("<MACRO> " + macro.ObjectName) {Tag = macro};

            if (ShowMacroParameters)
                foreach (var childSymbol in macro.Parameters)
                    node.Nodes.Add(Visit(childSymbol));

            if (ShowCommandsAndExpressions)
            {
                node.Nodes.Add(Visit(macro.SymbolBody));
            }

            return node;
        }

        public TreeNode Visit(GMacConstant constant)
        {
            var node = new TreeNode("<CONSTANT> " + constant.ObjectName) {Tag = constant};

            node.Nodes.Add(constant.AssociatedValue.AcceptVisitor(this));

            return node;
        }

        public TreeNode Visit(GMacFrameBasisVector basisVector)
        {
            var node = new TreeNode("<BASIS_VECTOR> " + basisVector.ObjectName) {Tag = basisVector};

            return node;
        }

        public TreeNode Visit(GMacFrameMultivector mvType)
        {
            var node = new TreeNode("<MULTIVECTOR_TYPE> " + mvType.ObjectName) {Tag = mvType};

            return node;
        }

        public TreeNode Visit(GMacFrameSubspace subspace)
        {
            var node = new TreeNode("<SUBSPACE> " + subspace.ObjectName) {Tag = subspace};

            return node;
        }

        public TreeNode Visit(GMacFrame frame)
        {
            var node = new TreeNode("<FRAME> " + frame.ObjectName) {Tag = frame};

            if (ShowFrameBasisVectors)
                foreach (var childSymbol in frame.FrameBasisVectors)
                    node.Nodes.Add(Visit(childSymbol));

            if (ShowFrameMultivectorType)
                node.Nodes.Add(Visit(frame.MultivectorType));

            if (ShowFrameSubspaces)
                foreach (var childSymbol in frame.FrameSubspaces)
                    node.Nodes.Add(Visit(childSymbol));

            if (ShowConstants)
                foreach (var childSymbol in frame.ChildConstants)
                    node.Nodes.Add(Visit(childSymbol));

            if (!ShowMacros) 
                return node;

            foreach (var childSymbol in frame.ChildMacros)
                node.Nodes.Add(Visit(childSymbol));

            return node;
        }

        public TreeNode Visit(GMacNamespace nameSpace)
        {
            var node = new TreeNode("<NAMESPACE> " + nameSpace.ObjectName) {Tag = nameSpace};

            foreach (var childSymbol in nameSpace.ChildNamespaces)
                node.Nodes.Add(Visit(childSymbol));

            if (ShowFrames)
                foreach (var childSymbol in nameSpace.ChildFrames)
                    node.Nodes.Add(Visit(childSymbol));

            if (ShowConstants)
                foreach (var childSymbol in nameSpace.ChildConstants)
                    node.Nodes.Add(Visit(childSymbol));

            if (ShowStructures)
                foreach (var childSymbol in nameSpace.ChildStructures)
                    node.Nodes.Add(Visit(childSymbol));

            if (ShowTransforms)
                foreach (var childSymbol in nameSpace.ChildTransforms)
                    node.Nodes.Add(Visit(childSymbol));

            if (ShowMacros)
                foreach (var childSymbol in nameSpace.ChildMacros)
                    node.Nodes.Add(Visit(childSymbol));

            if (!ShowMacroTemplates) 
                return node;

            foreach (var childSymbol in nameSpace.ChildMacroTemplates)
                node.Nodes.Add(Visit(childSymbol));

            return node;
        }

        public TreeNode Visit(GMacAst dsl)
        {
            var node = new TreeNode("<DSL> GMac") {Tag = dsl};

            foreach (var childSymbol in dsl.ChildNamespaces)
                node.Nodes.Add(Visit(childSymbol));

            return node;
        }


        public TreeNode Fallback(IIronyAstObject objItem, RuntimeBinderException excException)
        {
            return new TreeNode("Unrecognized Node!");
        }
    }
}
