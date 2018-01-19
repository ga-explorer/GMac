using System.Globalization;
using System.Linq;
using GMac.GMacCompiler.Semantic.AST;
using GMac.GMacCompiler.Semantic.AST.Extensions;
using IronyGrammars.DSLDebug;
using IronyGrammars.Semantic;
using IronyGrammars.Semantic.Command;
using IronyGrammars.Semantic.Expression;
using IronyGrammars.Semantic.Expression.Basic;
using IronyGrammars.Semantic.Expression.Value;
using IronyGrammars.Semantic.Expression.ValueAccess;
using IronyGrammars.Semantic.Operator;
using SymbolicInterface.Mathematica.Expression;
using TextComposerLib;

namespace GMac.GMacCompiler.Semantic.ASTDebug
{
    public sealed class GMacAstDescription : IronyAstDescription
    {
        public GMacAstDescription(bool addLineNumbering = false)
        {
            if (addLineNumbering)
                Log.AddLineCountHeader();
        }


        //    public static Generate(problem_domain : GMacSymbolTable) =
        //        var code_gen = new CodeGenerator(problem_domain)
        //
        //        code_gen.Generate_SymbolTable()



        public void Visit(ValuePrimitive<MathematicaScalar> value)
        {
            Log.Append("'");
            Log.Append(value.Value.ExpressionText);
            Log.Append("'");
        }

        public void Visit(ValuePrimitive<bool> value)
        {
            Log.Append(value.Value.ToString());
        }

        public void Visit(ValuePrimitive<double> value)
        {
            Log.Append(value.Value.ToString("G"));
        }

        public void Visit(ValuePrimitive<int> value)
        {
            Log.Append(value.Value.ToString());
        }

        public void Visit(ValueStructureSparse value)
        {
            Log.Append(value.ValueStructureType.SymbolAccessName);
            Log.Append("(");

            var flag = false;
            foreach (var pair in value)
            {
                if (flag)
                    Log.Append(", ");
                else
                    flag = true;

                Log.Append(pair.Key);
                Log.Append(" = ");
                pair.Value.AcceptVisitor(this);
            }

            Log.Append(")");
        }

        public void Visit(GMacValueMultivector value)
        {
            Log.Append(value.ValueMultivectorType.SymbolAccessName);
            Log.Append("(");

            var flag = false;
            foreach (var pair in value.MultivectorCoefficients)
            {
                if (flag)
                    Log.Append(", ");
                else
                    flag = true;

                Log.Append("#");
                Log.Append(value.MultivectorFrame.BasisBladeName(pair.Key));
                Log.Append("#");
                Log.Append(" = ");
                Log.Append("'");
                Log.Append(pair.Value.ExpressionText);
                Log.Append("'");
            }

            Log.Append(")");
        }

        public void Generate_ValueAccessStep(ValueAccessStep parentStep, ValueAccessStep accessStep)
        {
            if (!accessStep.IsFirstComponent)
                Log.Append(".");

            Log.Append(accessStep.GetName(parentStep));
        }

        public override void Visit(LanguageValueAccess valAccess)
        {
            ValueAccessStep parentStep = null;

            foreach (var compAccess in valAccess.AccessSteps)
            {
                Generate_ValueAccessStep(parentStep, compAccess);

                parentStep = compAccess;
            }
        }

        public void Visit(BasicUnary expr)
        {
            //var opPrimitive = expr.Operator as OperatorPrimitive;
            //if (opPrimitive != null)
            //    Log.Append(opPrimitive.OperatorSymbolString);
            //else
            //    Log.Append(expr.Operator.OperatorName);

            Log.Append(expr.Operator.OperatorName);

            Log.Append("(");

            expr.Operand.AcceptVisitor(this);

            Log.Append(")");
        }

        public void Visit(BasicBinary expr)
        {
            var opPrimitive = expr.Operator as OperatorPrimitive;
            if (opPrimitive != null)
            {
                Log.Append("(");

                expr.Operand1.AcceptVisitor(this);

                Log.Append(" ")
                    .Append(opPrimitive.OperatorSymbolString)
                    .Append(" ");

                expr.Operand2.AcceptVisitor(this);

                Log.Append(")");

                return;
            }

            Log.Append(expr.Operator.OperatorName);

            Log.Append("(");

            expr.Operand1.AcceptVisitor(this);
            
            Log.Append(", ");

            expr.Operand2.AcceptVisitor(this);

            Log.Append(")");
        }

