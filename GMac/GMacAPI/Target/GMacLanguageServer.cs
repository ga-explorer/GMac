using GeometricAlgebraSymbolicsLib.Cas.Mathematica;
using GMac.GMacAPI.Target.CSharp;
using GMac.GMacAPI.Target.GMacDSL;
using IronyGrammars.Semantic.Type;
using TextComposerLib.Code.Languages;
using TextComposerLib.Code.Languages.CSharp;
using TextComposerLib.Code.Languages.GMacDSL;
using Wolfram.NETLink;

namespace GMac.GMacAPI.Target
{
    /// <summary>
    /// This class can be used to generate syntaictically correct text code for some target language like
    /// comments, assignments, variable declarations, expressions, etc.
    /// </summary>
    public abstract class GMacLanguageServer : LanguageServer
    {
        public static CSharpGMacLanguageServer CSharp4()
        {
            return new CSharpGMacLanguageServer(
                CSharpUtils.CSharp4CodeGenerator(), 
                CSharpUtils.CSharp4SyntaxFactory()
                );
        }

        //public static VbDotNetLanguage VbDotNet()
        //{
        //    return new VbDotNetLanguage();
        //}

        //public static FSharpLanguage FSharp()
        //{
        //    return new FSharpLanguage();
        //}

        //public static CppLanguage Cpp()
        //{
        //    return new CppLanguage();
        //}

        //public static JavaLanguage Java()
        //{
        //    return new JavaLanguage();
        //}

        //public static ScalaLanguage Scala()
        //{
        //    return new ScalaLanguage();
        //}

        //public static PythonLanguage Python()
        //{
        //    return new PythonLanguage();
        //}
        
        public static GMacDslGMacLanguageServer GMacDsl()
        {
            return new GMacDslGMacLanguageServer(
                GMacDslUtils.GMacDslCodeGenerator(),
                GMacDslUtils.GMacDslSyntaxFactory()
                );
        }


        /// <summary>
        /// This can be used for converting symbolic expressions into target language expressions
        /// </summary>
        public GMacMathematicaExpressionConverter ExpressionConverter { get; protected set; }


        protected GMacLanguageServer(LanguageCodeGenerator codeGenerator, LanguageSyntaxFactory syntaxFactory)
            : base(codeGenerator, syntaxFactory)
        {
        }


        /// <summary>
        /// Gets the target language type name equivalent to the given GMacDSL primitive type
        /// </summary>
        /// <param name="itemType"></param>
        /// <returns></returns>
        public abstract string TargetTypeName(TypePrimitive itemType);

        /// <summary>
        /// Convert the given symbolic expression object into target language expression using the
        /// internal ExpressionConverter object if possible
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        public virtual string GenerateCode(Expr expr)
        {
            if (ReferenceEquals(ExpressionConverter, null))
                return expr.ToString();

            var symbolicExprTextTree = expr.ToTextExpressionTree();
            
            var targetExprTextTree = ExpressionConverter.Convert(symbolicExprTextTree);

            return CodeGenerator.GenerateCode(targetExprTextTree);
        }
    }
}
