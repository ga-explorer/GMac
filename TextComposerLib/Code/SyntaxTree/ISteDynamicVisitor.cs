namespace TextComposerLib.Code.SyntaxTree
{
    public interface ISteDynamicVisitor : IDynamicTreeVisitor<ISyntaxTreeElement>
    {
    }

    public interface ISteDynamicVisitor<out TReturnValue> : IDynamicTreeVisitor<ISyntaxTreeElement, TReturnValue>
    {
    }
}