        internal void Generate_BasicPolyadic_MacroCall(GMacMacro macro, OperandsByValueAccess operands)
        {
            //this.Log.Append(macro.OperatorName);
            Log.Append("(");

            var flag = false;
            foreach (var operand in operands.AssignmentsList)
            {
                if (flag) Log.Append(", "); else flag = true;

                operand.LhsValueAccess.AcceptVisitor(this);

                Log.Append(" : ");

                Log.Append(operand.LhsValueAccess.ExpressionType.TypeSignature);

                Log.Append(" = ");
                    
                operand.RhsExpression.AcceptVisitor(this);
            }

            Log.Append(")");
        }

        internal void Generate_BasicPolyadic_FrameMultivectorConstruction(GMacFrameMultivectorConstructor mvTypeCons, OperandsByIndex operands)
        {
            //this.Log.Append(mv_type_cons.OperatorName);
            Log.Append("{");

            if (mvTypeCons.HasDefaultValueSource)
                mvTypeCons.DefaultValueSource.AcceptVisitor(this);

            Log.Append("}");
            Log.Append("(");

            var flag = false;
            foreach (var pair in operands.OperandsDictionary)
            {
                if (flag) Log.Append(", "); else flag = true;

                Log.Append("#");
                Log.Append(mvTypeCons.MultivectorType.ParentFrame.BasisBladeName(pair.Key));
                Log.Append("#");

                Log.Append(" = ");
                    
                pair.Value.AcceptVisitor(this);
            }

            Log.Append(")");
        }

        internal void Generate_BasicPolyadic_StructureConstruction(GMacStructureConstructor structureCons, OperandsByValueAccess operands)
        {
            Log.Append("{");

            if (structureCons.HasDefaultValueSource)
                structureCons.DefaultValueSource.AcceptVisitor(this);

            Log.Append("}");
            Log.Append("(");

            var flag = false;
            foreach (var operand in operands.AssignmentsList)
            {
                if (flag) Log.Append(", "); else flag = true;

                operand.LhsValueAccess.AcceptVisitor(this);

                Log.Append(" : ");

                Log.Append(operand.LhsValueAccess.ExpressionType.TypeSignature);

                Log.Append(" = ");
                    
                operand.RhsExpression.AcceptVisitor(this);
            }

            Log.Append(")");
        }

        internal void Generate_BasicPolyadic_CASExpression(GMacParametricSymbolicExpression casExpr, OperandsByName operands)
        {
            Log.Append("(");

            var flag = false;
            foreach (var pair in operands.OperandsDictionary)
            {
                if (flag) Log.Append(", "); else flag = true;

                Log.Append(pair.Key);

                Log.Append(" = ");
                    
                pair.Value.AcceptVisitor(this);
            }

            Log.Append(")");
        }

        public void Visit(BasicPolyadic expr)
        {
            var gmacMacro = expr.Operator as GMacMacro;
            if (gmacMacro != null)
            {
                Log.Append(gmacMacro.SymbolAccessName);

                Generate_BasicPolyadic_MacroCall(gmacMacro, expr.Operands.AsByValueAccess);
                return;
            }

            Log.Append(expr.Operator.OperatorName);

            var multivectorConstructor = expr.Operator as GMacFrameMultivectorConstructor;
            if (multivectorConstructor != null)
            {
                Generate_BasicPolyadic_FrameMultivectorConstruction(multivectorConstructor, expr.Operands.AsByIndex);
                return;
            }

            var structureConstructor = expr.Operator as GMacStructureConstructor;
            if (structureConstructor != null)
            {
                Generate_BasicPolyadic_StructureConstruction(structureConstructor, expr.Operands.AsByValueAccess);
                return;
            }

            var symbolicExpression = expr.Operator as GMacParametricSymbolicExpression;
            if (symbolicExpression != null)
            {
                var casExpr = symbolicExpression;
                Generate_BasicPolyadic_CASExpression(casExpr, expr.Operands.AsByName);
                return;
            }

            Log.Append("<unknown polyadic operator>");
        }

