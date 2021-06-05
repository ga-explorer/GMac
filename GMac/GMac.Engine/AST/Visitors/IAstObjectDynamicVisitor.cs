

using DataStructuresLib;

namespace GMac.Engine.AST.Visitors
{
    public interface IAstObjectDynamicVisitor : IDynamicTreeVisitor<IAstObject>
    {
    }

    public interface IAstObjectDynamicVisitor<out TReturnValue> : IDynamicTreeVisitor<IAstObject, TReturnValue>
    {
    }
}
