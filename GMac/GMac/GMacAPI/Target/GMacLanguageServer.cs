using CodeComposerLib.Irony.Semantic.Type;
using CodeComposerLib.Languages;
using CodeComposerLib.Languages.Cpp;
using CodeComposerLib.Languages.CSharp;
using CodeComposerLib.Languages.Excel;
using CodeComposerLib.Languages.GMacDSL;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica;
using GMac.GMacAPI.Target.Cpp;
using GMac.GMacAPI.Target.CSharp;
using GMac.GMacAPI.Target.Excel;
using GMac.GMacAPI.Target.GMacDSL;
using Wolfram.NETLink;

namespace GMac.GMacAPI.Target
{
    /// <summary>
    /// This class can be used to generate syntactically correct text code for some target language like
    /// comments, assignments, variable declarations, expressions, etc.
    /// </summary>
    public abstract class GMacLanguageServer : LanguageServer
    {
        public static GMacCSharpLanguageServer CSharp4()
        {
            return new GMacCSharpLanguageServer(
                CSharpUtils.CSharp4CodeGenerator(), 
                CSharpUtils.CSharp4SyntaxFactory()
                );
        }

        public static ExcelGMacLanguageServer Excel2007()
        {
            return new ExcelGMacLanguageServer(
                ExcelUtils.ExcelCodeGenerator(),
                ExcelUtils.ExcelSyntaxFactory()
            );
        }

        public static GMacCppLanguageServer Cpp11()
        {
            return new GMacCppLanguageServer(
                CppUtils.Cpp11CodeGenerator(), 
                CppUtils.Cpp11SyntaxFactory()
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

            var symbolicTextExpr = expr.ToSymbolicTextExpression();
            
            var targetExprTextTree = ExpressionConverter.Convert(symbolicTextExpr);

            return CodeGenerator.GenerateCode(targetExprTextTree);
        }
    }
}
