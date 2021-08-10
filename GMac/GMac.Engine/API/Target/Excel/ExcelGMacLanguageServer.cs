using CodeComposerLib.Irony.Semantic.Type;
using CodeComposerLib.Languages.Excel;
using GMac.Engine.Compiler.Semantic.AST.Extensions;

namespace GMac.Engine.API.Target.Excel
{
    public sealed class ExcelGMacLanguageServer : GMacLanguageServer
    {
        public override string DefaultFileExtension => "xlsx";

        internal ExcelGMacLanguageServer(CclExcelCodeGenerator codeComposer, CclExcelSyntaxFactory syntaxFactory)
            : base(codeComposer, syntaxFactory)
        {
            ExpressionConverter = new MathematicaToExcelConverter();
        }

        public override string TargetTypeName(TypePrimitive itemType)
        {
            if (itemType.IsBoolean())
                return "bool";

            if (itemType.IsInteger())
                return "int";

            if (itemType.IsScalar())
                return CodeGenerator.ScalarTypeName;

            return @"/*<Unknown type>*/";
        }

    }
}