using GMac.GMacAST.Symbols;
using UtilLib.DataStructures.Dependency;

namespace GMac.GMacAST.Dependency
{
    public sealed class AstMacroDependencyGraph : DependencyGraph<string, AstMacro>
    {
        public AstRoot Root { get; }


        internal AstMacroDependencyGraph(AstRoot astRoot)
        {
            Root = astRoot;
        }


        public override string ItemToKey(AstMacro item)
        {
            return item.AccessName;
        }

        public override void Populate()
        {
            Clear();

            foreach (var callingMacro in Root.Macros)
                AddDependencies(callingMacro, callingMacro.CalledMacros);
        }
    }
}
