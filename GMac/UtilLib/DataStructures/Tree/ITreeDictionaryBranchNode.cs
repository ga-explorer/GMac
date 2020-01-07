using System.Collections.Generic;

namespace UtilLib.DataStructures.Tree
{
    public interface ITreeDictionaryBranchNode<TKey> : ITreeBranchNode, IDictionary<TKey, ITreeNode>
    {
    }
}
