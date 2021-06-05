using CodeComposerLib.Languages;
using CodeComposerLib.Languages.Mathematica;
using GMac.Engine.API.CodeBlock;

namespace GMac.Engine.API.Target
{
    public abstract class GMacMathematicaExpressionConverter : LanguageExpressionConverter
    {
        /// <summary>
        /// The code block containing a dictionary used to convert low-level variables names into 
        /// target code variables names to be used during expression conversion to target code
        /// </summary>
        public GMacCodeBlock ActiveCodeBlock { get; set; }


        protected GMacMathematicaExpressionConverter(LanguageInfo targetLanguageInfo)
            : base(targetLanguageInfo, MathematicaUtils.Mathematica7Info)
        {
            
        }
    }
}
