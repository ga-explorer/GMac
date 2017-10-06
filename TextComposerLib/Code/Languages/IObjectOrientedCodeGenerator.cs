using TextComposerLib.Code.SyntaxTree;

namespace TextComposerLib.Code.Languages
{
    public interface IObjectOrientedCodeGenerator : ILanguageCodeGenerator
    {
        void Visit(SteDeclareDataStore code);

        void Visit(SteDeclareLanguageConstruct code);
    }
}