        //    public this.Generate_LanguageBasicExpression_BasicUnaryTransform(expr : BasicUnaryTransform) =
        //        this.Log.Append(expr.Transform.SymbolQualifiedName)
        //        this.Log.Append("[")
        //        this.Generate_LanguageAtomicExpression(expr.Operand);
        //        this.Log.Append("]")

        //        this.Log

        public void Visit(CompositeExpression expr)
        {
            Log.AppendAtNewLine("begin");

            Log.IncreaseIndentation();

            Log.AppendAtNewLine("output ");
            Log.Append(expr.OutputVariable.ObjectName);
            Log.Append(" : ");
            Log.AppendLine(expr.OutputVariable.SymbolTypeSignature);

            foreach (var item in expr.Commands)
                item.AcceptVisitor(this);

            Log.DecreaseIndentation();

            Log.AppendLineAtNewLine("end");
        }

        public void Visit(OperandsByValueAccessAssignment command)
        {
            Log.AppendAtNewLine("param assign ");

            command.LhsValueAccess.AcceptVisitor(this);

            Log.Append(" : ");

            Log.Append(command.LhsValueAccess.ExpressionType.TypeSignature);

            Log.Append(" = ");

            command.RhsExpression.AcceptVisitor(this);

            Log.AppendAtNewLine();
        }

        public void Visit(CommandAssign command)
        {
            Log.AppendAtNewLine("let ");

            command.LhsValueAccess.AcceptVisitor(this);
            
            Log.Append(" : ");

            Log.Append(command.LhsValueAccess.ExpressionType.TypeSignature);

            Log.Append(" = ");

            command.RhsExpression.AcceptVisitor(this);

            Log.AppendAtNewLine();
        }

        public void Visit(CommandComment command)
        {
            Log.Append(command.CommentText);
        }

        public void Visit(CommandDeclareVariable command)
        {
            Log.AppendAtNewLine("declare ");

            Log.Append(command.DataStore.ObjectName);

            Log.Append(" : ");

            Log.AppendLine(command.DataStore.SymbolTypeSignature);
        }

        public void Visit(CommandBlock command)
        {
            Log.AppendAtNewLine("begin");

            Log.IncreaseIndentation();

            foreach (var item in command.Commands)
                item.AcceptVisitor(this);

            Log.DecreaseIndentation();

            Log.AppendLineAtNewLine("end");
        }

        private void Generate_Macro_UsingBody(GMacMacro macro, CommandBlock macroBody)
        {
            Log.AppendAtNewLine("macro ");
            Log.Append(macro.SymbolAccessName);
            Log.AppendLine("(");

            Log.IncreaseIndentation();

            var flag = false;
            foreach (var parameter in macro.Parameters)
            {
                if (flag) Log.Append(", "); else flag = true;

                if (parameter.DirectionOut)
                    Log.AppendAtNewLine("out " + parameter.ObjectName);
                else
                    Log.AppendAtNewLine(parameter.ObjectName);

                Log.Append(" : ");
                Log.Append(parameter.SymbolTypeSignature);
            }

            Log.DecreaseIndentation();

            Log.AppendAtNewLine(")");

            macroBody.AcceptVisitor(this);

            Log.AppendLineAtNewLine();
        }

        internal void Generate_Macro_UsingRawBody(GMacMacro macro)
        {
            Generate_Macro_UsingBody(macro, macro.SymbolBody);
        }

        internal void Generate_Macro_UsingRawCompiledBody(GMacMacro macro)
        {
            Generate_Macro_UsingBody(macro, macro.CompiledBody);
        }

        internal void Generate_Macro_UsingOptimizedCompiledBody(GMacMacro macro)
        {
            Generate_Macro_UsingBody(macro, macro.OptimizedCompiledBody);
        }

        public void Visit(GMacMacro macro)
        {
            Generate_Macro_UsingBody(macro, macro.SymbolBody);

            //macro.CompiledBody.AcceptVisitor(this);

            //macro.OptimizedCompiledBody.AcceptVisitor(this);

            //var gen = new LowLevelGenerator(macro);
            //gen.DefineAllParameters();
            //gen.GenerateLowLevelItems();
            //Log.AppendAtNewLine(gen.ToString());

            //var genOpt = new LowLevelOptimizer(gen);
            //genOpt.OptimizeLowLevelItems();
            //Log.AppendAtNewLine(genOpt.ToString());

            ////LowLevelGeneratorTester gen_test = new LowLevelGeneratorTester(macro);
            ////this.Log.AppendAtNewLine(gen_test.TestGenerationByConstantInputs());

            Log.AppendLineAtNewLine();
        }

