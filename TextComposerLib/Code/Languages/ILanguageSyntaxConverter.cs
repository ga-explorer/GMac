using TextComposerLib.Code.SyntaxTree;

namespace TextComposerLib.Code.Languages
{
    public interface ILanguageSyntaxConverter : ISteDynamicVisitor<ISyntaxTreeElement>
    {
        LanguageInfo SourceLanguageInfo { get; }

        LanguageInfo TargetLanguageInfo { get; }
    }
}