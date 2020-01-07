using CodeComposerLib.Irony.Semantic.Type;
using CodeComposerLib.Languages.GMacDSL;
using TextComposerLib.Text.Linear;
using Wolfram.NETLink;

namespace GMac.GMacAPI.Target.GMacDSL
{
    public sealed class GMacDslGMacLanguageServer : GMacLanguageServer
    {
        public override string DefaultFileExtension => "gmac";

        internal GMacDslGMacLanguageServer(GMacDslCodeGenerator codeGenerator, GMacDslSyntaxFactory syntaxFactory)
            : base(codeGenerator, syntaxFactory)
        {
        }


        public override string TargetTypeName(TypePrimitive itemType)
        {
            return itemType.SymbolAccessName;
        }

        public override string GenerateCode(Expr expr)
        {
            var textComposer = new LinearTextComposer();

            return textComposer.Append("@\"").Append(expr.ToString()).Append("\"").ToString();
        }
    }
}
