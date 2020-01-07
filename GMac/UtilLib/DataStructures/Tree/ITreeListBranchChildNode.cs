namespace UtilLib.DataStructures.Tree
{
    public interface ITreeListBranchChildNode : ITreeBranchChildNode
    {
        ITreeListBranchNode ParentListNode { get; }

        int Index { get; }
    }
}
