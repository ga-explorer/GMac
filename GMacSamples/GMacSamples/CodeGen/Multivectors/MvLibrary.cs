using System.Collections.Generic;
using System.Linq;
using GMac.GMacAPI.CodeGen;
using GMac.GMacAPI.Target;
using GMac.GMacAST;
using GMac.GMacAST.Symbols;
using TextComposerLib.Logs.Progress;

namespace GMacSamples.CodeGen.Multivectors
{
    public sealed class MvLibrary : GMacCodeLibraryComposer
    {
        #region Templates

        internal const string MultivectorTemplates = @"
delimiters # #

begin mv_class_file
using System;

namespace GMacModel.#frame#
{
    public sealed partial class #mv_class_name# : #frame#Multivector
    {
        #mv_class_code#
    }
}
end mv_class_file

";

        #endregion


        internal int MaxTargetLocalVars => 64;

        internal AstFrame CurrentFrame { get; private set; }

        internal string CurrentFrameName { get; private set; }

        internal Dictionary<int, MvClassData> MultivectorClassesData { get; }


        public override string Name => "Multivectors Library";

        public override string Description => "Generates a library for performing general GA operations on multivectors using a different class per multivector type";


        public MvLibrary(AstRoot ast)
            : base(ast, GMacLanguageServer.CSharp4())
        {
            MacroGenDefaults = new GMacMacroCodeComposerDefaults(this);

            MultivectorClassesData = new Dictionary<int, MvClassData>();
        }


        internal void SetTargetTempVariablesNames(GMacTargetVariablesNaming targetNaming)
        {
            //Temp variables target naming
            if (targetNaming.CodeBlock.TargetTempVarsCount > MaxTargetLocalVars)
            {
                //Name as array items
                targetNaming.SetTempVariables((int index) => "tempArray[" + index + "]");
            }
            else
            {
                //Name as set of local variables
                targetNaming.SetTempVariables(index => "tempVar" + index.ToString("X4") + "");
            }
        }

        private bool GeneratePreComputationsCode(GMacMacroCodeComposer macroCodeGen)
        {
            //Generate comments
            GMacMacroCodeComposer.DefaultGenerateCommentsBeforeComputations(macroCodeGen);

            //Temp variables declaration
            if (macroCodeGen.CodeBlock.TargetTempVarsCount > MaxTargetLocalVars)
            {
                //Add array declaration code
                macroCodeGen.SyntaxList.Add(
                    macroCodeGen.SyntaxFactory.DeclareLocalArray(
                        GMacLanguage.ScalarTypeName,
                        "tempArray",
                        macroCodeGen.CodeBlock.TargetTempVarsCount.ToString()
                        )
                    );

                macroCodeGen.SyntaxList.AddEmptyLine();
            }
            else
            {
                var tempVarNames =
                    macroCodeGen.CodeBlock
                    .TempVariables
                    .Select(item => item.TargetVariableName)
                    .Distinct();

                //Add temp variables declaration code
                foreach (var tempVarName in tempVarNames)
                    macroCodeGen.SyntaxList.Add(
                        macroCodeGen.SyntaxFactory.DeclareLocalVariable(GMacLanguage.ScalarTypeName, tempVarName)
                        );

                macroCodeGen.SyntaxList.AddEmptyLine();
            }
            
            return true;
        }

        private void GeneratePostComputationsCode(GMacMacroCodeComposer macroCodeGen)
        {
            //Generate comments
            GMacMacroCodeComposer.DefaultGenerateCommentsAfterComputations(macroCodeGen);
        }


