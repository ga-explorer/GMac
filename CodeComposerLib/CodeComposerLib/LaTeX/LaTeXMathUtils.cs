namespace CodeComposerLib.LaTeX
{
    public static class LaTeXMathUtils
    {
        public static string LaTeXMathAddParentheses(this string latexMathText)
        {
            return $@"\left( {latexMathText} \right)";
        }

        public static string LaTeXMathAddSquareBrackets(this string latexMathText)
        {
            return $@"\left[ {latexMathText} \right]";
        }

        public static string LaTeXMathAddCurlyBraces(this string latexMathText)
        {
            return $@"\left\{{ {latexMathText} \right\}}";
        }

        public static string LaTeXMathAddGroupBrackets(this string latexMathText)
        {
            return $@"\left\lgroup {latexMathText} \right\rgroup";
        }

        public static string LaTeXMathAddAngleBraces(this string latexMathText)
        {
            return $@"\left\langle {latexMathText} \right\rangle";
        }

        public static string LaTeXMathAddPipes(this string latexMathText)
        {
            return $@"\left| {latexMathText} \right|";
        }

        public static string LaTeXMathAddDoublePipes(this string latexMathText)
        {
            return $@"\left\| {latexMathText} \right\|";
        }

        public static string LaTeXMathAddCeilingBrackets(this string latexMathText)
        {
            return $@"\left\lceil {latexMathText} \right\rceil";
        }

        public static string LaTeXMathAddFloorBrackets(this string latexMathText)
        {
            return $@"\left\lfloor {latexMathText} \right\rfloor";
        }

        public static string LaTeXMathAddBrackets(this string latexMathText, string leftBracket, string rightBracket)
        {
            return $@"\left{leftBracket} {latexMathText} \right{rightBracket}";
        }
    }
}
