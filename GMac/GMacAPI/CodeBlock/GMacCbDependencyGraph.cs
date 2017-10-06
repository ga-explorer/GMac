using System.Linq;
using UtilLib.DataStructures.Dependency;

namespace GMac.GMacAPI.CodeBlock
{
    /// <summary>
    /// This class holds dependency information of low-level variables inside a code block
    /// </summary>
    public sealed class GMacCbDependencyGraph : DependencyGraph<string, GMacCbVariable>
    {
        public GMacCodeBlock CodeBlock { get; }


        internal GMacCbDependencyGraph(GMacCodeBlock codeBlock)
        {
            CodeBlock = codeBlock;
        }


        public override string ItemToKey(GMacCbVariable item)
        {
            return item.LowLevelName;
        }

        public override void Populate()
        {
            Clear();

            foreach (var cbVar in CodeBlock.Variables)
            {
                var inputVar = cbVar as GMacCbInputVariable;

                if (ReferenceEquals(inputVar, null) == false)
                {
                    ItemToDependency(cbVar);

                    continue;
                }

                var computedVar = (GMacCbComputedVariable) cbVar;

                AddDependencies(
                    computedVar, 
                    computedVar.RhsVariables.Distinct().Cast<GMacCbVariable>()
                    );
            }
        }
    }
}
