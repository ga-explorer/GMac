using CodeComposerLib.Irony.Semantic.Type;
using CodeComposerLib.Languages.Cpp;
using GMac.Engine.Compiler.Semantic.AST.Extensions;

namespace GMac.Engine.API.Target.Cpp
{
    // ReSharper disable once InconsistentNaming
    public sealed class GMacCppLanguageServer : GMacLanguageServer
    {
        public override string DefaultFileExtension => "cpp";

        internal GMacCppLanguageServer(CclCppCodeGenerator codeComposer, CclCppSyntaxFactory syntaxFactory)
            : base(codeComposer, syntaxFactory)
        {
            ExpressionConverter = new MathematicaToCppConverter();
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