using System.Linq;
using DataStructuresLib.Dependency;

namespace GMac.Engine.AST.Dependency
{
    public sealed class AstTypeDependencyGraph : DependencyGraph<string, AstType>
    {
        public AstRoot Root { get; }


        internal AstTypeDependencyGraph(AstRoot astRoot)
        {
            Root = astRoot;
        }


        public override string ItemToKey(AstType item)
        {
            return item.GMacTypeSignature;
        }

        public override void Populate()
        {
            Clear();

            AddDependencies(
                Root.FrameMultivectors.Select(mvType => mvType.GMacType), 
                Root.ScalarType
                );

            foreach (var structType in Root.Structures)
                AddDependencies(structType.GMacType, structType.DataMemberTypes);
        }
    }
}
