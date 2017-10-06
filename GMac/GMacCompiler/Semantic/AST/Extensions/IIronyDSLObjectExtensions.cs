using IronyGrammars.Semantic;

namespace GMac.GMacCompiler.Semantic.AST.Extensions
{
    public static class IronyDslObjectExtensions
    {
        /// <summary>
        /// The parent GMac DSL of this GMac symbol
        /// </summary>
        /// <param name="dslObject"></param>
        /// <returns></returns>
        internal static GMacAst GMacRootAst(this IIronyAstObject dslObject)
        {
            return (GMacAst)dslObject.RootAst;
        }
    }
}
