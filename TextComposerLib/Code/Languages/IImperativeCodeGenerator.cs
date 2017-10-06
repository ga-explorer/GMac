using TextComposerLib.Code.SyntaxTree;
using TextComposerLib.Code.SyntaxTree.Expressions;

namespace TextComposerLib.Code.Languages
{
    public interface IImperativeCodeGenerator : ILanguageCodeGenerator
    {
        void Visit(SteDeclareFixedSizeArray code);

        void Visit(SteDeclareMethod code);

        void Visit(SteDeclareSimpleDataStore code);

        void Visit(SteIf code);

        void Visit(SteIfElse code);

        void Visit(SteIfElseIfElse code);

        void Visit(SteForLoop code);

        void Visit(SteForEachLoop code);

        void Visit(SteWhileLoop code);

        void Visit(SteImportNamespaces code);

        void Visit(SteSetNamespace code);

        void Visit(TccSwitchCaseItem code);

        void Visit(SteSwitchCase code);

        void Visit(SteThrowException code);

        void Visit(TccTryCatchItem code);

        void Visit(SteTryCatch code);

        void Visit(SteExpression code);
    }
}
