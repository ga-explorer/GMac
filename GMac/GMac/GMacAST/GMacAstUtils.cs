using System;
using System.Linq;
using CodeComposerLib.GraphViz.Dot;
using CodeComposerLib.Irony.Semantic.Type;
using DataStructuresLib;
using GMac.GMacAPI.Binding;
using GMac.GMacAPI.CodeBlock;
using GMac.GMacAST.Dependency;
using GMac.GMacAST.Symbols;
using GMac.GMacAST.Visitors.Excel;
using GMac.GMacAST.Visitors.GraphViz;
using GMac.GMacCompiler.Semantic.AST;
using OfficeOpenXml;

namespace GMac.GMacAST
{
    public static class GMacAstUtils
    {
        internal static AstRoot ToAstRoot(this GMacAst ast)
        {
            return new AstRoot(ast);
        }

        internal static AstType ToAstType(this ILanguageType langType)
        {
            return new AstType(langType);
        }

        internal static AstFrameBasisBlade ToAstFrameBasisBlade(this GMacFrame frame, ulong id)
        {
            return new AstFrameBasisBlade(frame, id);
        }

        internal static AstFrameBasisBlade ToAstFrameBasisBlade(this GMacFrame frame, int grade, ulong index)
        {
            return new AstFrameBasisBlade(frame, grade, index);
        }


        public static bool IsNullOrInvalid(this AstObject astObj)
        {
            return ReferenceEquals(astObj, null) || astObj.IsInvalid;
        }

        public static bool IsNotNullAndValid(this AstObject astObj)
        {
            return !ReferenceEquals(astObj, null) && astObj.IsValid;
        }


        public static DotGraph ToGraphViz(this AstRoot ast)
        {
            var visitor = new AstToGraphViz();

            ast.AcceptVisitor(visitor);

            return visitor.Graph;
        }

        public static DotGraph ToGraphViz(this AstTypeDependencyGraph depGraph)
        {
            var visitor = new AstTypesDependencyToGraphViz();

            visitor.ToGraphViz(depGraph);

            return visitor.Graph;
        }

        public static DotGraph ToGraphViz(this AstMacroDependencyGraph depGraph)
        {
            var visitor = new AstMacrosDependencyToGraphViz();

            visitor.ToGraphViz(depGraph);

            return visitor.Graph;
        }

        public static DotGraph ToGraphViz(this AstFrame frame)
        {
            var visitor = new FrameToGraphViz();

            frame.AcceptVisitor(visitor);

            return visitor.Graph;
        }

        public static DotGraph ToGraphViz(this AstStructure structure)
        {
            var visitor = new StructureToGraphViz();

            structure.AcceptVisitor(visitor);

            return visitor.Graph;
        }

        public static DotGraph ToGraphViz(this AstType astType)
        {
            var visitor = new GMacTypeToGraphViz();

            astType.AcceptVisitor(visitor);

            return visitor.Graph;
        }

        public static DotGraph ToDependenciesGraphViz(this AstStructure astStruct)
        {
            var visitor = new TypeDependenciesToGraphViz();

            visitor.Visit(astStruct.GMacType);

            return visitor.Graph;
        }

        public static DotGraph ToDependenciesGraphViz(this AstFrameMultivector mvType)
        {
            var visitor = new TypeDependenciesToGraphViz();

            visitor.Visit(mvType.GMacType);

            return visitor.Graph;
        }

        public static DotGraph ToGraphViz(this AstMacro macro)
        {
            var visitor = new MacroToGraphViz();

            macro.AcceptVisitor(visitor);

            return visitor.Graph;
        }

        public static DotGraph ToDependenciesGraphViz(this AstMacro macro)
        {
            var visitor = new MacroDependenciesToGraphViz();

            visitor.Visit(macro);

            return visitor.Graph;
        }

        public static ExcelPackage ToExcel(this GMacCodeBlock codeBlock)
        {
            var visitor = new GMacCodeBlockToExcel(codeBlock);

            return visitor.ToExcel();
        }

        public static DotGraph ToGraphViz(this GMacCodeBlock codeBlock)
        {
            var visitor = new GMacCodeBlockToGraphViz(codeBlock);

            return visitor.ToGraphViz();
        }

        public static DotGraph ToLowLevelGraphViz(this AstMacro macro)
        {
            var random = new Random();

            var macroBinding = GMacMacroBinding.Create(macro);

            var lowLevelParameters = macro.InputParameters.SelectMany(p => p.DatastoreValueAccess.ExpandAll());
            foreach (var llp in lowLevelParameters)
                if (random.Next(1, 10) > -1)
                    macroBinding.BindToVariables(llp);

                else 
                    macroBinding.BindScalarToConstant(llp, 1);

            lowLevelParameters = macro.OutputParameter.DatastoreValueAccess.ExpandAll();
            foreach (var llp in lowLevelParameters)
                macroBinding.BindToVariables(llp);

            var codeBlock = macroBinding.CreateOptimizedCodeBlock();

            //var visitor = new MacroBindingToGraphViz(macroBinding, codeBlock);
            var visitor = new GMacCodeBlockToGraphViz(codeBlock);

            return visitor.ToGraphViz();
        }

    }
}
