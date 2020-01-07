using CodeComposerLib.Irony.Semantic.Type;
using CodeComposerLib.Languages.CSharp;
using GMac.GMacCompiler.Semantic.AST.Extensions;

namespace GMac.GMacAPI.Target.CSharp
{
    // ReSharper disable once InconsistentNaming
    public sealed class GMacCSharpLanguageServer : GMacLanguageServer
    {
        public override string DefaultFileExtension => "cs";

        internal GMacCSharpLanguageServer(CSharpCodeGenerator codeGenerator, CSharpSyntaxFactory syntaxFactory)
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
