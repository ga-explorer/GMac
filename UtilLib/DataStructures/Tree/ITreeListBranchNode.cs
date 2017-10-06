using System.Collections.Generic;

namespace UtilLib.DataStructures.Tree
{
    public interface ITreeListBranchNode : ITreeBranchNode, IList<ITreeNode>
    {
    }
}
