using System.Collections.Generic;

namespace UtilLib.DataStructures.Tree
{
    public interface ITreeBranchNode : ITreeNode
    {
        bool HasChildNodes { get; }

        IEnumerable<ITreeNode> ChildNodes { get; }

        IEnumerable<ITreeNode> DescendantNodes { get; }

        IEnumerable<ITreeNode> DownTreeNodes { get; }
    }
}
