using CodeComposerLib.Irony.Semantic.Type;
using CodeComposerLib.Languages.GMacDSL;
using TextComposerLib.Text.Linear;
using Wolfram.NETLink;

namespace GMac.Engine.API.Target.GMacDSL
{
    public sealed class GMacDslGMacLanguageServer : GMacLanguageServer
    {
        public override string DefaultFileExtension => "gmac";

        internal GMacDslGMacLanguageServer(CclGMacDslCodeGenerator codeComposer, CclGMacDslSyntaxFactory syntaxFactory)
            : base(codeComposer, syntaxFactory)
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
