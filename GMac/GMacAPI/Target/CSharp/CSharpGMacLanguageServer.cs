using GMac.GMacCompiler.Semantic.AST.Extensions;
using IronyGrammars.Semantic.Type;
using TextComposerLib.Code.Languages.CSharp;

namespace GMac.GMacAPI.Target.CSharp
{
    public sealed class CSharpGMacLanguageServer : GMacLanguageServer
    {
        internal CSharpGMacLanguageServer(CSharpCodeGenerator codeGenerator, CSharpSyntaxFactory syntaxFactory)
            : base(codeGenerator, syntaxFactory)
        {
            ExpressionConverter = new MathematicaToCSharpConverter();
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
