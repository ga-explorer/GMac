using System.Linq;
using Irony;
using Irony.Parsing;
using IronyGrammars.SourceCode;

namespace GMac.GMacCompiler.Syntax
{
    internal static class GMacSourceParser
    {
        private static readonly GMacGrammar GMacIronyGrammar = new GMacGrammar(GMacGrammarRootType.Normal);
        private static readonly LanguageData GMacIronyLangData = new LanguageData(GMacIronyGrammar);
        private static readonly Parser GMacIronyParser = new Parser(GMacIronyLangData);//(GMacIronyGrammar);

        private static readonly GMacGrammar GMacExpressionIronyGrammar = new GMacGrammar(GMacGrammarRootType.Expression);
        private static readonly LanguageData GMacExpressionIronyLangData = new LanguageData(GMacExpressionIronyGrammar);
        private static readonly Parser GMacExpressionIronyParser = new Parser(GMacExpressionIronyLangData);//(GMacExpressionIronyGrammar);

        private static readonly GMacGrammar GMacQualifiedItemIronyGrammar = new GMacGrammar(GMacGrammarRootType.QualifiedItem);
        private static readonly LanguageData GMacQualifiedItemIronyLangData = new LanguageData(GMacQualifiedItemIronyGrammar);
        private static readonly Parser GMacQualifiedItemIronyParser = new Parser(GMacQualifiedItemIronyLangData);//(GMacQualifiedItemIronyGrammar);

        private static readonly GMacGrammar GMacMacroIronyGrammar = new GMacGrammar(GMacGrammarRootType.Macro);
        private static readonly LanguageData GMacMacroIronyLangData = new LanguageData(GMacMacroIronyGrammar);
        private static readonly Parser GMacMacroIronyParser = new Parser(GMacMacroIronyLangData);//(GMacMacroIronyGrammar);

        private static readonly GMacGrammar GMacStructureIronyGrammar = new GMacGrammar(GMacGrammarRootType.Structure);
        private static readonly LanguageData GMacStructureIronyLangData = new LanguageData(GMacStructureIronyGrammar);
        private static readonly Parser GMacStructureIronyParser = new Parser(GMacStructureIronyLangData);//(GMacStructureIronyGrammar);

        private static readonly GMacGrammar GMacCommandsIronyGrammar = new GMacGrammar(GMacGrammarRootType.Commands);
        private static readonly LanguageData GMacCommandsIronyLangData = new LanguageData(GMacCommandsIronyGrammar);
        private static readonly Parser GMacCommandsIronyParser = new Parser(GMacCommandsIronyLangData);//(GMacCommandsIronyGrammar);


        private static ParseTreeNode Parse(Parser ironyParser, string sourceCode, LanguageCompilationLog compilationLog)
        {
            var parseTree = ironyParser.Parse(sourceCode);

            if (!parseTree.HasErrors())
                return parseTree.Root;

            var messageList =
                parseTree
                .ParserMessages
                .Where(message => message.Level == ErrorLevel.Error);

            foreach (var message in messageList)
                return compilationLog.RaiseParserError<ParseTreeNode>(message.ToString(), message.Location.Position);

            return parseTree.Root;
        }

        public static ParseTreeNode ParseAst(string sourceCode, LanguageCompilationLog compilationLog)
        {
            return Parse(GMacIronyParser, sourceCode, compilationLog);
        }

        public static ParseTreeNode ParseStructure(string sourceCode, LanguageCompilationLog compilationLog)
        {
            return Parse(GMacStructureIronyParser, sourceCode, compilationLog);
        }

        public static ParseTreeNode ParseMacro(string sourceCode, LanguageCompilationLog compilationLog)
        {
            return Parse(GMacMacroIronyParser, sourceCode, compilationLog);
        }

        public static ParseTreeNode ParseCommands(string sourceCode, LanguageCompilationLog compilationLog)
        {
            return Parse(GMacCommandsIronyParser, sourceCode, compilationLog);
        }

        public static ParseTreeNode ParseExpression(string sourceCode, LanguageCompilationLog compilationLog)
        {
            return Parse(GMacExpressionIronyParser, sourceCode, compilationLog);
        }

        public static ParseTreeNode ParseQualifiedItem(string sourceCode, LanguageCompilationLog compilationLog)
        {
            return Parse(GMacQualifiedItemIronyParser, sourceCode, compilationLog);
        }


        public static string PrintNonTerminals()
        {
            return ParserDataPrinter.PrintNonTerminals(GMacIronyLangData);
        }

        public static string PrintTerminals()
        {
            return ParserDataPrinter.PrintTerminals(GMacIronyLangData);
        }
    }
}