        public void Visit(GMacMacroTemplate template)
        {
            Log.AppendAtNewLine("template macro ");
            Log.Append(template.SymbolAccessName);

            Log.AppendLineAtNewLine();
        }

        public void Visit(GMacMultivectorTransform transform)
        {
            Log.AppendAtNewLine("transform ");
            Log.Append(transform.SymbolAccessName);
            Log.Append(" from ");
            Log.Append(transform.SourceFrame.SymbolAccessName);
            Log.Append(" to ");
            Log.Append(transform.TargetFrame.SymbolAccessName);

            Log.AppendLineAtNewLine();
        }

        public void Visit(GMacStructure structure)
        {
            Log.AppendAtNewLine("structure ");
            Log.Append(structure.SymbolAccessName);
            Log.AppendLine("(");

            Log.IncreaseIndentation();

            var flag = false;
            foreach (var dataMember in structure.DataMembers)
            {
                if (flag) Log.Append(", "); else flag = true;

                Log.AppendAtNewLine(dataMember.ObjectName);
                Log.Append(" : ");
                Log.Append(dataMember.SymbolTypeSignature);
            }

            Log.DecreaseIndentation();

            Log.AppendAtNewLine(")");

            foreach (var itam in structure.Macros)
                itam.AcceptVisitor(this);

            Log.AppendLineAtNewLine();
        }

        public void Visit(GMacFrameMultivector mvType)
        {
            Log.AppendAtNewLine("frame multivector type ");

            Log.AppendLine(mvType.SymbolAccessName);
        }

        public void Visit(GMacFrameBasisVector basisVector)
        {
            Log.AppendAtNewLine("basis vector ");
            Log.Append(basisVector.SymbolAccessName);
            Log.Append(" = ");

            basisVector.AssociatedValue.AcceptVisitor(this);

            Log.AppendLineAtNewLine();
        }

        public void Visit(GMacConstant constant)
        {
            Log.AppendAtNewLine("constant ");
            Log.Append(constant.SymbolAccessName);
            Log.Append(" = ");

            constant.AssociatedValue.AcceptVisitor(this);

            Log.AppendLineAtNewLine();
        }

        public void Visit(GMacFrameSubspace subspace)
        {
            Log.AppendAtNewLine("subspace ");
            Log.Append(subspace.ObjectName);
            Log.Append(" = @");

            Log.Append(
                Generate_StringList(
                    subspace
                    .SubspaceSignaturePattern
                    .TrueIndexes
                    .Select(id => subspace.ParentFrame.BasisBladeName(id))
                    )
                );

            Log.Append("@");

            Log.AppendAtNewLine();
        }

        public void Visit(GMacFrame frame)
        {
            Log.AppendAtNewLine("frame ");
            Log.Append(frame.SymbolQualifiedName);

            Log.Append("(");

            Log.Append(
                Generate_StringList(frame.BasisVectorNames)
                );

            Log.Append(")");

            Log.IncreaseIndentation();

            Log.AppendAtNewLine("signature ");

            Log.Append("IPM ' ");
            Log.Append(frame.AssociatedSymbolicFrame.Ipm.ToString());
            Log.AppendLine(" '");

            Log.AppendLineAtNewLine();

            foreach (var itam in frame.FrameSubspaces)
                itam.AcceptVisitor(this);

            Log.DecreaseIndentation();

            Log.AppendLineAtNewLine();

            foreach (var itam in frame.ChildConstants)
                itam.AcceptVisitor(this);

            foreach (var itam in frame.Structures)
                itam.AcceptVisitor(this);

            foreach (var itam in frame.ChildMacros)
                itam.AcceptVisitor(this);
        }

        //public void Visit(GMacAccessScheme access_scheme)
        //{
        //    this.Log.AppendAtNewLine("access ");
        //    this.Log.AppendLine(access_scheme.SymbolAccessName);
        //    this.Log.AppendLine("begin");

