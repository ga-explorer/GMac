namespace UtilLib.DataStructures.Tree
{
    public interface ITreeDictionaryBranchChildNode<TKey> : ITreeBranchChildNode
    {
        ITreeDictionaryBranchNode<TKey> ParentDictionaryNode { get; }

        TKey Key { get; }
    }
}
