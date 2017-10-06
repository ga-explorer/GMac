using TextComposerLib.Code.Languages;

namespace TextComposerLib.Code.SyntaxTree
{
    public class SteEmbeddedCode : SteSyntaxElement
    {
        public ILanguageSyntaxConverter LanguageConverter { get; private set; }

        public ISyntaxTreeElement Code { get; set; }


        public SteEmbeddedCode(ILanguageSyntaxConverter langConverter)
        {
            LanguageConverter = langConverter;
        }
    }
}
