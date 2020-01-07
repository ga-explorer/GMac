namespace UtilLib.DataStructures.Tree
{
    public interface ITreeNode
    {
        bool IsRootNode { get; }

        bool IsBranchNode { get; }

        bool IsLeafNode { get; }

        int NodeLevel { get; }

        //TResult AcceptVisitor<TResult>(ITreeNodeVisitor<TResult> visitor);
    }
}
