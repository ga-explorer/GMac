using TextComposerLib;

namespace GMac.GMacAST.Visitors
{
    public interface IAstObjectDynamicVisitor : IDynamicTreeVisitor<IAstObject>
    {
    }

    public interface IAstObjectDynamicVisitor<out TReturnValue> : IDynamicTreeVisitor<IAstObject, TReturnValue>
    {
    }
}