        private void InitializeMultivectorClassesData()
        {
            MultivectorClassesData.Clear();

            var maxId = (1 << (CurrentFrame.VSpaceDimension + 1)) - 1;

            //Create the default names for multivector classes
            var classNames = Enumerable.Range(0, maxId + 1).Select(id => "Multivector" + id).ToArray();

            //Override some of the default names with special names for multivector classes
            classNames[0] = "Zero";
            classNames[1] = "Scalar";
            classNames[2] = "Vector";

            if (CurrentFrame.VSpaceDimension >= 4)
            {
                for (var grade = 2; grade <= CurrentFrame.VSpaceDimension - 2; grade++)
                    classNames[1 << grade] = "KVector" + grade;
            }

            if (CurrentFrame.VSpaceDimension >= 3)
                classNames[1 << (CurrentFrame.VSpaceDimension - 1)] = "PseudoVector";

            classNames[1 << CurrentFrame.VSpaceDimension] = "PseudoScalar";
            classNames[maxId] = "Full";


            //Create classes data
            for (var id = 0; id <= maxId; id++)
                MultivectorClassesData.Add(
                    id,
                    new MvClassData(
                        CurrentFrame,
                        id,
                        CurrentFrameName + classNames[id]
                        )
                    );
        }

        private void GenerateBaseMvClassFile()
        {
            CodeFilesComposer.InitalizeFile(CurrentFrameName + "Multivector.cs");

            BaseMvClassFileGenerator.Generate(this);

            CodeFilesComposer.UnselectActiveFile();
        }

        private void GenerateDerivedMvClassFiles(MvClassData classData)
        {
            CodeFilesComposer.DownFolder(classData.ClassName);

            CodeFilesComposer.InitalizeFile(classData.ClassName + ".cs");

            DerivedMvClassFileGenerator.Generate(this, classData);

            CodeFilesComposer.UnselectActiveFile();

            foreach (var classData2 in MultivectorClassesData.Values)
            {
                CodeFilesComposer.InitalizeFile(classData.ClassName + "Calc" + classData2.ClassId + ".cs");

                DerivedMvClassCalcFileGenerator.Generate(this, classData, classData2);

                CodeFilesComposer.UnselectActiveFile();
            }

            CodeFilesComposer.UpFolder();
        }

        private void GenerateFrameCode(AstFrame frameInfo)
        {
            Progress.Enabled = true;
            var progressId = this.ReportStart(
                "Generating code files for frame " + frameInfo.AccessName
                );

            CurrentFrame = frameInfo;

            CurrentFrameName = GetSymbolTargetName(CurrentFrame);

            CodeFilesComposer.DownFolder(CurrentFrameName);


            InitializeMultivectorClassesData();

            GenerateBaseMvClassFile();

            foreach (var classData in MultivectorClassesData.Values)
                GenerateDerivedMvClassFiles(classData);

            //GenerateFctoredBladeFiles();

            //GenerateOutermorphismFiles();


            CodeFilesComposer.UpFolder();

            Progress.Enabled = true;
            this.ReportFinish(progressId);
        }


        public override GMacCodeLibraryComposer CreateEmptyGenerator()
        {
            return new MvLibrary(Root);
        }

        public override IEnumerable<AstSymbol> GetBaseSymbolsList()
        {
            return Root.Frames;
        }

        protected override string GetSymbolTargetName(AstSymbol symbol)
        {
            return symbol.Name;
        }

        protected override bool InitializeTemplates()
        {
            Templates.Parse(MultivectorTemplates);

            //Template for encoding grade1 multivectors as variables by basis blade index
            //Templates.Add("vmv", new ParametricComposer("#", "#", "#Var##index#"));

            return true;
        }

        protected override void InitializeOtherComponents()
        {
            MacroGenDefaults.ActionBeforeGenerateComputations = GeneratePreComputationsCode;

            MacroGenDefaults.ActionAfterGenerateComputations = GeneratePostComputationsCode;
        }

        protected override bool VerifyReadyToGenerate()
        {
            return SelectedSymbols.All(s => s.IsValidFrame);
        }

        protected override void ComposeTextFiles()
        {
            var framesList = SelectedSymbols.Cast<AstFrame>();

            foreach (var frame in framesList)
                GenerateFrameCode(frame);
        }

        protected override void FinalizeOtherComponents()
        {
        }
    }
}
