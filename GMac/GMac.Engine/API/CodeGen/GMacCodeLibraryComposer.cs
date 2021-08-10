using System.Collections.Generic;
using CodeComposerLib;
using CodeComposerLib.Irony.Semantic;
using CodeComposerLib.Languages;
using GMac.Engine.API.Target;
using GMac.Engine.AST;
using GMac.Engine.AST.Symbols;
using TextComposerLib.Loggers.Progress;

namespace GMac.Engine.API.CodeGen
{
    /// <summary>
    /// This class represents a text generator that can access information from a GMacAST to generate text into
    /// code files in a base output folder. This class should be the base for all GMac-based code generation processes
    /// </summary>
    public abstract class GMacCodeLibraryComposer : 
        CclCodeLibraryComposerBase
    {
        public override string ProgressSourceId => Name;

        public override ProgressComposer Progress => GMacEngineUtils.Progress;

        /// <summary>
        /// The GMacAST object.
        /// </summary>
        public AstRoot Root { get; }

        public override CclLanguageServerBase Language => GMacLanguage;

        /// <summary>
        /// The GMac target language of this generator
        /// </summary>
        public GMacLanguageServer GMacLanguage { get; }

        /// <summary>
        /// The defaults used in initializing macro code generators used in this class. This member should
        /// be created and initialized in the constructor of the code library generator class
        /// </summary>
        public GMacMacroCodeComposerDefaults MacroGenDefaults { get; protected set; }

        /// <summary>
        /// A dictionary of language symbols that will be used during generation. For example it can be
        /// used to prevent generation of certain symbols or to only generate text for certain symbols.
        /// If this dictionary is empty all symbols are assumed to be selected
        /// </summary>
        public AstSymbolsCollection SelectedSymbols { get; private set; }


        /// <summary>
        /// All derived classes must take a single AstRoot parameter for uniform operation purposes
        /// </summary>
        /// <param name="ast"></param>
        /// <param name="targetLanguage"></param>
        protected GMacCodeLibraryComposer(AstRoot ast, GMacLanguageServer targetLanguage)
        {
            Root = ast;
            GMacLanguage = targetLanguage;
            SelectedSymbols = new AstSymbolsCollection(Root);
        }


        /// <summary>
        /// Create an un-initialized copy of this library generator
        /// </summary>
        /// <returns></returns>
        public abstract GMacCodeLibraryComposer CreateEmptyComposer();

        /// <summary>
        /// This method can be used to select the AST symbols that code generation can start from
        /// For example some library may generate code starting from a frame definition so in the UI
        /// we need to let the library decide on using frames only in order to select one frame to
        /// start code generation
        /// </summary>
        public abstract IEnumerable<AstSymbol> GetBaseSymbolsList();

        /// <summary>
        /// Given an AST symbol this method gets a target language object name for the symbol
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        protected abstract string GetSymbolTargetName(AstSymbol symbol);

        /// <summary>
        /// Create a macro code generator based on this library
        /// </summary>
        /// <param name="macro"></param>
        /// <returns></returns>
        public virtual GMacMacroCodeComposer CreateMacroCodeGenerator(AstMacro macro)
        {
            return new GMacMacroCodeComposer(MacroGenDefaults, macro);
        }

        /// <summary>
        /// Generates a single line comment on a separate line in the active file
        /// </summary>
        /// <param name="commentText"></param>
        public void GenerateComment(string commentText)
        {
            ActiveFileTextComposer.AppendLineAtNewLine(
                CodeComposer.GenerateCode(
                    SyntaxFactory.Comment(commentText)
                    )
                );
        }

        /// <summary>
        /// Return a unique name for the given AST object
        /// </summary>
        /// <param name="astObject"></param>
        /// <returns></returns>
        protected string GetUniqueName(IIronyAstObjectNamed astObject)
        {
            return astObject.ObjectName + astObject.ObjectId.ToString("X4");
        }


    }
}
