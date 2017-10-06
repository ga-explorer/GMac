using System.Collections.Generic;

namespace UtilLib.DataStructures.Tree
{
    public interface ITreeBranchChildNode : ITreeNode
    {
        bool HasParentNode { get; }

        ITreeNode ParentNode { get; }

        IEnumerable<ITreeNode> AncestorNodes { get; }

        IEnumerable<ITreeNode> UpTreeNodes { get; }
    }
}
