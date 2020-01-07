namespace UtilLib.DataStructures.Tree
{
    public interface ITreeValueNode<out TValue> : ITreeNode
    {
        TValue Value { get; }
    }
}
