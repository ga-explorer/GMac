using TextComposerLib;

namespace IronyGrammars.Semantic
{
    public interface IAstNodeDynamicVisitor : IDynamicTreeVisitor<IIronyAstObject>
    {
    }

    public interface IAstNodeDynamicVisitor<out TReturnValue> : IDynamicTreeVisitor<IIronyAstObject, TReturnValue>
    {
    }
}
