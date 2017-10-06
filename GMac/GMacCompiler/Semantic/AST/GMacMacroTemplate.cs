using GMac.GMacCompiler.Semantic.ASTConstants;
using Irony.Parsing;
using IronyGrammars.Semantic.Scope;
using IronyGrammars.Semantic.Symbol;

namespace GMac.GMacCompiler.Semantic.AST
{
    /// <summary>
    /// A GMac macro template
    /// </summary>
    public sealed class GMacMacroTemplate : LanguageSymbol
    {
        /// <summary>
        /// The parse tree node associated with the macro template
        /// </summary>
        internal ParseTreeNode TemplateParseNode { get; private set; }

        internal GMacAst GMacRootAst => (GMacAst)RootAst;


        internal GMacMacroTemplate(string templateName, LanguageScope parentScope, ParseTreeNode templateParseNode)
            : base(templateName, parentScope, RoleNames.MacroTemplate)
        {
            TemplateParseNode = templateParseNode;
        }
    }
}
