using CodeComposerLib.Irony.Semantic.Type;
using CodeComposerLib.Languages.CSharp;
using GMac.Engine.Compiler.Semantic.AST.Extensions;

namespace GMac.Engine.API.Target.CSharp
{
    // ReSharper disable once InconsistentNaming
    public sealed class GMacCSharpLanguageServer : GMacLanguageServer
    {
        public override string DefaultFileExtension => "cs";

        internal GMacCSharpLanguageServer(CclCSharpCodeGenerator codeComposer, CclCSharpSyntaxFactory syntaxFactory)
            : base(codeComposer, syntaxFactory)
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