        //    this.Log.IncreaseIndentation();

        //    foreach (var language_name in GMacTargetCodeGenerator.AllowedTargetNames)
        //    {
        //        string template = access_scheme.GetTemplate(language_name);

        //        if (String.IsNullOrEmpty(template) == false)
        //        {
        //            this.Log.AppendAtNewLine(language_name);
        //            this.Log.Append(" @\"");
        //            this.Log.Append(template);
        //            this.Log.Append("\"");
        //        }
        //    }

        //    this.Log.DecreaseIndentation();

        //    this.Log.AppendLineAtNewLine("end");
        //}

        //public void Visit(GMacCommandBind command)
        //{
        //    this.Log.AppendAtNewLine("bind ");
            
        //    this.Visit(command.ValueAccess);

        //    this.Log.Append(" to");

        //    if (command.IsConstant)
        //    {
        //        this.Log.Append(" constant ");
        //        command.AccessConstantValue.AcceptVisitor(this);
        //    }
        //    else
        //    {
        //        string access_name = "@\"" + command.AccessName + "\"";

        //        if (command.HasAccessScheme)
        //        {
        //            this.Log.Append(" ");
        //            this.Log.Append(command.AccessScheme.SymbolAccessName);
        //            this.Log.Append("(");
        //            this.Log.Append(access_name);
        //            this.Log.Append(")");
        //        }
        //        else
        //        {
        //            this.Log.Append(" ");
        //            this.Log.Append(access_name);
        //        }

        //        if (command.HasAccessBinding)
        //        {
        //            this.Log.Append(" with ");
        //            this.Log.Append(command.AccessBinding.SymbolAccessName);
        //        }
        //    }

        //    this.Log.AppendAtNewLine();
        //}

        //public void Visit(GMacBinding binding)
        //{
        //    this.Log.AppendAtNewLine("binding ");
        //    this.Log.Append(binding.SymbolAccessName);
        //    this.Log.Append(" on ");
        //    this.Log.AppendLine(binding.BindingSymbol.SymbolAccessName);
        //    this.Log.AppendLine("begin");

        //    this.Log.IncreaseIndentation();

        //    foreach (var command in binding.BindingCommands)
        //        this.Visit(command);

        //    this.Log.DecreaseIndentation();

        //    this.Log.AppendLineAtNewLine("end");

        //    //if (binding.IsMacroBinding)
        //    //{
        //    //    this.Log.AppendLineAtNewLine("Begin Generated Code");

        //    //    this.Log.IncreaseIndentation();

        //    //    var gen = new CodeGenerator.CSharpCodeGenerator(binding, true);

        //    //    this.Log.Append(gen.WriteCode());

        //    //    this.Log.DecreaseIndentation();

        //    //    this.Log.AppendLineAtNewLine("End Generated Code");
        //    //}
        //}

        public void Visit(GMacNamespace nameSpace)
        {
            Log.AppendAtNewLine("namespace ");
            Log.AppendLine(nameSpace.SymbolAccessName);
            Log.AppendLine();

            foreach (var itam in nameSpace.ChildFrames)
                itam.AcceptVisitor(this);

            foreach (var itam in nameSpace.ChildConstants)
                itam.AcceptVisitor(this);

            foreach (var itam in nameSpace.ChildStructures)
                itam.AcceptVisitor(this);

            foreach (var itam in nameSpace.ChildTransforms)
                itam.AcceptVisitor(this);

            foreach (var itam in nameSpace.ChildMacros)
                itam.AcceptVisitor(this);

            foreach (var itam in nameSpace.ChildMacroTemplates)
                itam.AcceptVisitor(this);

            //foreach (var itam in name_space.AccessSchemes)
            //    itam.AcceptVisitor(this);

            //foreach (var itam in name_space.Bindings)
            //    itam.AcceptVisitor(this);

            foreach (var itam in nameSpace.ChildNamespaces)
                itam.AcceptVisitor(this);
        }

        public override void Visit(IronyAst dsl)
        {
            var gmacDsl = (GMacAst)dsl;

            foreach (var itam in gmacDsl.ChildNamespaces)
                itam.AcceptVisitor(this);
        }
    }
}
