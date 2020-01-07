namespace UtilLib.DataStructures.Tree
{
    public interface ITreeNodeVisitor<out TResult>
    {
        TResult Visit(ITreeNode node);
    }
}
